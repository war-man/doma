using doma.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace doma.Controllers
{
    public class CartController : Controller
    {
        ProjectDMEntities db = new ProjectDMEntities();
        //
        // GET: /Cart/
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult thongtin()
        {
            return View();
        }

         [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult thongtin(ThongTinNguoiDungMuaHang model)
        {
            return RedirectToAction("xacnhan", model);
        }

        public ActionResult xacnhan(ThongTinNguoiDungMuaHang model)
        {
            return View(model);
        }
    }
}