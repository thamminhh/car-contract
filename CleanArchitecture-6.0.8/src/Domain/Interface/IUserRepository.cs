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

        public bool DeleteUser(int id);

        bool CreateUser(CreateUserModel user, out string errorMessage);
        bool UpdateUser(int id, UserUpdateModel user, out string errorMessage);
        bool Save();
        bool UserExit(int id);
        bool UpdateUserRoleAsync(int id, UpdateUserRoleModel model);

        public int GetNumberOfUsers(UserFilter filter);

        public string CreateToken(string email);

        bool Login(LoginModel request, out string errorMessage);

        bool ChangePassword(UpdateUserPasswordModel request, out string errorMessage);

        bool EmailExit(string email);

        public void EncodeId(int? id, out byte[]? idHash, out byte[]? idSalt, out long timestamp);

        public bool DecodeId(int expectedId, byte[] hash, byte[] salt, long timestamp);



    }
}
