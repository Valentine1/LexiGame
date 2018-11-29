using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexiGame.View
{
    public struct ThrowResultDT
    {
        private int _row;
        public int Row
        {
            get
            {
                return _row;
            }
            set
            {
                _row = value;
            }
        }
        private int _col;
        public int Column
        {
            get
            {
                return _col;
            }
            set
            {
                _col = value;
            }
        }
        private bool _hitResult;
        public bool HitResult
        {
            get
            {
                return _hitResult;
            }
            set
            {
                _hitResult = value;
            }
        }
        public ThrowResultDT(int row, int col, bool hitResult)
        {
            this._row = row;
            this._col = col;
            this._hitResult = hitResult;
        }
    }
}
