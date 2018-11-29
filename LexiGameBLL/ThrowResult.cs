using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexiGame.BLL
{
    public struct ThrowResult
    {
        public int Row
        {
            get;
            set;
        }
        public int Column
        {
            get;
            set;
        }
        public bool HitResult
        {
            get;
            set;
        }
    }
}
