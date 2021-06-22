using FakeProductIden.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FakeProductIden.Controllers
{
    public class ProductUpdateController : Controller
    {
        // GET: ProductUpdate
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
        public ActionResult Submit(string proID, string proPrice)
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

                var pro = (from p in db.Products where (p.pr_id == proID) select p).ToList().LastOrDefault();
                var newPro = new Product();
                newPro.pr_id = newProID;
                newPro.pr_name = pro.pr_name;
                newPro.pr_cmp = pro.pr_cmp;
                newPro.pr_type = pro.pr_type;
                newPro.pr_origin = pro.pr_origin;
                newPro.pr_price = Double.Parse(proPrice);
                db.Products.Add(newPro);
                db.SaveChanges();
            }
            HomeController.chain.MineBlock(HomeController.minerAddress, newProID);
            ViewBag.Message = "Product is updated successfully!";
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