using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AfronewsRecorder
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            ShowSettingsSection();
        }

        MainWindow mainWindow = new MainWindow();
        SettingsPage _settingsPage;

        public void ShowSettingsSection()
        {
            if (_settingsPage == null)
                _settingsPage = new SettingsPage();

            Main.Content = _settingsPage;
        }

        #region titlebar buttons
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }


        private void ButtonWindowState_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        #endregion
    }
}
