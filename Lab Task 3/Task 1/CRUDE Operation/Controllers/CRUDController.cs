using CRUDE_Operation.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDE_Operation.Controllers
{
    public class CRUDController : Controller
    {
        // GET: CRUD
        public ActionResult ProductList()
        {
            var db = new ShopEntities();
            var products= db.Products.ToList();
            return View(products);
            
        }

        [HttpGet]
        public ActionResult AddItem()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddItem(Product prdct)
        {
            var db = new ShopEntities();
            db.Products.Add(prdct);
            db.SaveChanges();
            return RedirectToAction("ProductList");
            
        }

        [HttpGet]
        public ActionResult UpdateItem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateItem(Product pr)
        {
            var db = new ShopEntities();
            var existingProduct = (from prdct in db.Products
                       where prdct.Id == pr.Id
                       select prdct).SingleOrDefault();
            db.SaveChanges();
            return View(existingProduct);
        }

        [HttpGet]
        public ActionResult DeleteItem()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DeleteItem(Product pp)
        {
            var db = new ShopEntities();
            var existingProduct = (from prdct in db.Products
                                   where prdct.Id == pp.Id
                                   select prdct).SingleOrDefault();
            db.Products.Remove(existingProduct);
            db.SaveChanges();
            return RedirectToAction("ProductList");
        }
    }
}