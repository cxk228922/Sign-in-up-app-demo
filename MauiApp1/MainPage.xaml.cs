using MauiApp1.Helpers;
using MauiApp1.Services;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void forGotPassword(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewPage1());
        }
        private void SignUp(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }
        private async void Login(object sender, EventArgs e)
        {
            var username = account.Text; 
            var password = pass.Text;
            
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Username and password are required.", "OK");
                return;
            }

            try
            {
                var user = await DatabaseService.Instance.GetUserByUsernameAsync(username);

                if (user == null)
                {
                    await DisplayAlert("Error", "User not found.", "OK");
                    return;
                }

                
                bool isPasswordValid = password == AESCipher.Decrypt(user.Password);

                if (isPasswordValid)
                {
                    await DisplayAlert("Success", "Login successful.", "OK");
                    await Navigation.PushAsync(new VideoWindo());
                }
                else
                {
                    await DisplayAlert("Error", "Invalid password.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}
