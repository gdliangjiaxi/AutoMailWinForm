using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net;

namespace Model
{
    /// <summary>
    /// 系统共通实体参数，每个画面的实体类需继承此类
    /// </summary>
    [Serializable]
    public class MdlFrmMain
    {
        public string _SysName = string.Empty;   // 系统名称
        public string _Version = string.Empty;   // 版本号


        public string _SqlConn = string.Empty;   // 数据库链接

        public string _Factory = string.Empty;    //工厂类型

        public string _UserID = string.Empty; // 用户ID (默认为登录用户ID)
        public string _EmpNo = string.Empty;     // 工号
        public string _EmpName = string.Empty;   // 用户名

        public string _DeptID = string.Empty;    // 部门
        public string _LineID = string.Empty;     // 组别
        public string _EXT = string.Empty;       // 分机号

        public string _Email = string.Empty;     // 邮箱
        public string _UserPwd = string.Empty;   // 密码
        public string _Language = GetConfigKeyValue("Language");          // 默认语言种类:中文

        public bool _IsLock = false;            // 是否锁定
        public string _Technic = string.Empty;   // 技术支持

        public string _FormID = string.Empty;    // 画面编号
        public string _IP = string.Empty;        // 用户IP地址
        public DataTable _dtComm;                // 系统共通参数表信息
        public string _Sort = string.Empty;      // 排序字段与排序类型


        public string _ParaType = string.Empty;    // 参数类型值


        public int _PageRows = 20;               // 每页显示的记录数
        public int _PageSize = 1;                // 当前页码

        public string _AboutUs = string.Empty;    // 关于我们

        public string _Org_type = string.Empty;    // 工厂：D/部门:S/组别:B 区分 

        public string _HostName = Dns.GetHostName();   //电脑名称(默认为登录用户电脑名)
        /// <summary>
        /// 系统默认邮件内容框架
        /// </summary>
        private string _MailTemplate = AppDomain.CurrentDomain.BaseDirectory + "HtmlTemplate\\MailTemplate.htm";
        /// <summary>
        /// 系统默认邮件内容框架
        /// </summary>
        private string _MailTemplate_en = AppDomain.CurrentDomain.BaseDirectory + "HtmlTemplate\\MailTemplate_en.htm";


        /// <summary>
        /// 系统默认邮件内容框架
        /// </summary>
        public string MailTemplate
        {
            get
            {
                return _MailTemplate;
            }
            set
            {
                _MailTemplate = value;
            }
        }

        /// <summary>
        /// 系统默认邮件内容框架
        /// </summary>
        public string MailTemplate_en
        {
            get
            {
                return _MailTemplate_en;
            }
            set
            {
                _MailTemplate_en = value;
            }
        }

        /// <summary>
        /// 电脑名称 
        /// </summary>
        public string HostName
        {
            set { _HostName = value; }
            get { return _HostName; }
        }


        /// <summary>
        /// 工厂：D/部门:S/组别:L 区分 
        /// </summary>
        public string Org_type
        {
            set { _Org_type = value; }
            get { return _Org_type; }
        }


        /// <summary>
        /// 关于我们
        /// </summary>
        public string AboutUs
        {
            set { _AboutUs = value; }
            get { return _AboutUs; }
        }

        /// <summary>
        /// 工厂类型
        /// </summary>
        public string Factory
        {
            set { _Factory = value; }
            get { return _Factory; }
        }

        /// <summary>
        /// 组别
        /// </summary>
        public string LineID
        {
            set { _LineID = value; }
            get { return _LineID; }
        }


        /// <summary>
        /// 参数类型值

        /// </summary>
        public string ParaType
        {
            set { _ParaType = value; }
            get { return _ParaType; }
        }


        /// <summary>
        /// 系统名称
        /// </summary>
        public string SysName
        {
            set { _SysName = value; }
            get { return _SysName; }
        }

        /// <summary>
        /// 系统版本号

        /// </summary>
        public string Version
        {
            set { _Version = value; }
            get { return _Version; }
        }

        /// <summary>
        /// 数据连接
        /// </summary>
        public string SqlConn
        {
            set { _SqlConn = value; }
            get { return _SqlConn; }
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID
        {
            set { _UserID = value; }
            get { return _UserID; }
        }

        /// <summary>
        /// 工号
        /// </summary>
        public string EmpNo
        {
            set { _EmpNo = value; }
            get { return _EmpNo; }
        }

        /// <summary>
        /// 分机
        /// </summary>
        public string EXT
        {
            set { _EXT = value; }
            get { return _EXT; }
        }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email
        {
            set { _Email = value; }
            get { return _Email; }
        }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string EmpName
        {
            set { _EmpName = value; }
            get { return _EmpName; }
        }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPwd
        {
            set { _UserPwd = value; }
            get { return _UserPwd; }
        }

        /// <summary>
        /// 部门
        /// </summary>
        public string DeptID
        {
            set { _DeptID = value; }
            get { return _DeptID; }
        }


        /// <summary>
        /// 用户是否锁定
        /// </summary>
        public bool IsLock
        {
            set { _IsLock = value; }
            get { return _IsLock; }
        }

        /// <summary>
        /// 系统技术支持

        /// </summary>
        public string Technic
        {
            set { _Technic = value; }
            get { return _Technic; }
        }

        /// <summary>
        /// 语言
        /// </summary>
        public string Language
        {
            set { _Language = value; }
            get { return _Language; }
        }

        /// <summary>
        /// 共通参数数据

        /// </summary>
        public DataTable dtComm
        {
            set { _dtComm = value; }
            get { return _dtComm; }
        }

        /// <summary>
        /// 页面编号
        /// </summary>
        /// <remarks></remarks>
        public string FormID
        {
            get { return _FormID; }
            set { _FormID = value; }
        }

        /// <summary>
        /// 当前排序字段与排序类型

        /// </summary>
        /// <remarks></remarks>
        public string Sort
        {
            get { return _Sort; }
            set { _Sort = value; }
        }

        /// <summary>
        /// 每页显示行数
        /// </summary>
        /// <remarks></remarks>
        public int PageRows
        {
            get { return _PageRows; }
            set { _PageRows = value; }
        }

        /// <summary>
        /// 当前显示页码
        /// </summary>
        /// <remarks></remarks>
        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }

        public static string GetConfigKeyValue(string pKey)
        {
            System.Configuration.AppSettingsReader appReader = new System.Configuration.AppSettingsReader();
            string valueM = Convert.ToString(appReader.GetValue(pKey, typeof(string)));
            return valueM;
        }
    }
}
