using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using CleanArchitecture.Domain.Entities_SubModel.User;
using CleanArchitecture.Domain.Entities_SubModel.User.SubModel;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Application.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ContractContext _contractContext;
        private readonly IConfiguration _configuration;

        public UserRepository(ContractContext contractContext, IConfiguration configuration)
        {
            _contractContext = contractContext;
            _configuration = configuration;
        }

        private static string NormalizeEmail(string email)
        {
            return email.Trim().ToUpper();
        }

        public string CreateToken(string email)
        {
            var user = GetUserByEmail(email);
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public void CreatePasswordHash(string? password, out byte[]? passwordHash, out byte[]? passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string? password, byte[]? passwordHash, byte[]? passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public int GetNumberOfUsers()
        {
            return _contractContext.Users.Count();
        }

        public User GetUserById(int id)
        {
            return _contractContext.Users.Where(u => u.Id == id).FirstOrDefault();
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
            if (userCreate.PassportInfoNumber != null)
            {
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

            CreatePasswordHash(userCreate.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

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

        public bool Login(LoginModel request, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (!EmailExit(request.UserName))
            {
                errorMessage = "User not found";
                return false;
            }
            var user = GetUserByEmail(request.UserName);
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                errorMessage = "Password incorect";
                return false;
            }

            return true;
        }

        public bool EmailExit(string email)
        {
            return _contractContext.Users.Any(u => u.Email == email);
        }

        public User GetUserByEmail(string email)
        {
            return _contractContext.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        public int GetUserIdByEmail(string email)
        {
            var user = _contractContext.Users.Where(u => u.Email == email).FirstOrDefault();
            return user.Id;
        }

        //public bool UpdateUserRole(int id, string role)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
