using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MyHappyDays.Models;

namespace MyHappyDays.ViewModels
{
    public class TopStatisticsData
    {
        public IEnumerable<Activity> Actvities { get; set; }
        public AgeGroup AgeGroup { get; set; }
        public int ActivityCount { get; set; }
        public Activity Activity { get; set; }
            

    }
}
