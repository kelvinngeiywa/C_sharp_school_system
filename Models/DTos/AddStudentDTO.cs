using school.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace school.Models.DTos
{
    public record AddStudentDTO(
        [Required] string Fullname,
        [Required] string RegNumber,
        [Required] int Grade,
        [Required] StreamEnum Stream,
        [Required] Guid GuardianId,
        [Required] DateOnly DOB,
        [Required] string Residence
        );
    
    
}
