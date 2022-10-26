using Hangfire;
using Microsoft.Extensions.Hosting;

namespace JAPManagement.Services.Services.HangfireServices
{
    public class HangfireHostedService : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            RecurringJob.AddOrUpdate<HangfireReportService>(service => service.PerformCheck(), "0 17 * * *");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
