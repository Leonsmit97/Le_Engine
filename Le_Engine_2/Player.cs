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
    class Player : Le_Engine
    {
        public Player() : base(new Vector(800, 500), "test", false)
        {
            
        }
        public override void OnLoad()
        {
            Shape planet = new Shape(new Vector(400, 250), new Vector(25, 25), "planet", Type.Circle);
            planet.IsGravityObject = true;
            planet.IsAfectedByGravity = true;
            planet.SetDfaultValues(Physics.Default.Planet);
            Shape Orbit = new Shape(new Vector(300, 200), new Vector(25, 25), "orbit", Type.Circle);
            Orbit.IsAfectedByGravity = true;
            Orbit.color = Color.Red;
            Orbit.Velocity.X = 4;
            //test
        }
        public override void OnUpdate()
        {

        }
    }
}
