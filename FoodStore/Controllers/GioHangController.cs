
using FoodStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;




namespace FoodStore.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        private FoodStoreEntities db = new FoodStoreEntities();



        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }



        public JsonResult ThemGioHang(int idproduct)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.Find(n => n.productId == idproduct);
            if (sp == null)
            {
                sp = new GioHang(idproduct);
                lstGioHang.Add(sp);
            }

            return Json(new { item = sp, success = true }, JsonRequestBehavior.AllowGet);
        }



        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.productQuantity);
            }
            return iTongSoLuong;
        }



        static private double dTongTien = 0;



        public double getTongTien()
        {
            return dTongTien + 30000;
        }
        public double TongTien()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.totalPrice);
            }
            return dTongTien;
        }
        public double TongTienHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.totalPrice);
            }
            return dTongTien;
        }

        public double PhiShip()
        {
            double PhiShip = 30000;
            return PhiShip;
        }





        public ActionResult GioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }



        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }



        public JsonResult CapNhatGioHang(int id, int quantity)     //truy cập sử dụng Url
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.SingleOrDefault(n => n.productId == id);
            sp.productQuantity = quantity;
            return Json(new { item = sp }, JsonRequestBehavior.AllowGet);
        }



        public JsonResult XoaSPKhoiGioHang(int productId)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.SingleOrDefault(n => n.productId == productId);
            if (sp != null)
            {
                lstGioHang.RemoveAll(n => n.productId == productId);
                //if (lstGioHang.Count == 0)
                //{
                //    return RedirectToAction("Index", "SACHes");
                //}
            }
            return Json(new { item = sp }, JsonRequestBehavior.AllowGet);
        }




        public ActionResult XoaGioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            lstGioHang.Clear();
            return RedirectToAction("Index", "SACHes");
        }



        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return Redirect("~/User/DangNhap");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Products");
            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien() + 30000 ;
            ViewBag.PhiShip = PhiShip();
            ViewBag.TongTienHang = TongTienHang();
            return View(lstGioHang);




        }



        [HttpPost]
        public ActionResult DatHang(FormCollection f)
        {
            Orders ddh = new Orders();
            OrderDetail ct = new OrderDetail();
            Customer kh = (Customer)Session["cmt"];
            List<GioHang> lstGioHang = LayGioHang();
            //.NullReferenceException
            if (kh.CustomerId != null)
            {
                try
                {
                    
                    ddh.CustomerId = kh.CustomerId;
                    
                    ddh.OrderDate = DateTime.Now;
                    ddh.Address = kh.Address;
                    ddh.RecipientPhone = kh.Phone;
               
                    var NgayGiao = String.Format("{0:MM/mm/yyyy}", f["NgayGiao"]);
                    ddh.DeliveryDate = DateTime.Parse(NgayGiao);


                    var giatien = ct.Price ;
                    ddh.OrderPrice = giatien;

                    



                    db.Orders.Add(ddh);
                    db.SaveChanges();
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine(e);
                }
            }



            foreach (var item in lstGioHang)
            {
                OrderDetail ctdh = new OrderDetail();
                ctdh.OrderId = ddh.OrderId;
                ctdh.ProductId = item.productId;
                ctdh.Quantity = item.productQuantity;
                ctdh.Price = (decimal)item.productPrice;
                db.OrderDetail.Add(ctdh);
            }
            db.SaveChanges();

            Session["GioHang"] = null;
            return RedirectToAction("XacNhanDonHang", "GioHang");

           



        }

        public ActionResult ThanhToan()
        {
            return View();
        }

        public ActionResult XacNhanDonHang()
        {
            return View();
        }




    }
}
