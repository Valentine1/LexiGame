using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LexiGame.BLL;
using LexiGame.View;
using LexiGame.DB;

namespace LexiGame.Presenter
{
    public class GameWindowPresenter : BasePresenter
    {
        private IGameView gameView;
        public IGameView GameView
        {
            get
            {
                return gameView;
            }
            set
            {
                gameView = value;
            }
        }
        private IThemeGateway _themeGateway;
        private IThemeGateway ThemeGateway
        {
            get
            {
                if (_themeGateway == null)
                {
                    _themeGateway = Gateway.ThemeGateway;
                }
                return _themeGateway;
            }
        }
        private ILexemGateway _lexemGateway;
        private ILexemGateway LexemGateway
        {
            get
            {
                if (_lexemGateway == null)
                {
                    _lexemGateway = Gateway.LexemGateway;
                }
                return _lexemGateway;
            }
        }
        private Session _gameSession;
        private Session GameSession
        {
            get
            {
                if (_gameSession == null)
                {
                    _gameSession = new Session(Utility.Settings.UserSettings.Profile.Range, Utility.Settings.UserSettings.Profile.Step);
                    _gameSession.CurrentLevel = 1;
                }
                return _gameSession;
            }
        }
        private Game _myGame;
        private Game MyGame
        {
            get
            {
                if (_myGame == null)
                {
                    _myGame = new Game();
                }
                return _myGame;
            }
        }
        bool CanBeStarted;

        public GameWindowPresenter(IGameView view)
        {
            this.gameView = view;
            int themeID = 0;
            try
            {
                Lexemes.Clear();
                themeID = GetSelectedThemeID();
                if (themeID == -1)
                    throw new Exception("Selected theme for game could not be found, please, select existing one in the Settings menu");
               
                Lexemes.LexemeDictionary = LexemGateway.GetLexemes(themeID);
                if (Lexemes.LexemeDictionary.Count < 3)
                    throw new Exception("The theme should contain at least 3 words to start a game");
            }
            catch (Exception ex)
            {
                this.HandleException("", ex);
                CanBeStarted = false;
                return;
            }
            this.GameView.OnPlayEnd += new PlayEnd(GameView_OnPlayEnd);
            this.GameView.OnThrowBegin += new ThrowBegin(GameView_OnThrowBegin);
            this.GameView.OnThrowEnd += new ThrowEnd(GameView_OnThrowEnd);
            CanBeStarted = true;
        }

        private void GameView_OnThrowEnd()
        {
            this.GameView.SetScores(GameSession.Scores);
            int idLexim = MyGame.GetLeximIDtoPronounce();
            if (idLexim > 0)
            {
                this.GameView.Play(Lexemes.LexemeDictionary[idLexim].Sound, true);
            }
            else
            {
                if (GameSession.CurrentLevel < GameSession.Levels)
                {
                    GameSession.CurrentLevel++;
                    DoNextLevel();
                }
            }

        }

        private void GameView_OnThrowBegin(int xPos)
        {
            ThrowResult result = MyGame.GetThrowResult(xPos);
            GameSession.Scores += result.HitResult ? 1 : -1;
            ThrowResultDT resDT = new ThrowResultDT(result.Row, result.Column, result.HitResult);
            int yPos = result.Row == -1 ? 0 : (result.Row + 1) * FieldSettings.PictureHeight;
            this.GameView.AnimateBall(resDT, yPos);
        }

        private void GameView_OnPlayEnd()
        {
            this.GameView.DrawPadAndBall();
        }

        public void StartGame()
        {
            if (CanBeStarted)
            {
                foreach(Lexeme lexim in Lexemes.LexemeCollection)
                {
                    this.GameView.ImageStack.AddLexim(new LeximDTView(0, 0, lexim.Word, lexim.Picture, lexim.Sound), this.GameView.Play);
                }
                if (GameSession.Range > Lexemes.LexemeDictionary.Count)
                    GameSession.Range = Lexemes.LexemeDictionary.Count;
                this.GameView.SetThemeName(Utility.Settings.UserSettings.Profile.ThemeSelected);
                DoNextLevel();
            }
        }
        private void DoNextLevel()
        {
            try
            {
                Field field = MyGame.GetField(GameSession.StartIndex, GameSession.EndIndex);
                this.DisplayField(field);
                this.GameView.SetLevels(GameSession.Levels, GameSession.CurrentLevel);
                this.GameView.Play(Lexemes.LexemeDictionary[MyGame.GetLeximIDtoPronounce()].Sound, true);

            }
            catch (Exception ex)
            {
                this.HandleException("", ex);
                return;
            }
        }
        private void DisplayField(Field field)
        {
            this.GameView.ClearField();
            for (int i = 0; i < field.LexemLines.Count; i++)
            {
                for (int k = 0; k < field.LexemLines[i].LexemViews.Count; k++)
                {
                    this.GameView.DrawBitmap(field.LexemLines[i].LexemViews[k].Lexem.Picture, field.LexemLines[i].LexemViews[k].X,
                        field.LexemLines[i].Y);
                }
            }
            //foreach (LexemLine line in field.LexemLines)
            //{
            //    foreach (LexemView view in line.LexemViews)
            //    {
            //        this.GameView.DrawBitmap(view.Lexem.Picture, view.X, line.Y);
            //    }
            //}
        }
        private int GetSelectedThemeID()
        {
            List<Theme> themeList = ThemeGateway.GetThemesList();
            string selectedTheme = Utility.Settings.UserSettings.Profile.ThemeSelected;
            int themeID = -1;
            for (int i = 0; i < themeList.Count; i++)
            {
                if (themeList[i].Name == selectedTheme)
                {
                    themeID = themeList[i].ID;
                }
            }
            return themeID;
        }
    }
}
