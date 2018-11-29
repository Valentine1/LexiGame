using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace LexiGame.View
{

    public class GameCommands
    {
       
        static GameCommands()
        {
            // Initialize the command.
            //InputGestureCollection inputs = new InputGestureCollection();
            //inputs.Add(new KeyGesture(Key.R, ModifierKeys.Control, "Ctrl+R"));
            _start = new RoutedUICommand("Start", "Start", typeof(GameCommands));
            // Initialize the command.
            //InputGestureCollection inputs = new InputGestureCollection();
            //inputs.Add(new KeyGesture(Key.R, ModifierKeys.Control, "Ctrl+R"));
            _showThemes = new RoutedUICommand("ShowTheme", "ShowTheme", typeof(GameCommands));
            _showSettings = new RoutedUICommand("ShowSettings", "ShowSettings", typeof(GameCommands));
            _showStatistic = new RoutedUICommand("ShowStatistic", "ShowStatistic", typeof(GameCommands));
            _exit = new RoutedUICommand("Exit", "Exit", typeof(GameCommands));
            _newLexim = new RoutedUICommand("NewLexim", "NewLexim", typeof(GameCommands));
            _saveLexim = new RoutedUICommand("SaveLexim", "SaveLexim", typeof(GameCommands));
            _deleteLexim = new RoutedUICommand("SaveLexim", "SaveLexim", typeof(GameCommands));
            _newTheme = new RoutedUICommand("NewTheme", "NewTheme", typeof(GameCommands));
            _saveTheme = new RoutedUICommand("SaveTheme", "SaveTheme", typeof(GameCommands));
            _deleteTheme = new RoutedUICommand("SaveTheme", "SaveTheme", typeof(GameCommands));
        }
        private static RoutedUICommand _start;
        public static RoutedUICommand Start
        {
            get { return _start; }
        }

        private static RoutedUICommand _showThemes;
        public static RoutedUICommand ShowThemes
        {
            get { return _showThemes; }
        }
        private static RoutedUICommand _showSettings;
        public static RoutedUICommand ShowSettings
        {
            get { return _showSettings; }
        }

        private static RoutedUICommand _showStatistic;
        public static RoutedUICommand ShowStatistic
        {
            get { return _showStatistic; }
        }

        private static RoutedUICommand _newLexim;
        public static RoutedUICommand NewLexim
        {
            get { return _newLexim; }
        }

        private static RoutedUICommand _exit;
        public static RoutedUICommand Exit
        {
            get { return _exit; }
        }

        private static RoutedUICommand _saveLexim;
        public static RoutedUICommand SaveLexim
        {
            get { return _saveLexim; }
        }

        private static RoutedUICommand _deleteLexim;
        public static RoutedUICommand DeleteLexim
        {
            get { return _deleteLexim; }
        }

        private static RoutedUICommand _newTheme;
        public static RoutedUICommand NewTheme
        {
            get { return _newTheme; }
        }

        private static RoutedUICommand _saveTheme;
        public static RoutedUICommand SaveTheme
        {
            get { return _saveTheme; }
        }

        private static RoutedUICommand _deleteTheme;
        public static RoutedUICommand DeleteTheme
        {
            get { return _deleteTheme; }
        }
    }
}
