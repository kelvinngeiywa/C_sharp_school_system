using school.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace school.Models.DTos
{
    public record CreateUserCredentialsDTO(
        [Required(ErrorMessage = "Stream is required")] AccountTypeEnum AccountType,
           [Required,EmailAddress,StringLength(254), RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$",
    ErrorMessage = "Enter a valid email address, e.g. name@example.com")]
        string Email,
            [Required(ErrorMessage = "Password is required")]
    [StringLength(64, MinimumLength = 8,
        ErrorMessage = "Password must be between 8 and 64 characters")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\w\s]).+\z",
        ErrorMessage = "Password must contain an uppercase letter, a lowercase letter, a digit and a special character")]
        string Password
        
        );
    
    
}
