using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DailyDozen.Shared.Models
{
    public class Day
    {
        [Key]
        public int Id { get; set; }
        
        public string DateString { get; set; }
        
        public int Year { get; set; }
        
        public int Month { get; set; }
        
        public int DayOfMonth { get; set; }

        public DateTime DateTime { get; set; }

        public Day()
        {
            // Default constructor for Entity Framework
        }

        public Day(DateTime dateTime)
        {
            SetDate(dateTime);
        }

        private void SetDate(DateTime dateTime)
        {
            DateTime = dateTime;
            DateString = dateTime.ToString("yyyyMMdd");
            Year = dateTime.Year;
            Month = dateTime.Month;
            DayOfMonth = dateTime.Day;
        }

        public static Day GetToday()
        {
            return new Day(DateTime.Today);
        }

        public string GetFormattedDate()
        {
            return DateTime.ToString("ddd, MMM d");
        }

        public bool IsToday()
        {
            return DateTime.Date == DateTime.Today;
        }

        public override string ToString()
        {
            return GetFormattedDate();
        }
    }
}