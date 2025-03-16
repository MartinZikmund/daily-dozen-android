using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Windows.Input;

namespace DailyDozen.Shared.Controls
{
    public sealed partial class ServingsControl : UserControl
    {
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(ServingsControl), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty IconSourceProperty =
            DependencyProperty.Register(nameof(IconSource), typeof(string), typeof(ServingsControl), new PropertyMetadata(null));

        public static readonly DependencyProperty CountProperty =
            DependencyProperty.Register(nameof(Count), typeof(int), typeof(ServingsControl), new PropertyMetadata(0));

        public static readonly DependencyProperty RecommendedAmountProperty =
            DependencyProperty.Register(nameof(RecommendedAmount), typeof(int), typeof(ServingsControl), new PropertyMetadata(1));

        public static readonly DependencyProperty StreakProperty =
            DependencyProperty.Register(nameof(Streak), typeof(int), typeof(ServingsControl), new PropertyMetadata(0, OnStreakChanged));

        public static readonly DependencyProperty ShowStreakProperty =
            DependencyProperty.Register(nameof(ShowStreak), typeof(Visibility), typeof(ServingsControl), new PropertyMetadata(Visibility.Collapsed));

        public static readonly DependencyProperty IncrementCommandProperty =
            DependencyProperty.Register(nameof(IncrementCommand), typeof(ICommand), typeof(ServingsControl), new PropertyMetadata(null));

        public static readonly DependencyProperty DecrementCommandProperty =
            DependencyProperty.Register(nameof(DecrementCommand), typeof(ICommand), typeof(ServingsControl), new PropertyMetadata(null));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string IconSource
        {
            get => (string)GetValue(IconSourceProperty);
            set => SetValue(IconSourceProperty, value);
        }

        public int Count
        {
            get => (int)GetValue(CountProperty);
            set => SetValue(CountProperty, value);
        }

        public int RecommendedAmount
        {
            get => (int)GetValue(RecommendedAmountProperty);
            set => SetValue(RecommendedAmountProperty, value);
        }

        public int Streak
        {
            get => (int)GetValue(StreakProperty);
            set => SetValue(StreakProperty, value);
        }

        public Visibility ShowStreak
        {
            get => (Visibility)GetValue(ShowStreakProperty);
            set => SetValue(ShowStreakProperty, value);
        }

        public ICommand IncrementCommand
        {
            get => (ICommand)GetValue(IncrementCommandProperty);
            set => SetValue(IncrementCommandProperty, value);
        }

        public ICommand DecrementCommand
        {
            get => (ICommand)GetValue(DecrementCommandProperty);
            set => SetValue(DecrementCommandProperty, value);
        }

        public ServingsControl()
        {
            this.InitializeComponent();
        }

        private static void OnStreakChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (ServingsControl)d;
            var streak = (int)e.NewValue;
            control.ShowStreak = streak > 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            if (IncrementCommand != null && IncrementCommand.CanExecute(null))
            {
                IncrementCommand.Execute(null);
            }
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            if (DecrementCommand != null && DecrementCommand.CanExecute(null))
            {
                DecrementCommand.Execute(null);
            }
        }
    }
}