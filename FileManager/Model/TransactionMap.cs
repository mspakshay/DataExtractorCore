using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager.Model
{
    public class TransactionMap : ClassMap<Transaction>
    {
        public TransactionMap()
        {
            Map(m => m.IsMultiFill).Name("IsMultiFill");
            Map(m => m.ISIN).Name("ISIN");
            Map(m => m.Currency).Name("Currency");
            Map(m => m.Venue).Name("Venue");
            Map(m => m.OrderRef).Name("OrderRef");
            Map(m => m.PMID).Name("PMID");
            Map(m => m.CFICode).Name("CFICode");
            Map(m => m.ParticipantCode).Name("ParticipantCode");
            Map(m => m.TraderID).Name("TraderID");
            Map(m => m.CounterPartyCode).Name("CounterPartyCode");
            Map(m => m.DecisionTime).Name("DecisionTime");
            Map(m => m.ArrivalTime_QuoteTime).Name("ArrivalTime_QuoteTime");
            Map(m => m.FirstFillTime_TradeTime).Name("FirstFillTime_TradeTime");
            Map(m => m.LastFillTime).Name("LastFillTime");
            Map(m => m.Price).Name("Price");
            Map(m => m.Quantity).Name("Quantity");
            Map(m => m.Side).Name("Side");
            Map(m => m.TradeFlag).Name("TradeFlag");
            Map(m => m.SettlementDate).Name("SettlementDate");
            Map(m => m.PublicTradeID).Name("PublicTradeID");
            Map(m => m.UserDefinedFilter).Name("UserDefinedFilter");
            Map(m => m.TradingNetworkID).Name("TradingNetworkID");
            Map(m => m.SettlementPeriod).Name("SettlementPeriod");
            Map(m => m.MarketOrderId).Name("MarketOrderId");
            Map(m => m.ParticipationRate).Name("ParticipationRate");
            Map(m => m.BenchmarkVenues).Name("BenchmarkVenues");
            Map(m => m.BenchmarkType).Name("BenchmarkType");
            Map(m => m.FlowType).Name("FlowType");
            Map(m => m.BasketID).Name("BasketID");
            Map(m => m.MessageType).Name("MessageType");
            Map(m => m.ParentOrderRef).Name("ParentOrderRef");
            Map(m => m.ExecutionType).Name("ExecutionType");
            Map(m => m.LimitPrice).Name("LimitPrice");
            Map(m => m.Urgency).Name("Urgency");
            Map(m => m.AlgoName).Name("AlgoName");
            Map(m => m.AlgoParams).Name("AlgoParams");
            Map(m => m.Index).Name("Index");
            Map(m => m.Sector).Name("Sector");
        }
    }
}
