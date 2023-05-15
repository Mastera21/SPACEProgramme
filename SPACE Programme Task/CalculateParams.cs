using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPACE_Programme_Task
{
    internal static class CalculateParams
    {
        private static double GetMedian(IEnumerable<int> numbers)
        {
            try
            {
                var sortedNumbers = numbers.OrderBy(n => n).ToList();
                int count = sortedNumbers.Count;
                if (count % 2 == 0)
                {
                    int midIndex = count / 2;
                    return (sortedNumbers[midIndex - 1] + sortedNumbers[midIndex]) / 2.0;
                }
                else
                {
                    int midIndex = count / 2;
                    return sortedNumbers[midIndex];
                }

            }catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException("Parameter index is out of range.", ex);
            }
        }

        public static void GettingMedianaParametes(ref double medianaTemperature, ref double medianaWind, ref double medianaHumidity, ref double medianaPrecipitation, List<AppConfig.DataBaseCfg> dbCollection)
        {
            medianaTemperature = GetMedian(dbCollection.Select(db => db.temperature));
            medianaWind = GetMedian(dbCollection.Select(db => db.wind));
            medianaHumidity = GetMedian(dbCollection.Select(db => db.humidity));
            medianaPrecipitation = GetMedian(dbCollection.Select(db => db.precipitation));
        }

        public static void GettingMaxValOfParameters(ref int maxTemperatureVal, ref int maxWindVal, ref int maxHumidityVal, ref int maxPrecipitationVal, List<AppConfig.DataBaseCfg> dbCollection)
        {
            foreach (var db in dbCollection)
            {
                maxTemperatureVal = Math.Max(maxTemperatureVal, db.temperature);
                maxWindVal = Math.Max(maxWindVal, db.wind);
                maxHumidityVal = Math.Max(maxHumidityVal, db.humidity);
                maxPrecipitationVal = Math.Max(maxPrecipitationVal, db.precipitation);
            }
        }

        public static void GettingMinValOfParameters(ref int minTemperatureVal, ref int minWindVal, ref int minHumidityVal, ref int minPrecipitationVal, List<AppConfig.DataBaseCfg> dbCollection)
        {
            foreach (var db in dbCollection)
            {
                minTemperatureVal = Math.Min(minTemperatureVal, db.temperature);
                minWindVal = Math.Min(minWindVal, db.wind);
                minHumidityVal = Math.Min(minHumidityVal, db.humidity);
                minPrecipitationVal = Math.Min(minPrecipitationVal, db.precipitation);
            }
        }

        public static void GettingAverageValOfParameters(ref double avrgTemperature, ref double avrgWind, ref double avrgHumidity, ref double avrgPrecipitation, List<AppConfig.DataBaseCfg> dbCollection)
        {
            foreach (var db in dbCollection)
            {
                avrgTemperature += db.temperature;
                avrgWind += db.wind;
                avrgHumidity += db.humidity;
                avrgPrecipitation += db.precipitation;
            }

            avrgTemperature = Math.Round(avrgTemperature / dbCollection.Count, 2);
            avrgWind = Math.Round(avrgWind / dbCollection.Count, 2);
            avrgHumidity = Math.Round(avrgHumidity / dbCollection.Count, 2);
            avrgPrecipitation = Math.Round(avrgPrecipitation / dbCollection.Count, 2);
        }

    }
}
