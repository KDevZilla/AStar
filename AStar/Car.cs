using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AStar
{
    public class Car
    {
        public Car Box
        {
            get { return this; }
        }
        public Car Insulated
        {
            get { return this; }
        }
        public Car Include(String machine)
        {
            return this;
        }
        public void Test()
        {
            Car c = new Car().Box
                .Insulated
                .Include("Readio");


        }
    }
}
