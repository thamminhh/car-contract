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

        public bool DeleteUser(int id);

        bool CreateUser(CreateUserModel user, out string errorMessage);
        bool UpdateUser(int id, UserUpdateModel user);
        bool Save();
        bool UserExit(int id);
        bool UpdateUserRoleAsync(int id, UpdateUserRoleModel model);

        public int GetNumberOfUsers(UserFilter filter);

        public string CreateToken(string email);

        bool Login(LoginModel request, out string errorMessage);

        bool ChangePassword(UpdateUserPasswordModel request, out string errorMessage);

        bool EmailExit(string email);

    }
}
