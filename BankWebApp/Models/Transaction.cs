using BankWebApp.Areas.Identity.Data;
using BankWebApp.Enums;
using BankWebApp.Validation;
using System.ComponentModel.DataAnnotations;

namespace BankWebApp.Models
{
    public class Transaction
    {
        public int ID { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public TransactionType Type { get; set; } // Type of the transaction (Credit or Debit)
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Timestamp { get; set; }

        public string? UserID { get; set; }
        public BankWebAppUser? User { get; set; }

        // Additional properties for purchases
        [RequiredIfDebit]
        public string? ItemName { get; set; }
    }
}
