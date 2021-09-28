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
    public class test2 : Le_Engine
    {
        public test2() : base(new Vector(800, 500), "pong", false)
        {

        }
        public override void OnLoad()
        {
            p = new Shape(new Vector(100, 100), new Vector(50, 50), "p", Type.Circle);
            p.color = Color.Red;
            BackgroundColor = Color.Gray;
        }
        Shape p;

        List<Vector> angles = new List<Vector>();
        public override void OnUpdate()
        {
            Prefabs.PlayerControler(p, 4);
            p.Position.X += Vector.GetDirection(MousePosition, p.Position).X/10;
            p.Position.Y += Vector.GetDirection(MousePosition, p.Position).Y/10;
            
        }
        
    }
}
