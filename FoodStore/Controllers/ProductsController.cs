using FoodStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList;
using FoodStore.Models.CommentView;

namespace FoodStore.Controllers
{
    public class ProductsController : Controller
    {
        FoodStoreEntities db = new FoodStoreEntities();
        // GET: Products
        public ActionResult Index(int? page)
        {

            int iSize = 9;
            int iPageNum = (page ?? 1);
         
            var dac = from d in db.Product select d;
            return View(dac.OrderBy(s => s.ProductId).ToPagedList(iPageNum, iSize));
        }
        public ActionResult DoChay()
        {
            int c = 1;
            var dc = from s in db.Category
                     join p in db.Product on s.CategoryId equals p.CategoryId
                     where p.CategoryId == c
                     select p;
            return View(dc);

        }
        public ActionResult DoAnNhanh()
        {
            int c = 2;
            var dan = from s in db.Category
                      join p in db.Product on s.CategoryId equals p.CategoryId
                      where p.CategoryId == c
                      select p;
            return View(dan);

        }
        public ActionResult DoUong()
        {
            int c = 3;
            var du = from s in db.Category
                     join p in db.Product on s.CategoryId equals p.CategoryId
                     where p.CategoryId == c
                     select p;
            return View(du);

        }
        public ActionResult Lau()
        {
            int c = 4;
            var l = from s in db.Category
                    join p in db.Product on s.CategoryId equals p.CategoryId
                    where p.CategoryId == c
                    select p;
            return View(l);

        }
        public ActionResult HaiSan()
        {
            int c = 5;
            var hs = from s in db.Category
                     join p in db.Product on s.CategoryId equals p.CategoryId
                     where p.CategoryId == c
                     select p;
            return View(hs);

        }
        public ActionResult HoaQua()
        {
            int c = 6;
            var hq = from s in db.Category
                     join p in db.Product on s.CategoryId equals p.CategoryId
                     where p.CategoryId == c
                     select p;
            return View(hq);

        }
        public ActionResult DoNuong()
        {
            int c = 7;
            var dn = from s in db.Category
                     join p in db.Product on s.CategoryId equals p.CategoryId
                     where p.CategoryId == c
                     select p;
            return View(dn);

        }


        public ActionResult ChiTIetSanPham(int id)
        {
            var ctsp = from s in db.Product where s.ProductId == id select s;




            ViewBag.ListComment = new CommentDAO().ListCommentViewModel(0, id);

            return View(ctsp);
        }

        [ChildActionOnly]
        public ActionResult _ChildComment(int parentid, int productid)
        {
            var data = new CommentDAO().ListCommentViewModel(parentid, productid);
            var sessionUser = (Customer)Session["cmt"];


            return PartialView("_ChildComment", data);
        }


        public JsonResult AddNewComment(int productid, int customerid, int parentid, string commentmsg, string rate)
        {
            try
            {
                var dao = new CommentDAO();
                Comment comment = new Comment();


                comment.CommentMsg = commentmsg;
                comment.ProductId = productid;
                comment.CustomerId = customerid;
                comment.ParentID = parentid;
                comment.Rate = Convert.ToInt16(rate);
                comment.CommentDate = DateTime.Now;

                bool addcomment = dao.Insert(comment);
                if (addcomment == true)
                {
                    return Json(new { status = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetComment(int productid)
        {
            var data = new CommentDAO().ListCommentViewModel(0, productid);
            return PartialView("_ChildComment", data);
        }
    }


}
