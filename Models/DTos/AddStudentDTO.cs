using school.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace school.Models.DTos
{
    public record AddStudentDTO(
        [Required] string Fullname,
        [Required] string RegNumber,
        [Required(ErrorMessage = "Grade is required")][Range(1, 12, ErrorMessage = "Grade must be between 1 and 12")] int Grade,
        [Required(ErrorMessage = "Stream is required")] StreamEnum Stream,
        [Required(ErrorMessage = "GuardianId is required")] Guid GuardianId,
        [Required(ErrorMessage = "DOB is required")] DateOnly DOB,
        [Required] string Residence
        );
    
    
}
