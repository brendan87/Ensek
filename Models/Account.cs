using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensek.Models
{
    public class Account
    {
        public long AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<MeterReading> MeterReadings { get; set; }
    }
}
