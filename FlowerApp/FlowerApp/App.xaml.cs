using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FlowerApp.Views;

namespace FlowerApp
{
    public partial class App : Application
    {
        public static bool IsUserLoggedIn { get; set; }
        public static bool IsAdminLoggedIn { get; set; }

        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();

            if (!(IsUserLoggedIn || IsAdminLoggedIn))
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
        }
    

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
