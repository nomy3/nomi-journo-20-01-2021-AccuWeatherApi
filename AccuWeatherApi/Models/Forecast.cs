﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccuWeatherApi.Models
{
    public class Forecast
    {
        public Dailyforecast[] DailyForecasts { get; set; }

        public class Dailyforecast
        {
            public DateTime Date { get; set; }
            public int EpochDate { get; set; }
            public Temperature Temperature { get; set; }
            public Day Day { get; set; }
            public Night Night { get; set; }
            public string[] Sources { get; set; }
            public string MobileLink { get; set; }
            public string Link { get; set; }
        }

        public class Temperature
        {
            public Minimum Minimum { get; set; }
            public Maximum Maximum { get; set; }
        }

        public class Minimum
        {
            public float Value { get; set; }
            public string Unit { get; set; }
            public int UnitType { get; set; }
        }

        public class Maximum
        {
            public float Value { get; set; }
            public string Unit { get; set; }
            public int UnitType { get; set; }
        }

        public class Day
        {
            public int Icon { get; set; }
            public string IconPhrase { get; set; }
            public bool HasPrecipitation { get; set; }
            public string PrecipitationType { get; set; }
            public string PrecipitationIntensity { get; set; }
        }

        public class Night
        {
            public int Icon { get; set; }
            public string IconPhrase { get; set; }
            public bool HasPrecipitation { get; set; }
        }
    }
}
