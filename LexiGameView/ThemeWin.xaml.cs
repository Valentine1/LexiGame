using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
using Microsoft.Win32;

namespace LexiGame.View
{
    /// <summary>
    /// Interaction logic for ThemeWin.xaml
    /// </summary>
    public partial class ThemeWin : baseWindow, IThemeView
    {
        public ThemeWin()
        {
            InitializeComponent();
            InitializeCommandsForMenu();
        }

        private bool IsThemeReadyToSave;
        private bool IsLeximReadyToNew;
        private bool IsLeximReadyToSave
        {
            get
            {
                if (tbLeximName.Tag != null)
                {
                    try
                    {
                        string[] idArray = tbLeximName.Tag.ToString().Split(new string[] { "_" }, StringSplitOptions.None);
                        if (Convert.ToInt32(idArray[0]) == 0)
                        {
                            return IsLeximReadyToSaveText && IsLeximReadyToSaveImage && IsLeximReadyToSaveSound;
                        }
                        else
                        {
                            return IsLeximReadyToSaveText || IsLeximReadyToSaveImage || IsLeximReadyToSaveSound;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
                return false;
            }
        }
        private bool IsLeximReadyToSaveText;
        private bool IsLeximReadyToSaveImage;
        private bool IsLeximReadyToSaveSound;

        #region  ***public interfaces for presenter

        public event ThemeSelectionChanged OnThemeSelectionChanged;
        public event LeximSelectionChanged OnLeximSelectionChanged;

        public event NewTheme OnNewTheme;
        public event SaveTheme OnSaveTheme;
        public event DeleteTheme OnDeleteTheme;

        public event NewLexim OnNewLexim;
        public event SaveLexim OnSaveLexim;
        public event DeleteLexim OnDeleteLexim;

        public IThemeListBox ThemeListBox
        {
            get
            {
                return (IThemeListBox)lbThemes;
            }
        }
        public ILeximListBox LeximListBox
        {
            get
            {
                return (ILeximListBox)lbLexims;
            }
        }

        public void DisplayTheme(ThemeDTView themeDT)
        {
            tbSelectedTheme.Text = themeDT.Name;
            tbSelectedTheme.Tag = themeDT.ID;
            tbSelectedTheme.Focus();
            tbSelectedTheme.CaretIndex = tbSelectedTheme.Text.Length;
            IsLeximReadyToNew = themeDT.ID != 0;
        }
        public void DisplayLexim(LeximDTView lexim, PlayAudio play)
        {
            tbLeximName.Tag = lexim.ID.ToString() + "_" + lexim.ParentThemeID.ToString();
            tbLeximName.Text = lexim.Word;
            tbLeximName.Focus();
            tbLeximName.CaretIndex = tbLeximName.Text.Length;

            imLexim.Source = null;
            imLexim.GDIBitmap = null;
            if (lexim.Picture != null)
            {
                imLexim.GDIBitmap = lexim.Picture;
                IntPtr imagePointer = lexim.Picture.GetHbitmap();
                BitmapSource bs = Imaging.CreateBitmapSourceFromHBitmap(imagePointer, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(80, 60));
                imLexim.Source = bs;
                baseWindow.DeleteObject(imagePointer);
            }

            btLeximPlay.AudioStream = lexim.Sound;
            btLeximPlay.Play = play;
            btLeximPlay.Click += new RoutedEventHandler(btLeximPlay_Click);
            // button.Style = resource["buttonListBox"] as Style;
            // button.Template = (resource["tcPlayButton"] as ControlTemplate);
        }

        public override void SetDynamicResources()
        {
            this.lbLexims.SetResource();
        }
        #endregion

        #region InitializeCommandsBinding
        private void InitializeCommandsForMenu()
        {
            CommandBinding NewThemeBinding = new CommandBinding(GameCommands.NewTheme);
            NewThemeBinding.Executed += new ExecutedRoutedEventHandler(NewThemeBinding_Executed);
            NewThemeBinding.CanExecute += new CanExecuteRoutedEventHandler(NewThemeBinding_CanExecute);
            this.CommandBindings.Add(NewThemeBinding);

            CommandBinding SaveThemeBinding = new CommandBinding(GameCommands.SaveTheme);
            SaveThemeBinding.Executed += new ExecutedRoutedEventHandler(SaveThemeBinding_Executed);
            SaveThemeBinding.CanExecute += new CanExecuteRoutedEventHandler(SaveThemeBinding_CanExecute);
            this.CommandBindings.Add(SaveThemeBinding);

            CommandBinding DeleteThemeBinding = new CommandBinding(GameCommands.DeleteTheme);
            DeleteThemeBinding.Executed += new ExecutedRoutedEventHandler(DeleteThemeBinding_Executed);
            DeleteThemeBinding.CanExecute += new CanExecuteRoutedEventHandler(DeleteThemeBinding_CanExecute);
            this.CommandBindings.Add(DeleteThemeBinding);

            CommandBinding NewLeximBinding = new CommandBinding(GameCommands.NewLexim);
            NewLeximBinding.Executed += new ExecutedRoutedEventHandler(NewLeximBinding_Executed);
            NewLeximBinding.CanExecute += new CanExecuteRoutedEventHandler(NewLeximBinding_CanExecute);
            this.CommandBindings.Add(NewLeximBinding);

            CommandBinding SaveLeximBinding = new CommandBinding(GameCommands.SaveLexim);
            SaveLeximBinding.Executed += new ExecutedRoutedEventHandler(SaveLeximBinding_Executed);
            SaveLeximBinding.CanExecute += new CanExecuteRoutedEventHandler(SaveLeximBinding_CanExecute);
            this.CommandBindings.Add(SaveLeximBinding);

            CommandBinding DeleteLeximBinding = new CommandBinding(GameCommands.DeleteLexim);
            DeleteLeximBinding.Executed += new ExecutedRoutedEventHandler(DeleteLeximBinding_Executed);
            DeleteLeximBinding.CanExecute += new CanExecuteRoutedEventHandler(DeleteLeximBinding_CanExecute);
            this.CommandBindings.Add(DeleteLeximBinding);
        }

        void NewThemeBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        void NewThemeBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (OnNewTheme != null)
            {
                OnNewTheme();
            }
        }

        void SaveThemeBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = IsThemeReadyToSave;
        }
        void SaveThemeBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OnSaveTheme(new ThemeDTView(Convert.ToInt32(tbSelectedTheme.Tag), tbSelectedTheme.Text));
        }

        void DeleteThemeBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = lbThemes.SelectedIndex != -1;
        }
        void DeleteThemeBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            OnDeleteTheme(Convert.ToInt32(tbSelectedTheme.Tag));
        }

        void NewLeximBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = IsLeximReadyToNew;
        }
        void NewLeximBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (OnNewLexim != null)
            {
                OnNewLexim();
            }
        }

        void SaveLeximBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = IsLeximReadyToSave && IsLeximReadyToNew;
        }
        void SaveLeximBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (OnSaveLexim != null)
            {
                string[] idArray = tbLeximName.Tag.ToString().Split(new string[] { "_" }, StringSplitOptions.None);
                OnSaveLexim(new LeximDTView(Convert.ToInt32(idArray[0]), Convert.ToInt32(idArray[1]), tbLeximName.Text, imLexim.GDIBitmap, btLeximPlay.AudioStream));

            }
        }

        void DeleteLeximBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = lbLexims.SelectedIndex != -1;
        }
        void DeleteLeximBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (OnDeleteLexim != null)
            {
                string[] idArray = tbLeximName.Tag.ToString().Split(new string[] { "_" }, StringSplitOptions.None);
                OnDeleteLexim(Convert.ToInt32(idArray[0]));
            }
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.AjustLoacationToOwner();
        }

        private void lbThemes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
            if (OnThemeSelectionChanged != null && lbThemes.SelectedTheme != null)
            {
                OnThemeSelectionChanged(lbThemes.SelectedTheme);
                IsThemeReadyToSave = false;
            }
        }

        private void lbLexims_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
            if (OnLeximSelectionChanged != null && lbLexims.SelectedLexim != null)
            {
                OnLeximSelectionChanged(lbLexims.SelectedLexim);
                IsLeximReadyToSaveText = IsLeximReadyToSaveImage = IsLeximReadyToSaveSound = false;
            }
        }

        private void btLeximPlay_Click(object sender, RoutedEventArgs e)
        {
            AudioButton but = ((AudioButton)sender);
            //but.AudioStream.Position = 0;
            if (but.AudioStream != null)
            {
                but.Play(but.AudioStream,false);
            }
        }

        private void tbLeximName_TextChanged(object sender, TextChangedEventArgs e)
        {
            e.Handled = true;
            IsLeximReadyToSaveText = tbLeximName.Text.Length > 0;
        }

        private void tbSelectedTheme_TextChanged(object sender, TextChangedEventArgs e)
        {
            e.Handled = true;
            IsThemeReadyToSave = tbSelectedTheme.Text.Length > 0 ? true : false;
        }

        private void imLexim_OnBitmapImageChanged()
        {
            IsLeximReadyToSaveImage = imLexim.GDIBitmap != null;
        }

        private void btLeximPlay_OnStreamChanged()
        {
            IsLeximReadyToSaveSound = btLeximPlay.AudioStream != null;
        }

        private void btLeximImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.RestoreDirectory = true;
            ofd.Filter = "Picture (*.jpg)|*.jpg";
            ofd.Title = "Select Picture";
            bool? result = ofd.ShowDialog();
            if (result == true)
            {
                try
                {
                    Bitmap picture = new Bitmap(ofd.FileName);
                    if (picture.Width != 80 || picture.Height != 60)
                    {
                        throw new Exception("Picture size does not correspond to obligatory one");
                    }
                    imLexim.GDIBitmap = picture;
                    IntPtr imagePointer = picture.GetHbitmap();
                    imLexim.Source = Imaging.CreateBitmapSourceFromHBitmap(imagePointer, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(60, 45));
                    baseWindow.DeleteObject(imagePointer);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not assign new picture " + ex.Message);
                }
            }
        }

        private void btLeximAudio_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.RestoreDirectory = true;
            ofd.Filter = "Sound (*.mp3)|*.mp3";
            ofd.Title = "Select Sound";
            bool? result = ofd.ShowDialog();
            if (result == true)
            {
                try
                {
                    FileStream fstream = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);
                    MemoryStream mem = new MemoryStream();

                    if (fstream.Length > 51200)
                    {
                        throw new Exception("Sound size does not correspond to obligatory one");
                    }
                    if (!this.TryPlay(fstream))
                    {
                        throw new Exception("Invalid file format");
                    }
                    btLeximPlay.AudioStream = fstream;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not assign new sound " + ex.Message);
                }
            }
        }
    }
}
