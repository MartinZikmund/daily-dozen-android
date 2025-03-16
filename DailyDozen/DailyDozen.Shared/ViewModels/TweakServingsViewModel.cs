using DailyDozen.Shared.Models;
using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace DailyDozen.Shared.ViewModels
{
    public class TweakServingsViewModel : ViewModelBase
    {
        private readonly TweakServings _tweakServings;
        private readonly MainViewModel _mainViewModel;
        private int _count;
        private int _streak;

        public int Id => _tweakServings.Id;
        
        public Tweak Tweak => _tweakServings.Tweak;
        
        public Day Day => _tweakServings.Day;

        public int Count
        {
            get => _count;
            set
            {
                if (SetProperty(ref _count, value))
                {
                    _tweakServings.Count = value;
                    SaveServings();
                    _mainViewModel.CalculateTotalTweakServings();
                }
            }
        }

        public int Streak
        {
            get => _streak;
            set => SetProperty(ref _streak, value);
        }

        public int RecommendedAmount => Tweak.RecommendedAmount;

        public ICommand IncrementCountCommand { get; }
        public ICommand DecrementCountCommand { get; }
        public ICommand ShowInfoCommand { get; }

        public TweakServingsViewModel(TweakServings tweakServings, MainViewModel mainViewModel)
        {
            _tweakServings = tweakServings;
            _mainViewModel = mainViewModel;
            _count = tweakServings.Count;
            _streak = tweakServings.Streak;

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
                var servings = db.TweakServings.Find(_tweakServings.Id);
                if (servings != null)
                {
                    servings.Count = _count;
                    
                    // Recalculate streak
                    if (_count == RecommendedAmount)
                    {
                        // Try to find previous day's servings
                        var previousDay = new Day(Day.DateTime.AddDays(-1));
                        var previousDayServings = db.TweakServings
                            .FirstOrDefault(s => s.DayId == previousDay.Id && s.TweakId == Tweak.Id);
                        
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