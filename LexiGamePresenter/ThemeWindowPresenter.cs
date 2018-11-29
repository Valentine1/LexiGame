using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LexiGame.BLL;
using LexiGame.View;
using LexiGame.DB;
using System.Windows;

namespace LexiGame.Presenter
{
    public class ThemeWindowPresenter : BasePresenter
    {
        private IThemeView _themeView;
        public IThemeView ThemeView
        {
            get
            {
                return _themeView;
            }
            set
            {
                _themeView = value;
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
        public ThemeWindowPresenter(IThemeView themeView)
        {
            this._themeView = themeView;
            Initialize();
        }
        private void Initialize()
        {
            this.ThemeView.OnThemeSelectionChanged += new ThemeSelectionChanged(ThemeView_OnThemeSelectionChanged);
            this.ThemeView.OnLeximSelectionChanged += new LeximSelectionChanged(ThemeView_OnLeximSelectionChanged);
            this.ThemeView.OnNewTheme += new NewTheme(ThemeView_OnNewTheme);
            this.ThemeView.OnSaveTheme += new SaveTheme(ThemeView_OnSaveTheme);
            this.ThemeView.OnDeleteTheme += new DeleteTheme(ThemeView_OnDeleteTheme);
            this.ThemeView.OnNewLexim += new NewLexim(ThemeView_OnNewLexim);
            this.ThemeView.OnSaveLexim += new SaveLexim(ThemeView_OnSaveLexim);
            this.ThemeView.OnDeleteLexim += new DeleteLexim(ThemeView_OnDeleteLexim);
        }

        void ThemeView_OnNewLexim()
        {
            try
            {
                if (this.ThemeView.ThemeListBox.SelectedTheme == null)
                    throw new Exception("A Theme should be selected before a word may be created");
                int tid = this.ThemeView.ThemeListBox.SelectedTheme.ID;
                if (tid == 0)
                    throw new Exception("A word can not be created if a Theme is unsaved");
                this.ThemeView.DisplayLexim(new LeximDTView(0, tid, string.Empty, null, null), this.ThemeView.Play);
            }
            catch (Exception ex)
            {
                this.HandleException(string.Empty, ex);
            }
        }
        void ThemeView_OnSaveLexim(LeximDTView leximDT)
        {
            try
            {
                Lexeme lexim = new Lexeme(leximDT.ID, leximDT.ParentThemeID, leximDT.Word, leximDT.Picture, leximDT.Sound);
                if (lexim.ID == 0)
                {
                    leximDT.ID = LexemGateway.AddLexeme(lexim);
                    this.ThemeView.LeximListBox.InsertLexim(0, leximDT, this.ThemeView.Play);
                    this.ThemeView.LeximListBox.SetSelectionAt(0);
                }
                else
                {
                    LexemGateway.UpdateLexeme(lexim);
                    this.ThemeView.LeximListBox.UpdateLexim(leximDT);
                }
            }
            catch (Exception ex)
            {
                this.HandleException("Could not add or update a word ", ex);
            }
        }
        void ThemeView_OnDeleteLexim(int id)
        {
            try
            {
                LexemGateway.DeleteLexeme(id);
                this.ThemeView.LeximListBox.RemoveSelectedItem();
                if (this.ThemeView.LeximListBox.ItemsCount == 0)
                {
                    this.ThemeView.DisplayLexim(new LeximDTView(0,this.ThemeView.ThemeListBox.SelectedTheme.ID, string.Empty,null,null), this.ThemeView.Play);
                }
            }
            catch (Exception ex)
            {
                this.HandleException("Could not delete word", ex);
            }
        }

     
        void ThemeView_OnNewTheme()
        {
            this.ThemeView.DisplayTheme(new ThemeDTView(0, string.Empty));
            this.ThemeView.ThemeListBox.RemoveSelection();
            this.ThemeView.LeximListBox.ClearAllItems();
            this.ThemeView.DisplayLexim(new LeximDTView(0, 0, string.Empty, null, null), this.ThemeView.Play);
        }
        void ThemeView_OnSaveTheme(ThemeDTView themeDT)
        {
            try
            {
                Theme theme = new Theme(themeDT.ID, themeDT.Name);
                if (theme.ID == 0)
                {
                    themeDT.ID = ThemeGateway.AddTheme(theme);
                    this.ThemeView.ThemeListBox.InsertTheme(0, themeDT);
                    this.ThemeView.ThemeListBox.SetSelectionAt(0);
                    this.ThemeView.DisplayLexim(new LeximDTView(0, this.ThemeView.ThemeListBox.SelectedTheme.ID, string.Empty, null, null), this.ThemeView.Play);
                }
                else
                {
                    ThemeGateway.UpdateTheme(new Theme(themeDT.ID, themeDT.Name));
                    this.ThemeView.ThemeListBox.UpdateTheme(themeDT);
                }
            }
            catch (Exception ex)
            {
                this.HandleException("Could not add or update theme ", ex);
            }

        }
        void ThemeView_OnDeleteTheme(int id)
        {
            try
            {
                ThemeGateway.DeleteTheme(id);
                this.ThemeView.LeximListBox.ClearAllItems();
                this.ThemeView.DisplayLexim(new LeximDTView(0, 0, string.Empty, null, null), this.ThemeView.Play);
                this.ThemeView.ThemeListBox.RemoveSelectedItem();
                if (this.ThemeView.ThemeListBox.ItemsCount == 0)
                {
                    this.ThemeView.DisplayTheme(new ThemeDTView(0, string.Empty));
                }
            }
            catch (Exception ex)
            {
                this.HandleException("Could not delete theme", ex);
            }
        }

        void ThemeView_OnLeximSelectionChanged(LeximDTView leximDT)
        {
            this.ThemeView.DisplayLexim(leximDT, this.ThemeView.Play);
        }
        private void ThemeView_OnThemeSelectionChanged(ThemeDTView themeDT)
        {
            this.ThemeView.DisplayTheme(themeDT);
            DisplayLexims(themeDT.ID);
        }

        public void DisplayThemes()
        {
            List<Theme> themeList = ThemeGateway.GetThemesList();
            this.ThemeView.ThemeListBox.ClearAllItems();
            foreach (Theme theme in themeList)
            {
                this.ThemeView.ThemeListBox.AddDisplayTheme(new ThemeDTView(theme.ID, theme.Name));
            }
            if (themeList.Count > 0)
            {
                this.ThemeView.ThemeListBox.SetSelectionAt(0);
            }
        }
        void DisplayLexims(int themeID)
        {
            Lexemes.Clear();
            System.GC.Collect();
            ILexemGateway lexemGateway = Gateway.LexemGateway;
            Lexemes.LexemeDictionary = lexemGateway.GetLexemes(themeID);
            this.ThemeView.LeximListBox.ClearAllItems();
            foreach (Lexeme lex in Lexemes.LexemeCollection)
            {
                LeximDTView ldt = new LeximDTView(lex.ID, lex.ParentThemeID, lex.Word, lex.Picture, lex.Sound);
                this.ThemeView.LeximListBox.AddDisplayLexim(ldt, this.ThemeView.Play);
            }
            if (Lexemes.LexemeCollection.Count > 0)
            {
                this.ThemeView.LeximListBox.SetSelectionAt(0);
            }
            else
            {
                this.ThemeView.DisplayLexim(new LeximDTView(0, themeID, string.Empty, null, null), this.ThemeView.Play);

            }
        }
    }
}
