using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Input;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.VisualStyles;
using Le_Engine_2.Engine.Classes;
using System.Drawing.Drawing2D;

namespace Le_Engine_2.Engine
{
    public class Canvas : Form
    {
        public Canvas()
        {
            this.DoubleBuffered = true; 
        }
        
    }

    
    public abstract class Le_Engine
    {
        public static Vector ScreanSize = new Vector(512, 512);
        private string Title = "Undefined";
        public static Canvas Window = null;
        private Thread GameLoopThead = null;
        public static Color BackgroundColor = Color.White;
        public static List<Shape> RenderStack = new List<Shape>();
        public static List<Line> LineRenderStack = new List<Line>();
        public static List<Text> TextRenderstack = new List<Text>();
        public static bool MouseClick = false;
        public static Vector MousePosition = new Vector(0, 0);
        public static Vector CameraPosition = new Vector(0, 0);
        public static Vector CameraZoom = new Vector(10, 10);
        public static bool EnableGravity = false;
        public static double GravityStrength = 1;
        public static bool GlobalIlumination = true;
        public static int GlobalIluminationFactor = 100;
        public static bool W, A, S, D,E,Q;
        public static bool Up, Down, Left, Right;
        private static bool UW, UA, US, UD;
        public enum Keys {W,A,S,D}
        public enum Type {Qaud, Circle, Sprite}
        public  static long GameTime = 1;
        private static bool GetSearchStatus = false;
        public static Form1 debug = new Form1();
        public static bool Start = false;
        public static bool Step = false;
        private static int CurrentId = 0;
        private Vector WindowZoom = new Vector(0,0);
        public bool AutoZoom = false;
        public double DeltaTime = 0;

        public Le_Engine(Vector Screansize, string Title, bool ShowDebugPanel)
        {
            ScreanSize = Screansize;
            this.Title = Title;
            if (ShowDebugPanel == true)
            {
                debug.Show();
            }
            else Start = true;
            Window = new Canvas();
            Window.TopMost = true;
            Window.Size = new Size((int)ScreanSize.X + 15, (int)ScreanSize.Y + 36);
            Window.StartPosition = FormStartPosition.Manual;
            WindowZoom = new Vector(Window.Width / 10, Window.Height / 10);
            Window.Text = this.Title;
            Window.Paint += Renderer;
            Window.MouseDown += MouseDown;
            Window.MouseUp += MouseUp;
            GameLoopThead = new Thread(GameLoop);
            GameLoopThead.SetApartmentState(ApartmentState.STA);
            GameLoopThead.Start();

            Application.Run(Window);
        }
        private void MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseClick = true;
            }
        }
        private void MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MouseClick = false;
        }
        public static void RegisterShape(Shape Shape)
        {
            Shape.ID = CurrentId;
            CurrentId++;
            RenderStack.Add(Shape);
            RenderStack = RenderStack.OrderBy(shape => shape.Z).ToList();
        }

        public static Color AddLighting(Color c)
        {
            int r = c.R;
            int g = c.G;
            int b = c.B;
            r = (r - GlobalIluminationFactor >= 0) ? r - GlobalIluminationFactor : 0;
            g = (g - GlobalIluminationFactor >= 0) ? g - GlobalIluminationFactor : 0;
            b = (b - GlobalIluminationFactor >= 0) ? b - GlobalIluminationFactor : 0;
            r = (r > 255) ? 255 : r;
            g = (g > 255) ? 255 : g;
            b = (b > 255) ? 255 : b;
            return Color.FromArgb(r,g,b);
        }
        public static Color AddLighting(Color c, int Intensity)
        {
            int r = c.R;
            int g = c.G;
            int b = c.B;
            r = (r - Intensity >= 0) ? r - Intensity : 0;
            g = (g - Intensity >= 0) ? g - Intensity : 0;
            b = (b - Intensity >= 0) ? b - Intensity : 0;
            r = (r > 255) ? 255 : r;
            g = (g > 255) ? 255 : g;
            b = (b > 255) ? 255 : b;
            return Color.FromArgb(r, g, b);
        }
        public static void UpdateZ()
        {
            RenderStack = RenderStack.OrderBy(shape => shape.Z).ToList();
        }
        public static void UnRegisterShape(Shape Shape)
        {
            if (Shape != null)
            {
                RenderStack.Remove(Shape);
            }
        }
        public static void DestroyAll()
        { 
            RenderStack.Clear();
            TextRenderstack.Clear();
            LineRenderStack.Clear();

        }
        public static void RegisterLine(Line line)
        {
            if (line != null)
            {
                LineRenderStack.Add(line);
            }
            else
            {
                Console.WriteLine("ewew");
            }
        }
        public static void RegisterText(Text text)
        {
            TextRenderstack.Add(text);
        }
        public static void UnRegisterText(Text text)
        {
            TextRenderstack.Remove(text);
        }
        public static List<Shape> GetShapes(string Tag)
        {
            if (GetSearchStatus == false)
            {
                GetSearchStatus = true;
                List<Shape> found = new List<Shape>();
                foreach (Shape s in RenderStack)
                {
                    if (s.Tag == Tag)
                    {
                        found.Add(s);
                    }
                }
                GetSearchStatus = false;
                return found;
            }
            else
            {
                return new List<Shape>();
            }
        }
        public static Shape GetShape(string Tag)
        {
            if (GetSearchStatus == false)
            {
                GetSearchStatus = true;
                List<Shape> found = new List<Shape>();
                foreach (Shape s in RenderStack)
                {
                    if (s.Tag == Tag)
                    {
                        found.Add(s);
                    }
                }
                GetSearchStatus = false;
                if (found.Count != 0)
                {
                    return found[0];
                }
                else { return null; }
            }
            else
            {
                return null;
            }
        }
        public static void Dispose()
        {
            LineRenderStack.Clear();
            RenderStack.Clear();
            TextRenderstack.Clear();
            for (int x = 0; x < 38; x++)
            {
                for (int y = 0; y < 21; y++)
                {
                    Shape w = new Shape(new Vector(50 * x, 50 * y), new Vector(50, 50), "c", Type.Qaud);
                    w.color = BackgroundColor;
                    w.Z = -100000;
                }
            }

        }
        public static Shape GetShapeById(int ID)
        {
            foreach (Shape item in RenderStack)
            {
                if(ID == item.ID)
                {
                    return item;
                }
            }
            return null;
        }
        public static bool OncePerClick(Keys k)
        {
            if (W == true && UW == true && k == Keys.W)
            {
                UW = false;
                return true;
            }
            else if(A == true && UA == true && k == Keys.A)
            {
                UA = false;
                return true;
            }
            else if (S == true && US == true && k == Keys.S)
            {
                US = false;
                return true;
            }
            else if (D == true && UD == true && k == Keys.D)
            {
                UD = false;
                return true;
            }
            else return false;
        }
        List<Shape> RenderStack2;
        List<Line> LineRenderStack2;
        List<Text> TextRenderStack2;
        private void Renderer(object sender, PaintEventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            if (AutoZoom) CameraZoom = new Vector(Window.Width / WindowZoom.X, Window.Height / WindowZoom.Y);
            Window.Text = "[RenderFPS: " + Math.Round(1000 / RenderTime, 2) + "] [ FPS: " + Math.Round(1000 / FrameTime, 2) + "] [ DeltaTime: " + DeltaTime + "]";
            Window.Update();
            MousePosition.X = Window.PointToClient(System.Windows.Forms.Cursor.Position).X;
            MousePosition.Y = Window.PointToClient(System.Windows.Forms.Cursor.Position).Y;
            Graphics g = e.Graphics;
            Color backColor = (GlobalIlumination) ? BackgroundColor : AddLighting(BackgroundColor);
            g.Clear(backColor);
            g.TranslateTransform((int)CameraPosition.X, (int)CameraPosition.Y);
            g.ScaleTransform(((float)CameraZoom.X) / 10, ((float)CameraZoom.Y) / 10);
            try
            {
                RenderStack2 = new List<Shape>(RenderStack);
                LineRenderStack2 = new List<Line>(LineRenderStack);
                TextRenderStack2 = new List<Text>(TextRenderstack);
                foreach (Shape item in RenderStack2)
                {
                    
                    if (item.type == Type.Qaud)
                    {
                        g.FillRectangle(new SolidBrush((!GlobalIlumination && !item.HasBeenLighted) ? AddLighting(item.color) : item.color),
                            (int)item.Position.X, (int)item.Position.Y, (int)item.Scale.X, (int)item.Scale.Y);
                    }
                    if (item.type == Type.Circle)
                    {
                        g.FillEllipse(new SolidBrush((!GlobalIlumination && !item.HasBeenLighted) ? AddLighting(item.color) : item.color), (int)item.Position.X, (int)item.Position.Y, (int)item.Scale.X, (int)item.Scale.Y);
                    }

                    if (item.type == Type.Sprite)
                    {
                        g.DrawImageUnscaledAndClipped(item.Image, new Rectangle((int)item.Position.X, (int)item.Position.Y, (int)item.Scale.X, (int)item.Scale.Y));
                    }
                }
                
                for (int i = 0; i < LineRenderStack2.Count; i++)
                {
                    Line l = LineRenderStack2[i];
                    if (l != null)
                    {
                        g.DrawLine(new Pen(l.color), (int)l.StartPoint.X, (int)l.StartPoint.Y, (int)l.EndPoint.X, (int)l.EndPoint.Y);
                        if (l.LifeTime <= 0)
                        {
                            LineRenderStack.Remove(l);
                        }
                        else
                        {
                            l.LifeTime--;
                        }
                    }
                }
                
                foreach (Text item in TextRenderStack2)
                {
                    g.DrawString(item.text, item.font, new SolidBrush(item.color), (int)item.Position.X, (int)item.Position.Y);
                }
               
            }
            catch (Exception)
            {

            }
            watch.Stop();
            RenderTime = watch.ElapsedMilliseconds;

        }
        private static double RenderTime;
        private static double FrameTime;
        Thread PhysicsThread = null;
        void GameLoop()
        {
            OnLoad();
            PhysicsThread = new Thread(Physics);
            PhysicsThread.SetApartmentState(ApartmentState.STA);
            PhysicsThread.Start();
            for (int x = 0; x < 38; x++)
            {
                for (int y = 0; y < 21; y++)
                {
                    Shape w = new Shape(new Vector(50 * x, 50 * y), new Vector(50, 50), "c", Type.Qaud);
                    w.color = BackgroundColor;
                    w.Z = -100000;
                }
            }

            while (GameLoopThead.IsAlive)
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                try
                {                  
                    #region KeyInput
                    if ((Keyboard.GetKeyStates(Key.W) & KeyStates.Down) > 0)
                    {
                        W = true;
                    }
                    else { W = false; UW = true; }
                    if ((Keyboard.GetKeyStates(Key.A) & KeyStates.Down) > 0)
                    {
                        A = true;
                    }
                    else { A = false; UA = true; }
                    if ((Keyboard.GetKeyStates(Key.S) & KeyStates.Down) > 0)
                    {
                        S = true;
                    }
                    else { S = false; US = true; }
                    if ((Keyboard.GetKeyStates(Key.D) & KeyStates.Down) > 0)
                    {
                        D = true;
                    }
                    else { D = false; UD = true; }
                    E = ((Keyboard.GetKeyStates(Key.E) & KeyStates.Down) > 0) ? true : false;
                    Q = ((Keyboard.GetKeyStates(Key.Q) & KeyStates.Down) > 0) ? true : false;
                    Up = ((Keyboard.GetKeyStates(Key.Up) & KeyStates.Down) > 0) ? true : false;
                    Down = ((Keyboard.GetKeyStates(Key.Down) & KeyStates.Down) > 0) ? true : false;
                    Left = ((Keyboard.GetKeyStates(Key.Left) & KeyStates.Down) > 0) ? true : false;
                    Right = ((Keyboard.GetKeyStates(Key.Right) & KeyStates.Down) > 0) ? true : false;

                    #endregion
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                    if (Start == true || Step == true)
                    {
                        foreach (Shape item in RenderStack)
                        {
                            if (item.OnUpdate != null)
                            {
                                item.OnUpdate(item);
                            }
                            #region Velocity
                            int i1 = 0;
                            double i2 = 0;
                            while (i1 < Math.Abs(item.Velocity.X))
                            {
                                if (!item.components.IsColidedWithColiderObject(new Vector((item.Position.X + item.Velocity.X / Math.Abs(item.Velocity.X)), item.Position.Y), item.Scale))
                                {
                                    item.Position.X += item.Velocity.X / Math.Abs(item.Velocity.X);
                                }
                                else if (item.components.GetColidedObject.IsGravityObject)
                                {
                                    item.Position.X += item.Velocity.X / Math.Abs(item.Velocity.X);
                                }
                                else
                                {
                                    item.Velocity.X = 0;
                                }
                                i1++;
                            }
                            while (i2 < Math.Abs(item.Velocity.Y))
                            {
                                if (!item.components.IsColidedWithColiderObject(new Vector(item.Position.X, (item.Position.Y + item.Velocity.Y / Math.Abs(item.Velocity.Y))), item.Scale))
                                {
                                    item.Position.Y += item.Velocity.Y / Math.Abs(item.Velocity.Y);
                                }
                                else if (item.components.GetColidedObject.IsGravityObject)
                                {
                                    item.Position.Y += item.Velocity.Y / Math.Abs(item.Velocity.Y);
                                }
                                else
                                {
                                    item.Velocity.Y = 0;
                                    item.Position.Y = Math.Round(item.Position.Y);
                                }
                                i2++;
                            }
                            if (item.Velocity.X != 0)
                            {
                                item.Velocity.X += (item.Velocity.X < 0) ? item.Drag : -item.Drag;
                            }
                            if (item.Velocity.Y != 0)
                            {
                                item.Velocity.Y += (item.Velocity.Y < 0) ? item.Drag : -item.Drag;
                            }
                            #endregion
                            #region Gravity
                            if (item.IsAfectedByGravity && EnableGravity)
                            {
                                int i = 0;
                                item.Velocity.Y += GravityStrength;
                                while (i < Math.Abs(item.Velocity.Y))
                                {
                                    if (!item.components.IsColidedWithColiderObject(new Vector(item.Position.X, item.Position.Y + item.Velocity.Y / Math.Abs(item.Velocity.Y)), item.Scale))
                                    {
                                        item.Position.Y += item.Velocity.Y / Math.Abs(item.Velocity.Y);
                                    }
                                    else
                                    {

                                        item.Velocity.Y = 0;

                                    }
                                    i++;
                                }
                            }
                            #endregion
                            #region PlanetGravity
                            if (item.IsGravityObject)
                            {
                                foreach (Shape s in RenderStack)
                                {
                                    if (s.IsAfectedByGravity)
                                    {
                                        double delta = 0.05;
                                        Vector dir = new Vector(item.Position.X - s.Position.X, item.Position.Y - s.Position.Y);
                                        double force = item.GravityStregth * (s.Mass * item.Mass) / (Vector.GetDistance(s.Position, item.Position) * Vector.GetDistance(s.Position, item.Position));
                                        s.Velocity.X += (dir.X * force) * delta;
                                        s.Velocity.Y += (dir.Y * force) * delta;
                                    }
                                }
                            }
                            #endregion
                            #region Lighting
                            if (item.light != null)
                            {
                                foreach (Shape s in RenderStack)
                                {
                                    if (Vector.GetDistance(Vector.GetMiddleLocation(item.Position, item.Scale), Vector.GetMiddleLocation(s.Position, s.Scale)) < item.light.Raduis)
                                    {
                                        if (!s.HasBeenLighted)
                                        {
                                            s.color = AddLighting(s.color, item.light.Intensity);
                                            s.HasBeenLighted = true;
                                        }
                                    }
                                    else if (s.HasBeenLighted)
                                    {
                                        s.color = AddLighting(s.color, -item.light.Intensity);
                                        s.HasBeenLighted = false;
                                    }

                                }
                            }
                            #endregion
                        }
                        OnUpdate();
                        GameTime++;
                        Step = false;
                    }
                    Thread.Sleep(1);                  
                }
                catch (Exception) { }
                watch.Stop();
                FrameTime = watch.Elapsed.TotalMilliseconds;
                DeltaTime = watch.Elapsed.TotalSeconds * 50;
            }
        }
        void Physics()
        {
            while(true)
            {
      
            }
        }
        public abstract void OnLoad();
        public abstract void OnUpdate();
    }
}
