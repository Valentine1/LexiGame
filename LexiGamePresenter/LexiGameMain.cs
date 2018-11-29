using System;
using System.IO;
using System.Drawing;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LexiGame.View;
using LexiGame.BLL;


namespace LexiGame.Presenter
{
    public class LexiGamePresenter : LexiGame.Presenter.BasePresenter
    {
        public static void Initialize()
        {
            //FileStream pic1 = new FileStream("butterfly.jpg", FileMode.Open, FileAccess.Read);
            //FileStream pic2 = new FileStream("tiger.jpg", FileMode.Open, FileAccess.Read);
            //FileStream pic3 = new FileStream("beaver.jpg", FileMode.Open, FileAccess.Read);
            //FileStream pic4 = new FileStream("crow.jpg", FileMode.Open, FileAccess.Read);
            //FileStream pic5 = new FileStream("elk.jpg", FileMode.Open, FileAccess.Read);
            //FileStream pic6 = new FileStream("hare.jpg", FileMode.Open, FileAccess.Read);
            //FileStream pic7 = new FileStream("horse.jpg", FileMode.Open, FileAccess.Read);
            //FileStream pic8 = new FileStream("shark.jpg", FileMode.Open, FileAccess.Read);
            //FileStream pic9 = new FileStream("sparrow.jpg", FileMode.Open, FileAccess.Read);
            //FileStream pic10 = new FileStream("turkey.jpg", FileMode.Open, FileAccess.Read);
            //FileStream pic11 = new FileStream("wildBoar.jpg", FileMode.Open, FileAccess.Read);

            //FileStream sound1 = new FileStream("butterfly.mp3", FileMode.Open, FileAccess.Read);
            //FileStream sound2 = new FileStream("tiger.mp3", FileMode.Open, FileAccess.Read);
            //FileStream sound3 = new FileStream("beaver.mp3", FileMode.Open, FileAccess.Read);
            //FileStream sound4 = new FileStream("crow.mp3", FileMode.Open, FileAccess.Read);
            //FileStream sound5 = new FileStream("elk.mp3", FileMode.Open, FileAccess.Read);
            //FileStream sound6 = new FileStream("hare.mp3", FileMode.Open, FileAccess.Read);
            //FileStream sound7 = new FileStream("horse.mp3", FileMode.Open, FileAccess.Read);
            //FileStream sound8 = new FileStream("shark.mp3", FileMode.Open, FileAccess.Read);
            //FileStream sound9 = new FileStream("sparrow.mp3", FileMode.Open, FileAccess.Read);
            //FileStream sound10 = new FileStream("turkey.mp3", FileMode.Open, FileAccess.Read);
            //FileStream sound11 = new FileStream("wildBoar.mp3", FileMode.Open, FileAccess.Read);

            //Bitmap bit1 = new Bitmap(pic1);
            //Bitmap bit2 = new Bitmap(pic2);
            //Bitmap bit3 = new Bitmap(pic3);
            //Bitmap bit4 = new Bitmap(pic4);
            //Bitmap bit5 = new Bitmap(pic5);
            //Bitmap bit6 = new Bitmap(pic6);
            //Bitmap bit7 = new Bitmap(pic7);
            //Bitmap bit8 = new Bitmap(pic8);
            //Bitmap bit9 = new Bitmap(pic9);
            //Bitmap bit10 = new Bitmap(pic10);
            //Bitmap bit11 = new Bitmap(pic11);

            //Lexeme lex1 = new Lexeme(1, 1, "butterfly", bit1, sound1);
            //Lexeme lex2 = new Lexeme(2, 1, "tiger", bit2, sound2);
            //Lexeme lex3 = new Lexeme(3, 1, "beaver", bit3, sound3);
            //Lexeme lex4 = new Lexeme(4, 1, "crow", bit4, sound4);
            //Lexeme lex5 = new Lexeme(5, 1, "elk", bit5, sound5);
            //Lexeme lex6 = new Lexeme(6, 1, "hare", bit6, sound6);
            //Lexeme lex7 = new Lexeme(7, 1, "horse", bit7, sound7);
            //Lexeme lex8 = new Lexeme(8, 1, "shark", bit8, sound8);
            //Lexeme lex9 = new Lexeme(9, 1, "sparrow", bit9, sound9);
            //Lexeme lex10 = new Lexeme(10, 1, "turkey", bit10, sound10);
            //Lexeme lex11 = new Lexeme(11, 1, "wildBoar", bit11, sound11);

            //Lexemes.LexemeDictionary.Clear();
            //Lexemes.LexemeDictionary.Add(lex1.ID, lex1);
            //Lexemes.LexemeDictionary.Add(lex2.ID, lex2);
            //Lexemes.LexemeDictionary.Add(lex3.ID, lex3);
            //Lexemes.LexemeDictionary.Add(lex4.ID, lex4);
            //Lexemes.LexemeDictionary.Add(lex5.ID, lex5);
            //Lexemes.LexemeDictionary.Add(lex6.ID, lex6);
            //Lexemes.LexemeDictionary.Add(lex7.ID, lex7);
            //Lexemes.LexemeDictionary.Add(lex8.ID, lex8);
            //Lexemes.LexemeDictionary.Add(lex9.ID, lex9);
            //Lexemes.LexemeDictionary.Add(lex10.ID, lex10);
            //Lexemes.LexemeDictionary.Add(lex11.ID, lex11);

        }
        /// <summary>
        ///  Entry Point.
        /// </summary>
        [System.STAThreadAttribute()]
        public static void Main()
        {
          //  Initialize();
            try
            {
                ApplicationLoader appLoader = new ApplicationLoader();
                IApplication app = appLoader.LoadApplication();
                MainWindowPresenter mainWinPresenter = new MainWindowPresenter();
                mainWinPresenter.MainView.Show();
                app.RunApplication(mainWinPresenter.MainView);
            }
            catch (Exception ex)
            {
                string mes = ex.Message;
                if (ex.InnerException != null)
                {
                    mes += ex.InnerException.Message;
                }
                System.Windows.MessageBox.Show(mes);
            }
        }
    }
}
