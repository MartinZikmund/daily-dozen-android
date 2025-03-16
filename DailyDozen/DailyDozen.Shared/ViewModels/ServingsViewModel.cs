using DailyDozen.Shared.Models;
using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DailyDozen.Shared.ViewModels
{
    public class ServingsViewModel : ViewModelBase
    {
        private readonly Servings _servings;
        private readonly MainViewModel _mainViewModel;
        private int _count;
        private int _streak;

        public int Id => _servings.Id;
        
        public Food Food => _servings.Food;
        
        public Day Day => _servings.Day;

        public int Count
        {
            get => _count;
            set
            {
                if (SetProperty(ref _count, value))
                {
                    _servings.Count = value;
                    SaveServings();
                    _mainViewModel.CalculateTotalFoodServings();
                }
            }
        }

        public int Streak
        {
            get => _streak;
            set => SetProperty(ref _streak, value);
        }

        public int RecommendedAmount => Food.RecommendedAmount;

        public ICommand IncrementCountCommand { get; }
        public ICommand DecrementCountCommand { get; }
        public ICommand ShowInfoCommand { get; }

        public ServingsViewModel(Servings servings, MainViewModel mainViewModel)
        {
            _servings = servings;
            _mainViewModel = mainViewModel;
            _count = servings.Count;
            _streak = servings.Streak;

            IncrementCountCommand = new XamlUICommand(() =>
            {
                if (Count < RecommendedAmount)
                    Count++;
            });

            DecrementCountCommand = new XamlUICommand(() =>
            {
                if (Count > 0)
                    Count--;
            });

            ShowInfoCommand = new XamlUICommand(() =>
            {
                // Navigation to info page will be implemented later
            });
        }

        private void SaveServings()
        {
            using (var db = new DailyDozenDbContext())
            {
                var servings = db.Servings.Find(_servings.Id);
                if (servings != null)
                {
                    servings.Count = _count;
                    
                    // Recalculate streak
                    if (_count == RecommendedAmount)
                    {
                        // Try to find previous day's servings
                        var previousDay = new Day(Day.DateTime.AddDays(-1));
                        var previousDayServings = db.Servings
                            .FirstOrDefault(s => s.DayId == previousDay.Id && s.FoodId == Food.Id);
                        
                        servings.RecalculateStreak(previousDayServings);
                        _streak = servings.Streak;
                    }
                    else if (_count < RecommendedAmount)
                    {
                        servings.Streak = 0;
                        _streak = 0;
                    }
                    
                    db.SaveChanges();
                    OnPropertyChanged(nameof(Streak));
                }
            }
        }
    }
}