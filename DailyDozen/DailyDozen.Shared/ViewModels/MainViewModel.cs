using DailyDozen.Shared.Helpers;
using DailyDozen.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DailyDozen.Shared.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly DailyDozenDbContext _dbContext;
        private Day _selectedDay;
        private ObservableCollection<ServingsViewModel> _foodServings;
        private ObservableCollection<TweakServingsViewModel> _tweakServings;
        private bool _isDailyDozenMode = true;
        private int _totalFoodServings;
        private int _totalTweakServings;

        public Day SelectedDay
        {
            get => _selectedDay;
            set
            {
                if (SetProperty(ref _selectedDay, value))
                {
                    LoadServingsForSelectedDay();
                }
            }
        }

        public ObservableCollection<ServingsViewModel> FoodServings
        {
            get => _foodServings;
            set => SetProperty(ref _foodServings, value);
        }

        public ObservableCollection<TweakServingsViewModel> TweakServings
        {
            get => _tweakServings;
            set => SetProperty(ref _tweakServings, value);
        }

        public bool IsDailyDozenMode
        {
            get => _isDailyDozenMode;
            set => SetProperty(ref _isDailyDozenMode, value);
        }

        public int TotalFoodServings
        {
            get => _totalFoodServings;
            set => SetProperty(ref _totalFoodServings, value);
        }

        public int TotalTweakServings
        {
            get => _totalTweakServings;
            set => SetProperty(ref _totalTweakServings, value);
        }

        public ICommand GoToPreviousDayCommand { get; }
        public ICommand GoToNextDayCommand { get; }
        public ICommand GoToTodayCommand { get; }
        public ICommand ToggleModeCommand { get; }

        public MainViewModel()
        {
            _dbContext = new DailyDozenDbContext();
            _dbContext.Database.EnsureCreated();

            FoodServings = new ObservableCollection<ServingsViewModel>();
            TweakServings = new ObservableCollection<TweakServingsViewModel>();

            GoToPreviousDayCommand = new XamlUICommand(() => NavigateToDayOffset(-1));
            GoToNextDayCommand = new XamlUICommand(() => NavigateToDayOffset(1));
            GoToTodayCommand = new XamlUICommand(() => SelectedDay = Day.GetToday());
            ToggleModeCommand = new XamlUICommand(() => IsDailyDozenMode = !IsDailyDozenMode);

            InitializeAsync();
        }

        private void NavigateToDayOffset(int offset)
        {
            if (SelectedDay != null)
            {
                SelectedDay = new Day(SelectedDay.DateTime.AddDays(offset));
            }
        }

        private async void InitializeAsync()
        {
            await Task.Run(() => EnsureDatabaseIsInitialized());
            SelectedDay = Day.GetToday();
        }

        private void EnsureDatabaseIsInitialized()
        {
            // Check if we need to create the day for today
            var today = Day.GetToday();
            var existingDay = _dbContext.Days
                .FirstOrDefault(d => d.DateString == today.DateString);

            if (existingDay == null)
            {
                _dbContext.Days.Add(today);
                _dbContext.SaveChanges();
            }
        }

        private void LoadServingsForSelectedDay()
        {
            // First check if the day exists in the database
            var dbDay = _dbContext.Days
                .FirstOrDefault(d => d.DateString == SelectedDay.DateString);

            if (dbDay == null)
            {
                // Add the day to the database
                _dbContext.Days.Add(SelectedDay);
                _dbContext.SaveChanges();
                dbDay = SelectedDay;
            }
            else
            {
                // Update the selected day with database values
                SelectedDay = dbDay;
            }

            // Load food servings for the selected day
            LoadFoodServings();

            // Load tweak servings for the selected day
            LoadTweakServings();
        }

        private void LoadFoodServings()
        {
            FoodServings.Clear();

            // Get all foods
            var allFoods = Food.GetAllFoods();

            // Get existing servings for the selected day
            var existingServings = _dbContext.Servings
                .Include(s => s.Food)
                .Where(s => s.DayId == SelectedDay.Id)
                .ToList();

            // Create view models for each food
            foreach (var food in allFoods)
            {
                var serving = existingServings.FirstOrDefault(s => s.FoodId == food.Id);
                
                // If no serving exists for this food on this day, create one
                if (serving == null)
                {
                    serving = new Servings(SelectedDay, food);
                    _dbContext.Servings.Add(serving);
                    _dbContext.SaveChanges();
                }

                var servingViewModel = new ServingsViewModel(serving, this)
                {
                    IconSource = IconHelper.GetIconPath(food.IdName)
                };
                
                FoodServings.Add(servingViewModel);
            }

            CalculateTotalFoodServings();
        }

        private void LoadTweakServings()
        {
            TweakServings.Clear();

            // Get all tweaks
            var allTweaks = Tweak.GetAllTweaks();

            // Get existing servings for the selected day
            var existingServings = _dbContext.TweakServings
                .Include(s => s.Tweak)
                .Where(s => s.DayId == SelectedDay.Id)
                .ToList();

            // Create view models for each tweak
            foreach (var tweak in allTweaks)
            {
                var serving = existingServings.FirstOrDefault(s => s.TweakId == tweak.Id);
                
                // If no serving exists for this tweak on this day, create one
                if (serving == null)
                {
                    serving = new TweakServings(SelectedDay, tweak);
                    _dbContext.TweakServings.Add(serving);
                    _dbContext.SaveChanges();
                }

                var servingViewModel = new TweakServingsViewModel(serving, this)
                {
                    IconSource = IconHelper.GetIconPath(tweak.IdName, true)
                };
                
                TweakServings.Add(servingViewModel);
            }

            CalculateTotalTweakServings();
        }

        public void CalculateTotalFoodServings()
        {
            TotalFoodServings = FoodServings.Sum(s => s.Count);
        }

        public void CalculateTotalTweakServings()
        {
            TotalTweakServings = TweakServings.Sum(s => s.Count);
        }
    }
}