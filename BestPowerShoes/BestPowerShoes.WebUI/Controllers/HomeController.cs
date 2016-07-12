using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BestPowerShoes.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProcessDetail(int id)
        {
            return View();
        }
        public ActionResult MovePlanProcess(int id)
        {
            return View();
        }
        public ActionResult PlanChart()
        {
            return View();
        }
        public ActionResult PartyIndex()
        {
            return View();
        } 
        public ActionResult PaymentIndex()
        {
            return View();
        }
        public ActionResult PaymentSearch()
        {
            return View();
        }
        public ActionResult AdvancePayment()
        {
            return View();
        }
    }
}