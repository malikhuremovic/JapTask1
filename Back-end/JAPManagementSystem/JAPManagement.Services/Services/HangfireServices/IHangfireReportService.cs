namespace JAPManagement.Services.Services.HangfireServices
{
    public interface IHangfireReportService
    {
        Task<string> PerformCheck();
    }
}
