using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DailyDozen.Shared.Models
{
    public class TweakServings
    {
        [Key]
        public int Id { get; set; }
        
        public int DayId { get; set; }
        public Day Day { get; set; }
        
        public int TweakId { get; set; }
        public Tweak Tweak { get; set; }
        
        public int Count { get; set; }
        
        public int Streak { get; set; }
        
        public TweakServings()
        {
            // Default constructor for Entity Framework
        }
        
        public TweakServings(Day day, Tweak tweak)
        {
            Day = day;
            DayId = day.Id;
            Tweak = tweak;
            TweakId = tweak.Id;
            Count = 0;
            Streak = 0;
        }
        
        public void RecalculateStreak(TweakServings previousDayServings)
        {
            if (Count == Tweak.RecommendedAmount)
            {
                Streak = previousDayServings?.Streak + 1 ?? 1;
            }
            else if (Count < Tweak.RecommendedAmount)
            {
                Streak = 0;
            }
        }
        
        public override string ToString()
        {
            return $"{Tweak?.Name}: {Count} of {Tweak?.RecommendedAmount}";
        }
    }
}