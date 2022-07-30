using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models
{
    public enum VacationTypes
    {
        Annual=0,
        Sick=1,
        Leave=2,
        Exceptional=3
    }
    public enum VacationStatus
    {
         Draft=0,
         Submitted=1,
         Approved=2,
         Rejected=3
    }
    public enum UserType
    {
        user=0,
        manager=1,
    }
}
