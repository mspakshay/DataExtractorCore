using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager.Model
{
    public class Transaction
    {
        public bool IsMultiFill { get; set; }
        public string ISIN { get; set; }
        public string Currency { get; set; }
        public string Venue { get; set; }
        public int OrderRef { get; set; }
        public string PMID { get; set; }
        public string CFICode { get; set; }
        public string ParticipantCode { get; set; }
        public string TraderID { get; set; }
        public string CounterPartyCode { get; set; }
        public string DecisionTime { get; set; }
        public string ArrivalTime_QuoteTime { get; set; }
        public DateTime FirstFillTime_TradeTime { get; set; }
        public string LastFillTime { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Side { get; set; }
        public string TradeFlag { get; set; }
        public string SettlementDate { get; set; }
        public string PublicTradeID { get; set; }
        public string UserDefinedFilter { get; set; }
        public string TradingNetworkID { get; set; }
        public string SettlementPeriod { get; set; }
        public string MarketOrderId { get; set; }
        public string ParticipationRate { get; set; }
        public string BenchmarkVenues { get; set; }
        public string BenchmarkType { get; set; }
        public string FlowType { get; set; }
        public string BasketID { get; set; }
        public string MessageType { get; set; }
        public int ParentOrderRef { get; set; }
        public int? ExecutionType { get; set; }
        public string LimitPrice { get; set; }
        public string Urgency { get; set; }
        public string AlgoName { get; set; }
        public string AlgoParams { get; set; }
        public string Index { get; set; }
        public int Sector { get; set; }

        public override string ToString()
        {
            return "Transaction: " + ISIN + " " + Currency;
        }
    }
}
