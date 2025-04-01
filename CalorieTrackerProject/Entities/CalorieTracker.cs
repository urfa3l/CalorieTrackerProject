using System;
using System.Collections.Generic;

namespace CalorieTracker
{
    public class CalorieTrackerModel
    {
        public int TrackerID { get; set; }
        public UserModel User { get; set; }
        public double CalorieIntakeTotal { get; set; }
        public double CalorieOutputTotal { get; set; }
        public double BMR { get; set; }
        public DateTime Date { get; set; }
        public List<Excercise> Excercises { get; set; }
    }
}