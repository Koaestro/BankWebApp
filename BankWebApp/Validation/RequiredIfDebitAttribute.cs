using BankWebApp.Enums;
using BankWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace BankWebApp.Validation
{
    public class RequiredIfDebitAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var transaction = (Transaction)validationContext.ObjectInstance;

            // Check if the transaction type is debit
            if (transaction.Type == TransactionType.Debit)
            {
                // Check if ItemName is null or empty
                if (string.IsNullOrEmpty((string)value))
                {
                    return new ValidationResult("ItemName is required for debit transactions.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
