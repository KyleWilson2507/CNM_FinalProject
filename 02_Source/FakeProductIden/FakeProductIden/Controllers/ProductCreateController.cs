using FakeProductIden.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FakeProductIden.Controllers
{
    public class ProductCreateController : Controller
    {
        // GET: ProductCreate
        public ActionResult Index()
        {
            if (!HomeController.isLoaded)
            {
                HomeController.LoadRecord();
                HomeController.isLoaded = true;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Submit(string proName, string proCmp, string proType, string proOrigin, string proPrice)
        {
            if (!HomeController.isLoaded)
            {
                HomeController.LoadRecord();
                HomeController.isLoaded = true;
            }

            string newProID = "";
            using(var db = new FakeProductIdenEntities())
            {
                var proIDList = db.Products.ToList().Count();
                newProID = getProIDCode(proIDList + 1);

                var newPro = new Product();
                newPro.pr_id = newProID;
                newPro.pr_name = proName;
                newPro.pr_cmp = proCmp;
                newPro.pr_type = proType;
                newPro.pr_origin = proOrigin;
                newPro.pr_price = Double.Parse(proPrice);
                db.Products.Add(newPro);
                db.SaveChanges();
            }
            HomeController.chain.MineBlock(HomeController.minerAddress, newProID);
            ViewBag.Message = "Product is created successfully!";
            return View("Index");
        }

        private string getProIDCode(int ID)
        {
            if (ID < 10)
            {
                return "PR00" + ID.ToString();
            }
            else if (ID < 100)
            {
                return "PR0" + ID.ToString();
            }
            else
            {
                return "PR" + ID.ToString();
            }
        }
    }
}