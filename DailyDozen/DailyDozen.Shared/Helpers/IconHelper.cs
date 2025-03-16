using Microsoft.UI;
using System;
using System.Collections.Generic;

namespace DailyDozen.Shared.Helpers
{
    public static class IconHelper
    {
        private static readonly Dictionary<string, string> PlaceholderCache = new Dictionary<string, string>();
        
        public static readonly Dictionary<string, Windows.UI.Color> CategoryColors = new Dictionary<string, Windows.UI.Color>
        {
            // Foods
            { "beans", Colors.Brown },
            { "berries", Colors.Purple },
            { "other_fruits", Colors.Orange },
            { "cruciferous_vegetables", Colors.Green },
            { "greens", Colors.LightGreen },
            { "other_vegetables", Colors.ForestGreen },
            { "flaxseeds", Colors.SaddleBrown },
            { "nuts", Colors.SandyBrown },
            { "herbs_and_spices", Colors.DarkGreen },
            { "whole_grains", Colors.Wheat },
            { "beverages", Colors.Blue },
            { "exercise", Colors.Red },
            { "vitamin_b12", Colors.Pink },
            
            // Tweaks
            { "front_load_calories", Colors.IndianRed },
            { "negative_calorie_preloading", Colors.LightCoral },
            { "undistracted_meals", Colors.Firebrick },
            { "twenty_minute_rule", Colors.PaleVioletRed },
            { "water_preloading", Colors.CornflowerBlue },
            { "preload_vinegar", Colors.DarkOrange },
            { "daily_black_cumin", Colors.Black },
            { "daily_garlic", Colors.WhiteSmoke },
            { "cumin", Colors.Peru },
            { "ginger", Colors.Tan },
            { "nutritional_yeast", Colors.Gold },
            { "green_tea", Colors.LightGreen },
            { "hydrate", Colors.LightBlue },
            { "deflour_diet", Colors.BurlyWood },
            { "fast_after_7pm", Colors.MidnightBlue },
            { "frontload_breakfast", Colors.Goldenrod },
            { "time_restrict_eating", Colors.CadetBlue },
            { "twelve_hour_fast", Colors.DarkCyan },
            { "exercise_tweak", Colors.DarkRed },
            { "timed_walking", Colors.Crimson },
            { "negative_calorie_exercising", Colors.DarkSlateGray }
        };

        // Until we have proper icons, we'll use a generic system icon
        public static string GetIconPath(string idName, bool isTweak = false)
        {
            if (PlaceholderCache.TryGetValue(idName, out string cachedPath))
            {
                return cachedPath;
            }
            
            // Use a system icon that's guaranteed to be available
            // This avoids issues with missing files while testing
            var path = "ms-appx:///Assets/Icons/StoreLogo.png";
            
            PlaceholderCache[idName] = path;
            return path;
        }
    }
}