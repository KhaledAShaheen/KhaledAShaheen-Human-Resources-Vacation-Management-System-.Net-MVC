using HRM.BL.Interface;
using HRM.DAL.EF;
using HRM.Mapping;
using HRM.Models;
using HRM.PasswordHashing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace HRM.BL.Managers
{
    public class UserSqlManager : IUserManager
    {
        private readonly HrmContext _hrmContext;
        private readonly ILogger<UserSqlManager> _logger;
        private UserEntityMapper _userEntityMapper;
        private PasswordHash _hashPassword;
        public UserSqlManager(HrmContext hrmContext, ILogger<UserSqlManager> logger)
        {
            _hrmContext = hrmContext;
            _userEntityMapper = new UserEntityMapper();
            _logger = logger;
        }
        public Response<UserDto> GetUserByEmail(string email)
        {
            var user = _hrmContext.Users.FirstOrDefault(x => x.Email == email);
            var userEntity = _hrmContext.Users.FirstOrDefault(u => u.ID == user.ManagerID && u.Type == (int)UserType.manager);
            if (user != null) return new Response<UserDto>(_userEntityMapper.Map(user, userEntity));
            return new Response<UserDto>(ErrorCodes.UserNotFound, "User not found");
        }
        public async Task<Response<UserDto>> Create(Guid userType,CreateUserDto user)
        {
            try
            {
                var userEmail=_hrmContext.Users.FirstOrDefault(x => x.Email == user.Email);
                if (userEmail == null)
                {

                    string hashedPassword = PasswordHash.HashText(user.Password, "khaled", new SHA1CryptoServiceProvider());
                    var userToCreate = new UserEntity() { Name = user.Name, Type = (int)user.Type, MobileNumber = user.MobileNumber, Email = user.Email, Password = hashedPassword, JobTitle = user.JobTitle, ManagerID = userType, CreationDate = DateTime.Now };
                    _hrmContext.Users.Add(userToCreate);
                    await _hrmContext.SaveChangesAsync();
                    //find manager and add it
                    var userEntity = _hrmContext.Users.FirstOrDefault(u => u.ID == userToCreate.ManagerID && u.Type == (int)UserType.manager);
                    return new Response<UserDto>(_userEntityMapper.Map(userToCreate, userEntity));
                }
                return new Response<UserDto>(ErrorCodes.UserAlreadyFound, "User Exits"); 
            }
            catch (Exception ex)
            {
                _logger.LogCritical("UserManager - Create ", ex);
                return new Response<UserDto>(ErrorCodes.Unexpected,"Unexpected Error");
            }
        }
        public Response<UserDto> GetByID(Guid id)
        {
            try
            {
                var user = _hrmContext.Users.FirstOrDefault(x => x.ID == id);
                if(user==null) return new Response<UserDto>(ErrorCodes.UserNotFound, "No user found with such ID");
                var userEntity = _hrmContext.Users.FirstOrDefault(u => u.ID == user.ManagerID && u.Type == (int)UserType.manager);
                return new Response<UserDto>(_userEntityMapper.Map(user, userEntity));
            }
            catch (Exception ex)
            {
                _logger.LogCritical("UserManager - GetByID ", ex);
                return new Response<UserDto>(ErrorCodes.Unexpected, "Unexpected Error");
            }
        }
        public Response<List<UserDto>> GetUsersList(UserPaging page)
        {
            List<UserDto> _users = new List<UserDto>();
            try
            {
                var choosenUser = _hrmContext.Users.Skip((page.Page - 1) * page.ItemsPerPage).Where(x => x.ManagerID == page.ManagerId  && x.ID != page.ManagerId).Take(page.ItemsPerPage).ToList();
                foreach (var i in choosenUser)
                {
                    var userEntity = _hrmContext.Users.FirstOrDefault(u => u.ID == i.ManagerID && u.Type == (int)UserType.manager);
                    _users.Add(_userEntityMapper.Map(i, userEntity));
                }
                return new Response<List<UserDto>>(_users);
                
            }
            catch (Exception ex)
            {
                _logger.LogCritical("UserManager - GetUsersList ", ex);
                return new Response<List<UserDto>>(ErrorCodes.Unexpected, "Unexpected Error");
            }
        }
        public async Task<Response<UserDto>> Update(UserDto user)
        {
            try
            {
                if (!IsUserAval(user.ID))
                {
                    return new Response<UserDto>(ErrorCodes.UserNotFound, "User Not Found ");
                }
                var userToUpdate = _hrmContext.Users.FirstOrDefault(x => x.ID == user.ID);
                if (!string.IsNullOrEmpty(user.Name)) userToUpdate.Name = user.Name;
                if (!string.IsNullOrEmpty(user.MobileNumber)) userToUpdate.MobileNumber = user.MobileNumber;
                if (!string.IsNullOrEmpty(user.Email)) userToUpdate.Email = user.Email;
                if (!string.IsNullOrEmpty(user.JobTitle)) userToUpdate.JobTitle = user.JobTitle;
                if (!string.IsNullOrEmpty(user.Name)) userToUpdate.Name = user.Name;
                await _hrmContext.SaveChangesAsync();
                var userEntity = _hrmContext.Users.FirstOrDefault(u => u.ID == userToUpdate.ManagerID && u.Type == (int)UserType.manager);
                return new Response<UserDto>(_userEntityMapper.Map(userToUpdate,userEntity));
            }
            catch (Exception ex)
            {
                _logger.LogCritical("UserManager - Update ", ex);
                return new Response<UserDto>(ErrorCodes.Unexpected, "Unexpected Error");
            }
        }
        public async Task<Response<bool>> Delete(Guid id)
        {
            try
            {
                var userToDelete = _hrmContext.Users.FirstOrDefault(x => x.ID == id);
                if (userToDelete != null)
                {
                    _hrmContext.Remove(userToDelete);
                    await _hrmContext.SaveChangesAsync();
                    return new Response<bool>(true);
                }
                return new Response<bool>(false);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("UserManager - Delete ", ex);
                return new Response<bool>(ErrorCodes.Unexpected, "Unexpected Error");
            }
        }
        private bool IsUserAval(Guid? id)
        {
            var user = _hrmContext.Users.FirstOrDefault(x => x.ID == id);
            if (user == null) return false;
            return true;
        }
        public async Task<Response<bool>> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            try
            {
                var isUserFound = _hrmContext.Users.FirstOrDefault(x => x.Email == resetPasswordDto.UserName);
                if(isUserFound != null)
                {
                    string hashedNewPassword = PasswordHash.HashText(resetPasswordDto.NewPassword, "khaled", new SHA1CryptoServiceProvider());
                    string hashedOldPassword = PasswordHash.HashText(resetPasswordDto.OldPassword, "khaled", new SHA1CryptoServiceProvider());
                    var user = _hrmContext.Users.FirstOrDefault(x => x.Email == resetPasswordDto.UserName);
                    if (user.Password == hashedOldPassword)
                    {
                        user.Password = hashedNewPassword;
                        await _hrmContext.SaveChangesAsync();
                        return new Response<bool>(true);
                    }
                    return new Response<bool>(ErrorCodes.WrongPassOrUser, "Wrong Password Or Username");
                }
                return new Response<bool>(ErrorCodes.UserNotFound, "User is not found in our record");
            }
            catch (Exception ex)
            {
                _logger.LogCritical("UserManager - ResetPassword ", ex);
                return new Response<bool>(ErrorCodes.Unexpected, "Unexpected Error");
            }
        }
        public Response<UserDto> Login(LoginDto loginDto)
        {
            try
            {
                string hashedPassword = PasswordHash.HashText(loginDto.Password, "khaled", new SHA1CryptoServiceProvider());
                var user = _hrmContext.Users.FirstOrDefault(x => x.Email == loginDto.UserName);
                var userManager = _hrmContext.Users.FirstOrDefault(u => u.ID == user.ManagerID && u.Type == (int)UserType.manager);
                if (user!=null && user.Password==hashedPassword) return new Response<UserDto>(_userEntityMapper.Map(user, userManager));
                return new Response<UserDto>(ErrorCodes.UserNotFound,"User not found");
            }
            catch(Exception ex)
            {
                _logger.LogCritical("UserManager - Login ", ex);
                return new Response<UserDto>(ErrorCodes.Unexpected, "Unexpected Error");
            }
        }     
    }
}