using System;
using Xamarin.Forms;

namespace FlowerApp.Views
{
	public partial class MainPage : TabbedPage
	{
		public MainPage ()
		{
			InitializeComponent ();
		}

		async void OnLogoutButtonClicked (object sender, EventArgs e)
		{
			App.IsUserLoggedIn = false;
			Navigation.InsertPageBefore (new LoginPage (), this);
			await Navigation.PopAsync ();
		}
	}
}
