using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LexiGame.BLL;
using NUnit.Framework;
using LexiGame.Utility;
using LexiGame.DB;
using LexiGame.BLL;

namespace TestUnit
{
    [TestFixture]
    public class TestLexemeGateway
    {
        [SetUp]
        public void Initialize()
        {
            FileStream pic1 = new FileStream("butterfly.jpg", FileMode.Open, FileAccess.Read);
            FileStream pic2 = new FileStream("tiger.jpg", FileMode.Open, FileAccess.Read);
            FileStream pic3 = new FileStream("beaver.jpg", FileMode.Open, FileAccess.Read);
            FileStream pic4 = new FileStream("crow.jpg", FileMode.Open, FileAccess.Read);
            FileStream pic5 = new FileStream("elk.jpg", FileMode.Open, FileAccess.Read);
            FileStream pic6 = new FileStream("hare.jpg", FileMode.Open, FileAccess.Read);
            FileStream pic7 = new FileStream("horse.jpg", FileMode.Open, FileAccess.Read);
            FileStream pic8 = new FileStream("shark.jpg", FileMode.Open, FileAccess.Read);
            FileStream pic9 = new FileStream("sparrow.jpg", FileMode.Open, FileAccess.Read);
            FileStream pic10 = new FileStream("turkey.jpg", FileMode.Open, FileAccess.Read);
            FileStream pic11 = new FileStream("wildBoar.jpg", FileMode.Open, FileAccess.Read);

            FileStream sound1 = new FileStream("butterfly.mp3", FileMode.Open, FileAccess.Read);
            FileStream sound2 = new FileStream("tiger.mp3", FileMode.Open, FileAccess.Read);
            FileStream sound3 = new FileStream("beaver.mp3", FileMode.Open, FileAccess.Read);
            FileStream sound4 = new FileStream("crow.mp3", FileMode.Open, FileAccess.Read);
            FileStream sound5 = new FileStream("elk.mp3", FileMode.Open, FileAccess.Read);
            FileStream sound6 = new FileStream("hare.mp3", FileMode.Open, FileAccess.Read);
            FileStream sound7 = new FileStream("horse.mp3", FileMode.Open, FileAccess.Read);
            FileStream sound8 = new FileStream("shark.mp3", FileMode.Open, FileAccess.Read);
            FileStream sound9 = new FileStream("sparrow.mp3", FileMode.Open, FileAccess.Read);
            FileStream sound10 = new FileStream("turkey.mp3", FileMode.Open, FileAccess.Read);
            FileStream sound11 = new FileStream("wildBoar.mp3", FileMode.Open, FileAccess.Read);

            Bitmap bit1 = new Bitmap(pic1);
            Bitmap bit2 = new Bitmap(pic2);
            Bitmap bit3 = new Bitmap(pic3);
            Bitmap bit4 = new Bitmap(pic4);
            Bitmap bit5 = new Bitmap(pic5);
            Bitmap bit6 = new Bitmap(pic6);
            Bitmap bit7 = new Bitmap(pic7);
            Bitmap bit8 = new Bitmap(pic8);
            Bitmap bit9 = new Bitmap(pic9);
            Bitmap bit10 = new Bitmap(pic10);
            Bitmap bit11 = new Bitmap(pic11);

            Lexeme lex1 = new Lexeme(1, 2, "butterfly", bit1, sound1);
            Lexeme lex2 = new Lexeme(2, 2, "tiger", bit2, sound2);
            Lexeme lex3 = new Lexeme(3, 2, "beaver", bit3, sound3);
            Lexeme lex4 = new Lexeme(4, 2, "crow", bit4, sound4);
            Lexeme lex5 = new Lexeme(5, 2, "elk", bit5, sound5);
            Lexeme lex6 = new Lexeme(6, 2, "hare", bit6, sound6);
            Lexeme lex7 = new Lexeme(7, 2, "horse", bit7, sound7);
            Lexeme lex8 = new Lexeme(8, 2, "shark", bit8, sound8);
            Lexeme lex9 = new Lexeme(9, 2, "sparrow", bit9, sound9);
            Lexeme lex10 = new Lexeme(10, 2, "turkey", bit10, sound10);
            Lexeme lex11 = new Lexeme(11, 2, "wildBoar", bit11, sound11);

            Lexemes.LexemeDictionary.Clear();
            Lexemes.LexemeDictionary.Add(lex1.ID, lex1);
            Lexemes.LexemeDictionary.Add(lex2.ID, lex2);
            Lexemes.LexemeDictionary.Add(lex3.ID, lex3);
            Lexemes.LexemeDictionary.Add(lex4.ID, lex4);
            Lexemes.LexemeDictionary.Add(lex5.ID, lex5);
            Lexemes.LexemeDictionary.Add(lex6.ID, lex6);
            Lexemes.LexemeDictionary.Add(lex7.ID, lex7);
            Lexemes.LexemeDictionary.Add(lex8.ID, lex8);
            Lexemes.LexemeDictionary.Add(lex9.ID, lex9);
            Lexemes.LexemeDictionary.Add(lex10.ID, lex10);
            Lexemes.LexemeDictionary.Add(lex11.ID, lex11);

        }

        [Test]
        public void TestUtility()
        {
            Settings.UserSettings.Profile.ThemeSelected = "aliluja";
            Settings.UserSettings.Profile.Range = 6;
            Settings.UserSettings.Profile.Step = 2;
            Settings.Save();
            Assert.AreEqual("aliluja", Settings.UserSettings.Profile.ThemeSelected);
            Assert.AreEqual(6, Settings.UserSettings.Profile.Range);
            Assert.AreEqual(2, Settings.UserSettings.Profile.Step);

        }
        [Test]
        public void AddTheme()
        {
            IThemeGateway themeGateway = Gateway.ThemeGateway;
            themeGateway.DeleteAllThemes();

            Theme theme = new Theme(0, "Animals");
            int id = themeGateway.AddTheme(theme);
            List<Theme> themeList = themeGateway.GetThemesList();
            Assert.AreEqual(1, themeList.Count);
            Theme theme1 = themeList[0];
            Assert.AreEqual(id, theme1.ID);
            Assert.AreEqual(theme.Name, theme1.Name);

        }
        [Test]
        public void UpdateTheme()
        {
            IThemeGateway themeGateway = Gateway.ThemeGateway;
            themeGateway.DeleteAllThemes();

            Theme theme = new Theme(0, "Animals");
            int id = themeGateway.AddTheme(theme);
            List<Theme> themeList = themeGateway.GetThemesList();
            Assert.AreEqual(1, themeList.Count);
            theme = themeList[0];
            theme.Name = "Flowers";
            Assert.AreEqual(id, theme.ID);
            Assert.AreEqual("Flowers", theme.Name);

        }
        [Test]
        public void DeleteTheme()
        {
            IThemeGateway themeGateway = Gateway.ThemeGateway;
            themeGateway.DeleteAllThemes();

            Theme theme = new Theme(0, "Animals");
            int id = themeGateway.AddTheme(theme);
            themeGateway.DeleteTheme(id);
            List<Theme> themeList = themeGateway.GetThemesList();
            Assert.AreEqual(0, themeList.Count);

        }
        [Test]
        public void AddLexeme()
        {
            IThemeGateway themeGateway = Gateway.ThemeGateway;
            themeGateway.DeleteAllThemes();
            Theme theme = new Theme(0, "Animals");
            int id = themeGateway.AddTheme(theme);

            ILexemGateway lexemGateway = Gateway.LexemGateway;
            Lexeme lex = Lexemes.LexemeCollection[0];
            lex.ParentThemeID = id;
            lexemGateway.AddLexeme(lex);
            Lexeme lex1 = Lexemes.LexemeCollection[1];
            lex1.ParentThemeID = id;
            lex1.ID=lexemGateway.AddLexeme(lex1);
            Dictionary<int, Lexeme> dic = lexemGateway.GetLexemes(id);

            Lexeme l = lexemGateway.GetLexeme(lex1.ID);
            Assert.AreEqual(2, dic.Count);
        }

        [Test]
        public void UpdateLexim()
        {
            IThemeGateway themeGateway = Gateway.ThemeGateway;
            themeGateway.DeleteAllThemes();
            Theme theme = new Theme(0, "Animals");
            int id = themeGateway.AddTheme(theme);

            ILexemGateway lexemGateway = Gateway.LexemGateway;
            Lexeme lex = Lexemes.LexemeCollection[0];
            lex.ParentThemeID = id;
            lex.ID = lexemGateway.AddLexeme(lex);

            lex.Word = "Fossil";
            lexemGateway.UpdateLexeme(lex);

            Lexeme lex1 = lexemGateway.GetLexeme(lex.ID);
            Assert.AreEqual("Fossil", lex1.Word);
            Assert.AreEqual(lex.ID, lex1.ID);
        }
        [Test]
        public void DeleteLexim()
        {
            IThemeGateway themeGateway = Gateway.ThemeGateway;
            themeGateway.DeleteAllThemes();
            Theme theme = new Theme(0, "Animals");
            int id = themeGateway.AddTheme(theme);

            ILexemGateway lexemGateway = Gateway.LexemGateway;
            Lexeme lex = Lexemes.LexemeCollection[0];
            lex.ParentThemeID = id;
            lex.ID = lexemGateway.AddLexeme(lex);

            lexemGateway.DeleteLexeme(lex.ID);
            Dictionary<int, Lexeme> dic = lexemGateway.GetLexemes(id);
            Assert.AreEqual(0, dic.Count);

            string message = "";
            try
            {
                lexemGateway.GetLexeme(lex.ID);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            Assert.AreEqual("There is no word with provided id", message);
          
        }
        [Test]
        public void FillWithTwoThemesAndLexims()
        {
            IThemeGateway themeGateway = Gateway.ThemeGateway;
            themeGateway.DeleteAllThemes();
            Theme theme1 = new Theme(0, "AnimalsPart1");
            int tid1 = themeGateway.AddTheme(theme1);
            Theme theme2 = new Theme(0, "AnimalsPart2");
            int tid2 = themeGateway.AddTheme(theme2);

            ILexemGateway lexemGateway = Gateway.LexemGateway;
            for (int i = 0; i < 8; i++)
            {
                Lexeme lex = Lexemes.LexemeCollection[i];
                lex.ParentThemeID = tid1;
                 lexemGateway.AddLexeme(lex);
            }
            for (int i = 7; i < 11; i++)
            {
                Lexeme lex = Lexemes.LexemeCollection[i];
                lex.ParentThemeID = tid2;
                lexemGateway.AddLexeme(lex);
            }
           
        }
    }
}
