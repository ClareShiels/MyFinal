using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace MyHappyDays.Models
{
    //public enum SpecialNeeds
    //{
    //    yes, no
    //}
    public class Child
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Enter Guardian's First Name")]
        [StringLength(50, ErrorMessage = "Guardian's First Name cannot be longer than 50 characters.")]
        [Column("GuardianFirstName")]
        [Display(Name = "Guardian's First Name")]

        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter Guardian's Last Name")]
        [StringLength(50, ErrorMessage = "Guardian's Last Name cannot be longer than 50 characters.")]
        [Display(Name = "Guardian's Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Child's Full Name")]
        public string ChildFullName
        {
            get
            {
                return ChildFirstName + ", " + ChildLastName;
            }
        }
        [Required(ErrorMessage = "Enter Guardian's Contact Number")]
        [Display(Name = "Guardian's Contact Number")]
        public string GuardianPhNo { get; set; }

        [Required(ErrorMessage = "Enter Guardian's Email Address")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string GuardianEmail { get; set; }

        [Required(ErrorMessage = "Enter Child's Last Name")]
        [Display(Name = "Child's Family Name")]
        public string ChildLastName { get; set; }

        [Required(ErrorMessage = "Enter Child's First Name")]
        [Display(Name = "Child's First Name")]
        public string ChildFirstName { get; set; }

        [Required(ErrorMessage = "Enter Child's Address Line 1")]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Required(ErrorMessage = "Enter Child's Address Line 2")]
        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "Enter County")]
        [Display(Name = "County")]
        public string County { get; set; }

        [Display(Name = "EirCode")]
        public string EirCode { get; set; }

        [Required(ErrorMessage = "Please Select If Your Child  is Allowed to Leave Without Supervision")]
        [Display(Name = "Permission To Leave")]
        public Boolean PermissionToLeave { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Child's Date Of Birth")]
        public DateTime DOB { get; set; }


        [Display(Name = "Does your Child have any Special Needs")]
        public Boolean SpecialNeeds { get; set; }

        //ChildSingleEntity is used to ensure there isn't a duplicate entry for this child by combining FirstName, LastName, DOB
        public string ChildSingleEntity
        {
            get
            {
                return string.Format("{0},{1},{2}", ChildFirstName, ChildLastName, DOB.ToString());
            }
        }

        //navigation properties implementing a 1:m relationship between Child and Enrolments
        public virtual ICollection<Enrolment> Enrolments { get; set; }

        //navigation property implementing a 1:1 relationship between Club Manager and Application User
        //[ForeignKey("UserID")]
        //public virtual ApplicationUser User { get; set; }

    }
}