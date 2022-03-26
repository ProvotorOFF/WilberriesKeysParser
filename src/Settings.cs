using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Settings
{
    internal class Settings
    {
        public static bool UseCustomPath()
        {
            try
            {
                StreamReader reader = new StreamReader("settings.txt");
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains("USE_CUSTOM_BROWSER_PATH") && line.Contains("true"))
                {
                    return true;
                }
            }
            }
            catch { return false; }
            return false;
        }

        public static string CustomPath()
        {
            if (UseCustomPath())
            {
                StreamReader reader = new StreamReader("settings.txt");
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("BROWSER_PATH_VALUE "))
                    {
                        Console.WriteLine(line.Substring(21));
                        return line.Substring(21);
                    }
                }

            }
            return null;
        }
    }
}
