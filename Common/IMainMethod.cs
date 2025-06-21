using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{

    /// <summary>
    /// 公共类接口,所有邮件类都要实现这个接口
    /// </summary>
    public  interface   IMainMethod
    {
        //a.TaskID,c.ToList,c.CcList,c.ErrorList,d.SecretKey


          string  MainMethod(string taskId, string toList, string ccList, string SecretKey,CancellationToken token);

  
    }

    
}
