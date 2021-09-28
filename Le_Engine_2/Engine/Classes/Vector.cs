using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Le_Engine_2.Engine
{
    public class Vector
    {
        private double x;
        public double X
        {
            get { return x; }
            set { if (value < int.MaxValue && value > int.MinValue) x = value;}
        }
        private double y;
        public double Y
        {
            get { return y; }
            set { if (value < int.MaxValue && value > int.MinValue) y = value; }
        }
        public Vector(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public static int GetDistance(Vector Point1, Vector Point2)
        {
            if (Point1 != null && Point2 != null)
            {
                int x = Math.Abs((int)Point1.X - (int)Point2.X);
                int y = Math.Abs((int)Point1.Y - (int)Point2.Y);
                int Distance = (int)Math.Sqrt((x * x) + (y * y));
                return Distance;
            }
            return 0;
        }
        public static Vector GetMiddleLocation(Vector Position, Vector Scale)
        {
            return new Vector((int)Position.X + (int)Scale.X / 2, (int)Position.Y + (int)Scale.Y / 2);
        }
        public static Vector GetDirection(Vector Too, Vector From)
        {
            return new Vector(Too.x - From.x, Too.y - From.y);
        }
        public static Shape GetClosestShape(Vector Position, string Tag)
        {
            List<Shape> shapes = Le_Engine.GetShapes(Tag);
            if (shapes.Count == 0)
            {
                return null;
            }
            Shape CurrentClosest = shapes[0];
            foreach (Shape item in shapes)
            {
                if (GetDistance(item.Position, Position) < GetDistance(CurrentClosest.Position, Position))
                {
                    CurrentClosest = item;
                }
            }
            return CurrentClosest;
        }
        public static Vector AngleToVector(double Angle)
        {
            var x = 200 * Math.Sin(Math.PI * 2 * Angle / 360);
            var y = 200 * Math.Cos(Math.PI * 2 * Angle / 360);
            return new Vector(x, y);
        }
    }
}
