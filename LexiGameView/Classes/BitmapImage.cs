using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace LexiGame.View
{
    internal class BitmapImage : System.Windows.Controls.Image, IGDIBitmapImage
    {
        private Bitmap _gdiBitmap;
        public Bitmap GDIBitmap
        {
            get
            {
                return _gdiBitmap;
            }
            set
            {
                _gdiBitmap = value;
                if (OnBitmapImageChanged != null)
                {
                    OnBitmapImageChanged();
                }
            }
        }

        public event BitmapImageChanged OnBitmapImageChanged;
    }
}
