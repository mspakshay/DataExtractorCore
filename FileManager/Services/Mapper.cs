using CsvHelper;
using FileManager.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager.Services
{
    public class Mapper : IMapper
    {
        public void Setup(CsvReader reader)
        {
            reader.Context.RegisterClassMap<TransactionMap>();
        }
    }
}
