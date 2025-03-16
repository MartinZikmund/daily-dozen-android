using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DailyDozen.Shared.Models
{
    public class Servings
    {
        [Key]
        public int Id { get; set; }
        
        public int DayId { get; set; }
        public Day Day { get; set; }
        
        public int FoodId { get; set; }
        public Food Food { get; set; }
        
        public int Count { get; set; }
        
        public int Streak { get; set; }
        
        public Servings()
        {
            // Default constructor for Entity Framework
        }
        
        public Servings(Day day, Food food)
        {
            Day = day;
            DayId = day.Id;
            Food = food;
            FoodId = food.Id;
            Count = 0;
            Streak = 0;
        }
        
        public void RecalculateStreak(Servings previousDayServings)
        {
            if (Count == Food.RecommendedAmount)
            {
                Streak = previousDayServings?.Streak + 1 ?? 1;
            }
            else if (Count < Food.RecommendedAmount)
            {
                Streak = 0;
            }
        }
        
        public override string ToString()
        {
            return $"{Food?.Name}: {Count} of {Food?.RecommendedAmount} servings";
        }
    }
}