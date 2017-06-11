using _00003951_DBSD_CW2.DataAccess;
using _00003951_DBSD_CW2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _00003951_DBSD_CW2.Controllers
{
    [Authorize]
    public class CartController : BaseController
    {
        // GET: Cart
        public ActionResult Index()
        {
            Customer user = this.getCustomer();
            ShoppingCartManager manager = new ShoppingCartManager();
            IList<ShoppingCartItem> list = manager.GetItemsByCustomer(user.Id);
            FlowerManager flowerManager = new FlowerManager();
            IList<Flower> flowers = flowerManager.GetFlowers();
            foreach (ShoppingCartItem item in list)
            {
                item.Flower = flowers.First(f => f.Id == item.FlowerId);
            }
            return View(list);
        }


        // POST: Cart/Add/1
        [HttpPost]
        public ActionResult Add(int id)
        {
            // creates new shopping cart item if does not exist
            // increments quantity if already exists
            try
            {
                Customer user = this.getCustomer();
                ShoppingCartManager manager = new ShoppingCartManager();
                ShoppingCartItem model = manager.GetItemByFlowerId(id, user.Id);
                if(model == null)
                {
                    model = new ShoppingCartItem();
                    model.FlowerId = id;
                    model.CustomerId = user.Id;
                    model.Quantity = 1;
                    manager.CreateItem(model);
                } else
                {
                    // increment
                    model.Quantity++;
                    manager.UpdateItem(model.Id, model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cart/Edit/5
        public ActionResult Edit(int id)
        {
            Customer user = this.getCustomer();
            ShoppingCartManager manager = new ShoppingCartManager();
            ShoppingCartItem model = manager.GetItemById(id, user.Id);
            model.Flower = (new FlowerManager()).GetFlowerById(model.FlowerId);
            return View(model);
        }

        // POST: Cart/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ShoppingCartItem model)
        {
            try
            {
                Customer user = this.getCustomer();
                ShoppingCartManager manager = new ShoppingCartManager();
                
                manager.UpdateItem(id, model);
                return RedirectToAction("Index");
            } catch {
                return View(model);
            }
        }


        // GET: Cart/Delete/5

        public ActionResult Delete(int id)
        {
            Customer user = this.getCustomer();
            ShoppingCartManager manager = new ShoppingCartManager();
            manager.DeleteItem(id, user.Id);
            return RedirectToAction("Index");
        }
    }
}
