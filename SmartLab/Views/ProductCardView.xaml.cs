using MauiPopup;
using MauiPopup.Views;
using SmartLab.Core;
using SmartLab.ViewModels;

namespace SmartLab.Views;

public partial class ProductCardView : BasePopupPage
{
	public ProductCardView(CatalogItem item)
	{
		InitializeComponent();
		this.BindingContext = item;
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		await PopupAction.ClosePopup(this);
    }
}