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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LexiGame.View
{
    internal class ListBoxTheme : CustomListBox, IThemeListBox
    {
        public void AddDisplayTheme(ThemeDTView themeDT)
        {
            ListBoxItem lbi = MapToItem(themeDT);
            this.Items.Add(lbi);
        }

        public void InsertTheme(int index, ThemeDTView themeDT)
        {
            ListBoxItem lbi = MapToItem(themeDT);
            this.Items.Insert(0,lbi);
        }
      
        public ThemeDTView SelectedTheme
        {
            get 
            {
                if (this.SelectedIndex != -1)
                {
                    try
                    {
                        int id = Convert.ToInt32((((ListBoxItem)this.SelectedItem).Tag));
                        string name = Convert.ToString((((ListBoxItem)this.SelectedItem).Content));
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
 
        public void UpdateTheme(ThemeDTView themeDT)
        {
            if (this.SelectedItem != null)
            {
                ((ListBoxItem)this.SelectedItem).Content = themeDT.Name; ;
            }
        }

        private ListBoxItem MapToItem(ThemeDTView themeDT)
        {
            ListBoxItem lbi = new ListBoxItem();
            lbi.Tag = themeDT.ID;
            lbi.Content = themeDT.Name;
            return lbi;
        }
    }
}
