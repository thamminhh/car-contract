using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using CleanArchitecture.Domain.Entities_SubModel.User;
using CleanArchitecture.Domain.Entities_SubModel.User.SubModel;

namespace CleanArchitecture.Application.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ContractContext _contractContext;

        public UserRepository(ContractContext contractContext)
        {
            _contractContext = contractContext;
        }

        private static string NormalizeEmail(string email)
        {
            return email.Trim().ToUpper();
        }

        public int GetNumberOfUsers()
        {
            return _contractContext.Users.Count();
        }

        public User GetUserById(int id)
        {
            return _contractContext.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public User GetUserByName(string name)
        {
            throw new NotImplementedException();
        }

        public ICollection<User> GetUsers(int page, int pageSize, UserFilter filter)
        {
            if (page < 1)
            {
                page = 1;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }

            int skip = (page - 1) * pageSize;

            IQueryable<User> users = _contractContext.Users.AsQueryable();

            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.Name))
                {
                    users = users.Where(u => u.Name.Contains(filter.Name));
                }

                if (!string.IsNullOrWhiteSpace(filter.Email))
                {
                    users = users.Where(u => u.Email.Contains(filter.Email));
                }

                if (!string.IsNullOrWhiteSpace(filter.PhoneNumber))
                {
                    users = users.Where(u => u.PhoneNumber.Contains(filter.PhoneNumber));
                }
            }

            return users.OrderBy(u => u.Id).Skip(skip).Take(pageSize).ToList();
        }
        public bool UserExit(int id)
        {
            return _contractContext.Users.Any(u => u.Id == id);
        }

        public bool Save()
        {
            var saved = _contractContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool CreateUser(CreateUserModel userCreate, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (userCreate.Email == null)
            {
                errorMessage = "Email don't allow null";
                return false;
            }

            if (_contractContext.Users.Any(u => u.Email == userCreate.Email))
            {
                errorMessage = "User with this email already exists";
                return false;
            }
            if (userCreate.CitizenIdentificationInfoNumber != null)
            {
                if (_contractContext.Users.Any(u => u.CitizenIdentificationInfoNumber == userCreate.CitizenIdentificationInfoNumber))
                {
                    errorMessage = "User with this citizen identification number already exists";
                    return false;
                }
            }
            if (userCreate.PassportInfoNumber != null) { 
                if (_contractContext.Users.Any(u => u.PassportInfoNumber == userCreate.PassportInfoNumber))
                {
                    errorMessage = "User with this passport number already exists";
                    return false;
                }
        }

            var user = new User();
            user.Name = userCreate.Name;
            user.PhoneNumber = userCreate.PhoneNumber;
            user.Job = userCreate.Job;
            user.CurrentAddress = userCreate.CurrentAddress;
            user.Email = userCreate.Email;
            user.Password = userCreate.Password;
            user.Role = userCreate.Role;
            user.CitizenIdentificationInfoNumber = userCreate.CitizenIdentificationInfoNumber;
            user.CitizenIdentificationInfoAddress = userCreate.CitizenIdentificationInfoAddress;
            user.CitizenIdentificationInfoDateReceive = userCreate.CitizenIdentificationInfoDateReceive;
            user.PassportInfoNumber = userCreate.PassportInfoNumber;
            user.PassportInfoAddress = userCreate.PassportInfoAddress;
            user.PassportInfoDateReceive = userCreate.PassportInfoDateReceive;
            user.CreatedDate = userCreate.CreatedDate;
            user.IsDeleted = userCreate.IsDeleted;

            _contractContext.Add(user);
            return Save();
        }
        public bool UpdateUser(User user)
        {
            _contractContext.Update(user);
            return Save();
        }
        public bool UpdateUserRoleAsync(int id, UpdateUserRoleModel request)
        {
            var user = _contractContext.Users.Where(u => u.Id == id).FirstOrDefault();
            if (user == null) return false;

            user.Role = request.Role; 
       
            return Save();
        }

        //public bool UpdateUserRole(int id, string role)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
