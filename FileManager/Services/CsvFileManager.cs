using CsvHelper;
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
using System.ComponentModel.DataAnnotations;

namespace FileManager.Services
{
    public class CsvFileManager : IFileManager
    {
        private static CsvConfiguration _config;

        /// <summary>
        /// Initialized _config for CsvReader & CsvWriter
        /// </summary>
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

        /// <summary>
        /// Reads files present at the path provided.
        /// Exceptions :
        /// FileNotFoundException,
        /// Exception
        /// </summary>
        /// <param name="filePath">path of file to be read</param>
        /// <returns>List of transaction records present in the file</returns>
        public List<Transaction> ReadFile(string filePath)
        {
            List<Transaction> transactions = new List<Transaction>();

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

        /// <summary>
        /// Writes to file. Creates file in the specified folder path, if not present.
        /// Execptions : 
        /// DirectoryNotFoundException, ArgumentException, Exception
        /// </summary>
        /// <param name="directoryPath">Output Folder Location</param>
        /// <param name="data">List of processed records</param>
        /// <returns>true if write operation ios completed, else false</returns>
        public bool WriteToFile(string directoryPath, List<Output> data)
        {
            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException("Directory not Found:" + directoryPath);
            }

            foreach (var item in data)
            {
                var validationErrors = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(item
                    , new ValidationContext(item)
                    , validationErrors, true);

                if (!isValid)
                {
                    throw new ArgumentException("Invalid Input Object");
                }
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
                throw new Exception("Error while writing to file:" + outputFileName);
            }
            return result;
        }

        /// <summary>
        /// Performs operation on list of tramsaction records.
        /// </summary>
        /// <param name="data">List of transactions</param>
        /// <returns>List of processed records</returns>
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

        /// <summary>
        /// Reads contract size from algo parameters provided in the input
        /// </summary>
        /// <param name="algoParams">string containing list of parameters</param>
        /// <returns>AlgoParams Object</returns>
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

        /// <summary>
        /// Reads input from input file, performs process operation, writes processed records to output file in the provided output file path.
        /// </summary>
        /// <param name="inputFilePath">input file path</param>
        /// <param name="outputFilePath">output file path</param>
        public bool Process(string inputFilePath, string outputFilePath)
        {
            bool result = false;
            try
            {
                //Read Data
                var transactions = ReadFile(inputFilePath);

                //Process records
                var output = ProcessRecords(transactions);

                //Write To File
                result = WriteToFile(outputFilePath, output);
            }
            catch (Exception)
            {
                //Add logging here
                throw new Exception("Error while processing!");
            }

            return result;
        }
    }
}
