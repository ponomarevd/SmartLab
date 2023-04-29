using SmartLab.ViewModels;

namespace SmartLab.Views;

public partial class OnBoardView : ContentPage
{
	public OnBoardView()
	{
		InitializeComponent();
		this.BindingContext = new OnBoardViewModel();

        if (!VersionTracking.IsFirstLaunchEver)
        {
            AppShell.Current.GoToAsync($"//{nameof(LoginOrRegisterView)}");
        }

        if (Preferences.Get("UserMail", null) != null && Preferences.Get("UserCode", null) != null)
        {
            AppShell.Current.GoToAsync($"//{nameof(CreatePasswordView)}");
        }
        if (Preferences.Get("UserPassword", null) != null)
        {
            AppShell.Current.GoToAsync($"//{nameof(InputPasswordView)}");
        }
    }
}