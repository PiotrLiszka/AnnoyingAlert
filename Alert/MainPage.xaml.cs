using Alert.ViewModel;
using Alert.DataTypes;
using System.Diagnostics;

namespace Alert
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }

}
