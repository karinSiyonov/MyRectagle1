using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRectagle1.Shared.Models.OOP
{
    public class MyRectangle
    {
        public double RectHeight { get; set; }
        public double RectWidth { get; set; }
        public string RectColor { get; set; }

        public double CalcArea()
        {
            return RectHeight * RectWidth;
        }
        public double CalcPerimeter()
        {
            return (RectWidth + RectHeight) * 2;
        }


    }
}