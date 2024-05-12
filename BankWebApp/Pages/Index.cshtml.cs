using BankWebApp.Areas.Identity.Data;
using BankWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BankWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<BankWebAppUser> _userManager;
        private readonly SignInManager<BankWebAppUser> _signInManager;

        private readonly BankWebApp.Data.BankWebAppContext _context;

        public IndexModel(
            UserManager<BankWebAppUser> userManager,
            SignInManager<BankWebAppUser> signInManager,
            Data.BankWebAppContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public UserModel userModel { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class UserModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            /// 

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Full Name")]
            public string Name { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Account Number")]
            public string AccountNumber { get; set; }

            [DataType(DataType.Currency)]
            [Display(Name = "Balance")]
            public decimal Balance { get; set; }

            public ICollection<Transaction>? Transactions { get; set; }

        }

        private async Task LoadAsync(BankWebAppUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Console.WriteLine(user);
            Console.WriteLine(user.Transactions);

            userModel = new UserModel
            {
                Name = user.Name,
                AccountNumber = user.AccountNumber,
                Balance = user.Balance,
                Transactions = user.Transactions
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            
            if (user == null)
            {
                return Page();
                // return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            // Explicitly load transactions for the user
            await _context.Entry(user)
                .Collection(u => u.Transactions)
                .LoadAsync();

            await LoadAsync(user);
            return Page();
        }

       
    }
}
