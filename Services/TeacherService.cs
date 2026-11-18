using Microsoft.EntityFrameworkCore;
using school.Data;
using school.Models;
using school.Models.DTos;
using school.Services.Iservices;

namespace school.Services
{
    public class TeacherService(AppDbContext _dbContext) : ITeacher
    {
        public async Task<TeacherResponseDto> AddTeacherAsync(AddTeacherDto addTeacherDto)
        {
            var teacherExists = await _dbContext.teachers.AnyAsync(tr=>tr.Email == addTeacherDto.Email || tr.PhoneNumber == addTeacherDto.PhoneNumber);

            if (teacherExists)
            {
                return null;
            }

            var newTeacher = new Teacher
            {
                Name=addTeacherDto.Name,
                Email=addTeacherDto.Email,
                PhoneNumber=addTeacherDto.PhoneNumber,
                DOB=addTeacherDto.DOB,
                Residence= addTeacherDto.Residence
            };

            _dbContext.teachers.Add(newTeacher);
            await _dbContext.SaveChangesAsync();
            return toTeacherResponse(newTeacher);
        }

        public async Task<List<TeacherResponseDto>> GetAllTeachersAsync()
        {
            return await _dbContext.teachers.Select(tr=>new TeacherResponseDto(tr.Name,tr.Email,tr.PhoneNumber,tr.DOB,tr.Residence)).ToListAsync();
        }

        private TeacherResponseDto toTeacherResponse(Teacher teacher)
        {
            return new TeacherResponseDto(teacher.Name,teacher.Email,teacher.PhoneNumber,teacher.DOB,teacher.Residence);
        }
    }

}
