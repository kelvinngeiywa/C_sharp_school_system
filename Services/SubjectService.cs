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

        public async Task<(string? success, string? errors)> DeleteSubject(string subjectCode)
        {
            var subject = await _dbContext.subjects.FirstOrDefaultAsync(sb => sb.SubjectCode == subjectCode);

            if (subject is null)
            {
                return (null, "Subject not found");
            }

            _dbContext.subjects.Remove(subject);
            await _dbContext.SaveChangesAsync();

            return ("Subject deleted successfully!!", null);
        }

        public async Task<List<SubjectResponseDto>> GetAllSubjects()
        {
            return await _dbContext.subjects.Select(sb=> new SubjectResponseDto(sb.SubjectName,sb.SubjectCode)
                ).ToListAsync();
        }

        public async Task<(SubjectResponseDto? data, string? errors)> GetSubjectBySubjectCode(string subjectCode)
        {
            var subject = await _dbContext.subjects.FirstOrDefaultAsync(sb=>sb.SubjectCode == subjectCode);

            if(subject is null)
            {
                return (null, "Subject not found");
            }
            var response = toSubjectResponse(subject);

            return (response, null);
        }

        public async Task<(SubjectResponseDto? data, string? errors)> UpdateSubject(string subjectCode, AddSubjectDto addSubject)
        {
            var subject = await _dbContext.subjects.FirstOrDefaultAsync(sb => sb.SubjectCode == subjectCode);

            if (subject is null)
            {
                return (null, "Subject not found");
            }

            subject.SubjectName = addSubject.SubjectName;
            subject.SubjectCode = addSubject.SubjectCode;

            await _dbContext.SaveChangesAsync();
            var response = toSubjectResponse(subject);

            return (response, null);
        }

        private SubjectResponseDto toSubjectResponse(Subject subject)
        {
            return new SubjectResponseDto(subject.SubjectName,subject.SubjectCode);
        }
    }
}
