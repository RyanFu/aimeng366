using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using 艾梦小说更新提醒器.Properties;

namespace 艾梦小说更新提醒器
{
    public static class NotifyIconManager
    {
        static NotifyIcon _notifyIcon;

        static System.Timers.Timer timer;

        static System.Drawing.Icon youIcon = Resources.bitbug_favicon;
        static System.Drawing.Icon kongIcon = Resources.kong;

        public static MetroWindow NowWindow;
        static NotifyIconManager()
        {
            if (timer == null)
            {
                timer = new System.Timers.Timer(500);
                timer.Elapsed += timer_Elapsed;
            }
            if (_notifyIcon == null)
            {
                _notifyIcon = new NotifyIcon();
                _notifyIcon = new NotifyIcon();
                _notifyIcon.BalloonTipText = "艾梦小说更新提醒器";
                _notifyIcon.ShowBalloonTip(2000);
                _notifyIcon.Text = "艾梦小说更新提醒器";
                _notifyIcon.Icon = youIcon;
                _notifyIcon.Visible = true;
                //退出菜单项
                System.Windows.Forms.MenuItem exit = new System.Windows.Forms.MenuItem("退出");
                exit.Click += exit_Click;
                _notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] { exit });
                _notifyIcon.DoubleClick += _notifyIcon_DoubleClick;

            }
        }
        static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_notifyIcon != null)
            {
                if (_notifyIcon.Icon == youIcon)
                {
                    _notifyIcon.Icon = kongIcon;
                }
                else
                {
                    _notifyIcon.Icon = youIcon;
                }
            }
        }
        public static NotifyIcon NotifyIcon
        {
            get
            {
                if (_notifyIcon == null)
                {
                    _notifyIcon = new NotifyIcon();
                    _notifyIcon = new NotifyIcon();
                    _notifyIcon.BalloonTipText = "艾梦小说更新提醒器";
                    _notifyIcon.ShowBalloonTip(2000);
                    _notifyIcon.Text = "艾梦小说更新提醒器";
                    _notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
                    _notifyIcon.Visible = true;
                    //退出菜单项
                    System.Windows.Forms.MenuItem exit = new System.Windows.Forms.MenuItem("退出");
                    exit.Click += exit_Click;
                    _notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] { exit });
                    _notifyIcon.Click += _notifyIcon_DoubleClick;

                }
                return _notifyIcon;
            }
            set
            {
                _notifyIcon = value;
            }
        }

        static void exit_Click(object sender, EventArgs e)
        {
            JobManager.JobManager.Instance.EndJobServier();
            System.Windows.Application.Current.Shutdown();
        }

        static void _notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (NowWindow != null)
            {
                try
                {
                    NowWindow.Focus();
                    NowWindow.Topmost = true;
                    NowWindow.Show();
                    NowWindow.WindowState = System.Windows.WindowState.Normal;
                    EndShan();
                    NowWindow.Topmost = false;
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// 开始闪动
        /// </summary>
        public static void StartShan()
        {
            if (timer!=null)
            {
                timer.Start();
            }
        }
        /// <summary>
        /// 停止闪动
        /// </summary>
        public static void EndShan()
        {
            if (timer!=null)
            {
                timer.Stop();
                _notifyIcon.Icon = youIcon;
            }
        }


    }
}
