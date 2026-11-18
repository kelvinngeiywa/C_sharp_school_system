using System.ComponentModel.DataAnnotations;

namespace school.Models.DTos
{
    public record CreateUserDto(
       [Required,StringLength(200)] string Fullname,
       [Required,EmailAddress,StringLength(200)] string Email,
        [Required,StringLength(100)] string PhoneNumber,
       [Required] string Password,
       [Required] DateOnly DOB
        );
    
    
}
