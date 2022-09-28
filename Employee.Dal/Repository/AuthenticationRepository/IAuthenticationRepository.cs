using Employee.Models.Entities;

namespace Employee.Dal.Repository.AuthenticationRepository
{
    public interface IAuthenticationRepository
    {
        int RegisterUser(User user);
        User? CheckCredentials(User user);
        string GetUserRole(int roleId);
    }
}
