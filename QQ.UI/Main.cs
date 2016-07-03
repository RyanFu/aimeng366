using Db.EF;
using Db.EF.DbModel;
using QQ.Model;
using QQ.WebService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QQ.UI
{
    public partial class Main : Form
    {

        
        public Main()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var qq = txtQQ.Text.Trim();
                if (qq == "")
                {
                    MessageBox.Show("请输入qq号！");
                    return;
                }
                if (txtptwebqq.Text.Trim() == "" || txtpsessionid.Text.Trim() == "" || txtclientid.Text.Trim() == "")
                {
                    MessageBox.Show("信息不完整！");
                    return;
                }
                QQHelper qqHelper = null;
                if (QQHelperManager.qqHelpers.Keys.FirstOrDefault(r => r == qq) != null)
                {
                    qqHelper = QQHelperManager.qqHelpers[qq];
                }
                if (qqHelper == null)
                {
                    qqHelper = new QQHelper(qq);
                    QQHelperManager.qqHelpers.Add(qq, qqHelper);
                }
                qqHelper.ptwebqq = txtptwebqq.Text.Trim();
                qqHelper.psessionid = txtpsessionid.Text.Trim();
                qqHelper.clientid = txtclientid.Text.Trim();
                qqHelper.CheckQQ();
                Task task = new Task(qqHelper.GetMsg);
                task.Start();
            }
            catch (Exception xe)
            {
                MessageBox.Show(xe.Message);
            }
        }
        private void btnCookies_Click(object sender, EventArgs e)
        {
            try
            {
                var qq = txtQQ.Text.Trim();
                if (qq == "")
                {
                    MessageBox.Show("请输入qq号！");
                    return;
                }
                QQHelper qqHelper = null;
                if (QQHelperManager.qqHelpers.Keys.FirstOrDefault(r => r == qq) != null)
                {
                    qqHelper = QQHelperManager.qqHelpers[qq];
                }
                if (qqHelper == null)
                {
                    qqHelper = new QQHelper(qq);
                    QQHelperManager.qqHelpers.Add(qq, qqHelper);
                }
                var addcookies = txtAddCookies.Text.Trim();
                qqHelper.AddCookie(addcookies);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
