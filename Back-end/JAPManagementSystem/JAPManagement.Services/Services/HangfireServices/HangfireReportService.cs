using JAPManagement.Core.Interfaces.Repositories;
using JAPManagement.Core.Interfaces.Services;
using JAPManagement.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace JAPManagement.Services.Services.HangfireServices
{
    public class HangfireReportService : IHangfireReportService
    {
        private readonly ISelectionRepository _selections;
        private readonly ISelectionService _selectionService;
        private readonly IEmailService _emailService;
        public HangfireReportService(ISelectionRepository selections, ISelectionService selectionService, IEmailService emailService)
        {
            _selections = selections;
            _selectionService = selectionService;
            _emailService = emailService;
        }
        public async Task PerformCheck()
        {
            DateTime dateNow = DateTime.Now;
            var selections = await _selections.GetByEndMonthAndDay(dateNow.Month, dateNow.Day);
            var reportResponse = await _selectionService.GetSelectionsReport();
            selections.ForEach(selection =>
            {
                var currentSelectionReport = reportResponse.Data.First(sel => sel.SelectionName.Equals(selection.Name));
                _emailService.SendConfirmationEmail(currentSelectionReport);
            });
        }
    }
}


