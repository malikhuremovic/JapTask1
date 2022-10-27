namespace JAPManagement.Core.Interfaces
{
    public interface IHangfireReportService
    {
        Task<string> PerformCheck();
    }
}
