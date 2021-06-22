using FakeProductIden.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FakeProductIden.Controllers
{
    public class HomeController : Controller
    {
        public static string minerAddress = "miner";
        public static BlockChain chain = new BlockChain(10, 2);
        public static bool isLoaded = false;

        public static void LoadRecord()
        {
            var proDB = new FakeProductIdenEntities();
            var pro = proDB.Products.ToList();
            foreach (var item in pro)
            {
              chain.MineBlock(minerAddress, item.pr_id);
            }
            

        }

        public ActionResult Index()
        {
            if(!HomeController.isLoaded)
            {
                HomeController.LoadRecord();
                HomeController.isLoaded = true;
            }
            ViewBag.ViewChain = chain.GetAll();
            return View("Index");
        }

        

        public ActionResult About()
        {
            if (!HomeController.isLoaded)
            {
                HomeController.LoadRecord();
                HomeController.isLoaded = true;
            }
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}