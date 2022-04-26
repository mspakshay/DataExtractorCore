using FileManager.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace DataExtractorCore
{
    class Program
    {
        public static string _filePath;
        
        static void Main(string[] args)
        {
            _filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\..\Data\DataExtractor_Example_Input.csv");

            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IFileManager, CsvFileManager>()
                .BuildServiceProvider();

             
            var fileManager = serviceProvider.GetService<IFileManager>();

            fileManager.SetupConfig(isHeaderPresent: true, cultureInfo: CultureInfo.InvariantCulture);
            
            //Read Data
            var transactions = fileManager.ReadFile(_filePath);

            //Process records
            foreach (var data in transactions)
            {
                fileManager.ProcessRecord(data);  
            }
            Console.ReadLine();


        }
    }
}
