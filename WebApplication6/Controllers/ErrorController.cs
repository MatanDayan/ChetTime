using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ChetTime.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Unauthorized()
        {
            return View();
        }
    }
}