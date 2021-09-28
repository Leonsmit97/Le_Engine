using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Le_Engine_2.Engine;
using Le_Engine_2.Engine.Classes;

namespace Le_Engine_2
{
    class test3 : Le_Engine
    {
        public test3() : base(new Vector(800,500), "d", false)
        {

        }
        public override void OnLoad()
        {
            string[,] Map = new string[10, 16]
            {   {"f", "f", "f", "f", "f", "f", "f", "f", "f", "f","f", "f", "f", "f", "f", "f" },
                {"f", "e", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "e", "f"},
                {"f", "e", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "e", "f"},
                {"f", "e", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "e", "f"},
                {"f", "e", ".", ".", ".", ".", ".", "p", ".", ".", ".", ".", ".", ".", "e", "f"},
                {"f", "e", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "e", "f"},
                {"f", "e", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "e", "f"},
                {"f", "e", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "e", "f"},
                {"f", "e", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "e", "f"},
                {"f", "f", "f", "f", "f", "f", "f", "f", "f", "f", "f", "f", "f", "f", "f", "f"},
           };
            BackgroundColor = Color.DarkGray;
            Room.AddRoom(Map);
            foreach (Vector i in Room.GetTiles("f"))
            {
                Shape w = new Shape(i, new Vector(50, 50), "wall", Type.Sprite, new Bitmap(@"C:\Users\Leon\Documents\wall.png"));
                //w.HasColider = false;
            }
            foreach (Vector i in Room.GetTiles("p"))
            {
                p = new Shape(i, new Vector(50, 50), "p", Type.Sprite, new Bitmap(@"C:\Users\Leon\Documents\perr.png"));
            }
            foreach (Vector i in Room.GetTiles("e"))
            {
                Shape e = new Shape(i, new Vector(50, 50), "e", Type.Sprite, new Bitmap(@"C:\Users\Leon\Documents\spike.png"));
                e.HasColider = false;
                e.OnUpdate = Move;
            }
            p.HasColider = false;
            p.Z = 10;
            t = new Text("Score: 0", new Vector(60, 60), Color.Black, 20);
            t1 = new Text("Score: 0", new Vector(260, 60), Color.Black, 20);
            Shield = new Shape(new Vector(-100, -100), new Vector(100, 100), "shield", Type.Circle);
            Shield.Z = -1;
            Shield.color = Color.LightBlue;

        }
        Shape Shield;
        Text t;
        Text t1;
        Shape p;
        bool hasclicked = false;
        int score = 0;
        int highscore = 0;
        bool uses = false;
        bool once = true;
        public override void OnUpdate()
        {
            Prefabs.PlayerControler(p, 6);
            t.text = "Score: " + score.ToString();
            t1.text = "HighScore: " + highscore.ToString();
            if (MouseClick)
            {
                hasclicked = true;
            }
            if (!MouseClick && hasclicked)
            {
                hasclicked = false;
                Shape b = new Shape(new Vector(p.Position.X + 25, p.Position.Y + 25), new Vector(10, 10), "bullet", Type.Sprite, new Bitmap(@"C:\Users\Leon\Documents\b.png"));
                b.color = Color.Red;
                b.OnUpdate = iscolidedwithwall;
                Vector dir = Vector.GetDirection(new Vector(MousePosition.X, MousePosition.Y), new Vector(p.Position.X + 25, p.Position.Y + 25));
                b.Velocity.X = dir.X / 10;
                b.Velocity.Y = dir.Y / 10;
            }
            if (GameTime % time == 0)
            {
                Random r = new Random();
                int[] i = new int[] { 700, 50 };
                Shape e = new Shape(new Vector(i[r.Next(0,2)], r.Next(50, 400)), new Vector(50, 50), "e", Type.Sprite, new Bitmap(@"C:\Users\Leon\Documents\spike.png"));
                e.HasColider = false;
                e.OnUpdate = Move;
            }
            if(E && once)
            {
                if(uses)
                {
                    uses = false;
                }
                else
                {
                    uses = true;
                }
                once = false;
            }
            if(!E)
            {
                once = true;
            }
            if(score > 0 && uses)
            {
                Shield.Position.X = p.Position.X - 25;
                Shield.Position.Y = p.Position.Y - 25;
                score--;
            }
            else { Shield.Position = new Vector(-100, -100); }
        }
        int time = 100;
        void iscolidedwithwall(Shape p)
        {

            if (p.HasColided("wall"))
            {
                p.DestroySelf();
                p.components.GetColidedObject = null;
            }
            if (p.IsColided("e"))
            {

            }
            if (p.HasColided("e"))
            {
                
                p.components.GetColidedObject.DestroySelf();
                if(time > 10)
                {
                    time -= 1;
                    
                }
                score++;
                
            }
        }
        void Move(Shape s)
        {
            if (s.IsColided("p"))
            {
                s.DestroySelf();
                time = 100;
                if(score > highscore)
                {
                    highscore = score;
                }
                score = 0;
                DestroyAll();
                OnLoad();
            }
            if (s.IsColided("shield"))
            {
                s.DestroySelf();
            }
            if (!s.components.IsMoving)
            {
                s.MoveTo(p.Position, 1, 1);
            }
        }

    }
}
