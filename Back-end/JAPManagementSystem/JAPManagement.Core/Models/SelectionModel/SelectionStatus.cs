using System.Text.Json.Serialization;

namespace JAPManagement.Core.Models.SelectionModel
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SelectionStatus
    {
        Active = 1,
        Completed
    }
}
