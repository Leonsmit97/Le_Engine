using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Le_Engine_2.Engine.Classes
{
    public class Text
    {
        public Vector Position;
        public Color color;
        public string text;
        public Font font;
        public int Size;
        public Text(string Text, Vector position, Color color, int Size)
        {
            Position = position;
            this.color = color;
            this.Size = Size;
            font = new Font("Arial", Size);
            this.text = Text;
            Le_Engine.RegisterText(this);
        }
        public void DestroySelf()
        {
           Le_Engine.UnRegisterText(this);
        }

        public void ShowSelf()
        {
            Le_Engine.RegisterText(this);
        }
    }
}
