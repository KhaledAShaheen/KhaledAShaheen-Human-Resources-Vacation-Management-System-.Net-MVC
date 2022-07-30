using HRM.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRM.BL.Interface
{
    public interface IUserManager
    {
        public Task<Response<UserDto>> Create(Guid userType,CreateUserDto newUser);
        public Response<UserDto> GetUserByEmail(string email);
        public Response<UserDto> GetByID(Guid id);
        public Response<List<UserDto>> GetUsersList(UserPaging page);
        public Task<Response<UserDto>> Update(UserDto user);
        public Task<Response<bool>> Delete(Guid id);
        public Response<UserDto> Login(LoginDto loginDto);
        public Task<Response<bool>> ResetPassword(ResetPasswordDto resetPasswordDto);

    }
}