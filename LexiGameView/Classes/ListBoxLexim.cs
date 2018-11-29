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

namespace LexiGame.View
{
    internal class ListBoxLexim : CustomListBox, ILeximListBox
    {
        public void AddDisplayLexim(LeximDTView lexim, PlayAudio play)
        {
            ListBoxItem lbi = MapToItem(lexim, play);
            this.Items.Add(lbi);
        }
        void button_GotFocus(object sender, RoutedEventArgs e)
        {
            ((ListBoxItem)((StackPanel)((Button)sender).Parent).Parent).IsSelected = true;
        }
        private void tb_GotFocus(object sender, RoutedEventArgs e)
        {
            ((ListBoxItem)((StackPanel)((TextBlock)sender).Parent).Parent).IsSelected = true;
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            AudioButton but = ((AudioButton)sender);
            //but.AudioStream.Position = 0;
            but.Play(but.AudioStream, false);
        }

        public void UpdateLexim(LeximDTView leximDT)
        {
            if (this.SelectedIndex != -1)
            {
                StackPanel stackPanel = (StackPanel)((ListBoxItem)this.SelectedItem).Content;
                ((TextBlock)stackPanel.Children[0]).Text = leximDT.Word;
                ((BitmapImage)stackPanel.Children[1]).GDIBitmap = leximDT.Picture;
                ((BitmapImage)stackPanel.Children[1]).Source = Imaging.CreateBitmapSourceFromHBitmap(leximDT.Picture.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(60, 45));
                ((AudioButton)stackPanel.Children[2]).AudioStream = leximDT.Sound;
            }
        }
        public void InsertLexim(int index, LeximDTView leximDT, PlayAudio play)
        {
            ListBoxItem lbi = MapToItem(leximDT, play);
            this.Items.Insert(0, lbi);
        }
        public void SetResource()
        {
            ResourceDictionary resource = ((ResourceDictionary)((MyApplication)Application.Current).MyResources["resLeximListBox"]);
            foreach (ListBoxItem lbi in this.Items)
            {
                lbi.Template = resource["listBoxItem"] as ControlTemplate;
                 StackPanel panel=(StackPanel)(lbi).Content;
                 ((AudioButton)panel.Children[2]).Template = (resource["tcPlayButton"] as ControlTemplate);
            }
        }
        public LeximDTView SelectedLexim
        {
            get 
            {
                if (this.SelectedItem != null)
                {
                    string tag = (string)((ListBoxItem)this.SelectedItem).Tag;
                    string [] idArray=  tag.Split(new string[] { "_" }, StringSplitOptions.None);
                    if (idArray.Length < 2)
                    {
                        throw new Exception("Lexim does not belong to any theme");
                    }
                    try
                    {
                       
                        int id = int.Parse((idArray)[0]);
                        int tid=int.Parse((idArray)[1]);
                        StackPanel panel=(StackPanel)((ListBoxItem)this.SelectedItem).Content;
                        string word=((TextBlock)panel.Children[0]).Text;
                        Bitmap picture=((BitmapImage)panel.Children[1]).GDIBitmap;
                        Stream audio=((AudioButton)panel.Children[2]).AudioStream;
                        LeximDTView leximDT = new LeximDTView(id, tid, word, picture, audio);
                        return leximDT;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Lexim in ListBox is damaged and cznnot be retrieved", ex);
                    }
                }
                return null;
            }
        }
        private ListBoxItem MapToItem(LeximDTView leximDT, PlayAudio play)
        {
            ResourceDictionary resource = ((ResourceDictionary)((MyApplication)Application.Current).MyResources["resLeximListBox"]);
            ListBoxItem lbi = new ListBoxItem();
            lbi.Tag = leximDT.ID.ToString() + "_" + leximDT.ParentThemeID.ToString();
            lbi.Template = resource["listBoxItem"] as ControlTemplate;

            StackPanel stackPanel = new StackPanel();
            lbi.Content = stackPanel;
            stackPanel.Style = resource["stackListItem"] as Style;
            stackPanel.Orientation = Orientation.Horizontal;

            TextBlock tb = new TextBlock();
            stackPanel.Children.Add(tb);
            tb.Text = leximDT.Word;
            tb.Style = resource["tbName"] as Style;
            tb.GotFocus += new RoutedEventHandler(tb_GotFocus);

            BitmapImage im = new BitmapImage();
            stackPanel.Children.Add(im);
            im.Style = resource["imageListBox"] as Style;
            im.GDIBitmap = leximDT.Picture;
            IntPtr imagePointer = leximDT.Picture.GetHbitmap();
            WriteableBitmap b = null;
            im.Source =  Imaging.CreateBitmapSourceFromHBitmap(imagePointer, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(60, 45));
            baseWindow.DeleteObject(imagePointer);

            AudioButton button = new AudioButton();
            stackPanel.Children.Add(button);
            button.AudioStream = leximDT.Sound;
            button.Play = play;
            button.Style = resource["buttonListBox"] as Style;
            button.Template = (resource["tcPlayButton"] as ControlTemplate);
            button.Click += new RoutedEventHandler(button_Click);
            button.GotFocus += new RoutedEventHandler(button_GotFocus);
            return lbi;
        }
    }
}
