namespace school.Models.DTos
{
    public record UserResponseDto(
        string Fullname,string Email,string PhoneNumber,DateOnly DOB,DateOnly CreatedDate,List<RoleResponseDto> Roles

        );
}
