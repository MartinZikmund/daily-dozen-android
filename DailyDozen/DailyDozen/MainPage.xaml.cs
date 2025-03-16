using DailyDozen.Shared.Models;
using DailyDozen.Shared.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace DailyDozen
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(Microsoft.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            // Ensure the database is created when the app starts
            using (var db = new DailyDozenDbContext())
            {
                db.Database.EnsureCreated();
            }
        }
    }
}
