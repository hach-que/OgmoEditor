using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OgmoEditor.Protogame
{
    /// <summary>
    /// Provides an interface which the Protogame plugin for Ogmo Editor can use to start the game.
    /// </summary>
    public interface IOgmoRunner
    {
        object Start(string level);
    }
}
