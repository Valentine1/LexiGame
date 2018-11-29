using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace LexiGame.BLL
{
    public class LexemLine
    {
        private int y;
        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }
        private readonly List<LexemView> _lexemViews;
        public List<LexemView> LexemViews
        {
            get
            {
                return _lexemViews;
            }
        }

        internal LexemLine(List<LexemView> lexemViews)
        {
            if (lexemViews == null)
                lexemViews = new List<LexemView>();
            this._lexemViews = lexemViews;
        }
      
    }

}
