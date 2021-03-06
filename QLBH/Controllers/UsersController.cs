﻿
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Net;
using System.Data.Entity.Infrastructure;
using MySql.Data.MySqlClient;
using QLBH.Models;
using System;
using System.Web.Security;

namespace QLBH.Controllers
{
    [Filters.Authorized]
    public class UsersController : Controller
    {
        // GET: Users
 
        
        private bool show;
        private string type;
        private string message;

        
        public ActionResult Index()
        {
            //var users = db.users.SqlQuery("SELECT * FROM USERS").ToList();
            var users = new List<User>();

            var sqlData = new User().getUserList();
            for (int i = 0; i < sqlData.Count(); i++)
            {
                users.Add(new User(sqlData[i].user_id, sqlData[i].username, sqlData[i].password, sqlData[i].fullname, sqlData[i].email));
            }

            return View(users);
        }



        

        //Admin
        public ActionResult Details(int? id)
        {
            User user = new User();
            user.User_id = id;

            if (user.User_id == null)
            {
                return HttpNotFound();
            }

            User userData = new User().getUserbyId(user.User_id);

            if (userData == null)
            {
                return HttpNotFound();
            }
            return View(userData);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult saveData()
        {

            User user = new User();
            user.Username = Request["Username"];
            user.Password = Request["Password"];
            user.Fullname = Request["Fullname"];
            user.Email = Request["Email"];

            try
            {
                bool checkUserExisted = new User().checkUserExisted(user.Username);
                if (checkUserExisted == true)
                {
                    this.show = true;
                    this.type = "danger";
                    this.message = "Lưu dữ liệu không thành công do tài khoản đã tồn tại!";
                    ModelState.AddModelError("", this.message);
                }

                bool checkEmailExisted = new User().checkUserExisted(user.Email);
                if (checkEmailExisted == true)
                {
                    this.show = true;
                    this.type = "danger";
                    this.message = "Lưu dữ liệu không thành công do email đã tồn tại!";
                    ModelState.AddModelError("", this.message);
                }

                if (ModelState.IsValid)
                {
                    int noOfRowInserted = new User().insertData(user.Username, user.Password, user.Fullname, user.Email);

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
            var users = new List<User>();

            var sqlData = new User().getUserList();
            for (int i = 0; i < sqlData.Count(); i++)
            {
                users.Add(new User(sqlData[i].user_id, sqlData[i].username, sqlData[i].password, sqlData[i].fullname, sqlData[i].email));
            }

            //Noti
            var Noti = new Notification()
            {
                Show = this.show,
                Type = this.type,
                Message = this.message
            };

            ViewBag.Noti = Noti;
            return View("Index", users);
        }

        
        public ActionResult Edit(int? id)
        {
            User user = new User();
            user.User_id = id;

            if (user.User_id == null)
            {
                return HttpNotFound();
            }

            User userData = new User().getUserbyId(user.User_id);

            if (userData == null)
            {
                return HttpNotFound();
            }
            return View(userData);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult saveEditData()
        {

            User user = new User();
            user.User_id = Int32.Parse(Request["user_id"]);
            user.Fullname = Request["Fullname"];
            user.Email = Request["Email"];

            try
            {
                if (ModelState.IsValid)
                {
                    int noOfRowInserted = new User().updateData(user.User_id, user.Fullname, user.Email);

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
            var users = new List<User>();

            var sqlData = new User().getUserList();
            for (int i = 0; i < sqlData.Count(); i++)
            {
                users.Add(new User(sqlData[i].user_id, sqlData[i].username, sqlData[i].password, sqlData[i].fullname, sqlData[i].email));
            }

            //Noti
            var Noti = new Notification()
            {
                Show = this.show,
                Type = this.type,
                Message = this.message
            };

            ViewBag.Noti = Noti;
            return View("Index", users);
        }

        
        public ActionResult Delete(int? id)
        {
            User user = new User();
            user.User_id = id;

            if (id == null)
            {
                return HttpNotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    
                    int noOfRowInserted = new User().deleteData(user.User_id);

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

            var users = new List<User>();

            var sqlData = new User().getUserList();
            for (int i = 0; i < sqlData.Count(); i++)
            {
                users.Add(new User(sqlData[i].user_id, sqlData[i].username, sqlData[i].password, sqlData[i].fullname, sqlData[i].email));
            }

            //Noti
            var Noti = new Notification()
            {
                Show = this.show,
                Type = this.type,
                Message = this.message
            };

            ViewBag.Noti = Noti;
            return View("Index", users);
        }
    }
}