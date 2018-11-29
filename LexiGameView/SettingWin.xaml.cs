using System;
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
using System.Windows.Shapes;

namespace LexiGame.View
{
    /// <summary>
    /// Interaction logic for SettingWin.xaml
    /// </summary>
    public partial class SettingWin : baseWindow, ISettingView
    {
        public SettingWin()
        {
            InitializeComponent();
        }
        
        #region public interface
        public event OKclick OnOKclick;
        public event ResourceChanged OnResourceChanged;
        public void AddTheme(ThemeDTView themeDT)
        {
            ComboBoxItem cbi= new ComboBoxItem();
            cbi.Tag = themeDT.ID;
            cbi.Content = themeDT.Name;
            cbThemes.Items.Add(cbi);
            
        }

        public void SetSelectedThemeIndex(int i)
        {
            cbThemes.SelectedIndex = i;
        }
        public void InitRange(int rangeBegin, int rangeEnd, int index)
        {
            for (int i = rangeBegin; i <= rangeEnd; i++)
            {
                cbRange.Items.Add(i);
            }
            cbRange.SelectedIndex = index - 3;
        }
        public void InitStep(int range, int index)
        {
            for (int i = 1; i <= range; i++)
            {
                cbStep.Items.Add(i);
            }
            cbStep.SelectedIndex = index - 1;
        }
        public void InitSkins(List<string> skins, int index)
        {
            foreach (string s in skins)
            {
                cbSkin.Items.Add(s);
            }
            cbSkin.SelectedIndex = index;
        }
        public void InitLanguages(List<string> languages, int index)
        {
            foreach (string s in languages)
            {
                cbLanguage.Items.Add(s);
            }
            cbLanguage.SelectedIndex = index;
        }
        #endregion

        private ThemeDTView SelectedTheme
        {
            get
            {
                if (this.cbThemes.SelectedIndex != -1)
                {
                    try
                    {
                        int id = Convert.ToInt32((((ComboBoxItem)cbThemes.SelectedItem).Tag));
                        string name = Convert.ToString((((ComboBoxItem)cbThemes.SelectedItem).Content));
                        ThemeDTView themeDT = new ThemeDTView(id, name);
                        return themeDT;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Theme has no id", ex);
                    }
                }
                return null;
            }
        }
        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (OnOKclick != null)
            {
                
                OnOKclick(this.SelectedTheme, Convert.ToInt32(this.cbRange.SelectedValue), Convert.ToInt32(this.cbStep.SelectedValue),
                    Convert.ToString(this.cbSkin.SelectedValue), Convert.ToString(this.cbLanguage.SelectedValue));
              
                this.Close();
                ((MyApplication)Application.Current).SetResources();
                if (OnResourceChanged != null)
                {
                    OnResourceChanged();
                }
                
            }
           
        }
    }
}
