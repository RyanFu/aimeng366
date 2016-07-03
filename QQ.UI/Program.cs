using All.Helper;
using QQ.EF;
using QQ.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace QQ.UI
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                new RegisterQQ();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
            }
            catch (Exception e)
            {
                LogHelper.error(e.Message);
                MessageBox.Show(e.Message);
            }
        }
    }
}
