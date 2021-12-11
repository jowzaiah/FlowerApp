using System;
using System.Linq;
using Xamarin.Forms;
using FlowerApp.Models;
using FlowerApp.Data;


namespace FlowerApp.Views
{
	public partial class MainPage : TabbedPage
	{
		public MainPage ()
		{
			InitializeComponent ();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			ShopModelDatabase database = await ShopModelDatabase.Instance;
			//CatalogListView.ItemsSource = await database.GetUserAsync(user);

		}

		async void OnLogoutButtonClicked (object sender, EventArgs e)
		{
			App.IsUserLoggedIn = false;
			Navigation.InsertPageBefore (new LoginPage (), this);
			await Navigation.PopAsync ();
		}

	}


}
