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
    [Authorize]
    public class CustomersController : Controller
    {
        // GET: Customers
        private bool show;
        private string type;
        private string message;

        public ActionResult Index()
        {
            var customers = new List<Customer>();

            var sqlData = new Customer().getList();
            for (int i = 0; i < sqlData.Count(); i++)
            {
                customers.Add(new Customer(sqlData[i].cus_id, sqlData[i].cus_name, sqlData[i].cus_password, sqlData[i].cus_fullname, sqlData[i].cus_email, sqlData[i].cus_phone));
            }

            return View(customers);
        }

        public ActionResult Details(int? id)
        {

            Customer customer = new Customer();
            customer.Cus_id = id;
            if (customer.Cus_id == null)
            {
                return HttpNotFound();
            }

            Customer data = new Customer().getDatabyId(customer.Cus_id);

            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult saveData()
        {

            Customer customer = new Customer();
            customer.Cus_name = Request["Cus_name"];
            customer.Cus_password = Request["Cus_password"];
            customer.Cus_fullname = Request["Cus_fullname"];
            customer.Cus_email = Request["Cus_email"];
            customer.Cus_phone = Request["Cus_phone"];

            try
            {
                bool checkDataExisted = new Customer().checkCustomerExisted(customer.Cus_email);
                if (checkDataExisted == true)
                {
                    this.show = true;
                    this.type = "danger";
                    this.message = "Lưu dữ liệu không thành công do dữ liệu đã tồn tại!";
                    ModelState.AddModelError("", this.message);
                }

                if (ModelState.IsValid)
                {
                    int noOfRowInserted = new Customer().insertData(customer.Cus_name, customer.Cus_password, customer.Cus_fullname, customer.Cus_email, customer.Cus_phone);

                    if (noOfRowInserted > 0)
                    {
                        this.show = true;
                        this.type = "success";
                        this.message = "Lưu dữ liệu thành công!";
                    }
                }

            }
            catch (MySqlException ex)
            {
                this.show = true;
                this.type = "danger";
                this.message = "Lưu dữ liệu không thành công do lỗi CSDL! Mã lỗi là: " + string.Format("{0}", ex.Number);
                ModelState.AddModelError("", this.message);
            }
            catch (RetryLimitExceededException)
            {
                this.show = true;
                this.type = "danger";
                this.message = "Lưu dữ liệu không thành công!";
                ModelState.AddModelError("", this.message);
            }

            //var users = db.users.SqlQuery("SELECT * FROM USERS").ToList();
            var customers = new List<Customer>();

            var sqlData = new Customer().getList();
            for (int i = 0; i < sqlData.Count(); i++)
            {
                customers.Add(new Customer(sqlData[i].cus_id, sqlData[i].cus_name, sqlData[i].cus_password, sqlData[i].cus_fullname, sqlData[i].cus_email, sqlData[i].cus_phone));
            }

            //Noti
            var Noti = new Notification()
            {
                Show = this.show,
                Type = this.type,
                Message = this.message
            };

            ViewBag.Noti = Noti;
            return View("Index", customers);
        }

        public ActionResult Edit(int? id)
        {
            Customer data = new Customer();
            data.Cus_id = id;
            if (data.Cus_id == null)
            {
                return HttpNotFound();
            }

            Customer datas = new Customer().getDatabyId(data.Cus_id);

            if (data == null)
            {
                return HttpNotFound();
            }
            return View(datas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult saveEditData()
        {

            Customer customer = new Customer();
            customer.Cus_id = Int32.Parse(Request["Cus_id"]);
            customer.Cus_name = Request["Cus_name"];
            customer.Cus_password = Request["Cus_password"];
            customer.Cus_fullname = Request["Cus_fullname"];
            customer.Cus_email = Request["Cus_email"];
            customer.Cus_phone = Request["Cus_phone"];

            try
            {
                if (ModelState.IsValid)
                {
                    int noOfRowInserted = new Customer().updateData(customer.Cus_id, customer.Cus_name, customer.Cus_password, customer.Cus_fullname, customer.Cus_email, customer.Cus_phone);

                    if (noOfRowInserted > 0)
                    {
                        this.show = true;
                        this.type = "success";
                        this.message = "Lưu dữ liệu thành công!";
                    }
                }

            }
            catch (MySqlException)
            {
                this.show = true;
                this.type = "danger";
                this.message = "Lưu dữ liệu không thành công do lỗi CSDL!";
                ModelState.AddModelError("", this.message);
            }
            catch (RetryLimitExceededException)
            {
                this.show = true;
                this.type = "danger";
                this.message = "Lưu dữ liệu không thành công!";
                ModelState.AddModelError("", this.message);
            }

            //var users = db.users.SqlQuery("SELECT * FROM USERS").ToList();
            var customers = new List<Customer>();

            var sqlData = new Customer().getList();
            for (int i = 0; i < sqlData.Count(); i++)
            {
                customers.Add(new Customer(sqlData[i].cus_id, sqlData[i].cus_name, sqlData[i].cus_password, sqlData[i].cus_fullname, sqlData[i].cus_email, sqlData[i].cus_phone));
            }

            //Noti
            var Noti = new Notification()
            {
                Show = this.show,
                Type = this.type,
                Message = this.message
            };

            ViewBag.Noti = Noti;
            return View("Index", customers);
        }

        public ActionResult Delete(int? id)
        {

            Customer customer = new Customer();
            customer.Cus_id = id;
            if (customer.Cus_id == null)
            {
                return HttpNotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {

                    int noOfRowInserted = new Customer().deleteData(customer.Cus_id);

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

            var customers = new List<Customer>();

            var sqlData = new Customer().getList();
            for (int i = 0; i < sqlData.Count(); i++)
            {
                customers.Add(new Customer(sqlData[i].cus_id, sqlData[i].cus_name, sqlData[i].cus_password, sqlData[i].cus_fullname, sqlData[i].cus_email, sqlData[i].cus_phone));
            }

            //Noti
            var Noti = new Notification()
            {
                Show = this.show,
                Type = this.type,
                Message = this.message
            };

            ViewBag.Noti = Noti;
            return View("Index", customers);
        }
    }
}