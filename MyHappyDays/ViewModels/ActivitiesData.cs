using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyHappyDays.Models;
using MyHappyDays.Controllers;

namespace MyHappyDays.ViewModels
{
    public class ActivitiesData
    {
        public IEnumerable <Activity> Activities { get; set; }
        public IEnumerable<Club> Clubs { get; set; }
        public IEnumerable<Enrolment> Enrolments { get; set; }
    }
}