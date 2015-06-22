using doma.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace doma.Controllers
{
    public class adminController : Controller
    {
        //
        // GET: /admin/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult addproduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult addproduct([Bind(Include = "ID,Ten,DioGia,linkanh,MoTa,TinhTrang,SoLuong")] SanPham sanpham)
        {
            ProjectDMEntities db = new ProjectDMEntities();
            SanPham item = db.SanPhams.Create();
            item.DioGia = sanpham.DioGia;
            item.linkanh = sanpham.linkanh;
            item.MoTa = sanpham.MoTa;
            item.SoLuong = sanpham.SoLuong;
            item.Ten = sanpham.Ten;
            item.TinhTrang = sanpham.TinhTrang;

            db.SanPhams.Add(item);
            db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult editproduct(int id)
        {
            ProjectDMEntities db = new ProjectDMEntities();
            SanPham item = db.SanPhams.SingleOrDefault(t => t.ID == id);
            if (item != null)
            {
                return View(item);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult editproduct([Bind(Include = "ID,Ten,DioGia,linkanh,MoTa,TinhTrang,SoLuong")] SanPham sanpham)
        {
            ProjectDMEntities db = new ProjectDMEntities();
            SanPham item = db.SanPhams.SingleOrDefault(t => t.ID == sanpham.ID);
            if (item != null)
            {
                item.DioGia = sanpham.DioGia;
                item.linkanh = sanpham.linkanh;
                item.MoTa = sanpham.MoTa;
                item.SoLuong = sanpham.SoLuong;
                item.Ten = sanpham.Ten;
                item.TinhTrang = sanpham.TinhTrang;

                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }
    
    
        public ActionResult chitietdonhang(int id)
        {
            ProjectDMEntities db = new ProjectDMEntities();
            DonHang item = db.DonHangs.SingleOrDefault(t => t.ID == id);

            if(item != null)
            {
                return View(item);
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult doitrangthaidonhang(int id, int tinhtrang)
        {
            ProjectDMEntities db = new ProjectDMEntities();
            DonHang item = db.DonHangs.SingleOrDefault(t => t.ID == id);

            if (item != null)
            {
                item.TinhTrang = (short)tinhtrang;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult userinfor(string id)
        {
            ProjectDMEntities db = new ProjectDMEntities();
            AspNetUser user = db.AspNetUsers.SingleOrDefault(t => t.Id == id);
            return View(user);
        }
    }
}