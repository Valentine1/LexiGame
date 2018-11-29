using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using LexiGame.BLL;


namespace LexiGame.DTO
{
    [Table(Name = "Themes")]
    public class ThemeDT
    {
        private int _id;
        private string _name;

        public ThemeDT()
        {
        }
        public ThemeDT(Theme theme)
        {
            this._id = theme.ID;
            this._name = theme.Name;
        }
        [Column(Name = "id", IsPrimaryKey = true, IsDbGenerated = true)]
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
        [Column(Name = "name")]
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
    }
}
