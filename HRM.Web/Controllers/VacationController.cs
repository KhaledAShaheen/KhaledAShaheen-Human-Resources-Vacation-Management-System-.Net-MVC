﻿using Microsoft.AspNetCore.Mvc;

namespace HRM.Web.Controllers
{
    public class VacationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
