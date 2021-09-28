using Le_Engine_2.Engine;
using Le_Engine_2.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Le_Engine_2.Engine.Classes;


namespace Le_Engine_2
{
    public class pong : Le_Engine
    {
        public pong() : base(new Vector(800,500), "pong", false)
        {
        }

        private Shape p1;
        private Shape p2;
        private Shape ball;
        private Text t;
        private Text t1;
        private Text t2;
        public override void OnLoad()
        {
            
            BackgroundColor = Color.Gray;
            p1 = new Shape(new Vector(10, 213), new Vector(10, 80), "p1", Type.Qaud);
            p1.color = Color.White;
            p2 = new Shape(new Vector(780, 213), new Vector(10, 80), "p2", Type.Qaud);
            p2.color = Color.White;
            ball = new Shape(new Vector(400, 260), new Vector(20, 20), "ball", Type.Circle);
            ball.color = Color.White;
            t = new Text("Left Click To Begin", new Vector(270, 200), Color.White, 24);
            t1 = new Text("0", new Vector(362, 10), Color.White, 24);
            t2 = new Text("0", new Vector(410, 10), Color.White, 24);
        }

        private Vector dir = new Vector(-xs, -0);
        private static int xs = 9;
        private static int ys = 8;
        private int ps = 8;
        private bool start = false;
        private int s1 = 0;
        private int s2 = 0;
        private Text win;
        private bool bot = false;
        private bool bot1 = false;
        public override void OnUpdate()
        {
            
            if (A) bot = true;
            if (D) {bot1 = true;
                ball.Position.Y = 100;
            }
            if (s1 == 5 || s2 == 5)
            {
                start = false;
                bot = false;
                bot1 = false;
                s1 = 0;
                s2 = 0;
                ball.DestroySelf();
                p1.DestroySelf();
                p2.DestroySelf();
                t.DestroySelf();
                t1.DestroySelf();
                t2.DestroySelf();
                if (s1 == 5)
                {
                    win = new Text("Left Player Won", new Vector(300, 100), Color.White, 20);
                }
                else
                {
                    win = new Text("Right Player Won", new Vector(300, 100), Color.White, 20);
                }

                OnLoad();
            }
            if (MouseClick)
            {
                start = true;
                t.color = Color.Transparent;
                if(win != null) win.DestroySelf();
            }
            new Line(new Vector(400, 0), new Vector(400, 500), Color.White);
            if (start)
            {
                ball.Position.X += dir.X;
                ball.Position.Y += dir.Y;
                if (ball.Position.Y <= 0) dir.Y = ys;
                if (ball.Position.Y >= 480) dir.Y = -ys;
                if (ball.Position.X >= 800)
                {
                    dir.X = -xs;
                    dir.Y = 0;
                    ball.Position.X = 400;
                    ball.Position.Y = 250;
                    s1++;
                    p1.Position.Y = 213;
                    p2.Position.Y = 213;
                    t1.text = s1.ToString();
                }
                if (ball.Position.X <= 0) 
                {
                    dir.X = -xs;
                    dir.Y = 0;
                    ball.Position.X = 400;
                    ball.Position.Y = 250;
                    s2++;
                    p1.Position.Y = 213;
                    p2.Position.Y = 213;
                    t2.text = s2.ToString();
                }

                if (!bot1)
                {
                    if (W) p1.Position.Y -= ps;
                    if (S) p1.Position.Y += ps;
                }
                else
                {
                    if (ball.Position.Y < p1.Position.Y + 5 && ball.Position.X < 500) p1.Position.Y -= ps;
                    if (ball.Position.Y > p1.Position.Y + 70 && ball.Position.X<500) p1.Position.Y += ps;
                }

                if (!bot && !bot1)
                {
                    if (Up) p2.Position.Y -= ps;
                    if (Down) p2.Position.Y += ps;
                }
                else
                {
                    if (ball.Position.Y < p2.Position.Y + 5 && ball.Position.X > 300) p2.Position.Y -= ps;
                    if (ball.Position.Y > p2.Position.Y + 70 && ball.Position.X>300) p2.Position.Y += ps;
                }

                getcol(p1);
                getcol(p2);
            }
        }

        private void getcol(Shape player)
        {
            if (player.IsColided("ball"))
            {
                if (ball.Position.Y + 10 >= player.Position.Y && ball.Position.Y +10 <= player.Position.Y + 27)
                {
                    dir.Y = -ys;
                    dir.X = (player.Tag == "p1") ? xs : -xs;
                }
                if (ball.Position.Y +10 >= player.Position.Y + 27 && ball.Position.Y +10<= player.Position.Y + 27 * 2)
                {
                    if (!bot1)
                    {
                        dir.Y = 0;
                    }
                    else
                    {
                        Random R = new Random();
                        int i = R.Next(2);
                        if (i == 0)
                        {
                            dir.Y = -ys;
                        }
                        else
                        {
                            dir.Y = +ys;
                        }
                    }
                    dir.X = (player.Tag == "p1") ? xs : -xs;
                }
                if (ball.Position.Y  + 10 >= player.Position.Y + 27*2 && ball.Position.Y +10 <= player.Position.Y + 27 * 3)
                {
                    dir.Y = ys;
                    dir.X = (player.Tag == "p1") ? xs : -xs;
                }
            }
        }
    }
}