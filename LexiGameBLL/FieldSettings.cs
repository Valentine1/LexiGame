using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexiGame.BLL
{
    public static class FieldSettings
    {
        private static int _fieldWidth;
        public static int FieldWidth
        {
            get
            {
                return _fieldWidth;
            }
        }
        private static int _fieldheight;
        public static int FieldHeight
        {
            get
            {
                return _fieldheight;
            }
        }
        private static int _pictureWidth;
        public static int PictureWidth
        {
            get
            {
                return _pictureWidth;
            }
        }
        private static int _pictureHeight;
        public static int PictureHeight
        {
            get
            {
                return _pictureHeight;
            }
        }
        public static int ColumnsNumbers
        {
            get
            {
                return FieldWidth / PictureWidth;
            }
        }
        public static int RowNumbers
        {
            get
            {
                return FieldHeight / PictureHeight;
            }
        }
        static FieldSettings()
        {
            _fieldWidth = 640;
            _pictureWidth = 80;
            _fieldheight = 420;
            _pictureHeight = 60;
        }

      
    }
}
