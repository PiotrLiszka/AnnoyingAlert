using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

using Alert.DataTypes;
using System.Collections.Specialized;

namespace Alert.ViewModel
{
    [QueryProperty("ModifiedAlert", "ModifiedAlert")]
    [QueryProperty("Cancelled", "Cancelled")]

    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            Alerts = new ObservableCollection<AlertInfo>();
            Alerts.CollectionChanged += MyAlertsChanged;
        }

        [ObservableProperty]
        ObservableCollection<AlertInfo> alerts;

        [ObservableProperty]
        bool cancelled;

        [ObservableProperty]
        AlertInfo? modifiedAlert;

        // test
        [RelayCommand]
        async Task AddAlert()
        {
            // add new alert
            AlertInfo ai = new AlertInfo(TimeOnly.FromDateTime(DateTime.Now), $"Alert", true);

            await Shell.Current.GoToAsync("Alert Details", animate: true, 
                new Dictionary<string, object>
                {
                    {"Info", ai },
                    {"Name", ai.Name },
                    {"Time", ai.Time.ToTimeSpan() },
                    {"IsAlertNew", true }
                });

            //Alerts.Add(ai);
        }

        //  test
        [RelayCommand]
        async Task Tap(AlertInfo ai)
        {
            await Shell.Current.GoToAsync("Alert Details", animate: true,
                new Dictionary<string, object>
                {
                    {"Info", ai },
                    {"Name", ai.Name },
                    {"Time", ai.Time.ToTimeSpan() },
                    {"IsAlertNew", false }
                });
        }

        [RelayCommand]
        void DeleteAlert(AlertInfo ai)
        {
            Alerts.Remove(ai);
        }
        
        private void MyAlertsChanged(object? sender, NotifyCollectionChangedEventArgs args)
        {
            if (Alerts.Count == 0)
                return;
            // TODO : save data locally, read it at startup
            var json = JsonSerializer.Serialize(Alerts);
        }

        partial void OnModifiedAlertChanged(AlertInfo? value)
        {
            if (ModifiedAlert == null)
                return;

            Alerts.Add(ModifiedAlert);            
        }

    }
}
