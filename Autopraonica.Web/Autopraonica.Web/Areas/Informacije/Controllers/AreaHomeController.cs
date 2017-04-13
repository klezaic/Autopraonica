using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autopraonica.Web.Controllers;

namespace Autopraonica.Web.Areas.Informacije.Controllers
{
    public class AreaHomeController : BaseController
    {
        // GET: Informacije/AreaHome
        public ActionResult Index()
        {
            return View();
        }
    }
}