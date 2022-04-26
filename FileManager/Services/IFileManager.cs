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
        CsvConfiguration SetupConfig(bool isHeaderPresent, CultureInfo cultureInfo);
        List<Transaction> ReadFile(string filePath);
        void WriteToFile(string filePath);
        void Display(string filePath);
        void ProcessRecord(Transaction data);
    }
}
