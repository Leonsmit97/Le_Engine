using System.Drawing;
using Le_Engine_2.Engine;
using Le_Engine_2.Engine.Classes;

namespace Le_Engine_2
{
    public class gravity : Le_Engine
    {
        public gravity() : base(new Vector(800, 500), "test", false)
        {
        }

        private Shape player;
        public override void OnLoad()
        {
            string[,] Map = new string[10, 16]
            { {"f", "f", "f", "f", "f", "f", "f", "f", "f", "f","f", "f", "f", "f", "f", "f" },
                {"f", ".", ".", ".", "", ".", "", "", "", ".",".", ".", "f", "f", "f", "f"},
                {"f", ".", ".", ".", ".", ".", ".", "", "", ".",".", ".", ".", ".", ".", "f"},
                {"f", ".", ".", ".", "", "", "", "", "e", ".",".", ".", "f", "f", ".", "f"},
                {"f", ".", ".", ".", "", "", ".", "", "", ".", ".", ".", ".", "f", ".", "f"},
                {"f", ".", ".", ".", ".", "", ".", "", "", ".", ".", ".", ".", ".", ".", "f"},
                {"f", ".", ".", ".", ".", ".", "", "", "", ".",".", ".", "f", "f", "f", "f"},
                {"f", ".", ".", ".", ".", ".", ".", "", ".", ".", ".", ".", "", "f", "f", "f"},
                {"f", "p", ".", ".", ".", ".", ".", ".", ".", ".",".", ".", ".", "f", "f", "f"},
                {"f", "f", "f", "f", "f", "f", "f", "f", "f", "f","f", "f", "f", "f", "f", ""},
            };
            string[,] Map2 = new string[10, 16]
            { {"f", "f", "f", "f", "f", "f", "f", "f", "f", "f","f", "f", "f", "f", "f", "f" },
                {"f", ".", ".", "f", "", ".", "", "", "", ".",".", ".", "f", "f", "f", "f"},
                {"f", ".", ".", "f", ".", ".", ".", "", "", ".",".", ".", ".", ".", ".", "f"},
                {"f", ".", ".", "f", "", "", "", "", ".", ".",".", ".", "f", "f", ".", "f"},
                {"f", ".", ".", "f", "", "", "f", "", "", ".", ".", ".", ".", "f", ".", "f"},
                {"f", ".", ".", "f", ".", "", "f", "", "", ".", ".", ".", ".", ".", "e", "f"},
                {"f", ".", ".", ".", ".", ".", "f", "", "", ".",".", ".", "f", "f", "f", "f"},
                {"f", ".", ".", "f", ".", ".", "f", "", ".", ".", ".", ".", "", "f", "f", "f"},
                {"f", "p", ".", "f", ".", ".", "f", ".", ".", ".",".", ".", ".", "f", "f", "f"},
                {"f", "f", "f", "f", "f", "f", "f", "f", "f", "f","f", "f", "f", "f", "f", ""},
            };
            Room.AddRoom(Map);
            Room.AddRoom(Map2);
            foreach (Vector i in Room.GetTiles("e"))
            {
                p = new Shape(i, new Vector(10, 10), "p", Type.Qaud);
                p.IsAfectedByGravity = true;
                p.color = Color.Green;
            }
            foreach (Vector i in Room.GetTiles("f"))
            {
                new Shape(i, new Vector(50, 50), "c", Type.Qaud);
            }
           


            g = new Shape(new Vector(MousePosition.X, MousePosition.Y), new Vector(40, 40), "g", Type.Circle);
            g.IsGravityObject = true;
            //g1 = new Shape(new Vector(MousePosition.X, MousePosition.Y), new Vector(40, 40), "g", Type.Circle);
            //g1.IsGravityObject = true;
            GravityStrength = 0.8;
            GlobalIluminationFactor = 155;
            //GlobalIlumination = false;
            //EnableGravity = true;
            p.Velocity.X = 5.7;
            p.Z = 34;
            p.Tag = "pp";
            g.Position.X = 400;
            g.Position.Y = 250;
            //g1.Position.X = 300;
            //g1.Position.Y = 300;
            p.light = new Light(100, 100);
        }

        private Shape g;
        Shape g1;
        private Shape p;
        Physics ph = new Physics();
        public override void OnUpdate()
        {
            
            //Line.TurnShapeIntoLineHitbox(p);
            //Prefabs.PlayerControler(p, 3);
            Line l = new Line(new Vector(p.Position.X, p.Position.Y), new Vector(p.Position.X + 1, p.Position.Y), Color.Black);
            l.LifeTime = 3000;
            Line l1 = new Line(new Vector(p.Position.X + p.Scale.X/2, p.Position.Y + p.Scale.Y/2), new Vector((p.Velocity.X*10) + p.Position.X, (p.Velocity.Y * 10) + p.Position.Y), Color.Red);
            //Prefabs.PlayerControlerForce(p, 5);
            if(MouseClick)
            {
                p.Position.X = MousePosition.X;
                p.Position.Y = MousePosition.Y;
                //p.Velocity = new Vector(2, 0);
                LineRenderStack.Clear();
            }
            if(W)
            {
                //g1.Position.X = MousePosition.X;
                //g1.Position.Y = MousePosition.Y;
            }
            if(OncePerClick(Keys.A))
            {
                Room.CurrentRoom = 1;
                Dispose();
                OnLoad();
            }
            if (OncePerClick(Keys.D))
            {
                Room.CurrentRoom = 0;
                Dispose();
                OnLoad();
            }
            //use indevidauls lights and then change surounding objects lights
        }
    }
}