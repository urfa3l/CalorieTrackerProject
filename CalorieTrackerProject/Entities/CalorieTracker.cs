using System;

namespace CalorieTracker
{
    public class CalorieTrackerModel
    {
        public int TrackerID { get; set; }
        public int UserID { get; set; }
        public double CalorieIntakeTotal { get; set; }
        public double CalorieOutputTotal { get; set; }
        public double BMR { get; set; }
        public DateTime Date { get; set; }
    }
}