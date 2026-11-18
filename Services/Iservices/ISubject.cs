using school.Models.DTos;

namespace school.Services.Iservices
{
    public interface ISubject
    {
        Task<SubjectResponseDto> AddSubjectAsync(AddSubjectDto addSubjectDto);
        Task<List<SubjectResponseDto>> GetAllSubjects();
    }
}
