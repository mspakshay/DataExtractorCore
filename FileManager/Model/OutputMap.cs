using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager.Model
{
    public class OutputMap: ClassMap<Output>
    {
        public OutputMap()
        {
            Map(m => m.ISIN).Index(0).Name("ISIN");
            Map(m => m.CFICode).Index(1).Name("CFICode");
            Map(m => m.Venue).Index(2).Name("Venue");
            Map(m => m.ContractSize).Index(3).Name("Contract Size");
        }
    }
}
