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
using System.Text.Json.Serialization.Metadata;

namespace Alert.ViewModel
{
    [QueryProperty("ModifiedAlert", "ModifiedAlert")]
    [QueryProperty("Cancelled", "Cancelled")]

    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            //File.Delete(savePath);
            Alerts = ReadAlertsFromStorage();

            Alerts.CollectionChanged += SaveAlertChanges;
        }

        private readonly string savePath = Path.Combine(FileSystem.Current.AppDataDirectory, "alerts.json");

        [ObservableProperty]
        private ObservableCollection<AlertInfo> alerts;

        [ObservableProperty]
        private bool cancelled;

        [ObservableProperty]
        private AlertInfo? modifiedAlert;

        // test
        [RelayCommand]
        private async Task AddAlert()
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
        private async Task Tap(AlertInfo ai)
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
        private void DeleteAlert(AlertInfo ai)
        {
            Alerts.Remove(ai);
        }
        
        private void SaveAlertChanges(object? sender, NotifyCollectionChangedEventArgs args)
        {
            if (Alerts.Count == 0)
                return;

            // TODO : save data locally, read it at startup
            var jsonAlerts = JsonSerializer.Serialize(Alerts);

            File.WriteAllText(savePath, jsonAlerts);
        }

        /// <summary>
        /// Reads alert list from locally stored json file
        /// </summary>
        private ObservableCollection<AlertInfo> ReadAlertsFromStorage()
        {
            if (!File.Exists(savePath))
                return [];

            string jsonString = File.ReadAllText(savePath);
            
            if (string.IsNullOrEmpty(jsonString))
                return [];

            var alertInfo = JsonSerializer.Deserialize<ObservableCollection<AlertInfo>>(jsonString);

            if (alertInfo is not null)
                return alertInfo;

            return [];

        }

        partial void OnModifiedAlertChanged(AlertInfo? value)
        {
            if (ModifiedAlert == null)
                return;

            Alerts.Add(ModifiedAlert);            
        }

    }
}
