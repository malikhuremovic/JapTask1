using System.Text.Json.Serialization;

namespace JAPManagement.Core.Models.StudentModel
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StudentItemStatus
    {
        NotStarted = 0,
        Started = 1,
        Finished = 2
    }
}
