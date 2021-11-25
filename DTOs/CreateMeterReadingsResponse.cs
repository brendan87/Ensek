using Ensek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ensek.DTOs
{
    public class CreateMeterReadingsResponse
    {
        public int TotalRead { get; set; }
        public int Created { get; set; }
        public int Failed { get; set; }
        public IEnumerable<MeterReading> Errors { get; set; }
    }
}
