namespace JAPManagement.Core.Models.Response
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
