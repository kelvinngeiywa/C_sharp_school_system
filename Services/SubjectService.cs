using school.Data;
using school.Models.DTos;
using school.Services.Iservices;

namespace school.Services
{
    public class SubjectService(AppDbContext _dbContext) : ISubject
    {
        public Task<SubjectResponseDto> AddSubjectAsync(AddSubjectDto addSubjectDto)
        {
            throw new NotImplementedException();
        }

        public Task<List<SubjectResponseDto>> GetAllSubjects()
        {
            throw new NotImplementedException();
        }
    }
}
