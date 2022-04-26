using CsvHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager.Services
{
    public interface IMapper
    {
        void Setup(CsvReader reader);
    }
}
