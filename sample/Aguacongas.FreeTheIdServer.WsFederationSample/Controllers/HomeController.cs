// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Aguacongas.FreeTheIdServer.WsFederationSample.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secure()
        {
            return View();
        }
    }
}
