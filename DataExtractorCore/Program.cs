using FileManager.ServiceContracts;
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
        public readonly static string _inputFilePath = @"..\..\..\Data\DataExtractor_Example_Input.csv";
        public readonly static string _outputFilePath = @"..\..\..\Data";

        static void Main(string[] args)
        {

            //DI Setup
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IFileManager, CsvFileManager>()
                .BuildServiceProvider();

            var fileManager = serviceProvider.GetService<IFileManager>();

            try
            {
                var result = fileManager.Process(_inputFilePath, _outputFilePath);
                Console.WriteLine("Processing Completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
