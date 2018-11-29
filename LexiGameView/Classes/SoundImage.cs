using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexiGame.View
{
    internal class SoundImage : BitmapImage, IAudioButton
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
                _audioStream = value;
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
