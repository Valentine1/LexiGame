using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexiGame.BLL
{
    public class Lexeme
    {
        private int _id;
        private string _word;
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public string Word

        {
            get
            {
                return _word;
            }
            set
            {
                _word = value;
            }
        }
        private int _tid;
        public int ParentThemeID
        {
            get
            {
                return _tid;
            }
            set
            {
                _tid = value;
            }
        }
        private Bitmap _picture;
        public Bitmap Picture
        {
            get
            {
                return _picture;
            }
            set
            {
                _picture = value;
            }
        }
        private Stream _sound;
        public Stream Sound
        {
            get
            {
                return _sound;
            }
            set
            {
                _sound = value;
            }
         
        }
        public Lexeme(int id, int tid, string word, Bitmap pic, Stream sound)
        {
            this._id = id;
            this._tid = tid;
            this._word = word;
            this._picture = pic;
            this._sound = sound;
        }
    }
}
