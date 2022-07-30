using HRM.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.BL.Interface
{
    public interface IVacationManager
    {
        public Task<Response<VacationDto>> Create(Guid userId,CreateVacationDto vacation);
        public Response<List<VacationDto>> GetVacationList([FromQuery] Paging @param);
        public Task<Response<VacationDto>> Update(UpdateVacationDto vacation);
        public Task<Response<bool>> Delete(Guid id);
    }
}
