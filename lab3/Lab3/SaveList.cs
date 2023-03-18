using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Lab3
{
    public static class SaveList<T>
    {
        public static void WriteList(T listItems, string path)
        {

            StreamWriter sw = new StreamWriter(path);
            sw.Write(JsonSerializer.Serialize(listItems, new JsonSerializerOptions() { WriteIndented = true }));
            sw.Close();
        }

        public static T ReadList(string path)
        {
            if (!File.Exists(path))
            {
                return default(T);
            }

            StreamReader sr = new StreamReader(path);
            string json = sr.ReadToEnd();
            sr.Close();
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions() { WriteIndented = true });
        }
    }
}
