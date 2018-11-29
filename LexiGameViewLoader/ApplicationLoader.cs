using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace LexiGame.View
{
     public class ApplicationLoader:IApplicationLoader
    {

        public IApplication LoadApplication()
        {
            MyApplication app = new MyApplication();
            return (IApplication)app;
        }

    }
}
