using FakeProductIden.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FakeProductIden.Controllers
{
    public class QRValidateController : Controller
    {
        // GET: QRValidate
        public ActionResult Index(string id)
        {
            if (!HomeController.isLoaded) // chưa được load (!false => true)
            {
                HomeController.LoadRecord();
                HomeController.isLoaded = true;
            }

            using (var context = new FakeProductIdenEntities())
            {
                var _product = (from p in context.Products where (p.pr_id == id) select p).LastOrDefault();
                ViewBag.ProductList = _product;
            }

            ViewBag.ValidateChain = HomeController.chain.IsValidChain();
            return View("Index");
        }
    }
}