namespace MauiApp1;
using System;
using MauiApp1.Helpers;
using MauiApp1.models;
using MauiApp1.Services;
using Microsoft.Maui.Controls;

public partial class NewPage1 : ContentPage
{
    private readonly EmailService _emailService;

    public NewPage1()
    {
        InitializeComponent();
        _emailService = new EmailService("smtp.gmail.com", 587, "ben042708@gmail.com", "g v h u nov u x v q w e o y h");
    }

    public void ToHomePage(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }


    private async void OnSendPasswordClicked(object sender, EventArgs e)
    {
        var email = EmailEntry.Text;

        if (string.IsNullOrWhiteSpace(email))
        {
            await DisplayAlert("Error", "Email is required.", "OK");
            return;
        }

        try
        {
            var user = await DatabaseService.Instance.GetUserByEmailAsync(email);

            if (user == null)
            {
                await DisplayAlert("Error", "User not found.", "OK");
                return;
            }
            var decryptedPassword = AESCipher.Decrypt(user.Password);
            var subject = "你的帳號密碼";
            var message = $"Username: {user.Username}\nPassword: {decryptedPassword}";
            await _emailService.SendEmailAsync(email, subject, message);
            await DisplayAlert("Success", "Email sent successfully.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }
}
