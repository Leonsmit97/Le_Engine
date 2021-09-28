using Le_Engine_2.Engine;
using Le_Engine_2.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using Le_Engine_2.Engine.Classes;

namespace Le_Engine_2
{
    public class test : Le_Engine
    {
        public test() : base(new Vector(800, 500), "test", false)
        {
        }
        //indevidaul lighting of shapes
        //shapes
        public override void OnLoad()
        {
            p = new Shape(new Vector(120, 250), new Vector(20, 20), "player", Type.Circle);
            p.color = Color.Red;
            p2 = new Shape(new Vector(700, 250), new Vector(20, 20), "player2", Type.Circle);
            p2.color = Color.Lime;
            BackgroundColor = Color.Navy;
            
        }
        private Shape p;
        private Shape p2;
        private bool play = true;
        public override void OnUpdate()
        {
            if (play)
            {
                move(p);
                move(p2);
                check(p);
                check(p2);
                foreach (Line l in LineRenderStack)
                {
                    if (p.IsInArea(l.StartPoint, new Vector(2, 2)) && l.color == Color.Lime) 
                    {p.DestroySelf();
                        play = false;
                        Text t = new Text("Green wins", new Vector(300, 250), Color.White, 20);
                        Thread.Sleep(2000);
                        t.DestroySelf();
                    }
                    if (p2.IsInArea(l.StartPoint, new Vector(2, 2)) && l.color == Color.Red) 
                    {p2.DestroySelf();
                        play = false;
                        Text t = new Text("Red wins", new Vector(350, 150), Color.White, 20);
                        Thread.Sleep(2000);
                        t.DestroySelf();
                    }
                }
            }
            else
            {
                p.Position = new Vector(120, 250);
                p.CreateSelf();
                p2.Position = new Vector(700, 250);
                p2.CreateSelf();
                LineRenderStack.Clear();
                play = true;
            }

        }

        void check(Shape s)
        {
            if (s.Position.X < 0) s.Position.X = 0;
            if (s.Position.X > 780) s.Position.X = 780;
            if (s.Position.Y < 0) s.Position.Y = 0;
            if (s.Position.Y > 480) s.Position.Y = 480;
        }
        void move(Shape s)
        {
            Vector p1 = new Vector(s.Position.X + 10, s.Position.Y+10);
            if(s.Tag == "player") 
            {Prefabs.PlayerControler(s, 6);}
            else
            {
                if (Up)
                {
                    s.Position.Y -= 6;
                }
                if (Down)
                {
                    s.Position.Y += 6;
                }
                if (Left)
                {
                    s.Position.X -= 6;
                }
                if (Right)
                {
                    s.Position.X += 6;
                }
            }

            Vector p2 = new Vector(s.Position.X + 10, s.Position.Y + 10);
            Line l = new Line(p1, p2, (s.Tag == "player") ? Color.Red : Color.Lime);
            l.LifeTime = 70;
        }
    }
}