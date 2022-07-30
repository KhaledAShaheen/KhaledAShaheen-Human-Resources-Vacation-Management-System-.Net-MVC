using HRM.Models;
using HRM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Mapping
{
    public class MvcToDto
    {
        public CreateUserDto Map(UserModel userModel)
        {
            var temp = new CreateUserDto
            {
                Name = userModel.Name,
                Type = userModel.Type,
                MobileNumber = userModel.MobileNumber,
                Email = userModel.Email,
                JobTitle = userModel.JobTitle,
                Password = userModel.Password
            };
            return temp;
        }
    }
}
