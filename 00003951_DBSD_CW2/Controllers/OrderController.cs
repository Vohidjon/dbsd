using _00003951_DBSD_CW2.DataAccess;
using _00003951_DBSD_CW2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace _00003951_DBSD_CW2.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        // GET: Order
        public ActionResult Index()
        {

            Customer user = this.getCustomer();
            FlowerOrderManager manager = new FlowerOrderManager();
            IList<FlowerOrder> list = manager.GetFlowerOrdersByCustomer(user.Id);

            return View(list);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            FlowerOrderManager manager = new FlowerOrderManager();
            FlowerOrder model = manager.GetFlowerOrderById(id);

            IList<Flower> flowers = new FlowerManager().GetFlowers();
            
            model.OrderItems = manager.GetOrderItems(model.Id);
            foreach (OrderItem orderItem in model.OrderItems)
            {
                orderItem.Flower = flowers.First(f => f.Id == orderItem.FlowerId);
            }

            return View(model);
        }

        // GET: Order/Checkout
        public ActionResult Checkout()
        {
            return View();
        }

        // POST: Order/Checkout
        [HttpPost]
        public ActionResult Checkout(FlowerOrder order)
        {
            //try
            //{
                Customer user = this.getCustomer();
                ShoppingCartManager cartManager = new ShoppingCartManager();
                FlowerOrderManager orderManager = new FlowerOrderManager();
                order.CustomerId = user.Id;
                order.CreatedAt = DateTime.Now;
                order.ProcessStatus = FlowerOrder.UNDER_PROCESS;
                
                orderManager.CreateOrder(order);
                // refetching just created order from db, 
                // since id property is required for the following logic
                order = orderManager.GetLastFlowerOrderFOrCustomer(user.Id);

                IList<ShoppingCartItem> cartItems = cartManager.GetItemsByCustomer(user.Id);
                foreach(ShoppingCartItem cartItem in cartItems)
                {
                    OrderItem orderItem = new OrderItem()
                    {
                        OrderId = order.Id,
                        FlowerId = cartItem.FlowerId,
                        Quantity = cartItem.Quantity
                    };
                    orderManager.CreateOrderItem(orderItem);
                    // delete cart item afterwards
                    cartManager.DeleteItem(cartItem.Id, user.Id);
                }
                return RedirectToAction("Index");
            //}
            //catch
            //{
                //return View();
            //}
        }
    }
}
