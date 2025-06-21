using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class DateHelper
    {

        public static DateTime nowTime =>DateTime.Now;

        /// <summary>  
        /// 获取时间字符串 若传入时间为空则获取当前时间  
        /// </summary>  
        /// <returns></returns>  
        public static string getStrNowTime(DateTime? dateTime = null, string format = "yyyy-MM-dd HH:mm:ss")
        {
            if (dateTime == null)
            {
                dateTime = nowTime;
            }

            try
            {
                return dateTime.Value.ToString(format); // Use 'Value' to access the DateTime object from the nullable type  
            }
            catch (Exception)
            {
                return dateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");   
            }
        }
    }
}
