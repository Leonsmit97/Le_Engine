using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace Le_Engine_2.Engine.Classes
{
    public static class Prefabs
    {
        public static void PlayerControler(Shape Player, int Speed)
        {
            int i = 0;
            while (i < Speed)
            {
                if (Le_Engine.D && !Player.components.IsColidedWithColiderObject(new Vector(Player.Position.X + 1, Player.Position.Y), Player.Scale))
                {
                    Player.Position.X += 1;
                }
                if (Le_Engine.A && !Player.components.IsColidedWithColiderObject(new Vector(Player.Position.X - 1, Player.Position.Y), Player.Scale))
                {
                    Player.Position.X -= 1;
                }
                if (Le_Engine.S && !Player.components.IsColidedWithColiderObject(new Vector(Player.Position.X, Player.Position.Y + 1), Player.Scale))
                {
                    Player.Position.Y += 1;
                }
                if (Le_Engine.W && !Player.components.IsColidedWithColiderObject(new Vector(Player.Position.X, Player.Position.Y - 1), Player.Scale))
                {
                    Player.Position.Y -= 1;
                }
                i++;
            }
        }
        public static void PlayerControlerForce(Shape Player, int Speed)
        {
            int i = 0;
            while (i < Speed)
            {
                if (Le_Engine.D)
                {
                    Player.AddForce(new Vector(Speed, 0));
                }
                if (Le_Engine.A)
                {
                    Player.AddForce(new Vector(-Speed, 0));
                }
                if (Le_Engine.S)
                {
                    Player.AddForce(new Vector(0, Speed));
                }
                if (Le_Engine.W)
                {
                    Player.AddForce(new Vector(0, -Speed));
                }
                i++;
            }
        }
        public static void KeepOnScrean(Shape Object)
        {
            if (Object.Position.X < 0)
            {
                Object.Position.X = 0;
                Object.Velocity.X = 0;
            }
            if (Object.Position.X + Object.Scale.X > Le_Engine.ScreanSize.X)
            {
                Object.Position.X = Le_Engine.ScreanSize.X - Object.Scale.X;
                Object.Velocity.X = 0;
            }
            if(Object.Position.Y < 0)
            {
                Object.Position.Y = 0;
                Object.Velocity.Y = 0;
            }
            if(Object.Position.Y + Object.Scale.Y > Le_Engine.ScreanSize.Y)
            {
                Object.Position.Y = Le_Engine.ScreanSize.Y - Object.Scale.Y;
                Object.Velocity.Y = 0;
            }
        }
    }
}