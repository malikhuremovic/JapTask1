using System.Text.Json.Serialization;

namespace JAPManagementSystem.Models.StudentModel
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StudentStatus
    {
        InProgram = 1,
        Success,
        Failed,
        Extended
    }
}
