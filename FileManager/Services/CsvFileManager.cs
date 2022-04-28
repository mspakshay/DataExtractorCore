﻿using CsvHelper;
using CsvHelper.Configuration;
using FileManager.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using FileManager.ServiceContracts;

namespace FileManager.Services
{
    public class CsvFileManager : IFileManager
    {
        public const string PriceMultiplier = "PriceMultiplier";
        private static CsvConfiguration _config;

        public CsvFileManager()
        {
            if (_config == null)
            {
                _config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,//true if firstline has header value
                    PrepareHeaderForMatch = args => args.Header.ToLower()
                };
            }
        }

        public List<Transaction> ReadFile(string filePath)
        {
            List<Transaction> transactions = new List<Transaction>(); ;

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("Invalid Argument : filePath ");
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }

            try
            {
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, _config))
                {
                    csv.Context.RegisterClassMap<TransactionMap>();
                    reader.ReadLine();//Skip first line. Timezone=UTC
                    transactions = csv.GetRecords<Transaction>().ToList();
                }
            }
            catch (Exception)
            {
                //we can log exception here
                throw new Exception("Error while reading file");
            }

            return transactions;
        }

        public bool WriteToFile(string directoryPath, List<Output> data)
        {
            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException("Directory not Found:" + directoryPath);
            }

            bool result = false;
            string outputFileName = $"DataExtractor_Example_Output_{Guid.NewGuid().ToString()}.csv";

            try
            {
                
                using (var writer = new StreamWriter(string.Format("{0}\\{1}", directoryPath, outputFileName)))
                using (var csv = new CsvWriter(writer, _config))
                {
                    csv.Context.RegisterClassMap<OutputMap>();
                    csv.WriteHeader<Output>();
                    csv.NextRecord();
                    csv.WriteRecords(data);

                    result = true;
                }
            }
            catch (Exception)
            {
                //Log exceptions here
                throw new Exception("Error while writing to file:"+ outputFileName);
            }
            return result;
        }

        public List<Output> ProcessRecords(List<Transaction> data)
        {
            var outputList = new List<Output>();
            foreach (var item in data)
            {
                var algoParamsObj = ReadContractSize(item.AlgoParams);

                outputList.Add(
                    new Output
                    {
                        ISIN = item.ISIN,
                        CFICode = item.CFICode,
                        Venue = item.Venue,
                        ContractSize = algoParamsObj?.PriceMultiplier ?? 0
                    }
                );                
            }

            return outputList;
        }

        private AlgoParams ReadContractSize(string algoParams)
        {
            AlgoParams algoParamsObj = null;
            try
            {
                var arr = algoParams.Replace("|", string.Empty).Split(";");

                Dictionary<string, string> algoParamsData = new Dictionary<string, string>();
                foreach (var item in arr)
                {
                    var d = item.Split(":");
                    if (!algoParamsData.ContainsKey(d[0]))
                    {
                        algoParamsData.Add(d[0], d[1]);
                    }
                }

                var jsonAlgoParams = JsonConvert.SerializeObject(algoParamsData);
                algoParamsObj = JsonConvert.DeserializeObject<AlgoParams>(jsonAlgoParams);

            }
            catch (Exception)
            {
                //log exception here
                throw new Exception("Error while reading contract size");
            }
            return algoParamsObj;
        }

        public void Process(string inputFilePath, string outputFilePath)
        {
            try
            {
                //Read Data
                var transactions = ReadFile(inputFilePath);

                //Process records
                var output = ProcessRecords(transactions);

                //Write To File
                WriteToFile(outputFilePath, output);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
