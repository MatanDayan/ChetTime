﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChetTime.Db;
using ChetTime.Models;
using ChetTime.Services;
using Microsoft.AspNetCore.Identity;

namespace ChetTime.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IChatHistoryService _history;
        private Db.DbContext __db;

        public HomeController(Db.DbContext _db,ILogger<HomeController> logger, IChatHistoryService history)
        {
            _logger = logger;
            __db = _db;
            _history = history;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
