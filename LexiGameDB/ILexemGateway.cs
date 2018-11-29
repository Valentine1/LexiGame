using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LexiGame.BLL;

namespace LexiGame.DB
{
    public interface ILexemGateway
    {
        int AddLexeme(Lexeme lexem);
        void DeleteLexeme(int id);
        void UpdateLexeme(Lexeme lexem);
        Dictionary<int, Lexeme> GetLexemes(int themeID);
        List<Lexeme> GetLexemesList(int themeID);
        Lexeme GetLexeme(int id);
    }
}
