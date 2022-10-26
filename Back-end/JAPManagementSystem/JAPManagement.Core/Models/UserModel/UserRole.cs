using System.Text.Json.Serialization;

namespace JAPManagement.Core.Models.UserModel
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserRole
    {
        Admin = 0,
        Student
    };
}
