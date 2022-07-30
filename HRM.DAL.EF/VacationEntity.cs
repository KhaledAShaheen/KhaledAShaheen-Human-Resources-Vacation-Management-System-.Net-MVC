using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DAL.EF
{
    public class VacationEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid ID { get; set; }
        public int Type { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? StartingDate { get; set; }
        public int? Duration { get; set; }
        public int? Status { get; set; }
        public string? Note { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual UserEntity User { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
