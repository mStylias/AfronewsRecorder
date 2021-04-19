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
            foreach (var outputDevice in ScreenRecorder.AudioOutputDevices)
            {
                ComboboxOutputDevice.Items.Add(outputDevice.Value);
            }

            if (Settings.AudioOutputDevice != null)
            {
                ComboboxOutputDevice.SelectedItem = Settings.AudioOutputDevice;
                Trace.WriteLine(Settings.AudioOutputDevice);
                UpdateAudioOutputDevice();
            }
        }

        private void InitializeUI()
        {
            string recPath = Serializer.DeserializeRecordingPath();
            if (recPath != null)
            {
               TextBlockRecordingPath.Text = "Recording Path: " + recPath;
            }
            else
            {
                TextBlockRecordingPath.Text = "Recording Path: " + ScreenRecorder.VideoDirectory;
            }
        }

        private void UpdateAudioOutputDevice()
        {
            if (_componentsLoaded)
            {
                Trace.WriteLine(ComboboxOutputDevice.Text);
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

        private void ComboboxOutputDevice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_componentsLoaded)
            {
                string comboboxText = (sender as ComboBox).SelectedItem as string;
                Trace.WriteLine(comboboxText);
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

        }

        private void FPS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        
      
        private void WindowRecord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (sender as ComboBox).SelectedItem as string;
           var window =  ScreenRecorder.Windows.Find(w =>  w.Title == text);
            if (window != null)
            {
                ScreenRecorder.IsWindowRecording = true;
                ScreenRecorder.WindowHandle = window.Handle;
            }else
            {
                ScreenRecorder.IsWindowRecording = false;
            }
        }

        private void ButtonBrowsePath_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();

            if (dialog.ShowDialog().GetValueOrDefault())
            {
                ScreenRecorder.VideoDirectory = dialog.SelectedPath;
                TextBlockRecordingPath.Text = "Recording path: " + dialog.SelectedPath;
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
    }
}
