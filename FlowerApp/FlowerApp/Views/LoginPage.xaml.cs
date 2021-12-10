using System;
using System.Linq;
using Xamarin.Forms;
using FlowerApp.Models;
using FlowerApp.Data;

namespace FlowerApp.Views
{
	public partial class LoginPage : ContentPage
	{
		public string user_type = "User";

		public LoginPage ()
		{
			InitializeComponent();
		}

		async void OnSignUpButtonClicked (object sender, EventArgs e)
		{
			await Navigation.PushAsync (new SignUpPage ());
		}

		async void OnLoginButtonClicked (object sender, EventArgs e)
		{
			ShopModelDatabase database = await ShopModelDatabase.Instance;

			if (user_type == "User")
			{
				var entered_details = new Users
				{
					UserUsername = usernameEntry.Text,
					UserPassword = passwordEntry.Text
				};

				var user = await database.GetUserAsync(entered_details.UserUsername);
				if (user !=null){ 

					var isValid = AreUserCredentialsCorrect(user, entered_details);
					if (isValid )
					{
						App.IsUserLoggedIn = true;
						await database.SaveUserAsync(user);
						var nextpage = new MainPage();
						nextpage.BindingContext = user;
						Navigation.InsertPageBefore(nextpage, this);
						await Navigation.PopAsync();
					}
					else
					{
						messageLabel.Text = "Wrong User Password";
						passwordEntry.Text = string.Empty;
					}
				}
                else
                {
					messageLabel.Text = "Wrong User Username";
					passwordEntry.Text = string.Empty;
				}
			}
			else if (user_type=="Admin")
            {
				var entered_details = new Admins()
				{
					AdminUsername = usernameEntry.Text,
					AdminPassword = passwordEntry.Text,
				};

				var admin = await database.GetAdminAsync(entered_details.AdminUsername);

				
				if (admin != null)
				{
					var isValid = AreAdminCredentialsCorrect(admin, entered_details);

					if (isValid)
					{
						//var rootPage = Navigation.NavigationStack.FirstOrDefault();
						//rootPage != null
						//if ()
						//{
						App.IsAdminLoggedIn = true;
						await database.SaveAdminAsync(admin);
						var nextpage = new AdminPage();
						nextpage.BindingContext = admin;
						Navigation.InsertPageBefore(nextpage, this);
						await Navigation.PopAsync();
						//}
					}
					else
					{
						messageLabel.Text = "Wrong Admin Password";
						passwordEntry.Text = string.Empty;
					}
				}
                else
                {
					messageLabel.Text = "Wrong Admin Username";
					passwordEntry.Text = string.Empty;
				}
            }
		}

		bool AreUserCredentialsCorrect (Users user, Users entry)
		{
			return user.UserUsername == entry.UserUsername && user.UserPassword == entry.UserPassword;
		}

		bool AreAdminCredentialsCorrect(Admins admin, Admins entry)
		{
			return admin.AdminUsername == entry.AdminUsername && admin.AdminPassword == entry.AdminPassword;
		}


		private void AccountType_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = sender as RadioButton;
			user_type = (string)radioButton.Value;
		}
	}
}
