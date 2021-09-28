using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Le_Engine_2.Engine
{
    public class Line 
    {
        public Vector StartPoint;
        public Vector EndPoint;
        public Color color;
        public int LifeTime;
        public Line(Vector startPoint, Vector endPoint, Color color)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            this.color = color;
            Le_Engine.RegisterLine(this);
        }
        public Line(double StartX, double StartY, double EndX, double EndY, Color color)
        {
            StartPoint = new Vector(StartX, StartY);
            EndPoint = new Vector(EndX, EndY);
            this.color = color;
            Le_Engine.RegisterLine(this);
        }

        public static Vector GetCollisionOfInfiniteRay(Line line1, Line line2)
        {
            int sx1 = (int)line1.StartPoint.X;
            int sy1 = (int)line1.StartPoint.Y;
            int ex1 = (int)line1.EndPoint.X;
            int ey1 = (int)line1.EndPoint.Y;
            int sx2 = (int)line2.StartPoint.X;
            int sy2 = (int)line2.StartPoint.Y;
            int ex2 = (int)line2.EndPoint.X;
            int ey2 = (int)line2.EndPoint.Y;

            int A1 = ey1 - sy1;
            int B1 = sx1 - ex1;
            int C1 = A1 * sx1 + B1 * sy1;
            int A2 = ey2 - sy2;
            int B2 = sx2 - ex2;
            int C2 = A2 * sx2 + B2 * sy2;
            float delta = A1 * B2 - A2 * B1;
            float x = (B2 * C1 - B1 * C2) / delta;
            float y = (A1 * C2 - A2 * C1) / delta;
            if (delta != 0)
            {
                return new Vector(Convert.ToInt32(x), Convert.ToInt32(y));
            }
            else { return null; }
            
        }
        public static Vector GetCollisionOfRay(Line CollisionRay, Line Ray)
        {
            Vector colp = null;
            Vector Cp = GetCollisionOfInfiniteRay(Ray, CollisionRay);
            if (Cp != null)
            {
                if (Vector.GetDistance(Ray.StartPoint, Cp) <= Math.Max(Math.Abs(Ray.StartPoint.X - Ray.EndPoint.X), Math.Abs(Ray.StartPoint.Y - Ray.EndPoint.Y)))
                {
                    if (Ray.StartPoint.X >= Ray.EndPoint.X)
                    {
                        if (Cp.X <= Ray.StartPoint.X)
                        {
                            if (Ray.StartPoint.Y >= Ray.EndPoint.Y)
                            {
                                if (Cp.Y <= Ray.StartPoint.Y) colp = Cp;
                            }
                            else
                            {
                                if (Cp.Y >= Ray.StartPoint.Y) colp = Cp;
                            }
                        }
                        else { colp = null; }
                    }
                    else
                    {
                        if (Cp.X >= Ray.StartPoint.X)
                        {
                            if (Ray.StartPoint.Y >= Ray.EndPoint.Y)
                            {
                                if (Cp.Y <= Ray.StartPoint.Y) colp = Cp;
                            }
                            else
                            {
                                if (Cp.Y >= Ray.StartPoint.Y) colp = Cp;
                            }
                        }
                        else { colp = null; }
                    }
                }
                else
                {
                    colp = null;
                }
                Vector Cp1 = colp;
                if (Cp1 != null)
                {
                    if (Vector.GetDistance(CollisionRay.StartPoint, Cp1) <= Math.Max(Math.Abs(CollisionRay.StartPoint.X - CollisionRay.EndPoint.X), Math.Abs(CollisionRay.StartPoint.Y - CollisionRay.EndPoint.Y)))
                    {
                        if (CollisionRay.StartPoint.X >= CollisionRay.EndPoint.X)
                        {
                            if (Cp1.X <= CollisionRay.StartPoint.X)
                            {
                                if (CollisionRay.StartPoint.Y >= CollisionRay.EndPoint.Y)
                                {
                                    if (Cp1.Y <= CollisionRay.StartPoint.Y) return Cp1;
                                }
                                else
                                {
                                    if (Cp1.Y >= CollisionRay.StartPoint.Y) return Cp1;
                                }
                            }
                        }
                        else
                        {
                            if (Cp1.X >= CollisionRay.StartPoint.X)
                            {
                                if (CollisionRay.StartPoint.Y >= CollisionRay.EndPoint.Y)
                                {
                                    if (Cp1.Y <= CollisionRay.StartPoint.Y) return Cp1;
                                }
                                else
                                {
                                    if (Cp1.Y >= CollisionRay.StartPoint.Y) return Cp1;
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }
        public static List<Line> TurnShapeIntoLineHitbox(IColision s)
        {
            List<Line> ll = new List<Line>();
            ll.Add(new Line(s.Position.X, s.Position.Y, s.Position.X + s.Scale.X, s.Position.Y, Color.Red));
            ll.Add(new Line(s.Position.X, s.Position.Y, s.Position.X, s.Position.Y + s.Scale.Y, Color.Red));
            ll.Add(new Line(s.Position.X + s.Scale.X, s.Position.Y, s.Position.X + s.Scale.X, s.Position.Y + s.Scale.Y, Color.Red));
            ll.Add(new Line(s.Position.X, s.Position.Y + s.Scale.Y, s.Position.X + s.Scale.X, s.Position.Y + s.Scale.Y, Color.Red));
            return ll;
        }
    }
}
