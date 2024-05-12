using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BankWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace BankWebApp.Areas.Identity.Data;

// Add profile data for application users by adding properties to the BankWebAppUser class
public class BankWebAppUser : IdentityUser
{
    [PersonalData]
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    public string AccountNumber
    {
        get
        {
            return Id;
        }
    }
    public decimal Balance { get; set; }

    public ICollection<Transaction>? Transactions { get; set; }
}

