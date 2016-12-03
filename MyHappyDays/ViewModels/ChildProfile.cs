using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyHappyDays.Models;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using MyHappyDays.Controllers;
using MyHappyDays.DAL;

namespace MyHappyDays.ViewModels
{
    public class ChildProfile
    {
        public IEnumerable<ApplicationIdentity> Users { get; set; }
        public IEnumerable<Child> Children { get; set; }
        public IEnumerable<Enrolment> Enrolments { get; set; }
        public IEnumerable<Activity> Activities { get; set; }

    }
}
