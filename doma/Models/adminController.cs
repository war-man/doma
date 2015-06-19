using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace doma.Models
{
    [Authorize(Roles = "Admin")]    
    public class adminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult products()
        {
            return View();
        }

        public ActionResult donhang()
        {
            return View();
        }
	}
}