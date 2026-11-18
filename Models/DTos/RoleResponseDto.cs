using school.Models.Enums;

namespace school.Models.DTos
{
    public record RoleResponseDto(Guid Id, RoleEnum Name,string Description,DateOnly CreatedDate);
    
    
}
