using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LexiGame.View
{
    public delegate void BitmapImageChanged();
    public interface  IGDIBitmapImage
    {
        Bitmap GDIBitmap
        {
            get;
            set;
        }
        event BitmapImageChanged OnBitmapImageChanged;
    }
}
