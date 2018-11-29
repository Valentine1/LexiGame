using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexiGame.View
{
    interface IViewLoader
    {
        IMainView LoadMainWindow();
    }
}
