﻿using AfronewsRecorder.Structure;
using Ookii.Dialogs.Wpf;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ScreenRecorderLib;

namespace AfronewsRecorder
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
            _componentsLoaded = true;
            LoadData();
            InitializeUI();
        }

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

            // framerate
            if(Settings.Framerate != null)
                 UpdateFramerate();

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
                string comboboxText = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content as string;
                Trace.WriteLine("Combobox text is: " + comboboxText);
                

                if (comboboxText != null)
                {
                    Settings.Framerate = comboboxText;
                    ScreenRecorder.FramerateInput = Int32.Parse(comboboxText);
                    Serializer.SerializeFramerateInput(comboboxText);
                }
                else
                {
                    comboboxText = "30";
                    Settings.Framerate = comboboxText;
                    ScreenRecorder.FramerateInput = 30;
                    Serializer.SerializeFramerateInput(comboboxText);
                }
            }
        }

        private void UpdateFramerate()
        {
            if (_componentsLoaded)
            {
                Trace.WriteLine("Framerate " + ComboBoxFramerate.Text);
                string framerate = Settings.Framerate;

                if (!string.IsNullOrEmpty(Settings.Framerate))
                {
                    (ComboBoxFramerate.SelectedItem as ComboBoxItem).Content = framerate;
                    ScreenRecorder.FramerateInput = Int32.Parse(framerate);
                    Serializer.SerializeFramerateInput(framerate);
                }
                else
                {
                    framerate = "30";
                    (ComboBoxFramerate.SelectedItem as ComboBoxItem).Content = framerate;
                    ScreenRecorder.FramerateInput = 30;
                    Serializer.SerializeFramerateInput(framerate);
                }
            }
            
        }


        private void WindowRecord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (sender as ComboBox).SelectedItem as string;
            var window =  ScreenRecorder.Windows.Find(w =>  w.Title == text);
            if (window != null)
            {
                ScreenRecorder.IsWindowRecordingSelected = true;
                ScreenRecorder.WindowHandle = window.Handle;
            }else
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
            foreach(var window in ScreenRecorder.Windows)
            {
                WindowRecord.Items.Add(window.Title);
            }
        }



        private void ToggleButtonOutput_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)ToggleButtonOutput.IsChecked)
            {
                ScreenRecorder.IsOutputDeviceEnabled = true;
            }
            else
            {
                ScreenRecorder.IsInputDeviceEnabled = false;
            }
        }

        private void ToggleButtonInput_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)ToggleButtonInput.IsChecked)
            {
                ScreenRecorder.IsInputDeviceEnabled = true;
            }
            else
            {
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
    }
}
