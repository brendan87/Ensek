using CsvHelper.Configuration;
using Ensek.Data;
using Ensek.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Ensek.Validation
{
    public class MeterReadingMap : ClassMap<MeterReading>
    {
        static readonly string[] dateFormats =
        {
            "dd/MM/yyyy HH:mm"
        };
        public MeterReadingMap(MeterReadingContext context)
        {
            //Validation
            Map(m => m.Errors).Convert(m => {
                DateTimeOffset date;
                string errors = "";

                var accountField = m.Row.GetField(nameof(MeterReading.AccountId));
                long accountId;
                if(!long.TryParse(accountField, out accountId))
                {
                    errors += $"Expected AccountId {accountField} to be a number";
                }
                //TODO - async...
                else if(context.Accounts.Find(accountId) == null)
                {
                    errors += $"AccountId {accountField} does not exist";
                }

                var dateField = m.Row.GetField(nameof(MeterReading.MeterReadingDateTime));
                if (!DateTimeOffset.TryParseExact(dateField, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    errors += $"Cannot convert {nameof(MeterReading.MeterReadingDateTime)} with value '{dateField}'";
                }
                else if((date - DateTime.Now).TotalSeconds > 0)
                {
                    errors += $"Date '{dateField}' is in the future";
                }

                long meterReading;
                var meterValueField = m.Row.GetField(nameof(MeterReading.MeterReadValue));
                if (!long.TryParse(meterValueField, out meterReading))
                {
                    errors += $"Cannot convert {nameof(MeterReading.MeterReadValue)} with value '{meterValueField}'";
                }
                else if (meterReading < 0)
                {
                    errors += $"Meter reading must be a positive number";
                }

                if (context.MeterReadings.Any(m => m.AccountId == accountId && m.MeterReadingDateTime == date && m.MeterReadValue == meterReading))
                {
                    errors += $"Meter reading already exists";
                }

                if (errors.Any())
                {
                    return $"Row {m.Row.Parser.RawRow - 1}: " + errors;
                }
                return string.Empty;
            });
            Map(m => m.AccountId);
            Map(m => m.MeterReadingDateTime).Convert(m =>
            {
                DateTimeOffset date;
                if (!DateTimeOffset.TryParseExact(m.Row.GetField(nameof(MeterReading.MeterReadingDateTime)), dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    return default;
                }
                return date;
            });
            Map(m => m.MeterReadValue).Convert(m => {
                long meterReading;
                if (!long.TryParse(m.Row.GetField(nameof(MeterReading.MeterReadValue)), out meterReading)) {
                    return default;
                };
                return meterReading;
            });
        }
    }
}
