// Author(s): Jacob Morris
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{
    public class Settings
    {
        private string path;
        private Dictionary<string, string> data;

        public string Path
        {
            get { return path; }
        }

        public Dictionary<string, string> Data
        {
            get { return data; }
            set { data = value; }
        }

        public Settings(string path)
        {
            this.path = path;
            data = new Dictionary<string, string>();
        }

        public void Load(Dictionary<string,string> defaultData = null)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while((line = reader.ReadLine()) !=null)
                    {
                        string[] parts = line.Split('=');
                        data.Add(parts[0], parts[1]);
                    }
                }
            }
            catch (Exception e)
            {
                data = defaultData;
                Save();
            }
        }

        public void Save()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    foreach (string key in data.Keys)
                    {
                        writer.WriteLine(key + "=" + data[key]);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public T Get<T>(string key)
        {
            if (!data.ContainsKey(key))
                return (T)Convert.ChangeType(null, typeof(T));
            return (T) Convert.ChangeType(data[key], typeof(T));
        }
    }
}
