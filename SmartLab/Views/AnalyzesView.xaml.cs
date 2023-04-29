using MauiPopup;
using SmartLab.ViewModels;

namespace SmartLab.Views;

public partial class AnalyzesView : ContentPage
{
	public AnalyzesView()
	{
		InitializeComponent();
		this.BindingContext = new AnalyzesViewModel();
	}

    private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
    {
		var scroll = (sender as ScrollView).ScrollY;
		if (scroll > 269.454545454545) {
            SecondCollectionView.ScrollTo(0);
            FirstCollectionViewGrid.IsVisible = true;
            SecondCollectionView.IsVisible = true;
		}
        if (scroll < 269.454545454545)
        {
            FirstCollectionView.ScrollTo(0);
            FirstCollectionViewGrid.IsVisible = false;
            SecondCollectionView.IsVisible = false;
        }
    }
}