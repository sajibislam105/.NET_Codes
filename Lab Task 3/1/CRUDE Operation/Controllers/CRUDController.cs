using CRUDE_Operation.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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
            existingProduct.Name = pr.Name;
            existingProduct.Price = pr.Price;
            existingProduct.Qty = pr.Qty;
            db.SaveChanges();            
            return RedirectToAction("ProductList");
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

        
        public ActionResult AddtoCart(Product pCrt)
        {            
            var db = new ShopEntities();
            var existingProduct = (from prdct in db.Products
                                   where prdct.Id == pCrt.Id
                                   select prdct).SingleOrDefault();
            //                              select prdct).ToList();

            List<Product> cartproduct = new List<Product>();
            string json = null;
            Session["cart"] = null;
            if (Session["cart"] == null)
            {
                cartproduct.Add(new Product()
                {
                    Id = existingProduct.Id,
                    Name = existingProduct.Name,
                    Price = existingProduct.Price,
                    Qty = existingProduct.Qty
                });               
                json = new JavaScriptSerializer().Serialize(cartproduct);
                Session["cart"] = json;
            }
            else
            {
                var d = new JavaScriptSerializer().Deserialize<List<Product>>(json);
                for (int i = 1; i <= 10; i++)
                {
                    var s1 = new Product()
                    {
                        Id = existingProduct.Id,
                        Name = existingProduct.Name,
                        Price = existingProduct.Price,
                        Qty = existingProduct.Qty

                    };
                    cartproduct.Add(s1);
                };
                /*Session["Id"] = existingProduct.Id;
                Session["Name"] = existingProduct.Name;
                Session["Price"] = existingProduct.Price;
                Session["Qty"] = existingProduct.Qty;*/
            }     
            return View(cartproduct);
        } 
        
        public ActionResult Checkout(OrderPlaced order)
        {
            var db = new ShopEntities();
            db.OrderPlaceds.Add(order);
            return RedirectToAction("ProductList");
        }
    }
}