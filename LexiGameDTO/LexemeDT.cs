using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using LexiGame.BLL;

namespace LexiGame.DTO
{
    [Table(Name = "Lexims")]
    public class LexemeDT
    {
        private int _id;
        private int _tid;
        private string _word;
        private byte[] _pictArray;
        private byte[] _audioArray;

        public LexemeDT()
        {
        }
        public LexemeDT(Lexeme lexem)
        {
            this._id = lexem.ID;
            this._tid = lexem.ParentThemeID;
            this._word = lexem.Word;
            this.Picture = lexem.Picture;
            this.AudioStream = lexem.Sound;
        }

        [Column(Name = "id", IsPrimaryKey = true, IsDbGenerated = true)]
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
        [Column(Name = "tid")]
        public int TID
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
        [Column(Name = "name")]
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
       
        [Column(Name = "image")]
        public byte[] PictArray
        {
            get
            {
                return _pictArray;
            }
            set
            {
                _pictArray = value;
            }
        }
        public Bitmap Picture
        {
            get
            {
                if (_pictArray == null)
                    return null;
                else
                {
                    Stream stream = new MemoryStream(_pictArray);
                    return new Bitmap(stream);
                }
            }
            set
            {
                Stream stream = new MemoryStream();
                value.Save(stream, ImageFormat.Jpeg);
                stream.Position = 0;
                byte[] streamBytes = new byte[stream.Length];
                stream.Read(streamBytes, 0, Convert.ToInt32(stream.Length));
                _pictArray = streamBytes;
            }
        }

        [Column(Name = "audio")]
        public byte[] AudioArray
        {
            get
            {
                return _audioArray;
            }
            set
            {
                _audioArray = value;
            }
        }
        public Stream AudioStream
        {
            get
            {
                if (_audioArray == null)
                    return null;
                else
                {
                    return new MemoryStream(_audioArray);
                }
            }
            set
            {
                value.Position = 0;
                byte[] streamBytes = new byte[value.Length];
                value.Read(streamBytes, 0, Convert.ToInt32(value.Length));
                _audioArray = streamBytes;
            }
        }
       
    }
}
