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
        public readonly static string _outputFilePath = @"..\..\..\Data\DataExtractor_Example_Output_" + Guid.NewGuid().ToString() + ".csv";

        static void Main(string[] args)
        {
            var _filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), _inputFilePath);

            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IFileManager, CsvFileManager>()
                .BuildServiceProvider();

             
            var fileManager = serviceProvider.GetService<IFileManager>();
            
            fileManager.Process(_inputFilePath,_outputFilePath);

            Console.WriteLine("Processing Completed.");
            Console.ReadLine();


        }
    }
}
