﻿using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;
using CleanArchitecture.Domain.Entities_SubModel.User;
using CleanArchitecture.Domain.Entities_SubModel.User.SubModel;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using PdfSharpCore.Pdf.Filters;
using System.Text;
using NuGet.Packaging.Signing;

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
        public int GetNumberOfUsers(UserFilter filter)
        {
            IQueryable<User> users = _contractContext.Users.AsQueryable();

            if (filter != null)
            {
                if (filter.ParkingLotId.HasValue)
                {
                    users = users.Where(u => u.ParkingLotId == filter.ParkingLotId);
                }
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
                if (filter.IsDeleted.HasValue)
                {
                    users = users.Where(u => u.IsDeleted == filter.IsDeleted.Value);
                }

            }
            return users.Count();
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
                if (filter.ParkingLotId.HasValue)
                {
                    users = users.Where(u => u.ParkingLotId == filter.ParkingLotId);
                }
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
                if (filter.IsDeleted.HasValue)
                {
                    users = users.Where(u => u.IsDeleted == filter.IsDeleted.Value);
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
            user.ParkingLotId = userCreate.ParkingLotId;
            user.Name = userCreate.Name;
            user.PhoneNumber = userCreate.PhoneNumber;
            user.Job = userCreate.Job;
            user.CurrentAddress = userCreate.CurrentAddress;
            user.Email = userCreate.Email;

            CreatePasswordHash(userCreate.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.CardImage = userCreate.CardImage;
            user.Role = userCreate.Role;
            user.CitizenIdentificationInfoNumber = userCreate.CitizenIdentificationInfoNumber;
            user.CitizenIdentificationInfoAddress = userCreate.CitizenIdentificationInfoAddress;
            user.CitizenIdentificationInfoDateReceive = userCreate.CitizenIdentificationInfoDateReceive;
            user.PassportInfoNumber = userCreate.PassportInfoNumber;
            user.PassportInfoAddress = userCreate.PassportInfoAddress;
            user.PassportInfoDateReceive = userCreate.PassportInfoDateReceive;
            user.CreatedDate = userCreate.CreatedDate;
            user.IsDeleted = false;

            _contractContext.Users.Add(user);
            return Save();
        }
        public bool UpdateUser(int id, UserUpdateModel request, out string errorMessage)
        {
            var user = _contractContext.Users.Find(id);
            errorMessage = string.Empty;
            if (request.Email == null)
            {
                errorMessage = "Email don't allow null";
                return false;
            }

            if (_contractContext.Users.Any(u => u.Email == request.Email) && request.Email != user.Email)
            {
                errorMessage = "User with this email already exists";
                return false;
            }
            if (request.CitizenIdentificationInfoNumber != null && request.CitizenIdentificationInfoNumber != user.CitizenIdentificationInfoNumber)
            {
                if (_contractContext.Users.Any(u => u.CitizenIdentificationInfoNumber == request.CitizenIdentificationInfoNumber))
                {
                    errorMessage = "User with this citizen identification number already exists";
                    return false;
                }
            }
            if (request.PassportInfoNumber != null && request.PassportInfoNumber != user.PassportInfoNumber)
            {
                if (_contractContext.Users.Any(u => u.PassportInfoNumber == request.PassportInfoNumber))
                {
                    errorMessage = "User with this passport number already exists";
                    return false;
                }
            }

            user.Name = request.Name;
            user.ParkingLotId = request.ParkingLotId;
            user.PhoneNumber = request.PhoneNumber;
            user.Job = request.Job;
            user.CurrentAddress = request.CurrentAddress;
            user.Email = request.Email;
            user.CardImage = request.CardImage;
            user.Role = request.Role;
            user.CitizenIdentificationInfoNumber = request.CitizenIdentificationInfoNumber;
            user.CitizenIdentificationInfoAddress = request.CitizenIdentificationInfoAddress;
            user.CitizenIdentificationInfoDateReceive = request.CitizenIdentificationInfoDateReceive;
            user.PassportInfoNumber = request.PassportInfoNumber;
            user.PassportInfoAddress = request.PassportInfoAddress;
            user.PassportInfoDateReceive = request.PassportInfoDateReceive;
            user.IsDeleted = request.isDeleted;
            user.CardImage = request.CardImage;

            _contractContext.Users.Update(user);
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

        public bool ChangePassword(UpdateUserPasswordModel request, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (!EmailExit(request.UserName))
            {
                errorMessage = "User not found";
                return false;
            }
            var user = GetUserByEmail(request.UserName);
            if (!VerifyPasswordHash(request.OldPassword, user.PasswordHash, user.PasswordSalt))
            {
                errorMessage = "Password incorect";
                return false;
            }
            if (request.NewPassword != request.ConfirmPassword)
            {
                errorMessage = "Confirm password and new password don't match";
                return false;
            }
            CreatePasswordHash(request.NewPassword, out byte[] newPasswordHash, out byte[] newPasswordSalt);
            user.PasswordHash = newPasswordHash;
            user.PasswordSalt = newPasswordSalt;
            _contractContext.Update(user);
            return Save();
        }

        public bool EmailExit(string email)
        {
            return _contractContext.Users.Any(u => u.Email == email);
        }

        public User GetUserByEmail(string email)
        {
            return _contractContext.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        public bool DeleteUser(int id)
        {
            var user = _contractContext.Users.Where(u => u.Id == id).FirstOrDefault();
            user.IsDeleted = true;
            _contractContext.Users.Update(user);
            return Save();
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

        public void EncodeId(int? id, out byte[]? idHash, out byte[]? idSalt, out long timestamp)
        {
            timestamp = DateTime.UtcNow.Ticks;
            using (var hmac = new HMACSHA512())
            {
                idSalt = hmac.Key;
                idHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(id.ToString()));
            }
        }
        public bool DecodeId(int expectedId, byte[] hash, byte[] salt, long timestamp)
        {
            var timeout = TimeSpan.FromDays(1);
            var currentTimestamp = DateTime.UtcNow.Ticks;
            if (currentTimestamp - timestamp > timeout.Ticks)
            {
                // The timestamp has expired, return false
                return false;
            }
            using (var hmac = new HMACSHA512(salt))
            {
                var expectedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(expectedId.ToString()));
                if (!hash.SequenceEqual(expectedHash))
                {
                    // The hash does not match the expected hash, return false
                    return false;
                }
            }
            // The hash and timestamp are valid, return true
            return true;
        }


        //public bool UpdateUserRole(int id, string role)
        //{
        //    throw new NotImplementedException();
        //}

        //public byte[] EncodeId(int id)
        //{
        //    byte[] salt = new byte[] { 0x01, 0x02, 0x03, 0x04 };

        //    var data = $"{id}";

        //    using (var hmac = new HMACSHA512(salt))
        //    {
        //        return hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
        //    }
        //}
        //public bool TryDecodeId(byte[] encoded,/* byte[] salt,*/ out int id)
        //{
        //    byte[] salt = new byte[] { 0x01, 0x02, 0x03, 0x04 };
        //    using (var hmac = new HMACSHA512(salt))
        //    { 
        //            // The encoded value matches the expected hash, so decoding is successful
        //            id = int.Parse(Encoding.UTF8.GetString(encoded));
        //            return true;
        //    }
        //}
        //private static bool ConstantTimeEquals(byte[] a, byte[] b)
        //{
        //    if (a == null || b == null || a.Length != b.Length)
        //    {
        //        return false;
        //    }

        //    var result = 0;
        //    for (var i = 0; i < a.Length; i++)
        //    {
        //        result |= a[i] ^ b[i];
        //    }

        //    return result == 0;
        //}
    }
}
