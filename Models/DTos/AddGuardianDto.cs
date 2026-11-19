using System.ComponentModel.DataAnnotations;

namespace school.Models.DTos
{
    public record AddGuardianDto(
       [Required] string Name,
       [Required,EmailAddress,StringLength(254), RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$",
    ErrorMessage = "Enter a valid email address, e.g. name@example.com")] string Email,
       [Required,RegularExpression(@"^(\+254|0)[17]\d{8}$",
        ErrorMessage = "Invalid phone number format")] string PhoneNumber,
       [Required] string Residence);
}
