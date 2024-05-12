using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BankWebApp.Data;
using BankWebApp.Models;

namespace BankWebApp.Pages.Transactions
{
    public class IndexModel : PageModel
    {
        private readonly BankWebApp.Data.BankWebAppContext _context;

        public IndexModel(BankWebApp.Data.BankWebAppContext context)
        {
            _context = context;
        }

        public IList<Transaction> Transaction { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Transaction != null)
            {
                Transaction = await _context.Transaction.ToListAsync();
            }
        }
    }
}
