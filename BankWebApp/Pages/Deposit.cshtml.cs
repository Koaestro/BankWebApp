using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BankWebApp.Data;
using BankWebApp.Models;
using BankWebApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using BankWebApp.Enums;

namespace BankWebApp.Pages
{
    public class DepositModel : PageModel
    {
        private readonly BankWebApp.Data.BankWebAppContext _context;
        private readonly UserManager<BankWebAppUser> _userManager;


        public DepositModel(BankWebApp.Data.BankWebAppContext context, UserManager<BankWebAppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {

        ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Transaction Transaction { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Transaction == null || Transaction == null)
            {
                return Page();
            }
            var user = await _userManager.GetUserAsync(User);

            Transaction.User = user;
            Transaction.Timestamp = DateTime.Now;
            Transaction.Type = TransactionType.Credit;
            _context.Transaction.Add(Transaction);

            user.Balance += Transaction.Amount;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
