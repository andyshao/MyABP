using System;
using System.Collections.Generic;

namespace MyABP.PlugIns
{
    public interface IPlugInSource
    {
        List<Type> GetModules();
    }
}