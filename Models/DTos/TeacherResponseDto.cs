namespace school.Models.DTos
{
    public record TeacherResponseDto(string Name, string Email, string PhoneNumber, DateOnly DOB, string Residence);
}
