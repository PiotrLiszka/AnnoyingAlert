using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alert.DataTypes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Alert.ViewModel;

[QueryProperty("Info", "Info")]
[QueryProperty("Name", "Name")]
[QueryProperty("Time", "Time")]

public partial class AlertDetailViewModel : ObservableObject
{
    [ObservableProperty]
    TimeSpan time;

    [ObservableProperty]
    string? name;

    [ObservableProperty]
    AlertInfo? info;

    [RelayCommand]
    async Task Back()
    {
        await Shell.Current.GoToAsync("..", animate: true);
    }
    [RelayCommand]
    async Task Save()
    {
        if (Info is null)
            return;
        if (Name is null || Name == string.Empty)
            return;

        // save and then go back to main
        Info.Time = TimeOnly.FromTimeSpan(Time);
        Info.Name = Name;
        await Back();
    }
}
