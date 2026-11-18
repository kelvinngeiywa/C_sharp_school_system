using Microsoft.EntityFrameworkCore;
using school.Data;
using school.Models;
using school.Models.DTos;
using school.Services.Iservices;

namespace school.Services
{
    public class SubjectService(AppDbContext _dbContext) : ISubject
    {
        public async Task<SubjectResponseDto> AddSubjectAsync(AddSubjectDto addSubjectDto)
        {
            var subjectExists = await _dbContext.subjects.AnyAsync(
                sb => sb.SubjectName == addSubjectDto.SubjectName || sb.SubjectCode== addSubjectDto.SubjectCode);

            if (subjectExists)
            {
                return null;
            }

            var newSubject = new Subject 
            { 
                SubjectName= addSubjectDto.SubjectName,
                SubjectCode=addSubjectDto.SubjectCode
            };

            _dbContext.subjects.Add(newSubject);
            await _dbContext.SaveChangesAsync();

            var subjectResponse = new SubjectResponseDto(newSubject.SubjectName,newSubject.SubjectCode);
            return subjectResponse;
        }

        public async Task<List<SubjectResponseDto>> GetAllSubjects()
        {
            return await _dbContext.subjects.Select(sb=> new SubjectResponseDto(sb.SubjectName,sb.SubjectCode)
                ).ToListAsync();
        }
    }
}
