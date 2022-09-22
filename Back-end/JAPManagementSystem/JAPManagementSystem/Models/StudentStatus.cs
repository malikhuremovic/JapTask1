using System.Text.Json.Serialization;

namespace JAPManagementSystem.Models
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
