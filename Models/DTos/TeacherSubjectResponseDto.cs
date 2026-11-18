namespace school.Models.DTos
{
    public record TeacherSubjectResponseDto(string Name, string Email, string PhoneNumber,List<string> subjects);

}
