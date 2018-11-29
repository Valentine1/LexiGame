using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexiGame.BLL
{
    public class LexemView
    {
        private int _id;
        public int ID
        {
            get
            {
                return _id;
            }
        }
        private bool _isVisible;
        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                _isVisible = value;
            }
        }
        private bool _isAccessible;
        public bool IsAccessible
        {
            get
            {
                return _isAccessible;
            }
            set
            {
                _isAccessible = value;
            }
        }
        private int _x;
        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }
        public Lexeme Lexem
        {
            get
            {
               return  Lexemes.MapLexemeById(this.ID);
            }
        }
        public LexemView(int id, int x)
        {
            this._id = id;
            this._x = x;
        }
        public LexemView(int id, int x, bool isVisible, bool isAccessible)
        {
            this._id = id;
            this._x = x;
            this._isVisible = isVisible;
            this._isAccessible = isAccessible;
        }
    }
}
