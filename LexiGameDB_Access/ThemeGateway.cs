using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using LexiGame.DB;
using LexiGame.BLL;
using LexiGame.DTO;


namespace LexiGameDB_Access
{
    class ThemeGateway : BaseGateway, IThemeGateway
    {
        private OleDbCommand _command;
        private OleDbCommand Command
        {
            get
            {
                if (_command == null)
                {
                    _command = new OleDbCommand();
                }
                return _command;
            }
        }
        public ThemeGateway(string connectionString)
            : base(connectionString)
        {

        }
       
        #region Add
        public int AddTheme(Theme theme)
        {
            ThemeDT themeDT = new ThemeDT(theme);
            FillCommand(this.Command, themeDT);
            this.Command.CommandText = "INSERT INTO [Themes] ([name])VALUES (?);";
            this.Connect(this.Command);
            this.ExecuteNonQuery(this.Command, false);
            this.Command.CommandText = "SELECT @@IDENTITY as newID;";
            int id = Convert.ToInt32(this.ExecuteScalar(this.Command, true));
            return id;
        }
        private void FillCommand(OleDbCommand com, ThemeDT themeDT)
        {
            OleDbParameter p1 = new OleDbParameter("name", OleDbType.LongVarWChar);
            p1.Value = themeDT.Name;

            OleDbParameter p2 = new OleDbParameter("ID", OleDbType.BigInt);
            p2.Value = themeDT.ID;
            this.Command.Parameters.Clear();
            com.Parameters.Add(p1);
            com.Parameters.Add(p2);
        }
        #endregion

        public void UpdateTheme(Theme theme)
        {
            ThemeDT themeDT = new ThemeDT(theme);
            FillCommand(this.Command, themeDT);
            this.Command.CommandText = "UPDATE Themes SET [name]=? WHERE [id]=?;";
            this.Connect(this.Command);
            this.ExecuteNonQuery(this.Command, true);
        }

        #region Select
        public Dictionary<int, Theme> GetThemes()
        {
            List<ThemeDT> ThemesDTList = SelectLexemesDT();
            return MapToThemeDictionary(ThemesDTList); ;
        }
        public List<Theme> GetThemesList()
        {
            List<ThemeDT> ThemesDTList = SelectLexemesDT();
            return MapToThemeList(ThemesDTList);
        }
        private List<ThemeDT> SelectLexemesDT()
        {
            this.Command.CommandText = "SELECT * FROM Themes;";
            this.Connect(this.Command);
            OleDbDataReader reader = (OleDbDataReader)this.ExecuteReader(this.Command, false);
            List<ThemeDT> ThemesDTList = MapToThemesDTList(reader);
            this.Close();
            return ThemesDTList;
        }
        private List<ThemeDT> MapToThemesDTList(OleDbDataReader reader)
        {
            List<ThemeDT> ThemeDTList = new List<ThemeDT>();
            while (reader.Read())
            {
                ThemeDT theme = new ThemeDT();
                theme.ID = Convert.ToInt32(reader["id"]);
                theme.Name = reader["name"] != DBNull.Value ? reader["Name"].ToString() : "Undefined";
                ThemeDTList.Add(theme);
            }
            return ThemeDTList;
        }

        private List<Theme> MapToThemeList(List<ThemeDT> listDT)
        {
            List<Theme> list = new List<Theme>();
            foreach (ThemeDT themeDT in listDT)
            {
                Theme theme = new Theme(themeDT.ID, themeDT.Name);
                list.Add(theme);
            }
            return list;
        }
        private Dictionary<int, Theme> MapToThemeDictionary(List<ThemeDT> listDT)
        {
            Dictionary<int, Theme> dictionary = new Dictionary<int, Theme>();
            foreach (ThemeDT themeDT in listDT)
            {
                Theme theme = new Theme(themeDT.ID, themeDT.Name);
                dictionary.Add(theme.ID, theme);
            }
            return dictionary;
        }
        #endregion

        public void DeleteTheme(int id)
        {
            OleDbParameter p1 = new OleDbParameter("ID", OleDbType.BigInt);
            p1.Value = id;
            this.Command.Parameters.Clear();
            this.Command.Parameters.Add(p1);
            this.Command.CommandText = "DELETE FROM [Themes] WHERE [id]=?;";
            this.Connect(this.Command);
            this.ExecuteNonQuery(this.Command, true);
        }
        public void DeleteAllThemes()
        {
            this.Command.CommandText = "DELETE FROM [Themes];";
            this.Connect(this.Command);
            this.ExecuteNonQuery(this.Command, true);
        }


       

    }
}
