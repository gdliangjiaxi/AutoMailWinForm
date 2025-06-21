using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common
{
    public class ComboxHelper
    {

        /// <summary>
        /// 下拉框绑定datatable数据源
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="displayStr"></param>
        /// <param name="valueStr"></param>
        /// <param name="defaultStr"></param>
        /// <param name="nullItem"></param>
        public static void CmbDataBind(ComboBox cbx,DataTable dt,string displayStr,string valueStr,string defaultStr,bool emptyStrItem)
        {
            //对象引用都是地址引用,需要拷贝一份后面修改才不会影响到原数据
            DataTable dataSource= dt.Copy();

            //显示空字符
            if (emptyStrItem)
            {
                DataRow dr= dataSource.NewRow();
                dr[displayStr] = "";
                dr[valueStr] = "";
                dataSource.Rows.Add(dr);

            }

            cbx.DataSource = dataSource;
            cbx.DisplayMember = displayStr;
            cbx.ValueMember = valueStr;

            if (!string.IsNullOrEmpty(defaultStr))
            {
                cbx.SelectedValue = defaultStr;
            }



        }



    }
}
