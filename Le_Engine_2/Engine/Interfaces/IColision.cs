﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Le_Engine_2.Engine
{
    public interface IColision
    {
        Vector Position { get; set; }
        Vector Scale { get; set; }
    }
}
