using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AfronewsRecorder
{
    public static class Serializer
    {
        public static string DataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                                                    "OneButtonRecorder");
        static Serializer()
        {
            Directory.CreateDirectory(DataDirectory);
        }

        public static void SerializeRecordingPath(string recordingPath)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Path.Combine(DataDirectory, "rec_path.obr"), FileMode.OpenOrCreate, FileAccess.Write);

            formatter.Serialize(stream, recordingPath);

            stream.Close();
        }

        public static string DeserializeRecordingPath()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Path.Combine(DataDirectory, "rec_path.obr"), FileMode.OpenOrCreate, FileAccess.Read);

            string recPath;
            if (stream.Length != 0)
            {
                recPath = (string)formatter.Deserialize(stream);
            }
            else
            {
                recPath = null;
            }

            stream.Close();
            return recPath;
        }
        public static void SerializeAudioOutput(string outputDevice)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Path.Combine(DataDirectory, "rec_output_device.obr"), FileMode.OpenOrCreate, FileAccess.Write);

            formatter.Serialize(stream, outputDevice);

            stream.Close();
        }

        public static string DeserializeAudioOutput()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Path.Combine(DataDirectory, "rec_output_device.obr"), FileMode.OpenOrCreate, FileAccess.Read);

            string outputDevice;
            Trace.WriteLine(stream.Length);
            if (stream.Length != 0)
            {
                outputDevice = (string)formatter.Deserialize(stream);
            }
            else
            {
                outputDevice = null;
            }

            stream.Close();
            return outputDevice;
        }


        public static void SerializeAudioInput(string outputDevice)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Path.Combine(DataDirectory, "rec_input_device.obr"), FileMode.OpenOrCreate, FileAccess.Write);

            formatter.Serialize(stream, outputDevice);

            stream.Close();
        }

        public static string DeserializeAudioInput()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Path.Combine(DataDirectory, "rec_input_device.obr"), FileMode.OpenOrCreate, FileAccess.Read);

            string inputDevice;
            Trace.WriteLine(stream.Length);
            if (stream.Length != 0)
            {
                inputDevice = (string)formatter.Deserialize(stream);
            }
            else
            {
                inputDevice = null;
            }

            stream.Close();
            return inputDevice;
        }

        public static void SerializeFramerateInput(string framerate)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Path.Combine(DataDirectory, "rec_framerate.obr"), FileMode.OpenOrCreate, FileAccess.Write);

            formatter.Serialize(stream, framerate);

            stream.Close();
        }

        public static string DeserializeFramerateInput()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Path.Combine(DataDirectory, "rec_framerate.obr"), FileMode.OpenOrCreate, FileAccess.Read);

            string framerate;
            Trace.WriteLine(stream.Length);
            if (stream.Length != 0)
            {
                framerate = (string)formatter.Deserialize(stream);
            }
            else
            {
                framerate = null;
            }

            stream.Close();
            return framerate;
        }
    }
}
