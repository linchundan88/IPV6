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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.comboBox_thread_num = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.radioButton_auto = new System.Windows.Forms.RadioButton();
            this.radioButton_man = new System.Windows.Forms.RadioButton();
            this.panel_man = new System.Windows.Forms.Panel();
            this.btn_sysName = new System.Windows.Forms.Button();
            this.btn_VLAN = new System.Windows.Forms.Button();
            this.btn_dot1dBase = new System.Windows.Forms.Button();
            this.btn_mac_address = new System.Windows.Forms.Button();
            this.btn_arp = new System.Windows.Forms.Button();
            this.btn_IfTable = new System.Windows.Forms.Button();
            this.panel_auto = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_timer = new System.Windows.Forms.ComboBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.panel_auto_snmp_type = new System.Windows.Forms.Panel();
            this.checkBox_sysName = new System.Windows.Forms.CheckBox();
            this.checkBox_dot1BasePort = new System.Windows.Forms.CheckBox();
            this.checkBox_vlan = new System.Windows.Forms.CheckBox();
            this.checkBox_mac_address = new System.Windows.Forms.CheckBox();
            this.checkBox_arp = new System.Windows.Forms.CheckBox();
            this.checkBox_ifTable = new System.Windows.Forms.CheckBox();
            this.btn_arp_new = new System.Windows.Forms.Button();
            this.checkBox_arp_new = new System.Windows.Forms.CheckBox();
            this.panel_man.SuspendLayout();
            this.panel_auto.SuspendLayout();
            this.panel_auto_snmp_type.SuspendLayout();
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
            this.comboBox_thread_num.Location = new System.Drawing.Point(95, 102);
            this.comboBox_thread_num.Name = "comboBox_thread_num";
            this.comboBox_thread_num.Size = new System.Drawing.Size(76, 20);
            this.comboBox_thread_num.TabIndex = 11;
            this.comboBox_thread_num.SelectedIndexChanged += new System.EventHandler(this.comboBox_thread_num_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "并发线程数";
            // 
            // timer1
            // 
            this.timer1.Interval = 120000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // radioButton_auto
            // 
            this.radioButton_auto.AutoSize = true;
            this.radioButton_auto.Checked = true;
            this.radioButton_auto.Location = new System.Drawing.Point(14, 8);
            this.radioButton_auto.Name = "radioButton_auto";
            this.radioButton_auto.Size = new System.Drawing.Size(71, 16);
            this.radioButton_auto.TabIndex = 18;
            this.radioButton_auto.TabStop = true;
            this.radioButton_auto.Text = "自动执行";
            this.radioButton_auto.UseVisualStyleBackColor = true;
            this.radioButton_auto.Click += new System.EventHandler(this.radioButton_auto_Click);
            // 
            // radioButton_man
            // 
            this.radioButton_man.AutoSize = true;
            this.radioButton_man.Location = new System.Drawing.Point(100, 8);
            this.radioButton_man.Name = "radioButton_man";
            this.radioButton_man.Size = new System.Drawing.Size(71, 16);
            this.radioButton_man.TabIndex = 19;
            this.radioButton_man.Text = "手动执行";
            this.radioButton_man.UseVisualStyleBackColor = true;
            this.radioButton_man.Click += new System.EventHandler(this.radioButton_man_Click);
            // 
            // panel_man
            // 
            this.panel_man.Controls.Add(this.btn_arp_new);
            this.panel_man.Controls.Add(this.btn_sysName);
            this.panel_man.Controls.Add(this.btn_VLAN);
            this.panel_man.Controls.Add(this.btn_dot1dBase);
            this.panel_man.Controls.Add(this.btn_mac_address);
            this.panel_man.Controls.Add(this.btn_arp);
            this.panel_man.Controls.Add(this.btn_IfTable);
            this.panel_man.Location = new System.Drawing.Point(14, 187);
            this.panel_man.Name = "panel_man";
            this.panel_man.Size = new System.Drawing.Size(579, 103);
            this.panel_man.TabIndex = 20;
            this.panel_man.Visible = false;
            // 
            // btn_sysName
            // 
            this.btn_sysName.Location = new System.Drawing.Point(7, 8);
            this.btn_sysName.Name = "btn_sysName";
            this.btn_sysName.Size = new System.Drawing.Size(161, 23);
            this.btn_sysName.TabIndex = 14;
            this.btn_sysName.Text = "获取设备厂家、名称等";
            this.btn_sysName.UseVisualStyleBackColor = true;
            this.btn_sysName.Click += new System.EventHandler(this.btn_sysName_Click);
            // 
            // btn_VLAN
            // 
            this.btn_VLAN.Location = new System.Drawing.Point(422, 8);
            this.btn_VLAN.Name = "btn_VLAN";
            this.btn_VLAN.Size = new System.Drawing.Size(152, 23);
            this.btn_VLAN.TabIndex = 13;
            this.btn_VLAN.Text = "获取VLAN(包括VTP VLAN)";
            this.btn_VLAN.UseVisualStyleBackColor = true;
            this.btn_VLAN.Click += new System.EventHandler(this.btn_VLAN_Click);
            // 
            // btn_dot1dBase
            // 
            this.btn_dot1dBase.Location = new System.Drawing.Point(306, 8);
            this.btn_dot1dBase.Name = "btn_dot1dBase";
            this.btn_dot1dBase.Size = new System.Drawing.Size(110, 23);
            this.btn_dot1dBase.TabIndex = 12;
            this.btn_dot1dBase.Text = "端口桥接表";
            this.btn_dot1dBase.UseVisualStyleBackColor = true;
            this.btn_dot1dBase.Click += new System.EventHandler(this.btn_dot1dBase_Click);
            // 
            // btn_mac_address
            // 
            this.btn_mac_address.Location = new System.Drawing.Point(297, 37);
            this.btn_mac_address.Name = "btn_mac_address";
            this.btn_mac_address.Size = new System.Drawing.Size(124, 23);
            this.btn_mac_address.TabIndex = 11;
            this.btn_mac_address.Text = "获取MAC地址表";
            this.btn_mac_address.UseVisualStyleBackColor = true;
            this.btn_mac_address.Click += new System.EventHandler(this.btn_dot1dTpFdbTable_Click);
            // 
            // btn_arp
            // 
            this.btn_arp.Location = new System.Drawing.Point(8, 37);
            this.btn_arp.Name = "btn_arp";
            this.btn_arp.Size = new System.Drawing.Size(124, 23);
            this.btn_arp.TabIndex = 10;
            this.btn_arp.Text = "获取ARP表";
            this.btn_arp.UseVisualStyleBackColor = true;
            this.btn_arp.Click += new System.EventHandler(this.btn_ipNetToMediaTable_Click);
            // 
            // btn_IfTable
            // 
            this.btn_IfTable.Location = new System.Drawing.Point(190, 8);
            this.btn_IfTable.Name = "btn_IfTable";
            this.btn_IfTable.Size = new System.Drawing.Size(110, 23);
            this.btn_IfTable.TabIndex = 8;
            this.btn_IfTable.Text = "网路设备端口";
            this.btn_IfTable.UseVisualStyleBackColor = true;
            this.btn_IfTable.Click += new System.EventHandler(this.btn_IfTable_Click);
            // 
            // panel_auto
            // 
            this.panel_auto.Controls.Add(this.label2);
            this.panel_auto.Controls.Add(this.comboBox_timer);
            this.panel_auto.Controls.Add(this.btnStart);
            this.panel_auto.Location = new System.Drawing.Point(14, 120);
            this.panel_auto.Name = "panel_auto";
            this.panel_auto.Size = new System.Drawing.Size(358, 61);
            this.panel_auto.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "自动执行间隔(秒)";
            // 
            // comboBox_timer
            // 
            this.comboBox_timer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_timer.FormattingEnabled = true;
            this.comboBox_timer.Items.AddRange(new object[] {
            "60",
            "120",
            "240",
            "300",
            "600",
            "3600"});
            this.comboBox_timer.Location = new System.Drawing.Point(111, 21);
            this.comboBox_timer.Name = "comboBox_timer";
            this.comboBox_timer.Size = new System.Drawing.Size(76, 20);
            this.comboBox_timer.TabIndex = 19;
            this.comboBox_timer.SelectedIndexChanged += new System.EventHandler(this.comboBox_timer_SelectedIndexChanged);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(225, 19);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 18;
            this.btnStart.Text = "启动";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // panel_auto_snmp_type
            // 
            this.panel_auto_snmp_type.Controls.Add(this.checkBox_arp_new);
            this.panel_auto_snmp_type.Controls.Add(this.checkBox_sysName);
            this.panel_auto_snmp_type.Controls.Add(this.checkBox_dot1BasePort);
            this.panel_auto_snmp_type.Controls.Add(this.checkBox_vlan);
            this.panel_auto_snmp_type.Controls.Add(this.checkBox_mac_address);
            this.panel_auto_snmp_type.Controls.Add(this.checkBox_arp);
            this.panel_auto_snmp_type.Controls.Add(this.checkBox_ifTable);
            this.panel_auto_snmp_type.Location = new System.Drawing.Point(22, 30);
            this.panel_auto_snmp_type.Name = "panel_auto_snmp_type";
            this.panel_auto_snmp_type.Size = new System.Drawing.Size(566, 52);
            this.panel_auto_snmp_type.TabIndex = 22;
            // 
            // checkBox_sysName
            // 
            this.checkBox_sysName.AutoSize = true;
            this.checkBox_sysName.Location = new System.Drawing.Point(13, 3);
            this.checkBox_sysName.Name = "checkBox_sysName";
            this.checkBox_sysName.Size = new System.Drawing.Size(180, 16);
            this.checkBox_sysName.TabIndex = 20;
            this.checkBox_sysName.Text = "自动获取设备厂商和设备名称";
            this.checkBox_sysName.UseVisualStyleBackColor = true;
            // 
            // checkBox_dot1BasePort
            // 
            this.checkBox_dot1BasePort.AutoSize = true;
            this.checkBox_dot1BasePort.Location = new System.Drawing.Point(419, 3);
            this.checkBox_dot1BasePort.Name = "checkBox_dot1BasePort";
            this.checkBox_dot1BasePort.Size = new System.Drawing.Size(120, 16);
            this.checkBox_dot1BasePort.TabIndex = 19;
            this.checkBox_dot1BasePort.Text = "自动获取桥端口表";
            this.checkBox_dot1BasePort.UseVisualStyleBackColor = true;
            // 
            // checkBox_vlan
            // 
            this.checkBox_vlan.AutoSize = true;
            this.checkBox_vlan.Location = new System.Drawing.Point(317, 3);
            this.checkBox_vlan.Name = "checkBox_vlan";
            this.checkBox_vlan.Size = new System.Drawing.Size(96, 16);
            this.checkBox_vlan.TabIndex = 18;
            this.checkBox_vlan.Text = "自动获取VLAN";
            this.checkBox_vlan.UseVisualStyleBackColor = true;
            // 
            // checkBox_mac_address
            // 
            this.checkBox_mac_address.AutoSize = true;
            this.checkBox_mac_address.Location = new System.Drawing.Point(282, 32);
            this.checkBox_mac_address.Name = "checkBox_mac_address";
            this.checkBox_mac_address.Size = new System.Drawing.Size(126, 16);
            this.checkBox_mac_address.TabIndex = 17;
            this.checkBox_mac_address.Text = "自动获取MAC地址表";
            this.checkBox_mac_address.UseVisualStyleBackColor = true;
            // 
            // checkBox_arp
            // 
            this.checkBox_arp.AutoSize = true;
            this.checkBox_arp.Location = new System.Drawing.Point(13, 32);
            this.checkBox_arp.Name = "checkBox_arp";
            this.checkBox_arp.Size = new System.Drawing.Size(102, 16);
            this.checkBox_arp.TabIndex = 16;
            this.checkBox_arp.Text = "自动获取ARP表";
            this.checkBox_arp.UseVisualStyleBackColor = true;
            // 
            // checkBox_ifTable
            // 
            this.checkBox_ifTable.AutoSize = true;
            this.checkBox_ifTable.Location = new System.Drawing.Point(204, 3);
            this.checkBox_ifTable.Name = "checkBox_ifTable";
            this.checkBox_ifTable.Size = new System.Drawing.Size(96, 16);
            this.checkBox_ifTable.TabIndex = 15;
            this.checkBox_ifTable.Text = "自动获取接口";
            this.checkBox_ifTable.UseVisualStyleBackColor = true;
            // 
            // btn_arp_new
            // 
            this.btn_arp_new.Location = new System.Drawing.Point(149, 37);
            this.btn_arp_new.Name = "btn_arp_new";
            this.btn_arp_new.Size = new System.Drawing.Size(124, 23);
            this.btn_arp_new.TabIndex = 15;
            this.btn_arp_new.Text = "获取ARP表(IPV6)";
            this.btn_arp_new.UseVisualStyleBackColor = true;
            this.btn_arp_new.Click += new System.EventHandler(this.btn_arp_new_Click);
            // 
            // checkBox_arp_new
            // 
            this.checkBox_arp_new.AutoSize = true;
            this.checkBox_arp_new.Location = new System.Drawing.Point(141, 32);
            this.checkBox_arp_new.Name = "checkBox_arp_new";
            this.checkBox_arp_new.Size = new System.Drawing.Size(138, 16);
            this.checkBox_arp_new.TabIndex = 21;
            this.checkBox_arp_new.Text = "自动获取ARP表(IPV6)";
            this.checkBox_arp_new.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 353);
            this.Controls.Add(this.panel_auto_snmp_type);
            this.Controls.Add(this.panel_auto);
            this.Controls.Add(this.panel_man);
            this.Controls.Add(this.radioButton_man);
            this.Controls.Add(this.radioButton_auto);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_thread_num);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "自动用SNMP协议获取网络设备的数据";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel_man.ResumeLayout(false);
            this.panel_auto.ResumeLayout(false);
            this.panel_auto.PerformLayout();
            this.panel_auto_snmp_type.ResumeLayout(false);
            this.panel_auto_snmp_type.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox_thread_num;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RadioButton radioButton_auto;
        private System.Windows.Forms.RadioButton radioButton_man;
        private System.Windows.Forms.Panel panel_man;
        private System.Windows.Forms.Button btn_dot1dBase;
        private System.Windows.Forms.Button btn_mac_address;
        private System.Windows.Forms.Panel panel_auto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_timer;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btn_VLAN;
        private System.Windows.Forms.Button btn_arp;
        private System.Windows.Forms.Button btn_IfTable;
        private System.Windows.Forms.Panel panel_auto_snmp_type;
        private System.Windows.Forms.CheckBox checkBox_dot1BasePort;
        private System.Windows.Forms.CheckBox checkBox_vlan;
        private System.Windows.Forms.CheckBox checkBox_mac_address;
        private System.Windows.Forms.CheckBox checkBox_arp;
        private System.Windows.Forms.CheckBox checkBox_ifTable;
        private System.Windows.Forms.Button btn_sysName;
        private System.Windows.Forms.CheckBox checkBox_sysName;
        private System.Windows.Forms.Button btn_arp_new;
        private System.Windows.Forms.CheckBox checkBox_arp_new;
    }
}

