namespace AutoMailWinForm
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tbContolTask = new System.Windows.Forms.TabControl();
            this.tbPageTaskList = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lsvLog = new System.Windows.Forms.ListView();
            this.colLogTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLogTaskName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colInfoType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLogText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.DgvList = new System.Windows.Forms.DataGridView();
            this.colTaskID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskRunDll = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsUse = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colTaskType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskToList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskCcList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskErrorList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSecretKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbPageTaskEdit = new System.Windows.Forms.TabPage();
            this.grpSetRunServer = new System.Windows.Forms.GroupBox();
            this.txtMailErrorList = new System.Windows.Forms.TextBox();
            this.lblErrorEmail_Edit = new System.Windows.Forms.TextBox();
            this.txtMailCcList = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.txtMailToList = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chbIsUse = new System.Windows.Forms.CheckBox();
            this.lblTaskID_Edit = new System.Windows.Forms.TextBox();
            this.txtTaskID_Edit = new System.Windows.Forms.TextBox();
            this.txtRemark_Edit = new System.Windows.Forms.TextBox();
            this.lblRemark_Edit = new System.Windows.Forms.TextBox();
            this.lblTaskRunDll_Edit = new System.Windows.Forms.TextBox();
            this.lblTaskName_Edit = new System.Windows.Forms.TextBox();
            this.lblTaskTypeID_Edit = new System.Windows.Forms.TextBox();
            this.cmbTaskType = new System.Windows.Forms.ComboBox();
            this.txtTaskRunDll_Edit = new System.Windows.Forms.TextBox();
            this.txtTaskName_Edit = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAddDb = new System.Windows.Forms.Button();
            this.grpAddServer = new System.Windows.Forms.GroupBox();
            this.btnTestAndAddDB = new System.Windows.Forms.Button();
            this.txtDbPassword = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.txtDbUser = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.txtDbName = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.txtDbIp = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.cmbServerDb = new System.Windows.Forms.ComboBox();
            this.tbPageTaskLog = new System.Windows.Forms.TabPage();
            this.dgvTaskLog = new System.Windows.Forms.DataGridView();
            this.colTaskID_His = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskName_His = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskTypeID_His = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRunStatus_His = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLogContent_His = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBeginRun_His = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbxTaskLogDate = new System.Windows.Forms.CheckBox();
            this.lblTaskID_His = new System.Windows.Forms.TextBox();
            this.txtTaskID_His = new System.Windows.Forms.TextBox();
            this.lblTaskTypeID_His = new System.Windows.Forms.TextBox();
            this.lblTaskName_His = new System.Windows.Forms.TextBox();
            this.txtTaskName_His = new System.Windows.Forms.TextBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.DateRunTime_End = new System.Windows.Forms.DateTimePicker();
            this.DateRunTime_Begin = new System.Windows.Forms.DateTimePicker();
            this.cbxTaskType_His = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.grpRunningTask = new System.Windows.Forms.GroupBox();
            this.lsvRunningTask = new System.Windows.Forms.ListView();
            this.lsvColTaskId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lsvColTaskTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lsvColTaskStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel4 = new System.Windows.Forms.Panel();
            this.countdownTimer1 = new Common.CountdownTimer();
            this.timerTask = new System.Windows.Forms.Timer(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnDel = new System.Windows.Forms.ToolStripButton();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnCancel = new System.Windows.Forms.ToolStripButton();
            this.btnStartTask = new System.Windows.Forms.ToolStripButton();
            this.btnEndTask = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.tbContolTask.SuspendLayout();
            this.tbPageTaskList.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvList)).BeginInit();
            this.tbPageTaskEdit.SuspendLayout();
            this.grpSetRunServer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpAddServer.SuspendLayout();
            this.tbPageTaskLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskLog)).BeginInit();
            this.panel3.SuspendLayout();
            this.grpRunningTask.SuspendLayout();
            this.panel4.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbContolTask
            // 
            this.tbContolTask.Controls.Add(this.tbPageTaskList);
            this.tbContolTask.Controls.Add(this.tbPageTaskEdit);
            this.tbContolTask.Controls.Add(this.tbPageTaskLog);
            this.tbContolTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbContolTask.ItemSize = new System.Drawing.Size(55, 20);
            this.tbContolTask.Location = new System.Drawing.Point(0, 50);
            this.tbContolTask.Name = "tbContolTask";
            this.tbContolTask.SelectedIndex = 0;
            this.tbContolTask.Size = new System.Drawing.Size(1036, 630);
            this.tbContolTask.TabIndex = 1;
            this.tbContolTask.SelectedIndexChanged += new System.EventHandler(this.tbPageTaskLog_SelectedIndexChanged);
            // 
            // tbPageTaskList
            // 
            this.tbPageTaskList.Controls.Add(this.panel2);
            this.tbPageTaskList.Controls.Add(this.DgvList);
            this.tbPageTaskList.Location = new System.Drawing.Point(4, 24);
            this.tbPageTaskList.Name = "tbPageTaskList";
            this.tbPageTaskList.Padding = new System.Windows.Forms.Padding(3);
            this.tbPageTaskList.Size = new System.Drawing.Size(1028, 602);
            this.tbPageTaskList.TabIndex = 0;
            this.tbPageTaskList.Text = "任务列表";
            this.tbPageTaskList.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lsvLog);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 436);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1022, 163);
            this.panel2.TabIndex = 9;
            // 
            // lsvLog
            // 
            this.lsvLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lsvLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLogTime,
            this.colLogTaskName,
            this.colInfoType,
            this.colLogText});
            this.lsvLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvLog.GridLines = true;
            this.lsvLog.HideSelection = false;
            this.lsvLog.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.lsvLog.LargeImageList = this.imageList1;
            this.lsvLog.Location = new System.Drawing.Point(0, 0);
            this.lsvLog.Name = "lsvLog";
            this.lsvLog.ShowGroups = false;
            this.lsvLog.ShowItemToolTips = true;
            this.lsvLog.Size = new System.Drawing.Size(1022, 163);
            this.lsvLog.SmallImageList = this.imageList1;
            this.lsvLog.TabIndex = 12;
            this.lsvLog.UseCompatibleStateImageBehavior = false;
            this.lsvLog.View = System.Windows.Forms.View.Details;
            // 
            // colLogTime
            // 
            this.colLogTime.Text = "执行时间";
            this.colLogTime.Width = 162;
            // 
            // colLogTaskName
            // 
            this.colLogTaskName.Text = "任务名称";
            this.colLogTaskName.Width = 98;
            // 
            // colInfoType
            // 
            this.colInfoType.Text = "状态信息";
            this.colInfoType.Width = 123;
            // 
            // colLogText
            // 
            this.colLogText.Text = "任务信息";
            this.colLogText.Width = 600;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "info.png");
            this.imageList1.Images.SetKeyName(1, "warning.png");
            this.imageList1.Images.SetKeyName(2, "error.png");
            this.imageList1.Images.SetKeyName(3, "loading.gif");
            // 
            // DgvList
            // 
            this.DgvList.AllowUserToAddRows = false;
            this.DgvList.AllowUserToDeleteRows = false;
            this.DgvList.AllowUserToResizeColumns = false;
            this.DgvList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.DgvList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DgvList.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTaskID,
            this.colTaskName,
            this.colTaskRunDll,
            this.colIsUse,
            this.colTaskType,
            this.colRemark,
            this.colTaskToList,
            this.colTaskCcList,
            this.colTaskErrorList,
            this.colServerName,
            this.colSecretKey});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvList.DefaultCellStyle = dataGridViewCellStyle9;
            this.DgvList.Dock = System.Windows.Forms.DockStyle.Top;
            this.DgvList.Location = new System.Drawing.Point(3, 3);
            this.DgvList.MultiSelect = false;
            this.DgvList.Name = "DgvList";
            this.DgvList.ReadOnly = true;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvList.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.DgvList.RowHeadersWidth = 25;
            this.DgvList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            this.DgvList.RowsDefaultCellStyle = dataGridViewCellStyle11;
            this.DgvList.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.DgvList.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
            this.DgvList.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.DgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvList.Size = new System.Drawing.Size(1022, 433);
            this.DgvList.TabIndex = 6;
            // 
            // colTaskID
            // 
            this.colTaskID.DataPropertyName = "TaskID";
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.colTaskID.DefaultCellStyle = dataGridViewCellStyle3;
            this.colTaskID.FillWeight = 120F;
            this.colTaskID.HeaderText = "任务工程ID";
            this.colTaskID.MinimumWidth = 6;
            this.colTaskID.Name = "colTaskID";
            this.colTaskID.ReadOnly = true;
            this.colTaskID.Width = 120;
            // 
            // colTaskName
            // 
            this.colTaskName.DataPropertyName = "TaskName";
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.colTaskName.DefaultCellStyle = dataGridViewCellStyle4;
            this.colTaskName.FillWeight = 180F;
            this.colTaskName.HeaderText = "任务名称";
            this.colTaskName.MinimumWidth = 6;
            this.colTaskName.Name = "colTaskName";
            this.colTaskName.ReadOnly = true;
            this.colTaskName.Width = 180;
            // 
            // colTaskRunDll
            // 
            this.colTaskRunDll.DataPropertyName = "TaskRunDll";
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.colTaskRunDll.DefaultCellStyle = dataGridViewCellStyle5;
            this.colTaskRunDll.FillWeight = 120F;
            this.colTaskRunDll.HeaderText = "任务运行dll名称";
            this.colTaskRunDll.MinimumWidth = 6;
            this.colTaskRunDll.Name = "colTaskRunDll";
            this.colTaskRunDll.ReadOnly = true;
            this.colTaskRunDll.Width = 120;
            // 
            // colIsUse
            // 
            this.colIsUse.DataPropertyName = "IsUse";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.NullValue = false;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.colIsUse.DefaultCellStyle = dataGridViewCellStyle6;
            this.colIsUse.FillWeight = 75F;
            this.colIsUse.HeaderText = "是否启用";
            this.colIsUse.MinimumWidth = 6;
            this.colIsUse.Name = "colIsUse";
            this.colIsUse.ReadOnly = true;
            this.colIsUse.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colIsUse.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colIsUse.Width = 75;
            // 
            // colTaskType
            // 
            this.colTaskType.DataPropertyName = "TaskType";
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.colTaskType.DefaultCellStyle = dataGridViewCellStyle7;
            this.colTaskType.HeaderText = "任务类型";
            this.colTaskType.MinimumWidth = 6;
            this.colTaskType.Name = "colTaskType";
            this.colTaskType.ReadOnly = true;
            this.colTaskType.Width = 125;
            // 
            // colRemark
            // 
            this.colRemark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colRemark.DataPropertyName = "Remark";
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.colRemark.DefaultCellStyle = dataGridViewCellStyle8;
            this.colRemark.FillWeight = 150F;
            this.colRemark.HeaderText = "备注";
            this.colRemark.MinimumWidth = 6;
            this.colRemark.Name = "colRemark";
            this.colRemark.ReadOnly = true;
            // 
            // colTaskToList
            // 
            this.colTaskToList.DataPropertyName = "ToList";
            this.colTaskToList.HeaderText = "邮件发送人";
            this.colTaskToList.MinimumWidth = 6;
            this.colTaskToList.Name = "colTaskToList";
            this.colTaskToList.ReadOnly = true;
            this.colTaskToList.Visible = false;
            this.colTaskToList.Width = 125;
            // 
            // colTaskCcList
            // 
            this.colTaskCcList.DataPropertyName = "CcList";
            this.colTaskCcList.HeaderText = "邮件抄送人";
            this.colTaskCcList.MinimumWidth = 6;
            this.colTaskCcList.Name = "colTaskCcList";
            this.colTaskCcList.ReadOnly = true;
            this.colTaskCcList.Visible = false;
            this.colTaskCcList.Width = 125;
            // 
            // colTaskErrorList
            // 
            this.colTaskErrorList.DataPropertyName = "ErrorList";
            this.colTaskErrorList.HeaderText = "邮件错误通知人";
            this.colTaskErrorList.MinimumWidth = 6;
            this.colTaskErrorList.Name = "colTaskErrorList";
            this.colTaskErrorList.ReadOnly = true;
            this.colTaskErrorList.Visible = false;
            this.colTaskErrorList.Width = 125;
            // 
            // colServerName
            // 
            this.colServerName.DataPropertyName = "ServerName";
            this.colServerName.HeaderText = "服务器名称";
            this.colServerName.MinimumWidth = 6;
            this.colServerName.Name = "colServerName";
            this.colServerName.ReadOnly = true;
            this.colServerName.Visible = false;
            this.colServerName.Width = 125;
            // 
            // colSecretKey
            // 
            this.colSecretKey.DataPropertyName = "SecretKey";
            this.colSecretKey.HeaderText = "服务器秘钥";
            this.colSecretKey.MinimumWidth = 6;
            this.colSecretKey.Name = "colSecretKey";
            this.colSecretKey.ReadOnly = true;
            this.colSecretKey.Visible = false;
            this.colSecretKey.Width = 125;
            // 
            // tbPageTaskEdit
            // 
            this.tbPageTaskEdit.Controls.Add(this.grpSetRunServer);
            this.tbPageTaskEdit.Controls.Add(this.panel1);
            this.tbPageTaskEdit.Controls.Add(this.groupBox1);
            this.tbPageTaskEdit.Location = new System.Drawing.Point(4, 24);
            this.tbPageTaskEdit.Name = "tbPageTaskEdit";
            this.tbPageTaskEdit.Padding = new System.Windows.Forms.Padding(3);
            this.tbPageTaskEdit.Size = new System.Drawing.Size(1028, 602);
            this.tbPageTaskEdit.TabIndex = 1;
            this.tbPageTaskEdit.Text = "任务编辑";
            this.tbPageTaskEdit.UseVisualStyleBackColor = true;
            // 
            // grpSetRunServer
            // 
            this.grpSetRunServer.Controls.Add(this.txtMailErrorList);
            this.grpSetRunServer.Controls.Add(this.lblErrorEmail_Edit);
            this.grpSetRunServer.Controls.Add(this.txtMailCcList);
            this.grpSetRunServer.Controls.Add(this.textBox3);
            this.grpSetRunServer.Controls.Add(this.txtMailToList);
            this.grpSetRunServer.Controls.Add(this.textBox1);
            this.grpSetRunServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSetRunServer.Location = new System.Drawing.Point(3, 147);
            this.grpSetRunServer.Name = "grpSetRunServer";
            this.grpSetRunServer.Size = new System.Drawing.Size(1022, 197);
            this.grpSetRunServer.TabIndex = 80;
            this.grpSetRunServer.TabStop = false;
            this.grpSetRunServer.Text = "邮箱设置（用 ; 相隔）";
            // 
            // txtMailErrorList
            // 
            this.txtMailErrorList.Location = new System.Drawing.Point(140, 124);
            this.txtMailErrorList.Name = "txtMailErrorList";
            this.txtMailErrorList.Size = new System.Drawing.Size(669, 26);
            this.txtMailErrorList.TabIndex = 92;
            // 
            // lblErrorEmail_Edit
            // 
            this.lblErrorEmail_Edit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblErrorEmail_Edit.Location = new System.Drawing.Point(19, 124);
            this.lblErrorEmail_Edit.Multiline = true;
            this.lblErrorEmail_Edit.Name = "lblErrorEmail_Edit";
            this.lblErrorEmail_Edit.ReadOnly = true;
            this.lblErrorEmail_Edit.Size = new System.Drawing.Size(669, 22);
            this.lblErrorEmail_Edit.TabIndex = 91;
            this.lblErrorEmail_Edit.Tag = "*";
            this.lblErrorEmail_Edit.Text = "任务失败时邮件通知人";
            // 
            // txtMailCcList
            // 
            this.txtMailCcList.Location = new System.Drawing.Point(140, 81);
            this.txtMailCcList.Name = "txtMailCcList";
            this.txtMailCcList.Size = new System.Drawing.Size(669, 26);
            this.txtMailCcList.TabIndex = 89;
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Location = new System.Drawing.Point(19, 81);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(669, 22);
            this.textBox3.TabIndex = 87;
            this.textBox3.Text = "抄送人CcList";
            // 
            // txtMailToList
            // 
            this.txtMailToList.Location = new System.Drawing.Point(140, 40);
            this.txtMailToList.Name = "txtMailToList";
            this.txtMailToList.Size = new System.Drawing.Size(669, 26);
            this.txtMailToList.TabIndex = 90;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(20, 40);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(669, 22);
            this.textBox1.TabIndex = 88;
            this.textBox1.Text = "收件人ToList";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chbIsUse);
            this.panel1.Controls.Add(this.lblTaskID_Edit);
            this.panel1.Controls.Add(this.txtTaskID_Edit);
            this.panel1.Controls.Add(this.txtRemark_Edit);
            this.panel1.Controls.Add(this.lblRemark_Edit);
            this.panel1.Controls.Add(this.lblTaskRunDll_Edit);
            this.panel1.Controls.Add(this.lblTaskName_Edit);
            this.panel1.Controls.Add(this.lblTaskTypeID_Edit);
            this.panel1.Controls.Add(this.cmbTaskType);
            this.panel1.Controls.Add(this.txtTaskRunDll_Edit);
            this.panel1.Controls.Add(this.txtTaskName_Edit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1022, 144);
            this.panel1.TabIndex = 1;
            // 
            // chbIsUse
            // 
            this.chbIsUse.AutoSize = true;
            this.chbIsUse.Checked = true;
            this.chbIsUse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbIsUse.Location = new System.Drawing.Point(855, 14);
            this.chbIsUse.Name = "chbIsUse";
            this.chbIsUse.Size = new System.Drawing.Size(59, 24);
            this.chbIsUse.TabIndex = 20;
            this.chbIsUse.Text = "启用";
            this.chbIsUse.UseVisualStyleBackColor = true;
            // 
            // lblTaskID_Edit
            // 
            this.lblTaskID_Edit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblTaskID_Edit.Location = new System.Drawing.Point(21, 11);
            this.lblTaskID_Edit.Multiline = true;
            this.lblTaskID_Edit.Name = "lblTaskID_Edit";
            this.lblTaskID_Edit.ReadOnly = true;
            this.lblTaskID_Edit.Size = new System.Drawing.Size(117, 20);
            this.lblTaskID_Edit.TabIndex = 16;
            this.lblTaskID_Edit.Tag = "*";
            this.lblTaskID_Edit.Text = "任务工程ID";
            // 
            // txtTaskID_Edit
            // 
            this.txtTaskID_Edit.Location = new System.Drawing.Point(141, 11);
            this.txtTaskID_Edit.Name = "txtTaskID_Edit";
            this.txtTaskID_Edit.Size = new System.Drawing.Size(232, 26);
            this.txtTaskID_Edit.TabIndex = 17;
            this.txtTaskID_Edit.TextChanged += new System.EventHandler(this.txtTaskID_Edit_TextChanged);
            // 
            // txtRemark_Edit
            // 
            this.txtRemark_Edit.Location = new System.Drawing.Point(141, 81);
            this.txtRemark_Edit.Name = "txtRemark_Edit";
            this.txtRemark_Edit.Size = new System.Drawing.Size(668, 26);
            this.txtRemark_Edit.TabIndex = 28;
            // 
            // lblRemark_Edit
            // 
            this.lblRemark_Edit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblRemark_Edit.Location = new System.Drawing.Point(22, 81);
            this.lblRemark_Edit.Multiline = true;
            this.lblRemark_Edit.Name = "lblRemark_Edit";
            this.lblRemark_Edit.ReadOnly = true;
            this.lblRemark_Edit.Size = new System.Drawing.Size(117, 20);
            this.lblRemark_Edit.TabIndex = 27;
            this.lblRemark_Edit.Text = "备注";
            // 
            // lblTaskRunDll_Edit
            // 
            this.lblTaskRunDll_Edit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblTaskRunDll_Edit.Location = new System.Drawing.Point(458, 11);
            this.lblTaskRunDll_Edit.Multiline = true;
            this.lblTaskRunDll_Edit.Name = "lblTaskRunDll_Edit";
            this.lblTaskRunDll_Edit.ReadOnly = true;
            this.lblTaskRunDll_Edit.Size = new System.Drawing.Size(117, 20);
            this.lblTaskRunDll_Edit.TabIndex = 18;
            this.lblTaskRunDll_Edit.Tag = "*";
            this.lblTaskRunDll_Edit.Text = "任务工程名称(dll)";
            // 
            // lblTaskName_Edit
            // 
            this.lblTaskName_Edit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblTaskName_Edit.Location = new System.Drawing.Point(458, 47);
            this.lblTaskName_Edit.Multiline = true;
            this.lblTaskName_Edit.Name = "lblTaskName_Edit";
            this.lblTaskName_Edit.ReadOnly = true;
            this.lblTaskName_Edit.Size = new System.Drawing.Size(117, 20);
            this.lblTaskName_Edit.TabIndex = 23;
            this.lblTaskName_Edit.Tag = "*";
            this.lblTaskName_Edit.Text = "任务名称";
            // 
            // lblTaskTypeID_Edit
            // 
            this.lblTaskTypeID_Edit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblTaskTypeID_Edit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTaskTypeID_Edit.Location = new System.Drawing.Point(21, 47);
            this.lblTaskTypeID_Edit.Multiline = true;
            this.lblTaskTypeID_Edit.Name = "lblTaskTypeID_Edit";
            this.lblTaskTypeID_Edit.ReadOnly = true;
            this.lblTaskTypeID_Edit.Size = new System.Drawing.Size(117, 20);
            this.lblTaskTypeID_Edit.TabIndex = 21;
            this.lblTaskTypeID_Edit.Tag = "*";
            this.lblTaskTypeID_Edit.Text = "任务类型";
            // 
            // cmbTaskType
            // 
            this.cmbTaskType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTaskType.FormattingEnabled = true;
            this.cmbTaskType.Location = new System.Drawing.Point(141, 46);
            this.cmbTaskType.Name = "cmbTaskType";
            this.cmbTaskType.Size = new System.Drawing.Size(232, 28);
            this.cmbTaskType.TabIndex = 22;
            // 
            // txtTaskRunDll_Edit
            // 
            this.txtTaskRunDll_Edit.Location = new System.Drawing.Point(578, 11);
            this.txtTaskRunDll_Edit.Name = "txtTaskRunDll_Edit";
            this.txtTaskRunDll_Edit.Size = new System.Drawing.Size(232, 26);
            this.txtTaskRunDll_Edit.TabIndex = 19;
            // 
            // txtTaskName_Edit
            // 
            this.txtTaskName_Edit.Location = new System.Drawing.Point(579, 48);
            this.txtTaskName_Edit.Name = "txtTaskName_Edit";
            this.txtTaskName_Edit.Size = new System.Drawing.Size(230, 26);
            this.txtTaskName_Edit.TabIndex = 24;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAddDb);
            this.groupBox1.Controls.Add(this.grpAddServer);
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.cmbServerDb);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(3, 344);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1022, 255);
            this.groupBox1.TabIndex = 81;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "执行服务器设置";
            // 
            // btnAddDb
            // 
            this.btnAddDb.Location = new System.Drawing.Point(391, 20);
            this.btnAddDb.Name = "btnAddDb";
            this.btnAddDb.Size = new System.Drawing.Size(103, 24);
            this.btnAddDb.TabIndex = 18;
            this.btnAddDb.Text = "添加更多服务器";
            this.btnAddDb.UseVisualStyleBackColor = true;
            this.btnAddDb.Click += new System.EventHandler(this.btnAddDb_Click);
            // 
            // grpAddServer
            // 
            this.grpAddServer.Controls.Add(this.btnTestAndAddDB);
            this.grpAddServer.Controls.Add(this.txtDbPassword);
            this.grpAddServer.Controls.Add(this.textBox12);
            this.grpAddServer.Controls.Add(this.txtDbUser);
            this.grpAddServer.Controls.Add(this.textBox10);
            this.grpAddServer.Controls.Add(this.txtDbName);
            this.grpAddServer.Controls.Add(this.textBox8);
            this.grpAddServer.Controls.Add(this.txtDbIp);
            this.grpAddServer.Controls.Add(this.textBox7);
            this.grpAddServer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpAddServer.Location = new System.Drawing.Point(3, 66);
            this.grpAddServer.Name = "grpAddServer";
            this.grpAddServer.Size = new System.Drawing.Size(1016, 186);
            this.grpAddServer.TabIndex = 32;
            this.grpAddServer.TabStop = false;
            this.grpAddServer.Text = "添加更多服务器";
            this.grpAddServer.Visible = false;
            // 
            // btnTestAndAddDB
            // 
            this.btnTestAndAddDB.Location = new System.Drawing.Point(13, 120);
            this.btnTestAndAddDB.Name = "btnTestAndAddDB";
            this.btnTestAndAddDB.Size = new System.Drawing.Size(119, 23);
            this.btnTestAndAddDB.TabIndex = 18;
            this.btnTestAndAddDB.Text = "测试连接并添加";
            this.btnTestAndAddDB.UseVisualStyleBackColor = true;
            this.btnTestAndAddDB.Click += new System.EventHandler(this.btnTestAndAddDB_Click);
            // 
            // txtDbPassword
            // 
            this.txtDbPassword.Location = new System.Drawing.Point(534, 69);
            this.txtDbPassword.Name = "txtDbPassword";
            this.txtDbPassword.Size = new System.Drawing.Size(232, 26);
            this.txtDbPassword.TabIndex = 17;
            // 
            // textBox12
            // 
            this.textBox12.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox12.Location = new System.Drawing.Point(412, 69);
            this.textBox12.Multiline = true;
            this.textBox12.Name = "textBox12";
            this.textBox12.ReadOnly = true;
            this.textBox12.Size = new System.Drawing.Size(119, 20);
            this.textBox12.TabIndex = 16;
            this.textBox12.Tag = "*";
            this.textBox12.Text = "密码";
            // 
            // txtDbUser
            // 
            this.txtDbUser.Location = new System.Drawing.Point(534, 31);
            this.txtDbUser.Name = "txtDbUser";
            this.txtDbUser.Size = new System.Drawing.Size(232, 26);
            this.txtDbUser.TabIndex = 17;
            // 
            // textBox10
            // 
            this.textBox10.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox10.Location = new System.Drawing.Point(412, 31);
            this.textBox10.Multiline = true;
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.Size = new System.Drawing.Size(119, 20);
            this.textBox10.TabIndex = 16;
            this.textBox10.Tag = "*";
            this.textBox10.Text = "账号";
            // 
            // txtDbName
            // 
            this.txtDbName.Location = new System.Drawing.Point(135, 69);
            this.txtDbName.Name = "txtDbName";
            this.txtDbName.Size = new System.Drawing.Size(232, 26);
            this.txtDbName.TabIndex = 17;
            // 
            // textBox8
            // 
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox8.Location = new System.Drawing.Point(13, 69);
            this.textBox8.Multiline = true;
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(119, 20);
            this.textBox8.TabIndex = 16;
            this.textBox8.Tag = "*";
            this.textBox8.Text = "数据库名";
            // 
            // txtDbIp
            // 
            this.txtDbIp.Location = new System.Drawing.Point(135, 31);
            this.txtDbIp.Name = "txtDbIp";
            this.txtDbIp.Size = new System.Drawing.Size(232, 26);
            this.txtDbIp.TabIndex = 17;
            // 
            // textBox7
            // 
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox7.Location = new System.Drawing.Point(13, 31);
            this.textBox7.Multiline = true;
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(119, 20);
            this.textBox7.TabIndex = 16;
            this.textBox7.Tag = "*";
            this.textBox7.Text = "IP地址";
            // 
            // textBox5
            // 
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox5.Location = new System.Drawing.Point(16, 22);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(123, 20);
            this.textBox5.TabIndex = 30;
            this.textBox5.Tag = "*";
            this.textBox5.Text = "执行服务器";
            // 
            // cmbServerDb
            // 
            this.cmbServerDb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServerDb.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbServerDb.FormattingEnabled = true;
            this.cmbServerDb.Location = new System.Drawing.Point(140, 21);
            this.cmbServerDb.Name = "cmbServerDb";
            this.cmbServerDb.Size = new System.Drawing.Size(230, 28);
            this.cmbServerDb.TabIndex = 31;
            // 
            // tbPageTaskLog
            // 
            this.tbPageTaskLog.Controls.Add(this.dgvTaskLog);
            this.tbPageTaskLog.Controls.Add(this.panel3);
            this.tbPageTaskLog.Controls.Add(this.grpRunningTask);
            this.tbPageTaskLog.Location = new System.Drawing.Point(4, 24);
            this.tbPageTaskLog.Name = "tbPageTaskLog";
            this.tbPageTaskLog.Size = new System.Drawing.Size(1028, 602);
            this.tbPageTaskLog.TabIndex = 2;
            this.tbPageTaskLog.Text = "运行日志";
            this.tbPageTaskLog.UseVisualStyleBackColor = true;
            // 
            // dgvTaskLog
            // 
            this.dgvTaskLog.AllowUserToAddRows = false;
            this.dgvTaskLog.AllowUserToDeleteRows = false;
            this.dgvTaskLog.AllowUserToResizeColumns = false;
            this.dgvTaskLog.AllowUserToResizeRows = false;
            this.dgvTaskLog.BackgroundColor = System.Drawing.Color.White;
            this.dgvTaskLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTaskLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTaskID_His,
            this.colTaskName_His,
            this.colTaskTypeID_His,
            this.colRunStatus_His,
            this.colLogContent_His,
            this.colBeginRun_His});
            this.dgvTaskLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTaskLog.Location = new System.Drawing.Point(269, 147);
            this.dgvTaskLog.Name = "dgvTaskLog";
            this.dgvTaskLog.ReadOnly = true;
            this.dgvTaskLog.RowHeadersWidth = 20;
            this.dgvTaskLog.Size = new System.Drawing.Size(759, 455);
            this.dgvTaskLog.TabIndex = 5;
            // 
            // colTaskID_His
            // 
            this.colTaskID_His.DataPropertyName = "TaskID";
            this.colTaskID_His.HeaderText = "任务工程ID";
            this.colTaskID_His.MinimumWidth = 6;
            this.colTaskID_His.Name = "colTaskID_His";
            this.colTaskID_His.ReadOnly = true;
            this.colTaskID_His.Width = 125;
            // 
            // colTaskName_His
            // 
            this.colTaskName_His.DataPropertyName = "TaskName";
            this.colTaskName_His.FillWeight = 120F;
            this.colTaskName_His.HeaderText = "任务名称";
            this.colTaskName_His.MinimumWidth = 6;
            this.colTaskName_His.Name = "colTaskName_His";
            this.colTaskName_His.ReadOnly = true;
            this.colTaskName_His.Width = 120;
            // 
            // colTaskTypeID_His
            // 
            this.colTaskTypeID_His.DataPropertyName = "TaskType";
            this.colTaskTypeID_His.FillWeight = 120F;
            this.colTaskTypeID_His.HeaderText = "任务类型";
            this.colTaskTypeID_His.MinimumWidth = 6;
            this.colTaskTypeID_His.Name = "colTaskTypeID_His";
            this.colTaskTypeID_His.ReadOnly = true;
            this.colTaskTypeID_His.Width = 120;
            // 
            // colRunStatus_His
            // 
            this.colRunStatus_His.DataPropertyName = "Status";
            this.colRunStatus_His.FillWeight = 120F;
            this.colRunStatus_His.HeaderText = "执行结果";
            this.colRunStatus_His.MinimumWidth = 6;
            this.colRunStatus_His.Name = "colRunStatus_His";
            this.colRunStatus_His.ReadOnly = true;
            this.colRunStatus_His.Width = 120;
            // 
            // colLogContent_His
            // 
            this.colLogContent_His.DataPropertyName = "RunInfo";
            this.colLogContent_His.FillWeight = 150F;
            this.colLogContent_His.HeaderText = "执行信息";
            this.colLogContent_His.MinimumWidth = 6;
            this.colLogContent_His.Name = "colLogContent_His";
            this.colLogContent_His.ReadOnly = true;
            this.colLogContent_His.Width = 150;
            // 
            // colBeginRun_His
            // 
            this.colBeginRun_His.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colBeginRun_His.DataPropertyName = "BeginTime";
            this.colBeginRun_His.FillWeight = 110F;
            this.colBeginRun_His.HeaderText = "执行时间";
            this.colBeginRun_His.MinimumWidth = 6;
            this.colBeginRun_His.Name = "colBeginRun_His";
            this.colBeginRun_His.ReadOnly = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cbxTaskLogDate);
            this.panel3.Controls.Add(this.lblTaskID_His);
            this.panel3.Controls.Add(this.txtTaskID_His);
            this.panel3.Controls.Add(this.lblTaskTypeID_His);
            this.panel3.Controls.Add(this.lblTaskName_His);
            this.panel3.Controls.Add(this.txtTaskName_His);
            this.panel3.Controls.Add(this.btnQuery);
            this.panel3.Controls.Add(this.DateRunTime_End);
            this.panel3.Controls.Add(this.DateRunTime_Begin);
            this.panel3.Controls.Add(this.cbxTaskType_His);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(269, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(759, 147);
            this.panel3.TabIndex = 3;
            // 
            // cbxTaskLogDate
            // 
            this.cbxTaskLogDate.BackColor = System.Drawing.SystemColors.Control;
            this.cbxTaskLogDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxTaskLogDate.Location = new System.Drawing.Point(22, 78);
            this.cbxTaskLogDate.Name = "cbxTaskLogDate";
            this.cbxTaskLogDate.Size = new System.Drawing.Size(94, 24);
            this.cbxTaskLogDate.TabIndex = 21;
            this.cbxTaskLogDate.Text = "日期范围";
            this.cbxTaskLogDate.UseVisualStyleBackColor = false;
            this.cbxTaskLogDate.CheckedChanged += new System.EventHandler(this.cbxTaskLogDate_CheckedChanged);
            // 
            // lblTaskID_His
            // 
            this.lblTaskID_His.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblTaskID_His.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTaskID_His.Location = new System.Drawing.Point(22, 32);
            this.lblTaskID_His.Multiline = true;
            this.lblTaskID_His.Name = "lblTaskID_His";
            this.lblTaskID_His.ReadOnly = true;
            this.lblTaskID_His.Size = new System.Drawing.Size(94, 20);
            this.lblTaskID_His.TabIndex = 11;
            this.lblTaskID_His.Text = "任务工程ID";
            // 
            // txtTaskID_His
            // 
            this.txtTaskID_His.Location = new System.Drawing.Point(117, 32);
            this.txtTaskID_His.Name = "txtTaskID_His";
            this.txtTaskID_His.Size = new System.Drawing.Size(203, 26);
            this.txtTaskID_His.TabIndex = 12;
            // 
            // lblTaskTypeID_His
            // 
            this.lblTaskTypeID_His.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblTaskTypeID_His.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTaskTypeID_His.Location = new System.Drawing.Point(347, 78);
            this.lblTaskTypeID_His.Multiline = true;
            this.lblTaskTypeID_His.Name = "lblTaskTypeID_His";
            this.lblTaskTypeID_His.ReadOnly = true;
            this.lblTaskTypeID_His.Size = new System.Drawing.Size(94, 20);
            this.lblTaskTypeID_His.TabIndex = 18;
            this.lblTaskTypeID_His.Text = "任务类型";
            // 
            // lblTaskName_His
            // 
            this.lblTaskName_His.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblTaskName_His.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTaskName_His.Location = new System.Drawing.Point(347, 32);
            this.lblTaskName_His.Multiline = true;
            this.lblTaskName_His.Name = "lblTaskName_His";
            this.lblTaskName_His.ReadOnly = true;
            this.lblTaskName_His.Size = new System.Drawing.Size(94, 20);
            this.lblTaskName_His.TabIndex = 13;
            this.lblTaskName_His.Text = "任务名称";
            // 
            // txtTaskName_His
            // 
            this.txtTaskName_His.Location = new System.Drawing.Point(442, 32);
            this.txtTaskName_His.Name = "txtTaskName_His";
            this.txtTaskName_His.Size = new System.Drawing.Size(181, 26);
            this.txtTaskName_His.TabIndex = 14;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(664, 46);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 36);
            this.btnQuery.TabIndex = 20;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // DateRunTime_End
            // 
            this.DateRunTime_End.Enabled = false;
            this.DateRunTime_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateRunTime_End.Location = new System.Drawing.Point(225, 78);
            this.DateRunTime_End.Name = "DateRunTime_End";
            this.DateRunTime_End.Size = new System.Drawing.Size(95, 26);
            this.DateRunTime_End.TabIndex = 17;
            this.DateRunTime_End.Value = new System.DateTime(2025, 1, 1, 0, 0, 0, 0);
            // 
            // DateRunTime_Begin
            // 
            this.DateRunTime_Begin.Enabled = false;
            this.DateRunTime_Begin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateRunTime_Begin.Location = new System.Drawing.Point(117, 78);
            this.DateRunTime_Begin.Name = "DateRunTime_Begin";
            this.DateRunTime_Begin.Size = new System.Drawing.Size(102, 26);
            this.DateRunTime_Begin.TabIndex = 16;
            this.DateRunTime_Begin.Value = new System.DateTime(2025, 1, 2, 0, 0, 0, 0);
            // 
            // cbxTaskType_His
            // 
            this.cbxTaskType_His.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTaskType_His.DropDownWidth = 303;
            this.cbxTaskType_His.FormattingEnabled = true;
            this.cbxTaskType_His.Location = new System.Drawing.Point(443, 76);
            this.cbxTaskType_His.Name = "cbxTaskType_His";
            this.cbxTaskType_His.Size = new System.Drawing.Size(180, 28);
            this.cbxTaskType_His.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(217, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(10, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "-";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpRunningTask
            // 
            this.grpRunningTask.Controls.Add(this.lsvRunningTask);
            this.grpRunningTask.Controls.Add(this.panel4);
            this.grpRunningTask.Dock = System.Windows.Forms.DockStyle.Left;
            this.grpRunningTask.Location = new System.Drawing.Point(0, 0);
            this.grpRunningTask.Name = "grpRunningTask";
            this.grpRunningTask.Size = new System.Drawing.Size(269, 602);
            this.grpRunningTask.TabIndex = 6;
            this.grpRunningTask.TabStop = false;
            this.grpRunningTask.Text = "正在进行中的任务";
            // 
            // lsvRunningTask
            // 
            this.lsvRunningTask.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lsvColTaskId,
            this.lsvColTaskTime,
            this.lsvColTaskStatus});
            this.lsvRunningTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvRunningTask.GridLines = true;
            this.lsvRunningTask.HideSelection = false;
            this.lsvRunningTask.LargeImageList = this.imageList1;
            this.lsvRunningTask.Location = new System.Drawing.Point(3, 151);
            this.lsvRunningTask.MultiSelect = false;
            this.lsvRunningTask.Name = "lsvRunningTask";
            this.lsvRunningTask.Size = new System.Drawing.Size(263, 448);
            this.lsvRunningTask.SmallImageList = this.imageList1;
            this.lsvRunningTask.TabIndex = 0;
            this.lsvRunningTask.UseCompatibleStateImageBehavior = false;
            this.lsvRunningTask.View = System.Windows.Forms.View.Details;
            // 
            // lsvColTaskId
            // 
            this.lsvColTaskId.Text = "任务ID";
            this.lsvColTaskId.Width = 87;
            // 
            // lsvColTaskTime
            // 
            this.lsvColTaskTime.Text = "开始时间";
            this.lsvColTaskTime.Width = 96;
            // 
            // lsvColTaskStatus
            // 
            this.lsvColTaskStatus.Text = "状态";
            this.lsvColTaskStatus.Width = 73;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.countdownTimer1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 22);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(263, 129);
            this.panel4.TabIndex = 1;
            // 
            // countdownTimer1
            // 
            this.countdownTimer1.BackColor = System.Drawing.Color.Transparent;
            this.countdownTimer1.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countdownTimer1.Location = new System.Drawing.Point(32, 28);
            this.countdownTimer1.Margin = new System.Windows.Forms.Padding(4);
            this.countdownTimer1.Name = "countdownTimer1";
            this.countdownTimer1.Size = new System.Drawing.Size(195, 70);
            this.countdownTimer1.TabIndex = 0;
            this.countdownTimer1.TimeBackGroundColor = System.Drawing.Color.Silver;
            this.countdownTimer1.TimeCountDown = 10;
            this.countdownTimer1.TimeCountDownStart = false;
            this.countdownTimer1.TimeEveryIntervalX = 5;
            this.countdownTimer1.TimeNumColor = System.Drawing.Color.Red;
            this.countdownTimer1.TimePointX = 20;
            this.countdownTimer1.TimePointY = 20;
            this.countdownTimer1.TimeRectangleUnit = 6;
            // 
            // timerTask
            // 
            this.timerTask.Enabled = true;
            this.timerTask.Interval = 10000;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 22);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 22);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 22);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 22);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 22);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 22);
            // 
            // toolStripMain
            // 
            this.toolStripMain.AutoSize = false;
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.toolStripSeparator1,
            this.btnDel,
            this.toolStripSeparator3,
            this.btnEdit,
            this.toolStripSeparator2,
            this.btnSave,
            this.toolStripSeparator6,
            this.btnCancel,
            this.toolStripSeparator4,
            this.btnStartTask,
            this.toolStripSeparator5,
            this.btnEndTask,
            this.toolStripLabel1});
            this.toolStripMain.Location = new System.Drawing.Point(0, 28);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Padding = new System.Windows.Forms.Padding(0);
            this.toolStripMain.Size = new System.Drawing.Size(1036, 22);
            this.toolStripMain.TabIndex = 4;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(63, 19);
            this.btnAdd.Text = "新增";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDel
            // 
            this.btnDel.Image = ((System.Drawing.Image)(resources.GetObject("btnDel.Image")));
            this.btnDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(63, 19);
            this.btnDel.Text = "删除";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(63, 19);
            this.btnEdit.Text = "修改";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(63, 19);
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(63, 19);
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnStartTask
            // 
            this.btnStartTask.Image = ((System.Drawing.Image)(resources.GetObject("btnStartTask.Image")));
            this.btnStartTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStartTask.Name = "btnStartTask";
            this.btnStartTask.Size = new System.Drawing.Size(93, 19);
            this.btnStartTask.Text = "开始运行";
            this.btnStartTask.Click += new System.EventHandler(this.btnStartTask_Click);
            // 
            // btnEndTask
            // 
            this.btnEndTask.Image = ((System.Drawing.Image)(resources.GetObject("btnEndTask.Image")));
            this.btnEndTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEndTask.Name = "btnEndTask";
            this.btnEndTask.Size = new System.Drawing.Size(93, 19);
            this.btnEndTask.Text = "结束运行";
            this.btnEndTask.Click += new System.EventHandler(this.btnEndTask_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(0, 19);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Image = global::AutoMailWinForm.Properties.Resources.btnQuery;
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(78, 24);
            this.退出ToolStripMenuItem.Text = "Help";
            // 
            // MainMenu
            // 
            this.MainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.退出ToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(1036, 28);
            this.MainMenu.TabIndex = 3;
            this.MainMenu.Text = "menuStrip1";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 680);
            this.Controls.Add(this.tbContolTask);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.MainMenu);
            this.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "邮件自动发送平台";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tbContolTask.ResumeLayout(false);
            this.tbPageTaskList.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvList)).EndInit();
            this.tbPageTaskEdit.ResumeLayout(false);
            this.grpSetRunServer.ResumeLayout(false);
            this.grpSetRunServer.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpAddServer.ResumeLayout(false);
            this.grpAddServer.PerformLayout();
            this.tbPageTaskLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskLog)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.grpRunningTask.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl tbContolTask;
        private System.Windows.Forms.TabPage tbPageTaskEdit;
        private System.Windows.Forms.TabPage tbPageTaskLog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chbIsUse;
        private System.Windows.Forms.TextBox lblTaskID_Edit;
        private System.Windows.Forms.TextBox txtTaskID_Edit;
        private System.Windows.Forms.TextBox txtRemark_Edit;
        private System.Windows.Forms.TextBox lblRemark_Edit;
        private System.Windows.Forms.TextBox lblTaskRunDll_Edit;
        private System.Windows.Forms.TextBox lblTaskName_Edit;
        private System.Windows.Forms.TextBox lblTaskTypeID_Edit;
        private System.Windows.Forms.ComboBox cmbTaskType;
        private System.Windows.Forms.TextBox txtTaskRunDll_Edit;
        private System.Windows.Forms.TextBox txtTaskName_Edit;
        private System.Windows.Forms.GroupBox grpSetRunServer;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Timer timerTask;
        private System.Windows.Forms.TabPage tbPageTaskList;
        private System.Windows.Forms.DataGridView DgvList;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnDel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnCancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnStartTask;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnEndTask;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.ComboBox cmbServerDb;
        private System.Windows.Forms.TextBox txtMailErrorList;
        private System.Windows.Forms.TextBox lblErrorEmail_Edit;
        private System.Windows.Forms.TextBox txtMailCcList;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox txtMailToList;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnAddDb;
        private System.Windows.Forms.GroupBox grpAddServer;
        private System.Windows.Forms.Button btnTestAndAddDB;
        private System.Windows.Forms.TextBox txtDbPassword;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox txtDbUser;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox txtDbName;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox txtDbIp;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox lblTaskID_His;
        private System.Windows.Forms.TextBox txtTaskID_His;
        private System.Windows.Forms.TextBox lblTaskTypeID_His;
        private System.Windows.Forms.TextBox lblTaskName_His;
        private System.Windows.Forms.TextBox txtTaskName_His;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.DateTimePicker DateRunTime_End;
        private System.Windows.Forms.DateTimePicker DateRunTime_Begin;
        private System.Windows.Forms.ComboBox cbxTaskType_His;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dgvTaskLog;
        private System.Windows.Forms.GroupBox grpRunningTask;
        private System.Windows.Forms.CheckBox cbxTaskLogDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskID_His;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskName_His;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskTypeID_His;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRunStatus_His;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLogContent_His;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBeginRun_His;
        private System.Windows.Forms.ListView lsvRunningTask;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ColumnHeader lsvColTaskId;
        private System.Windows.Forms.ColumnHeader lsvColTaskTime;
        private System.Windows.Forms.ColumnHeader lsvColTaskStatus;
        private System.Windows.Forms.ListView lsvLog;
        private System.Windows.Forms.ColumnHeader colLogTime;
        private System.Windows.Forms.ColumnHeader colLogTaskName;
        private System.Windows.Forms.ColumnHeader colInfoType;
        private System.Windows.Forms.ColumnHeader colLogText;
        private Common.CountdownTimer countdownTimer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskRunDll;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsUse;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskToList;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskCcList;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskErrorList;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSecretKey;
    }
}

