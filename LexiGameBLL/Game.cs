using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexiGame.BLL
{
    public class Game
    {
        private Random rand = new Random();
        private FieldFactory _fieldFactory;
        private FieldFactory FieldFactory
        {
            get
            {
                if (_fieldFactory == null)
                {
                    _fieldFactory = new FieldFactory();
                }
                return _fieldFactory;
            }
        }
        private List<int> _accessibleLeximIDs;
        private List<int> AccessibleLeximIDs
        {
            get
            {
                if (_accessibleLeximIDs == null)
                {
                    _accessibleLeximIDs = new List<int>();
                }
                return _accessibleLeximIDs;
            }
        }
        private int LeximPronouncedID;
        private int LeximIdIndex;
        private Field GameField
        {
            get;
            set;
        }
        ///<summary>
        ///returns the result of ball throw
        ///</summary>
        public ThrowResult GetThrowResult(int x)
        {
            int col = x / FieldSettings.PictureWidth;
            int deltaX = x % FieldSettings.PictureWidth;
            ThrowResult result = new ThrowResult();
            result.Column = col;
            result.Row = -1;
            if (deltaX < 15)
            {
                for (int i = 0; i < this.GameField.LexemLines.Count; i++)
                {
                    if (this.GameField.LexemLines[i].LexemViews[col - 1].IsAccessible)
                    {
                        result.Row = i;
                        result.Column = col - 1;
                        for (int k = i; k < this.GameField.LexemLines.Count; k++)
                        {
                            if (this.GameField.LexemLines[k].LexemViews[col].IsAccessible)
                            {
                                result.Row = k;
                                result.Column = col;
                                break;
                            }
                        }
                        break;
                    }
                }
                if (result.Row == -1)
                {
                    for (int i = 0; i < this.GameField.LexemLines.Count; i++)
                    {
                        if (this.GameField.LexemLines[i].LexemViews[col].IsAccessible)
                        {
                            result.Row = i;
                            result.Column = col;
                            break;
                        }
                    }
                }
            }
            else if (deltaX > 65)
            {
                for (int i = 0; i < this.GameField.LexemLines.Count; i++)
                {
                    if (this.GameField.LexemLines[i].LexemViews[col].IsAccessible)
                    {
                        result.Row = i;
                        result.Column = col;
                        for (int k = i+1; k < this.GameField.LexemLines.Count; k++)
                        {
                            if (this.GameField.LexemLines[k].LexemViews[col+1].IsAccessible)
                            {
                                result.Row = k;
                                result.Column = col+1;
                                break;
                            }
                        }
                        break;
                    }
                }
                if (result.Row == -1)
                {
                    for (int i = 0; i < this.GameField.LexemLines.Count; i++)
                    {
                        if (this.GameField.LexemLines[i].LexemViews[col+1].IsAccessible)
                        {
                            result.Row = i;
                            result.Column = col + 1;
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.GameField.LexemLines.Count; i++)
                {
                    if (this.GameField.LexemLines[i].LexemViews[col].IsAccessible)
                    {
                        result.Row = i;
                        result.Column = col;
                        break;
                    }
                }
            }
            if(result.Row!=-1)
            SetPreviosAccessible(ref result);
            return result;
        }
        private void SetPreviosAccessible(ref ThrowResult result)
        {
            if (LeximPronouncedID == this.GameField.LexemLines[result.Row].LexemViews[result.Column].ID)
            {
                AccessibleLeximIDs.RemoveAt(LeximIdIndex);
                result.HitResult = true;
                this.GameField.LexemLines[result.Row].LexemViews[result.Column].IsVisible = false;
                this.GameField.LexemLines[result.Row].LexemViews[result.Column].IsAccessible = false;
                if (result.Row > 0)
                {
                    this.GameField.LexemLines[result.Row - 1].LexemViews[result.Column].IsAccessible = true;
                    AccessibleLeximIDs.Add(this.GameField.LexemLines[result.Row - 1].LexemViews[result.Column].ID);
                }
            }
        }
        ///<summary>
        ///Get random Lexem ID from Game Field, if no Lexem left returns zero 
        ///</summary>
        public int GetLeximIDtoPronounce()
        {
            if (AccessibleLeximIDs.Count > 0)
            {
                LeximIdIndex = rand.Next(AccessibleLeximIDs.Count);
                LeximPronouncedID = AccessibleLeximIDs[LeximIdIndex];
                return LeximPronouncedID;
            }
            else
            {
                return 0;
            }
        }
        public Field GetField(int indexStart, int indexEnd)
        {
            Field field = FieldFactory.MakeField(indexStart, indexEnd);
            AccessibleLeximIDs.Clear();
            foreach (LexemView lexim in field.LexemLines[field.LexemLines.Count - 1].LexemViews)
            {
                lexim.IsAccessible = true;
                AccessibleLeximIDs.Add(lexim.ID);
            }
            this.GameField = field;
            return field;
        }
    }

}
