using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using 艾梦小说更新提醒器;

namespace MyWindow
{
    public class WindowBase : MetroWindow
    {
        public WindowBase()
        {
            this.Activated += WindowBase_Activated;
        }

        void WindowBase_Activated(object sender, EventArgs e)
        {
            NotifyIconManager.EndShan();
        }
    }
}