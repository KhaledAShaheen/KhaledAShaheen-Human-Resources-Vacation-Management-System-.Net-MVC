using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models
{
    public class ManagerDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public Guid? ManagerID { get; set; }
    }
}
