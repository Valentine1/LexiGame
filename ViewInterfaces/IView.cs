using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexiGame.View
{
    public delegate void PlayEnd();
    public interface IView
    {
         void Show();
         bool? ShowDialog();
         void Close();
         IView ViewOwner
         {
             get;
             set;
         }
         void Play(Stream audio,bool notifyEnd);
         event PlayEnd OnPlayEnd;
         void SetDynamicResources();
    }
}
