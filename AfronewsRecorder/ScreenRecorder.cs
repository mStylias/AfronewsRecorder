using ScreenRecorderLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace AfronewsRecorder
{
    public class ScreenRecorder
    {
        public static Dictionary<string, string> AudioInputDevices { get => Recorder.GetSystemAudioDevices(AudioDeviceSource.InputDevices); }
        public static Dictionary<string, string> AudioOutputDevices { get => Recorder.GetSystemAudioDevices(AudioDeviceSource.OutputDevices); }
        public static string RecordingPath { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AfronewsRecorder", "temp.mp4");
        public static string VideoDirectory { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AfronewsRecorder");
        public static string SelectedInputDevice { get { return Options.AudioOptions.AudioInputDevice; } set { Options.AudioOptions.AudioInputDevice = value; } }
        public static string SelectedOutputDevice { get { return Options.AudioOptions.AudioOutputDevice; } set { Options.AudioOptions.AudioOutputDevice = value; } }

        public List<RecordableWindow> RecordableWindows { get => Recorder.GetWindows(); }
        public bool IsRecording { get; set; } = false;

        public static RecorderOptions Options { get; set; } = new RecorderOptions
        {
            RecorderMode = RecorderMode.Video,
            //If throttling is disabled, out of memory exceptions may eventually crash the program,
            //depending on encoder settings and system specifications.
            IsThrottlingDisabled = false,
            //Hardware encoding is enabled by default.
            IsHardwareEncodingEnabled = true,
            //Low latency mode provides faster encoding, but can reduce quality.
            IsLowLatencyEnabled = true,
            //Fast start writes the mp4 header at the beginning of the file, to facilitate streaming.
            IsMp4FastStartEnabled = false,

            AudioOptions = new AudioOptions
            {
                Bitrate = AudioBitrate.bitrate_128kbps,
                Channels = AudioChannels.Stereo,
                IsAudioEnabled = true,
                IsInputDeviceEnabled = false,
                IsOutputDeviceEnabled = true,
                AudioInputDevice = null, // Null means default
                AudioOutputDevice = null
            },

            VideoOptions = new VideoOptions
            {
                BitrateMode = BitrateControlMode.Quality,
                Quality = 70,
                Framerate = 30,
                IsFixedFramerate = true,
                EncoderProfile = H264Profile.Main
            },

            RecorderApi = RecorderApi.WindowsGraphicsCapture
        };

        Recorder _recorder;
        DateTime _startTime;
        public void CreateRecording()
        {
            _recorder = Recorder.CreateRecorder(Options);
            _recorder.OnRecordingComplete += Rec_OnRecordingComplete;
            _recorder.OnRecordingFailed += Rec_OnRecordingFailed;
            _recorder.OnStatusChanged += Rec_OnStatusChanged;

            _startTime = DateTime.Now;

            //Record to a file
            if (File.Exists(RecordingPath))
            {
                File.Delete(RecordingPath);
            }

            _recorder.Record(RecordingPath);

            IsRecording = true;
        }
   

        public void EndRecording()
        {
            _recorder.Stop();
        }

        private void Rec_OnRecordingComplete(object sender, RecordingCompleteEventArgs e)
        {
            //Get the file path if recorded to a file
            Trace.WriteLine("Successfully saved recording!");
            string videoName = _startTime.ToString("yy-MM-dd hh-mm-ss");
            string newFile = Path.Combine(VideoDirectory, videoName + ".mp4");

            try
            {
                Directory.CreateDirectory(VideoDirectory);
                File.Move(RecordingPath, newFile, true);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                Trace.WriteLine(RecordingPath);
                Trace.WriteLine(newFile);
                IsRecording = false;
                return;
            }

            IsRecording = false;
        }

        private void Rec_OnRecordingFailed(object sender, RecordingFailedEventArgs e)
        {
            Trace.WriteLine(e.Error);
            IsRecording = false;
        }

        private void Rec_OnStatusChanged(object sender, RecordingStatusEventArgs e)
        {
            RecorderStatus status = e.Status;
            Trace.WriteLine(status);
        }
    }
}
