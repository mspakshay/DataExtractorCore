using CsvHelper.Configuration;
using FileManager.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;


namespace FileManager.Services
{
    public interface IFileManager
    {
        //CsvConfiguration SetupConfig();
        //List<Transaction> ReadFile(string filePath);
        //bool WriteToFile(string filePath, List<Output> data);
        //void Display(string filePath);
        //List<Output> ProcessRecords(List<Transaction> data);
        void Process(string inputFilePath, string outputFilePath);
    }
}
