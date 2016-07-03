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
using System.Windows.Shapes;
using MyWindow;
using SqliteORM;
using System.Reflection;
using wpfClient.Manager;
using wpfClient.Model;
using ClientWebHelper;
using System.Diagnostics;
using All.Helper;
using All.Core;
using JobManager.job;

namespace 艾梦小说更新提醒器
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : WindowBase
    {
        List<BookMessage> data = new List<BookMessage>();
        int pageIndex = 0;
        int pageSize = 10;
        Where where;
        BookMessageManager service = new BookMessageManager();

        public MainWindow()
        {
            InitializeComponent();
            this.listBox.ItemsSource = data;
            NotifyIconManager.NowWindow = this;
            MonitorDispatcher.OnBookMsgLoaded += RegisterUpdateMsg;
            //打开任务调度
            JobManager.JobManager.Instance.StartJobServer();
            //启动的时候更新一次消息
            RegisterUpdateMsg();
        }

        /// <summary>
        /// 注册消息更新
        /// </summary>
        public void RegisterUpdateMsg()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                pageIndex = 0;
                if (service.Count() > 0)
                {
                    NotifyIconManager.StartShan();
                    
                    SoundPlayHelper.Play(艾梦小说更新提醒器.Properties.Resources._10);
                    
                }
                BindData();
            }));

        }

        /// <summary>
        /// 更多更新信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HypeLinkMore(object sender, RoutedEventArgs e)
        {
            var obj = sender as Hyperlink;
            if (obj.Tag.ToString() == "没有消息")
            {
                return;
            }
            var temp = obj.Parent as TextBlock;
            if (temp != null)
            {
                var book = data.FirstOrDefault(r => r.Id == Convert.ToInt32(temp.Tag));
                book.IsRead = true;
                service.UpdateReadState((int)book.Id, true);
                BindData(where, false);
            }

            var url = "http://tieba.baidu.com/f?kw=" + obj.Tag.ToString() + "&fr=index";
            OpenUrl.OpenUrlDefault(url);
        }
        private void BookName_MouseEnter(object sender, MouseEventArgs e)
        {
            var obj = sender as Label;
            if (obj != null)
            {
                obj.Foreground = new SolidColorBrush(Color.FromRgb(200, 0, 0));
                obj.Cursor = Cursors.Hand;
            }
        }
        private void BookName_MouseLeave(object sender, MouseEventArgs e)
        {
            var obj = sender as Label;
            if (obj != null)
            {
                obj.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
        }

        private void lastChapter_MouseEnter(object sender, MouseEventArgs e)
        {
            var obj = sender as Label;
            if (obj != null)
            {
                obj.Foreground = new SolidColorBrush(Color.FromRgb(200, 0, 0));
                obj.Cursor = Cursors.Hand;
            }
        }

        private void lastChapter_MouseLeave(object sender, MouseEventArgs e)
        {
            var obj = sender as Label;
            if (obj != null)
            {
                obj.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
        }

        private void BookName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var conl = sender as Label;
            if (conl != null)
            {
                var obj = data.FirstOrDefault(r => r.Id == Convert.ToInt32(conl.Tag));
                if (obj.Url == "" || obj.Url == null)
                {
                    return;
                }
                obj.IsRead = true;
                service.UpdateReadState((int)obj.Id, true);
                BindData(where, false);
                OpenUrl.OpenUrlDefault(obj.Url);
            }
        }

        private void lastChapter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var conl = sender as Label;
            if (conl != null)
            {
                var obj = data.FirstOrDefault(r => r.Id == Convert.ToInt32(conl.Tag));
                if (obj.LastChapterUrl == "" || obj.LastChapterUrl == null)
                {
                    return;
                }
                obj.IsRead = true;
                service.UpdateReadState((int)obj.Id, true);
                BindData(where, false);
                OpenUrl.OpenUrlDefault(obj.LastChapterUrl);
            }
        }

        private void TopPage_Click(object sender, RoutedEventArgs e)
        {
            if (pageIndex == 0)
            {
                return;
            }
            pageIndex--;
            BindData(where, false);
        }

        private void DownPage_Click(object sender, RoutedEventArgs e)
        {
            if (data.Count == 0 || data.Count < pageSize)
            {
                return;
            }
            pageIndex++;
            BindData(where, false);
        }

        private void BindData(Where clause = null, bool isResetCheckBook = true)
        {
            if (isResetCheckBook)
            {
                BindCheckBook();
            }
            data = service.GetPage(pageIndex, pageSize, clause);
            if (data.Count == 0)
            {
                data.Add(new BookMessage()
                {
                    Name = "没有消息",
                    AuthorName = "没有消息",
                    IsRead = true
                });
            }
            this.listBox.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.listBox.ItemsSource = data.Select(r => new BindBookMsg
                {
                    Id = r.Id,
                    Name = r.Name,
                    AuthorName = r.AuthorName,
                    ImgUrl = r.ImgUrl,
                    Url = r.Url,
                    LastChapterUrl = r.LastChapterUrl,
                    LastChapter = r.LastChapter,
                    ResourceFromsite = r.ResourceFromsite,
                    IsRead = r.IsRead ? "" : "未读",
                    CreateOn = r.CreateOn
                }).ToList();
                if (listBox.Items.Count > 0)
                {
                    listBox.ScrollIntoView(listBox.Items[0]);
                }
            }));
        }

        private void WindowBase_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void MoreDingyue_Click(object sender, RoutedEventArgs e)
        {
            OpenUrl.OpenUrlDefault("http://aimeng366.com");
        }

        private void CheckBook_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.pageIndex = 0;
            if (this.CheckBook.SelectedItem != null)
            {
                where = Where.Equal("Name", this.CheckBook.SelectedItem.ToString());
                if (this.CheckBook.SelectedItem.ToString() == "全部")
                {
                    where = null;
                }
                CheckBook.Text = CheckBook.SelectedItem.ToString();
                BindData(where, false);
            }


        }

        private void BindCheckBook()
        {
            var data = new List<string>();
            var tempdata = service.GetAllBookNames();
            foreach (var item in tempdata)
            {
                data.Add(item.Name);
            }
            if (data.Count > 0)
            {
                var temp = data[0];
                data[0] = "全部";
                data.Add(temp);
            }
            else
            {
                data.Add("全部");
            }
            this.CheckBook.Dispatcher.Invoke(new Action(() =>
            {
                this.CheckBook.ItemsSource = data;
                this.CheckBook.SelectedIndex = 0; 
                if (listBox.Items.Count > 0)
                {
                    listBox.ScrollIntoView(listBox.Items[0]);
                }
            }));

        }

        private void WindowBase_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            NotifyIconManager.EndShan();
        }
    }

    public class BindBookMsg
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public string ImgUrl { get; set; }
        public string Url { get; set; }
        public string LastChapterUrl { get; set; }
        public string LastChapter { get; set; }
        public string ResourceFromsite { get; set; }
        public string IsRead { get; set; }
        public DateTime CreateOn { get; set; }
    }

    public class BindBookName
    {
        public int Id { get; set; }
        public int Name { get; set; }
    }

}
