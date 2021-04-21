using AfronewsRecorder.Structure;
using Ookii.Dialogs.Wpf;
using ScreenRecorderLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            _componentsLoaded = true;
            LoadData();
            InitializeUI();
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

        #region Settings Logic

        bool _componentsLoaded = false;
        private void LoadData()
        {
            //output devices
            foreach (var outputDevice in ScreenRecorder.AudioOutputDevices)
            {
                ComboboxOutputDevice.Items.Add(outputDevice.Value);
            }

            if (Settings.AudioOutputDevice != null)
            {
                ComboboxOutputDevice.SelectedItem = Settings.AudioOutputDevice;
                Trace.WriteLine("Audio output device: " + Settings.AudioOutputDevice);
                UpdateAudioOutputDevice();
            }

            //input devices
            foreach (var inputDevice in ScreenRecorder.AudioInputDevices)
            {
                ComboboxInputDevice.Items.Add(inputDevice.Value);
            }

            if (Settings.AudioInputDevice != null)
            {
                ComboboxInputDevice.SelectedItem = Settings.AudioInputDevice;
                Trace.WriteLine("Audio input device: " + Settings.AudioInputDevice);
                UpdateAudioInputDevice();
            }

            ComboBoxFramerate.Items.Add("30");
            ComboBoxFramerate.Items.Add("45");
            ComboBoxFramerate.Items.Add("60");
            ComboBoxFramerate.Items.Add("120");

            if (Settings.Framerate != null)
                UpdateFramerate(ComboBoxFramerate);

        }

        private void InitializeUI()
        {
            string recPath = Serializer.DeserializeRecordingPath();
            if (recPath != null)
            {
                TextBlockRecordingPath.Text = "Save to: " + recPath;
            }
            else
            {
                TextBlockRecordingPath.Text = "Save to: " + ScreenRecorder.VideoDirectory;
            }

        }

        private void UpdateAudioOutputDevice()
        {
            if (_componentsLoaded)
            {
                string selectedDevice = ScreenRecorder.AudioOutputDevices.FirstOrDefault(x => x.Value == ComboboxOutputDevice.Text).Key;

                if (selectedDevice != null)
                {
                    ScreenRecorder.SelectedOutputDevice = selectedDevice;
                    Serializer.SerializeAudioOutput(ComboboxOutputDevice.Text);
                }
                else
                {
                    ScreenRecorder.SelectedOutputDevice = null;
                    Serializer.SerializeAudioOutput("Default");
                }
            }
        }

        private void UpdateAudioInputDevice()
        {
            if (_componentsLoaded)
            {
                string selectedDevice = ScreenRecorder.AudioOutputDevices.FirstOrDefault(x => x.Value == ComboboxOutputDevice.Text).Key;

                if (selectedDevice != null)
                {
                    ScreenRecorder.SelectedInputDevice = selectedDevice;
                    Serializer.SerializeAudioOutput(ComboboxOutputDevice.Text);
                }
                else
                {
                    ScreenRecorder.SelectedInputDevice = null;
                    Serializer.SerializeAudioOutput("Default");
                }
            }
        }

        private void ComboboxOutputDevice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_componentsLoaded)
            {
                string comboboxText = (sender as ComboBox).SelectedItem as string;
                string selectedDevice = ScreenRecorder.AudioOutputDevices.FirstOrDefault(x => x.Value == comboboxText).Key;

                if (selectedDevice != null)
                {
                    ScreenRecorder.SelectedOutputDevice = selectedDevice;
                    Serializer.SerializeAudioOutput(comboboxText);
                }
                else
                {
                    ScreenRecorder.SelectedOutputDevice = null;
                    Serializer.SerializeAudioOutput("Default");
                }
            }
        }

        private void ComboboxInputDevice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_componentsLoaded)
            {
                string comboboxText = (sender as ComboBox).SelectedItem as string;
                string selectedDevice = ScreenRecorder.AudioInputDevices.FirstOrDefault(x => x.Value == comboboxText).Key;

                if (selectedDevice != null)
                {
                    ScreenRecorder.SelectedInputDevice = selectedDevice;
                    Serializer.SerializeAudioInput(comboboxText);
                }
                else
                {
                    ScreenRecorder.SelectedInputDevice = null;
                    Serializer.SerializeAudioInput("Default");
                }
            }
        }

        private void FPS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_componentsLoaded)
            {
                string text = (sender as ComboBox).SelectedItem as string;
                Trace.WriteLine("Combobox text is: " + text);


                if (!string.IsNullOrEmpty(text))
                {
                    Settings.Framerate = text;
                    ComboBoxFramerate.SelectedItem = text;
                    ScreenRecorder.Options.VideoOptions.Framerate = Int32.Parse(text);
                    Serializer.SerializeFramerateInput(text);
                }
            }
        }

        private void UpdateFramerate(object sender)
        {
            if (_componentsLoaded)
            {
                Trace.WriteLine("Framerate " + ComboBoxFramerate.Text);
                ComboBoxFramerate.SelectedItem = Settings.Framerate;
                ScreenRecorder.Options.VideoOptions.Framerate = Int32.Parse(Settings.Framerate);
                Serializer.SerializeFramerateInput(Settings.Framerate);
            }

        }


        private void WindowRecord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (sender as ComboBox).SelectedItem as string;
            var window = ScreenRecorder.Windows.Find(w => w.Title == text);
            if (window != null)
            {
                ScreenRecorder.IsWindowRecordingSelected = true;
                ScreenRecorder.RecordableWindow = window;
            }
            else
            {
                ScreenRecorder.IsWindowRecordingSelected = false;
            }
        }

        private void ButtonBrowsePath_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();

            if (dialog.ShowDialog().GetValueOrDefault())
            {
                ScreenRecorder.VideoDirectory = dialog.SelectedPath;
                TextBlockRecordingPath.Text = "Save to: " + dialog.SelectedPath;
                Serializer.SerializeRecordingPath(dialog.SelectedPath);
            }
        }

        private void WindowRecord_DropDownOpened(object sender, EventArgs e)
        {
            WindowRecord.Items.Clear();
            ScreenRecorder.Windows = Recorder.GetWindows();
            foreach (var window in ScreenRecorder.Windows)
            {
                WindowRecord.Items.Add(window.Title);
            }
        }



        private void ToggleButtonOutput_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)ToggleButtonOutput.IsChecked)
            {
                ComboboxOutputDevice.Visibility = Visibility.Visible;
                ScreenRecorder.IsOutputDeviceEnabled = true;
            }
            else
            {
                ComboboxOutputDevice.Visibility = Visibility.Hidden;
                ScreenRecorder.IsInputDeviceEnabled = false;
            }
        }

        private void ToggleButtonInput_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)ToggleButtonInput.IsChecked)
            {
                ComboboxInputDevice.Visibility = Visibility.Visible;
                ScreenRecorder.IsInputDeviceEnabled = true;
            }
            else
            {
                ComboboxInputDevice.Visibility = Visibility.Hidden;
                ScreenRecorder.IsInputDeviceEnabled = false;
            }
        }

        private void ToggleButtonWindowRecord_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)ToggleButtonWindowRecord.IsChecked)
            {
                WindowRecord.Visibility = Visibility.Visible;
            }
            else
            {
                WindowRecord.Visibility = Visibility.Hidden;
            }
        }
    #endregion
}
}
