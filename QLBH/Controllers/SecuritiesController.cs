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
    public class SecuritiesController : Controller
    {

        //Session
        private bool show;
        private string type;
        private string message;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult doLogin()
        {
            User user = new User();
            user.Username = Request["Username"];
            user.Password = Request["Password"];

            try
            {
                bool checkUserExisted = new User().checkUserExisted(user.Username);
                if (checkUserExisted == false)
                {
                    this.show = true;
                    this.type = "danger";
                    this.message = "Tài khoản không tồn tại. Vui lòng kiểm tra!";
                    ModelState.AddModelError("", this.message);
                }

                if (ModelState.IsValid)
                {
                    bool doLogin = new User().doLogin(user.Username, user.Password);

                    if (doLogin == true)
                    {
                        this.show = true;
                        this.type = "success";
                        this.message = "Đăng nhập thành công!";
                        var info = new User().getFullUserInfo(user.Username);

                        Session["username"] = info.Username;
                        Session["fullname"] = info.Fullname;
                        Session["isLogged"] = true;

                    }
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (MySqlException)
            {
                this.show = true;
                this.type = "danger";
                this.message = "Đăng nhập không thành công do lỗi CSDL!";
                ModelState.AddModelError("", this.message);
            }
            catch (RetryLimitExceededException)
            {
                this.show = true;
                this.type = "danger";
                this.message = "Đăng nhập không thành công!";
                ModelState.AddModelError("", this.message);
            }

            var Noti = new Notification()
            {
                Show = this.show,
                Type = this.type,
                Message = this.message
            };

            

            TempData["Noti"] = Noti;

            return RedirectToAction("Login", "Home");
        }


        public ActionResult doLogout()
        {
            Session["isLogged"] = false;
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}