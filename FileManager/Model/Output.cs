using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager.Model
{
    public class Output
    {
        [Name("ISIN")]
        public string ISIN { get; set; }
        [Name("CFICode")]
        public string CFICode { get; set; }
        [Name("Venue")]
        public string Venue { get; set; }
        [Name("Contract Size")]
        public double ContractSize { get; set; }

    }
}
