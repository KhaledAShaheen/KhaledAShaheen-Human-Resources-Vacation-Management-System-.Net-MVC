using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DAL.EF
{
    public class UserEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid ID {get;set;}
        [MaxLength(50)]
        public string Name { get;set;}
        public int Type { get; set; }
        [MaxLength(70)]
        public string MobileNumber { get; set; }
        [MaxLength(100)]
        public string Email { get;set;}
        [MaxLength(100)]
        public string Password { get; set; }
        [MaxLength(70)]
        public string JobTitle { get; set; }
        public Guid? ManagerID { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
