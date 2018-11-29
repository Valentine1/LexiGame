using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexiGame.BLL
{
    public class Session
    {
        public int LexemesCount
        {
            get
            {
                return Lexemes.LexemeCollection.Count;
            }

        }

        public int Levels
        {
            get
            {
                if (this.LexemesCount == 0)
                {
                    return 0;
                }
                return (int)Math.Ceiling((double)this.LexemesCount / (double)this.Step) + 1;
            }
        }
        private int currentLevel;
        public int CurrentLevel
        {
            get
            {
                return currentLevel;
            }
            set
            {
                value = value < 0 ? 0 : value;
                currentLevel = (value < this.Levels) ? value : this.Levels;
            }
        }
        public Session(int range, int step)
        {
            this.Range = range;
            this.Step = step;
        }
        public int Scores
        {
            get;
            set;
        }
        private int _range;
        public int Range
        {
            get
            {
                return _range;
            }
            set
            {
                _range = value < 3 ? 3 : value;
            }
        }
        private int _step;
        public int Step
        {
            get
            {
                return _step;
            }
            set
            {
                _step = value < 1 ? 1 : value > 3 ? 3 : value;
            }

        }
        public int StartIndex
        {
            get
            {
                if (this.CurrentLevel < this.Levels)
                {
                    return (this.CurrentLevel - 1) * this.Step;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int EndIndex
        {
            get
            {
                if (this.CurrentLevel < this.Levels)
                {
                    return this.StartIndex + this.Range - 1;
                }
                else
                {
                    return this.LexemesCount - 1;
                }
            }
        }
    }
}
