using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10_3_Figures
{
    abstract class Action
    {
        abstract public void Undo();
        abstract public void Redo();
    }
}
