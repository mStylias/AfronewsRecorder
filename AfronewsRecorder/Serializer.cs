using System;
using System.Collections.Generic;
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
                                                                    "AfronewsRecorder");
        static Serializer()
        {
            Directory.CreateDirectory(DataDirectory);
        }

        public static void SerializeRecordingPath(string recordingPath)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Path.Combine(DataDirectory, "rec_path.anr"), FileMode.OpenOrCreate, FileAccess.Write);

            formatter.Serialize(stream, recordingPath);

            stream.Close();
        }

        public static string DeserializeRecordingPath()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Path.Combine(DataDirectory, "rec_path.anr"), FileMode.OpenOrCreate, FileAccess.Read);

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
    }
}
