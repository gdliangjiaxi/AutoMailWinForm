using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DataAccess;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Common;


namespace DataAccess
{
    /// <summary>
    /// 数据处理层
    /// </summary>
    public class DalFrmMain
    {
        private const string  connectionString = "";

        // 存储过程名称
        private const string SP_FrmMain = "SP_FrmMain";

        /// <summary>
        /// 存储过程内容处理方法各类型名称
        /// </summary>
        public enum SpType
        {
            GetDetail
        }
        /// <summary>
        /// 取任务信息
        /// </summary>
        /// <param name="p_Model">实体类</param>
        /// <returns></returns>
        public static DataSet GetDetail(MdlFrmMain p_Model)
        {
            int ReturnValue;
            // 设置SP参前台
            Object[] objParams = SetParams(p_Model, SpType.GetDetail.ToString());
            DataSet ds = SqlHelper.ExecuteDataset("", SP_FrmMain, objParams);
            return ds;
        }



        /// <summary>
        /// 设置SP_User参数
        /// </summary>
        /// <param name="p_Mode">用户表实体类</param>
        /// <param name="p_TypeId">SP操作类型</param>
        /// <returns></returns>
        public static object[] SetParams(MdlFrmMain p_Mode, string p_TypeId)
        {
            // SP对应的参数，顺序需一致，如SP增加参数，此方法也需增加,各操作所需参数不同，对应的实体类(Model)各参数需初始化值
            Object[] objParams = {
                0,                                                          // 传出参数：返回信息总数
                p_TypeId                                                    // SP操作区分类型

                };

            return objParams;
        }
    }
}
