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
                System.Diagnostics.Process.Start(url); 
            }));
            task.Start();
        }
    }
}
