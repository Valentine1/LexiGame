using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LexiGame.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : baseWindow, IMainView
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeCommandsForMenu();

        }

        #region InitializeCommandsBinding
        private void InitializeCommandsForMenu()
        {
            CommandBinding StartBinding = new CommandBinding(GameCommands.Start);
            StartBinding.Executed += startCommand_Executed;
            StartBinding.CanExecute += StartCommand_CanExecute;
            this.CommandBindings.Add(StartBinding);

            CommandBinding ShowThemeBinding = new CommandBinding(GameCommands.ShowThemes);
            ShowThemeBinding.Executed += new ExecutedRoutedEventHandler(ShowThemeCommand_Executed);
            ShowThemeBinding.CanExecute += new CanExecuteRoutedEventHandler(ShowThemeCommand_CanExecute);
            this.CommandBindings.Add(ShowThemeBinding);

            CommandBinding ShowSettingsBinding = new CommandBinding(GameCommands.ShowSettings);
            ShowSettingsBinding.Executed += new ExecutedRoutedEventHandler(ShowSettingsBinding_Executed);
            ShowSettingsBinding.CanExecute += new CanExecuteRoutedEventHandler(ShowSettingsBinding_CanExecute);
            this.CommandBindings.Add(ShowSettingsBinding);

            CommandBinding ShowStatisticBinding = new CommandBinding(GameCommands.ShowStatistic);
            ShowStatisticBinding.Executed += new ExecutedRoutedEventHandler(ShowStatisticBinding_Executed);
            ShowStatisticBinding.CanExecute += new CanExecuteRoutedEventHandler(ShowStatisticBinding_CanExecute);
            this.CommandBindings.Add(ShowStatisticBinding);

            CommandBinding ExitBinding = new CommandBinding(GameCommands.Exit);
            ExitBinding.Executed += new ExecutedRoutedEventHandler(ExitBinding_Executed);
            ExitBinding.CanExecute += new CanExecuteRoutedEventHandler(ExitBinding_CanExecute);
            this.CommandBindings.Add(ExitBinding);
        }

        void ExitBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        void ExitBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        void ShowStatisticBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        void ShowStatisticBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Unavailable in Demo");
        }

        void StartCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        void startCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.OnGameStarted != null)
            {
                this.OnGameStarted();
            }
        }

        void ShowThemeCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

            e.CanExecute = true;
        }
        void ShowThemeCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.OnThemesShown != null)
            {
                this.OnThemesShown();
            }
        }

        void ShowSettingsBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        void ShowSettingsBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (OnSettingsShown != null)
            {
                OnSettingsShown();
            }
        }

        #endregion

        #region  ***public interfaces for presenter

        public event GameStarted OnGameStarted;
        public event ThemesShown OnThemesShown;
        public event SettingsShown OnSettingsShown;
        public void ClearOwnedWindows()
        {
            foreach (Window win in this.OwnedWindows)
            {

                win.Close();
            }

        }
        public override void SetDynamicResources()
        {
            foreach (Window win in this.OwnedWindows)
            {

                ((IView)win).SetDynamicResources();
            }
        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.AjustChildWindowLoacation();
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            AjustChildWindowLoacation();
        }


        
    }
}
