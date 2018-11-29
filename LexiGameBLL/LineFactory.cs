using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexiGame.BLL
{
    public class LineFactory
    {
        private Random rand = new Random();
        protected List<LexemView> MakeLexemViewList(int indexStart, int indexEnd)
        {
            if (indexStart >= Lexemes.LexemeCollection.Count && indexEnd >= Lexemes.LexemeCollection.Count)
                throw new Exception("Collection of Lexims is shorter than index requested");
            List<LexemView> lexemViews = new List<LexemView>();
            for (int i = 0; i < FieldSettings.ColumnsNumbers; i++)
            {
                int index = rand.Next(indexStart, indexEnd + 1);
                index = index < Lexemes.LexemeCollection.Count ? index : index - Lexemes.LexemeCollection.Count;
                int id = Lexemes.LexemeCollection[index].ID;
                LexemView view = new LexemView(id, i * FieldSettings.PictureWidth,true,false);
                lexemViews.Add(view);
            }
            return lexemViews;
        }
        public LexemLine MakeLexemLine(int indexStart, int indexEnd)
        {
            List<LexemView> lexemViews = this.MakeLexemViewList(indexStart, indexEnd);
            LexemLine line = new LexemLine(lexemViews);
            return line;
        }
    }
}
