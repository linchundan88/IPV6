namespace SNMP
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.comboBox_thread_num = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_SNMP_port = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_SNMP_community_ro = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_end_ip = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_start_ip = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_SNMP_community_rw = new System.Windows.Forms.TextBox();
            this.checkBox_getmacaddress = new System.Windows.Forms.CheckBox();
            this.checkBox_getarp = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_thread_num
            // 
            this.comboBox_thread_num.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_thread_num.FormattingEnabled = true;
            this.comboBox_thread_num.Items.AddRange(new object[] {
            "1",
            "2",
            "4",
            "6",
            "8",
            "12"});
            this.comboBox_thread_num.Location = new System.Drawing.Point(154, 344);
            this.comboBox_thread_num.Name = "comboBox_thread_num";
            this.comboBox_thread_num.Size = new System.Drawing.Size(76, 20);
            this.comboBox_thread_num.TabIndex = 11;
            this.comboBox_thread_num.SelectedIndexChanged += new System.EventHandler(this.comboBox_thread_num_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 347);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "并发线程数";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(346, 344);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 29;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.textBox_SNMP_port);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.textBox_SNMP_community_ro);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBox_end_ip);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox_start_ip);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Location = new System.Drawing.Point(48, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(514, 138);
            this.panel1.TabIndex = 32;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label7.Location = new System.Drawing.Point(18, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(189, 14);
            this.label7.TabIndex = 0;
            this.label7.Text = "以下设置用来搜索网络设备：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 34;
            this.label5.Text = "SNMP端口号：";
            // 
            // textBox_SNMP_port
            // 
            this.textBox_SNMP_port.Location = new System.Drawing.Point(156, 95);
            this.textBox_SNMP_port.Name = "textBox_SNMP_port";
            this.textBox_SNMP_port.Size = new System.Drawing.Size(115, 21);
            this.textBox_SNMP_port.TabIndex = 33;
            this.textBox_SNMP_port.Text = "161";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 12);
            this.label4.TabIndex = 32;
            this.label4.Text = "只读Community_string：";
            // 
            // textBox_SNMP_community_ro
            // 
            this.textBox_SNMP_community_ro.Location = new System.Drawing.Point(156, 68);
            this.textBox_SNMP_community_ro.Name = "textBox_SNMP_community_ro";
            this.textBox_SNMP_community_ro.Size = new System.Drawing.Size(115, 21);
            this.textBox_SNMP_community_ro.TabIndex = 31;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(269, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 30;
            this.label3.Text = "结束IP地址：";
            // 
            // textBox_end_ip
            // 
            this.textBox_end_ip.Location = new System.Drawing.Point(352, 33);
            this.textBox_end_ip.Name = "textBox_end_ip";
            this.textBox_end_ip.Size = new System.Drawing.Size(130, 21);
            this.textBox_end_ip.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 28;
            this.label2.Text = "开始IP地址：";
            // 
            // textBox_start_ip
            // 
            this.textBox_start_ip.Location = new System.Drawing.Point(96, 30);
            this.textBox_start_ip.Name = "textBox_start_ip";
            this.textBox_start_ip.Size = new System.Drawing.Size(130, 21);
            this.textBox_start_ip.TabIndex = 27;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.textBox_SNMP_community_rw);
            this.panel2.Controls.Add(this.checkBox_getmacaddress);
            this.panel2.Controls.Add(this.checkBox_getarp);
            this.panel2.Location = new System.Drawing.Point(48, 161);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(392, 157);
            this.panel2.TabIndex = 34;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(51, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 12);
            this.label6.TabIndex = 35;
            this.label6.Text = "读写Community_string：";
            // 
            // textBox_SNMP_community_rw
            // 
            this.textBox_SNMP_community_rw.Location = new System.Drawing.Point(194, 35);
            this.textBox_SNMP_community_rw.Name = "textBox_SNMP_community_rw";
            this.textBox_SNMP_community_rw.Size = new System.Drawing.Size(115, 21);
            this.textBox_SNMP_community_rw.TabIndex = 34;
            // 
            // checkBox_getmacaddress
            // 
            this.checkBox_getmacaddress.AutoSize = true;
            this.checkBox_getmacaddress.Location = new System.Drawing.Point(194, 70);
            this.checkBox_getmacaddress.Name = "checkBox_getmacaddress";
            this.checkBox_getmacaddress.Size = new System.Drawing.Size(150, 16);
            this.checkBox_getmacaddress.TabIndex = 33;
            this.checkBox_getmacaddress.Text = "获取该设备的MAC地址表";
            this.checkBox_getmacaddress.UseVisualStyleBackColor = true;
            // 
            // checkBox_getarp
            // 
            this.checkBox_getarp.AutoSize = true;
            this.checkBox_getarp.Location = new System.Drawing.Point(48, 70);
            this.checkBox_getarp.Name = "checkBox_getarp";
            this.checkBox_getarp.Size = new System.Drawing.Size(126, 16);
            this.checkBox_getarp.TabIndex = 32;
            this.checkBox_getarp.Text = "获取该设备的ARP表";
            this.checkBox_getarp.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label8.Location = new System.Drawing.Point(8, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(231, 14);
            this.label8.TabIndex = 36;
            this.label8.Text = "以下设置设置搜索到的设备参数备：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 391);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_thread_num);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "扫码网络设备";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox_thread_num;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_SNMP_port;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_SNMP_community_ro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_end_ip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_start_ip;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_SNMP_community_rw;
        private System.Windows.Forms.CheckBox checkBox_getmacaddress;
        private System.Windows.Forms.CheckBox checkBox_getarp;
    }
}

