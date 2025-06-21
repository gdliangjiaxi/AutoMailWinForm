using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.VisualStyles;
using Common;
using DataAccess;
using Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace AutoMailWinForm
{
    public partial class FrmMain : Form
    {


        #region 定义变量

        string dllPath = Application.StartupPath + "\\dll\\";

        MdlMain mdlMain = new MdlMain();

      


        List<Dictionary<string, List<CancellationTokenSource>>> cancelTaskList
            = new List<Dictionary<string, List<CancellationTokenSource>>>(); //任务列表,Dictionary的key为taskid，value为CancellationToken,用于手动结束任务

        enum FrmMainStaus
        {
            常规,
            新增,
            修改,
            浏览


        }
        //删除窗口提示
        FrmMessage frmDelMessage;
        //关闭窗口提示
        FrmMessage frmMainClosing;

        //public static event Action<string> TaskEnding;//手动结束任务委托,参数为taskid



        // List<Task> tasks = new List<Task>();
        #endregion

        public FrmMain()
        {
            InitializeComponent();

        }


        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            InitControls();

        }

        /// <summary>
        /// 初始化窗体控件
        /// </summary>  
        /// <exception cref="NotImplementedException"></exception>
        private void InitControls()
        {

            //服务器下拉框数据源
            ComboxHelper.CmbDataBind(this.cmbServerDb, DalMain.InitControl(mdlMain).Tables[0],"ServerName", "ServerName", "", false);
            //绑定Dgv数据源
            this.DgvList.DataSource = DalMain.GetList(mdlMain).Tables[0];
            this.cmbTaskType.DataSource = new string[] { "自动邮件" }; //任务类型选项框


            //日志界面控件初始化
            this.cbxTaskType_His.DataSource = new string[] {"", "自动邮件" }; //任务类型选项框
            this.grpRunningTask.Text = string.Format("正在进行的任务({0}s查询一次)", (this.timerTask.Interval / 1000).ToString()); //定时器间隔时间显示
            this.DateRunTime_Begin.Value = DateTime.Now.AddDays(-7); //默认查询时间范围为7天
            this.DateRunTime_End.Value = DateTime.Now;
            this.lsvColTaskId.Width = 96;
            this.lsvColTaskTime.Width = 117;
            this.lsvColTaskStatus.Width = this.lsvRunningTask.Width - this.lsvColTaskId.Width- this.lsvColTaskTime.Width-5;


            //绑定全局日志委托
            LogHelper.LogAction += WriteLog;

            //设置窗体状态
            this.Tag = FrmMainStaus.常规;


            ToolStripBtnControl(FrmMainStaus.常规);   //工具条按钮权限设置
            //自定义控件倒计时器
            countdownTimer1.TimeCountDown = this.timerTask.Interval / 1000;
            countdownTimer1.TimeCountDownStart = true;
            //初始化查询sql任务列表定时器
            this.timerTask.Enabled = true;
            this.timerTask.Tick += new EventHandler(timerTask_Tick);
            this.timerTask.Start();         
            

        }


        /// <summary>
        /// 任务读取定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void timerTask_Tick(object sender, EventArgs e)
        {


            DataSet dsRunTask=DalMain.GetRunList(mdlMain);

            //绑定正在运行的，准备运行的任务列表到lsv
            lsvRunningTask_DataBind(dsRunTask.Tables[0]);



            //运行要运行的任务       
            foreach (DataRow dataRow in dsRunTask.Tables[1].Rows)
            {
                string taskId = dataRow["TaskID"].ToString();

                string taskDllId = dataRow["TaskRunDll"].ToString();

                string toList = dataRow["ToList"].ToString();

                string ccList = dataRow["CcList"].ToString();

                string secretKey = dataRow["SecretKey"].ToString();

                string errorList = dataRow["ErrorList"].ToString();

                //创建任务取消令牌
                Dictionary<string,List<CancellationTokenSource>>  taskCancelDic=CreateTaskCancelToken(taskId,out CancellationToken cancelToken, out CancellationTokenSource tokenSource);

                Task.Run(() =>
                {

                    LoadDll(taskId, dllPath + taskDllId, new object[] { taskId, toList, ccList, secretKey, cancelToken }, errorList, taskCancelDic, tokenSource);


                },cancelToken);

            }

            //tasks.Clear(); //清空任务列表

        }
        /// <summary>
        /// 绑定正在运行的，准备运行的任务列表到lsv
        /// </summary>
        /// <param name="dataTable">数据源dt</param>
        private void lsvRunningTask_DataBind(DataTable dtRunningTask)
        {
            //先清空
            lsvRunningTask.Items.Clear();

            List<ListViewItem> listViewItems = new List<ListViewItem>(); 

            foreach (DataRow drTaskRow in dtRunningTask.Rows)
            {
                string taskId=drTaskRow["TaskID"].ToString(); //任务ID
                string taskTime = DateHelper.getStrNowTime(DateTime.Parse(drTaskRow["BeginTime"].ToString()));
                string taskStatus = drTaskRow["RunFlag"].ToString();

                ListViewItem listViewItem = new ListViewItem(" "+taskId, 3);
                listViewItem.SubItems.Add(taskTime);
                listViewItem.SubItems.Add(taskStatus);
                listViewItems.Add(listViewItem);

            }
            lsvRunningTask.Items.AddRange(listViewItems.ToArray());




        }





        /// <summary>
        /// 日志写入界面和数据库
        /// </summary>
        public void WriteLog(string taskId, int levelIndex, string levelStr, string logStr)
        {
            //写入界面记录
            DateTime logTime = DateTime.Now;
            ListViewItem listViewItem = new ListViewItem(DateHelper.getStrNowTime(logTime), levelIndex);
            listViewItem.SubItems.Add(taskId);
            listViewItem.SubItems.Add(levelStr);
            listViewItem.SubItems.Add(logStr);
            if (this.lsvLog.InvokeRequired) //更新控件只能是UI线程
            {

                this.lsvLog.Invoke((Action)(() =>
                {

                    this.lsvLog.Items.Insert(0, listViewItem);
                }));


            }
            else
            {
                this.lsvLog.Items.Insert(0, listViewItem);
            }
            //写入数据库
            DalMain.WriteLog(new MdlMain() { TaskId = taskId, LogTime = logTime, LogLevel = levelStr, Logstr = logStr });
        }


        /// <summary>
        /// 调用AutoMail的DLL文件
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <param name="dllPath">DLL文件路径</param>
        /// <param name="parameters">MainMethod方法的接收参数</param>
        /// <param name="errorList">错误列表</param>
        /// <param name="taskCancelDic">任务取消字典</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <param name="manuallySend">是否手动发送</param>
        private void LoadDll(string taskId, string dllPath, object[] parameters, string errorList, Dictionary<string, List<CancellationTokenSource>> taskCancelDic, CancellationTokenSource cancellationTokenSource, bool manuallySend = false)
        {

            MdlMain mdlMainLoad = new MdlMain();

            string loadResult = string.Empty; ;

            //外部捕获调用dll异常

            try

            {

                //反射导入dll

           

                Assembly assembly = Assembly.LoadFrom(dllPath);

                mdlMainLoad.TaskId = taskId;

                Type[] types = assembly.GetTypes();

                 

                foreach (Type type in types)

                {

                    //判断dll是否实现了接口和类名

                    MethodInfo method = type.GetMethod("MainMethod");

                    if (type.Name == "MainClass" && method != null)

                    {

                        //实例化并调用方法

                        object obj = Activator.CreateInstance(type);

                        //内部捕获方法体异常

                        try

                        {

                            //手动点击发送不需要标记数据库

                            if (manuallySend)

                            {

                                LogHelper.LogAction?.Invoke(taskId, LogHelper.INFO, LogHelper.SEND_BEGIN_MANUALLY, "");//写入日志

                            }

                            else

                            {

                                DalMain.MarkRunTask(mdlMainLoad);       //标记正在处理

                                LogHelper.LogAction?.Invoke(taskId, LogHelper.INFO, LogHelper.SEND_BEGIN, "");//写入日志

                            }

                            loadResult = method.Invoke(obj, parameters)?.ToString();    //MainMethod方法返回值


                            if (string.IsNullOrEmpty(loadResult))   //字符串返回值为空说明成功运行,否则运行失败,返回值为错误信息

                            {



                                LogHelper.LogAction?

                                    .Invoke(taskId, LogHelper.INFO, manuallySend ? LogHelper.SEND_SUCCESS_MANUALLY : LogHelper.SEND_SUCCESS, loadResult);//写入日志

                            }

                            else

                            {

                                LogHelper.LogAction?

                                    .Invoke(taskId, LogHelper.ERROR, manuallySend ? LogHelper.SEND_FAIL_MANUALLY : LogHelper.SEND_FAIL, loadResult);

                            }

                        }

                        catch (Exception ex)

                        {



                            LogHelper.LogAction?

                                .Invoke(taskId, LogHelper.ERROR, manuallySend ? LogHelper.SEND_EXCEPTION_MANUALLY : LogHelper.SEND_EXCEPTION, "dll运行方法内部异常,请检查具体实现方法 " + ex.Message.ToString());

                        }
                    }

                }

            }

            catch (Exception ex)

            {

                LogHelper.LogAction?

                    .Invoke(taskId, LogHelper.ERROR, manuallySend ? LogHelper.SEND_EXCEPTION_MANUALLY : LogHelper.SEND_EXCEPTION, "调用dll文件异常,请检查dll文件是否有效" + ex.Message.ToString());



            }

            finally

            {

                //手动发送就不需要删除数据库任务

                if (manuallySend)

                {

                    LogHelper.LogAction?.Invoke(taskId, LogHelper.INFO, LogHelper.SEND_END_MANUALLY, "");//写入日志

                }

                else

                {

                    DalMain.DeleteRunTask(mdlMainLoad);       //处理完成删除任务

                    LogHelper.LogAction?.Invoke(taskId, LogHelper.INFO, LogHelper.SEND_END, "");//写入日志

                }



                if (!string.IsNullOrEmpty(loadResult) && !string.IsNullOrEmpty(errorList))

                {

                    //发送失败邮件通知



                }

                //发送完成就取消令牌
                DeleteTaskCancelToken(taskId, taskCancelDic, cancellationTokenSource);
            }



        }

        /// <summary>
        /// 任务增加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //设置tag标签设置状态值
            this.Tag = FrmMainStaus.新增;
            ClearTaskParameter();
            tbContolTask.SelectedTab = tbPageTaskEdit;




        }
        /// <summary>
        /// 任务取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //如果在新增任务状态点击任务列表，那么取消就应该先转回编辑页面
            if (tbContolTask.SelectedTab == tbPageTaskList)
            {
                tbContolTask.SelectedIndexChanged -= tbPageTaskLog_SelectedIndexChanged;//转回编辑页面不要触发事件
                tbContolTask.SelectedTab = tbPageTaskEdit;
                tbContolTask.SelectedIndexChanged += tbPageTaskLog_SelectedIndexChanged;
            }
            else
            {
                //设置tag标签设置状态值
                this.Tag = FrmMainStaus.常规;
                ClearTaskParameter();
                tbContolTask.SelectedTab = tbPageTaskList;
            }


        }
        /// <summary>
        /// 任务修改按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {

            tbContolTask.SelectedTab = tbPageTaskEdit;
        }

        /// <summary>
        /// 状态控制工具条按钮权限
        /// </summary>
        /// <param name="toolSkipStaus"></param>
        private void ToolStripBtnControl(FrmMainStaus frmMainStaus)
        {
            switch (frmMainStaus)
            {
                case FrmMainStaus.常规:
                    this.btnAdd.Enabled = true;
                    this.btnDel.Enabled = true;
                    this.btnEdit.Enabled = true;
                    this.btnSave.Enabled = false;
                    this.btnCancel.Enabled = false;
                    this.btnStartTask.Enabled = true;
                    this.btnEndTask.Enabled = true;
                    break;
                case FrmMainStaus.新增:
                    this.btnAdd.Enabled = false;
                    this.btnDel.Enabled = false;
                    this.btnEdit.Enabled = false;
                    this.btnSave.Enabled = true;
                    this.btnCancel.Enabled = true;
                    this.btnStartTask.Enabled = false;
                    this.btnEndTask.Enabled = false;
                    break;
                case FrmMainStaus.修改:
                    this.btnAdd.Enabled = false;
                    this.btnDel.Enabled = false;
                    this.btnEdit.Enabled = false;
                    this.btnSave.Enabled = true;
                    this.btnCancel.Enabled = true;
                    this.btnStartTask.Enabled = false;
                    this.btnEndTask.Enabled = false;
                    break;
                case FrmMainStaus.浏览:
                    this.btnAdd.Enabled = false;
                    this.btnDel.Enabled = false;
                    this.btnEdit.Enabled = false;
                    this.btnSave.Enabled = false;
                    this.btnCancel.Enabled = false;
                    this.btnStartTask.Enabled = false;
                    this.btnEndTask.Enabled = false;
                    break;

            }



        }
        /// <summary>
        /// 任务保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckMustTxt(out string taskId, out string taskDll, out string taskName, out string taskMailToList))
            {
                MdlMain mdlMain = new MdlMain();
                mdlMain.TaskId = taskId;
                mdlMain.TaskDll = taskDll;
                mdlMain.TaskMailToList = taskMailToList;
                mdlMain.TaskUse = this.chbIsUse.Checked ? '1' : '0';
                mdlMain.TaskMailCcList = this.txtMailCcList.Text.ToString().Trim();
                mdlMain.TaskRemark = this.txtRemark_Edit.Text.ToString().Trim();
                mdlMain.TaskMailErrorList = this.txtMailErrorList.Text.ToString().Trim();
                mdlMain.TaskName = taskName;
                mdlMain.TaskType = cmbTaskType.SelectedItem.ToString();
                mdlMain.TaskServerDb = this.cmbServerDb.Text.ToString();
                bool result = false;
                if ((FrmMainStaus)this.Tag == FrmMainStaus.新增)
                {
                    result = DalMain.InsertTask(mdlMain);       //新增任务
                }
                else if ((FrmMainStaus)this.Tag == FrmMainStaus.修改)
                {
                    result = DalMain.UpdateTask(mdlMain);       //修改任务
                }


                if (result)//成功
                {
                    new FrmMessage(this.Tag.ToString() + "成功").Show();
                    ClearTaskParameter();
                    //重新绑定Dgv数据源
                    this.DgvList.DataSource = DalMain.GetList(mdlMain).Tables[0];
                    this.Tag = FrmMainStaus.常规;
                    tbContolTask.SelectedTab = tbPageTaskList;



                }
                else       //失败
                {
                    new FrmMessage(this.Tag.ToString() + "失败").Show();
                }





            }

        }


        /// <summary>
        /// 检查参数必填项
        /// </summary>
        /// <param name="taskId">Output parameter for Task ID.</param>
        /// <param name="taskDll">Output parameter for Task DLL.</param>
        /// <param name="taskName">Output parameter for Task Name.</param>
        /// <param name="taskMailToList">Output parameter for Task Mail List.</param>
        /// <returns>True if all required fields are valid, otherwise false.</returns>
        private bool CheckMustTxt(out string taskId, out string taskDll, out string taskName, out string taskMailToList)
        {
            taskId = txtTaskID_Edit.Text.ToString().Trim();
            taskDll = txtTaskRunDll_Edit.Text.ToString().Trim();
            taskName = txtTaskName_Edit.Text.ToString().Trim();
            taskMailToList = txtMailToList.Text.ToString().Trim();

            //Linq 查询是否有重复的任务ID
            List<DataRow> lstTaskId =
                 (from i in ((DataTable)DgvList.DataSource).AsEnumerable()
                  where i.Field<string>("TaskID") == txtTaskID_Edit.Text.ToString().Trim()
                  select i).ToList();


            if (string.IsNullOrEmpty(taskId))
            {
                txtTaskID_Edit.Focus();
                new FrmMessage("任务ID不能为空").Show();
                return false;
            }
            else if (lstTaskId.Count > 0 && this.txtTaskID_Edit.Enabled)
            {
                txtTaskID_Edit.Focus();
                new FrmMessage("任务ID不能重复").Show();
                return false;
            }

            if (string.IsNullOrEmpty(taskDll))
            {
                txtTaskRunDll_Edit.Focus();
                new FrmMessage("任务Dll不能为空").Show();
                return false;
            }
            else if (taskDll.Length <= 4 || !taskDll.Substring(taskDll.Length - 4, 4).Equals(".dll"))
            {
                txtTaskRunDll_Edit.Focus();
                new FrmMessage("任务Dll名称必须包含.dll后缀").Show();
                return false;
            }
            if (string.IsNullOrEmpty(taskName))
            {
                txtTaskName_Edit.Focus();
                new FrmMessage("任务名称不能为空").Show();
                return false;
            }
            if (string.IsNullOrEmpty(taskMailToList))
            {
                txtMailToList.Focus();
                new FrmMessage("邮件收件人不能为空").Show();
                return false;
            }

            return true;
        }
        /// <summary>
        /// 清空恢复任务编辑页面参数
        /// </summary>
        private void ClearTaskParameter()
        {
            
            this.txtTaskID_Edit.Enabled = true;
            this.txtTaskID_Edit.Text = string.Empty;
            this.txtTaskRunDll_Edit.Text = string.Empty;
            this.txtTaskName_Edit.Text = string.Empty;
            this.txtMailToList.Text = string.Empty;
            this.chbIsUse.Checked = true;
            this.txtMailCcList.Text = string.Empty;
            this.txtRemark_Edit.Text = string.Empty; ;
            this.txtMailErrorList.Text = string.Empty;
            //新增服务器控件
            this.txtDbIp.Text = string.Empty;
            this.txtDbName.Text = string.Empty;
            this.txtDbUser.Text = string.Empty;
            this.txtDbPassword.Text = string.Empty;
            this.grpAddServer.Visible = false;

        }




        /// <summary>
        /// tab页切换事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbPageTaskLog_SelectedIndexChanged(object sender, EventArgs e)
        {

            FrmMainStaus frmMainStaus = (FrmMainStaus)this.Tag;



            ///切换页面任务列表，任务编辑，任务日志
            if (tbContolTask.SelectedTab == tbPageTaskList && frmMainStaus != FrmMainStaus.新增)
            {
                this.Tag = FrmMainStaus.常规; //枚举类型实际上还是值传递,frmMainStaus=FrmMainStaus.常规 改变不了this.tag的值
            }
            else if (tbContolTask.SelectedTab == tbPageTaskEdit && frmMainStaus != FrmMainStaus.新增)//点击新增按钮或者手动点击tab跳转，手动跳转就是编辑状态
            {

                string taskID = DgvList.CurrentRow.Cells["colTaskID"].Value.ToString();
                //需要重新查询数据库,因为防止后台有人更新,无法及时映射到编辑页面 
                DataTable tbTask= DalMain.GetList(mdlMain).Tables[0];
                DataRow[] dataRow = tbTask.Select($"TaskID = '{taskID}'");

                if (dataRow==null||dataRow.Length == 0)
                {
                    new FrmMessage("任务不存在或已被删除").Show();
                    //重新绑定
                    this.DgvList.DataSource = tbTask;
                    return;
                }


                this.Tag = FrmMainStaus.修改;
                this.txtTaskID_Edit.Enabled = false; //不允许修改任务id

                string taskDll = dataRow[0]["TaskRunDll"].ToString();
                string taskName = dataRow[0]["TaskName"].ToString();
                string taskMailToList = dataRow[0]["ToList"].ToString();
                string taskMailCcList = dataRow[0]["CcList"].ToString();
                string taskMailErrorList = dataRow[0]["ErrorList"].ToString();
                string taskRemark = dataRow[0]["Remark"].ToString();
                bool taskUse = bool.Parse(dataRow[0]["isUse"].ToString());
                string taskType = dataRow[0]["TaskType"].ToString();
                string serverName = dataRow[0]["ServerName"].ToString();


                //string taskDll = DgvList.CurrentRow.Cells["colTaskRunDll"].Value.ToString();
                //string taskName = DgvList.CurrentRow.Cells["colTaskName"].Value.ToString();
                //string taskMailToList = DgvList.CurrentRow.Cells["colTaskToList"].Value.ToString();
                //string taskMailCcList = DgvList.CurrentRow.Cells["colTaskCcList"].Value.ToString();
                //string taskMailErrorList = DgvList.CurrentRow.Cells["colTaskErrorList"].Value.ToString();
                //string taskRemark = DgvList.CurrentRow.Cells["colRemark"].Value.ToString();
                //bool taskUse = bool.Parse(DgvList.CurrentRow.Cells["colIsUse"].Value.ToString());
                //string taskType = DgvList.CurrentRow.Cells["colTaskType"].Value.ToString();
                //string serverName = DgvList.CurrentRow.Cells["colServerName"].Value.ToString();

                //赋值给修改页面
                txtTaskID_Edit.Text = taskID;
                txtTaskRunDll_Edit.Text = taskDll;
                txtTaskName_Edit.Text = taskName;
                chbIsUse.Checked = taskUse;
                txtRemark_Edit.Text = taskRemark;
                txtMailCcList.Text = taskMailCcList;
                txtMailToList.Text = taskMailToList;
                txtMailErrorList.Text = taskMailErrorList;
                cmbServerDb.SelectedValue = serverName;
                cmbTaskType.SelectedItem = taskType;


            }
            else if (tbContolTask.SelectedTab == tbPageTaskLog)
            {
                this.Tag = FrmMainStaus.浏览;
            }

            //控制工具条按钮权限
            ToolStripBtnControl((FrmMainStaus)this.Tag);






        }
        /// <summary>
        /// 删除任务按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {

            string taskId = this.DgvList.CurrentRow.Cells["colTaskID"].Value.ToString();
            //防止多次点击删除键
            if (frmDelMessage != null)
            {
                frmDelMessage.Show();
            }
            else
            {

                frmDelMessage = new FrmMessage($"是否要删除任务:{taskId}", taskId);
                frmDelMessage.TopMost = true;
                frmDelMessage.ResCallBack += FrmDelMessage_ResCallBack;
                frmDelMessage.Show();

            }


            //这里不能这么写，因为我想要的是不阻塞主线程的提示框，那么就不能用ShowDialog，要用到绑定事件回调参数，当提示框被关闭时，才执行下面的代码
            // DialogResult result = frmDelMessage.DialogResult;
            //if (result == DialogResult.OK)
            //{
            //    MdlMain mdl = new MdlMain() { TaskId = taskId };
            //    string resStr = DalMain.DeleteTask(mdl) ? "删除成功" : "删除失败";
            //    new FrmMessage(resStr).Show();
            //    //重新绑定Dgv数据源
            //    this.DgvList.DataSource = DalMain.GetList(mdlMain).Tables[0];
            //}
        }
        /// <summary>
        /// 绑定删除提示框的事件
        /// </summary>
        /// <param name="obj">回调参数，接收的是用户点击是或否</param>
        /// <exception cref="NotImplementedException"></exception>
        private void FrmDelMessage_ResCallBack(DialogResult dialogResult,string taskId)
        {

            if (dialogResult == DialogResult.OK)
            {
                MdlMain mdl = new MdlMain() { TaskId = taskId };
                string resStr = DalMain.DeleteTask(mdl) ? "删除成功" : "删除失败";
                new FrmMessage(resStr).Show();
                //重新绑定Dgv数据源
                this.DgvList.DataSource = DalMain.GetList(mdl).Tables[0];
            }

            frmDelMessage = null; //释放资源

        }
        /// <summary>
        /// 添加更多服务器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDb_Click(object sender, EventArgs e)
        {
            

            this.grpAddServer.Visible= this.grpAddServer.Visible? false : true;



        }
        /// <summary>
        /// 添加服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestAndAddDB_Click(object sender, EventArgs e)
        {


            string ip = txtDbIp.Text.ToString().Trim();
            string dbName = txtDbName.Text.ToString().Trim();
            string user = txtDbUser.Text.ToString().Trim();
            string password = txtDbPassword.Text.ToString().Trim();
            string serverName = ip + "_" + dbName;
            string conn;

            if (string.IsNullOrEmpty(ip))
            {
                txtDbIp.Focus();
                new FrmMessage("IP不能为空").Show();
                return ;
            }
            
            if (string.IsNullOrEmpty(dbName))
            {
                txtDbName.Focus();
                new FrmMessage("数据库名不能为空").Show();
                return ;
            }
            if (string.IsNullOrEmpty(user))
            {
                txtDbUser.Focus();
                new FrmMessage("账号不能为空").Show();
                return ;
            }
            if (string.IsNullOrEmpty(password))
            {
                txtDbPassword.Focus();
                new FrmMessage("密码不能为空").Show();
                return ;
            }
            //Linq 查询是否有重复的服务器
            List<DataRow> lstServerName =
                 (from i in ((DataTable)cmbServerDb.DataSource).AsEnumerable()
                  where i.Field<string>("ServerName") == serverName select i).ToList();

            if (lstServerName.Count>0) {

                cmbServerDb.SelectedValue = serverName;
                new FrmMessage("该服务器已存在").Show();
                return ;

            }
            //测试是否连接成功
            conn = $"Data Source={ip};Initial Catalog={dbName};User ID={user};Password={password};Connect Timeout=10";
            try
            {
                
                using (SqlConnection sqlConnection = new SqlConnection(conn))
                {
                    
                    sqlConnection.Open();
                }
            }
            catch (Exception ex)
            {
                new FrmMessage("测试连接失败："+ex.Message).Show();
                return ;
            }
            MdlMain mdlMain = new MdlMain() { DbName = dbName, TaskServerDb = serverName, IpAddress = ip, SecretKey = EncryptAndDecrypt.Encode(conn) };
            if (DalMain.InsertServerDb(mdlMain))
            {
                new FrmMessage("添加服务器成功").Show();
                
                //服务器下拉框数据源
                ComboxHelper.CmbDataBind(this.cmbServerDb, DalMain.InitControl(mdlMain).Tables[0], "ServerName", "ServerName","", false);

                //cmbServerDb.SelectedItem = serverName;

            }
            else
            {
                new FrmMessage("添加服务器失败").Show();
            }

            
        }
        /// <summary>
        /// 运行任务按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartTask_Click(object sender, EventArgs e)
        {


            string taskID = DgvList.CurrentRow.Cells["colTaskID"].Value.ToString();
            //需要重新查询数据库,因为防止后台有人更新,无法及时映射到编辑页面 
            DataTable tbTask = DalMain.GetList(mdlMain).Tables[0];
            DataRow[] dataRow = tbTask.Select($"TaskID = '{taskID}'");

            if (dataRow == null || dataRow.Length == 0)
            {
                new FrmMessage("任务不存在或已被删除").Show();
                //重新绑定
                this.DgvList.DataSource = tbTask;
                return;
            }



            string taskDllId = dataRow[0]["TaskRunDll"].ToString();
            string toList = dataRow[0]["ToList"].ToString();
            string ccList = dataRow[0]["CcList"].ToString();
            string errorList = dataRow[0]["ErrorList"].ToString();
            string secretKey = dataRow[0]["SecretKey"].ToString();



   


            Dictionary<string, List<CancellationTokenSource>> taskCancelDic = CreateTaskCancelToken(taskID, out CancellationToken cancelToken, out CancellationTokenSource tokenSource); //创建任务取消令牌

            Task.Run(() =>
            {
                LoadDll(taskID, dllPath + taskDllId, new object[] { taskID, toList, ccList, secretKey, cancelToken }, errorList, taskCancelDic, tokenSource, true);
            });

        }
        /// <summary>
        /// 结束任务按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEndTask_Click(object sender, EventArgs e)
        {
            ////通知订阅者任务结束

            //TaskEnding?.Invoke(taskId);


            string taskId = this.DgvList.CurrentRow.Cells["colTaskID"].Value.ToString();
            List <CancellationTokenSource> lstCancelTokens= FindTaskCanceToken(taskId);

            if (lstCancelTokens!=null)
            {
                lstCancelTokens.ForEach(token => { token.Cancel(); });
            }
            else
            {
                new FrmMessage(taskId+"没有在运行状态").Show();
            }





        }

        /// <summary>
        /// 任务日志查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            MdlMain mdl = new MdlMain();
            mdl.TaskId = this.txtTaskID_His.Text.ToString().Trim();
            mdl.TaskName= this.txtTaskName_His.Text.ToString().Trim();
            mdl.TaskType= this.cbxTaskType_His.SelectedItem.ToString();
            if (this.cbxTaskLogDate.Checked)
            {
                mdl.TaskLogBeginTime = this.DateRunTime_Begin.Value;
                mdl.TaskLogEndTime = this.DateRunTime_End.Value;

            } 

            this.dgvTaskLog.DataSource= DalMain.GetTaskLog(mdl).Tables[0];       //查询任务历史日志


        }

        /// <summary>
        /// 日期单选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxTaskLogDate_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbxTaskLogDate.Checked)
            {
                this.DateRunTime_Begin.Enabled = true;
                this.DateRunTime_End.Enabled = true;
            }
            else
            {
                this.DateRunTime_Begin.Enabled = false;
                this.DateRunTime_End.Enabled = false;
            }
            
        }
        /// <summary>
        /// 创建任务取消令牌
        /// </summary>
        private Dictionary<string, List<CancellationTokenSource>> CreateTaskCancelToken(string taskId, out CancellationToken token,out CancellationTokenSource source)
        {


            source = new CancellationTokenSource();
            token = source.Token;
         
            Dictionary<string, List<CancellationTokenSource>> taskCancelDictionary;

            //查找list是否存有相同的任务
            List<Dictionary<string, List<CancellationTokenSource>>> enumerableTaskDic =
                (from dic in cancelTaskList where dic.ContainsKey(taskId) select dic).ToList();

            //不存在相同任务id键则创建字典
            if (enumerableTaskDic.Count() <= 0)
            {
                Dictionary<string, List<CancellationTokenSource>> dicTaskCancelToken = new Dictionary<string, List<CancellationTokenSource>>();
                dicTaskCancelToken.Add(taskId, new List<CancellationTokenSource> { source });
                cancelTaskList.Add(dicTaskCancelToken);
                taskCancelDictionary = dicTaskCancelToken;
            }
            else//存在相同的键则添加令牌
            {
                taskCancelDictionary = (Dictionary<string, List<CancellationTokenSource>>)enumerableTaskDic[0];
                if (taskCancelDictionary.TryGetValue(taskId, out List<CancellationTokenSource> lstTokens)) //一定是存在的
                {
                    lstTokens.Add(source);
                   
                }

            }
            return taskCancelDictionary;

        }
        /// <summary>
        /// 任务取消令牌
        /// </summary>
        private void DeleteTaskCancelToken(string taskid,Dictionary<string, List<CancellationTokenSource>> taskCancelDictionary, CancellationTokenSource source)
        {
            if (taskCancelDictionary.TryGetValue(taskid,out List<CancellationTokenSource> lstTaskCancelToken))
            {
                lstTaskCancelToken.Remove(source); //移除当前任务的取消令牌
            }
        }
        /// <summary>
        /// 查找任务的所有取消令牌
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        private List<CancellationTokenSource> FindTaskCanceToken(string taskId)
        {
            List<CancellationTokenSource> result = null;

            cancelTaskList.ForEach(dic =>
            {
                if (dic.ContainsKey(taskId))
                {
                    dic.TryGetValue(taskId, out result);
                }
            });

            return result;
        }
        /// <summary>
        /// 任务工程id文本框修改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTaskID_Edit_TextChanged(object sender, EventArgs e)
        {
            txtTaskRunDll_Edit.Text = txtTaskID_Edit.Text.Trim() + ".dll"; //默认任务Dll名称为任务ID.dll
        }
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {



            //取消默认关闭
            e.Cancel = true;
            if (frmMainClosing == null)
            {
                frmMainClosing = new FrmMessage("是否需要关闭窗口?");
                frmMainClosing.ResCallBack += FrmMainClosing_ResCallBack;

            }

            frmMainClosing.Show();

            //if (frmMainClosing==null&&!frmMainClosing.Visible)
            //{

            //    frmMainClosing = new FrmMessage("是否需要关闭窗口?");
            //    frmMainClosing.ResCallBack += FrmMainClosing_ResCallBack;
            //}

            //frmMainClosing.Show();





        }
        /// <summary>
        /// 关闭窗口事件回调
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void FrmMainClosing_ResCallBack(DialogResult arg1, string arg2)
        {
            if (arg1==DialogResult.OK)
            {
                this.FormClosing -= FrmMain_FormClosing; //取消关闭事件，防止递归
                this.Close();
                
            }
            frmMainClosing = null;
        }



    


    }
}