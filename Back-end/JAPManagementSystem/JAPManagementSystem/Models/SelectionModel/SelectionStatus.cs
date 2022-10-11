using System.Text.Json.Serialization;

namespace JAPManagementSystem.Models.SelectionModel
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SelectionStatus
    {
        Active = 1,
        Completed
    }
}
