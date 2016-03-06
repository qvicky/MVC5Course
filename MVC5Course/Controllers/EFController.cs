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
        // GET: EF
        public ActionResult Index()
        {
            var db = new FabricsEntities();

            db.Product.Add(new Product() {
                ProductName = "test123",
                Price = 3,
                Stock = 1,
                Active = true
            });

            try {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex) {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors) {
                    string entityName = item.Entry.Entity.GetType().Name;

                    foreach (DbValidationError err in item.ValidationErrors) {
                        throw new Exception(entityName + " 類型驗證失敗 : " + err.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception ex) {
                
                throw;
            }

             var data = db.Product.ToList();

            return View(data);
        }
        public ActionResult CustomerOrder() {
            var db = new FabricsEntities();
            var query = (from o in db.Order
                         join c in db.Client on o.ClientId equals c.ClientId
                         select new {
                             c.FirstName,
                             c.LastName,
                             o.OrderDate,
                             o.OrderTotal,
                             o.OrderStatus
                         });

            return View(query);
        }

    }
}