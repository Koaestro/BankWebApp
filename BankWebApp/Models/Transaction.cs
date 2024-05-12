using BankWebApp.Areas.Identity.Data;
using BankWebApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace BankWebApp.Models
{
    public class Transaction
    {
        public int ID { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public TransactionType Type { get; set; } // Type of the transaction (Credit or Debit)
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Timestamp { get; set; }

        public BankWebAppUser? User { get; set; }

        // Additional properties for purchases
        public string? ItemName { get; set; }
    }
}
