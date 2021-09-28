using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using Le_Engine_2.Engine.Classes;


namespace Le_Engine_2.Engine
{
    public class Shape : Physics, IMoveable, IColision, IPushable
    {
        public Vector Position { get; set; }
        public Vector Scale { get; set; }
        private int z;
        public int Z 
        {
            get { return z; }   
            set { z = value; Le_Engine.UpdateZ(); }  
        }
        public delegate void Update(Shape Object);
        public Update OnUpdate;
        public Components components { get; set; }
        public bool IsAfectedByGravity = false;
        public bool HasColider = true;
        public Color color = Color.Black;
        public bool HasBeenLighted = false;
        public string Tag = String.Empty;
        public Le_Engine.Type type;
        public Light light = null;
        public Image Image;
        public int ID = -1;
        public Shape(Vector Position, Vector Scale, string Tag, Le_Engine.Type type)
        {         
            components = new Components();
            this.Position = Position;
            this.Scale = Scale;
            this.Tag = Tag;
            this.type = type;
            Z = 0;
            Le_Engine.RegisterShape(this);
        }
        public Shape(Vector Position, Vector Scale, string Tag, Le_Engine.Type type, Image image)
        {
            components = new Components();
            this.Position = Position;
            this.Scale = Scale;
            this.Tag = Tag;
            this.type = type;
            Image = image;
            Z = 0;
            Le_Engine.RegisterShape(this);
        }
        public void DestroySelf()
        {
            Le_Engine.UnRegisterShape(this);
        }
        public void CreateSelf()
        {
            Le_Engine.RegisterShape(this);
        }
        public void MoveTo(Vector Position, int speed, int delay)
        {
            components.MoveTo(this, Position, speed, delay);
        }
        public bool IsColided(string tag)
        {
            return components.IsColided(this, tag);
        }
        public bool HasColided(string tag)
        {
            if(components.GetColidedObject != null)
            {
                if(components.GetColidedObject.Tag == tag)
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsInArea(Vector Position1, Vector scale1)
        {
            return components.IsInArea(this, Position1, scale1);
        }
        public bool IsAreaColidedWithTag(string tag, Vector position, Vector scale)
        {
            return components.IsAreaColidedWithTag(tag, position, scale);
        }
        public void IsPushable(string Tag, Vector Speed, Shape Shape)
        {
            components.IsPushable(Tag, Speed, Shape);
        }
    }
}
