/*
 * Credits:
 * SetUp() LINQ API file loading (lines 47-124)
 * * Built on in-class code by Janell Baxter
 * Weather.cs: Storing API key in .env file (lines 130-142)
 * * “Load .env Files in C# .NET” by Ricardo Mauro: https://rmauro.dev/read-env-file-in-csharp/
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static WeatherMoment.Utility;
using static System.Environment;
using System.IO;

namespace WeatherMoment
{
    public class Weather
    {
        private string fileName;
        private string APIKey;
        public int Zip = 60618;

        #region Weather
        public string City = "N/A";

        public string Condition = "Data not available";
        public string ConditionIDString = "N/A";
        public int ConditionID = 0;

        public string TemperatureString = "N/A";
        public int Temperature = 0;

        public string TempMinString = "N/A";
        public int TemperatureMin = 0;

        public string TempMaxString = "N/A";
        public int TemperatureMax = 0;

        public string WindSpeedString = "N/A";
        public int WindSpeed = 0;

        public string HumidityString = "";
        public int Humidity = 0;
        #endregion


        public void SetUp()
        {
            APIKey = LoadEnv(); 

            fileName = $"https://api.openweathermap.org/data/2.5/weather?zip={Zip},us&mode=xml&units=imperial&appid={APIKey}";
            XDocument xdoc = XDocument.Load(fileName);

            var tempList = xdoc.Descendants()
                              .Where(x => x.Name == "city" ||
                                           x.Name == "weather" ||
                                           x.Name == "temperature" ||
                                           x.Name == "speed" ||
                                           x.Name == "humidity");

            foreach (XElement node in tempList)
            {
                //-------- City --------//
                if (node.Name == "city")
                {
                    City = node.Attribute("name").Value;
                }

                //-------- Weather --------//
                if (node.Name == "weather")
                {
                    Condition = node.Attribute("value").Value;
                    ConditionIDString = node.Attribute("number").Value;
                }
                else { Condition = "not found"; ConditionIDString = "not found"; }

                if (int.TryParse(ConditionIDString, out int id))
                {
                    ConditionID = id;
                }

                //-------- Temperature --------//
                if (node.Name == "temperature")
                {
                    TemperatureString = node.Attribute("value").Value;
                    TempMinString = node.Attribute("min").Value;
                    TempMaxString = node.Attribute("max").Value;
                }
                else { TemperatureString = "not found"; TempMinString = "not found"; TempMaxString = "not found"; }

                if (float.TryParse(TemperatureString, out float t))
                {
                    Temperature = FloatToInt(t);
                }
                if (float.TryParse(TempMinString, out float tmin))
                {
                    TemperatureMin = FloatToInt(tmin);
                }
                if (float.TryParse(TempMaxString, out float tmax))
                {
                    TemperatureMax = FloatToInt(tmax);
                }

                //-------- Wind --------//
                if (node.Name == "speed")
                {
                    WindSpeedString = node.Attribute("value").Value;
                }
                else { WindSpeedString = "not found"; }
                if (float.TryParse(WindSpeedString, out float s))
                {
                    WindSpeed = FloatToInt(s);
                }

                //-------- Humidity --------//
                if (node.Name == "humidity")
                {
                    HumidityString = node.Attribute("value").Value;
                }
                else { HumidityString = "not found"; }
                if (int.TryParse(HumidityString, out int h))
                {
                    Humidity = h;
                }
            }
        }

        private string LoadEnv()
        {
            foreach (var line in File.ReadAllLines("secret.env"))
            {
                var split = line.Split('=', 2);
                if (split.Length == 2)
                {
                    Environment.SetEnvironmentVariable(split[0], split[1]);
                }
            }

            return Environment.GetEnvironmentVariable("API_KEY");
        }
    }
}
