using Microsoft.EntityFrameworkCore;
using school.Data;
using school.Models;
using school.Models.DTos;
using school.Services.Iservices;

namespace school.Services
{
    public class StudentService(AppDbContext _dbContext) : IStudent
    {
        public async Task<(StudentResponseDTo? data, string? errors)> AddStudentAsync(AddStudentDTO addStudent)
        {
            var studentExists = await _dbContext.students.AnyAsync(st => st.RegNumber == addStudent.RegNumber);
            if (studentExists)
            {
                return (null, $"Student with Reg Number {addStudent.RegNumber}");
            }

            var guardianExists = await _dbContext.guardians.FindAsync(addStudent.GuardianId);

            if (guardianExists is null)
            {
                return (null, "Guardian not found");
            }


            var newStudent = new Student
            {
                Fullname=addStudent.Fullname,
                RegNumber=addStudent.RegNumber,
                Grade=addStudent.Grade,
                Residence=addStudent.Residence

            };
            newStudent.DOB= addStudent.DOB;
            var TodayDate = DateOnly.FromDateTime(DateTime.UtcNow);

            newStudent.Age =TodayDate.Year - addStudent.DOB.Year;

            var studentGuardian = new StudentGuardian
            {
                GuardianId=guardianExists.Id,
                StudentRegNumber=newStudent.RegNumber,
                Relationship="Guardian",
                IsPrimaryContact=true

            };
            newStudent.StudentGuardian.Add(studentGuardian);

            _dbContext.students.Add(newStudent);
            await _dbContext.SaveChangesAsync();

            var response = new StudentResponseDTo(newStudent.Fullname,newStudent.RegNumber,newStudent.Grade,newStudent.Stream,
                newStudent.DOB,newStudent.Age,newStudent.Residence);

            return (response, null);



        }

        public async Task<List<StudentResponseDTo>> GetAllStudents()
        {
            return await _dbContext.students.Select(st=> new StudentResponseDTo(st.Fullname,st.RegNumber,st.Grade,
                st.Stream,st.DOB,st.Age,st.Residence)).ToListAsync();
        }

        public async Task<(StudentResponseDTo? data, string? errors)> GetStudentByRegNumber(string regNumber)
        {
            var student = await _dbContext.students.FindAsync(regNumber);

            if( student is null)
            {
                return (null, $"Student with Reg number {regNumber} not found");
            }
            var response = new StudentResponseDTo(student.Fullname,student.RegNumber,student.Grade,student.Stream,student.DOB,student.Age,
                student.Residence);

            return (response, null);
        }
    }
}
