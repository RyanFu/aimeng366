using SqliteORM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using wpfClient.Manager;
using wpfClient.Model;

namespace 艾梦小说更新提醒器
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //初始化数据库
            DbConnection.Initialise("Data Source = " + System.AppDomain.CurrentDomain.BaseDirectory + "data\\data.db;pooling=true", Assembly.Load("wpfClient.Model"));
            Updater.CheckUpdateStatus();
            //初始化窗口
            new LoginWindow().Show();
        }
    }
}
