using CsvHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager.ServiceContracts
{
    public interface IMapper
    {
        void Setup(CsvReader reader);
    }
}
