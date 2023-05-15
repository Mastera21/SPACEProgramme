using AppConfig;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SPACE_Programme_Task
{
    internal class Application
    {

        private List<DataBaseCfg> m_GoodDaysForLaunch;
        private List<DataBaseCfg> m_DbCollection;
        private MinMaxParamsCfg m_MinMaxParamsCfg;
        private AverageParamsCfg m_AvrgParamsCfg;
        private MedianaParamsCfg m_MedianaParamsCfg;

        private CsvReader m_CsvReader;

        public void Init()
        {
            m_GoodDaysForLaunch = new List<DataBaseCfg>();
            m_DbCollection = new List<DataBaseCfg>();
            m_MinMaxParamsCfg = new MinMaxParamsCfg();
            m_AvrgParamsCfg = new AverageParamsCfg();
            m_MedianaParamsCfg = new MedianaParamsCfg();

            var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                HasHeaderRecord = false,
                Comment = '#',
                AllowComments = true,
                Delimiter = ",",
            };

            string fileDbName = "DataBase.csv";
            var streamReader = File.OpenText(fileDbName);
            try
            {
                m_CsvReader = new CsvReader(streamReader, csvConfig);

            }catch (Exception ex) when (streamReader.EndOfStream)
            {
                Console.WriteLine("Error, the {0} file is empty.", ex.Message);
            }

            Console.WriteLine("Hey there. ;)");
            Console.WriteLine("Please select what type input you want");
            Console.WriteLine("1) Read custom input");
            Console.WriteLine("2) Read from database input");

        }


        public void RunApp()
        {
            Init();

            string userInput = Console.ReadLine();
            if (userInput == "1")
            {
                CustomInput();
            }
            else if (userInput == "2")
            {
                ReadFromDatabase(m_CsvReader);
            }

            Console.Clear();

            try
            {
                CalculateParams.GettingMaxValOfParameters(ref m_MinMaxParamsCfg.maxTemperatureVal, ref m_MinMaxParamsCfg.maxWindVal, ref m_MinMaxParamsCfg.maxHumidityVal, ref m_MinMaxParamsCfg.maxPrecipitationVal, m_DbCollection);
                CalculateParams.GettingMinValOfParameters(ref m_MinMaxParamsCfg.minTemperatureVal, ref m_MinMaxParamsCfg.minWindVal, ref m_MinMaxParamsCfg.minHumidityVal, ref m_MinMaxParamsCfg.minPrecipitationVal, m_DbCollection);
                CalculateParams.GettingAverageValOfParameters(ref m_AvrgParamsCfg.averageTemperatureVal, ref m_AvrgParamsCfg.averageWindVal, ref m_AvrgParamsCfg.averageHumidityVal, ref m_AvrgParamsCfg.averagePrecipitationVal, m_DbCollection);
                CalculateParams.GettingMedianaParametes(ref m_MedianaParamsCfg.medianaTemperatureVal, ref m_MedianaParamsCfg.medianaWindVal, ref m_MedianaParamsCfg.medianaHumidityVal, ref m_MedianaParamsCfg.medianaPrecipitationVal, m_DbCollection);
                FillUpDataBase();

                //Enter your inputs below.
                string fromEmailAddress = "";
                string password = "";
                string toEmailAddress = "";
                EmailSender.SendEmail("WeatherReport.csv", fromEmailAddress, password, toEmailAddress);
            }
            catch (Exception ex) when (m_DbCollection.Count == 0)
            {
                Console.WriteLine("Error, DataBase collection is empty.\n", ex.Message);
            }

        }
        private void ReadFromDatabase(CsvReader csvReader)
        {
            Console.WriteLine("Reading from database file.\n");
            while (csvReader.Read())
            {
                var db = new DataBaseCfg
                {
                    day = csvReader.GetField<int>(0),
                    temperature = csvReader.GetField<int>(1),
                    wind = csvReader.GetField<int>(2),
                    humidity = csvReader.GetField<int>(3),
                    precipitation = csvReader.GetField<int>(4),
                    lightning = csvReader.GetField(5),
                    clouds = csvReader.GetField(6)
                };
                m_DbCollection.Add(db);
                if (IsGoodDayForLaunch(db))
                {
                    m_GoodDaysForLaunch.Add(db);
                }
            }
        }

        private void CustomInput()
        {
            var db = new DataBaseCfg();
            Console.WriteLine("Enter day:");
            db.day = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter temperature:");
            db.temperature = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter wind:");
            db.wind = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter humidity:");
            db.humidity = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter precipitation:");
            db.precipitation = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter lightning:");
            db.lightning = Console.ReadLine();
            Console.WriteLine("Enter clouds:");
            db.clouds = Console.ReadLine();

            m_DbCollection.Add(db);

            if (IsGoodDayForLaunch(db))
            {
                m_GoodDaysForLaunch.Add(db);
            }

            Console.WriteLine("Do you want to add more information");
            Console.WriteLine("Please enter: Yes or No");
            string input = Console.ReadLine();

            if (input == "Yes")
            {
                Console.Clear();
                CustomInput();
            }
            else
            {
                return;
            }
            Console.Clear();
        }

        private bool IsGoodDayForLaunch(DataBaseCfg db)
        {
            if (db.temperature < 2 || db.temperature > 31)
            {
                return false;
            }

            if (db.wind > 10 || db.humidity >= 60)
            {
                return false;
            }

            if (db.precipitation != 0 || db.lightning == "Yes")
            {
                return false;
            }

            if (db.clouds == "Cumulus" || db.clouds == "Nimbus")
            {
                return false;
            }

            return true;
        }

        public void FillUpDataBase()
        {
            using (StreamWriter writer = new StreamWriter("WeatherReport.csv"))
            {
                writer.WriteLine(";" + "Temperature" + ";" + "Wind" + ";" + "Humidity" + ";" + "Precipitation");

                writer.WriteLine("Max Value" + ";" + m_MinMaxParamsCfg.maxTemperatureVal.ToString() + ";" + m_MinMaxParamsCfg.maxWindVal.ToString() + ";" + m_MinMaxParamsCfg.maxHumidityVal.ToString() + ";" + m_MinMaxParamsCfg.maxPrecipitationVal.ToString());
                writer.WriteLine("Min Value" + ";" + m_MinMaxParamsCfg.minTemperatureVal.ToString() + ";" + m_MinMaxParamsCfg.minWindVal.ToString() + ";" + m_MinMaxParamsCfg.minHumidityVal.ToString() + ";" + m_MinMaxParamsCfg.minPrecipitationVal.ToString());
                writer.WriteLine("Average Value" + ";" + m_AvrgParamsCfg.averageTemperatureVal.ToString() + ";" + m_AvrgParamsCfg.averageWindVal.ToString() + ";" + m_AvrgParamsCfg.averageHumidityVal.ToString() + ";" + m_AvrgParamsCfg.averagePrecipitationVal.ToString());
                writer.WriteLine("Mediana Value" + ";" + m_MedianaParamsCfg.medianaTemperatureVal.ToString() + ";" + m_MedianaParamsCfg.medianaWindVal.ToString() + ";" + m_MedianaParamsCfg.medianaHumidityVal.ToString() + ";" + m_MedianaParamsCfg.medianaPrecipitationVal.ToString());

                writer.WriteLine(";" + "Best days for launch");
                foreach (var dbs in m_GoodDaysForLaunch)
                {
                    writer.WriteLine("Day: " + dbs.day.ToString() + ";" + "Temperature: " + dbs.temperature.ToString() + ";" + "Wind: " + dbs.wind.ToString() + ";" + "Humidity: " + dbs.humidity.ToString() + ";" + "Precipitation: " + dbs.precipitation.ToString() + ";" + "Lightning: " + dbs.lightning.ToString() + ";" + "Clouds: " + dbs.clouds.ToString());
                }
            }
        }

    }
}
