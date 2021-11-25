using Ensek.Models;
using System;
using System.Linq;
using CsvHelper;
using System.IO;
using System.Globalization;

namespace Ensek.Data
{
    public static class Seeder
    {
        public static void Seed(MeterReadingContext context)
        {
            context.Database.EnsureCreated();

            if (context.Accounts.Any())
            {
                return;
            }
            using (var reader = new StreamReader("Data/Test_Accounts.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Account>();
                foreach(var record in records)
                {
                    context.Accounts.Add(record);
                }
            }

            context.SaveChanges();
        }
    }
}