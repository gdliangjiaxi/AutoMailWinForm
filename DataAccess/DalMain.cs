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
using System.Configuration;
using System.Runtime.CompilerServices;


namespace DataAccess
{
    /// <summary>
    /// 数据处理层
    /// </summary>
    public class DalMain
    {
        private static string  connectionString =  EncryptAndDecrypt.Decode(ConfigurationManager.AppSettings["conn"].ToString());

        // 存储过程名称
        private const string SP_FrmMain = "SP_FrmMain";

        /// <summary>
        /// 存储过程内容处理方法各类型名称
        /// </summary>
        public enum SpType
        {
            GetList,
            GetRunList,
            WriteLog,
            MarkRunTask,
            DeleteRunTask,
            InitControl,
            InsertTask,
            UpdateTask,
            DeleteTask,
            InsertServerDb,
            GetTaskLog
        }
        /// <summary>
        /// 取任务信息
        /// </summary>
        /// <param name="p_Model">实体类</param>
        /// <returns></returns>
        public static DataSet GetList(MdlMain p_Model)
        {
          
            // 设置SP参前台
            Object[] objParams = SetParams(p_Model, SpType.GetList.ToString());
            DataSet ds = SqlHelper.ExecuteDataset(connectionString, SP_FrmMain, objParams);
            return ds;
        }
        /// <summary>
        /// 取任务日志信息
        /// </summary>
        /// <param name="p_Model">实体类</param>
        /// <returns></returns>
        public static DataSet GetTaskLog(MdlMain p_Model)
        {
            
            // 设置SP参前台
            Object[] objParams = SetParams(p_Model, SpType.GetTaskLog.ToString());
            DataSet ds = SqlHelper.ExecuteDataset(connectionString, SP_FrmMain, objParams);
            return ds;
        }
        /// <summary>
        /// 初始化控件数据源
        /// </summary>
        /// <param name="p_Model">实体类</param>
        /// <returns></returns>
        public static DataSet InitControl(MdlMain p_Model)
        {
         
            // 设置SP参前台
            Object[] objParams = SetParams(p_Model, SpType.InitControl.ToString());
            DataSet ds = SqlHelper.ExecuteDataset(connectionString, SP_FrmMain, objParams);
            return ds;
        }
        /// <summary>
        /// 取要运行的任务信息
        /// </summary>
        /// <param name="p_Model">实体类</param>
        /// <returns></returns>
        public static DataSet GetRunList(MdlMain p_Model)
        {
       
            // 设置SP参前台
            Object[] objParams = SetParams(p_Model, SpType.GetRunList.ToString());
            DataSet ds = SqlHelper.ExecuteDataset(connectionString, SP_FrmMain, objParams);
            return ds;
        }
        /// <summary>
        /// 新增任务
        /// </summary>
        /// <param name="p_Model">实体类</param>
        /// <returns></returns>
        public static bool InsertTask(MdlMain p_Model)
        {
            int ReturnValue;
            // 设置SP参前台
            Object[] objParams = SetParams(p_Model, SpType.InsertTask.ToString());
            ReturnValue = SqlHelper.ExecuteNonQuery(connectionString, SP_FrmMain, objParams);
            return  (int)objParams[0] ==0;


        }
        /// <summary>
        /// 修改任务
        /// </summary>
        /// <param name="p_Model">实体类</param>
        /// <returns></returns>
        public static bool UpdateTask(MdlMain p_Model)
        {
            int ReturnValue;
            // 设置SP参前台
            Object[] objParams = SetParams(p_Model, SpType.UpdateTask.ToString());
            ReturnValue = SqlHelper.ExecuteNonQuery(connectionString, SP_FrmMain, objParams);
            return (int)objParams[0] == 0;
        }
        

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="p_Model">实体类</param>
        /// <returns></returns>
        public static DataSet WriteLog(MdlMain p_Model)
        {
           
            // 设置SP参前台
            Object[] objParams = SetParams(p_Model, SpType.WriteLog.ToString());
            DataSet ds = SqlHelper.ExecuteDataset(connectionString, SP_FrmMain, objParams);
            return ds;
        }

        /// <summary>
        /// 标记正在执行的任务
        /// </summary>
        /// <param name="p_Model">实体类</param>
        /// <returns></returns>
        public static DataSet MarkRunTask(MdlMain p_Model)
        {
      
            // 设置SP参前台
            Object[] objParams = SetParams(p_Model, SpType.MarkRunTask.ToString());
            DataSet ds = SqlHelper.ExecuteDataset(connectionString, SP_FrmMain, objParams);
            return ds;
        }


        /// <summary>
        /// 删除执行完的任务
        /// </summary>
        /// <param name="p_Model">实体类</param>
        /// <returns></returns>
        public static DataSet DeleteRunTask(MdlMain p_Model)
        {
           
            // 设置SP参前台
            Object[] objParams = SetParams(p_Model, SpType.DeleteRunTask.ToString());
            DataSet ds = SqlHelper.ExecuteDataset(connectionString, SP_FrmMain, objParams);
            return ds;
        }
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="p_Model">实体类</param>
        /// <returns></returns>
        public static bool DeleteTask(MdlMain p_Model)
        {
            int ReturnValue;
            // 设置SP参前台
            Object[] objParams = SetParams(p_Model, SpType.DeleteTask.ToString());
            ReturnValue = SqlHelper.ExecuteNonQuery(connectionString, SP_FrmMain, objParams);
            return (int)objParams[0] == 0; ;
        }
        /// <summary>
        /// 新增服务器
        /// </summary>
        /// <param name="p_Model">实体类</param>
        /// <returns></returns>
        public static bool InsertServerDb(MdlMain p_Model)
        {
            int ReturnValue;
            // 设置SP参前台
            Object[] objParams = SetParams(p_Model, SpType.InsertServerDb.ToString());
            ReturnValue = SqlHelper.ExecuteNonQuery(connectionString, SP_FrmMain, objParams);
            return (int)objParams[0] == 0;


        }
        /// <summary>
        /// 设置SP_User参数
        /// </summary>
        /// <param name="p_Mode">用户表实体类</param>
        /// <param name="p_TypeId">SP操作类型</param>
        /// <returns></returns>
        public static object[] SetParams(MdlMain p_Mode, string p_TypeId)
        {
            // SP对应的参数，顺序需一致，如SP增加参数，此方法也需增加,各操作所需参数不同，对应的实体类(Model)各参数需初始化值
            Object[] objParams = {
                0,                                                          // 传出参数：返回信息总数
                p_TypeId,                                                    // SP操作区分类型
                p_Mode.TaskId,
                p_Mode.LogTime,
                p_Mode.LogLevel,
                p_Mode.Logstr,
                p_Mode.TaskName,
                p_Mode.TaskDll,
                p_Mode.TaskType,
                p_Mode.TaskUse,
                p_Mode.TaskRemark,
                p_Mode.TaskMailToList,
                p_Mode.TaskMailCcList,
                p_Mode.TaskMailErrorList,
                p_Mode.TaskServerDb,
                p_Mode.DbName,
                p_Mode.IpAddress,
                p_Mode.SecretKey,
                p_Mode.TaskLogBeginTime,
                p_Mode.TaskLogEndTime



    };

            return objParams;
        }
    }
}
