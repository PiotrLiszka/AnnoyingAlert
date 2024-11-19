using Alert.ViewModel;

namespace Alert;

public partial class AlertDetailPage : ContentPage
{
	public AlertDetailPage(AlertDetailViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}