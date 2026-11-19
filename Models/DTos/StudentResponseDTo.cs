using school.Models.Enums;

namespace school.Models.DTos
{
    public record StudentResponseDTo(string Fullname, string RegNumber,int Grade, StreamEnum Stream,DateOnly DOB, int Age,
        string Residence);
    
    
}
