using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json.Serialization;


namespace Alert.DataTypes
{
    
    public partial class AlertInfo : ObservableObject
    {
        public AlertInfo(TimeOnly time, string name, bool active)
        {
            this.Time = time;
            this.Name = name;
            this.Active = active;
        }

        [ObservableProperty]
        [JsonIgnore]
        //[JsonPropertyName("time")]
        TimeOnly time;

        [ObservableProperty]
        [JsonIgnore]
        //[JsonPropertyName("name")]
        string name;

        [ObservableProperty]
        [JsonIgnore]
        //[JsonPropertyName("active")]
        bool active;
    }
}
