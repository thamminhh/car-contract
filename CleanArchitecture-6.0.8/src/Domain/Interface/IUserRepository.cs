using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.User;
using CleanArchitecture.Domain.Entities_SubModel.User.SubModel;

namespace CleanArchitecture.Domain.Interface
{
    public interface IUserRepository
    {

        ICollection<User> GetUsers(int page, int pageSize, UserFilter filter);

        User GetUserById(int id);

        User GetUserByEmail(string Email);
        int GetUserIdByEmail(string Email);



        bool CreateUser(CreateUserModel user, out string errorMessage);
        bool UpdateUser(User user);
        bool Save();
        bool UserExit(int id);
        bool UpdateUserRoleAsync(int id, UpdateUserRoleModel model);

        public int GetNumberOfUsers();

        public string CreateToken(string email);

        bool Login(LoginModel request, out string errorMessage);

        bool EmailExit(string email);

    }
}
