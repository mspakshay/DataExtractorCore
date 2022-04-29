# DataExtractorCore

Solution contains 3 projects:
  1. DataExtractorCore - Console Application
  2. File Manager - Library(Business Layer)
  3. FileManagerTests - Test Project(This tests Business layer functionalities)


# DataExtractorCore - Console Application
This Project invokes file manager service methods to process given input file and generates new formatted output csv with required fields in specified outpupt directory when executed. Both Input and Output directories are placed/created under Data folder in the Project. This contains reference  to File Manager Project. Dependency Injection is used which injects instance of CsvFileManager class.


# File Manager Library - Class Library
This is the class library which contains all the business logic. It has all the methods which are being used to process input CSV file and generate formatted output csv.
This Project has following folders -
  1. Models - Data models for parsing Csv data into objects and vice versa. 
  2. Services - Service classes which contains all the logic
  3. ServiceContracts - Interfaces for services

As this is the project that processes CSV files, it refers to nuget package of CsvHelper that is being used for reading/writing csv files. We are using Mapper classes for mapping headers into csv file. CsvFileManager Service implements service contract IFileManager which has following methods:

  1. ReadFile - This methods only reads data from input csv. Reads csv from given input path and returns List of trasnaction records present in the file.
  2. ProcessRecords - This methods processes records. This methods receives input from ReadFile methods. It processes the list of trasnaction records by reading Algo parameter property which is a complex object. First data cleanup is done on Algo Parameter object and then data is mapped to AlgoParam class using Newtonsoft.Json library. This methods returns the formatted Output object which will be written to the output csv. 
  3. WriteToFile - This method receives List of output records and writes to the file in the mentioned output directory. This performs operation related to writing of the file.
  4. Process - This methods invokes ReadFile, ProcessRecords & WriteToFile method one by one. To avoid calling multiple methods in Main class, this method is provided.

# FileManagerTests - Test Project
This is NUnit Test Project. This Projects contains methods to test CsvFileManager class Functionality. 
