using System;
using System.Linq;
using Xamarin.Forms;
using FlowerApp.Models;
using FlowerApp.Data;

namespace FlowerApp.Views
{
	public partial class SignUpPage : ContentPage
	{
		public string user_type="User";
		public SignUpPage ()
		{
			InitializeComponent ();
		}


		async void OnSignUpButtonClicked (object sender, EventArgs e)
        {
			ShopModelDatabase database = await ShopModelDatabase.Instance;
			if (user_type == "User")
            {
				var user = new Users()
				{
					UserEmail = emailEntry.Text,
					UserUsername = usernameEntry.Text,
					UserPassword = passwordEntry.Text,
					UserFirstName = firstnameEntry.Text,
					UserLastName = lastnameEntry.Text
				};

				// Sign up logic goes here

				var signUpSucceeded = AreUserDetailsValid(user);
				if (signUpSucceeded && !database.CheckIfUserUsernameorEmailTaken(user))
				{
					var rootPage = Navigation.NavigationStack.FirstOrDefault();
					if (rootPage != null)
					{
						App.IsUserLoggedIn = true;
						await database.SaveUserAsync(user);
						var nextpage = new MainPage();
						nextpage.BindingContext = user;
						Console.Write(user);
						Navigation.InsertPageBefore(nextpage, Navigation.NavigationStack.First());
						await Navigation.PopToRootAsync();
					}
				}
				else
				{
					messageLabel.Text = "User Sign up failed";
				}
			}
            else if(user_type == "Admin")
            {
				var admin = new Admins()
				{
					AdminEmail = emailEntry.Text,
					AdminUsername = usernameEntry.Text,
					AdminPassword = passwordEntry.Text,
					AdminFirstName = firstnameEntry.Text,
					AdminLastName = lastnameEntry.Text
				};

				var signUpSucceeded = AreAdminDetailsValid(admin);
				if (signUpSucceeded && !database.CheckIfAdminUsernameorEmailTaken(admin))
				{
					var rootPage = Navigation.NavigationStack.FirstOrDefault();
					if (rootPage != null)
					{
						App.IsAdminLoggedIn = true;
						await database.SaveAdminAsync(admin);
						var nextpage = new AdminPage();
						nextpage.BindingContext = admin;
						Navigation.InsertPageBefore(nextpage, Navigation.NavigationStack.First());
						await Navigation.PopToRootAsync();
					}
				}
				else
				{
					messageLabel.Text = "Admin Sign up failed";
				}
			}


            // Sign up logic goes here
			/*
            var signUpSucceeded = AreDetailsValid (user);
			if (signUpSucceeded) {
				var rootPage = Navigation.NavigationStack.FirstOrDefault ();
				if (rootPage != null) {
					App.IsUserLoggedIn = true;
					Navigation.InsertPageBefore (new MainPage (), Navigation.NavigationStack.First ());
					await Navigation.PopToRootAsync ();
				}
			} else {
				messageLabel.Text = "Sign up failed";
			}
			*/
		}

		bool AreUserDetailsValid (Users user)
		{
			return (!string.IsNullOrWhiteSpace (user.UserUsername) && !string.IsNullOrWhiteSpace (user.UserPassword) && !string.IsNullOrWhiteSpace (user.UserEmail) && user.UserEmail.Contains ("@"));
		}

		bool AreAdminDetailsValid(Admins admin)
		{
			return (!string.IsNullOrWhiteSpace(admin.AdminUsername) && !string.IsNullOrWhiteSpace(admin.AdminPassword) && !string.IsNullOrWhiteSpace(admin.AdminEmail) && admin.AdminEmail.Contains("@"));
		}



		private void AccountType_CheckedChanged(object sender, EventArgs e)
        {
			RadioButton radioButton = sender as RadioButton;
			user_type = (string)radioButton.Value;
		}
    }
}
