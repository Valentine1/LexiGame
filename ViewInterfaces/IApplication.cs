using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexiGame.View
{
    public interface IApplication
    {
        int RunApplication(IView view);
    }
  
}
