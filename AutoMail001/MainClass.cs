using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using DataAccess;
using Model;
using Org.BouncyCastle.Crypto;

namespace AutoMail001
{
    /// <summary>  
    /// 程序进入方法体  
    /// 条件:  
    /// 1.实现接口主方法,基类
    /// 2.命名此类为MainClass,  
    /// 3.编译此类的时候要把AutoMail001.dll的放在启动目录dll文件夹下  
    /// </summary>  
    public class MainClass : MdlBase,IMainMethod
    {
        /// <summary>  
        /// 实现接口主方法  
        /// </summary>  
        /// <returns>返回信息(运行成功就不需要值)</returns>  
        public  string MainMethod(string taskId, string toList, string ccList, string SecretKey, CancellationToken token)
        {




            #region 1.检查模板是否存在
            string strFileName = string.Empty;
            strFileName = AppDomain.CurrentDomain.BaseDirectory + "Template\\AutoMail001.xlsx";

            if (!File.Exists(strFileName))
            {
                //etc. 模板文件不存在
                return "The tempate is not exist：" + strFileName;
            }
            string ReportName = "报价Excel版本";
            string Subject = ReportName + "_" + taskId;
            string strSaveFileName = AppDomain.CurrentDomain.BaseDirectory + "OutputFile\\" + Subject + "_" + DateTime.Now.Date.ToString("ddMMyyyy") + ".xlsx";
            #endregion

            #region 2.获取数据            
            DataSet rptDS = new DataSet();
            rptDS = new DalAutoMail001().GetReportData(SecretKey);

            if (rptDS == null || rptDS.Tables.Count == 0 || rptDS.Tables[0].Rows.Count == 0)
            {
                return "No data";
            }
            #endregion

            #region 3.生成excel文件
            try
            {
               
                NPOI.XSSF.UserModel.XSSFWorkbook resultIWK;
                FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read);
                using (file)
                {
                   //NPOI.SS.UserModel.WorkbookFactory.Create(file);

                    resultIWK = NPOI.SS.UserModel.WorkbookFactory.Create(file) as NPOI.XSSF.UserModel.XSSFWorkbook;
                };

                #region 导出
                //填充哪个报表
                int _iSheet = 1;
                //Excel填入内容
                resultIWK.SetActiveSheet(_iSheet);
                NPOI.XSSF.UserModel.XSSFSheet _xsheet = resultIWK.GetSheetAt(_iSheet) as NPOI.XSSF.UserModel.XSSFSheet;
                NPOI.SS.UserModel.ISheet iST = resultIWK.GetSheet(_xsheet.SheetName.ToString());

                NPOI.SS.UserModel.IDataFormat formatNumber = resultIWK.CreateDataFormat();
                NPOI.SS.UserModel.IFont _IFontText = resultIWK.CreateFont();
                _IFontText.IsBold = true;

                NPOI.SS.UserModel.ICellStyle _ICellEditNums = resultIWK.CreateCellStyle();
                _ICellEditNums.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                _ICellEditNums.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                _ICellEditNums.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                _ICellEditNums.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                _ICellEditNums.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                _ICellEditNums.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                _ICellEditNums.SetFont(_IFontText);
                _ICellEditNums.DataFormat = formatNumber.GetFormat("0.00");
                _ICellEditNums.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;
                _ICellEditNums.FillForegroundColor = 0;
                ((NPOI.XSSF.UserModel.XSSFColor)_ICellEditNums.FillForegroundColorColor).SetRgb(new byte[] { 255, 255, 0 });

                for (int iSheetNums = 0; iSheetNums <= 9; iSheetNums++)
                {
                    _iSheet = iSheetNums + 1;
                    resultIWK.SetActiveSheet(_iSheet);
                    _xsheet = resultIWK.GetSheetAt(_iSheet) as NPOI.XSSF.UserModel.XSSFSheet;

                    int _currStyleType = 0;
                    _currStyleType = Convert.ToInt32(_xsheet.SheetName.ToString().Split('-')[0]);
                    int _currRowIdx = 2;
                    int _currColIdx_YDZS = 3;   //B1、D
                    int _currColIdx_YDCL = 13;   //L11、N
                    int _currColIdx_YDVL = 23;   //V21、X
                    string _OPCode = "";
                    DataView dv = rptDS.Tables[0].AsDataView();
                    while (true)
                    {
                        //获取OPCode
                        _OPCode = _xsheet.GetRow(_currRowIdx).GetCell(1).StringCellValue;

                        if (_OPCode == "" && _xsheet.GetRow(_currRowIdx + 1).GetCell(1).StringCellValue == "")
                        {
                            break;
                        }

                        if (_OPCode == "")
                        {
                            _currRowIdx++;
                            continue;
                        }

                        //dv.RowFilter = "Substring(OpCode,3,6) = Substring('" + _OPCode + "',2,6) and StyleCode = '" + _currStyleType + "'";
                        dv.RowFilter = "Substring(OpCode,3,6) = '" + _OPCode + "' and StyleCode = '" + _currStyleType + "'";
                        if (dv.Count > 0)
                        {
                            for (int i = 0; i < dv.Count; i++)
                            {
                                int _currColIndx = -1;
                                if (dv[i]["FactoryID"].ToString() == "1")
                                {
                                    _currColIndx = _currColIdx_YDZS;
                                }
                                else if (dv[i]["FactoryID"].ToString() == "2")
                                {
                                    _currColIndx = _currColIdx_YDCL;
                                }
                                else if (dv[i]["FactoryID"].ToString() == "4")
                                {
                                    _currColIndx = _currColIdx_YDVL;
                                }

                                if (_currColIndx == -1)
                                {
                                    continue;
                                }

                                _xsheet.GetRow(133).GetCell(_currColIndx - 3).SetCellValue("本单使用汇率:" + rptDS.Tables[1].Rows[0][0].ToString());

                                if ( dv[i]["LaserOP"].ToString() == "1")
                                {
                                    string _cellfI = dv[i]["UnitPrice"].ToString() + "*" + ToName(_currColIndx + 1) + (_currRowIdx + 1).ToString();
                                    _xsheet.GetRow(_currRowIdx).GetCell(_currColIndx).SetCellFormula(_cellfI);
                                    _xsheet.GetRow(_currRowIdx).GetCell(_currColIndx + 1).CellStyle = _ICellEditNums;
                                }
                                else
                                {
                                    _xsheet.GetRow(_currRowIdx).GetCell(_currColIndx).SetCellValue(Double.Parse(dv[i]["UnitPrice"].ToString()));
                                }
                            }
                        }

                        _currRowIdx++;
                    }
                }
                #endregion
                resultIWK.SetActiveSheet(1);
                _xsheet.ForceFormulaRecalculation = true;
                resultIWK.GetCreationHelper().CreateFormulaEvaluator().EvaluateAll();

                //保存
           
                    using (FileStream fs = new FileStream(strSaveFileName, FileMode.Create, FileAccess.Write))
                    {
                        resultIWK.Write(fs);
                    }
                

            
            }
            catch (Exception ex)
            {
                //etc.导出报表错误
                return "Failed to generate Excel file " + ex.Message.ToString();
            }
            #endregion

            #region 4.发送邮件处理
            //接收人地址
            string EmailToList = "";
            string EmailCcList = "";
 

            EmailToList = toList;
            EmailCcList = ccList;

            

            //构建邮件html正文
            string strBody = "<span style='font-family: Calibri;font-size: 11pt;color: black;'>Dear All:</span><br/><br/><span style='font-family: Calibri; font-size: 11pt;'> " +
                "Enclosed please find the " + Subject + " for your kindly reference, thanks!<br/><br/><p><div><img src='cid:AutoMail001_1' /><img src='cid:AutoMail001_2' /><div/><p/>";
           
            //构建邮件正文图片
            string autoMail001_1 = AppDomain.CurrentDomain.BaseDirectory + "Template\\AutoMail001_1.PNG";
            string autoMail001_2 = AppDomain.CurrentDomain.BaseDirectory + "Template\\AutoMail001_2.PNG";
            string imageType = "image/PNG";
            Dictionary<string, string> images = new Dictionary<string, string>
            {
                {"AutoMail001_1", autoMail001_1},
                {"AutoMail001_2", autoMail001_2}
            };

            //发送邮件前需要检查任务是否被取消了
            string res = base.CheckTaskCancelToken(token);
            if (string.IsNullOrEmpty(res))
            {
                
                return new MailHelper().ToSendEmail(toList, ccList, Subject, strBody, strSaveFileName, images, imageType);
            }
            else
            {
                return res;
            }
            #endregion
        }






        private string ToName(int index)
        {
            if (index < 0) { throw new Exception("invalid parameter"); }
            List<string> chars = new List<string>();
            do
            {
                if (chars.Count > 0)
                    index--;
                chars.Insert(0, ((char)(index % 26 + (int)'A')).ToString());
                index = (int)((index - index % 26) / 26);
            } while (index > 0);
            return String.Join(string.Empty, chars.ToArray());
        }

    }
}
