using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.OleDb;
using LexiGame.DB;
using LexiGame.BLL;
using LexiGame.DTO;

namespace LexiGameDB_MS
{
    class LexemGateway : ILexemGateway
    {

        private DataContext DBContext;
        private Table<LexemeDT> TblLexemes;
        public LexemGateway(string connectionString)
        {//DataContext dc = new DataContext(@"C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Data\UCL.mdf");

            OleDbConnection connection = new OleDbConnection(connectionString);
            DBContext = new DataContext(connection);
            TblLexemes = DBContext.GetTable<LexemeDT>();
        }
        public int AddLexeme(Lexeme lexem)
        {
            LexemeDT lexemDT = new LexemeDT(lexem);
            TblLexemes.InsertOnSubmit(lexemDT);
            try
            {
                DBContext.SubmitChanges();
            }
            catch (Exception ex)
            {

            }
            return lexemDT.ID;
        }

        public void DeleteLexeme(int id)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, Lexeme> GetLexemes(int themeID)
        {
            throw new NotImplementedException();
        }

        public void UpdateLexeme(LexiGame.BLL.Lexeme lexem)
        {
            throw new NotImplementedException();
        }



        public Lexeme GetLexeme(int id)
        {
            throw new NotImplementedException();
        }


        public List<Lexeme> GetLexemesList(int themeID)
        {
            throw new NotImplementedException();
        }

    }
}
