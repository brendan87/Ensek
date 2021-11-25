using Ensek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ensek.Services
{
    public interface IMeterReadingPersister
    {
        Task<int> Persist(IEnumerable<MeterReading> readings);
    }
}
