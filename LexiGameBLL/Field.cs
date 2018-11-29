using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexiGame.BLL
{
    public class Field
    {
        private readonly List<LexemLine> _lexemLines;
        public List<LexemLine> LexemLines
        {
            get
            {
                return _lexemLines;
            }
        }
        internal Field(List<LexemLine> lexemLines)
        {
            if (lexemLines == null)
                lexemLines = new List<LexemLine>();
            this._lexemLines = lexemLines;
        }
    }
}
