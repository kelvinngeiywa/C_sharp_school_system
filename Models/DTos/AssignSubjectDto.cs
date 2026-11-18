using System.ComponentModel.DataAnnotations;

namespace school.Models.DTos
{
    public record AssignSubjectDto([Required] List<Guid> SubjectIds);
    
    
}
