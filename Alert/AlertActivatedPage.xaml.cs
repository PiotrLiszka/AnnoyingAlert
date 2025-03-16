using Alert.ViewModel;

namespace Alert;

public partial class AlertActivatedPage : ContentPage
{
	public AlertActivatedPage(AlertActivatedViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		SetSizes();
	}

	private void SetSizes()
	{
#if WINDOWS
		var btnSize = 70;
#else
		var btnSize = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
#endif
        List<Button> pinButtons = new(9);
		pinButtons.AddRange([PinButton1, PinButton2, PinButton3, PinButton4, PinButton5, PinButton6, PinButton7, PinButton8, PinButton9]);

		foreach (Button button in pinButtons)
		{
#if WINDOWS
			button.WidthRequest = btnSize;
			button.HeightRequest = btnSize;
#else
			button.WidthRequest = btnSize / 4 - 15;
			button.HeightRequest = btnSize / 4 - 15;
#endif
        }
    }

    private void ContentPage_Unloaded(object sender, EventArgs e)
    {
		mediaElement.Handler?.DisconnectHandler();
    }
}