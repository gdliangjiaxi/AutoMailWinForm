using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Diagnostics;



namespace Common
{
    public class MailHelper
    {
        /// <summary>
        /// 生成图片参数
        /// </summary>
        struct BuildImg
        {
            /// <summary>
            /// 内容来自哪个Excel文件 Pic. from file
            /// </summary>
            public string xlsFilePath;
            /// <summary>
            /// 开始行 Copy begin row
            /// </summary>
            public int BeginRow;
            /// <summary>
            /// 开始列 Copy begin column
            /// </summary>
            public int BeginCol;
            /// <summary>
            /// 结束行 Copy end row
            /// </summary>
            public int EndRow;
            /// <summary>
            /// 结束列 Copy end column
            /// </summary>
            public int EndCol;
            /// <summary>
            /// 图片内容来自Sheet Pic. from sheet
            /// </summary>
            public string FromSheet;
            /// <summary>
            /// 保存图片路径 Save pic. path
            /// </summary>
            public string JpgTargetPath;
            /// <summary>
            /// 保存用的文件名 Pic. name 
            /// </summary>
            public string JpgTargetFileName;
            /// <summary>
            /// 截图第Index张图片（从1开始） Save which pic. 
            /// </summary>
            public int Index;
        }

        MailMessage mailM = new MailMessage();

        public MailHelper()
        {
            //发送人
            mailM.From = new MailAddress(GetConfigKeyValue("FormMail"));
            mailM.IsBodyHtml = true;
        }

        public MailHelper(string fromStr)
        {
            //发送人
            mailM.From = new MailAddress(fromStr);
            mailM.IsBodyHtml = true;
        }

        private SmtpClient ConfiguratSMTP()
        {
         
            switch (GetConfigKeyValue("SmtpClass"))
            {
                
                case "QQ":
                    SmtpClient smtpQQ = new SmtpClient(GetConfigKeyValue("SmtpHost"), int.Parse(GetConfigKeyValue("SmtpPort")))
                    {
                        Credentials = new NetworkCredential(GetConfigKeyValue("FormMail"), GetConfigKeyValue("AuthorizationCode")),
                        EnableSsl = true, //QQ,163邮箱要求使用SSL
                    };
                    return smtpQQ;
                case "163":
                    SmtpClient smtp163 = new SmtpClient(GetConfigKeyValue("SmtpHost"), int.Parse(GetConfigKeyValue("SmtpPort")))
                    {
                        Credentials = new NetworkCredential(GetConfigKeyValue("FormMail"), GetConfigKeyValue("AuthorizationCode")),
                        EnableSsl = false, //163邮箱不要求使用SSL
                    };
                    return smtp163;
                default:
                    //SMTP服务
                    SmtpClient smtp2 = new SmtpClient();
                    smtp2.Host = GetConfigKeyValue("EmailHost");
                    return smtp2; 


            }
         

        }


        /// <summary>
        /// 发送邮件(正文不带图片)
        /// Send email(the body have not pic)
        /// </summary>
        /// <param name="arrStrTO">收件人地址</param>
        /// <param name="arrStrCC">CC地址</param>
        /// <param name="strSubject">邮件标题</param>
        /// <param name="strBody">邮件内容</param>
        /// <param name="strAttPath">附件文件地址</param>
        /// <returns>发送是否成功</returns>
        public string ToSendEmail(string arrStrTO, string arrStrCC, string strSubject, string strBody, string arrAttPath)
        {
            try
            {
                //收件人地址
                if (arrStrTO != "" && arrStrTO.Length > 0)
                {
                    //拆分多个收件人
                    string[] StrEmailTO = arrStrTO.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string strTO in StrEmailTO)
                    {
                        try
                        {
                            if (strTO.Trim() != "")
                            {
                                mailM.To.Add(strTO);
                            }
                        }
                        catch { }
                    }
                }

                //CC人地址
                if (arrStrCC != null && arrStrCC.Length > 0)
                {
                    //拆分多个CC人
                    string[] StrEmailCC = arrStrCC.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strCC in StrEmailCC)
                    {
                        if (strCC.Trim() != "")
                        {
                            try
                            {
                                mailM.CC.Add(strCC);
                            }
                            catch { }
                        }
                    }
                }

                //邮件标题
                mailM.Subject = strSubject;

                //如果内容为HTML网页可用下方法
                //StreamReader sr=new StreamReader(@"c:\\zhouqiang.htm",Encoding.GetEncoding("gb2312"));
                //mailM.Body = sr.ReadToEnd();

                //添加附件
                if (arrAttPath != null && arrAttPath.Length > 0)
                {
                    //拆分多个附件
                    string[] StrEmailAtt = arrAttPath.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strAtt in StrEmailAtt)
                    {
                        if (!string.IsNullOrEmpty(strAtt))
                        {
                            mailM.BodyEncoding = Encoding.GetEncoding("utf-8");
                            Attachment att = new Attachment(@strAtt, MediaTypeNames.Application.Octet);
                            mailM.Attachments.Add(att);
                        }
                    }
                }

                //邮件内容
                mailM.Body = strBody;


                // 设置SMTP客户端
                SmtpClient smtp = ConfiguratSMTP();
                //发送
                smtp.Send(mailM);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        /// <summary>
        /// 发送邮件(正文带图片，图片为准备好的附件图片)
        /// Send email(the body have not pic)
        /// </summary>
        /// <param name="arrStrTO">收件人地址</param>
        /// <param name="arrStrCC">CC地址</param>
        /// <param name="strSubject">邮件标题</param>
        /// <param name="strBody">邮件内容</param>
        /// <param name="strAttPath">附件文件地址</param>
        /// <returns>发送是否成功</returns>
        public string ToSendEmail(string arrStrTO, string arrStrCC, string strSubject, string strBody, string arrAttPath, Dictionary<string, string> imagePaths,string imageType)
        {
            try
            {
                //收件人地址
                if (arrStrTO != "" && arrStrTO.Length > 0)
                {
                    //拆分多个收件人
                    string[] StrEmailTO = arrStrTO.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string strTO in StrEmailTO)
                    {
                        try
                        {
                            if (strTO.Trim() != "")
                            {
                                mailM.To.Add(strTO);
                            }
                        }
                        catch { }
                    }
                }

                //CC人地址
                if (arrStrCC != null && arrStrCC.Length > 0)
                {
                    //拆分多个CC人
                    string[] StrEmailCC = arrStrCC.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strCC in StrEmailCC)
                    {
                        if (strCC.Trim() != "")
                        {
                            try
                            {
                                mailM.CC.Add(strCC);
                            }
                            catch { }
                        }
                    }
                }

                //邮件标题
                mailM.Subject = strSubject;

              

                //添加附件
                if (arrAttPath != null && arrAttPath.Length > 0)
                {
                    //拆分多个附件
                    string[] StrEmailAtt = arrAttPath.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strAtt in StrEmailAtt)
                    {
                        if (!string.IsNullOrEmpty(strAtt))
                        {
                            mailM.BodyEncoding = Encoding.GetEncoding("utf-8");
                            Attachment att = new Attachment(@strAtt, MediaTypeNames.Application.Octet);
                            mailM.Attachments.Add(att);
                        }
                    }
                }

                // 创建AlternateView用于HTML内容
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(strBody, null, MediaTypeNames.Text.Html);

                foreach (var imagePath in imagePaths)
                {
                    LinkedResource inline = new LinkedResource(imagePath.Value)
                    {
                        ContentId = imagePath.Key // 使用提供的键作为Content-ID
                    };

                    // 设置MediaType根据你的图片类型调整此行
                    inline.ContentType.MediaType = imageType; // 或者"image/png", 根据实际情况调整

                    // 将LinkedResource添加到AlternateView
                    htmlView.LinkedResources.Add(inline);
                }

                // 将AlternateView添加到MailMessage
                mailM.AlternateViews.Add(htmlView);






                // 设置SMTP客户端
                SmtpClient smtp = ConfiguratSMTP();
                //发送
                smtp.Send(mailM);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        /// <summary> 
        /// 发送邮件(正文只带一个表格图片)  
        /// Send email(the body only have one table pic)
        /// </summary>
        /// <param name="arrStrTO">收件人地址</param>
        /// <param name="arrStrCC">CC地址</param>
        /// <param name="strSubject">邮件标题</param>
        /// <param name="strBody">邮件内容</param>
        /// <param name="arrAttPath">附件文件地址</param>
        /// <param name="strAttPath">截图文件地址</param>
        /// <param name="strSheetName">截图Sheet名称</param>
        /// <param name="iRowColumn">截图坐标{BeginRow, BeginCol, EndRow, EndCol}</param>
        /// <returns>发送是否成功</returns>
        public string ToSendEmail(string arrStrTO, string arrStrCC, string strSubject, string strBody, string arrAttPath,
            string strAttPath, string strSheetName, int[] iRowColumn)
        {
            try
            {
                //收件人地址
                if (arrStrTO != "" && arrStrTO.Length > 0)
                {
                    //拆分多个收件人
                    string[] StrEmailMTO = arrStrTO.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string strTO in StrEmailMTO)
                    {
                        try
                        {
                            if (strTO.Trim() != "")
                            {
                                mailM.To.Add(strTO);
                            }
                        }
                        catch { }
                    }
                }

                //CC人地址
                if (arrStrCC != null && arrStrCC.Length > 0)
                {
                    //拆分多个CC人
                    string[] StrEmailMCC = arrStrCC.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strCC in StrEmailMCC)
                    {
                        try
                        {
                            if (strCC.Trim() != "")
                            {
                                mailM.CC.Add(strCC);
                            }
                        }
                        catch { }
                    }
                }

                //邮件标题
                mailM.Subject = strSubject;

                //添加附件
                if (arrAttPath != null && arrAttPath.Length > 0)
                {
                    //拆分多个附件
                    string[] StrEmailMAtt = arrAttPath.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strAtt in StrEmailMAtt)
                    {
                        //添加附件
                        mailM.BodyEncoding = Encoding.GetEncoding("utf-8");
                        Attachment att = new Attachment(@strAtt);
                        mailM.Attachments.Add(att);
                    }
                }

                //邮件正文图片
                string imgPath = "";
                if (!string.IsNullOrEmpty(strAttPath))
                {
                    ParameterizedThreadStart _paraThread = new ParameterizedThreadStart(MakeJpg);
                    Thread _thread = new Thread(_paraThread);
                    _thread.IsBackground = true;
                    BuildImg _buildimg = new BuildImg();
                    _buildimg.xlsFilePath = strAttPath;
                    _buildimg.FromSheet = strSheetName;
                    _buildimg.BeginRow = iRowColumn[0];
                    _buildimg.BeginCol = iRowColumn[1];
                    _buildimg.EndRow = iRowColumn[2];
                    _buildimg.EndCol = iRowColumn[3];
                    _buildimg.JpgTargetFileName = "IMGA_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
                    _buildimg.JpgTargetPath = @Directory.GetCurrentDirectory() + "\\img\\";
                    imgPath = _buildimg.JpgTargetPath + _buildimg.JpgTargetFileName + ".jpg";
                    _thread.SetApartmentState(ApartmentState.STA);
                    _thread.Start(_buildimg);

                    //while (_thread.ThreadState == ThreadState.Running) { }
                    while (_thread.IsAlive == true) { }

                    //添加1张图片
                    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(@imgPath);
                    mailM.Attachments.Add(attachment);

                    //邮件内容替换为图片
                    strBody = strBody.Replace("[picture1]", "<img src=\"cid:" + attachment.ContentId + "\" />");
                }

                //邮件内容
                mailM.Body = strBody;

                // 设置SMTP客户端
                SmtpClient smtp = ConfiguratSMTP();

                //发送
                smtp.Send(mailM);
                return "";
            }
            catch (Exception ex)
            {
                return  ex.Message.ToString();
            }
        }

        /// <summary> 
        /// 发送邮件(正文只带一个表格图片)  
        /// Send email(the body only have one table pic)
        /// </summary>
        /// <param name="arrStrTO">收件人地址</param>
        /// <param name="arrStrCC">CC地址</param>
        /// <param name="strSubject">邮件标题</param>
        /// <param name="strBody">邮件内容</param>
        /// <param name="arrAttPath">附件文件地址</param>
        /// <param name="strAttPath">截图文件地址</param>
        /// <param name="strSheetName">截图Sheet名称</param>
        /// <param name="iRowColumn">截图坐标{BeginRow, BeginCol, EndRow, EndCol}</param>
        /// <param name="Cycles">循环次数</param>
        /// <returns>发送是否成功</returns>
        public string ToSendEmail(string arrStrTO, string arrStrCC, string strSubject, string strBody, string arrAttPath,
            string strAttPath, string strSheetName, int[] iRowColumn, int Cycles, string strMail)
        {
            try
            {
                //收件人地址
                if (arrStrTO != "" && arrStrTO.Length > 0)
                {
                    //拆分多个收件人
                    string[] StrEmailMTO = arrStrTO.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string strTO in StrEmailMTO)
                    {
                        try
                        {
                            if (strTO.Trim() != "")
                            {
                                mailM.To.Add(strTO);
                            }
                        }
                        catch { }
                    }
                }

                //CC人地址
                if (arrStrCC != null && arrStrCC.Length > 0)
                {
                    //拆分多个CC人
                    string[] StrEmailMCC = arrStrCC.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strCC in StrEmailMCC)
                    {
                        try
                        {
                            if (strCC.Trim() != "")
                            {
                                mailM.CC.Add(strCC);
                            }
                        }
                        catch { }
                    }
                }

                //邮件标题
                mailM.Subject = strSubject;

                //添加附件
                if (arrAttPath != null && arrAttPath.Length > 0)
                {
                    //拆分多个附件
                    string[] StrEmailMAtt = arrAttPath.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strAtt in StrEmailMAtt)
                    {
                        //添加附件
                        mailM.BodyEncoding = Encoding.GetEncoding("utf-8");
                        Attachment att = new Attachment(@strAtt);
                        mailM.Attachments.Add(att);
                    }
                }

                //邮件正文图片
                string imgPath = "";
                if (!string.IsNullOrEmpty(strAttPath))
                {

                    for (int i = 0; i < Cycles; i++)
                    {
                        ParameterizedThreadStart _paraThread = new ParameterizedThreadStart(MakeJpgII);
                        Thread _thread = new Thread(_paraThread);
                        _thread.IsBackground = true;
                        BuildImg _buildimg = new BuildImg();
                        _buildimg.xlsFilePath = strAttPath;
                        _buildimg.FromSheet = strSheetName;
                        _buildimg.BeginRow = iRowColumn[0];
                        _buildimg.BeginCol = iRowColumn[1];
                        _buildimg.EndRow = iRowColumn[2];
                        _buildimg.EndCol = iRowColumn[3];
                        _buildimg.Index = i + 1;
                        _buildimg.JpgTargetFileName = "IMGA_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
                        _buildimg.JpgTargetPath = @Directory.GetCurrentDirectory() + "\\img\\";
                        imgPath = _buildimg.JpgTargetPath + _buildimg.JpgTargetFileName + ".jpg";
                        _thread.SetApartmentState(ApartmentState.STA);
                        _thread.Start(_buildimg);

                        //while (_thread.ThreadState == ThreadState.Running) { }
                        while (_thread.IsAlive == true) { }

                        //添加1张图片
                        System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(@imgPath);
                        mailM.Attachments.Add(attachment);

                        strBody += "<img src=\"cid:" + attachment.ContentId + "\" /><br/>";


                        iRowColumn[0] += 20;
                        iRowColumn[2] += 20;
                        //if (i == 0)
                        //{

                        //}
                        //else if (i == 1)
                        //{
                        //    iRowColumn[0] += 21;
                        //    iRowColumn[2] += 20;
                        //}
                        //else if (i == 2)
                        //{
                        //    iRowColumn[0] += 21;
                        //    iRowColumn[2] += 21;
                        //}

                    }

                    ////邮件内容替换为图片
                    //strBody = strBody.Replace("[picture1]", "<img src=\"cid:" + attachment.ContentId + "\" />");
                }
                strBody += strMail;

                //邮件内容
                mailM.Body = strBody;

                // 设置SMTP客户端
                SmtpClient smtp = ConfiguratSMTP();

                //发送
                smtp.Send(mailM);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        /// <summary> 
        /// 发送邮件(正文只带一个表格图片)  
        /// Send email(the body only have one table pic)
        /// </summary>
        /// <param name="arrStrTO">收件人地址</param>
        /// <param name="arrStrCC">CC地址</param>
        /// <param name="strSubject">邮件标题</param>
        /// <param name="strBody">邮件内容</param>
        /// <param name="arrAttPath">附件文件地址</param>
        /// <param name="strAttPath">截图文件地址</param>
        /// <param name="strSheetName">截图Sheet名称</param>
        /// <param name="iRowColumn">截图坐标{BeginRow, BeginCol, EndRow, EndCol}</param>
        /// <returns>发送是否成功</returns>
        public string ToSendEmail(string arrStrTO, string arrStrCC, string arrStrBCC, string strSubject, string strBody, string arrAttPath,
            string strAttPath, string strSheetName, int[] iRowColumn)
        {
            try
            {
                //收件人地址
                if (arrStrTO != "" && arrStrTO.Length > 0)
                {
                    //拆分多个收件人
                    string[] StrEmailMTO = arrStrTO.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string strTO in StrEmailMTO)
                    {
                        try
                        {
                            if (strTO.Trim() != "")
                            {
                                mailM.To.Add(strTO);
                            }
                        }
                        catch { }
                    }
                }

                //CC人地址
                if (arrStrCC != null && arrStrCC.Length > 0)
                {
                    //拆分多个CC人
                    string[] StrEmailMCC = arrStrCC.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strCC in StrEmailMCC)
                    {
                        try
                        {
                            if (strCC.Trim() != "")
                            {
                                mailM.CC.Add(strCC);
                            }
                        }
                        catch { }
                    }
                }

                //CC人地址
                if (arrStrBCC != null && arrStrBCC.Length > 0)
                {
                    //拆分多个CC人
                    string[] StrEmailMBCC = arrStrBCC.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strCC in StrEmailMBCC)
                    {
                        try
                        {
                            if (strCC.Trim() != "")
                            {
                                mailM.Bcc.Add(strCC);
                            }
                        }
                        catch { }
                    }
                }

                //邮件标题
                mailM.Subject = strSubject;

                //添加附件
                if (arrAttPath != null && arrAttPath.Length > 0)
                {
                    //拆分多个附件
                    string[] StrEmailMAtt = arrAttPath.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strAtt in StrEmailMAtt)
                    {
                        //添加附件
                        mailM.BodyEncoding = Encoding.GetEncoding("utf-8");
                        Attachment att = new Attachment(@strAtt);
                        mailM.Attachments.Add(att);
                    }
                }

                //邮件正文图片
                string imgPath = "";
                if (!string.IsNullOrEmpty(strAttPath))
                {
                    ParameterizedThreadStart _paraThread = new ParameterizedThreadStart(MakeJpg);
                    Thread _thread = new Thread(_paraThread);
                    _thread.IsBackground = true;
                    BuildImg _buildimg = new BuildImg();
                    _buildimg.xlsFilePath = strAttPath;
                    _buildimg.FromSheet = strSheetName;
                    _buildimg.BeginRow = iRowColumn[0];
                    _buildimg.BeginCol = iRowColumn[1];
                    _buildimg.EndRow = iRowColumn[2];
                    _buildimg.EndCol = iRowColumn[3];
                    _buildimg.JpgTargetFileName = "IMGA_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
                    _buildimg.JpgTargetPath = @Directory.GetCurrentDirectory() + "\\img\\";
                    imgPath = _buildimg.JpgTargetPath + _buildimg.JpgTargetFileName + ".jpg";
                    _thread.SetApartmentState(ApartmentState.STA);
                    _thread.Start(_buildimg);

                    //while (_thread.ThreadState == ThreadState.Running) { }
                    while (_thread.IsAlive == true) { }

                    //添加1张图片
                    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(@imgPath);
                    mailM.Attachments.Add(attachment);

                    //邮件内容替换为图片
                    strBody = strBody.Replace("[picture1]", "<img src=\"cid:" + attachment.ContentId + "\" />");
                }

                //邮件内容
                mailM.Body = strBody;

                // 设置SMTP客户端
                SmtpClient smtp = ConfiguratSMTP();

                //发送
                smtp.Send(mailM);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        /// <summary> 
        /// 发送邮件(正文带一个表格图片和一个图表图片) 
        /// Send email(the body have one table pic and one shape pic)
        /// </summary>
        /// <param name="arrStrTO">收件人地址</param>
        /// <param name="arrStrCC">CC地址</param>
        /// <param name="strSubject">邮件标题</param>
        /// <param name="strBody">邮件内容</param>
        /// <param name="arrAttPath">附件文件地址</param>
        /// <param name="strAttPath1">截图1文件地址</param>
        /// <param name="strSheetName1">截图1Sheet名称</param>
        /// <param name="iRowColumn1">截图1坐标{BeginRow, BeginCol, EndRow, EndCol}</param>
        /// <param name="strAttPath2">截图2文件地址</param>
        /// <param name="sheetName2">截图2Sheet名称</param>
        /// <param name="iIndex2">截图2Sheet中第i张图片</param>
        /// <returns>发送是否成功</returns>
        public string ToSendEmail(string arrStrTO, string arrStrCC, string strSubject, string strBody, string arrAttPath,
            string strAttPath1, string strSheetName1, int[] iRowColumn1,
            string strAttPath2, string strSheetName2, int iIndex2,
            string strAttPath3 = "", string strSheetName3 = "", int iIndex3 = 0)
        {
            try
            {
                //收件人地址
                if (arrStrTO != "" && arrStrTO.Length > 0)
                {
                    //拆分多个收件人
                    string[] StrEmailMTO = arrStrTO.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strTO in StrEmailMTO)
                    {
                        if (strTO.Trim() != "")
                        {
                            mailM.To.Add(strTO);
                        }

                    }
                }

                //CC人地址
                if (arrStrCC != null && arrStrCC.Length > 0)
                {
                    //拆分多个CC人
                    string[] StrEmailMCC = arrStrCC.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strCC in StrEmailMCC)
                    {
                        if (strCC.Trim() != "")
                        {
                            mailM.CC.Add(strCC);
                        }
                    }
                }

                //邮件标题
                mailM.Subject = strSubject;

                //添加附件
                if (arrAttPath != null && arrAttPath.Length > 0)
                {
                    //拆分多个附件
                    string[] StrEmailMAtt = arrAttPath.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strAtt in StrEmailMAtt)
                    {
                        mailM.BodyEncoding = Encoding.GetEncoding("utf-8");
                        Attachment att = new Attachment(@strAtt);
                        mailM.Attachments.Add(att);
                    }
                }

                //生成图片1
                string imgPath1 = "";
                if (!string.IsNullOrEmpty(strAttPath1))
                {
                    ParameterizedThreadStart _paraThread = new ParameterizedThreadStart(MakeJpg);
                    Thread _thread = new Thread(_paraThread);
                    _thread.IsBackground = true;
                    BuildImg _buildimg = new BuildImg();
                    _buildimg.xlsFilePath = strAttPath1;
                    _buildimg.FromSheet = strSheetName1;
                    _buildimg.BeginRow = iRowColumn1[0];
                    _buildimg.BeginCol = iRowColumn1[1];
                    _buildimg.EndRow = iRowColumn1[2];
                    _buildimg.EndCol = iRowColumn1[3];
                    _buildimg.JpgTargetFileName = "IMGA_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
                    _buildimg.JpgTargetPath = @Directory.GetCurrentDirectory() + "\\img\\";
                    imgPath1 = _buildimg.JpgTargetPath + _buildimg.JpgTargetFileName + ".jpg";
                    _thread.SetApartmentState(ApartmentState.STA);
                    _thread.Start(_buildimg);

                    //while (_thread.ThreadState == ThreadState.Running) { }
                    while (_thread.IsAlive == true) { }

                    //添加图片
                    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(@imgPath1);
                    mailM.Attachments.Add(attachment);
                    //邮件内容替换为图片
                    strBody = strBody.Replace("[picture1]", "<img src=\"cid:" + attachment.ContentId + "\" />");
                }

                //生成图片2
                string imgPath2 = "";
                if (!string.IsNullOrEmpty(strAttPath2))
                {
                    ParameterizedThreadStart _paraThreadII = new ParameterizedThreadStart(MakeJpgII);
                    Thread _threadII = new Thread(_paraThreadII);
                    _threadII.IsBackground = true;
                    BuildImg _buildimgII = new BuildImg();
                    _buildimgII.xlsFilePath = strAttPath2;
                    _buildimgII.FromSheet = strSheetName2;
                    _buildimgII.Index = iIndex2;
                    _buildimgII.JpgTargetFileName = "IMGB_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
                    _buildimgII.JpgTargetPath = @Directory.GetCurrentDirectory() + "\\img\\";
                    imgPath2 = _buildimgII.JpgTargetPath + _buildimgII.JpgTargetFileName + ".jpg";//makeImg(path, name);
                    _threadII.SetApartmentState(ApartmentState.STA);
                    _threadII.Start(_buildimgII);
                    //while (_threadII.ThreadState == ThreadState.Running) { }    //最早时使用waitone，现使用一个死循环
                    while (_threadII.IsAlive == true) { }

                    System.Net.Mail.Attachment attachment2 = new System.Net.Mail.Attachment(@imgPath2);
                    mailM.Attachments.Add(attachment2);

                    //邮件内容替换为图片
                    strBody = strBody.Replace("[picture2]", "<img src=\"cid:" + attachment2.ContentId + "\" />");
                }

                string imgPath3 = "";
                if (!string.IsNullOrEmpty(strAttPath3))
                {
                    ParameterizedThreadStart _paraThreadIII = new ParameterizedThreadStart(MakeJpgIII);
                    Thread _threadIII = new Thread(_paraThreadIII);
                    _threadIII.IsBackground = true;
                    BuildImg _buildimgIII = new BuildImg();
                    _buildimgIII.xlsFilePath = strAttPath3;
                    _buildimgIII.FromSheet = strSheetName3;
                    _buildimgIII.Index = iIndex3;
                    _buildimgIII.JpgTargetFileName = "IMGB_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
                    _buildimgIII.JpgTargetPath = @Directory.GetCurrentDirectory() + "\\img\\";
                    imgPath3 = _buildimgIII.JpgTargetPath + _buildimgIII.JpgTargetFileName + ".jpg";//makeImg(path, name);
                    _threadIII.SetApartmentState(ApartmentState.STA);
                    _threadIII.Start(_buildimgIII);
                    //while (_threadII.ThreadState == ThreadState.Running) { }    //最早时使用waitone，现使用一个死循环
                    while (_threadIII.IsAlive == true) { }

                    System.Net.Mail.Attachment attachment3 = new System.Net.Mail.Attachment(@imgPath3);
                    mailM.Attachments.Add(attachment3);

                    //邮件内容替换为图片
                    strBody = strBody.Replace("[picture3]", "<img src=\"cid:" + attachment3.ContentId + "\" />");
                }

                mailM.Body = strBody;

                // 设置SMTP客户端
                SmtpClient smtp = ConfiguratSMTP(); ;
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        /// <summary> 
        /// 发送邮件(正文只带一个表格图片)  
        /// Send email(the body only have one table pic)
        /// </summary>
        /// <param name="arrStrTO">收件人地址</param>
        /// <param name="arrStrCC">CC地址</param>
        /// <param name="strSubject">邮件标题</param>
        /// <param name="strBody">邮件内容</param>
        /// <param name="arrAttPath">附件文件地址</param>
        /// <param name="strAttPath">截图文件地址</param>
        /// <param name="strSheetName">截图Sheet名称</param>
        /// <param name="iIndex"> capture index of image/param>
        /// <returns>发送是否成功</returns>

        /// 
        public string ToSendEmail(string arrStrTO, string arrStrCC, string strSubject, string strBody, string arrAttPath,
        string strAttPath1, string strSheetName1, int[] iRowColumn1,
        string strAttPath2, string strSheetName2, int[] iIndex)
        {
            try
            {
                //收件人地址
                if (arrStrTO != "" && arrStrTO.Length > 0)
                {
                    //拆分多个收件人
                    string[] StrEmailMTO = arrStrTO.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strTO in StrEmailMTO)
                    {
                        if (strTO.Trim() != "")
                        {
                            mailM.To.Add(strTO);
                        }

                    }
                }

                //CC人地址
                if (arrStrCC != null && arrStrCC.Length > 0)
                {
                    //拆分多个CC人
                    string[] StrEmailMCC = arrStrCC.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strCC in StrEmailMCC)
                    {
                        if (strCC.Trim() != "")
                        {
                            mailM.CC.Add(strCC);
                        }
                    }
                }

                //邮件标题
                mailM.Subject = strSubject;

                //添加附件
                if (arrAttPath != null && arrAttPath.Length > 0)
                {
                    //拆分多个附件
                    string[] StrEmailMAtt = arrAttPath.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strAtt in StrEmailMAtt)
                    {
                        mailM.BodyEncoding = Encoding.GetEncoding("utf-8");
                        Attachment att = new Attachment(@strAtt);
                        mailM.Attachments.Add(att);
                    }
                }

                ////生成图片1
                //string imgPath1 = "";
                //if (!string.IsNullOrEmpty(strAttPath1))
                //{
                //    ParameterizedThreadStart _paraThread = new ParameterizedThreadStart(MakeJpg);
                //    Thread _thread = new Thread(_paraThread);
                //    _thread.IsBackground = true;
                //    BuildImg _buildimg = new BuildImg();
                //    _buildimg.xlsFilePath = strAttPath1;
                //    _buildimg.FromSheet = strSheetName1;
                //    _buildimg.BeginRow = iRowColumn1[0];
                //    _buildimg.BeginCol = iRowColumn1[1];
                //    _buildimg.EndRow = iRowColumn1[2];
                //    _buildimg.EndCol = iRowColumn1[3];
                //    _buildimg.JpgTargetFileName = "IMGA_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
                //    _buildimg.JpgTargetPath = @Directory.GetCurrentDirectory() + "\\img\\";
                //    imgPath1 = _buildimg.JpgTargetPath + _buildimg.JpgTargetFileName + ".jpg";
                //    _thread.SetApartmentState(ApartmentState.STA);
                //    _thread.Start(_buildimg);

                //    //while (_thread.ThreadState == ThreadState.Running) { }
                //    while (_thread.IsAlive == true) { }

                //    //添加图片
                //    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(@imgPath1);
                //    mailM.Attachments.Add(attachment);
                //    //邮件内容替换为图片
                //    strBody = strBody.Replace("[picture1]", "<img src=\"cid:" + attachment.ContentId + "\" />");
                //}

                //生成图片2
                foreach (int i in iIndex)
                {
                    string imgPath2 = "";
                    if (!string.IsNullOrEmpty(strAttPath2))
                    {
                        ParameterizedThreadStart _paraThreadII = new ParameterizedThreadStart(MakeJpgII);
                        Thread _threadII = new Thread(_paraThreadII);
                        _threadII.IsBackground = true;
                        BuildImg _buildimgII = new BuildImg();
                        _buildimgII.xlsFilePath = strAttPath2;
                        _buildimgII.FromSheet = strSheetName2;
                        _buildimgII.Index = i;
                        _buildimgII.JpgTargetFileName = "IMGA_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
                        _buildimgII.JpgTargetPath = @Directory.GetCurrentDirectory() + "\\img\\";
                        imgPath2 = _buildimgII.JpgTargetPath + _buildimgII.JpgTargetFileName + ".jpg";//makeImg(path, name);
                        _threadII.SetApartmentState(ApartmentState.STA);
                        _threadII.Start(_buildimgII);
                        //while (_threadII.ThreadState == ThreadState.Running) { }    //最早时使用waitone，现使用一个死循环
                        while (_threadII.IsAlive == true) { }

                        System.Net.Mail.Attachment attachment2 = new System.Net.Mail.Attachment(@imgPath2);
                        mailM.Attachments.Add(attachment2);

                        //邮件内容替换为图片
                        strBody = strBody.Replace("[picture" + i.ToString() + "]", "<img src=\"cid:" + attachment2.ContentId + "\" />");
                    }
                }



                mailM.Body = strBody;

                // 设置SMTP客户端
                SmtpClient smtp = ConfiguratSMTP();

                //发送
                smtp.Send(mailM);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        /// <summary> 
        /// 发送邮件(正文带两个表格图片和两个图表图片) 
        /// Send email(the body have two table pic and two shape pic)
        /// </summary>
        /// <param name="arrStrTO">收件人地址</param>
        /// <param name="arrStrCC">CC地址</param>
        /// <param name="strSubject">邮件标题</param>
        /// <param name="strBody">邮件内容</param>
        /// <param name="arrAttPath">附件文件地址</param>
        /// <param name="strAttPath1">截图1文件地址</param>
        /// <param name="strSheetName1">截图1Sheet名称</param>
        /// <param name="iRowColumn1">截图1坐标{BeginRow, BeginCol, EndRow, EndCol}</param>
        /// <param name="strAttPath2">截图2文件地址</param>
        /// <param name="sheetName2">截图2Sheet名称</param>
        /// <param name="iIndex2">截图2Sheet中第i张图片</param>
        /// <param name="strAttPath3">截图3文件地址</param>
        /// <param name="strSheetName3">截图3Sheet名称</param>
        /// <param name="iRowColumn3">截图3坐标{BeginRow, BeginCol, EndRow, EndCol}</param>
        /// <param name="strAttPath4">截图4文件地址</param>
        /// <param name="sheetName4">截图4Sheet名称</param>
        /// <param name="iIndex4">截图4Sheet中第i张图片</param>
        /// <returns>发送是否成功</returns>

        public string ToSendEmail(string arrStrTO, string arrStrCC, string strSubject, string strBody, string arrAttPath,
            string strAttPath1, string strSheetName1, int[] iRowColumn1,
            string strAttPath2, string strSheetName2, int iIndex2,
            string strAttPath3, string strSheetName3, int[] iRowColumn3,
            string strAttPath4, string strSheetName4, int iIndex4)
        {
            try
            {
                //收件人地址
                if (arrStrTO != "" && arrStrTO.Length > 0)
                {
                    //拆分多个收件人
                    string[] StrEmailMTO = arrStrTO.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strTO in StrEmailMTO)
                    {
                        try
                        {
                            if (strTO.Trim() != "")
                            {
                                mailM.To.Add(strTO);
                            }
                        }
                        catch { }
                    }
                }

                //CC人地址
                if (arrStrCC != null && arrStrCC.Length > 0)
                {
                    //拆分多个CC人
                    string[] StrEmailMCC = arrStrCC.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strCC in StrEmailMCC)
                    {
                        try
                        {
                            if (strCC.Trim() != "")
                            {
                                mailM.CC.Add(strCC);
                            }
                        }
                        catch { }
                    }
                }

                //邮件标题
                mailM.Subject = strSubject;

                //添加附件
                if (arrAttPath != null && arrAttPath.Length > 0)
                {
                    //拆分多个附件
                    string[] StrEmailMAtt = arrAttPath.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strAtt in StrEmailMAtt)
                    {
                        mailM.BodyEncoding = Encoding.GetEncoding("utf-8");
                        Attachment att = new Attachment(@strAtt, MediaTypeNames.Application.Octet);
                        mailM.Attachments.Add(att);
                    }
                }

                //生成表格1
                string imgPath1 = "";
                if (!string.IsNullOrEmpty(strAttPath1))
                {
                    ParameterizedThreadStart _paraThread = new ParameterizedThreadStart(MakeJpg);
                    Thread _thread = new Thread(_paraThread);
                    _thread.IsBackground = true;
                    BuildImg _buildimg = new BuildImg();
                    _buildimg.xlsFilePath = strAttPath1;
                    _buildimg.FromSheet = strSheetName1;
                    _buildimg.BeginRow = iRowColumn1[0];
                    _buildimg.BeginCol = iRowColumn1[1];
                    _buildimg.EndRow = iRowColumn1[2];
                    _buildimg.EndCol = iRowColumn1[3];
                    _buildimg.JpgTargetFileName = "IMGA_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
                    _buildimg.JpgTargetPath = @Directory.GetCurrentDirectory() + "\\img\\";
                    imgPath1 = _buildimg.JpgTargetPath + _buildimg.JpgTargetFileName + ".jpg";
                    _thread.SetApartmentState(ApartmentState.STA);
                    _thread.Start(_buildimg);

                    //while (_thread.ThreadState == ThreadState.Running) { }
                    while (_thread.IsAlive == true) { }

                    //添加图片
                    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(@imgPath1);
                    mailM.Attachments.Add(attachment);
                    //邮件内容替换为图片
                    strBody = strBody.Replace("[picture1]", "<img src=\"cid:" + attachment.ContentId + "\" />");
                }

                //生成图表2
                string imgPath2 = "";
                if (!string.IsNullOrEmpty(strAttPath2))
                {
                    ParameterizedThreadStart _paraThreadII = new ParameterizedThreadStart(MakeJpgII);
                    Thread _threadII = new Thread(_paraThreadII);
                    _threadII.IsBackground = true;
                    BuildImg _buildimgII = new BuildImg();
                    _buildimgII.xlsFilePath = strAttPath2;
                    _buildimgII.FromSheet = strSheetName2;
                    _buildimgII.JpgTargetFileName = "IMGB_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
                    _buildimgII.JpgTargetPath = @Directory.GetCurrentDirectory() + "\\img\\";
                    _buildimgII.Index = iIndex2;
                    imgPath2 = _buildimgII.JpgTargetPath + _buildimgII.JpgTargetFileName + ".jpg";
                    _threadII.SetApartmentState(ApartmentState.STA);
                    _threadII.Start(_buildimgII);
                    //while (_threadII.ThreadState == ThreadState.Running) { }    //最早时使用waitone，现使用一个死循环
                    while (_threadII.IsAlive == true) { }

                    //添加图片
                    System.Net.Mail.Attachment attachment2 = new System.Net.Mail.Attachment(@imgPath2);
                    mailM.Attachments.Add(attachment2);
                    //邮件内容替换为图片
                    strBody = strBody.Replace("[picture2]", "<img src=\"cid:" + attachment2.ContentId + "\" />");
                }

                //生成表格3
                string imgPath3 = "";
                if (!string.IsNullOrEmpty(strAttPath3))
                {
                    ParameterizedThreadStart _paraThread = new ParameterizedThreadStart(MakeJpg);
                    Thread _thread = new Thread(_paraThread);
                    _thread.IsBackground = true;
                    BuildImg _buildimg = new BuildImg();
                    _buildimg.xlsFilePath = strAttPath3;
                    _buildimg.FromSheet = strSheetName3;
                    _buildimg.BeginRow = iRowColumn3[0];
                    _buildimg.BeginCol = iRowColumn3[1];
                    _buildimg.EndRow = iRowColumn3[2];
                    _buildimg.EndCol = iRowColumn3[3];
                    _buildimg.JpgTargetFileName = "IMGC_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
                    _buildimg.JpgTargetPath = @Directory.GetCurrentDirectory() + "\\img\\";
                    imgPath3 = _buildimg.JpgTargetPath + _buildimg.JpgTargetFileName + ".jpg";
                    _thread.SetApartmentState(ApartmentState.STA);
                    _thread.Start(_buildimg);

                    //while (_thread.ThreadState == ThreadState.Running) { }
                    while (_thread.IsAlive == true) { }

                    //添加图片
                    System.Net.Mail.Attachment attachment3 = new System.Net.Mail.Attachment(@imgPath3);
                    mailM.Attachments.Add(attachment3);
                    //邮件内容替换为图片
                    strBody = strBody.Replace("[picture3]", "<img src=\"cid:" + attachment3.ContentId + "\" />");
                }

                //生成图表4
                string imgPath4 = "";
                if (!string.IsNullOrEmpty(strAttPath4))
                {
                    ParameterizedThreadStart _paraThreadII = new ParameterizedThreadStart(MakeJpgII);
                    Thread _threadII = new Thread(_paraThreadII);
                    _threadII.IsBackground = true;
                    BuildImg _buildimgII = new BuildImg();
                    _buildimgII.xlsFilePath = strAttPath4;
                    _buildimgII.FromSheet = strSheetName4;
                    _buildimgII.JpgTargetFileName = "IMGD_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
                    _buildimgII.JpgTargetPath = @Directory.GetCurrentDirectory() + "\\img\\";
                    _buildimgII.Index = iIndex4;
                    imgPath4 = _buildimgII.JpgTargetPath + _buildimgII.JpgTargetFileName + ".jpg";
                    _threadII.SetApartmentState(ApartmentState.STA);
                    _threadII.Start(_buildimgII);
                    //while (_threadII.ThreadState == ThreadState.Running) { }    //最早时使用waitone，现使用一个死循环
                    while (_threadII.IsAlive == true) { }

                    //添加图片
                    System.Net.Mail.Attachment attachment4 = new System.Net.Mail.Attachment(@imgPath4);
                    mailM.Attachments.Add(attachment4);
                    //邮件内容替换为图片
                    strBody = strBody.Replace("[picture4]", "<img src=\"cid:" + attachment4.ContentId + "\" />");
                }

                //邮件内容
                mailM.Body = strBody;

                // 设置SMTP客户端
                SmtpClient smtp = ConfiguratSMTP();


                //发送
                smtp.Send(mailM);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        /// <summary> 
        /// 发送邮件(正文带三个表格图片和三个图表图片) 
        /// Send email(the body have three table pic and three shape pic)
        /// </summary>
        /// <param name="arrStrTO">收件人地址</param>
        /// <param name="arrStrCC">CC地址</param>
        /// <param name="strSubject">邮件标题</param>
        /// <param name="strBody">邮件内容</param>
        /// <param name="arrAttPath">附件文件地址</param>
        /// <param name="strAttPath1">截图1文件地址</param>
        /// <param name="strSheetName1">截图1Sheet名称</param>
        /// <param name="iRowColumn1">截图1坐标{BeginRow, BeginCol, EndRow, EndCol}</param>
        /// <param name="strAttPath2">截图2文件地址</param>
        /// <param name="sheetName2">截图2Sheet名称</param>
        /// <param name="iIndex2">截图2Sheet中第i张图片</param>
        /// <param name="strAttPath3">截图3文件地址</param>
        /// <param name="strSheetName3">截图3Sheet名称</param>
        /// <param name="iRowColumn3">截图3坐标{BeginRow, BeginCol, EndRow, EndCol}</param>
        /// <param name="strAttPath4">截图4文件地址</param>
        /// <param name="sheetName4">截图4Sheet名称</param>
        /// <param name="iIndex4">截图4Sheet中第i张图片</param>
        /// <param name="strAttPath3">截图5文件地址</param>
        /// <param name="strSheetName3">截图5Sheet名称</param>
        /// <param name="iRowColumn3">截图5坐标{BeginRow, BeginCol, EndRow, EndCol}</param>
        /// <param name="strAttPath4">截图6文件地址</param>
        /// <param name="sheetName4">截图6Sheet名称</param>
        /// <param name="iIndex4">截图6Sheet中第i张图片</param>
        /// <returns>发送是否成功</returns>

        public string ToSendEmail(string arrStrTO, string arrStrCC, string strSubject, string strBody, string arrAttPath,
            string strAttPath1, string strSheetName1, int[] iRowColumn1,
            string strAttPath2, string strSheetName2, int iIndex2,
            string strAttPath3, string strSheetName3, int[] iRowColumn3,
            string strAttPath4, string strSheetName4, int iIndex4,
            string strAttPath5, string strSheetName5, int[] iRowColumn5,
            string strAttPath6, string strSheetName6, int iIndex6)
        {
            try
            {
                //收件人地址
                if (arrStrTO != "" && arrStrTO.Length > 0)
                {
                    //拆分多个收件人
                    string[] StrEmailMTO = arrStrTO.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strTO in StrEmailMTO)
                    {
                        try
                        {
                            if (strTO.Trim() != "")
                            {
                                mailM.To.Add(strTO);
                            }
                        }
                        catch { }
                    }
                }

                //CC人地址
                if (arrStrCC != null && arrStrCC.Length > 0)
                {
                    //拆分多个CC人
                    string[] StrEmailMCC = arrStrCC.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strCC in StrEmailMCC)
                    {
                        try
                        {
                            if (strCC.Trim() != "")
                            {
                                mailM.CC.Add(strCC);
                            }
                        }
                        catch { }
                    }
                }

                //邮件标题
                mailM.Subject = strSubject;

                //添加附件
                if (arrAttPath != null && arrAttPath.Length > 0)
                {
                    //拆分多个附件
                    string[] StrEmailMAtt = arrAttPath.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strAtt in StrEmailMAtt)
                    {
                        mailM.BodyEncoding = Encoding.GetEncoding("utf-8");
                        Attachment att = new Attachment(@strAtt, MediaTypeNames.Application.Octet);
                        mailM.Attachments.Add(att);
                    }
                }

                //生成表格1
                string imgPath1 = "";
                if (!string.IsNullOrEmpty(strAttPath1))
                {
                    ParameterizedThreadStart _paraThread = new ParameterizedThreadStart(MakeJpg);
                    Thread _thread = new Thread(_paraThread);
                    _thread.IsBackground = true;
                    BuildImg _buildimg = new BuildImg();
                    _buildimg.xlsFilePath = strAttPath1;
                    _buildimg.FromSheet = strSheetName1;
                    _buildimg.BeginRow = iRowColumn1[0];
                    _buildimg.BeginCol = iRowColumn1[1];
                    _buildimg.EndRow = iRowColumn1[2];
                    _buildimg.EndCol = iRowColumn1[3];
                    _buildimg.JpgTargetFileName = "IMGA_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
                    _buildimg.JpgTargetPath = @Directory.GetCurrentDirectory() + "\\img\\";
                    imgPath1 = _buildimg.JpgTargetPath + _buildimg.JpgTargetFileName + ".jpg";
                    _thread.SetApartmentState(ApartmentState.STA);
                    _thread.Start(_buildimg);

                    //while (_thread.ThreadState == ThreadState.Running) { }
                    while (_thread.IsAlive == true) { }

                    //添加图片
                    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(@imgPath1);
                    mailM.Attachments.Add(attachment);
                    //邮件内容替换为图片
                    strBody = strBody.Replace("[picture1]", "<img src=\"cid:" + attachment.ContentId + "\" />");
                }

                //生成图表2
                string imgPath2 = "";
                if (!string.IsNullOrEmpty(strAttPath2))
                {
                    ParameterizedThreadStart _paraThreadII = new ParameterizedThreadStart(MakeJpgII);
                    Thread _threadII = new Thread(_paraThreadII);
                    _threadII.IsBackground = true;
                    BuildImg _buildimgII = new BuildImg();
                    _buildimgII.xlsFilePath = strAttPath2;
                    _buildimgII.FromSheet = strSheetName2;
                    _buildimgII.JpgTargetFileName = "IMGB_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
                    _buildimgII.JpgTargetPath = @Directory.GetCurrentDirectory() + "\\img\\";
                    _buildimgII.Index = iIndex2;
                    imgPath2 = _buildimgII.JpgTargetPath + _buildimgII.JpgTargetFileName + ".jpg";
                    _threadII.SetApartmentState(ApartmentState.STA);
                    _threadII.Start(_buildimgII);
                    //while (_threadII.ThreadState == ThreadState.Running) { }    //最早时使用waitone，现使用一个死循环
                    while (_threadII.IsAlive == true) { }

                    //添加图片
                    System.Net.Mail.Attachment attachment2 = new System.Net.Mail.Attachment(@imgPath2);
                    mailM.Attachments.Add(attachment2);
                    //邮件内容替换为图片
                    strBody = strBody.Replace("[picture2]", "<img src=\"cid:" + attachment2.ContentId + "\" />");
                }

                //生成表格3
                string imgPath3 = "";
                if (!string.IsNullOrEmpty(strAttPath3))
                {
                    ParameterizedThreadStart _paraThread = new ParameterizedThreadStart(MakeJpg);
                    Thread _thread = new Thread(_paraThread);
                    _thread.IsBackground = true;
                    BuildImg _buildimg = new BuildImg();
                    _buildimg.xlsFilePath = strAttPath3;
                    _buildimg.FromSheet = strSheetName3;
                    _buildimg.BeginRow = iRowColumn3[0];
                    _buildimg.BeginCol = iRowColumn3[1];
                    _buildimg.EndRow = iRowColumn3[2];
                    _buildimg.EndCol = iRowColumn3[3];
                    _buildimg.JpgTargetFileName = "IMGC_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
                    _buildimg.JpgTargetPath = @Directory.GetCurrentDirectory() + "\\img\\";
                    imgPath3 = _buildimg.JpgTargetPath + _buildimg.JpgTargetFileName + ".jpg";
                    _thread.SetApartmentState(ApartmentState.STA);
                    _thread.Start(_buildimg);

                    //while (_thread.ThreadState == ThreadState.Running) { }
                    while (_thread.IsAlive == true) { }

                    //添加图片
                    System.Net.Mail.Attachment attachment3 = new System.Net.Mail.Attachment(@imgPath3);
                    mailM.Attachments.Add(attachment3);
                    //邮件内容替换为图片
                    strBody = strBody.Replace("[picture3]", "<img src=\"cid:" + attachment3.ContentId + "\" />");
                }

                //生成图表4
                string imgPath4 = "";
                if (!string.IsNullOrEmpty(strAttPath4))
                {
                    ParameterizedThreadStart _paraThreadII = new ParameterizedThreadStart(MakeJpgII);
                    Thread _threadII = new Thread(_paraThreadII);
                    _threadII.IsBackground = true;
                    BuildImg _buildimgII = new BuildImg();
                    _buildimgII.xlsFilePath = strAttPath4;
                    _buildimgII.FromSheet = strSheetName4;
                    _buildimgII.JpgTargetFileName = "IMGD_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
                    _buildimgII.JpgTargetPath = @Directory.GetCurrentDirectory() + "\\img\\";
                    _buildimgII.Index = iIndex4;
                    imgPath4 = _buildimgII.JpgTargetPath + _buildimgII.JpgTargetFileName + ".jpg";
                    _threadII.SetApartmentState(ApartmentState.STA);
                    _threadII.Start(_buildimgII);
                    //while (_threadII.ThreadState == ThreadState.Running) { }    //最早时使用waitone，现使用一个死循环
                    while (_threadII.IsAlive == true) { }

                    //添加图片
                    System.Net.Mail.Attachment attachment4 = new System.Net.Mail.Attachment(@imgPath4);
                    mailM.Attachments.Add(attachment4);
                    //邮件内容替换为图片
                    strBody = strBody.Replace("[picture4]", "<img src=\"cid:" + attachment4.ContentId + "\" />");
                }

                //生成表格5
                string imgPath5 = "";
                if (!string.IsNullOrEmpty(strAttPath5))
                {
                    ParameterizedThreadStart _paraThread = new ParameterizedThreadStart(MakeJpg);
                    Thread _thread = new Thread(_paraThread);
                    _thread.IsBackground = true;
                    BuildImg _buildimg = new BuildImg();
                    _buildimg.xlsFilePath = strAttPath5;
                    _buildimg.FromSheet = strSheetName5;
                    _buildimg.BeginRow = iRowColumn5[0];
                    _buildimg.BeginCol = iRowColumn5[1];
                    _buildimg.EndRow = iRowColumn5[2];
                    _buildimg.EndCol = iRowColumn5[3];
                    _buildimg.JpgTargetFileName = "IMGE_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
                    _buildimg.JpgTargetPath = @Directory.GetCurrentDirectory() + "\\img\\";
                    imgPath5 = _buildimg.JpgTargetPath + _buildimg.JpgTargetFileName + ".jpg";
                    _thread.SetApartmentState(ApartmentState.STA);
                    _thread.Start(_buildimg);

                    //while (_thread.ThreadState == ThreadState.Running) { }
                    while (_thread.IsAlive == true) { }

                    //添加图片
                    System.Net.Mail.Attachment attachment5 = new System.Net.Mail.Attachment(@imgPath5);
                    mailM.Attachments.Add(attachment5);
                    //邮件内容替换为图片
                    strBody = strBody.Replace("[picture5]", "<img src=\"cid:" + attachment5.ContentId + "\" />");
                }

                //生成图表6
                string imgPath6 = "";
                if (!string.IsNullOrEmpty(strAttPath6))
                {
                    ParameterizedThreadStart _paraThreadII = new ParameterizedThreadStart(MakeJpgII);
                    Thread _threadII = new Thread(_paraThreadII);
                    _threadII.IsBackground = true;
                    BuildImg _buildimgII = new BuildImg();
                    _buildimgII.xlsFilePath = strAttPath6;
                    _buildimgII.FromSheet = strSheetName6;
                    _buildimgII.JpgTargetFileName = "IMGF_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
                    _buildimgII.JpgTargetPath = @Directory.GetCurrentDirectory() + "\\img\\";
                    _buildimgII.Index = iIndex6;
                    imgPath6 = _buildimgII.JpgTargetPath + _buildimgII.JpgTargetFileName + ".jpg";
                    _threadII.SetApartmentState(ApartmentState.STA);
                    _threadII.Start(_buildimgII);
                    //while (_threadII.ThreadState == ThreadState.Running) { }    //最早时使用waitone，现使用一个死循环
                    while (_threadII.IsAlive == true) { }

                    //添加图片
                    System.Net.Mail.Attachment attachment6 = new System.Net.Mail.Attachment(@imgPath6);
                    mailM.Attachments.Add(attachment6);
                    //邮件内容替换为图片
                    strBody = strBody.Replace("[picture6]", "<img src=\"cid:" + attachment6.ContentId + "\" />");
                }

                //邮件内容
                mailM.Body = strBody;

                // 设置SMTP客户端
                SmtpClient smtp = ConfiguratSMTP();


                //发送
                smtp.Send(mailM);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        /// <summary> 
        /// 发送邮件(正文带四个表格图片) 
        /// Send email(the body have four table pic and two shape pic)
        /// </summary>
        /// <param name="arrStrTO">收件人地址</param>
        /// <param name="arrStrCC">CC地址</param>
        /// <param name="strSubject">邮件标题</param>
        /// <param name="strBody">邮件内容</param>
        /// <param name="arrAttPath">附件文件地址</param>
        /// <param name="strAttPath1">截图1文件地址</param>
        /// <param name="strSheetName1">截图1Sheet名称</param>
        /// <param name="iRowColumn1">截图1坐标{BeginRow, BeginCol, EndRow, EndCol}</param>
        /// <param name="strAttPath2">截图2文件地址</param>
        /// <param name="sheetName2">截图2Sheet名称</param>
        /// <param name="iIndex2">截图2坐标{BeginRow, BeginCol, EndRow, EndCol}</param>
        /// <param name="strAttPath3">截图3文件地址</param>
        /// <param name="strSheetName3">截图3Sheet名称</param>
        /// <param name="iRowColumn3">截图3坐标{BeginRow, BeginCol, EndRow, EndCol}</param>
        /// <param name="strAttPath4">截图4文件地址</param>
        /// <param name="sheetName4">截图4Sheet名称</param>
        /// <param name="iIndex4">截图4坐标{BeginRow, BeginCol, EndRow, EndCol}</param>
        /// <returns>发送是否成功</returns>
    
        public string ToSendEmail(string arrStrTO, string arrStrCC, string strSubject, string strBody, string arrAttPath,
            string strAttPath1, string strSheetName1, int[] iRowColumn1,
            string strAttPath2, string strSheetName2, int[] iRowColumn2,
            string strAttPath3, string strSheetName3, int[] iRowColumn3,
            string strAttPath4, string strSheetName4, int[] iRowColumn4)
        {
            try
            {
                //收件人地址
                if (arrStrTO != "" && arrStrTO.Length > 0)
                {
                    //拆分多个收件人
                    string[] StrEmailMTO = arrStrTO.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strTO in StrEmailMTO)
                    {
                        try
                        {
                            if (strTO.Trim() != "")
                            {
                                mailM.To.Add(strTO);
                            }
                        }
                        catch { }
                    }
                }

                //CC人地址
                if (arrStrCC != null && arrStrCC.Length > 0)
                {
                    //拆分多个CC人
                    string[] StrEmailMCC = arrStrCC.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strCC in StrEmailMCC)
                    {
                        try
                        {
                            if (strCC.Trim() != "")
                            {
                                mailM.CC.Add(strCC);
                            }
                        }
                        catch { }
                    }
                }

                //邮件标题
                mailM.Subject = strSubject;

                //添加附件
                if (arrAttPath != null && arrAttPath.Length > 0)
                {
                    //拆分多个附件
                    string[] StrEmailMAtt = arrAttPath.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strAtt in StrEmailMAtt)
                    {
                        mailM.BodyEncoding = Encoding.GetEncoding("utf-8");
                        Attachment att = new Attachment(@strAtt, MediaTypeNames.Application.Octet);
                        mailM.Attachments.Add(att);
                    }
                }

                //生成表格1
                string imgPath1 = "";
                if (!string.IsNullOrEmpty(strAttPath1))
                {
                    ParameterizedThreadStart _paraThread = new ParameterizedThreadStart(MakeJpg);
                    Thread _thread = new Thread(_paraThread);
                    _thread.IsBackground = true;
                    BuildImg _buildimg = new BuildImg();
                    _buildimg.xlsFilePath = strAttPath1;
                    _buildimg.FromSheet = strSheetName1;
                    _buildimg.BeginRow = iRowColumn1[0];
                    _buildimg.BeginCol = iRowColumn1[1];
                    _buildimg.EndRow = iRowColumn1[2];
                    _buildimg.EndCol = iRowColumn1[3];
                    _buildimg.JpgTargetFileName = "IMGA_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
                    _buildimg.JpgTargetPath = @Directory.GetCurrentDirectory() + "\\img\\";
                    imgPath1 = _buildimg.JpgTargetPath + _buildimg.JpgTargetFileName + ".jpg";
                    _thread.SetApartmentState(ApartmentState.STA);
                    _thread.Start(_buildimg);

                    //while (_thread.ThreadState == ThreadState.Running) { }
                    while (_thread.IsAlive == true) { }

                    //添加图片
                    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(@imgPath1);
                    mailM.Attachments.Add(attachment);
                    //邮件内容替换为图片
                    strBody = strBody.Replace("[picture1]", "<img src=\"cid:" + attachment.ContentId + "\" />");
                }

                //生成图表2
                string imgPath2 = "";
                if (!string.IsNullOrEmpty(strAttPath2))
                {
                    ParameterizedThreadStart _paraThread = new ParameterizedThreadStart(MakeJpg);
                    Thread _thread = new Thread(_paraThread);
                    _thread.IsBackground = true;
                    BuildImg _buildimg = new BuildImg();
                    _buildimg.xlsFilePath = strAttPath1;
                    _buildimg.FromSheet = strSheetName2;
                    _buildimg.BeginRow = iRowColumn2[0];
                    _buildimg.BeginCol = iRowColumn2[1];
                    _buildimg.EndRow = iRowColumn2[2];
                    _buildimg.EndCol = iRowColumn2[3];
                    _buildimg.JpgTargetFileName = "IMGB_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
                    _buildimg.JpgTargetPath = @Directory.GetCurrentDirectory() + "\\img\\";
                    imgPath2 = _buildimg.JpgTargetPath + _buildimg.JpgTargetFileName + ".jpg";
                    _thread.SetApartmentState(ApartmentState.STA);
                    _thread.Start(_buildimg);

                    //while (_thread.ThreadState == ThreadState.Running) { }
                    while (_thread.IsAlive == true) { }

                    //添加图片
                    System.Net.Mail.Attachment attachment2 = new System.Net.Mail.Attachment(@imgPath2);
                    mailM.Attachments.Add(attachment2);
                    //邮件内容替换为图片
                    strBody = strBody.Replace("[picture2]", "<img src=\"cid:" + attachment2.ContentId + "\" />");
                }

                //生成表格3
                string imgPath3 = "";
                if (!string.IsNullOrEmpty(strAttPath3))
                {
                    ParameterizedThreadStart _paraThread = new ParameterizedThreadStart(MakeJpg);
                    Thread _thread = new Thread(_paraThread);
                    _thread.IsBackground = true;
                    BuildImg _buildimg = new BuildImg();
                    _buildimg.xlsFilePath = strAttPath3;
                    _buildimg.FromSheet = strSheetName3;
                    _buildimg.BeginRow = iRowColumn3[0];
                    _buildimg.BeginCol = iRowColumn3[1];
                    _buildimg.EndRow = iRowColumn3[2];
                    _buildimg.EndCol = iRowColumn3[3];
                    _buildimg.JpgTargetFileName = "IMGC_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
                    _buildimg.JpgTargetPath = @Directory.GetCurrentDirectory() + "\\img\\";
                    imgPath3 = _buildimg.JpgTargetPath + _buildimg.JpgTargetFileName + ".jpg";
                    _thread.SetApartmentState(ApartmentState.STA);
                    _thread.Start(_buildimg);

                    //while (_thread.ThreadState == ThreadState.Running) { }
                    while (_thread.IsAlive == true) { }

                    //添加图片
                    System.Net.Mail.Attachment attachment3 = new System.Net.Mail.Attachment(@imgPath3);
                    mailM.Attachments.Add(attachment3);
                    //邮件内容替换为图片
                    strBody = strBody.Replace("[picture3]", "<img src=\"cid:" + attachment3.ContentId + "\" />");
                }

                //生成图表4
                string imgPath4 = "";
                if (!string.IsNullOrEmpty(strAttPath4))
                {
                    ParameterizedThreadStart _paraThread = new ParameterizedThreadStart(MakeJpg);
                    Thread _thread = new Thread(_paraThread);
                    _thread.IsBackground = true;
                    BuildImg _buildimg = new BuildImg();
                    _buildimg.xlsFilePath = strAttPath3;
                    _buildimg.FromSheet = strSheetName4;
                    _buildimg.BeginRow = iRowColumn4[0];
                    _buildimg.BeginCol = iRowColumn4[1];
                    _buildimg.EndRow = iRowColumn4[2];
                    _buildimg.EndCol = iRowColumn4[3];
                    _buildimg.JpgTargetFileName = "IMGD_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
                    _buildimg.JpgTargetPath = @Directory.GetCurrentDirectory() + "\\img\\";
                    imgPath4 = _buildimg.JpgTargetPath + _buildimg.JpgTargetFileName + ".jpg";
                    _thread.SetApartmentState(ApartmentState.STA);
                    _thread.Start(_buildimg);

                    //while (_thread.ThreadState == ThreadState.Running) { }
                    while (_thread.IsAlive == true) { }

                    //添加图片
                    System.Net.Mail.Attachment attachment4 = new System.Net.Mail.Attachment(@imgPath4);
                    mailM.Attachments.Add(attachment4);
                    //邮件内容替换为图片
                    strBody = strBody.Replace("[picture4]", "<img src=\"cid:" + attachment4.ContentId + "\" />");
                }

                //邮件内容
                mailM.Body = strBody;

                // 设置SMTP客户端
                SmtpClient smtp = ConfiguratSMTP();


                //发送
                smtp.Send(mailM);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        /// <summary> 
        /// 发送邮件(正文带一个表格图片和一个图表图片) 
        /// Send email(the body have one table pic and one shape pic)
        /// </summary>
        /// <param name="arrStrTO">收件人地址</param>
        /// <param name="arrStrCC">CC地址</param>
        /// <param name="strSubject">邮件标题</param>
        /// <param name="strBody">邮件内容</param>
        /// <param name="arrAttPath">附件文件地址</param>
        /// <param name="strAttPath1">截图1文件地址</param>
        /// <param name="strSheetName1">截图1Sheet名称</param>
        /// <param name="iRowColumn1">截图1坐标{BeginRow, BeginCol, EndRow, EndCol}</param>
        /// <param name="strAttPath2">截图2文件地址</param>
        /// <param name="sheetName2">截图2Sheet名称</param>
        /// <param name="iIndex2">截图2Sheet中第i张图片</param>
        /// <returns>发送是否成功</returns>
       
        public string ToSendEmail(string arrStrTO, string arrStrCC, string strSubject, string strBody, string arrAttPath,
            string strAttPath1, string strSheetName1, int[] iRowColumn1,
            string strAttPath2, string strSheetName2, int iIndex2,
            string strAttPath3, string strSheetName3, int iIndex3,
            string strAttPath4, string strSheetName4, int iIndex4,
            int ImgWith, int ImgHeight)
        {
            try
            {
                //收件人地址
                if (arrStrTO != "" && arrStrTO.Length > 0)
                {
                    //拆分多个收件人
                    string[] StrEmailMTO = arrStrTO.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strTO in StrEmailMTO)
                    {
                        if (strTO.Trim() != "")
                        {
                            mailM.To.Add(strTO);
                        }

                    }
                }

                //CC人地址
                if (arrStrCC != null && arrStrCC.Length > 0)
                {
                    //拆分多个CC人
                    string[] StrEmailMCC = arrStrCC.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strCC in StrEmailMCC)
                    {
                        if (strCC.Trim() != "")
                        {
                            mailM.CC.Add(strCC);
                        }
                    }
                }

                //邮件标题
                mailM.Subject = strSubject;

                //添加附件
                if (arrAttPath != null && arrAttPath.Length > 0)
                {
                    //拆分多个附件
                    string[] StrEmailMAtt = arrAttPath.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strAtt in StrEmailMAtt)
                    {
                        mailM.BodyEncoding = Encoding.GetEncoding("utf-8");
                        Attachment att = new Attachment(@strAtt);
                        mailM.Attachments.Add(att);
                    }
                }

                //生成图片1
                string imgPath1 = "";
                if (!string.IsNullOrEmpty(strAttPath1))
                {
                    ParameterizedThreadStart _paraThread = new ParameterizedThreadStart(MakeJpg);
                    Thread _thread = new Thread(_paraThread);
                    _thread.IsBackground = true;
                    BuildImg _buildimg = new BuildImg();
                    _buildimg.xlsFilePath = strAttPath1;
                    _buildimg.FromSheet = strSheetName1;
                    _buildimg.BeginRow = iRowColumn1[0];
                    _buildimg.BeginCol = iRowColumn1[1];
                    _buildimg.EndRow = iRowColumn1[2];
                    _buildimg.EndCol = iRowColumn1[3];
                    _buildimg.JpgTargetFileName = "IMGA_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
                    _buildimg.JpgTargetPath = @Directory.GetCurrentDirectory() + "\\img\\";
                    imgPath1 = _buildimg.JpgTargetPath + _buildimg.JpgTargetFileName + ".jpg";
                    _thread.SetApartmentState(ApartmentState.STA);
                    _thread.Start(_buildimg);

                    //while (_thread.ThreadState == ThreadState.Running) { }
                    while (_thread.IsAlive == true) { }

                    //添加图片
                    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(@imgPath1);
                    mailM.Attachments.Add(attachment);
                    //邮件内容替换为图片
                    strBody = strBody.Replace("[picture1]", "<img src=\"cid:" + attachment.ContentId + "\" />");

                }

                //生成图片2
                string imgPath2 = "";
                if (!string.IsNullOrEmpty(strAttPath2))
                {
                    ParameterizedThreadStart _paraThreadII = new ParameterizedThreadStart(MakeJpgII);
                    Thread _threadII = new Thread(_paraThreadII);
                    _threadII.IsBackground = true;
                    BuildImg _buildimgII = new BuildImg();
                    _buildimgII.xlsFilePath = strAttPath2;
                    _buildimgII.FromSheet = strSheetName2;
                    _buildimgII.Index = iIndex2;
                    _buildimgII.JpgTargetFileName = "IMGB_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
                    _buildimgII.JpgTargetPath = @Directory.GetCurrentDirectory() + "\\img\\";
                    imgPath2 = _buildimgII.JpgTargetPath + _buildimgII.JpgTargetFileName + ".jpg";//makeImg(path, name);
                    _threadII.SetApartmentState(ApartmentState.STA);
                    _threadII.Start(_buildimgII);
                    //while (_threadII.ThreadState == ThreadState.Running) { }    //最早时使用waitone，现使用一个死循环
                    while (_threadII.IsAlive == true) { }

                    System.Net.Mail.Attachment attachment2 = new System.Net.Mail.Attachment(@imgPath2);
                    mailM.Attachments.Add(attachment2);

                    //邮件内容替换为图片
                    strBody = strBody.Replace("[picture2]", "<img src=\"cid:" + attachment2.ContentId + "\" />");
                }

                string imgPath3 = "";
                if (!string.IsNullOrEmpty(strAttPath3))
                {
                    ParameterizedThreadStart _paraThreadIII = new ParameterizedThreadStart(MakeJpgIII);
                    Thread _threadIII = new Thread(_paraThreadIII);
                    _threadIII.IsBackground = true;
                    BuildImg _buildimgIII = new BuildImg();
                    _buildimgIII.xlsFilePath = strAttPath3;
                    _buildimgIII.FromSheet = strSheetName3;
                    _buildimgIII.Index = iIndex3;
                    _buildimgIII.JpgTargetFileName = "IMGB_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
                    _buildimgIII.JpgTargetPath = @Directory.GetCurrentDirectory() + "\\img\\";
                    imgPath3 = _buildimgIII.JpgTargetPath + _buildimgIII.JpgTargetFileName + ".jpg";//makeImg(path, name);
                    _threadIII.SetApartmentState(ApartmentState.STA);
                    _threadIII.Start(_buildimgIII);
                    //while (_threadII.ThreadState == ThreadState.Running) { }    //最早时使用waitone，现使用一个死循环
                    while (_threadIII.IsAlive == true) { }

                    System.Net.Mail.Attachment attachment3 = new System.Net.Mail.Attachment(@imgPath3);
                    mailM.Attachments.Add(attachment3);

                    //邮件内容替换为图片
                    if (ImgWith > 0)
                    {
                        strBody = strBody.Replace("[picture3]", "<img width='" + ImgWith.ToString() + "' height='" + ImgHeight.ToString() + "' src=\"cid:" + attachment3.ContentId + "\" />");
                    }
                    else
                    {
                        strBody = strBody.Replace("[picture3]", "<img  src=\"cid:" + attachment3.ContentId + "\" />");
                    }

                }

                string imgPath4 = "";
                if (!string.IsNullOrEmpty(strAttPath4))
                {
                    ParameterizedThreadStart _paraThreadIV = new ParameterizedThreadStart(MakeJpgIII);
                    Thread _threadIV = new Thread(_paraThreadIV);
                    _threadIV.IsBackground = true;
                    BuildImg _buildimgIV = new BuildImg();
                    _buildimgIV.xlsFilePath = strAttPath4;
                    _buildimgIV.FromSheet = strSheetName4;
                    _buildimgIV.Index = iIndex4;
                    _buildimgIV.JpgTargetFileName = "IMGB_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
                    _buildimgIV.JpgTargetPath = @Directory.GetCurrentDirectory() + "\\img\\";
                    imgPath4 = _buildimgIV.JpgTargetPath + _buildimgIV.JpgTargetFileName + ".jpg";//makeImg(path, name);
                    _threadIV.SetApartmentState(ApartmentState.STA);
                    _threadIV.Start(_buildimgIV);
                    //while (_threadII.ThreadState == ThreadState.Running) { }    //最早时使用waitone，现使用一个死循环
                    while (_threadIV.IsAlive == true) { }

                    System.Net.Mail.Attachment attachment4 = new System.Net.Mail.Attachment(@imgPath4);
                    mailM.Attachments.Add(attachment4);

                    //邮件内容替换为图片
                    if (ImgWith > 0)
                    {
                        strBody = strBody.Replace("[picture4]", "<img width='" + ImgWith.ToString() + "' height='" + ImgHeight.ToString() + "' src=\"cid:" + attachment4.ContentId + "\" />");
                    }
                    else
                    {
                        strBody = strBody.Replace("[picture4]", "<img src=\"cid:" + attachment4.ContentId + "\" />");
                    }

                }

                mailM.Body = strBody;

                // 设置SMTP客户端
                SmtpClient smtp = ConfiguratSMTP();

                //发送
                smtp.Send(mailM);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }   
        }
        /// <summary>
        /// 生成图片 对指定的范围生成图片保存到其他地方
        /// </summary>
        /// <param name="para">这个参数需要BuildImg结构体列表</param>

        private void MakeJpg(object para)
        {
            BuildImg _buildimg = (BuildImg)para;

            int beginRow = _buildimg.BeginRow;              //拷贝开始行
            int beginColumn = _buildimg.BeginCol;           //拷贝开始列 
            int endRow = _buildimg.EndRow;                     //拷贝结束行
            int endColumn = _buildimg.EndCol;                  //拷贝结束列

            string imgName = _buildimg.JpgTargetFileName;

            System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();//引用Excel对象
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(_buildimg.xlsFilePath);
            excel.UserControl = true;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            excel.Visible = false;

            try
            {
                for (int i = 0; i < workbook.Worksheets.Count; i++)//循环取所有的Sheet.
                {
                    Microsoft.Office.Interop.Excel.Worksheet sheet = workbook.Worksheets.get_Item(i + 1) as Microsoft.Office.Interop.Excel.Worksheet;//从1开始.
                    if (sheet.Name.ToString() == _buildimg.FromSheet)
                    {
                        lock (this)
                        {
                            Microsoft.Office.Interop.Excel.Range cell = sheet.Range[sheet.Cells[beginRow, beginColumn], sheet.Cells[endRow, endColumn]];
                            //Clipboard.Clear();
                            cell.CopyPicture(Microsoft.Office.Interop.Excel.XlPictureAppearance.xlScreen, Microsoft.Office.Interop.Excel.XlCopyPictureFormat.xlBitmap);

                            IDataObject iData = Clipboard.GetDataObject();

                            if (iData != null && iData.GetDataPresent(DataFormats.Bitmap))
                            {
                                string path = _buildimg.JpgTargetPath;//@Directory.GetCurrentDirectory() + "\\img";

                                if (!System.IO.Directory.Exists(path))
                                {
                                    System.IO.Directory.CreateDirectory(path);
                                }

                                System.Drawing.Image img = Clipboard.GetImage();    //从内存中读取图片

                                if (img != null)
                                {
                                    img.Save(path + "\\" + imgName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                                }
                                img.Dispose();
                                img = null;
                                Clipboard.Clear();
                            }
                        }

                        //if (iData.GetDataPresent(DataFormats.Bitmap))
                        //{
                        //    Image image = (Bitmap)iData.GetData(DataFormats.Bitmap);    //从内存取值；
                        //    //Bitmap newBitmap = new Bitmap(image);
                        //    string path = _buildimg.JpgTargetPath;//@Directory.GetCurrentDirectory() + "\\img";

                        //    if (!System.IO.Directory.Exists(path))
                        //    {
                        //        System.IO.Directory.CreateDirectory(path);
                        //    }

                        //    try
                        //    {
                        //        image.Save(path + "\\" + imgName + ".jpg"); //保存。
                        //    }
                        //    catch
                        //    {
                        //    }
                        //    image.Dispose();
                        //    image = null;
                        //}
                    }
                }

            }
            catch (Exception )
            {
                //string TxtPath = AppDomain.CurrentDomain.BaseDirectory + "ImageGenetateFail.txt";
                //string FailInfo = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ":" + ex.Message.ToString();

                //FileStream fs = new FileStream(TxtPath,FileMode.Append);
                //StreamWriter sw = new StreamWriter(fs);
                //sw.WriteLine(FailInfo);

                //sw.Flush();
                //sw.Close();
            }
            finally
            {
                workbook.Close(false, null, null);
                excel.Quit();

                System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
            }

        }

        /// <summary>
        /// 生成图片 把Excel里的Shape保存到其他地方 第index张图片 
        /// </summary>
        /// <param name="para">这个参数需要BuildImg结构体列表</param>
        /// <remarks></remarks>
        private void MakeJpgII(object para)
        {
            BuildImg _buildimg = (BuildImg)para;

            string exclePath = _buildimg.xlsFilePath;
            string name = _buildimg.FromSheet;
            string imgName = _buildimg.JpgTargetFileName;

            System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            int index = _buildimg.Index;
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();//引用Excel对象
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(exclePath);
            excel.UserControl = true;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            excel.Visible = false;

            System.Drawing.Image img;

            try
            {
                for (int i = 0; i < workbook.Worksheets.Count; i++)//循环取所有的Sheet.
                {

                    Microsoft.Office.Interop.Excel.Worksheet sheet = workbook.Worksheets.get_Item(i + 1) as Microsoft.Office.Interop.Excel.Worksheet;//从1开始.
                    if (sheet.Name.ToString() == name)
                    {
                        lock (this)
                        {

                            Microsoft.Office.Interop.Excel.Shape s = sheet.Shapes.Item(index) as Microsoft.Office.Interop.Excel.Shape;
                            //Clipboard.Clear();
                            s.CopyPicture(Appearance.Button, Microsoft.Office.Interop.Excel.XlCopyPictureFormat.xlBitmap);　//COPY到内存。
                            IDataObject iData = Clipboard.GetDataObject();

                            if (iData != null && iData.GetDataPresent(DataFormats.Bitmap))
                            {
                                string path = _buildimg.JpgTargetPath;//@Directory.GetCurrentDirectory() + "\\img";

                                if (!System.IO.Directory.Exists(path))
                                {
                                    System.IO.Directory.CreateDirectory(path);
                                }
                                img = Clipboard.GetImage();    //从内存中读取图片

                                if (img != null)
                                {
                                    img.Save(path + "\\" + imgName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                                }
                                img.Dispose();
                                img = null;
                                Clipboard.Clear();
                            }
                        }


                        //if (iData.GetDataPresent(DataFormats.Bitmap))
                        //{
                        //    Image image = (Bitmap)iData.GetData(DataFormats.Bitmap);    //从内存取值；
                        //    string path = _buildimg.JpgTargetPath;//@Directory.GetCurrentDirectory() + "\\img";
                        //    if (!System.IO.Directory.Exists(path))
                        //    {
                        //        System.IO.Directory.CreateDirectory(path);
                        //    }
                        //    image.Save(path + "\\" + imgName + ".jpg"); //保存。
                        //}
                    }
                }
            }
            catch
            {
            }
            finally
            {
                workbook.Close(false, null, null);
                excel.Quit();

                System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
            }

        }

        /// <summary>
        /// 生成图片 把Excel里的chart保存到其他地方 第index张图片
        /// </summary>
        /// <param name="para">这个参数需要BuildImg结构体列表</param>
        /// <remarks>add by wells 2021-11-10(图片可以多格式)</remarks>
        private void MakeJpgIII(object para)
        {
            BuildImg _buildimg = (BuildImg)para;

            string exclePath = _buildimg.xlsFilePath;
            string name = _buildimg.FromSheet;
            string imgName = _buildimg.JpgTargetFileName;

            System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            int index = _buildimg.Index;
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();//引用Excel对象
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(exclePath);
            excel.UserControl = true;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            excel.Visible = false;

            try
            {
                for (int i = 0; i < workbook.Charts.Count; i++)//循环取所有的Charts.
                {
                    Microsoft.Office.Interop.Excel.Chart chart = workbook.Charts[i + 1] as Microsoft.Office.Interop.Excel.Chart;
                    //Excel.Chart chart = workbook.Sheets.Item[i + 1] as Excel.Chart;//从1开始.
                    if (chart != null && chart.Name.ToString() == name)
                    {
                        lock (this)
                        {

                            string path = _buildimg.JpgTargetPath;//@Directory.GetCurrentDirectory() + "\\img";

                            if (!System.IO.Directory.Exists(path))
                            {
                                System.IO.Directory.CreateDirectory(path);
                            }

                            chart.Export(path + "\\" + imgName + ".jpg", "jpg", false);
                        }
                    }
                }
            }
            catch (Exception )
            {
            }
            finally
            {
                workbook.Close(false, null, null);
                excel.Quit();

                System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
            }

        }

        /// <summary> 
        /// 发送邮件(正文只带一个表格图片)  
        /// Send email(the body only have one table pic)
        /// </summary>
        /// <param name="arrStrTO">收件人地址</param>
        /// <param name="arrStrCC">CC地址</param>
        /// <param name="strSubject">邮件标题</param>
        /// <param name="strBody">邮件内容</param>
        /// <param name="arrAttPath">附件文件地址</param>
        /// <param name="strAttPath">截图文件地址</param>
        /// <param name="strSheetName">截图Sheet名称</param>
        /// <param name="iRowColumn">截图坐标{BeginRow, BeginCol, EndRow, EndCol}</param>
        /// <returns>发送是否成功</returns>

        public string ToSendEmail(string arrStrTO, string arrStrCC, string strSubject, string strBody, string arrAttPath,
            string strAttPath, string strSheetName, int[] iRowColumn, int cycle, int incremental, int width, int RowIncremental)
        {
            try
            {
                //收件人地址
                if (arrStrTO != "" && arrStrTO.Length > 0)
                {
                    //拆分多个收件人
                    string[] StrEmailMTO = arrStrTO.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string strTO in StrEmailMTO)
                    {
                        try
                        {
                            if (strTO.Trim() != "")
                            {
                                mailM.To.Add(strTO);
                            }
                        }
                        catch { }
                    }
                }

                //CC人地址
                if (arrStrCC != null && arrStrCC.Length > 0)
                {
                    //拆分多个CC人
                    string[] StrEmailMCC = arrStrCC.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strCC in StrEmailMCC)
                    {
                        try
                        {
                            if (strCC.Trim() != "")
                            {
                                mailM.CC.Add(strCC);
                            }
                        }
                        catch { }
                    }
                }

                //邮件标题
                mailM.Subject = strSubject;

                //添加附件
                if (arrAttPath != null && arrAttPath.Length > 0)
                {
                    //拆分多个附件
                    string[] StrEmailMAtt = arrAttPath.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strAtt in StrEmailMAtt)
                    {
                        //添加附件
                        mailM.BodyEncoding = Encoding.GetEncoding("utf-8");
                        Attachment att = new Attachment(@strAtt);
                        mailM.Attachments.Add(att);
                    }
                }

                //邮件正文图片
                string imgPath = "";
                if (!string.IsNullOrEmpty(strAttPath))
                {
                    for (int i = 0; i < cycle; i++)
                    {
                        ParameterizedThreadStart _paraThread = new ParameterizedThreadStart(MakeJpg);
                        Thread _thread = new Thread(_paraThread);
                        _thread.IsBackground = true;
                        BuildImg _buildimg = new BuildImg();
                        _buildimg.xlsFilePath = strAttPath;
                        _buildimg.FromSheet = strSheetName;
                        _buildimg.BeginRow = iRowColumn[0];
                        _buildimg.BeginCol = iRowColumn[1];

                        _buildimg.EndRow = iRowColumn[2];

                        if (i == cycle - 1)
                        {
                            _buildimg.EndRow = iRowColumn[4];
                        }

                        _buildimg.EndCol = iRowColumn[3];

                        _buildimg.JpgTargetFileName = "IMGA_" + DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
                        _buildimg.JpgTargetPath = @Directory.GetCurrentDirectory() + "\\img\\";
                        imgPath = _buildimg.JpgTargetPath + _buildimg.JpgTargetFileName + ".jpg";
                        _thread.SetApartmentState(ApartmentState.STA);
                        _thread.Start(_buildimg);

                        //while (_thread.ThreadState == ThreadState.Running) { }
                        while (_thread.IsAlive == true) { }

                        //添加1张图片
                        System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(@imgPath);
                        mailM.Attachments.Add(attachment);

                        if (width > 0)
                        {
                            strBody = strBody.Replace("[picture" + i.ToString() + "]", "<img width='" + width.ToString() + "' src=\"cid:" + attachment.ContentId + "\" />");
                        }
                        else
                        {
                            //邮件内容替换为图片
                            strBody = strBody.Replace("[picture" + i.ToString() + "]", "<img src=\"cid:" + attachment.ContentId + "\" />");
                        }


                        iRowColumn[0] = iRowColumn[2] + RowIncremental;
                        iRowColumn[2] = iRowColumn[0] + incremental;
                    }
                }

                //邮件内容
                mailM.Body = strBody;

                // 设置SMTP客户端
                SmtpClient smtp = ConfiguratSMTP();

                //发送
                smtp.Send(mailM);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        /// <summary>
        /// 获取配置文件节点
        /// </summary>
        /// <param name="pKey"></param>
        /// <returns></returns>

        public string GetConfigKeyValue(string pKey)
        {
            string valueM = System.Configuration.ConfigurationManager.AppSettings[pKey];
            return valueM;
        }

        #region 解决附件为乱码的问题 
        public static string base64GbkDecode(string data)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(data);//先经过base64解码，在经过gbk2312解码
            try
            {
                decode = Encoding.GetEncoding("gb2312").GetString(bytes);
            }
            catch (Exception ex1)
            {
                return "Error in base64Encode" + ex1.Message;
            }
            return decode;
        }


        public static string base64Utf8Decode(string data)
        {
            string result = "";
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();//获取解码器
                byte[] todecode_byte = Convert.FromBase64String(data);//先经过base64解码，在经过utf-8解码
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);//多少个字符
                char[] decoded_char = new char[charCount];//字符字符数组
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);//解码到字符数组
                result = new String(decoded_char);
            }
            catch (Exception e)
            {
                return "Error in base64Encode" + e.Message;
            }
            return result;
        }


        //base64解码
        public static string DecodeStr(string allstr, string code)
        {
            //形如=?...?=是结束开始的标志
            //=?utf-8?B?5rWL6K+V5o6l5pS25pys6YKu5Lu26L+Z5piv5Li76aKY?=
            //=?gbk?B?suLK1L3TytXN4rK/08q8/tXiuPbKx9b3zOU=?=
            //返回的字符串
            string str = "";
            if (code == "gbk")
            {
                str = base64GbkDecode(allstr);
            }
            else if (code == "utf-8")
            {
                str = base64Utf8Decode(allstr);
            }
            return str;
        }

        #endregion

    }
}
