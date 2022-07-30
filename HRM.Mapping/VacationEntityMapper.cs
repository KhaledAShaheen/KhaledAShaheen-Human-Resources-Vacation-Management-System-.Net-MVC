using HRM.DAL.EF;
using HRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Mapping
{
    public class VacationEntityMapper
    {
        public VacationDto Map(VacationEntity user)
        {
            var temp = new VacationDto
            {
                ID = user.ID,
                Type = (VacationTypes)user.Type,
                StartingDate=user.StartingDate,
                Duration=user.Duration,
                Status=(VacationStatus)user.Status,
                Note=user.Note,
                UserId=user.UserId,
            };
            return temp;
        }
    }
}
