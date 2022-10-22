using System.Text.Json.Serialization;

namespace JAPManagementSystem.Models.StudentModel
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StudentItemStatus
    {
        NotStarted = 0,
        Started = 1,
        Finished = 2
    }
}
