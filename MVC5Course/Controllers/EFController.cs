using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        FabricsEntities db = new FabricsEntities();
        // GET: EF
        public ActionResult Index(bool? IsActive, string keyword)
        {
            //var db = new FabricsEntities();

            //db.Product.Add(new Product() {
            //    ProductName = "test123",
            //    Price = 3,
            //    Stock = 1,
            //    Active = true
            //});

            //Product newProduct = new Product() {
            //    ProductName = "test456",
            //    Price = 3,
            //    Stock = 1,
            //    Active = true
            //};
            //db.Product.Add(newProduct);

            //try {
            //    db.SaveChanges();
            //}
            //catch (DbEntityValidationException ex) {
            //    foreach (DbEntityValidationResult item in ex.EntityValidationErrors) {
            //        string entityName = item.Entry.Entity.GetType().Name;

            //        foreach (DbValidationError err in item.ValidationErrors) {
            //            throw new Exception(entityName + " 類型驗證失敗 : " + err.ErrorMessage);
            //        }
            //    }
            //    throw;
            //}
            //catch (Exception ex) {
            //    throw;
            //}
            //var pkey=newProduct.ProductId;
            //var data = db.Product.OrderByDescending(p=>p.ProductId);

            //var data = db.Product.Where(p => p.Active.HasValue ? p.Active.Value == IsActive : false).OrderByDescending(p => p.ProductId);
            var data = db.Product.OrderByDescending(p => p.ProductId).AsQueryable();

            if (IsActive.HasValue) {
                data = data.Where(p => p.Active.HasValue ? p.Active.Value == IsActive : false);
            }

            if (!string.IsNullOrEmpty(keyword)) {
                data = data.Where(p => p.ProductName.Contains(keyword));
            }

            //更新資料
            //foreach (Product item in data) {
            //    item.Price = item.Price + 1;
            //}
            //db.SaveChanges();

            return View(data);
        }

        public ActionResult Detail(int id) {
            //var product = db.Product.Find(id);
            var product = db.Product.Where(p => p.ProductId == id).FirstOrDefault();

            return View(product);
        }

        public ActionResult Delete(int id) {
            var product = db.Product.Find(id);  //找到刪除的資料

            //foreach (var ol in db.OrderLine.Where(p=>p.ProductId ==id).ToList()) {
            //    db.OrderLine.Remove(ol);
            //}

            //foreach (var ol in product.OrderLine.ToList()) {
            //    db.OrderLine.Remove(ol);
            //}

            db.OrderLine.RemoveRange(product.OrderLine);
            db.Product.Remove(product);

            db.SaveChanges();

            return RedirectToAction("index");
        }


    }
}