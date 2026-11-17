namespace school.Models.DTos
{
    public record CreateUserDto(
        string Fullname,
        string Email,
        string PhoneNumber,
        string Password,
        DateOnly DOB
        );
    
    
}
