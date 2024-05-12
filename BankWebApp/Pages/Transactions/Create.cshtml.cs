using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BankWebApp.Data;
using BankWebApp.Models;
using BankWebApp.Enums;
using Microsoft.AspNetCore.Identity;
using BankWebApp.Areas.Identity.Data;

namespace BankWebApp.Pages.Transactions
{
    public class CreateModel : PageModel
    {
        private readonly BankWebApp.Data.BankWebAppContext _context;
        private readonly UserManager<BankWebAppUser> _userManager;

        public CreateModel(BankWebApp.Data.BankWebAppContext context, UserManager<BankWebAppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Transaction Transaction { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine(ModelState.IsValid);
          if (!ModelState.IsValid || _context.Transaction == null || Transaction == null)
            {
                return Page();
            }
            var user = await _userManager.GetUserAsync(User);

            Transaction.User = user;
            _context.Transaction.Add(Transaction);

            // Move stuff below into another method with all the valid testing and stuff

            if (Transaction.Type == TransactionType.Credit)
            {
                user.Balance += Transaction.Amount;
            }
            else if (Transaction.Type == TransactionType.Debit)
            {
                user.Balance -= Transaction.Amount;
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
