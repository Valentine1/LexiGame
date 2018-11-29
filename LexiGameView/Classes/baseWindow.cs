using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Interop;
using LexiGame.Utility;

namespace LexiGame.View
{
    public class baseWindow : Window, IView
    {
        private Mp3Player _mp3Player;
        private Mp3Player Mp3Player
        {
            get
            {
                if (_mp3Player == null)
                {
                    _mp3Player = new Mp3Player();
                }
                return _mp3Player;
            }

            set
            {
                _mp3Player = value;
            }
        }

        public baseWindow()
        {
            Mp3Player.OnPlayEnd += Mp3Player_OnPlayEnd;
        }

        void Mp3Player_OnPlayEnd()
        {
            if (OnPlayEnd != null)
            {
                OnPlayEnd();
            }
        }
        protected void AjustLoacationToOwner()
        {
            if (this.Owner != null)
            {
                this.Top = this.Owner.Top + 83;
                this.Left = this.Owner.Left + 3;

            }
        }
        protected void AjustChildWindowLoacation()
        {
            if (this.OwnedWindows.Count > 0)
            {
                foreach (Window win in this.OwnedWindows)
                {
                    win.Top = this.Top + 83;
                    win.Left = this.Left + 3;
                    
                }
            }
        }

        public event PlayEnd OnPlayEnd;
        public void Play(Stream audio, bool notifyEnd)
        {
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            if (notifyEnd)
            {
                Mp3Player.PlayMp3(audio);
            }
            else
            {
                Mp3Player.PlayMp3(audio);
            }
        }

        public void Play(Stream audio)
        {
        }
        public bool TryPlay(Stream audio)
        {
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
           return LexiGame.Utility.Mp3Player.TryPlay(audio, hwnd);
        }
        public IView ViewOwner
        {
            get
            {
                return (IView)this.Owner;
            }
            set
            {
                this.Owner = (baseWindow)value;
            }
        }
        public virtual void SetDynamicResources()
        {
                
        }
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

      
    }
}
