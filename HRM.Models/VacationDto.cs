using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models
{
    public class VacationDto
    {
        public Guid ID { get; set; }
        public VacationTypes Type { get; set; }
        public DateTime? StartingDate { get; set; }
        public int? Duration { get; set; }
        public VacationStatus Status { get; set; }
        public string Note { get; set; }
        public Guid UserId { get; set; }
    }
    public class UpdateVacationDto
    {
        public Guid ID { get; set; }
        public VacationTypes Type { get; set; }
        public DateTime? StartingDate { get; set; }
        public int Duration { get; set; }
        public VacationStatus? Status { get; set; }
        public string Note { get; set; }
    }
    public class CreateVacationDto
    {
        public VacationTypes Type { get; set; }
        public DateTime StartingDate { get; set; }
        public int Duration { get; set; }
        public VacationStatus Status { get; set; }

    }
}
