using CommunityToolkit.Mvvm.ComponentModel;


namespace Alert.DataTypes
{
    public partial class AlertInfo : ObservableObject
    {
        public AlertInfo(TimeOnly time, string name)
        {
            this.Time = time;
            this.Name = name;
        }

        [ObservableProperty]
        TimeOnly time;

        [ObservableProperty]
        string name;
    }
}
