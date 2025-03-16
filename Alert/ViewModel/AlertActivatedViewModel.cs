using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Alert.DataTypes;

namespace Alert.ViewModel
{
    [QueryProperty("Info", "Info")]
    public partial class AlertActivatedViewModel : ObservableObject
    {
        [ObservableProperty]
        private AlertInfo? info;

        [ObservableProperty]
        private int pin = 696969;
    }
}
