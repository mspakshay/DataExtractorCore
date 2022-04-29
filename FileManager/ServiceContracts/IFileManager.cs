using CsvHelper.Configuration;
using FileManager.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;


namespace FileManager.ServiceContracts
{
    public interface IFileManager
    {
        List<Transaction> ReadFile(string filePath);
        bool WriteToFile(string directoryPath, List<Output> data);
        List<Output> ProcessRecords(List<Transaction> data);
        bool Process(string inputFilePath, string outputFilePath);
    }
}
