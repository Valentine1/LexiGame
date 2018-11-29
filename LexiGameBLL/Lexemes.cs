using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LexiGame.BLL
{
   public static class Lexemes
    {
       private static Dictionary<int,Lexeme> _lexemeDictionary;
       public static Dictionary<int, Lexeme> LexemeDictionary
       {
           get
           {
               if (_lexemeDictionary == null)
               {
                   _lexemeDictionary = new Dictionary<int, Lexeme>();
               }
               return _lexemeDictionary;
           }
           set
           {
               _lexemeDictionary = value;
               _lexemeCollection = null;
           }
          
       }
       private static  List<Lexeme> _lexemeCollection;
       public static  List<Lexeme> LexemeCollection
       {
           get
           {
               if (_lexemeCollection == null)
               {
                   _lexemeCollection = new List<Lexeme>(Lexemes.LexemeDictionary.Values);
               }
               return _lexemeCollection;
           }
       }

       public static void Clear()
       {
           Lexemes.LexemeDictionary.Clear();
           Lexemes.LexemeCollection.Clear();
           Lexemes._lexemeDictionary = null;
           Lexemes._lexemeCollection = null;
       }
       public static Lexeme MapLexemeById(int id)
       {
           Lexeme lex = (Lexeme)LexemeDictionary[id];
           if (lex == null)
               throw new Exception("No Lexeme with requested id exists.");
           return lex;
       }
    }
}
