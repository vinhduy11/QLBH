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
    public class CategoriesController : Controller
    {
        // GET: Categories
        private bool show;
        private string type;
        private string message;

        public ActionResult Index()
        {
            var categories = new List<Category>();

            var sqlData = new Category().getCategoryList();
            for (int i = 0; i < sqlData.Count(); i++)
            {
                categories.Add(new Category(sqlData[i].category_id, sqlData[i].category_name));
            }

            return View(categories);
        }

        public ActionResult Details(int? id)
        {

            Category category = new Category();
            category.Category_id = id;
            if (category.Category_id == null)
            {
                return HttpNotFound();
            }

            Category data = new Category().getCategorybyId(category.Category_id);

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

            Category category = new Category();
            category.Category_name = Request["Category_name"];

            try
            {
                bool checkCategoryExisted = new Category().checkCategoryExisted(category.Category_name);
                if (checkCategoryExisted == true)
                {
                    this.show = true;
                    this.type = "danger";
                    this.message = "Lưu dữ liệu không thành công do dữ liệu đã tồn tại!";
                    ModelState.AddModelError("", this.message);
                }

                if (ModelState.IsValid)
                {
                    int noOfRowInserted = new Category().insertData(category.Category_name);

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
            var categories = new List<Category>();

            var sqlData = new Category().getCategoryList();
            for (int i = 0; i < sqlData.Count(); i++)
            {
                categories.Add(new Category(sqlData[i].category_id, sqlData[i].category_name));
            }

            //Noti
            var Noti = new Notification()
            {
                Show = this.show,
                Type = this.type,
                Message = this.message
            };

            ViewBag.Noti = Noti;
            return View("Index", categories);
        }

        public ActionResult Edit(int? id)
        {
            Category category = new Category();
            category.Category_id = id;
            if (category.Category_id == null)
            {
                return HttpNotFound();
            }

            Category data = new Category().getCategorybyId(category.Category_id);

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

            Category category = new Category();
            category.Category_name = Request["Category_name"];
            category.Category_id = Int32.Parse(Request["Category_id"]);

            try
            {
                if (ModelState.IsValid)
                {
                    int noOfRowInserted = new Category().updateData(category.Category_id, category.Category_name);

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
            var categories = new List<Category>();

            var sqlData = new Category().getCategoryList();
            for (int i = 0; i < sqlData.Count(); i++)
            {
                categories.Add(new Category(sqlData[i].category_id, sqlData[i].category_name));
            }

            //Noti
            var Noti = new Notification()
            {
                Show = this.show,
                Type = this.type,
                Message = this.message
            };

            ViewBag.Noti = Noti;
            return View("Index", categories);
        }

        public ActionResult Delete(int? id)
        {

            Category category = new Category();
            category.Category_id = id;
            if (category.Category_id == null)
            {
                return HttpNotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {

                    int noOfRowInserted = new Category().deleteData(category.Category_id);

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

            var categories = new List<Category>();

            var sqlData = new Category().getCategoryList();
            for (int i = 0; i < sqlData.Count(); i++)
            {
                categories.Add(new Category(sqlData[i].category_id, sqlData[i].category_name));
            }

            //Noti
            var Noti = new Notification()
            {
                Show = this.show,
                Type = this.type,
                Message = this.message
            };

            ViewBag.Noti = Noti;
            return View("Index", categories);
        }
    }
}