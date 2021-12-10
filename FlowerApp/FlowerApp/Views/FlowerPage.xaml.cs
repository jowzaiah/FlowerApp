using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FlowerApp.Data;
using FlowerApp.Models;

namespace FlowerApp.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlowerPage : TabbedPage
    {
        public FlowerPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            ShopModelDatabase database = await ShopModelDatabase.Instance;
            //listView.ItemsSource = await database.GetUserAsync();

        }
    }
}