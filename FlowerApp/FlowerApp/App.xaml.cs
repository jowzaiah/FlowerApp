using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FlowerApp.Views;
using FlowerApp.Models;
using FlowerApp.Data;
using System.IO;

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


        async void DeleteDatabase()
        {
            //string filename = "FlowerAppSQLite.db3";
            //ShopModelDatabase database = await ShopModelDatabase.Instance;
            //SQLiteConnection connection = new SQLiteConnection("Data Source=" + filename + ";Version=3;");
            if(ShopModelDatabase.Database != null)
            {
                SQLite.SQLiteAsyncConnection db_conn = ShopModelDatabase.Database;
                await db_conn.CloseAsync();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.Delete(DBConstants.DatabasePath);
                //Console.Write(File.Delete(DBConstants.DatabasePath));
            }
            
        }

        protected override void OnStart()
        {
            DeleteDatabase();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
