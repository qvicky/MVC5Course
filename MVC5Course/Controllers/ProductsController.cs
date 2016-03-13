using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers {
    public class ProductsController : BaseController {
        //private FabricsEntities db = new FabricsEntities();
        //ProductRepository repo = RepositoryHelper.GetProductRepository();

        // GET: Products
        public ActionResult Index() {
            // var data =  repo.All().Where(p => !p.IsDeleted);    
            //var data = repo.All();
            var data = repo.All(true).Take(5);

            //共用同一個UnitOfWork (當有一個Repository以上)
            //var repoOL = RepositoryHelper.GetOrderLineRepository(repo.UnitOfWork);

            return View(data);
        }
        [HttpPost]
        public ActionResult Index(IList<Products批次更新ViewModel> products) {
            //public ActionResult Index(Product[] products) {
            if (ModelState.IsValid) {
                foreach (var item in products) {
                    var product = repo.Find(item.ProductId);
                    product.Stock = item.Stock;
                    product.Price = item.Price;
                }
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(repo.All().Take(5));
        }
        // GET: Products/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Find(id.Value);
            if (product == null && product.IsDeleted) {
                return HttpNotFound();
            }
            //ViewData[""] == product.OrderLine.ToList();


            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product) {
            if (ModelState.IsValid) {
                repo.Add(product);
                repo.UnitOfWork.Commit();
                //db.Product.Add(product);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Find(id.Value);
            if (product == null) {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form) {
            //Include 裡的欄位都會被Bind
            //Exclude 裡的欄位不會被Bind
            IProducts product = repo.Find(id);
            //延遲驗證
            if (TryUpdateModel<IProducts>(product))
            {
                repo.UnitOfWork.Commit();
                TempData["ProductsEditDoneMsg"] = "商品編輯成功 !!";
                return RedirectToAction("Index");
            }

            //預先驗證
            //if (ModelState.IsValid) {
            //    var db = (FabricsEntities)repo.UnitOfWork.Context;
            //    db.Entry(product).State = EntityState.Modified;
            //    db.SaveChanges();

            //    TempData["ProductsEditDoneMsg"] = "商品編輯成功 !!";
            //    return RedirectToAction("Index");
            //}
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Find(id.Value);
            if (product == null) {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Product product = repo.Find(id);
            product.IsDeleted = true;
            repo.UnitOfWork.Commit();
            //db.Product.Remove(product);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                var db = (FabricsEntities)repo.UnitOfWork.Context;
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
