using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace HRM.Models
{
    public enum ErrorCodes
    {
        Success = 0,
        UserNotFound = 1110,
        WrongPassOrUser=10,
        Unexpected = 44444,
        UserAlreadyFound=1122
    }
}
