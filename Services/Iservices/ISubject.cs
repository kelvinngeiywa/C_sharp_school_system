using school.Models.DTos;

namespace school.Services.Iservices
{
    public interface ISubject
    {
        Task<SubjectResponseDto> AddSubjectAsync(AddSubjectDto addSubjectDto);
        Task<List<SubjectResponseDto>> GetAllSubjects();
        Task<(SubjectResponseDto? data,string? errors)> GetSubjectBySubjectCode(string subjectCode);
        Task<(SubjectResponseDto? data, string? errors)> UpdateSubject(string subjectCode, AddSubjectDto addSubject);

        Task<(string? success,string? errors)> DeleteSubject(string subjectCode);
    }
}
