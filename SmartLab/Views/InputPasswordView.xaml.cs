using SmartLab.ViewModels;

namespace SmartLab.Views;

public partial class InputPasswordView : ContentPage
{
    IDispatcherTimer timer;
    public InputPasswordView()
	{
		InitializeComponent();
        this.BindingContext = new InputPasswordViewModel();
	}
    private void timerCallback(object sender)
    {
        (sender as Button).BackgroundColor = Color.FromArgb("#F5F5F9");
        (sender as Button).TextColor = Color.FromArgb("#000000");
    }

    private void Btn1_Clicked(object sender, EventArgs e)
    {
        timer = Application.Current.Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(0.200);
        timer.Tick += (s, e) => timerCallback(sender);
        timer.Start();

        (sender as Button).BackgroundColor = Color.FromArgb("#1A6FEE");
        (sender as Button).TextColor = Color.FromArgb("#FFFFFF");
    }
}