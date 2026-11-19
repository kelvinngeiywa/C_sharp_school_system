using System.ComponentModel.DataAnnotations;

namespace school.Models.DTos
{
    public record AddTeacherDto(
        
        [Required] string Name,
        [Required,EmailAddress,StringLength(254), RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]",
    ErrorMessage = "Enter a valid email address, e.g. name@example.com")] string Email,
        [Required,StringLength(100)] string PhoneNumber,
          
       [Required] DateOnly DOB,
        [Required, StringLength(100)] string Residence);
}
