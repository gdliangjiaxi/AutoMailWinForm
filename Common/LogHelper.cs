using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Common
{   
    
    /// 系统共用常量
    public static class LogHelper {

        //日志常量定义
        public const int INFO= 0;

        public const int WARNING = 1;

        public const int ERROR = 2;

        public const string SEND_BEGIN = "自动发送开始";

        public const string SEND_SUCCESS = "自动发送成功";

        public const string SEND_CANCEL = "自动发送取消";

        public const string SEND_EXCEPTION = "自动发送异常";


        public const string SEND_FAIL = "自动发送失败";

        public const string SEND_END = "自动发送结束";



        public const string SEND_BEGIN_MANUALLY = "手动发送开始";

        public const string SEND_SUCCESS_MANUALLY = "手动发送成功";

        public const string SEND_CANCEL_MANUALLY = "手动发送取消";

        public const string SEND_EXCEPTION_MANUALLY = "手动发送异常";


        public const string SEND_FAIL_MANUALLY = "手动发送失败";

        public const string SEND_END_MANUALLY = "手动发送结束";

        //全局日志委托
        public static Action<string, int, string, string> LogAction;











    }














}
