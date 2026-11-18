using school.Models.DTos;

namespace school.Services.Iservices
{
    public interface ITeacher
    {
        Task<TeacherResponseDto> AddTeacherAsync(AddTeacherDto addTeacherDto);
        Task<List<TeacherResponseDto>> GetAllTeachersAsync();
    }
}
