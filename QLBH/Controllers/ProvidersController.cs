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
    public class ProvidersController : Controller
    {
        // GET: Providers
        private bool show;
        private string type;
        private string message;

        public ActionResult Index()
        {
            var providers = new List<Provider>();

            var sqlData = new Provider().getProviderList();
            for (int i = 0; i < sqlData.Count(); i++)
            {
                providers.Add(new Provider(sqlData[i].provider_id, sqlData[i].provider_name, sqlData[i].provider_email, sqlData[i].provider_address, sqlData[i].provider_phone));
            }

            return View(providers);
        }

        public ActionResult Details(int? id)
        {
            Provider provider = new Provider();
            provider.Provider_id = id;
            if (provider.Provider_id == null)
            {
                return HttpNotFound();
            }

            Provider data = new Provider().getProviderbyId(provider.Provider_id);

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
            Provider provider = new Provider();
            provider.Provider_name = Request["Provider_name"];
            provider.Provider_address = Request["Provider_address"];
            provider.Provider_email = Request["Provider_email"];
            provider.Provider_phone = Request["Provider_phone"];

            try
            {
                bool checkNameExisted = new Provider().checkNameExisted(provider.Provider_name);
                if (checkNameExisted == true)
                {
                    this.show = true;
                    this.type = "danger";
                    this.message = "Lưu dữ liệu không thành công do nhà cung cấp đã tồn tại!";
                    ModelState.AddModelError("", this.message);
                }

                bool checkEmailExisted = new Provider().checkEmailExisted(provider.Provider_email);
                if (checkEmailExisted == true)
                {
                    this.show = true;
                    this.type = "danger";
                    this.message = "Lưu dữ liệu không thành công do email đã tồn tại!";
                    ModelState.AddModelError("", this.message);
                }

                if (ModelState.IsValid)
                {
                    int noOfRowInserted = new Provider().insertData(provider.Provider_name, provider.Provider_email, provider.Provider_address, provider.Provider_phone);

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
            var providers = new List<Provider>();

            var sqlData = new Provider().getProviderList();
            for (int i = 0; i < sqlData.Count(); i++)
            {
                providers.Add(new Provider(sqlData[i].provider_id, sqlData[i].provider_name, sqlData[i].provider_email, sqlData[i].provider_address, sqlData[i].provider_phone));
            }

            //Noti
            var Noti = new Notification()
            {
                Show = this.show,
                Type = this.type,
                Message = this.message
            };

            ViewBag.Noti = Noti;
            return View("Index", providers);
        }

        public ActionResult Edit(int? id)
        {
            Provider provider = new Provider();
            provider.Provider_id = id;
            if (provider.Provider_id == null)
            {
                return HttpNotFound();
            }

            Provider data = new Provider().getProviderbyId(provider.Provider_id);

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

            Provider provider = new Provider();
            provider.Provider_id = Int32.Parse(Request["Provider_id"]);
            provider.Provider_name = Request["Provider_name"];
            provider.Provider_address = Request["Provider_address"];
            provider.Provider_email = Request["Provider_email"];
            provider.Provider_phone = Request["Provider_phone"];

            try
            {

                if (ModelState.IsValid)
                {
                    int noOfRowInserted = new Provider().updateData(provider.Provider_id, provider.Provider_name, provider.Provider_email, provider.Provider_address, provider.Provider_phone);

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
            var providers = new List<Provider>();

            var sqlData = new Provider().getProviderList();
            for (int i = 0; i < sqlData.Count(); i++)
            {
                providers.Add(new Provider(sqlData[i].provider_id, sqlData[i].provider_name, sqlData[i].provider_email, sqlData[i].provider_address, sqlData[i].provider_phone));
            }

            //Noti
            var Noti = new Notification()
            {
                Show = this.show,
                Type = this.type,
                Message = this.message
            };

            ViewBag.Noti = Noti;
            return View("Index", providers);
        }

        public ActionResult Delete(int? id)
        {
            Provider provider = new Provider();
            provider.Provider_id = id;

            if (provider.Provider_id == null)
            {
                return HttpNotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {

                    int noOfRowInserted = new Provider().deleteData(provider.Provider_id);

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

            var providers = new List<Provider>();

            var sqlData = new Provider().getProviderList();
            for (int i = 0; i < sqlData.Count(); i++)
            {
                providers.Add(new Provider(sqlData[i].provider_id, sqlData[i].provider_name, sqlData[i].provider_email, sqlData[i].provider_address, sqlData[i].provider_phone));
            }

            //Noti
            var Noti = new Notification()
            {
                Show = this.show,
                Type = this.type,
                Message = this.message
            };

            ViewBag.Noti = Noti;
            return View("Index", providers);
        }
    }
}