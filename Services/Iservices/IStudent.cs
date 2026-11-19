using school.Models.DTos;

namespace school.Services.Iservices
{
    public interface IStudent
    {
        Task<(StudentResponseDTo? data,string? errors)> AddStudentAsync(AddStudentDTO addStudent);
        Task<List<StudentResponseDTo>> GetAllStudents();
        Task<(StudentResponseDTo? data, string? errors)> GetStudentByRegNumber(string regNumber);
        Task<(StudentGuardianResponseDTO? data, string? error)> GetStudentGuardinDetails(string regNumber);
    }
}
