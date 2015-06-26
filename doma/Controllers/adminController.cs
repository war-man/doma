
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

        //////////////////////////
        /////// Product //////////
        //////////////////////////
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

        //////////////////////////
        /////// Order //////////
        //////////////////////////
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

        //////////////////////////
        /////// Group Product ////
        //////////////////////////
        public ActionResult removegroupproduct(int id)
        {
            BoSanPham item = db.BoSanPhams.SingleOrDefault(t => t.ID == id);
            if (item != null)
            {
                db.ChiTietBoSanPhams.RemoveRange(item.ChiTietBoSanPhams);
                db.BoSanPhams.Remove(item);
                db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
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
            if (id != null)
            {
                ViewBag.id = id;
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

        public ActionResult getGroupProduct(int id)
        {
            BoSanPham item = db.BoSanPhams.SingleOrDefault(t => t.ID == id);

            if (item != null)
            {
                EditGroupProductModel model = new EditGroupProductModel();
                model.id = item.ID;
                model.Mota = item.Mota;
                model.Ten = item.Ten;
                model.products = new List<ProductInfo>();
                List<ChiTietBoSanPham> listpr = item.ChiTietBoSanPhams.ToList();

                for (int i = 0; i < listpr.Count; i++)
                {
                    ProductInfo prod = new ProductInfo();
                    prod.id = listpr[i].IDSanPham;
                    prod.number = listpr[i].SoLuongThuongMua;
                    prod.Ten = listpr[i].SanPham.Ten;
                    prod.DioGia = listpr[i].SanPham.DioGia;

                    model.products.Add(prod);
                }

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return HttpNotFound();
            }
        }

        //////////////////////////
        /////// Bài viết ////
        //////////////////////////
        public ActionResult addbaiviet() 
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult addbaiviet(BaiViet model)
        {
            model.NgayDang = DateTime.Now;
            if (ModelState.IsValid)
            {
                BaiViet item = db.BaiViets.Create();
                item.linkHinh = model.linkHinh;
                item.NgayDang = DateTime.Now;
                item.NoiDung = model.NoiDung;
                item.TieuDe = model.TieuDe;

                db.BaiViets.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult editbaiviet(int id)
        {
            BaiViet baiviet = db.BaiViets.SingleOrDefault(t => t.ID == id);
            if(baiviet!= null)
            {
                return View(baiviet);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult editbaiviet(BaiViet model)
        {
            model.NgayDang = DateTime.Now;
            if (ModelState.IsValid)
            {               
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult deletebaiviet(int id)
        {
            BaiViet baiviet = db.BaiViets.SingleOrDefault(t => t.ID == id);
            if (baiviet != null)
            {
                db.BaiViets.Remove(baiviet);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
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
        public List<ProductInfo> products { get; set; }
    }

    public class ProductInfo
    {
        public int id { get; set; }
        public string Ten { get; set; }
        public int DioGia { get; set; }

        public int number { get; set; }
    }
}