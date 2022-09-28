using Employee.Models.Entities;

namespace Employee.ControllerWebApi.Jwt
{
    public interface ITokenManager
    {
        string GenerateToken(User user, string roleName);
    }
}
