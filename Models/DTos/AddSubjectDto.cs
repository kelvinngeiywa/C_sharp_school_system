using System.ComponentModel.DataAnnotations;

namespace school.Models.DTos
{
    public record AddSubjectDto(
        [Required] string SubjectName,
        [Required] string SubjectCode
        );
    
}
