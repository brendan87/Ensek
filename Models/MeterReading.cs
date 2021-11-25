using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensek.Models
{
    public class MeterReading
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public Account Account { get; set; }
        public DateTimeOffset MeterReadingDateTime { get; set; }
        public long MeterReadValue { get; set; }
        public string Errors { get; set; }
    }

    public class MeterReadingComparer : EqualityComparer<MeterReading>
    {
        public override bool Equals(MeterReading r1, MeterReading r2)
        {
            return r1.AccountId == r2.AccountId
                && r1.MeterReadingDateTime == r2.MeterReadingDateTime
                && r1.MeterReadValue == r2.MeterReadValue;
        }

        public override int GetHashCode(MeterReading meterReading)
        {
            long hCode = meterReading.AccountId ^ meterReading.MeterReadingDateTime.ToUnixTimeSeconds() ^ meterReading.MeterReadValue;
            return hCode.GetHashCode();
        }
    }
}
