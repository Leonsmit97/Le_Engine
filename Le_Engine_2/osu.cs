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
    class osu : Le_Engine
    {
        public osu() : base(new Vector(190, 600), "osu", false)
        {

        }
        public override void OnLoad()
        {
            new Shape(new Vector(10, 510), new Vector(80, 50), "y", Type.Qaud);
            new Shape(new Vector(100, 510), new Vector(80, 50), "x", Type.Qaud);
            new Text("X", new Vector(125, 520), Color.White, 20);
            new Text("Y", new Vector(35, 520), Color.White, 20);
            BackgroundColor = Color.Transparent;
        }
        int length = 0;
        bool hasclicked = false;
        int length1 = 0;
        bool hasclicked1 = false;
        public override void OnUpdate()
        {
            if(A)
            {
                length+=2;
                hasclicked = true;
                Shape s = GetShape("y");
                s.color = Color.Gray;
            }
            if(!A)
            {
                Shape s = GetShape("y");
                s.color = Color.Black;
            }
            if(!A && hasclicked)
            {
                hasclicked = false;
                Shape s = new Shape(new Vector(10, 500 - length), new Vector(80, length), "s", Type.Qaud);
                if (s.Scale.Y > 25) s.color = Color.Red;
                if (s.Scale.Y < 25) s.color = Color.Orange;
                if (s.Scale.Y < 20) s.color = Color.Yellow;
                if (s.Scale.Y < 15) s.color = Color.Pink;
                if (s.Scale.Y < 10) s.color = Color.Blue;
                if (s.Scale.Y < 5) s.color = Color.Lime;
                s.Velocity.Y = -2;
                s.Drag = 0.00;
                s.IsGravityObject = true;
                length = 0;
            }
            if (S)
            {
                length1 += 2;
                hasclicked1 = true;
                Shape s = GetShape("x");
                s.color = Color.Gray;
            }
            if(!S)
            {
                Shape s = GetShape("x");
                s.color = Color.Black;
            }
            if (!S && hasclicked1)
            {
                hasclicked1 = false;
                Shape s = new Shape(new Vector(100, 500 - length1), new Vector(80, length1), "s", Type.Qaud);
                if (s.Scale.Y > 25) s.color = Color.Red;
                if (s.Scale.Y < 25) s.color = Color.Orange;
                if (s.Scale.Y < 20) s.color = Color.Yellow;
                if (s.Scale.Y < 15) s.color = Color.Pink;
                if (s.Scale.Y < 10) s.color = Color.Blue;
                if (s.Scale.Y < 5) s.color = Color.Lime;
                s.Velocity.Y = -2;
                s.Drag = 0.00;
                s.IsGravityObject = true;
                length1 = 0;
            }
            foreach (Shape item in RenderStack)
            {
                if (item.Position.Y < -50) item.DestroySelf();
            }
        }
    }
}
