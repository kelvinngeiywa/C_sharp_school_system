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

        public async Task<(TeacherSubjectResponseDto? data, string? error)> AssignTeacherSubject(Guid teacherId, AssignSubjectDto assignSubject)
        {
            if (assignSubject.SubjectIds is null || assignSubject.SubjectIds.Count ==0 )
            {
                return (null, "At least one subject Id is required!!");
            }
            var teacher = await _dbContext.teachers.Include(ts => ts.Subjects).FirstOrDefaultAsync(tr=>tr.Id == teacherId);

            if(teacher is null)
            {
                return (null,"Teacher not found");
            }

            var requestIds = assignSubject.SubjectIds.Distinct().ToList();

            var subjects = await _dbContext.subjects.Where(s=>requestIds.Contains(s.Id)).ToListAsync();

            var missingIds = requestIds.Except(subjects.Select(s=>s.Id)).ToList();

            if(missingIds.Count > 0)
            {
                return (null, $"Subject(s) not found: {string.Join(", ", missingIds)}");
            }

            var newSubjects = subjects.Where(s => !teacher.Subjects.Any(ts => ts.Id == s.Id)).ToList();

            if(newSubjects.Count == 0)
            {
                return (null, "Teacher already has all of the selected subjects");
            }

            foreach (var subject in newSubjects)
            {
                teacher.Subjects.Add(subject);
            }
            await _dbContext.SaveChangesAsync();
            var response = new TeacherSubjectResponseDto(teacher.Name, teacher.Email, teacher.PhoneNumber, teacher.Subjects.Select(
                sb => sb.SubjectName).ToList());
            return (response,null);

        }

        //public async Task<TeacherSubjectResponseDto> AssignTeacherSubject(Guid teacherId, AssignSubjectDto assignSubject)
        //{
        //    var teacher = await _dbContext.teachers.Include(ts=>ts.Subjects).FirstOrDefaultAsync(tr=>tr.Id == teacherId);

        //    if(teacher is null)
        //    {
        //        return null;
        //    }

        //    var subject = await _dbContext.subjects.FindAsync(assignSubject.SubjectId);
        //    if(subject is null)
        //    {
        //        return null;
        //    }

        //    teacher.Subjects.Add(subject);
        //    await _dbContext.SaveChangesAsync();

        //    var response = new TeacherSubjectResponseDto(teacher.Name,teacher.Email,teacher.PhoneNumber,teacher.Subjects.Select(
        //        sb=>sb.SubjectName).ToList());

        //    return response;



        //}

        public async Task<List<TeacherResponseDto>> GetAllTeachersAsync()
        {
            return await _dbContext.teachers.Select(tr=>new TeacherResponseDto(tr.Name,tr.Email,tr.PhoneNumber,tr.DOB,tr.Residence)).ToListAsync();
        }

        public async Task<(TeacherSubjectResponseDto? response, string? error)> RemoveSubject(Guid teacherId, Guid subjectId)
        {
            var teacher= await _dbContext.teachers.Include(ts => ts.Subjects).FirstOrDefaultAsync(tr => tr.Id == teacherId);

            if (teacher is null)
            {
                return (null,"Teacher not found!!");
            }

            var subject= teacher.Subjects.FirstOrDefault(sb=>sb.Id==subjectId);

            if (subject is null)
            {
                return (null,"Subject not found or assigned to this teacher");
            }

            teacher.Subjects.Remove(subject);
            await _dbContext.SaveChangesAsync();
            var response = new TeacherSubjectResponseDto(teacher.Name, teacher.Email, teacher.PhoneNumber, teacher.Subjects.Select(
                sb => sb.SubjectName).ToList());

            return (response,null);

        }

        private TeacherResponseDto toTeacherResponse(Teacher teacher)
        {
            return new TeacherResponseDto(teacher.Name,teacher.Email,teacher.PhoneNumber,teacher.DOB,teacher.Residence);
        }
    }

}
