/*
 * Credits:
 * DateTime Struct (lines 32-85)
 * * C# 10 In a Nutshell: Chapter 6 .Net Fundamentals pg.284-291
 * String to Brush Conversion (lines 29-31)
 * * Stack Overflow solution by Lucas: https://stackoverflow.com/questions/372693/convert-string-to-brushes-brush-color-name-in-c-sharp
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WeatherMoment
{
    public static class Utility
    {
        public static System.Windows.Media.Color color; 

        public static int FloatToInt(float f)
        {
            return (int)f;
        }

        public static Brush StringToBrush(string s)
        {
            return (Brush)new BrushConverter().ConvertFromString(s);
        }

        public static string GetDateString()
        {
            DateTime dt = DateTime.Now;
            return $"{dt.ToString("MMMM")} {dt.Day.ToString()}";
        }

        public static string GetDayString()
        {
            DateTime dt = DateTime.Now;
            return dt.DayOfWeek.ToString();
        }

        public static string GetTimeString()
        {
            DateTime dt = DateTime.Now;
            string meridiem = "";

            int newHour;
            if (dt.Hour > 13)
            {
                newHour = dt.Hour - 12;
                meridiem = "pm";
            }
            else if (dt.Hour == 0)
            {
                newHour = 12;
                meridiem = "am";
            }
            else
            {
                newHour = dt.Hour;
                meridiem = "am";
            }

            string newMinute = "0";
            if (dt.Minute < 10)
            {
                newMinute = "0" + dt.Minute;
            }
            else
            {
                newMinute = dt.Minute.ToString(); 
            }

            return $"{newHour}:{newMinute} {meridiem}";
        }

        public static int GetHour()
        {
            DateTime dt = DateTime.Now;
            return dt.Hour;
        }
    }
}
