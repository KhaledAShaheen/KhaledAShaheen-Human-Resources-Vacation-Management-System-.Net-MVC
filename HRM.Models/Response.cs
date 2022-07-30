
namespace HRM.Models
{
    public class Response<T>
    {
        public Response(ErrorCodes errorCode, string description)
        {
            ErrorCode = errorCode;
            Description = description;
        }
        public Response(T data)
        {
            Data = data;

        }
        public Response()
        {

        }
        public T Data { get; set; }
        public ErrorCodes ErrorCode { get; set; } = 0;
        public string Description { get; set; }
    }
    public class LoginResponse : Response<UserDto>
    {
        public string Token { get; set; }
        public LoginResponse(UserDto user,ErrorCodes errorcode,string description,string token)
        {
            Data = user;
            ErrorCode = errorcode;
            Description = description;
            Token = token;
        }
    }
}


