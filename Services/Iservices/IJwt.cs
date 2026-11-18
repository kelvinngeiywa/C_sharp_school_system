using school.Models;

namespace school.Services.Iservices
{
    public interface IJwt
    {
        string CreateToken(User user);
    }
}
