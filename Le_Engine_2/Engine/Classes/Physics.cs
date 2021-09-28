using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;
using System.Threading;

namespace Le_Engine_2.Engine
{
    public class Physics
    {
        private Vector velocity = new Vector(0,0);
        public Vector Velocity
        {
            get { return velocity; }
            set { if (value.X < MaxSpeed && value.Y < MaxSpeed && value.X > -MaxSpeed && value.Y > -MaxSpeed) velocity = value;}
        }
        public enum Default {Planet, Spring}
        public Vector Acceleration;
        public int Mass = 20;
        public int MaxSpeed = 5;
        public double Drag = 0.0;
        public bool IsGravityObject;
        public double GravityStregth = 2;

        public void AddForce(Vector Force)
        {
            Velocity.X += Force.X/Mass;
            Velocity.Y += Force.Y/Mass;
        }
        public Vector UpdateSpring(Vector pos, Vector Anchor, double k = 0.8)
        {
            double springForceY = -k * (pos.Y - Anchor.Y);
            double springForceX = -k * (pos.X - Anchor.X);
            double dampingForceY = Drag * Velocity.Y;
            double dampingForceX = Drag * Velocity.X;
            double forceY = springForceY + Mass * GravityStregth - dampingForceY;
            double forceX = springForceX - dampingForceX;
            double accelerationY = forceY / Mass;
            double accelerationX = forceX / Mass;
            Velocity.Y += accelerationY;
            Velocity.X += accelerationX;
            pos.Y += Velocity.Y;
            pos.X += Velocity.X;
            return new Vector(Convert.ToInt32(pos.X), Convert.ToInt32(pos.Y));

        }
        public void SetDfaultValues(Default Type)
        {
            if (Type == Default.Planet)
            {
                Mass = 20;
                MaxSpeed = 5;
                GravityStregth = 2;
                Drag = 0;
            }
            if(Type == Default.Spring)
            {
                GravityStregth = 10;
                Mass = 10;
                Drag = 0.8;
            }
        }
    }
}