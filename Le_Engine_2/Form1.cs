using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Le_Engine_2.Engine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static int fps;
        public static List<Shape> SaveStack = new List<Shape>();
        bool dock = true;
        Shape selectedshape = null;
        public static Components comps = new Components();
        public static Shape clickedShape;
        public int step = 1;
        bool clickingItem = false;
        bool UpdatePropertiesQ = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            CreateRightClick();
            timer1.Interval = 1000;
            timer1.Start();
            timer2.Interval = 100;
            timer2.Start();
            LoadSavedShapes();
            GetAndMoveClickedShape();
        }
        private void menuItem1_Click(object sender, System.EventArgs e)
        {
            DeleteSelectedShape();
        }
        private void menuItem3_Click(object sender, System.EventArgs e)
        {
            foreach (Shape item in SaveStack)
            {
                item.DestroySelf();
            }
            SaveStack.Clear();
            listBox2.Items.Clear();
            LoadSavedShapes();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (fps != 0)
            {
                label1.Text = "Fps: " + fps.ToString();
                this.chart1.Series["Series1"].Points.AddXY("", fps);
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            DockGameView();
            UpdateProperties();
        }
        private void playToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Le_Engine.Start == false)
            {
                Le_Engine.Start = true;
                playToolStripMenuItem1.Text = "Stop";
            }
            else
            {
                Le_Engine.Start = false;
                playToolStripMenuItem1.Text = "Start";
            }
        }
        private void stepToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Le_Engine.Step = true;
        }
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(-1);
        }     
        private void createQaudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateQaud();
        }
        private void createCircleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateCircle();
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void saveAndQuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
            Application.Exit();
            Environment.Exit(-1);
        }     
        private void button1_Click(object sender, EventArgs e)
        {
            if(dock == true)
            {
                ConsoleLog("Undocked");
                dock = false;
                button1.Text = "Dock";
            }
            else
            {
                ConsoleLog("Docked");
                dock = true;
                button1.Text = "UnDock";
            }
        }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                string item = listBox2.SelectedItem.ToString();
                int id = Convert.ToInt32(item.Substring(item.IndexOf('(')).Replace("(", "").Replace(")", ""));
                selectedshape = Le_Engine.GetShapeById(id);
                ConsoleLog("Shape " + selectedshape.Tag + "Selected");
                PopulateProperties(selectedshape);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if(selectedshape != null)
            {
                ConsoleLog("Transform Saved");
                selectedshape.Position.X = Convert.ToInt32(textBox1.Text);
                selectedshape.Position.Y = Convert.ToInt32(textBox2.Text);
                selectedshape.Scale.X = Convert.ToInt32(textBox4.Text);
                selectedshape.Scale.Y = Convert.ToInt32(textBox3.Text);
                selectedshape.Z = Convert.ToInt32(Ztextbox.Text);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedshape != null)
                {
                    if (Convert.ToInt32(textBox8.Text) < 256 && Convert.ToInt32(textBox8.Text) > -1 && Convert.ToInt32(textBox7.Text) < 256 && Convert.ToInt32(textBox7.Text) > -1 && Convert.ToInt32(textBox5.Text) < 256 && Convert.ToInt32(textBox5.Text) > -1)
                    {

                        ConsoleLog("Color Saved");
                        selectedshape.color = Color.FromArgb(Convert.ToInt32(textBox8.Text), Convert.ToInt32(textBox7.Text), Convert.ToInt32(textBox5.Text));
                    }
                    else ConsoleLog("Number Outside Range");
                }
            }
            catch (Exception) { ConsoleLog("Must Use Only Numbers"); }
        }
        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                step = Convert.ToInt32(toolStripTextBox1.Text);
                ConsoleLog("Step = " + step.ToString());
            }
            catch (Exception) { }           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (selectedshape != null)
            {
                for (int i = 0; i < listBox2.Items.Count; i++)
                {
                    if (listBox2.Items[i].ToString().Contains(label14.Text) == true)
                    {
                        listBox2.Items.RemoveAt(i);
                        listBox2.Items.Insert(i, textBox10.Text + $"  ({selectedshape.ID})");
                    }
                }
                ConsoleLog("Tag Updated");
                selectedshape.Tag = textBox10.Text;
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (Le_Engine.Start == false)
            {
                ConsoleLog("Starting Game...");
                Le_Engine.Start = true;
                button5.Text = "Stop";
            }
            else
            {
                ConsoleLog("Stopping Gmae...");
                Le_Engine.Start = false;
                button5.Text = "Start";
            }
        }
        private void addCircleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void CreateRightClick()
        {
            ContextMenu m = new ContextMenu();
            panel4.ContextMenu = m;
            MenuItem menuItem1 = new MenuItem();
            MenuItem menuItem2 = new MenuItem();
            menuItem1.Text = "Delete Selected Object";
            menuItem2.Text = "Deselect Item";
            m.MenuItems.Add(menuItem1);
            m.MenuItems.Add(menuItem2);
            menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            ContextMenu m1 = new ContextMenu();
            panel3.ContextMenu = m1;
            MenuItem menuItem3 = new MenuItem();
            menuItem3.Text = "Reload";
            m1.MenuItems.Add(menuItem3);
            menuItem3.Click += new System.EventHandler(this.menuItem3_Click);

        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            selectedshape = null;
            clickedShape = null;
            comps.GetColidedObject = null;
        }

        void PopulateProperties(Shape s)
        {
            ConsoleLog("Populating Properties");
            textBox1.Text = s.Position.X.ToString();
            textBox2.Text = s.Position.Y.ToString();
            textBox4.Text = s.Scale.X.ToString();
            textBox3.Text = s.Scale.Y.ToString();
            Ztextbox.Text = s.Z.ToString();
            textBox8.Text = selectedshape.color.R.ToString();
            textBox7.Text = selectedshape.color.G.ToString();
            textBox5.Text = selectedshape.color.B.ToString();
            textBox10.Text = selectedshape.Tag;
            label14.Text = selectedshape.ID.ToString();
        }
        private void LoadSavedShapes()
        {
            foreach (Shape item in StateHandler.LoadData())
            {
                listBox2.Items.Add(item.Tag + $"  ({item.ID})");
                SaveStack.Add(item);
                ConsoleLog("Loaded Saved Shapes");
            }
        }
        private void DeleteSelectedShape()
        {
            if (selectedshape != null)
            {
                int index = listBox2.Items.IndexOf(selectedshape.Tag + $"  ({selectedshape.ID})");
                listBox2.Items.RemoveAt(index);
                SaveStack.Remove(selectedshape);
                selectedshape.DestroySelf();
                selectedshape = null;
            }
        }
        private void DockGameView()
        {
            if (dock == true)
            {
                Point location = panel3.PointToScreen(Point.Empty);
                Le_Engine.Window.StartPosition = FormStartPosition.Manual;
                Le_Engine.Window.Left = location.X +5;
                Le_Engine.Window.Top = location.Y + 45;
            }
        }
        private void Save()
        {
            List<string> FormatedForSave = new List<string>();
            foreach (Shape s in SaveStack)
            {
                FormatedForSave.Add("PositionX " + s.Position.X + " | PositionY " + s.Position.Y + " | ScaleX " + s.Scale.X + " | ScaleY " + s.Scale.Y + " | Tag " + s.Tag + " | Type " + s.type + " | R" + s.color.R + " | G" + s.color.G + " | B" + s.color.B + " | Z" + s.Z);
            }
            StateHandler.SaveData(FormatedForSave);
        }
        private void UpdateProperties()
        {
            if(selectedshape != null && UpdatePropertiesQ == true)
            {
                PopulateProperties(selectedshape);
                UpdatePropertiesQ = false;
            }
        }
        private void CreateQaud()
        {
            Shape qaud = new Shape(new Vector(0, 0), new Vector(50, 50), "GameObject", Le_Engine.Type.Qaud);
            SaveStack.Add(qaud);
            listBox2.Items.Add("GameObject" + $"  ({qaud.ID})");
        }
        private void CreateCircle()
        {
            Shape circle = new Shape(new Vector(0, 0), new Vector(50, 50), "GameObject", Le_Engine.Type.Circle);
            SaveStack.Add(circle);
            listBox2.Items.Add("GameObject" + $"  ({circle.ID})");
        }
        public bool IsAreaColidedWithAnyShape(Vector Position, Vector Scale)
        {
            List<Shape> p = Le_Engine.RenderStack;
            foreach (Shape s in p)
            {
                if (s.Position.Y + s.Scale.Y > Position.Y && Position.Y + Scale.Y > s.Position.Y && s.Position.X + s.Scale.X > Position.X && Position.X + Scale.X > s.Position.X)
                {
                    comps.GetColidedObject = s;
                    return true;
                }
            }
            return false;
        }
        public void GetAndMoveClickedShape()
        {
            Thread th = new Thread(() =>
            {
                while (true)
                {
                    if (Le_Engine.MouseClick == true)
                    {
                        if (IsAreaColidedWithAnyShape(new Vector(Le_Engine.MousePosition.X, Le_Engine.MousePosition.Y), new Vector(1, 1)) && clickingItem == false)
                        {
                            clickedShape = comps.GetColidedObject;
                            selectedshape = clickedShape;
                            clickingItem = true;
                            UpdatePropertiesQ = true;
                        }
                        if (comps.GetColidedObject != null)
                        {
                            UpdatePropertiesQ = true;
                            comps.GetColidedObject.Position.X = Convert.ToInt32(Math.Round(Convert.ToDouble(Le_Engine.MousePosition.X - comps.GetColidedObject.Scale.X / 2) / step) * step);
                            comps.GetColidedObject.Position.Y = Convert.ToInt32(Math.Round(Convert.ToDouble(Le_Engine.MousePosition.Y - comps.GetColidedObject.Scale.Y / 2) / step) * step);
                        }
                    }
                    else
                    {
                        clickingItem = false;
                    }
                }
            });
            th.Start();
        }
        private void ConsoleLog(string Message)
        {
            listBox3.Items.Add(Message);
            listBox3.SelectedIndex = listBox3.Items.Count - 1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(selectedshape != null)
            {
                SaveStack.Remove(selectedshape);
                for (int i = 0; i < listBox2.Items.Count; i++)
                {
                    if (listBox2.Items[i].ToString().Contains(selectedshape.ID.ToString()) == true)
                    {
                        listBox2.Items.RemoveAt(i);
                    }
                }
                selectedshape.DestroySelf();
                selectedshape = null;
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }


    }
}
