using Newtonsoft.Json;
using SmartLab.Core;
using SmartLab.Models;
using SmartLab.ViewModels;

namespace SmartLab.Views;

public partial class EmailCodeView : ContentPage
{
    private readonly HttpRequests _httpRequests;
    public EmailCodeView()
    {
        InitializeComponent();
        this.BindingContext = new EmailCodeViewModel();
        _httpRequests = new HttpRequests();
        Field1.Focus();
    }
    private void Field3_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (Field3.Text.Trim() != string.Empty)
        {
            Field4.Focus();
        }
    }

    private void Field2_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (Field2.Text.Trim() != string.Empty)
        {
            Field3.Focus();
        }
    }

    private void Field1_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (Field1.Text.Trim() != string.Empty)
        {
            Field2.Focus();
        }
    }

    private async void Field4_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            if (Field1.Text.Trim() != string.Empty || Field1.Text != null
                && Field2.Text.Trim() != string.Empty || Field2.Text != null
                && Field3.Text.Trim() != string.Empty || Field3.Text != null
                && Field4.Text.Trim() != string.Empty || Field4.Text != null)
            {
                Field4.IsEnabled = false;
                Field4.IsEnabled = true;
                this.Focus();
                string Code = Field1.Text + Field2.Text + Field3.Text + Field4.Text;
                string response = await _httpRequests.SignIn(Preferences.Get("UserMail", null), Code);

                if (response.Contains("errors"))
                    return;
                else
                {
                    UserToken tokenModel = JsonConvert.DeserializeObject<UserToken>(response);
                    string token = tokenModel.token;
                    Preferences.Set("UserToken", token);

                    Preferences.Set("UserCode", Code);
                    await AppShell.Current.GoToAsync($"//{nameof(CreatePasswordView)}");
                }
            }
            else
            {
                return;
            }
        }
        catch (Exception)
        {
            return;
        }

    }
}