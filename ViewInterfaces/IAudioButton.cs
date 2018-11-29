using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace LexiGame.View
{
    public delegate void PlayAudio(Stream audio, bool notifyEnd);
    public delegate void StreamChanged();
    public interface IAudioButton
    {
        Stream AudioStream
        {
            get;
            set;
        }
        PlayAudio Play
        {
            get;
            set;
        }
        event StreamChanged OnStreamChanged;
    }
}
