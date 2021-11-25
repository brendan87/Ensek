using Ensek.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using System.IO;
using System.Globalization;
using Ensek.Data;
using Ensek.Validation;

namespace Ensek.Services
{
    public class MeterReadingCsvService : IMeterReadingFileService
    {
        private readonly MeterReadingContext _context;

        public MeterReadingCsvService(MeterReadingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MeterReading>> ReadFromFile(IFormFile file)
        {
            var meterReadings = new List<MeterReading>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap(new MeterReadingMap(_context));
                var records = csv.GetRecordsAsync<MeterReading>();
                await foreach (var record in records)
                {
                    meterReadings.Add(record);
                }
            }
            return meterReadings.Distinct(new MeterReadingComparer());
        }
    }
}
