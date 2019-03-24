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
    public class ProductsController : Controller
    {
        // GET: Products
        private bool show;
        private string type;
        private string message;

        public ActionResult Index()
        {
            var products = new List<Product>();

            var sqlData = new Product().getProductList();

            for (int i = 0; i < sqlData.Count(); i++)
            {
                products.Add(new Product(sqlData[i].Product_id, sqlData[i].Category_id, sqlData[i].Provider_id, sqlData[i].Product_name, sqlData[i].Product_image, sqlData[i].Product_price, sqlData[i].Quantities, sqlData[i].Product_description, sqlData[i].Category_name, sqlData[i].Provider_name));
            }

            return View(products);
        }

        public ActionResult Details(int? id)
        {
            Product products = new Product();
            products.Product_id = id;
            if (products.Product_id == null)
            {
                return HttpNotFound();
            }

            Product data = new Product().getProductbyId(products.Product_id);

            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        public ActionResult Create()
        {
            List<SelectListItem> l1 = new List<SelectListItem>();
            var cats = new Product().getCategories();
            foreach (var item in cats)
            {
                l1.Add(new SelectListItem { Value = item.Category_id.ToString(), Text = item.Category_name });
            }

            List<SelectListItem> l2 = new List<SelectListItem>();
            var providers = new Product().getProviders();
            foreach (var item in providers)
            {
                l2.Add(new SelectListItem { Value = item.Provider_id.ToString(), Text = item.Provider_name });
            }

            ViewBag.cats = l1;
            ViewBag.providers = l2;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult saveData()
        {
            string file_name = null;
            string url_filename = null;
            Product Product = new Product();
            Product.Category_id = Int32.Parse(Request["Category_id"]);
            Product.Provider_id = Int32.Parse(Request["Provider_id"]);
            Product.Product_name = Request["Product_name"];
            
            var tmpFile = Request.Files[0];

            if (tmpFile.ContentLength > 0)
            {
                file_name = string.Format("~/Content/Uploads/{0}", tmpFile.FileName);
                url_filename = string.Format("/Content/Uploads/{0}", tmpFile.FileName);
                tmpFile.SaveAs(HttpContext.Server.MapPath(file_name));
            }

            Product.Product_image = url_filename;
            Product.Product_price = Int32.Parse(Request["Product_price"]);
            Product.Quantities = Int32.Parse(Request["Quantities"]);
            Product.Product_description = Request["Product_description"];

            try
            {
                bool checkNameExisted = new Product().checkNameExisted(Product.Product_name);
                if (checkNameExisted == true)
                {
                    this.show = true;
                    this.type = "danger";
                    this.message = "Lưu dữ liệu không thành công do tên sản phẩm đã tồn tại!";
                    ModelState.AddModelError("", this.message);
                }

                if (ModelState.IsValid)
                {
                    int noOfRowInserted = new Product().insertData(
                        Product.Category_id,
                        Product.Provider_id,
                        Product.Product_name,
                        Product.Product_image,
                        Product.Product_price,
                        Product.Quantities,
                        Product.Product_description
                        );

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
            var products = new List<Product>();

            var sqlData = new Product().getProductList();

            for (int i = 0; i < sqlData.Count(); i++)
            {
                products.Add(new Product(sqlData[i].Product_id, sqlData[i].Category_id, sqlData[i].Provider_id, sqlData[i].Product_name, sqlData[i].Product_image, sqlData[i].Product_price, sqlData[i].Quantities, sqlData[i].Product_description, sqlData[i].Category_name, sqlData[i].Provider_name));
            }

            //Noti
            var Noti = new Notification()
            {
                Show = this.show,
                Type = this.type,
                Message = this.message
            };

            ViewBag.Noti = Noti;
            return View("Index", products);
        }

        public ActionResult Edit(int? id)
        {
            

            Product products = new Product();
            products.Product_id = id;
            if (products.Product_id == null)
            {
                return HttpNotFound();
            }

            Product data = new Product().getProductbyId(products.Product_id);

            List<SelectListItem> l1 = new List<SelectListItem>();
            var cats = new Product().getCategories();
            foreach (var item in cats)
            {
                l1.Add(new SelectListItem { Value = item.Category_id.ToString(), Text = item.Category_name });
            }

            List<SelectListItem> l2 = new List<SelectListItem>();
            var providers = new Product().getProviders();
            foreach (var item in providers)
            {
                l2.Add(new SelectListItem { Value = item.Provider_id.ToString(), Text = item.Provider_name });
            }

            ViewBag.cats = l1;
            ViewBag.providers = l2;


            if (data == null)
            {
                return HttpNotFound();
            }

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult saveEditData()
        {
            string file_name = null;
            string url_filename = null;
            Product Product = new Product();
            Product.Category_id = Int32.Parse(Request["Category_id"]);
            Product.Provider_id = Int32.Parse(Request["Provider_id"]);
            Product.Product_name = Request["Product_name"];

            if (Request.Files[0].ContentLength > 0)
            {
                var tmpFile = Request.Files[0];

                file_name = string.Format("~/Content/Uploads/{0}", tmpFile.FileName);
                url_filename = string.Format("/Content/Uploads/{0}", tmpFile.FileName);
                tmpFile.SaveAs(HttpContext.Server.MapPath(file_name));
                Product.Product_image = url_filename;
            }
            else
            {
                Product.Product_image = url_filename;
            }
            
            Product.Product_price = Int32.Parse(Request["Product_price"]);
            Product.Quantities = Int32.Parse(Request["Quantities"]);
            Product.Product_description = Request["Product_description"];

            try
            {

                if (ModelState.IsValid)
                {
                    int noOfRowInserted = new Product().updateData(
                        Product.Product_id,
                        Product.Category_id,
                        Product.Provider_id,
                        Product.Product_name,
                        Product.Product_image,
                        Product.Product_price,
                        Product.Quantities,
                        Product.Product_description
                    );

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
            var products = new List<Product>();

            var sqlData = new Product().getProductList();

            for (int i = 0; i < sqlData.Count(); i++)
            {
                products.Add(new Product(sqlData[i].Product_id, sqlData[i].Category_id, sqlData[i].Provider_id, sqlData[i].Product_name, sqlData[i].Product_image, sqlData[i].Product_price, sqlData[i].Quantities, sqlData[i].Product_description, sqlData[i].Category_name, sqlData[i].Provider_name));
            }

            //Noti
            var Noti = new Notification()
            {
                Show = this.show,
                Type = this.type,
                Message = this.message
            };

            ViewBag.Noti = Noti;
            return View("Index", products);
        }

        public ActionResult Delete(int? id)
        {
            Product Product = new Product();
            Product.Product_id = id;

            if (Product.Product_id == null)
            {
                return HttpNotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {

                    int noOfRowInserted = new Product().deleteData(Product.Product_id);

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

            var products = new List<Product>();

            var sqlData = new Product().getProductList();

            for (int i = 0; i < sqlData.Count(); i++)
            {
                products.Add(new Product(sqlData[i].Product_id, sqlData[i].Category_id, sqlData[i].Provider_id, sqlData[i].Product_name, sqlData[i].Product_image, sqlData[i].Product_price, sqlData[i].Quantities, sqlData[i].Product_description, sqlData[i].Category_name, sqlData[i].Provider_name));
            }
            //Noti
            var Noti = new Notification()
            {
                Show = this.show,
                Type = this.type,
                Message = this.message
            };

            ViewBag.Noti = Noti;
            return View("Index", products);
        }
    }
}
