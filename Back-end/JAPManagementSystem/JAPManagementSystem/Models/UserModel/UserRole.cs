using System.Text.Json.Serialization;

namespace JAPManagementSystem.Models.UserModel
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserRole
    {
        Admin = 0,
        Student
    };
}
