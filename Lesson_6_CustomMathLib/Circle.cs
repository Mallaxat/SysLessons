using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson_6_CustomMathLib
{
    // Circle - математическая окружность (что бы это не значило)
    public class Circle
    {
        private string name = "";
        private double r;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                name = value;
            }
        }

        public double R
        {
            get
            {
                return r;
            }
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegative(value);
                r = value;
            }
        }

        public Circle() : this(0) { }

        public Circle(double r) : this("", r) { }

        public Circle(string name, double r)
        {
            Name = name;
            R = r;
        }

        public double S()
        {
            return Math.PI * r * r;
        }

        public double L()
        {
            return 2 * Math.PI * r;
        }

        public override string ToString()
        {
            return $"{name}({r})";
        }
    }
}
