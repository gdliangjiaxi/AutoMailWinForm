using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Common
{
    public partial class FrmMessage : Form
    {

        public event Action<DialogResult,string> ResCallBack; //string是事件参数 DiaglogResult是返回值

        private string returnStr;

        public FrmMessage(string content)
        {

            InitializeComponent();
            this.lblContent.Text = content;
            this.TopMost = true;
        }
        /// <summary>
        /// 重载构造函数
        /// </summary>
        /// <param name="content"></param>
        /// <param name="returnStr"></param>
        public FrmMessage(string content,string returnStr) : this(content)
        {

            this.returnStr = returnStr;
        }
        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            ResCallBack?.Invoke(this.DialogResult,returnStr);
            this.Close();
        }
        /// <summary>
        /// 确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            ResCallBack?.Invoke(this.DialogResult, returnStr);
            this.Close();
        }
       
    }
}
