using AfronewsRecorder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AfronewsRecorder.Structure
{
    public static class Settings
    {
        public static string VideoDirectory { get; set; } = VideoDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Auto Lecture Recorder");
        public static string AudioOutputDevice { get; set; }
        public static string AudioInputDevice { get; set; }
        public static string Framerate { get; set; }


        static Settings()
        {
            Framerate = Serializer.DeserializeFramerateInput();
            AudioInputDevice = Serializer.DeserializeAudioInput();
            AudioOutputDevice = Serializer.DeserializeAudioOutput();
        }
    }
}
