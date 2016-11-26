using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHappyDays.Models
{
    public enum AgeGroup
    {
        UnderSix, SixToNine, NineToTwelve
    }

    public enum ActivityType
    {
        DropIn, Course
    }


    public class Activity
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Activity Name is Required")]
        [Display(Name = "Activity")]
        public string NameOfActivity { get; set; }


        [Required(ErrorMessage = "What's the max capacity of the class")]
        [Display(Name = "Max Capacity of Class")]
        public int MaxCapacity { get; set; }

        [Required(ErrorMessage = "Please specify the age group")]
        [Display(Name = "Age Group")]
        public AgeGroup AgeGroup { get; set; }

        [Required(ErrorMessage = "Please specify")]
        [Display(Name = "Drop-In Class or PreBooked")]
        public ActivityType ActivityType { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Column(TypeName = "money")]
        public decimal PriceOfActivity { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime ActivityCourseStartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Finish Date")]
        public DateTime ActivityCourseEndDate { get; set; }

        public DayOfWeek Day { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public DateTime ClassTime { get; set; }

        //foreign key from club
        [ForeignKey("Club")]
        public int ClubID { get; set; }

        //foreign key from Instructor
        [ForeignKey("Instructor")]
        public int InstructorID { get; set; }

        //Navigation Property 
        //implementing a 1:m relationship between  activity and  instructor
        public virtual Instructor Instructor { get; set; }

        //implementing a 1:m relationship between activity and enrolments
        public virtual ICollection<Enrolment> Enrolments { get; set; }
        //implementing a m:1 relationship between activities and activity centre
        public virtual Club Club { get; set; }

    }
}