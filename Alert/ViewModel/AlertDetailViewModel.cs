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
[QueryProperty("IsAlertNew", "IsAlertNew")]

public partial class AlertDetailViewModel : ObservableObject
{
    [ObservableProperty]
    private bool isAlertNew;

    [ObservableProperty]
    private TimeSpan time;

    [ObservableProperty]
    private string? name;

    [ObservableProperty]
    private AlertInfo? info;

    [RelayCommand]
    private async Task Back(bool cancelled = true)
    {
        if (Info is null)
            return;

        if (cancelled == false)
        { 
            // if backed from existing alert, just go back to MainView
            if (IsAlertNew == false)
            {
                await Shell.Current.GoToAsync($"..", animate: true,
                    new Dictionary<string,object>
                    {
                        {"Cancelled", cancelled }
                    });
                return;
            }
            // if saved new alert, go back to MainView and add it to ObservableCollection
            await Shell.Current.GoToAsync($"..", animate: true,
                new Dictionary<string, object>
            {
                {"ModifiedAlert", this.Info},
                //{"Cancelled", cancelled }
            });
            return;
        }

        // if back is clicked with new alert, just go back to MainView
        await Shell.Current.GoToAsync($"..?Cancelled = {cancelled}", animate: true);
    }

    [RelayCommand]
    private async Task Save()
    {
        if (Info is null)
            return;
        if (Name is null || Name == string.Empty)
            return;

        // save new alert information to Info <AlertInfo> object and then go back to main
        Info.Time = TimeOnly.FromTimeSpan(Time);
        Info.Name = Name;
        Info.Active = true;
        await Back(false);
    }

}
