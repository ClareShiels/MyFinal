using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyHappyDays.Models;
using MyHappyDays.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyHappyDays.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //need to come bck to get the LINQ query right 4/12
        public ActionResult Index()
        {
            IQueryable<TopStatisticsData> data = from activity in db.Activities
                                           group activity by activity.AgeGroup into activityGroupByAge
                                           select new TopStatisticsData()
                                           {
                                               AgeGroup = activityGroupByAge.Key,
                                               
                                               ActivityCount = activityGroupByAge.Count()
                                           };
            
            
            return View(data.ToList());
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

        public ActionResult SearchAllActivities()
        {
            ViewBag.Message = "Search All Available Activities";
            return View();
        }
    }
}