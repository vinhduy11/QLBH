using QLBH.DatabaseModels;
using QLBH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBH.Controllers
{
    public class HomeController : Controller
    {
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

            ViewBag.Test = "test";

            return View(products);
        }

        public ActionResult ProductDetails(int? id)
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Noti = TempData["Noti"];
            return View();
        }
    }
}