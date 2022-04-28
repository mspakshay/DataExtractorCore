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
        private List<Output> _validOutputDataList = new List<Output>();
        private List<Output> _invalidOutputDataList = new List<Output>();

        [SetUp]
        public void Setup()
        {
            _fileManagerService = new CsvFileManager();
            
            //Valid data list
            Output sampleValidOutData = new Output()
            {
                ISIN = "ISIN123",
                CFICode = "CFI123",
                Venue = "SampleVenue",
                ContractSize = 10.8
            };

            _validOutputDataList.Add(sampleValidOutData);

            //Invalid data list
            Output sampleInvalidOutputData = new Output()
            {
                ISIN = "ISIN123",
                Venue = "SampleVenue",
                ContractSize = 10.8
            };
            _invalidOutputDataList.Add(sampleInvalidOutputData);
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
        public void ReadFile_InvalidFilePath_ThrowsDirectoryNotFoundException(string directoryPath)
        {
            Assert.Throws<DirectoryNotFoundException>(() => _fileManagerService.WriteToFile(directoryPath, _validOutputDataList));
        }

        [Test]
        [TestCase(@"..\..\..\Data")]
        public void ReadFile_ValidFilePath_Creates_and_Writes_To_File(string directoryPath)
        {
            bool result = _fileManagerService.WriteToFile(directoryPath, _validOutputDataList);

            Assert.AreEqual(true, result);

        }


    }
}
