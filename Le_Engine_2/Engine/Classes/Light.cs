using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Le_Engine_2.Engine.Classes
{
    public class Light
    {
        public Light(int raduis, int intensity)
        {
            Raduis = raduis;
            Intensity = intensity;
        }

        public int Raduis { get; set; }
        public int Intensity { get; set; }
    }
}
