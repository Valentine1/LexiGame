using System;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
//using Microsoft.DirectX.DirectSound;
//using Microsoft.DirectX.AudioVideoPlayback;
using Microsoft.Windows.Themes;
using System.Windows.Threading;

namespace LexiGame.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GameWin : baseWindow, IGameView
    {
        //Microsoft.DirectX.DirectSound.Buffer buf;
        //Microsoft.DirectX.AudioVideoPlayback.Audio audio;
        private System.Windows.Shapes.Rectangle pad;
        private System.Windows.Shapes.Ellipse ball;
        private ThrowResultDT resultThrow
        {
            get;
            set;
        }
        private bool IsBallOnPad;
        private DispatcherTimer timerSlideUp;
        private DispatcherTimer timerSlideDown;
        public GameWin(): base()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            InitializeComponent();
            InitializePadAndBall();
            this.Closing += new System.ComponentModel.CancelEventHandler(GameWin_Closing);
            canvasField.MouseMove += new MouseEventHandler(canvasField_MouseMove);
            timerSlideUp = new DispatcherTimer();
            timerSlideUp.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timerSlideUp.Tick +=timerSlideUp_Tick;
            timerSlideDown = new DispatcherTimer();
            timerSlideDown.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timerSlideDown.Tick+=timerSlideDown_Tick;



        }
        private void InitializePadAndBall()
        {
            ResourceDictionary resource = ((ResourceDictionary)((MyApplication)Application.Current).MyResources["resPadAnBall"]);
            pad = new System.Windows.Shapes.Rectangle();
            pad.Visibility = Visibility.Hidden;
            pad.Style = resource["Pad"] as Style;
            canvasField.Children.Add(pad);
            pad.SetValue(Canvas.TopProperty, Convert.ToDouble(510));
            pad.SetValue(Canvas.LeftProperty, Convert.ToDouble(290));

            ball = new System.Windows.Shapes.Ellipse();
            ball.Visibility = Visibility.Hidden;
            ball.Style = resource["Ball"] as Style;
            canvasField.Children.Add(ball);

        }
       
        #region public interface
        public override void SetDynamicResources()
        {
            ResourceDictionary resource = ((ResourceDictionary)((MyApplication)Application.Current).MyResources["resPadAnBall"]);
            pad.Style = resource["Pad"] as Style;
            ball.Style = resource["Ball"] as Style;
        }
        public IImageStack ImageStack
        {
            get
            {
                return (IImageStack)spImages;
            }
        }
        public event ThrowBegin OnThrowBegin;
        public event ThrowEnd OnThrowEnd;

        public void DrawPadAndBall()
        {
            drawPad draw = new drawPad(displayPadAndBall);
            this.Dispatcher.Invoke(draw, null);
        }
        public void AnimateBall(ThrowResultDT result, int yPos)
        {
            resultThrow = result;
            DoubleAnimation anime = new DoubleAnimation();
            anime.From = Convert.ToDouble(ball.GetValue(Canvas.TopProperty));
            anime.To = Convert.ToDouble(yPos);
            if (yPos > 360)
            {
                anime.Duration = TimeSpan.FromSeconds(0.1);
            }
            else if (yPos > 300)
            {
                anime.Duration = TimeSpan.FromSeconds(0.2);
            }
            else if (yPos > 240)
            {
                anime.Duration = TimeSpan.FromSeconds(0.3);
            }
            else if (yPos > 180)
            {
                anime.Duration = TimeSpan.FromSeconds(0.4);
            }
            else if (yPos > 120)
            {
                anime.Duration = TimeSpan.FromSeconds(0.5);
            }
            else
            {
                anime.Duration = TimeSpan.FromSeconds(0.6);
            }

            anime.Completed += new EventHandler(anime_Completed);
            //  anime.DecelerationRatio = 0.3;
            ball.BeginAnimation(Canvas.TopProperty, anime);
        }
        public void ClearField()
        {
            canvasField.Children.Clear();
            canvasField.Children.Add(pad);
            canvasField.Children.Add(ball);
        }
        public void SetLevels(int all, int current)
        {
            tbLevels.Text = all.ToString() + "/" + current.ToString();
        }
        public void SetScores(int scores)
        {
            tbScores.Text = scores.ToString();
        }
        public void SetThemeName(string name)
        {
            tbTheme.Text = name;
        }
        #endregion
        delegate void drawPad();
        void anime_Completed(object sender, EventArgs e)
        {
            ball.BeginAnimation(Canvas.TopProperty, null);
            ball.Visibility = Visibility.Hidden;
            if (resultThrow.HitResult)
            {
                int orderNumb = resultThrow.Row * 8 + resultThrow.Column + 2;
                canvasField.Children[orderNumb].Visibility = Visibility.Hidden;
            }
            if (OnThrowEnd != null)
            {
                OnThrowEnd();
            }
        }
        private void displayPadAndBall()
        {
            canvasField.Cursor = Cursors.None;
            ball.SetValue(Canvas.TopProperty, Convert.ToDouble(pad.GetValue(Canvas.TopProperty)) - 30);
            ball.SetValue(Canvas.LeftProperty, Convert.ToDouble(pad.GetValue(Canvas.LeftProperty)) + 15);
            pad.Visibility = Visibility.Visible;
            ball.Visibility = Visibility.Visible;
            IsBallOnPad = true;
            canvasField.MouseDown += canvasField_MouseDown;

        }

        void canvasField_MouseDown(object sender, MouseButtonEventArgs e)
        {
            canvasField.MouseDown -= canvasField_MouseDown;
            IsBallOnPad = false;
            if (OnThrowBegin != null)
            {
                OnThrowBegin(Convert.ToInt32(ball.GetValue(Canvas.LeftProperty)) + 15);
            }
        }

        void canvasField_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.GetPosition(canvasField).X < 580)
            {
                pad.SetValue(Canvas.LeftProperty, e.GetPosition(canvasField).X);
                if (IsBallOnPad)
                {
                    ball.SetValue(Canvas.LeftProperty, e.GetPosition(canvasField).X + 15);
                }
            }
        }

        void GameWin_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            canvasField.Children.Clear();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AjustLoacationToOwner();
            //Microsoft.DirectX.DirectSound.Device dev = new Microsoft.DirectX.DirectSound.Device();
            //IntPtr hwnd = new WindowInteropHelper(this).Handle;
            //dev.SetCooperativeLevel(hwnd, Microsoft.DirectX.DirectSound.CooperativeLevel.Normal);
            //buf = new Microsoft.DirectX.DirectSound.Buffer("C:\\START.WAV", dev);

            //audio = new Audio("Ispano.wma");

        }

        private void cbTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //switch (cbTheme.SelectedIndex)
            //{
            //    case 0:
            //        Application.Current.Resources.MergedDictionaries[0] = ((MyApplication)Application.Current).resContrastSliderStyle;
            //        break;
            //    case 1:
            //        Application.Current.Resources.MergedDictionaries[0] = ((MyApplication)Application.Current).resLightSliderStyle;
            //        break;
            //}
        }

        public void ownerWin_LocationChanged(object sender, EventArgs e)
        {
            this.Top = ((Window)sender).Top + 23;
            this.Left = ((Window)sender).Left + 3;
        }
        public void DrawBitmap(Bitmap bit, int x, int y)
        {
            System.Windows.Controls.Image im = new System.Windows.Controls.Image();
            IntPtr intPt = bit.GetHbitmap();
            BitmapSource bs = Imaging.CreateBitmapSourceFromHBitmap(intPt, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            im.Source = bs;
            canvasField.Children.Add(im);

            im.SetValue(Canvas.TopProperty, Convert.ToDouble(y));
            im.SetValue(Canvas.LeftProperty, Convert.ToDouble(x));

            DeleteObject(intPt);

        }

        private void upArrow_MouseEnter(object sender, MouseEventArgs e)
        {
            timerSlideUp.Start();
        }

        private void upArrow_MouseLeave(object sender, MouseEventArgs e)
        {
            timerSlideUp.Stop();
        }
        void timerSlideUp_Tick(object sender, EventArgs e)
        {
            scrollImages.LineUp();
        }


        private void downArrow_MouseEnter(object sender, MouseEventArgs e)
        {
            timerSlideDown.Start();
        }

        private void downArrow_MouseLeave(object sender, MouseEventArgs e)
        {
            timerSlideDown.Stop();
        }
        void timerSlideDown_Tick(object sender, EventArgs e)
        {
            scrollImages.LineDown();
        }



       
    }
}
