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

namespace FileManager.Services
{
    public class CsvFileManager : IFileManager
    {
        private static CsvConfiguration _config;

        public CsvFileManager()
        {
        }

        public CsvConfiguration SetupConfig(bool isHeaderPresent, CultureInfo cultureInfo)
        {
            if (_config == null)
            {
                _config = new CsvConfiguration(cultureInfo)
                {
                    HasHeaderRecord = isHeaderPresent,//true if firstline has header value
                    PrepareHeaderForMatch = args => args.Header.ToLower()
                };
            }

            return _config;
        }

        public List<Transaction> ReadFile(string filePath)
        {
            SetupConfig(true, CultureInfo.InvariantCulture);

            List<Transaction> transactions = new List<Transaction>();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, _config))
            {
                csv.Context.RegisterClassMap<TransactionMap>();
                reader.ReadLine();//Skip first line. Timezone=UTC
                transactions = csv.GetRecords<Transaction>().ToList();
            }

            return transactions;
        }

        public void WriteToFile(string filePath)
        {
            throw new NotImplementedException();
        }

        public void Display(string filePath)
        {
            var records = ReadFile(filePath);

            foreach (var item in records)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public void ProcessRecord(Transaction data)
        {
            var algoParamsDict = CleanupData(data.AlgoParams);
            Console.WriteLine("Data= " + " ISIN:" + data.ISIN + " CFICode:" + data.CFICode + " Venue:" + data.Venue + " ContractSize:"+algoParamsDict["PriceMultiplier"]);
        }

        private Dictionary<string, dynamic> CleanupData(string algoParams)
        {
            var arr = algoParams.Replace("|", string.Empty).Split(";");

            Dictionary<string, dynamic> algoParamsData = new Dictionary<string, dynamic>();
            foreach (var item in arr)
            {
                var d = item.Split(":");
                if (!algoParamsData.ContainsKey(d[0]))
                {
                    algoParamsData.Add(d[0], d[1]);
                }
                else
                {
                    throw new Exception("Duplicate Key");
                }
                    
            }

            return algoParamsData;
        }
    }
}
