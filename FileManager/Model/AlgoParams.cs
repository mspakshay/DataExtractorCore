using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager.Model
{
    public class AlgoParams
    {
        public string InstIdentCode { get; set; }
        public string InstFullName { get; set; }
        public string InstClassification { get; set; }
        public string NotionalCurr { get; set; }
        public double PriceMultiplier { get; set; }
        public string UnderlInstCode { get; set; }
        public string UnderlIndexName { get; set; }
        public string OptionType { get; set; }
        public double StrikePrice { get; set; }
        public string OptionExerciseStyle { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string DeliveryType { get; set; }
    }
}
