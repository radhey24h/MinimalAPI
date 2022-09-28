using Employee.Models.Entities;

namespace Employee.Dal.Repository.AuthenticationRepository
{
    public class AuthenticationRepository: IAuthenticationRepository
    {
        private readonly EmployeeDbContext _dbContext;
        public AuthenticationRepository(EmployeeDbContext context)
        {
            _dbContext = context;
        }
        public User? CheckCredentials(User user)
        {
            var userCredentials = _dbContext.Users.SingleOrDefault(u => u.Email == user.Email);
            return userCredentials;
        }

        public string GetUserRole(int roleId)
        {
            return _dbContext.Roles.SingleOrDefault(role => role.RoleId == roleId).RoleName;
        }

        public int RegisterUser(User user)
        {
            _dbContext.Users.Add(user);
            return _dbContext.SaveChanges();
        }
    }
}
