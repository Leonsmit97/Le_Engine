using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace Le_Engine_2.Engine
{
    public static class StateHandler
    {
        public static string FilePath = @"C:\Users\Leon\source\repos\Le_Engine_2\Le_Engine_2\Engine\Save.txt";
        public static void SaveData(List<string> Objects)
        {
            File.WriteAllLines(FilePath, Objects.ToArray());
        }
        public static List<Shape> LoadData()
        {
            List<Shape> newshapes = new List<Shape>();
            string[] SaveData = File.ReadAllLines(FilePath);
            foreach (string s in SaveData)
            {
                string[] split = s.Split('|');
                int PosX = Convert.ToInt32(split[0].Replace("PositionX", "").Trim());
                int PosY = Convert.ToInt32(split[1].Replace("PositionY", "").Trim());
                int ScaleX = Convert.ToInt32(split[2].Replace("ScaleX", "").Trim());
                int ScaleY = Convert.ToInt32(split[3].Replace("ScaleY", "").Trim());
                string Tag = split[4].Replace("Tag", "").Trim();
                string Type = split[5].Replace("Type", "").Trim();
                int R = Convert.ToInt32(split[6].Replace("R", "").Trim());
                int G = Convert.ToInt32(split[7].Replace("G", "").Trim());
                int B = Convert.ToInt32(split[8].Replace("B", "").Trim());
                int Z = Convert.ToInt32(split[9].Replace("Z", "").Trim());
                if (Type == "Qaud")
                {
                    Shape s1 = new Shape(new Vector(PosX, PosY), new Vector(ScaleX, ScaleY), Tag, Le_Engine.Type.Qaud);
                    s1.Z = Z;
                    s1.color = Color.FromArgb(R, G, B);
                    newshapes.Add(s1);
                }
                if (Type == "Circle")
                {
                    Shape s1 = new Shape(new Vector(PosX, PosY), new Vector(ScaleX, ScaleY), Tag, Le_Engine.Type.Circle);
                    s1.color = Color.FromArgb(R, G, B);
                    s1.Z = Z;
                    newshapes.Add(s1);
                }
            }
            return newshapes;
        }
    }
}
