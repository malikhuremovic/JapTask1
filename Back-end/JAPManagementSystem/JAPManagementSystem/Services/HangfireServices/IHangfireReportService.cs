namespace JAPManagementSystem.Services.HangfireServices
{
    public interface IHangfireReportService
    {
        Task<string> PerformCheck();
    }
}
