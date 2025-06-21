using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DataAccess
{
    public class DalAutoMail001
    {
        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <param name="Conn_string">数据库连接串</param>
        /// <returns></returns>
        public DataSet GetReportData(string Conn_string)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            
            ds = SqlHelper.ExecuteDataset(EncryptAndDecrypt.Decode(Conn_string), CommandType.Text, string.Format("exec SP_AutoMail001 "));
        
            return ds;
        }






    }
}
