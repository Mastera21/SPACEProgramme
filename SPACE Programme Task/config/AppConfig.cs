using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppConfig
{
    [Serializable]
    public class DataBaseCfg
    {
        public int day = 0;
        public int temperature = 0;
        public int wind = 0;
        public int humidity = 0;
        public int precipitation = 0;
        public string lightning = "";
        public string clouds = "";
    }

    [Serializable]
    public class MinMaxParamsCfg
    {
        public int maxTemperatureVal = int.MinValue;
        public int maxWindVal = int.MinValue;
        public int maxHumidityVal = int.MinValue;
        public int maxPrecipitationVal = int.MinValue;

        public int minTemperatureVal = int.MaxValue;
        public int minWindVal = int.MaxValue;
        public int minHumidityVal = int.MaxValue;
        public int minPrecipitationVal = int.MaxValue;
    }

    [Serializable]
    public class AverageParamsCfg
    {
        public double averageTemperatureVal = 0.0;
        public double averageWindVal = 0.0;
        public double averageHumidityVal = 0.0;
        public double averagePrecipitationVal = 0.0;
    }

    [Serializable]
    public class MedianaParamsCfg
    {
        public double medianaTemperatureVal = 0.0;
        public double medianaWindVal = 0.0;
        public double medianaHumidityVal = 0.0;
        public double medianaPrecipitationVal = 0.0;
    }

}
