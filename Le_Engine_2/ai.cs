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
    class ai : Le_Engine
    {
        public ai() : base(new Vector(1000, 500), "Test", false) { }
   
        public override void OnLoad()
        {
            g = new Shape(new Vector(500, 250), new Vector(50, 50), "d", Type.Circle);
            g.IsGravityObject = true;
            g.Mass = 60;
            p = new Shape(new Vector(400, 150), new Vector(50, 50), "d", Type.Circle);
            p.IsAfectedByGravity = true;
          
            p.Velocity.X = 10;
        }
        Shape g;
        Shape p;
        public override void OnUpdate()
        {

        }
    }
}
