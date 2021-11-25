using Ensek.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ensek.Services
{
    public interface IMeterReadingFileService
    {
        Task<IEnumerable<MeterReading>> ReadFromFile(IFormFile file);
    }
}
