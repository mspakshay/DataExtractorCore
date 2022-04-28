using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FileManager.Model;
using FileManager.ServiceContracts;
using FileManager.Services;
using NUnit.Framework;

namespace FileManagerTests
{
    [TestFixture]
    class FileManagerTests
    {
        private IFileManager _fileManagerService;

        [SetUp]
        public void Setup()
        {
            _fileManagerService = new CsvFileManager();
        }


        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ReadFile_NullOrEmptyFilePath_ThrowArgumentException(string filePath)
        {
            Assert.Throws<ArgumentException>(() => _fileManagerService.ReadFile(filePath));
        }

        [Test]
        [TestCase(@"..\InvalidFilePath.csv")]
        public void ReadFile_InvalidFilePath_ThrowsFileNotFoundException(string filePath)
        {
            Assert.Throws<FileNotFoundException>(() => _fileManagerService.ReadFile(filePath));
        }

        [Test]
        [TestCase(@"..\..\..\Data\DataExtractor_Example_Input.csv")]
        public void ReadFile_ValidFilePath_ReturnListOfTransactions(string filePath)
        {
            var transactions = _fileManagerService.ReadFile(filePath);

            Assert.IsInstanceOf(typeof(List<Transaction>), transactions);
            Assert.That(transactions, Has.Count.GreaterThan(0));
        }

        [Test]
        [TestCase(@"E:\InvalidDirectory")]
        public void WriteToFile_InvalidFilePath_ThrowsDirectoryNotFoundException(string directoryPath)
        {
            List<Output> _sampleOutputDataList = new List<Output>();
            Output sampleOutputData = new Output()
            {
                ISIN = "ISIN23",
                Venue = "SampleVenue",
                ContractSize = 23.8,
                CFICode = "CFI0987"
            };

            _sampleOutputDataList.Add(sampleOutputData);
            Assert.Throws<DirectoryNotFoundException>(() => _fileManagerService.WriteToFile(directoryPath, _sampleOutputDataList));
        }

        [Test]
        [TestCase(@"..\..\..\Data")]
        public void WriteToFile_ValidFilePath_Creates_and_Writes_To_File(string directoryPath)
        {
            List<Output> _sampleOutputDataList = new List<Output>();

            //Valid data list
            Output sampleValidOutData = new Output()
            {
                ISIN = "ISIN1",
                CFICode = "CFI123",
                Venue = "SampleVenue",
                ContractSize = 10.8
            };

            _sampleOutputDataList.Add(sampleValidOutData);

            bool result = _fileManagerService.WriteToFile(directoryPath, _sampleOutputDataList);

            Assert.IsTrue(result);

        }

        [Test]
        [TestCase(@"..\..\..\Data")]
        public void WriteToFile_InvalidInputObject_ThrowDataAnnotationError(string directoryPath)
        {
            List<Output> _sampleOutputDataList = new List<Output>();
            //Invalid data list
            Output sampleInvalidOutputData = new Output()
            {
                ISIN = "ISIN12",
                Venue = "",
                ContractSize = 10.8,
                CFICode = null
            };

            _sampleOutputDataList.Add(sampleInvalidOutputData);

            Assert.Throws<ArgumentException>(() => _fileManagerService.WriteToFile(directoryPath, _sampleOutputDataList));
        }


        [Test]
        [TestCase(@"..\..\..\Data")]
        public void WriteToFile_ValidInputObject_ReturnsTrue(string directoryPath)
        {
            List<Output> _sampleOutputDataList = new List<Output>();
            Output sampleOutputData = new Output()
            {
                ISIN = "ISIN23",
                Venue = "SampleVenue",
                ContractSize = 23.8,
                CFICode = "CFI0987"
            };

            _sampleOutputDataList.Add(sampleOutputData);

            var result = _fileManagerService.WriteToFile(directoryPath, _sampleOutputDataList);
            Assert.IsTrue(result);
        }

        [Test]
        public void ProcessRecords_Return_List_Of_Output()
        {
            Transaction transactionRecord = new Transaction()
            {
                AlgoName = "",
                AlgoParams = "InstIdentCode:DE000ABCDEFG|;InstFullName:DAX|;InstClassification:FFICSX|;NotionalCurr:EUR|;PriceMultiplier:20.0|;UnderlInstCode:DE0001234567|;UnderlIndexName:DAX PERFORMANCE-INDEX|;OptionType:OTHR|;StrikePrice:0.0|;OptionExerciseStyle:|;ExpiryDate:2021-01-01|;DeliveryType:PHYS|",
                ArrivalTime_QuoteTime = "",
                BasketID = "",
                BenchmarkType = "",
                BenchmarkVenues = "",
                CFICode = "FFICSX",
                CounterPartyCode = "DMA",
                Currency = "EUR",
                DecisionTime = "",
                ExecutionType = 1,
                FirstFillTime_TradeTime = Convert.ToDateTime("4/28/2022 12:23:22 AM").Date,
                FlowType = "Y",
                ISIN = "DE000ABCDEFG",
                Index = "G",
                IsMultiFill = false,
                LastFillTime = "",
                LimitPrice = "",
                MarketOrderId = "",
                MessageType = "F",
                OrderRef = 1,
                PMID = "Bob1",
                ParentOrderRef = 100000011,
                ParticipantCode = "ABCDEFGHIJKL",
                ParticipationRate = "",
                Price = 1239.5,
                PublicTradeID = "",
                Quantity = 1,
                Sector = 1,
                SettlementDate = "",
                SettlementPeriod = "",
                Side = "B",
                TradeFlag = "",
                TraderID = "",
                TradingNetworkID = "BigBank",
                Urgency = "",
                UserDefinedFilter = "",
                Venue = "XEUR"
            };

            Output outputRecord = new Output()
            {
                ISIN = "DE000ABCDEFG",
                CFICode = "FFICSX",
                ContractSize = 20,
                Venue = "XEUR"
            };

            var inputList = new List<Transaction>();
            inputList.Add(transactionRecord);

            var outputList = new List<Output>();
            outputList.Add(outputRecord);

            var result = _fileManagerService.ProcessRecords(inputList);

            Assert.AreEqual(outputList.Count, result.Count);
            Assert.AreEqual(outputList[0].CFICode, result[0].CFICode);
            Assert.AreEqual(outputList[0].ContractSize, result[0].ContractSize);
            Assert.AreEqual(outputList[0].ISIN, result[0].ISIN);
            Assert.AreEqual(outputList[0].Venue, result[0].Venue);
        }

        [Test]
        [TestCase(@"..\..\..\Data\DataExtractor_Example_Input.csv", @"..\..\..\Data")]
        public void Process_InputFile_ReturnsTrue(string inputFilePath, string outputDirectory)
        {

            var result = _fileManagerService.Process(inputFilePath, outputDirectory);
            Assert.IsTrue(result);
        }
    }
}
