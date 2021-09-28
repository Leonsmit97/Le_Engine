using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Le_Engine_2.Engine.Classes
{
    public static class Room
    {
        public static List<string[,]> Rooms = new List<string[,]>();
        public static int CurrentRoom = 0;
        public static void AddRoom(string[,] room)
        {
            Rooms.Add(room);
        }
        public static string[,] GetCurrentRoom()
        {
            return Rooms[CurrentRoom];
        }
        public static List<Vector> GetTiles(string TileName)
        {
            List<Vector> v = new List<Vector>();
            for (int x = 0; x < GetCurrentRoom().GetLength(1); x++)
            {
                for (int y = 0; y < GetCurrentRoom().GetLength(0); y++)
                {
                    if(GetCurrentRoom()[y, x] == TileName)
                    {
                        v.Add(new Vector(x * 50, y * 50)); 
                    }
                }
            }
            return v;
        }
    }
}
