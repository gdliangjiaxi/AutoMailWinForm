using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using Common;

namespace Model
{
    public abstract class MdlBase
    {

        
        /// <summary>
        /// 公共发送邮件事件,协同取消token最后一步,取消掉任务只要不发出邮件就行了
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual string CheckTaskCancelToken(CancellationToken cancellationToken)
        {
            try
            {
                
                //检查是否手动取消任务了，没有取消就发送邮件
                cancellationToken.ThrowIfCancellationRequested();
            

            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return "手动结束任务";
                }
                //如果是其他异常，返回异常信息
                return ex.Message;
            }
            return "";
        }

       



    }
}
