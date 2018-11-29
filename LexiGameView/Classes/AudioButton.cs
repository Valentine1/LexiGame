using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;

namespace LexiGame.View
{
    internal class AudioButton : Button, IAudioButton
    {

        private Stream _audioStream;
        public Stream AudioStream
        {
            get
            {
                return _audioStream;
            }
            set
            {
                _audioStream=value;
                if (OnStreamChanged != null)
                {
                    OnStreamChanged();
                }
            }
        }
        private PlayAudio _play;
        public PlayAudio Play
        {
            get
            {
               return _play;
            }
            set
            {
                _play = value;
            }
        }

        public event StreamChanged OnStreamChanged;
    }
}
