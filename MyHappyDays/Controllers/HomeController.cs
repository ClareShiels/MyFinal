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
                                                 from enrolment in db.Enrolments
                                                 where activity.ID == enrolment.ActivityID
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
            ViewBag.Message = "A Children's Activity Scheduler - allows the Activity Centre to advertise and the Child's Guardian to register";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Our contact details are: ";

            return View();
        }

        //Allow All Users to Search All Activities
        [AllowAnonymous]
        [HttpGet]
        public ActionResult HomeSearchAllActivities(int? SelectedClub)
        {
            var clubs = db.Clubs.OrderBy(c => c.ClubName).ToList();
            ViewBag.SelectedClub = new SelectList(clubs, "ID", "ClubName", SelectedClub);
            int cluID = SelectedClub.GetValueOrDefault();

            IQueryable<Activity> activities = db.Activities.
                Where(a => !SelectedClub.HasValue || a.ClubID == cluID).
                OrderBy(c => c.ID).
                Include(c => c.Club);
            var clubActivities = activities.ToString();
            return View(activities.ToList());

        }
    }
}