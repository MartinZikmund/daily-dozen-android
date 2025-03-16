using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DailyDozen.Shared.Helpers
{
    public static class IconHelper
    {
        private static readonly Dictionary<string, string> PlaceholderCache = new Dictionary<string, string>();
        private static readonly Random Random = new Random();
        
        public static readonly Dictionary<string, Windows.UI.Color> CategoryColors = new Dictionary<string, Windows.UI.Color>
        {
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

        public static async Task<string> GetOrCreatePlaceholderIcon(string idName, string name, bool isTweak = false)
        {
            if (PlaceholderCache.TryGetValue(idName, out string cachedPath))
            {
                return cachedPath;
            }

            var folderName = isTweak ? "Tweaks" : "Foods";
            var localFolder = ApplicationData.Current.LocalFolder;
            var iconFolder = await localFolder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);

            var filePath = Path.Combine(iconFolder.Path, $"{idName}.png");
            if (File.Exists(filePath))
            {
                PlaceholderCache[idName] = filePath;
                return filePath;
            }
            
            // Use the first letter of the name for the placeholder
            var firstLetter = !string.IsNullOrEmpty(name) ? name[0].ToString().ToUpper() : "?";
            
            // Pick a color based on the idName or a default color
            var color = CategoryColors.TryGetValue(idName, out var specificColor) 
                ? specificColor 
                : Colors.Gray;

            // Generate placeholder icon (This will be platform-specific)
            // For now, we'll return a placeholder path
            PlaceholderCache[idName] = $"ms-appx:///Assets/{folderName}/placeholder.png";
            return PlaceholderCache[idName];
        }
    }
}