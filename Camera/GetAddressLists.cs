using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Camera
{
    public static class GlobalData
    {
        public static List<string> StringList { get; private set; }

        public static void LoadJSONFile(string filePath)
        {
            try
            {
                string jsonData = File.ReadAllText(filePath);
                StringList = JsonConvert.DeserializeObject<List<string>>(jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading JSON file: " + ex.Message);
            }
        }
    }
}
