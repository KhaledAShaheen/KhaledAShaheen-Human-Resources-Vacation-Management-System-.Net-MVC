using HRM.BL.Interface;
using HRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MvcApplication.HowTo.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly ILogger<UserController> _logger;
        private IConfiguration _configuration;
        public UserController(IUserManager userManager,ILogger<UserController> logger, IConfiguration configuration, IHttpContextAccessor context)
        {
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
        }
        [HttpPost]
        [AuthorizeEnum(UserType.manager)]
        public async Task<IActionResult> Create(CreateUserDto createUserDto)
        {
            try 
            {
                var managerID=Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var newUser = await _userManager.Create(managerID, createUserDto);
                if (newUser.ErrorCode == ErrorCodes.Success) return Ok(new Response<UserDto>(newUser.Data));
                else if (newUser.ErrorCode == ErrorCodes.UserAlreadyFound) return BadRequest(new Response<UserDto>(ErrorCodes.UserAlreadyFound, "User Already Found in Our Record"));
                else return BadRequest(new Response<UserDto>(ErrorCodes.Unexpected, "Unexpected Error Occured"));
            }
            catch (Exception ex)
            {
                _logger.LogCritical("UserController - Create ",ex);
                return BadRequest(new Response<UserDto>(ErrorCodes.Unexpected,"Un expected error in create - userControler"));
            }
        }
        [HttpGet]
        // [Authorize(nameof(UserType.manager)]
        [AuthorizeEnum(UserType.manager)]
        public IActionResult Get(Guid id)
        {
            try
            {
                Response<UserDto> user = _userManager.GetByID(id);
                if (user.ErrorCode == 0) return Ok(user);
                else return BadRequest(user);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("UserController - Get ", ex);
                return BadRequest(new Response<UserDto>(ErrorCodes.Unexpected, "Un expected error in get - userControler"));
            }
        }

        [HttpPost]
        //[Authorize(Roles=nameof(UserType.manager))]
        [AuthorizeEnum(UserType.manager)]

        public IActionResult GetAll(UserPaging pagging)
        {
            try
            {
                var users = _userManager.GetUsersList(pagging);
                if (users.ErrorCode == 0) return Ok(new Response<List<UserDto>>(users.Data));
                else return BadRequest(new Response<UserDto>(ErrorCodes.Unexpected, "Un expected error in getAll - userControler"));

            }
            catch (Exception ex)
            {
                _logger.LogCritical("UserController - GetAll",ex);
                return BadRequest(new Response<UserDto>(ErrorCodes.Unexpected, "Un expected error in getAll - userControler"));
            }
        }
        [HttpGet]
        [AuthorizeEnum(UserType.manager)]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                Response<bool> isDeleted = await _userManager.Delete(id);
                if (isDeleted.ErrorCode == 0) return Ok(isDeleted);
                else return BadRequest(isDeleted);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("UserController - Remove", ex);
                return BadRequest(new Response<UserDto>(ErrorCodes.Unexpected, "Un expected error in remove - userControler"));
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var user = _userManager.Login(loginDto);
                if (user.Data!=null)
                {
                    string token = Generate(user.Data);
                        //HttpContext.Session.SetString($"{user.ID}_user",JsonConvert.SerializeObject(user));
                        //HttpContext.Session.SetString($"{user.ID}_token",token);
                    return Ok(new LoginResponse(user.Data,user.ErrorCode,user.Description,token));
                }
                return BadRequest(new LoginResponse(user.Data,ErrorCodes.UserNotFound, "User not found",null));
            }
            catch (Exception ex)
            {
                _logger.LogCritical("UserController - Login", ex);
                return BadRequest(new LoginResponse(null,ErrorCodes.Unexpected, "Unexpected error in login - userControler", null));
            }
        }
        
        private string Generate(UserDto userDto)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userDto.ID.ToString()),
                new Claim(ClaimTypes.Name, userDto.Name),
                new Claim(ClaimTypes.Role, userDto.Type.ToString()),
                new Claim(ClaimTypes.MobilePhone, userDto.MobileNumber),
                new Claim(ClaimTypes.Email, userDto.Email),
                new Claim(ClaimTypes.UserData, userDto.JobTitle),
                new Claim(ClaimTypes.Gender,userDto.ManagerId.ToString())
            };


        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Audience"],
              claims,
              expires: DateTime.UtcNow.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [AllowAnonymous]
        [HttpPut]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            try
            {
                Response<bool> resetPass = await _userManager.ResetPassword(resetPasswordDto);
                if (resetPass.ErrorCode == 0) return Ok(resetPass);
                else return BadRequest(resetPass);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("UserController - ResetPassword", ex);
                return BadRequest(new Response<bool>(ErrorCodes.Unexpected, "Un expected error in ResetPassword - userControler"));
            }
        }
        [HttpPut]
        [AuthorizeEnum(UserType.manager)]
        public async Task<IActionResult> Update(UserDto user)
        {
            try
            {
                Response<UserDto> updatedUser =await _userManager.Update(user);
                if (updatedUser.ErrorCode == 0) return Ok(updatedUser);
                else return BadRequest(updatedUser);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("UserController - Update", ex);
                return BadRequest(new Response<UserDto>(ErrorCodes.Unexpected, "Un expected error in update - userControler"));
            }
        }

    }
}
