using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Le_Engine_2.Engine;
using Le_Engine_2.Engine.Classes;
using System.Media;
using System.Threading;
using System.Windows.Media;

namespace Le_Engine_2
{
    public class platformer : Le_Engine
    {
        public platformer() : base(new Vector(800, 500), "test", false)
        {
        }
        public override void OnLoad()
        {
            //MediaPlayer myPlayer = new MediaPlayer();
            //myPlayer.Open(new System.Uri(@"C:\Users\Leon\Downloads\m.wav"));
            //myPlayer.Play();
            Room.Rooms.Clear();
            string[,] Map = new string[10, 16]
            {   {"f", "f", "f", "f", "f", "f", "f", "f", "f", "f","f", "f", "f", "f", "f", "f" },
                {"f", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f"},
                {"f", ".", ".", ".", ".", ".", ".", ".", "", ".", "1", ".", ".", ".", ".", "f"},
                {"f", ".", ".", ".", ".", ".", ".", ".", "1", ".", ".", ".", ".", ".", ".", "f"},
                {"f", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "e", "f"},
                {"f", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "."},
                {"f", ".", ".", ".", ".", ".", "1", ".", ".", ".", ".", ".", ".", "1", ".", "."},
                {"f", ".", "p", ".", "1", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "s"},
                {"f", ".", "1", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f"},
                {"l", "l", "l", "l", "l", "l", "l", "l", "l", "l","l", "l", "l", "l", "l", "l"},
            };
            string[,] Map1 = new string[10, 16]
            {   {"f", "f", "f", "f", "f", "f", "f", "f", "f", "f","f", "f", "f", "f", "f", "f" },
                {"f", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f"},
                {"f", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f"},
                {"f", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f"},
                {"f", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f"},
                {"f", "p", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "e", "f"},
                {"f", "1", ".", ".", "1", ".", ".", "1", ".", ".", "1", ".", ".", "1", ".", "f"},
                {"f", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "s", "f"},
                {"f", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f"},
                {"l", "l", "l", "l", "l", "l", "l", "l", "l", "l","l", "l", "l", "l", "l", "l"},
            };
            string[,] Map2 = new string[10, 16]
            {   {"f", "f", "f", "f", "f", "f", "f", "f", "f", "f","f", "f", "f", "f", "f", "f" },
                {"f", "p", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f"},
                {"f", "f", "f", "f", "f", "f", "f", "f", "f", "f", "f", "f", "f", "f", ".", "f"},
                {"f", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "e", "f"},
                {"f", ".", "f", "f", "f", "f", ".", "", "f", "f", "f", "f", ".", "f", "f", "f"},
                {"f", ".", ".", ".", ".", ".", "1", "1", ".", ".", ".", ".", "1", ".", ".", "f"},
                {"f", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f"},
                {"f", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f"},
                {"f", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f"},
                {"l", "f", "l", "l", "f", "f", "l", "f", "l", "l","f", "f", "l", "f", "s", "f"},
            };
            string[,] Map3 = new string[10, 16]
            {   {"f", "f", "f", "f", "f", "f", "f", "f", "f", "f","f", "f", "f", "f", "f", "f" },
                {"f", ".", ".", ".", ".", ".", ".", ".", "f", ".", ".", ".", ".", ".", "s", "f"},
                {"f", ".", ".", ".", ".", ".", ".", ".", "f", ".", ".", ".", ".", ".", "e", "f"},
                {"f", ".", ".", ".", "1", ".", ".", ".", "f", ".", ".", ".", ".", ".", ".", "f"},
                {"f", ".", ".", ".", ".", ".", ".", "p", "f", ".", ".", ".", ".", "1", ".", "f"},
                {"f", ".", ".", ".", ".", "l", "1", "1", "f", ".", ".", ".", ".", ".", ".", "f"},
                {"l", "l", "l", ".", ".", ".", ".", ".", ".", ".", ".", ".", "1", ".", ".", "f"},
                {"l", "l", "l", ".", "1", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f"},
                {"l", "l", "l", "l", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f"},
                {"l", "l", "l", "l", "f", "f", "l", "f", "f", "f", "f", "l", "l", "l", "l", "l"},
            };
            string[,] Map4 = new string[10, 16]
            {   {"f", "f", "f", "f", "f", "f", "f", "f", "f", "f","f", "f", "f", "f", "f", "f" },
                {"f", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f"},
                {"f", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f"},
                {"f", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f"},
                {"f", "p", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f"},
                {"f", "1", ".", "1", "1", ".", "1", "1", ".", "1", "1", ".", ".", "1", ".", "f"},
                {"f", ".", "e", ".", ".", "e", ".", ".", "e", ".", ".", "e", "e", ".", "s", "f"},
                {"f", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f"},
                {"f", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f"},
                {"l", "l", "l", "l", "f", "f", "l", "f", "f", "f", "f", "l", "l", "l", "l", "l"},
            };
            string[,] Map5 = new string[10, 16]
            {   {"f", "f", "f", "f", "f", "f", "f", "f", "f", "f","f", "f", "f", "f", "f", "f" },
                {"f", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f"},
                {"f", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f"},
                {"f", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f"},
                {"f", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f"},
                {"f", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f"},
                {"f", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f"},
                {"f", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f"},
                {"f", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "f"},
                {"l", "l", "l", "l", "f", "f", "l", "f", "f", "f", "f", "l", "l", "l", "l", "l"},
            };
            Room.AddRoom(Map);
            Room.AddRoom(Map1);
            Room.AddRoom(Map2);
            Room.AddRoom(Map3);
            Room.AddRoom(Map4);
            Room.AddRoom(Map5);
            foreach (Vector i in Room.GetTiles("f"))
            {
                new Shape(i, new Vector(50, 50), "wall", Type.Sprite, new Bitmap(@"C:\Users\Leon\Documents\wall.png"));
            }
            foreach (Vector i in Room.GetTiles("1"))
            {
                Shape s = new Shape(i, new Vector(50, 50), "wall", Type.Sprite, new Bitmap(@"C:\Users\Leon\Documents\plat.png"));
                
            }
            foreach (Vector i in Room.GetTiles("l"))
            {
                Shape s = new Shape(i, new Vector(50, 50), "lava",Type.Sprite, new Bitmap(@"C:\Users\Leon\Documents\lava.png"));
           
            }
            foreach (Vector i in Room.GetTiles("p"))
            {
                p = new Shape(i, new Vector(50, 50), "p", Type.Sprite, new Bitmap(@"C:\Users\Leon\Documents\perr.png"));
                p.IsAfectedByGravity = true;
            }
            foreach (Vector i in Room.GetTiles("e"))
            {
                e = new Shape(i, new Vector(50, 50), "e", Type.Sprite, new Bitmap(@"C:\Users\Leon\Documents\spike.png"));
                e.HasColider = false;
            }
            foreach (Vector i in Room.GetTiles("s"))
            {
                Shape e1 = new Shape(i, new Vector(50, 50), "s", Type.Sprite, new Bitmap(@"C:\Users\Leon\Documents\por.png"));
               
            }
            
            BackgroundColor = System.Drawing.Color.Gray;
            EnableGravity = true;
            GravityStrength = 0.4;
        }
       
        Shape e;
        Shape p;
        double speed = 4;
        bool left = true;
        public override void OnUpdate()
        {

            if (p.HasColided("lava"))
            {
                p.DestroySelf();
                end();
                SoundPlayer player = new SoundPlayer(@"C:\Users\Leon\Documents\Main\txt_Storage\ex.wav");
                player.Play();
            }
            else if (p.HasColided("s"))
            {
                p.DestroySelf();
                Room.CurrentRoom++;
                SoundPlayer player = new SoundPlayer(@"C:\Users\Leon\Documents\Main\txt_Storage\n.wav");
                player.Play();
                end();
            }
            else if (p.HasColided("e"))
            {
                end();
                SoundPlayer player = new SoundPlayer(@"C:\Users\Leon\Documents\Main\txt_Storage\ex.wav");
                player.Play();
            }
            if (Up)
            {
                Room.CurrentRoom = 4;
                end();
            }
            if (e != null)
            {
                if (e.IsColided("p"))
                {
                    end();
                }
                if (Room.CurrentRoom != 4)
                {
                    if (e.Position.X > 50 && left == true)
                    {
                        e.Position.X -= 8 * DeltaTime;
                    }
                    else
                    {
                        left = false;
                    }
                    if (e.Position.X < 700 && left == false)
                    {
                        e.Position.X += 8 * DeltaTime;
                    }
                    else
                    {
                        left = true;
                    }
                }
                else
                {
                    foreach (Shape s in GetShapes("e"))
                    {
                        s.components.MoveBetween2Points(s, new Vector(s.Position.X, 200), new Vector(s.Position.X, 300), 2);
                        if (s.IsColided("p"))
                        {
                            end();
                        }
                    }
                }
            }
            if (OncePerClick(Keys.W) && p.Velocity.Y == 0)
            {
                p.Velocity.Y = 0;
                p.AddForce(new Vector(0, -150));
                SoundPlayer player = new SoundPlayer(@"C:\Users\Leon\Documents\Main\txt_Storage\jump.wav");
                player.Play();

            }
            if (D)
            {
                Prefabs.PlayerControler(p, (int)speed);
            }
            if (A)
            {
                Prefabs.PlayerControler(p, (int)speed);
            }
        }
        void end()
        {
            //e = null;
           
            DestroyAll();
            OnLoad();
        }
    }
}
