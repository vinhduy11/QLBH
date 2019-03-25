
using MySql.Data.MySqlClient;
using QLBH.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBH.Controllers
{
    [Filters.Authorized]
    public class OrdersController : Controller
    {
        // GET: Bills
        private bool show;
        private string type;
        private string message;
        public ActionResult Index()
        {
            //var users = db.users.SqlQuery("SELECT * FROM USERS").ToList();
            var orders = new List<Order>();

            orders = new Order().getList();
            return View(orders);
        }

        public ActionResult Details(int id)
        {
            Order order = new Order();
            order.Order_id = id;
            if (order.Order_id == null)
            {
                return HttpNotFound();
            }

            var orders = new Order().getOrderbyId(id);

            var order_details = new OrderDetails().getOderDetailsbyId(id);
            ViewBag.Order_details = order_details;

            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Delete(int? id)
        {
            Order order = new Order();
            order.Order_id = id;

            if (order.Order_id == null)
            {
                return HttpNotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {

                    int noOfRowInserted = new Order().deleteData(order.Order_id);

                    if (noOfRowInserted > 0)
                    {
                        this.show = true;
                        this.type = "success";
                        this.message = "Xóa dữ liệu thành công!";
                    }
                }

            }
            catch (MySqlException)
            {
                this.show = true;
                this.type = "danger";
                this.message = "Xóa dữ liệu không thành công do lỗi CSDL!";
                ModelState.AddModelError("", this.message);
            }
            catch (RetryLimitExceededException)
            {
                this.show = true;
                this.type = "danger";
                this.message = "Xóa dữ liệu không thành công!";
                ModelState.AddModelError("", this.message);
            }

            var orders = new List<Order>();

            orders = new Order().getList();

            //Noti
            var Noti = new Notification()
            {
                Show = this.show,
                Type = this.type,
                Message = this.message
            };

            ViewBag.Noti = Noti;
            return View("Index", orders);
        }
    }
}