using SmartLab.ViewModels;

namespace SmartLab.Views;

public partial class LoginOrRegisterView : ContentPage
{
	public LoginOrRegisterView()
	{
		InitializeComponent();
		this.BindingContext = new LoginOrRegisterViewModel();
	}
}