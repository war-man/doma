using doma.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace doma.Controllers
{
    public class HomeController : Controller
    {
        ProjectDMEntities db = new ProjectDMEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getallproducts()
        {
            List<SanPhamReturn> sanphams = (from item in db.SanPhams.AsNoTracking()
                                            select new SanPhamReturn
                                            {
                                                Ten = item.Ten,
                                                ID = item.ID,
                                                DonGia = item.DioGia,
                                                MoTa = item.MoTa,
                                                linkanh = item.linkanh
                                            }).ToList();
            return Json(sanphams, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getallgroupproducts()
        {
            List<BoSanPhamIndexModal> sanphams = (from item in db.BoSanPhams.AsNoTracking()
                                                  select new BoSanPhamIndexModal
                                            {
                                                Ten = item.Ten,
                                                id = item.ID,
                                                img = item.ChiTietBoSanPhams.FirstOrDefault().SanPham.linkanh
                                            }).ToList();
            return Json(sanphams, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getproductinfor(int id)
        {
            SanPham item = db.SanPhams.SingleOrDefault(t => t.ID == id);
            if (item != null)
            {
                SanPhamReturn sanpham = new SanPhamReturn
                {
                    Ten = item.Ten,
                    ID = item.ID,
                    DonGia = item.DioGia,
                    MoTa = item.MoTa,
                    linkanh = item.linkanh
                };
                return Json(sanpham, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }

        public ActionResult getgroupproductinfo(int id)
        {
            BoSanPham item = db.BoSanPhams.SingleOrDefault(t => t.ID == id);
            if (item != null)
            {
                BoSanPhamInfoIndexModal sanpham = new BoSanPhamInfoIndexModal
                {
                    Ten = item.Ten,
                    id = item.ID
                };

                List<ChiTietBoSanPham> chitiets = item.ChiTietBoSanPhams.ToList();

                for (int i = 0; i < chitiets.Count; i++)
                {
                    SanPhamTrongBoSanPham pro = new SanPhamTrongBoSanPham();
                    pro.ID = chitiets[i].IDSanPham;
                    pro.linkanh = chitiets[i].SanPham.linkanh;
                    pro.MoTa = chitiets[i].SanPham.MoTa;
                    pro.Ten = chitiets[i].SanPham.Ten;

                    sanpham.products.Add(pro);
                }
                return Json(sanpham, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult bosanpham(int id)
        {
            BoSanPham item = db.BoSanPhams.SingleOrDefault(t => t.ID == id);
            if (item != null)
            {
                BoSanPhamInfoIndexModal sanpham = new BoSanPhamInfoIndexModal
                {
                    Ten = item.Ten,
                    id = item.ID,
                    Mota = item.Mota
                };

                List<ChiTietBoSanPham> chitiets = item.ChiTietBoSanPhams.ToList();
                sanpham.products = new List<SanPhamTrongBoSanPham>();
                for (int i = 0; i < chitiets.Count; i++)
                {
                    SanPhamTrongBoSanPham pro = new SanPhamTrongBoSanPham();
                    pro.ID = chitiets[i].IDSanPham;
                    pro.linkanh = chitiets[i].SanPham.linkanh;
                    pro.MoTa = chitiets[i].SanPham.MoTa;
                    pro.Ten = chitiets[i].SanPham.Ten;
                    pro.GiaThuongMua = chitiets[i].SoLuongThuongMua;
                    pro.DonGia = chitiets[i].SanPham.DioGia;

                    sanpham.products.Add(pro);
                }
                return View(sanpham);
            }

            return HttpNotFound();
        }

        public ActionResult sanpham(int id)
        {
            SanPham item = db.SanPhams.SingleOrDefault(t => t.ID == id);
            if (item != null)
            {
                SanPhamReturn sanpham = new SanPhamReturn
                {
                    Ten = item.Ten,
                    ID = item.ID,
                    MoTa = item.MoTa,
                    DonGia =item.DioGia,
                    linkanh = item.linkanh
                };

                return View(sanpham);
            }
            return HttpNotFound();
        }

        public ActionResult baiviet(int id)
        {
            BaiViet item = db.BaiViets.SingleOrDefault(t => t.ID == id);
            if(item != null)
            {
                return View(item);
            }

            return RedirectToAction("index");
        }

        //public ActionResult listsanphamrelated(int id, bool bsp) 
        //{
        //    List<RelativeObject> results = new List<RelativeObject>();
        //    // Nếu là bộ sản phẩm thì đưa ra ds sản phẩm
        //    if(bsp)
        //    {
        //            results = (from i in db.SanPhams.AsNoTracking()
        //                       select new RelativeObject
        //                       {
        //                           id = i.ID,
        //                           name = i.Ten,
        //                           isgroup = false
        //                       }).ToList();                             
        //    }
        //    // Nếu là sản phẩm thì đưa ra ds các bộ sản phẩm mà sản phẩm đó có trong
        //    else
        //    {
        //        SanPham sanpham = db.SanPhams.SingleOrDefault(t => t.ID == id);
        //        results = (from i in db.ChiTietBoSanPhams
        //                   where i.IDSanPham == id
        //                   select new RelativeObject
        //                   {
        //                       id = i.IDBoSanPham,
        //                       name = i.BoSanPham.Ten,
        //                       isgroup = true,
        //                       linkanh = sanpham.linkanh
        //                   }).ToList();
        //    }
        //    return null;
        //}

    }

    public class SanPhamReturn
    {
        public string Ten { get; set; }
        public int ID { get; set; }
        public int DonGia { get; set; }
        public string MoTa { get; set; }
        public string linkanh { get; set; }
    }

    public class BoSanPhamIndexModal
    {
        public string Ten { get; set; }
        public int id { get; set; }
        public string img { get; set; }
    }

    public class BoSanPhamInfoIndexModal
    {
        public string Ten { get; set; }
        public int id { get; set; }
        public string Mota { get; set; }
        public List<SanPhamTrongBoSanPham> products { get; set; }
    }

    public class RelativeObject 
    {
        public string name;
        public int id;
        public bool isgroup;
        public string linkanh;
    }

    public class SanPhamTrongBoSanPham
    {
        public string Ten { get; set; }
        public int ID { get; set; }
        public int DonGia { get; set; }
        public string MoTa { get; set; }
        public string linkanh { get; set; }

        public int GiaThuongMua { get; set; }
    }
}