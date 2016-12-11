using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHappyDays.Models
{
    public class Enrolment
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please Select if Payment has been Made")]
        public Boolean PaymentMade { get; set; }
        public Boolean PaymentDue { get; set; }

        //foreign key from child entity
        [ForeignKey("Child")]
        public int ChildID { get; set; }

        //foreign key from activity entity
        [ForeignKey("Activity")]
        public int ActivityID { get; set; }

        
        //Navigation Properties:
        //implementing a m - 1 relationship between enrolment and activity
        public virtual Activity Activity { get; set; }
        //implementing a m:1 relationship between enrolment and child
        public virtual Child Child { get; set; }

        //navigation property to payment implementing a 1:1 relationship
       // public virtual Payment Payment { get; set; }
    }
}