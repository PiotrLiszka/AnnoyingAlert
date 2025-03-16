using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Alert.DataTypes;
using System.Collections.Specialized;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Diagnostics;

namespace Alert.ViewModel
{
    [QueryProperty("ModifiedAlert", "ModifiedAlert")]
    [QueryProperty("Cancelled", "Cancelled")]

    public partial class MainViewModel : ObservableObject
    {

        private Stopwatch alertHeldDown = new();
        public MainViewModel()
        {
            //File.Delete(savePath);
            Alerts = ReadAlertsFromStorage();

            Alerts.CollectionChanged += SaveAlertChangesEvent;
        }

        private readonly string savePath = Path.Combine(FileSystem.Current.AppDataDirectory, "alerts.json");

        [ObservableProperty]
        private ObservableCollection<AlertInfo> alerts;

        [ObservableProperty]
        private bool cancelled = true;

        [ObservableProperty]
        private AlertInfo? modifiedAlert;

        [RelayCommand]
        private async Task AddAlert()
        {
            // open alert page to add a new alert
            AlertInfo ai = new AlertInfo(TimeOnly.FromDateTime(DateTime.Now), $"Alert", true);

            await Shell.Current.GoToAsync("Alert Details", animate: true, 
                new Dictionary<string, object>
                {
                    {"Info", ai },
                    {"Name", ai.Name },
                    {"Time", ai.Time.ToTimeSpan() },
                    {"IsAlertNew", true }
                });

        }

        // this command runs when alert item in UI is tapped
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
        
        // test function
        [RelayCommand]
        private async Task ActivateAlert(AlertInfo ai)
        {
            // TODO
            await Task.Delay(500);
            await Shell.Current.GoToAsync("Activate Alert", animate: true,
                new Dictionary<string, object>
                {
                    {"Info", ai}
                });
        }

        [RelayCommand]
        internal void DeleteAlert(AlertInfo ai)
        {
            Alerts.Remove(ai);
        }

        // saves changes in alerts to local file
        
        private void SaveAlertChanges()
        {
            if (Alerts.Count == 0)
                return;

            var jsonAlerts = JsonSerializer.Serialize(Alerts);

            File.WriteAllText(savePath, jsonAlerts);
        }

        private void SaveAlertChangesEvent(object? sender, NotifyCollectionChangedEventArgs args)
        {
            SaveAlertChanges();
        }

        /// <summary>
        /// Reads alert list from locally stored json file
        /// </summary>
        /// <returns>Array with saved alerts or empty array</returns>
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
        partial void OnCancelledChanged(bool value)
        {
            if (value == false)
            {
                SaveAlertChanges();
                Cancelled = true;
            }
        }

        // implementation of auto generated OnModifiedAlertChanged event
        // adds new alert to ObservableCollection
        partial void OnModifiedAlertChanged(AlertInfo? value)
        {
            if (ModifiedAlert == null)
                return;

            Alerts.Add(ModifiedAlert);
        }

    }
}
