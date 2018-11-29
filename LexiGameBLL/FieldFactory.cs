using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexiGame.BLL
{
    public class FieldFactory
    {
        public FieldFactory()
        {

        }
        public Field MakeField(int indexStart, int indexEnd)
        {
            List<LexemLine> lexeLines = new List<LexemLine>();
            LineFactory lineFactory = new LineFactory();
            for (int i = 0; i < FieldSettings.RowNumbers; i++)
            {
                LexemLine lexemeLine = lineFactory.MakeLexemLine(indexStart, indexEnd);
                lexemeLine.Y = i * FieldSettings.PictureHeight;
                if (i == FieldSettings.RowNumbers - 1)
                {
                    foreach (LexemView lexim in lexemeLine.LexemViews)
                        lexim.IsAccessible = true;
                }
                lexeLines.Add(lexemeLine);
            }
            Field field = new Field(lexeLines);
            return field;
        }
    }
}
