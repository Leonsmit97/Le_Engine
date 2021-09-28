using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Le_Engine_2.Engine
{
    public class Components
    {
        public bool IsMoving = false;
        public Shape GetColidedObject;
        private bool SwitchX = true;
        private bool SwitchY = false;
        public void MoveTo(IMoveable Entity, Vector Position, int speed, int delay)
        {
            IsMoving = true;
            Thread th = new Thread(() =>
            {
                lock(this)
                {
                    while (Position != Entity.Position)
                    {
                        if (Vector.GetDistance(Entity.Position, Position) < speed)
                        {
                            Entity.Position.X = Position.X;
                            Entity.Position.Y = Position.Y;
                            break;
                        }
                        if (Entity.Position.X > Position.X)
                        {
                            Entity.Position.X -= speed;
                        }
                        if (Entity.Position.X < Position.X)
                        {
                            Entity.Position.X += speed;
                        }
                        if (Entity.Position.Y > Position.Y)
                        {
                            Entity.Position.Y -= speed;
                        }
                        if (Entity.Position.Y < Position.Y)
                        {
                            Entity.Position.Y += speed;
                        }
                        Thread.Sleep(delay);
                    }
                    IsMoving = false;
                }
            });
            if (th.ThreadState == ThreadState.Stopped || th.ThreadState == ThreadState.Unstarted)
            {
                th.Start();
            }
        }
        public void MoveToPath(IMoveable Entity, List<Vector> Positions, int speed, int delay)
        {
            if (IsMoving == false)
            {
                IsMoving = true;
                Thread th = new Thread(() =>
                {
                    foreach (Vector i in Positions)
                    {
                        while (i != Entity.Position)
                        {
                            if (Vector.GetDistance(Entity.Position, i) < speed)
                            {
                                Entity.Position.X = i.X;
                                Entity.Position.Y = i.Y;
                                break;
                            }
                            if (Entity.Position.X > i.X)
                            {
                                Entity.Position.X -= speed;
                            }
                            if (Entity.Position.X < i.X)
                            {
                                Entity.Position.X += speed;
                            }
                            if (Entity.Position.Y > i.Y)
                            {
                                Entity.Position.Y -= speed;
                            }
                            if (Entity.Position.Y < i.Y)
                            {
                                Entity.Position.Y += speed;
                            }
                            Thread.Sleep(delay);
                        }
                    }
                    IsMoving = false;
                  ;


                });

                if (th.ThreadState == ThreadState.Stopped || th.ThreadState == ThreadState.Unstarted)
                {
                    th.Start();
                }
            }

        }
        public void MoveBetween2Points(Shape Entity, Vector p1, Vector p2, int speed)
        {
            if (Entity.Position.X > Math.Min(p1.X, p2.X) && SwitchX == true)
            {
                Entity.Position.X -= speed;
            }
            else
            {
                SwitchX = false;
            }
            if (Entity.Position.X < Math.Max(p1.X, p2.X) && SwitchX == false)
            {
                Entity.Position.X += speed;
            }
            else
            {
                SwitchX = true;
            }
            if (Entity.Position.Y > Math.Min(p1.Y, p2.Y) && SwitchY == true)
            {
                Entity.Position.Y -= speed;
            }
            else
            {
                SwitchY = false;
            }
            if (Entity.Position.Y < Math.Max(p1.Y, p2.Y) && SwitchY == false)
            {
                Entity.Position.Y += speed;
            }
            else
            {
                SwitchY = true;
            }
        }
        public bool IsColided(IColision Entity, string tag)
        {
            List<Shape> p = Le_Engine.GetShapes(tag);
            foreach (Shape s in p)
            {
                if (s.Position.Y + s.Scale.Y > Entity.Position.Y && Entity.Position.Y + Entity.Scale.Y > s.Position.Y && s.Position.X + s.Scale.X > Entity.Position.X && Entity.Position.X + Entity.Scale.X > s.Position.X)
                {
                    GetColidedObject = s;
                    return true;
                }
            }
            return false;
        }
        public bool IsColidedWithAny(Vector Position, Vector Scale)
        {
            List<Shape> p = Le_Engine.RenderStack;
            foreach (Shape s in p)
            {
                if (s.Position.Y + s.Scale.Y > Position.Y && Position.Y + Scale.Y > s.Position.Y &&
                    s.Position.X + s.Scale.X > Position.X && Position.X + Scale.X > s.Position.X)
                {
                    if (s.Position != Position && s.Scale != Scale && s.Z >= 0)
                    {
                        GetColidedObject = s;
                        return true;
                    }
                }
            }

            return false;
        }
        public bool IsColidedWithColiderObject(Vector Position, Vector Scale)
        {
            List<Shape> p = new List<Shape>();
            foreach(Shape sha in Le_Engine.RenderStack)
            {
                if(sha.HasColider)
                {
                    p.Add(sha);
                }
            }
            foreach (Shape s in p)
            {
                if (s.Position.Y + s.Scale.Y > Position.Y && Position.Y + Scale.Y > s.Position.Y &&
                    s.Position.X + s.Scale.X > Position.X && Position.X + Scale.X > s.Position.X)
                {
                    if (s.Position != Position && s.Scale != Scale && s.Z >= 0)
                    {
                        GetColidedObject = s;
                        return true;
                    }
                }
            }

            return false;
        }
        public bool IsAreaColidedWithTag(string Tag, Vector Position, Vector Scale)
        {
            List<Shape> p = Le_Engine.GetShapes(Tag);
            foreach (Shape s in p)
            {
                if (s.Position.Y + s.Scale.Y > Position.Y && Position.Y + Scale.Y > s.Position.Y && s.Position.X + s.Scale.X > Position.X && Position.X + Scale.X > s.Position.X)
                {
                    GetColidedObject = s;
                    return true;
                }
            }
            return false;
        }
        public bool IsInArea(Shape s, Vector p2, Vector s2)
        {
            if (s.Position.Y + s.Scale.Y > p2.Y && p2.Y + s2.Y > s.Position.Y && s.Position.X + s.Scale.X > p2.X && p2.X + s2.X > s.Position.X)
            {
                return true;
            }
            return false;
        }
        public void IsPushable(string Tag, Vector Speed, IPushable Entity)
        {
            bool colided = Entity.components.IsAreaColidedWithTag(Tag, new Vector(Entity.Position.X + Speed.X, Entity.Position.Y + Speed.Y), Entity.Scale);
            if(colided)
            {
                IsPushable(Tag, Speed, Entity.components.GetColidedObject);
                Entity.components.GetColidedObject.Position.X += Speed.X;
                Entity.components.GetColidedObject.Position.Y += Speed.Y;
            }
        }
        public static List<Vector> GetPathFromShapes(List<Shape> shapes)
        {
            List<Vector> path = new List<Vector>();
            foreach (Shape item in shapes)
            {
                path.Add(item.Position);
            }
            return path;
        }
        public static List<Vector> RandomizePath(List<Vector> path)
        {
            return path.OrderBy(a => Guid.NewGuid()).ToList();;
        }
    }
}
