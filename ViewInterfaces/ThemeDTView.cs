using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexiGame.View
{
    public class ThemeDTView
    {
        private int _id;
        private string _name;
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
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public ThemeDTView(int id, string name)
        {
            this._id = id;
            this._name = name;
        }
    }
}
