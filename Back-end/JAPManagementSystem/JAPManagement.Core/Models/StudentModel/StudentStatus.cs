using System.Text.Json.Serialization;

namespace JAPManagement.Core.Models.StudentModel
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
