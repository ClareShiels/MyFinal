using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHappyDays.Models
{
    public enum PaymentType
    {
        Visa, VisaDebit, Mastercard
    }
    public class Payment
    {
        //pk and fk is the enrolment id as payment is dependent on enrolment
        [Key]
        [ForeignKey("Enrolment")]

        public int EnrolmentID { get; set; }
        public int ID { get; set; }
        public double AmountReceived { get; set; }
        public double AmountDue { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Payment Received")]
        public DateTime DateReceived { get; set; }
        public string PayeeName { get; set; }

        //navigation property to implement a 1:1 relationship between payment and enrolment
        public virtual Enrolment Enrolment { get; set; }
    }
}