using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FileManager.Model
{
    public class Output
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "ISIN field value is required.")]
        [Name("ISIN")]
        public string ISIN { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "CFICode field value is required.")]
        [Name("CFICode")]
        public string CFICode { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Venue field value is required.")]
        [Name("Venue")]
        public string Venue { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "ContractSize field value is required.")]
        [Name("Contract Size")]
        public double ContractSize { get; set; }

    }
}
