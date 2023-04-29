using SmartLab.ViewModels;

namespace SmartLab.Views;

public partial class PatientCardView : ContentPage
{
	public PatientCardView()
	{
		InitializeComponent();
		this.BindingContext = new PatientCardViewModel();
	}
}