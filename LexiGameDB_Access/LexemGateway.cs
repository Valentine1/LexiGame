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
    class LexemGateway : BaseGateway, ILexemGateway
    {
     
        public LexemGateway(string connectionString)
            : base(connectionString)
        {

        }

        #region Add
        public int AddLexeme(Lexeme lexem)
        {
            LexemeDT lexemDT = new LexemeDT(lexem);
            OleDbCommand command = new OleDbCommand();
            FillCommand(command, lexemDT);
            command.CommandText = "INSERT INTO [Lexims] ([tid],[name],[image],[audio])VALUES (?,?,?,?);";
            Connect(command);
            this.ExecuteNonQuery(command, false);
            command.CommandText = "SELECT @@IDENTITY as newID;";
            int id = Convert.ToInt32(this.ExecuteScalar(command, true));
            return id;
        }
        private void FillCommand(OleDbCommand com, LexemeDT lexemDT)
        {
            OleDbParameter p1 = new OleDbParameter("tid", OleDbType.BigInt);
            p1.Value = lexemDT.TID;

            OleDbParameter p2 = new OleDbParameter("name", OleDbType.LongVarWChar);
            p2.Value = lexemDT.Word;

            OleDbParameter p3 = new OleDbParameter("image", OleDbType.Binary);
            p3.Value = lexemDT.PictArray;

            OleDbParameter p4 = new OleDbParameter("audio", OleDbType.Binary);
            p4.Value = lexemDT.AudioArray;

            OleDbParameter p5 = new OleDbParameter("ID", OleDbType.BigInt);
            p5.Value = lexemDT.ID;

            com.Parameters.Clear();
            com.Parameters.Add(p1);
            com.Parameters.Add(p2);
            com.Parameters.Add(p3);
            com.Parameters.Add(p4);
            com.Parameters.Add(p5);
        }
        #endregion

        public void UpdateLexeme(Lexeme lexem)
        {
            OleDbCommand Command = new OleDbCommand();
            LexemeDT lexemDT = new LexemeDT(lexem);
            FillCommand(Command, lexemDT);
            Command.CommandText = "UPDATE Lexims SET [tid]=?,[name]=?,[image]=?,[audio]=? WHERE [id]=?;";
            Connect(Command);
            this.ExecuteNonQuery(Command, true);
        }

        #region Select
        public List<Lexeme> GetLexemesList(int themeID)
        {
            List<LexemeDT> LexemeDTList = SelectLexemesDT(themeID);
            return MapToLexemeList(LexemeDTList);
        }
        public Dictionary<int, Lexeme> GetLexemes(int themeID)
        {
            List<LexemeDT> LexemeDTList = SelectLexemesDT(themeID);
            return MapToLexemeDictionary(LexemeDTList);
        }
        private List<LexemeDT> SelectLexemesDT(int themeID)
        {
            OleDbCommand Command = new OleDbCommand();
            OleDbParameter p1 = new OleDbParameter("tid", OleDbType.BigInt);
            p1.Value = themeID;
            Command.Parameters.Clear();
            Command.Parameters.Add(p1);
            Command.CommandText = "SELECT * FROM Lexims WHERE [tid]=?;";
            Connect(Command);
            OleDbDataReader reader = (OleDbDataReader)this.ExecuteReader(Command, false);
            List<LexemeDT> LexemeDTList = MapToLexemDTList(reader);
            Close();
            return LexemeDTList;
        }
        private List<LexemeDT> MapToLexemDTList(OleDbDataReader reader)
        {
            List<LexemeDT> LexemeDTList = new List<LexemeDT>();
            while (reader.Read())
            {
                LexemeDT lex = MapToLexemDT(reader);
                LexemeDTList.Add(lex);
            }
            return LexemeDTList;
        }
        private Dictionary<int, Lexeme> MapToLexemeDictionary(List<LexemeDT> listDT)
        {
            Dictionary<int, Lexeme> dictionary = new Dictionary<int, Lexeme>();
            foreach (LexemeDT lexDT in listDT)
            {
                Lexeme lex = MapToLexeme(lexDT);
                dictionary.Add(lex.ID, lex);
            }
            return dictionary;
        }
        private List<Lexeme> MapToLexemeList(List<LexemeDT> listDT)
        {
            List<Lexeme> list = new  List<Lexeme>();
            foreach (LexemeDT lexDT in listDT)
            {
                Lexeme lex = MapToLexeme(lexDT);
                list.Add(lex);
            }
            return list;
        }
        #endregion

        public void DeleteLexeme(int id)
        {
            OleDbCommand Command = new OleDbCommand();
            OleDbParameter p1 = new OleDbParameter("ID", OleDbType.BigInt);
            p1.Value = id;
            Command.Parameters.Clear();
            Command.Parameters.Add(p1);
            Command.CommandText = "DELETE FROM [Lexims] where [id]=?;";
            Connect(Command);
            this.ExecuteNonQuery(Command, true);
        }

        public Lexeme GetLexeme(int id)
        {
            OleDbCommand Command = new OleDbCommand();
            OleDbParameter p1 = new OleDbParameter("id", OleDbType.BigInt);
            p1.Value = id;
            Command.Parameters.Clear();
            Command.Parameters.Add(p1);
            Command.CommandText = "SELECT * FROM Lexims WHERE [id]=?;";
            Connect(Command);
            OleDbDataReader reader = (OleDbDataReader)this.ExecuteReader(Command, false);
            if (!reader.Read())
                throw new Exception("There is no word with provided id");
            LexemeDT LexemeDTList = MapToLexemDT(reader);
            Close();
            return MapToLexeme(LexemeDTList);
        }
        private LexemeDT MapToLexemDT(OleDbDataReader reader)
        {
            LexemeDT lex = new LexemeDT(); ;
            lex.ID = Convert.ToInt32(reader["id"]);
            lex.TID = Convert.ToInt32(reader["tid"]);
            lex.Word = reader["name"] != DBNull.Value ? reader["name"].ToString() : string.Empty;
            lex.PictArray = (byte[])reader["image"];
            lex.AudioArray = (byte[])reader["audio"];
            return lex;
        }
        private Lexeme MapToLexeme(LexemeDT lexDT)
        {
            return new Lexeme(lexDT.ID, lexDT.TID, lexDT.Word, lexDT.Picture, lexDT.AudioStream);
        }
    }
}
