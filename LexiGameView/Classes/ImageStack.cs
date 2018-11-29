using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.Windows.Input;

namespace LexiGame.View
{
    internal class ImageStack : StackPanel, IImageStack
    {
        public void AddLexim(LeximDTView lexim, PlayAudio play)
        {
            ResourceDictionary resource = ((ResourceDictionary)((MyApplication)Application.Current).MyResources["resSliderStyle"]);
            SoundImage im = new SoundImage();
            this.Children.Add(im);
            IntPtr intPt = lexim.Picture.GetHbitmap();
            BitmapSource bs = Imaging.CreateBitmapSourceFromHBitmap(intPt, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(60, 45));
            im.Style=resource["SoundImage"] as Style;
            im.Source = bs;
            im.AudioStream = lexim.Sound;
            im.Play = play;
            im.MouseEnter += new System.Windows.Input.MouseEventHandler(im_MouseEnter);
            baseWindow.DeleteObject(intPt);
        }

        void im_MouseEnter(object sender, MouseEventArgs e)
        {
            SoundImage im=((SoundImage)sender);
            im.Play(im.AudioStream, false);
        }
    }
}
