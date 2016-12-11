using System;
using System.Collections.Generic;
using MyHappyDays.Models;


namespace MyHappyDays.ViewModels
{
    public class ChildProfile
    {
        public IEnumerable<ApplicationIdentity> Users { get; set; }
        public IEnumerable<Child> Children { get; set; }
        public IEnumerable<Enrolment> Enrolments { get; set; }
        public IEnumerable<Activity> Activities { get; set; }

        public int ActivityVMid { get; set; }
        public int EnrolmentVMid { get;set;}
        public int PaymentVMid { get; set; }
        public bool Paid { get; set; }

    }
}
