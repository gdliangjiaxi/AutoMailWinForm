using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Common;
using Model;

namespace AutoMail002
{

    /// <summary>  
    /// 程序进入方法体  
    /// 条件:  
    /// 1.实现接口主方法,  
    /// 2.命名此类为MainClass,  
    /// 3.编译此类的时候要把AutoMail001.dll的放在启动目录dll文件夹下  
    /// </summary>  
    public class MainClass : MdlBase,IMainMethod
    {
       

        public string MainMethod(string taskId, string toList, string ccList, string SecretKey, CancellationToken token)
        {

            
            string strFileName= AppDomain.CurrentDomain.BaseDirectory + "Template\\个人简历.pdf";
            

            #region 1.检查模版是否存在
            if (!File.Exists(strFileName))
            {
                //etc. 模板文件不存在
                return "The tempate is not exist：" + strFileName;
            }
            #endregion

            #region 2.发送邮件
            //发送邮件前需要检查任务是否被取消了
            string subject = "个人简历";
            //构建邮件html正文
            string strBody = "<span style='font-family: Calibri;font-size: 11pt;color: black;'>Dear All:</span><br/><br/><span style='font-family: Calibri; font-size: 11pt;'> " +
                "Enclosed please find the " + subject + " for your kindly reference, thanks!<br/><br/><p><div><img src='cid:AutoMail002_1' /><img src='cid:AutoMail002_2' /><div/><p/>";

            //构建邮件正文图片
            string autoMail002_1 = AppDomain.CurrentDomain.BaseDirectory + "Template\\AutoMail002_1.PNG";
            string autoMail002_2 = AppDomain.CurrentDomain.BaseDirectory + "Template\\AutoMail002_2.PNG";
            string imageType = "image/PNG";
            Dictionary<string, string> images = new Dictionary<string, string>
            {
                {"AutoMail002_1", autoMail002_1},
                {"AutoMail002_2", autoMail002_2}
            };

            string res =base.CheckTaskCancelToken(token);
            if (string.IsNullOrEmpty(res))
            {
                return new MailHelper().ToSendEmail(toList, ccList, subject, strBody, strFileName,images,imageType);   
            }
            else
            {
                return res;
            }
            #endregion


        }
    }
}
