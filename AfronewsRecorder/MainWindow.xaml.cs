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
            InitializeUI();
        }

        private void InitializeUI()
        {
            TextBlockRecordTime.Visibility = Visibility.Visible;
            string recPath = Serializer.DeserializeRecordingPath();
            //if (recPath != null)
            //{
            //    TextBlockRecordingPath.Text = "Recording Path: " + recPath;
            //}
            //else
            //{
            //    TextBlockRecordingPath.Text = "Recording Path: " + ScreenRecorder.VideoDirectory;
            //}
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


        ScreenRecorder _recorder = new ScreenRecorder();
        private void ButtonRecord_Click(object sender, RoutedEventArgs e)
        {
            if (_recorder.IsRecording)
            {
                _recorder.EndRecording();
                TextBlockRecordTime.Text = "REC: 00:00:00";
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
            TextBlockRecordTime.Text = "REC: " + _recordTime.ToString();
        }

        //private void ButtonBrowse_Click(object sender, RoutedEventArgs e)
        //{
        //    VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();

        //    if (dialog.ShowDialog().GetValueOrDefault())
        //    {
        //        ScreenRecorder.VideoDirectory = dialog.SelectedPath;
        //        TextBlockRecordingPath.Text = "Recording path: " + dialog.SelectedPath;
        //        Serializer.SerializeRecordingPath(dialog.SelectedPath);
        //    }
        //}

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

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            //TaskDialog taskDialog = new TaskDialog();
            //taskDialog.WindowTitle = "Recording is active";
            //taskDialog.Content = "Recording is still in progress. Do you want to exit anyway?";
            //taskDialog.Buttons.Add(new TaskDialogButton("Yes"));
            //taskDialog.Buttons.Add(new TaskDialogButton("No"));
            //taskDialog.ButtonClicked += new EventHandler((sender, e) => TaskDialog_ButtonClicked());
            //taskDialog.ShowDialog();
            if (_recorder.IsRecording)
            {
                var userResponse = MessageBox.Show("Recording is still in progress. Exit anyway?", "Recording is active", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (userResponse == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    _recorder.EndRecording();
                    while (_recorder.IsRecording)
                    {
                        Thread.Sleep(1000);
                    }

                }
            }

        }
    }
}
