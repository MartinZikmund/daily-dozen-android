using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DailyDozen.Shared.Models
{
    public class Tweak
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string IdName { get; set; }
        
        public int RecommendedAmount { get; set; }
        
        public string IconPath { get; set; }
        
        public string MoreInfoUrl { get; set; }

        public static List<Tweak> GetAllTweaks()
        {
            return new List<Tweak>
            {
                new Tweak { Id = 1, Name = "Front-Load Calories", IdName = "front_load_calories", RecommendedAmount = 1, IconPath = "ms-appx:///Assets/Tweaks/front_load_calories.png" },
                new Tweak { Id = 2, Name = "Negative Calorie Preloading", IdName = "negative_calorie_preloading", RecommendedAmount = 2, IconPath = "ms-appx:///Assets/Tweaks/negative_calorie_preloading.png" },
                new Tweak { Id = 3, Name = "Undistracted Meals", IdName = "undistracted_meals", RecommendedAmount = 3, IconPath = "ms-appx:///Assets/Tweaks/undistracted_meals.png" },
                new Tweak { Id = 4, Name = "20-Minute Rule", IdName = "twenty_minute_rule", RecommendedAmount = 3, IconPath = "ms-appx:///Assets/Tweaks/twenty_minute_rule.png" },
                new Tweak { Id = 5, Name = "Water Preloading", IdName = "water_preloading", RecommendedAmount = 2, IconPath = "ms-appx:///Assets/Tweaks/water_preloading.png" },
                new Tweak { Id = 6, Name = "Preload Vinegar", IdName = "preload_vinegar", RecommendedAmount = 2, IconPath = "ms-appx:///Assets/Tweaks/preload_vinegar.png" },
                new Tweak { Id = 7, Name = "Daily Black Cumin", IdName = "daily_black_cumin", RecommendedAmount = 1, IconPath = "ms-appx:///Assets/Tweaks/daily_black_cumin.png" },
                new Tweak { Id = 8, Name = "Daily Garlic", IdName = "daily_garlic", RecommendedAmount = 1, IconPath = "ms-appx:///Assets/Tweaks/daily_garlic.png" },
                new Tweak { Id = 9, Name = "Cumin", IdName = "cumin", RecommendedAmount = 1, IconPath = "ms-appx:///Assets/Tweaks/cumin.png" },
                new Tweak { Id = 10, Name = "Ginger", IdName = "ginger", RecommendedAmount = 1, IconPath = "ms-appx:///Assets/Tweaks/ginger.png" },
                new Tweak { Id = 11, Name = "Nutritional Yeast", IdName = "nutritional_yeast", RecommendedAmount = 1, IconPath = "ms-appx:///Assets/Tweaks/nutritional_yeast.png" },
                new Tweak { Id = 12, Name = "Green Tea", IdName = "green_tea", RecommendedAmount = 3, IconPath = "ms-appx:///Assets/Tweaks/green_tea.png" },
                new Tweak { Id = 13, Name = "Hydrate", IdName = "hydrate", RecommendedAmount = 5, IconPath = "ms-appx:///Assets/Tweaks/hydrate.png" },
                new Tweak { Id = 14, Name = "Deflour Diet", IdName = "deflour_diet", RecommendedAmount = 1, IconPath = "ms-appx:///Assets/Tweaks/deflour_diet.png" },
                new Tweak { Id = 15, Name = "Fast After 7pm", IdName = "fast_after_7pm", RecommendedAmount = 1, IconPath = "ms-appx:///Assets/Tweaks/fast_after_7pm.png" },
                new Tweak { Id = 16, Name = "Frontload Breakfast", IdName = "frontload_breakfast", RecommendedAmount = 1, IconPath = "ms-appx:///Assets/Tweaks/frontload_breakfast.png" },
                new Tweak { Id = 17, Name = "Time-Restrict Eating", IdName = "time_restrict_eating", RecommendedAmount = 1, IconPath = "ms-appx:///Assets/Tweaks/time_restrict_eating.png" },
                new Tweak { Id = 18, Name = "12-Hour Fast", IdName = "twelve_hour_fast", RecommendedAmount = 1, IconPath = "ms-appx:///Assets/Tweaks/twelve_hour_fast.png" },
                new Tweak { Id = 19, Name = "Exercise", IdName = "exercise_tweak", RecommendedAmount = 1, IconPath = "ms-appx:///Assets/Tweaks/exercise_tweak.png" },
                new Tweak { Id = 20, Name = "Timed Walking", IdName = "timed_walking", RecommendedAmount = 1, IconPath = "ms-appx:///Assets/Tweaks/timed_walking.png" },
                new Tweak { Id = 21, Name = "Negative Calorie Exercising", IdName = "negative_calorie_exercising", RecommendedAmount = 1, IconPath = "ms-appx:///Assets/Tweaks/negative_calorie_exercising.png" }
            };
        }

        public static Tweak GetById(int id)
        {
            return GetAllTweaks().Find(t => t.Id == id);
        }

        public static Tweak GetByName(string name)
        {
            return GetAllTweaks().Find(t => t.Name == name);
        }

        public static Tweak GetByIdName(string idName)
        {
            return GetAllTweaks().Find(t => t.IdName == idName);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}