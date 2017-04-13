using System;
using Ninject;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Autopraonica.DAL;

namespace Autopraonica.Web.Controllers
{
    public class BaseController : Controller
    {
        [Inject]
        public AutopraonicaDbContext dbContext { get; set; }
    }
}