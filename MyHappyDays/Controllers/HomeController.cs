using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyHappyDays.Models;
using MyHappyDays.DAL;
using MyHappyDays.ViewModels;

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
            ViewBag.Message = "A Children's Activity Scheduler used by 2 Types of User, 1 - Allows You To Register and Schedule Your Child's Activity Calendar, 2 - Allows an Activity Centre or Club to Advertise ther Activities.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Our contact details are: ";

            return View();
        }

        ////public ActionResult SearchAllActivities()
        ////{
        ////    ViewBag.Message = "Search All Available Activities";
        ////    return View();
        ////}


        ////Allow All Users to Search All Activities
        //[HttpGet]
        //public ActionResult HomeSearchAllActivities(int? SelectedClub)
        //{
        //    var clubs = db.Clubs.OrderBy(c => c.ClubName).ToList();
        //    ViewBag.SelectedClub = new SelectList(clubs, "ID", "ClubName", SelectedClub);
        //    int cluID = SelectedClub.GetValueOrDefault();

        //    IQueryable<Activity> activities = db.Activities.
        //        Where(a => !SelectedClub.HasValue || a.ClubID == cluID).
        //        OrderBy(c => c.ID).
        //        Include(c => c.Club);
        //    var sql = activities.ToString();
        //    return View(activities.ToList());

        //}
    }
}