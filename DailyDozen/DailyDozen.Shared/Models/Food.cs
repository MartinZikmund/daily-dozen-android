using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DailyDozen.Shared.Models
{
    public class Food
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string IdName { get; set; }
        
        public int RecommendedAmount { get; set; }
        
        public string IconPath { get; set; }
        
        public bool IsSupplement { get; set; }
        
        public string MoreInfoUrl { get; set; }

        public static List<Food> GetAllFoods()
        {
            return new List<Food>
            {
                new Food { Id = 1, Name = "Beans", IdName = "beans", RecommendedAmount = 3, IconPath = "ms-appx:///Assets/Foods/beans.png", IsSupplement = false },
                new Food { Id = 2, Name = "Berries", IdName = "berries", RecommendedAmount = 1, IconPath = "ms-appx:///Assets/Foods/berries.png", IsSupplement = false },
                new Food { Id = 3, Name = "Other Fruits", IdName = "other_fruits", RecommendedAmount = 3, IconPath = "ms-appx:///Assets/Foods/other_fruits.png", IsSupplement = false },
                new Food { Id = 4, Name = "Cruciferous Vegetables", IdName = "cruciferous_vegetables", RecommendedAmount = 1, IconPath = "ms-appx:///Assets/Foods/cruciferous_vegetables.png", IsSupplement = false },
                new Food { Id = 5, Name = "Greens", IdName = "greens", RecommendedAmount = 2, IconPath = "ms-appx:///Assets/Foods/greens.png", IsSupplement = false },
                new Food { Id = 6, Name = "Other Vegetables", IdName = "other_vegetables", RecommendedAmount = 2, IconPath = "ms-appx:///Assets/Foods/other_vegetables.png", IsSupplement = false },
                new Food { Id = 7, Name = "Flaxseeds", IdName = "flaxseeds", RecommendedAmount = 1, IconPath = "ms-appx:///Assets/Foods/flaxseeds.png", IsSupplement = false },
                new Food { Id = 8, Name = "Nuts & Seeds", IdName = "nuts", RecommendedAmount = 1, IconPath = "ms-appx:///Assets/Foods/nuts.png", IsSupplement = false },
                new Food { Id = 9, Name = "Herbs & Spices", IdName = "herbs_and_spices", RecommendedAmount = 1, IconPath = "ms-appx:///Assets/Foods/herbs_and_spices.png", IsSupplement = false },
                new Food { Id = 10, Name = "Whole Grains", IdName = "whole_grains", RecommendedAmount = 3, IconPath = "ms-appx:///Assets/Foods/whole_grains.png", IsSupplement = false },
                new Food { Id = 11, Name = "Beverages", IdName = "beverages", RecommendedAmount = 5, IconPath = "ms-appx:///Assets/Foods/beverages.png", IsSupplement = false },
                new Food { Id = 12, Name = "Exercise", IdName = "exercise", RecommendedAmount = 1, IconPath = "ms-appx:///Assets/Foods/exercise.png", IsSupplement = false },
                new Food { Id = 13, Name = "Vitamin B12", IdName = "vitamin_b12", RecommendedAmount = 1, IconPath = "ms-appx:///Assets/Foods/vitamin_b12.png", IsSupplement = true }
            };
        }

        public static Food GetById(int id)
        {
            return GetAllFoods().Find(f => f.Id == id);
        }

        public static Food GetByName(string name)
        {
            return GetAllFoods().Find(f => f.Name == name);
        }

        public static Food GetByIdName(string idName)
        {
            return GetAllFoods().Find(f => f.IdName == idName);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}