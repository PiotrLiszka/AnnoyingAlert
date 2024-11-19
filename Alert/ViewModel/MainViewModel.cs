using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Alert.DataTypes;

namespace Alert.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        int i = 0;
        public MainViewModel()
        {
            Alerts = new ObservableCollection<AlertInfo>();
        }

        [ObservableProperty]
        ObservableCollection<AlertInfo> alerts;

        // test
        [RelayCommand]
        void Add()
        {
            // add new alert
            Alerts.Add(new AlertInfo(TimeOnly.FromDateTime(DateTime.Now), $"yessir-{i}"));
            i++;
        }

        //  test
        [RelayCommand]
        async Task Tap(AlertInfo ai)
        {
            await Shell.Current.GoToAsync("Alert Details",
                new Dictionary<string, object>
                {
                    {"Info", ai },
                    {"Name", ai.Name },
                    {"Time", ai.Time.ToTimeSpan() }
                }
            );
        }

    }
}
