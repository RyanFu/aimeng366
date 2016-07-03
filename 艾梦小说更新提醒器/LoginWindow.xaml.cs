using All.Core;
using ClientWebHelper;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MyWindow;
using All.Helper;
using System.Threading;
using System.Threading.Tasks;
using All.ConfigHelper;
using wpfClient.Manager;

namespace 艾梦小说更新提醒器
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : WindowBase
    {
        UserManager serivce = new UserManager();
        public LoginWindow()
        {
            InitializeComponent();
            NotifyIconManager.NowWindow = this;
            var user = serivce.GetRememberPwdUser();
            if (user != null)
            {
                this.UserCode.Text = user.Code;
                this.Pwd.Password = user.Pwd;
                this.SavePwd.IsChecked = true;
            }
        }

        private void btn_login_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn_login = (Button)sender;
            Label lb1 = (Label)btn_login.Template.FindName("tips_for_login", btn_login);
            lb1.Foreground = new SolidColorBrush(Colors.Red);
        }

        private void btn_login_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn_login = (Button)sender;
            Label lb1 = (Label)btn_login.Template.FindName("tips_for_login", btn_login);
            lb1.Foreground = new SolidColorBrush(Colors.White);
        }
        private bool CheckUser()
        {
            if (UserCode.Text.Trim() == "")
            {
                CMessageBox.Show("请输入帐号!");
                return false;
            }
            if (Pwd.Password.Trim() == "")
            {
                CMessageBox.Show("请输入密码!");
                return false;
            }
            return true;
        }
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!CheckUser())
                {
                    return;
                }
                var service = new HttpService();
                this._loading.Start();
                var code = UserCode.Text.Trim();
                var pwd = Pwd.Password.Trim();
                Task task = new Task(new Action(() =>
                {
                    var rst = service.Login(code, pwd).GetAwaiter().GetResult();
                    this._loading.Dispatcher.Invoke(new Action(() =>
                    {
                        this._loading.Stop();
                    }));
                    if (rst == null)
                    {
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            serivce.SetRememberPwdUser(code,pwd, true);
                            new MainWindow().Show();
                            this.Hide();
                        }));
                    }
                    else
                    {
                        CMessageBox.Show(rst);
                    }
                }));
                task.Start();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        private void btn_regist_Click(object sender, RoutedEventArgs e)
        {
            OpenUrl.OpenUrlDefault("http://aimeng366.com/home/register");
        }
    }
}
