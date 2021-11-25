using Ensek.Data;
using Ensek.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ensek.Services
{
    public class MeterReadingDatabaseLoader : IMeterReadingPersister
    {
        private readonly MeterReadingContext _context;

        public MeterReadingDatabaseLoader(MeterReadingContext context)
        {
            _context = context;
        }

        public async Task<int> Persist(IEnumerable<MeterReading> readings)
        {
            foreach(var reading in readings)
            {
                _context.MeterReadings.Add(reading);
            }
            return await _context.SaveChangesAsync();
        }
    }
}
