using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectSound;
using System.Windows.Interop;
using Mp3Sharp;
using NAudio;
using NAudio.Wave;

namespace LexiGame.Utility
{
    public delegate void PlayEnd();
    public class Mp3Player
    {
        private Mp3Sharp.Mp3Stream _mp3Stream;
        private Mp3Sharp.Mp3Stream MyMp3Sream
        {
            get
            {
                return _mp3Stream;
            }
            set
            {
                _mp3Stream = value;
            }
        }
        static private Microsoft.DirectX.DirectSound.Device dev;
        private bool IsPlaying;
        StreamedMp3Sound ApplicationStreamedSound;
        public Mp3Player()
        {
            IsPlaying = false;
        }
        public static bool TryPlay(Stream audio, IntPtr hwnd)
        {
            try
            {
                Mp3Stream mp3Stream = new Mp3Sharp.Mp3Stream(audio);
                mp3Stream.Position = 0;
                if (mp3Stream.DecodeFrames(1) == 0)
                {
                    throw new Exception("Could not decode");
                }
                Device dev = new Microsoft.DirectX.DirectSound.Device();
                dev.SetCooperativeLevel(hwnd, Microsoft.DirectX.DirectSound.CooperativeLevel.Normal);
                StreamedMp3Sound ApplicationStreamedSound = new StreamedMp3Sound(dev, mp3Stream);
                ApplicationStreamedSound.Play();
                ApplicationStreamedSound.Stop();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void Play(Stream audio, IntPtr hwnd)
        {
            if (!IsPlaying)
            {
                IsPlaying = true;
                _mp3Stream = new Mp3Sharp.Mp3Stream(audio);
                MyMp3Sream.Position = 0;
                dev = new Microsoft.DirectX.DirectSound.Device();
                dev.SetCooperativeLevel(hwnd, Microsoft.DirectX.DirectSound.CooperativeLevel.Normal);
                ApplicationStreamedSound = new StreamedMp3Sound(dev, MyMp3Sream);
                ApplicationStreamedSound.BufferNotification += ApplicationStreamedSound_BufferNotification;
                ApplicationStreamedSound.Play();
              
            }

        }
        public void PlayWithEndNotifacation(Stream audio, IntPtr hwnd)
        {
            Mp3Stream mp3Stream = new Mp3Sharp.Mp3Stream(audio);
            mp3Stream.Position = 0;
            dev = new Microsoft.DirectX.DirectSound.Device();
            dev.SetCooperativeLevel(hwnd, Microsoft.DirectX.DirectSound.CooperativeLevel.Normal);
            ApplicationStreamedSound = new StreamedMp3Sound(dev, mp3Stream);
            ApplicationStreamedSound.BufferNotification += ApplicationStreamedSound_BufferNotificationSendPostback;
            ApplicationStreamedSound.Play();
            
        }

        public void PlayMp3(Stream audio)
        {
           using(Mp3FileReader mp3 = new Mp3FileReader(audio))
           using (WaveStream waveFormatConv = WaveFormatConversionStream.CreatePcmStream(mp3))
           using (BlockAlignReductionStream blockAlignedStream = new BlockAlignReductionStream(waveFormatConv))
            {
                WaveOut waveOut = new WaveOut();
                waveOut.Init(blockAlignedStream);
                waveOut.Play();
              
            }
            
        }

        public event PlayEnd OnPlayEnd;
        void ApplicationStreamedSound_BufferNotification(object sender, BufferNotificationEventArgs e)
        {
            if (e.SoundFinished)
            {
                IsPlaying = false;
            }
        }
        void ApplicationStreamedSound_BufferNotificationSendPostback(object sender, BufferNotificationEventArgs e)
        {
            if (e.SoundFinished)
            {
                IsPlaying = false;
                if (OnPlayEnd != null)
                {
                    OnPlayEnd();
                }
            }
        }
        //  MyMp3Sream.Close();

        //  int numberOfPcmBytesToReadPerChunk =1024;
        //  byte[] buffer = new byte[numberOfPcmBytesToReadPerChunk];

        //  int bytesReturned = -1;
        //  int totalBytes = 0;
        //  MemoryStream memWav = new MemoryStream();
        //  while (bytesReturned != 0)
        //  {
        //      bytesReturned = Mp3Sream.Read(buffer, 0, buffer.Length);
        //      totalBytes += bytesReturned;
        //      memWav.Write(buffer, 0, buffer.Length);
        //  }
        //WaveFormat wFormat=  new WaveFormat();
        //  wFormat.SamplesPerSecond=Mp3Sream.Frequency;
        //  wFormat.BitsPerSample=16;
        //  wFormat.FormatTag = WaveFormatTag.Pcm;
        //  wFormat.Channels=Mp3Sream.ChannelCount;

        //  wFormat.BlockAlign = (short)(wFormat.Channels * (wFormat.BitsPerSample / 8));
        //  wFormat.AverageBytesPerSecond = wFormat.SamplesPerSecond * wFormat.BlockAlign;

        //  memWav.Position = 0;
        //  Microsoft.DirectX.DirectSound.Buffer buf = new Microsoft.DirectX.DirectSound.Buffer(memWav,1,new BufferDescription(wFormat), dev);
        //  buf.Play(0, BufferPlayFlags.Default);
    }
}
