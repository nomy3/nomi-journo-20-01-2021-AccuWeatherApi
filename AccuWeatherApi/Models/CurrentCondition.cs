using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccuWeatherApi.Models
{
    public class CurrentCondition
    {
        public DateTime LocalObservationDateTime { get; set; }
        public int EpochTime { get; set; }
        public string WeatherText { get; set; }
        public int WeatherIcon { get; set; }
        public bool HasPrecipitation { get; set; }
        public object PrecipitationType { get; set; }
        public bool IsDayTime { get; set; }
        public Temperature Temperature { get; set; }
    }

    public class Temperature
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    public class Metric
    {
        public float Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }

    public class Imperial
    {
        public float Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }
}
