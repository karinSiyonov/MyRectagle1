using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRectagle1.Shared.Models.OOP
{
	public class MyRectagle
	{
        public string color { get; set; }
        public double Length { get; set; }
        public double width { get; set; }

        public double CalcWhitespace()
        {
            return Length * width;
        }

        public double perimeterRectangle()
        {
            return (width * 2) + (Length * 2);
        }

    }
}
