using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace IProject
{
    public interface IPlugin
    {
        string PlugName { get; }

        Control ContentControl { get; }
    }
}
