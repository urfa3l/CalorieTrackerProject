using System;
using System.Collections.Generic;
using System.Text;

namespace CalorieTracker
{
    internal class Food
    {
       public int FoodsID{get; set;}
        public string Name { get; set; }
        public double CaloriesperAmount { get; set; }
        public int FoodType { get; set;}
    }
}
