using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AfronewsRecorder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        #region titlebar buttons
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            if (_recorder.IsRecording)
            {
                var userResponse = MessageBox.Show("Recording is still in progress. Exit anyway?", "Recording is active", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (userResponse == MessageBoxResult.Yes)
                {
                    _recorder.EndRecording();
                    while (_recorder.IsRecording)
                    {
                        Thread.Sleep(1000);
                    }
                    Application.Current.Shutdown();
                }
            }
            else
                Application.Current.Shutdown();      
            
        }


        private void ButtonWindowState_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        #endregion

        #region btnRecord
        ScreenRecorder _recorder = new ScreenRecorder();
        private void ButtonRecord_Click(object sender, RoutedEventArgs e)
        {
            if (_recorder.IsRecording)
            {
                _recorder.EndRecording();
                TextBlockRecordTime.Text = "00:00:00";
                CircleRecord.Fill = new SolidColorBrush(Color.FromRgb(124, 77, 150));
                StopRecordTimer();
            }
            else
            {
                _recorder.CreateRecording();
                CircleRecord.Fill = new SolidColorBrush(Color.FromRgb(212, 28, 34));
                StartRecordTimer();
            } 
        }

        TimeSpan _recordTime;
        DispatcherTimer _timer;
        private void StartRecordTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += TimerRecord_Tick;
            _recordTime = TimeSpan.FromSeconds(0);
            _timer.Start();
        }
        
        private void StopRecordTimer()
        {
            _timer.Stop();
        }

        private void TimerRecord_Tick(object sender, EventArgs e)
        {
            _recordTime = _recordTime.Add(TimeSpan.FromSeconds(1));
            TextBlockRecordTime.Text =  _recordTime.ToString();
        }

        #endregion
        SettingsWindow _settingsWindow;
        private void ButtonOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("explorer.exe", ScreenRecorder.VideoDirectory);
            }
            catch
            {
                MessageBox.Show("Revolut hasn't been activated yet. Refer to more people to continue", "Revolut not refered enough", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {

            if (_settingsWindow == null)
                _settingsWindow = new SettingsWindow();
            _settingsWindow.ShowDialog();
        }

    }
}
