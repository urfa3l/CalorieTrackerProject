using System;
using System.Collections.Generic;
using System.Text;

namespace CalorieTracker
{
    internal class FoodIntake
    {
        public int FoodIntakeID { get; set; }
        public int FoodID { get; set; }
        public int TrackerID { get; set; }
        public double Amount { get; set; }
        public double CalorieIncrease { get; set; }
    }
}
