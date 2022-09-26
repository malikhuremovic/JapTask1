using System.Text.Json.Serialization;

namespace JAPManagementSystem.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SelectionStatus
    {
        Active = 1,
        Completed
    }
}
