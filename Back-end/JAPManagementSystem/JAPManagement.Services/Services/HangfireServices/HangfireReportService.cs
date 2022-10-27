using JAPManagement.Core.Interfaces;
using JAPManagement.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace JAPManagement.Services.Services.HangfireServices
{
    public class HangfireReportService : IHangfireReportService
    {
        private readonly DataContext _context;
        private readonly ISelectionService _selectionService;
        private readonly IEmailService _emailService;
        public HangfireReportService(DataContext context, ISelectionService selectionService, IEmailService emailService)
        {
            _context = context;
            _selectionService = selectionService;
            _emailService = emailService;
        }
        public async Task<string> PerformCheck()
        {
            DateTime dateNow = DateTime.Now;
            var selections = await _context.Selections.Where(s => s.DateEnd.Month == dateNow.Month && s.DateEnd.Day == dateNow.Day).ToListAsync();
            var reportResponse = await _selectionService.GetSelectionsReport();
            selections.ForEach(selection =>
            {
                try
                {
                    var currentSelectionReport = reportResponse.Data.First(sel => sel.SelectionName.Equals(selection.Name));
                    _emailService.SendConfirmationEmail(currentSelectionReport);
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
                Console.WriteLine("Success");
            });
            return "The length is: " + selections.Count();
        }
    }
}


