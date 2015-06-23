
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
        ProjectDMEntities db = new ProjectDMEntities();
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
            DonHang item = db.DonHangs.SingleOrDefault(t => t.ID == id);

            if (item != null)
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
            AspNetUser user = db.AspNetUsers.SingleOrDefault(t => t.Id == id);
            return View(user);
        }

        public ActionResult addgroupproduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult addgroupproduct(AddGroupProductModel model)
        {
            if (model.products.Count <= 0)
            {
                return null;
            }
            BoSanPham groups = db.BoSanPhams.Create();
            groups.Ten = model.Ten;
            groups.Mota = model.Mota;
            groups.NgayTao = DateTime.Now;

            db.BoSanPhams.Add(groups);
            for (int i = 0; i < model.products.Count; i++)
            {
                ChiTietBoSanPham item = db.ChiTietBoSanPhams.Create();
                item.IDBoSanPham = groups.ID;
                item.IDSanPham = model.products[i].id;
                item.SoLuongThuongMua = model.products[i].number;

                db.ChiTietBoSanPhams.Add(item);
            }
            db.SaveChangesAsync();
            return View();
        }

        public ActionResult editgroupproduct(int id)
        {
            BoSanPham item = db.BoSanPhams.SingleOrDefault(t => t.ID == id);

            if (item != null)
            {
                EditGroupProductModel model = new EditGroupProductModel();
                model.id = item.ID;
                model.Mota = item.Mota;
                model.Ten = item.Ten;
                model.products = new List<ItemInGroupProductModel>();
                List<ChiTietBoSanPham> listpr = item.ChiTietBoSanPhams.ToList();
                for (int i = 0; i < listpr.Count; i++)
                {
                    ItemInGroupProductModel prod = new ItemInGroupProductModel();
                    prod.id = listpr[i].IDSanPham;
                    prod.number = listpr[i].SoLuongThuongMua;

                    model.products.Add(prod);
                }

                ViewBag.item = model;
                return View();
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult editgroupproduct(EditGroupProductModel model)
        {
            if (model.products.Count <= 0)
            {
                return null;
            }
            BoSanPham groups = db.BoSanPhams.SingleOrDefault(t => t.ID == model.id);

            if (groups != null)
            {
                groups.Ten = model.Ten;
                groups.Mota = model.Mota;
                groups.NgayTao = DateTime.Now;

                db.Entry(groups).State = System.Data.Entity.EntityState.Modified;

                for (int i = 0; i < groups.ChiTietBoSanPhams.Count; i++)
                {
                    db.ChiTietBoSanPhams.RemoveRange(groups.ChiTietBoSanPhams);
                }

                for (int i = 0; i < model.products.Count; i++)
                {
                    ChiTietBoSanPham item = db.ChiTietBoSanPhams.Create();
                    item.IDBoSanPham = groups.ID;
                    item.IDSanPham = model.products[i].id;
                    item.SoLuongThuongMua = model.products[i].number;

                    db.ChiTietBoSanPhams.Add(item);
                }
                db.SaveChangesAsync();
                return View();
            }

            return HttpNotFound();
        }
    }

    public class AddGroupProductModel
    {
        public string Ten { get; set; }
        public string Mota { get; set; }
        public List<ItemInGroupProductModel> products { get; set; }
    }

    public class ItemInGroupProductModel
    {
        public int id { get; set; }
        public int number { get; set; }
    }

    public class EditGroupProductModel
    {
        public int id { get; set; }
        public string Ten { get; set; }
        public string Mota { get; set; }
        public List<ItemInGroupProductModel> products { get; set; }
    }
}