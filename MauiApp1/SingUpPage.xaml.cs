namespace MauiApp1;
using System.Text.RegularExpressions;
using MauiApp1.models;
using MauiApp1.Services;
using MauiApp1.Helpers;
public partial class SignUpPage : ContentPage
{
    bool IsValidEmail(string email)
    {
        string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailPattern);
    }


    public SignUpPage()
	{
		InitializeComponent();
	}
	private void ToHomePage(object sender, EventArgs e)
	{
        Navigation.PushAsync(new MainPage());
    }

	private async void SignUp(object sender, EventArgs e)
	{
		string name = NameEntry.Text;
		string email = EmailEntry.Text;
		string password = PasswordEntry.Text;
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }
        if (!IsValidEmail(email))
		{
			await DisplayAlert("Error", "Please enter a valid email address", "OK");
			return;
		}
        if(await DatabaseService.Instance.GetUserByUsernameAsync(name)!= null)
        {
            await DisplayAlert("Error", "Username already exists", "OK");
            return;
        }
		if (password.Length < 8)
		{
			await DisplayAlert("Success", "You have successfully signed up!", "OK");
			NameEntry.Text = "";
			EmailEntry.Text = "";
			PasswordEntry.Text = "";
            var newUser = new User
            {
                Username = name,
                Email = email,
                Password = AESCipher.Encrypt(password)
            };

            await DatabaseService.Instance.AddUserAsync(newUser);
            await Navigation.PushAsync(new MainPage());
        }

	}
}