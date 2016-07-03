using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWebHelper
{
    public class OpenUrl
    {
        public static void OpenUrlDefault(string url)
        {
            Task task = new Task(new Action(() =>
            {
                RegistryKey key = Registry.ClassesRoot.OpenSubKey("http\\shell\\open\\command", false);
                String path = key.GetValue("").ToString();
                if (path.Contains("\""))
                {
                    path = path.TrimStart('"');
                    path = path.Substring(0, path.IndexOf('"'));
                }
                key.Close();
                if (path == "" || path == null)
                {
                    path = "iexplore.exe";
                }
                Process.Start(path, url); 
            }));
            task.Start();
        }
    }
}
