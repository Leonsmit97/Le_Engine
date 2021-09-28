using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Le_Engine_2.Engine
{
    public static class Log
    {
        
        public static void log(string Message, ConsoleColor c)
        {
            Console.ForegroundColor = c;
            Console.WriteLine(Message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void log(string Message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        private static int FrameCounter = 0;
        public static void FrameCount()
        {
            FrameCounter++;
        }
        private static int fps = 0;
        public static void FPS()
        {
            fps++;
        }
        public static void StartFrameCounter()
        {
            
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }
        private static void OnTimedEvent(object sender, EventArgs eventArgs)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Frames Per Second: [" + fps + "]");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Form1.fps = fps;
            fps = 0;
        }
    }
}
