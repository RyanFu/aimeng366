namespace QQ.UI
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.listViewQQ = new System.Windows.Forms.ListView();
            this.txtAddCookies = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtQQ = new System.Windows.Forms.TextBox();
            this.btnCookies = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listViewFriend = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtclientid = new System.Windows.Forms.TextBox();
            this.txtptwebqq = new System.Windows.Forms.TextBox();
            this.txtpsessionid = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewQQ
            // 
            this.listViewQQ.Dock = System.Windows.Forms.DockStyle.Left;
            this.listViewQQ.Location = new System.Drawing.Point(0, 0);
            this.listViewQQ.Name = "listViewQQ";
            this.listViewQQ.Size = new System.Drawing.Size(122, 444);
            this.listViewQQ.TabIndex = 0;
            this.listViewQQ.UseCompatibleStateImageBehavior = false;
            this.listViewQQ.View = System.Windows.Forms.View.List;
            // 
            // txtAddCookies
            // 
            this.txtAddCookies.Location = new System.Drawing.Point(24, 53);
            this.txtAddCookies.Multiline = true;
            this.txtAddCookies.Name = "txtAddCookies";
            this.txtAddCookies.Size = new System.Drawing.Size(197, 94);
            this.txtAddCookies.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "添加Cookies：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtQQ);
            this.groupBox1.Controls.Add(this.btnCookies);
            this.groupBox1.Controls.Add(this.btnLogin);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtAddCookies);
            this.groupBox1.Location = new System.Drawing.Point(265, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(366, 187);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // txtQQ
            // 
            this.txtQQ.Location = new System.Drawing.Point(48, 153);
            this.txtQQ.Name = "txtQQ";
            this.txtQQ.Size = new System.Drawing.Size(149, 21);
            this.txtQQ.TabIndex = 4;
            this.txtQQ.Text = "1205279120";
            // 
            // btnCookies
            // 
            this.btnCookies.Location = new System.Drawing.Point(237, 73);
            this.btnCookies.Name = "btnCookies";
            this.btnCookies.Size = new System.Drawing.Size(99, 36);
            this.btnCookies.TabIndex = 3;
            this.btnCookies.Text = "添加cookies";
            this.btnCookies.UseVisualStyleBackColor = true;
            this.btnCookies.Click += new System.EventHandler(this.btnCookies_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(237, 132);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(99, 36);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "登陆";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "QQ：";
            // 
            // listViewFriend
            // 
            this.listViewFriend.Dock = System.Windows.Forms.DockStyle.Left;
            this.listViewFriend.Location = new System.Drawing.Point(122, 0);
            this.listViewFriend.Name = "listViewFriend";
            this.listViewFriend.Size = new System.Drawing.Size(122, 444);
            this.listViewFriend.TabIndex = 4;
            this.listViewFriend.UseCompatibleStateImageBehavior = false;
            this.listViewFriend.View = System.Windows.Forms.View.List;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(266, 221);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "clientid：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(275, 254);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "ptwebqq：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(251, 295);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "psessionid：";
            // 
            // txtclientid
            // 
            this.txtclientid.Location = new System.Drawing.Point(334, 218);
            this.txtclientid.Name = "txtclientid";
            this.txtclientid.Size = new System.Drawing.Size(128, 21);
            this.txtclientid.TabIndex = 4;
            // 
            // txtptwebqq
            // 
            this.txtptwebqq.Location = new System.Drawing.Point(334, 254);
            this.txtptwebqq.Name = "txtptwebqq";
            this.txtptwebqq.Size = new System.Drawing.Size(128, 21);
            this.txtptwebqq.TabIndex = 4;
            // 
            // txtpsessionid
            // 
            this.txtpsessionid.Location = new System.Drawing.Point(334, 292);
            this.txtpsessionid.Name = "txtpsessionid";
            this.txtpsessionid.Size = new System.Drawing.Size(128, 21);
            this.txtpsessionid.TabIndex = 4;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 444);
            this.Controls.Add(this.txtpsessionid);
            this.Controls.Add(this.txtptwebqq);
            this.Controls.Add(this.txtclientid);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listViewFriend);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listViewQQ);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "艾梦366";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewQQ;
        private System.Windows.Forms.TextBox txtAddCookies;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.ListView listViewFriend;
        private System.Windows.Forms.TextBox txtQQ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtclientid;
        private System.Windows.Forms.TextBox txtptwebqq;
        private System.Windows.Forms.TextBox txtpsessionid;
        private System.Windows.Forms.Button btnCookies;
    }
}

