using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpHomeworkWeek3
{
    public abstract class Graph
    {

        public double Area { get; set; }

        public Graph()
        {
        }

        public abstract void calculateArea();

        public void showAreaByType(string type)
        {
            System.Console.WriteLine("Area of this "+type+" is " + Area);
        }

        public abstract void showArea();
    }



    public class Triangle : Graph
    {
        private double a, b, c;

        public Triangle(double a,double b ,double c) : base()
        {
            this.a = a;
            this.b = b;
            this.c = c;
            calculateArea();
        }

        public override void calculateArea()
        {
            double p = (a + b + c) / 2;
            double tmp = p * (p - a) * (p - b) * (p - c);  // 计算海伦公式的中间变量
            if(tmp > 0)
            {
                Area = Math.Sqrt(p * (p - a) * (p - b) * (p - c));
            }
            else
            {
                Area = -1;    //将输入不合法的情况面积置为负数；
            }
            
        }

        public override void showArea()
        {
            if (Area > 0)
            {
                showAreaByType("triangle");
            }
            else
            {
                System.Console.WriteLine("The input can't make a triangle!");
            }
        }

    }



    public class Circle : Graph
    {
        private double r;

        public Circle(double r) : base()
        {
            this.r = r;
            calculateArea();
        }

        public override void calculateArea()
        {
            Area = r * r * Math.PI;
        }

        public override void showArea()
        {
            showAreaByType("circle");
        }
    }



    public class Square : Graph
    {
        private double a;

        public Square(double a) : base()
        {
            this.a = a;
            calculateArea();
        }

        public override void calculateArea()
        {
            Area = a*a;
        }

        public override void showArea()
        {
            showAreaByType("square");
        }
    }



    public class Rectangle : Graph
    {
        private double width,height;

        public Rectangle(double width, double height) : base()
        {
            this.width = width;
            this.height = height;
            calculateArea();
        }

        public override void calculateArea()
        {
            Area = width * height;
        }

        public override void showArea()
        {
            showAreaByType("rectangle");
        }
    }



    public class GraphFactory
    {

        public static Graph GetGraph(string type)
        {
            Graph graph = null;
            if(type== "triangle")
            {
                Console.Write("Please input 3 side of this triangle:");
                double[] sides = new double[3];
                if (getNums(sides, 3))
                {
                    graph = new Triangle(sides[0], sides[1], sides[2]);
                }
            }
            else if(type == "circle")
            {
                Console.Write("Please input the radius of this circle:");
                double r = double.Parse(Console.ReadLine());
                graph = new Circle(r);
            }
            else if(type == "square")
            {
                Console.Write("Please input the side of this square:");
                double side = double.Parse(Console.ReadLine());
                graph = new Square(side);
            }
            else if (type == "rectangle")
            {
                Console.Write("Please input two sides of this rectangle:");
                double[] sides = new double[2];
                if (getNums(sides, 2))
                {
                    graph = new Rectangle(sides[0], sides[1]);
                }
            }
            return graph;
        }

        public static bool getNums(double[] num, int n)
        {
            string s = Console.ReadLine();
            string[] numstr = s.Split(' ');
            if (n != numstr.Length)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    num[i] = double.Parse(numstr[i]);
                }
                return true;
            }
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please input type of a shape(triangle/circle/square/rectangle): ");
            string type = Console.ReadLine();
            Graph graph;
            graph = GraphFactory.GetGraph(type);
            if(graph!=null)
                graph.showArea();
            else
                Console.WriteLine("The input is valid!");

        }
    }

}
