using school.Models.Enums;

namespace school.Models.DTos
{
    public record StudentGuardianResponseDTO(
        string Fullname, string RegNumber, int Grade, StreamEnum Stream, DateOnly DOB,List<GuardianResponseDto> Guardian

        );
    
}
