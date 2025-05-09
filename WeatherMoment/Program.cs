/*
 * Credits:
 * INotifyPropertyChanged: PropertyChangedEventHandler (lines 189-196) and OnPropertyChanged() calls in field setters (lines 41-49)
 * "WPF Data Binding: C# INotifyPropertyChanged" by Bradley Wells: https://wellsb.com/csharp/learn/wpf-data-binding-csharp-inotifypropertychanged/
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Xml.Linq;
using static WeatherMoment.Utility;
using static System.Environment;

namespace WeatherMoment
{
    public class Program : INotifyPropertyChanged
    {
        Weather weather = new Weather();

        public event PropertyChangedEventHandler PropertyChanged;

        public enum ConditionType
        {
            clouds,
            clear,
            atmosphere,
            snow,
            rain,
            drizzle,
            thunderstorms
        }

        #region Weather
        public int Zip { get => weather.Zip; set { weather.Zip = value; OnPropertyChanged(nameof(weather.Zip)); } }
        public string City { get => weather.City; set { weather.City = value; OnPropertyChanged(nameof(weather.City)); } }
        public string Condition { get => weather.Condition; set { weather.Condition = value; OnPropertyChanged(nameof(weather.Condition)); } }
        public int ConditionID { get => weather.ConditionID; set { weather.ConditionID = value; OnPropertyChanged(nameof(weather.ConditionID)); } }
        public int Temperature { get => weather.Temperature; set { weather.Temperature = value; OnPropertyChanged(nameof(weather.Temperature)); } }
        public int TemperatureMin { get => weather.TemperatureMin; set { weather.TemperatureMin = value; OnPropertyChanged(nameof(weather.TemperatureMin)); } }
        public int TemperatureMax { get => weather.TemperatureMax; set { weather.TemperatureMax = value; OnPropertyChanged(nameof(weather.TemperatureMax)); } }
        public int WindSpeed { get => weather.WindSpeed; set { weather.WindSpeed = value; OnPropertyChanged(nameof(weather.WindSpeed)); } }
        public int Humidity { get => weather.Humidity; set { weather.Humidity = value; OnPropertyChanged(nameof(weather.Humidity)); } }
        #endregion

        #region DateTime
        private string dateAndTime = "not set";
        public string DateAndTime { get => dateAndTime; set { dateAndTime = value; OnPropertyChanged(nameof(dateAndTime)); } }
        #endregion

        #region Brush
        private Brush backgroundColor;
        public Brush BackgroundColor { get => backgroundColor; set { backgroundColor = value; OnPropertyChanged(nameof(backgroundColor)); } }
        private Brush foregroundColor;
        public Brush ForegroundColor { get => foregroundColor; set { foregroundColor = value; OnPropertyChanged(nameof(foregroundColor)); } }
        #endregion


        public void Start()
        {
            //Sets binding values for MainPage Refresh
            Zip = weather.Zip;
            City = weather.City;
            Condition = weather.Condition;
            ConditionID = weather.ConditionID;
            Temperature = weather.Temperature;
            TemperatureMin = weather.TemperatureMin;
            TemperatureMax = weather.TemperatureMax;
            WindSpeed = weather.WindSpeed;
            Humidity = weather.Humidity;

            weather.SetUp();
            SetDateTime();
        }

        private void SetDateTime()
        {
            var date = GetDateString();
            var dayOfWeek = GetDayString();
            var time = GetTimeString();

            DateAndTime = $"{dayOfWeek}, {date} | {time}";
        }

        private ConditionType GetCurrentCondition()
        {
            if (ConditionID > 800)
            {
                return ConditionType.clouds;
            }
            else if (ConditionID == 800)
            {
                return ConditionType.clear;
            }
            else if (ConditionID > 700)
            {
                return ConditionType.atmosphere;
            }
            else if (ConditionID > 600)
            {
                return ConditionType.snow;
            }
            else if (ConditionID > 500)
            {
                return ConditionType.rain;
            }
            else if (ConditionID > 300)
            {
                return ConditionType.drizzle;
            }
            else if (ConditionID > 200)
            {
                return ConditionType.thunderstorms;
            }
            return ConditionType.clear;
        }

        public string GetConditionImage()
        {
            var condition = GetCurrentCondition();

            switch (condition)
            {
                case ConditionType.clouds:
                    return "/img/cloud.png";
                case ConditionType.clear:
                    return "/img/clear.png";
                case ConditionType.atmosphere:
                    return "/img/atmosphere.png";
                case ConditionType.snow:
                    return "/img/snow.png";
                case ConditionType.rain:
                    return "/img/rain.png";
                case ConditionType.drizzle:
                    return "/img/drizzle.png";
                case ConditionType.thunderstorms:
                    return "/img/thunderstorm.png";
                default:
                    return "/img/clear.png";
            }
        }

        public Brush GetForegroundColor()
        {
            var condition = GetCurrentCondition();
            switch (condition)
            {
                case ConditionType.clouds:
                    return StringToBrush("#E2E2E2");
                case ConditionType.clear:
                    backgroundColor = StringToBrush("#D6EFFF");
                    return StringToBrush("#463730");
                case ConditionType.atmosphere:
                    return StringToBrush("#293847");
                case ConditionType.snow:
                    return StringToBrush("#6988AE");
                case ConditionType.rain:
                    return StringToBrush("#FFFFFF");
                case ConditionType.drizzle:
                    return StringToBrush("#4D9994");
                case ConditionType.thunderstorms:
                    return StringToBrush("#3B3B3B");
                default:
                    return StringToBrush("#463730");
            }
        }

        public Brush GetBackgroundColor()
        {
            var condition = GetCurrentCondition();
            switch (condition)
            {
                case ConditionType.clouds:
                    return StringToBrush("#6E88B5");
                case ConditionType.clear:
                    return StringToBrush("#D6EFFF");
                case ConditionType.atmosphere:
                    return StringToBrush("#B9B8D3");
                case ConditionType.snow:
                    return StringToBrush("#D7E0E8");
                case ConditionType.rain:
                    return StringToBrush("#7A9E86");
                case ConditionType.drizzle:
                    return StringToBrush("#FAF3DD");
                case ConditionType.thunderstorms:
                    return StringToBrush("#F4D35E");
                default:
                    return StringToBrush("#D6EFFF");
            }
        }

        protected void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

    }
}
