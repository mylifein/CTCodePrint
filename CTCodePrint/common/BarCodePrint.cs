using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LabelManager2;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Windows.Forms;
using Model;
using System.Reflection;
using static System.Drawing.Printing.PrinterSettings;

namespace CTCodePrint.common
{
    class BarCodePrint
    {
        ApplicationClass lbl;
        static BarCodePrint instance;
        private static PrintDocument fPrintDocument = new PrintDocument();
        PrinterSettings printerSettings = new PrinterSettings();

        //获取本机默认打印机名称
        public static String DefaultPrinter()
        {
            //StringCollection stringCollection = PrinterSettings.InstalledPrinters;
            //return stringCollection[2];
            return fPrintDocument.PrinterSettings.PrinterName;
        }

        public BarCodePrint()
        {
            lbl = new ApplicationClass();
        }

        public static BarCodePrint getInstance()
        {
            if (instance == null)
                instance = new BarCodePrint();
            return instance;
        }

        public void close()
        {
            if (lbl != null)
            {
                lbl.Quit();
            }
            killProcess();

        }

        void killProcess()
        {
            Process[] pro = Process.GetProcesses();//获取已开启的所有进程

            //遍历所有查找到的进程

            for (int i = 0; i < pro.Length; i++)
            {

                //判断此进程是否是要查找的进程
                if (pro[i].ProcessName.ToString().ToLower() == "lppa")
                {
                    pro[i].Kill();//结束进程
                }
            }
        }


        public bool PrintBC(string templateFileName,PrintContent printContent,MandatoryField manF)
        {

            //未加載模板文件或者模板發生變更時，重新加載新的模板
            if (lbl.Documents.Count == 0 || templateFileName.IndexOf(lbl.ActiveDocument.Name) == -1)
            {
                lbl.Documents.Open(templateFileName, false);// 调用设计好的label文件

            }
            Document doc = lbl.ActiveDocument;
            try
            {
                if (manF.Ctcodem == "0")
                {
                    doc.Variables.FormVariables.Item("ctcode").Value = printContent.CtCode;   //给参数传值             可以不傳參數
                }

                int Num = 1;                      //打印数量
                doc.Printer.SwitchTo(DefaultPrinter());
                doc.PrintLabel(1, 1, 1, Num, 1, "");
                //doc.PrintDocument(Num);           //打印               

            }
            catch (Exception ex)
            {
                return false;                          //返回,後面代碼不執行
            }
            finally
            {
                doc.FormFeed();
            }
            return true;

        }

        public bool PrintBCByModel(string templateFileName, CTCode ctCode, List<MandUnionFieldType> mandUnionFieldTypeList)
        {
            if(lbl == null)
            {
                lbl = new ApplicationClass();
            }
            //未加載模板文件或者模板發生變更時，重新加載新的模板
            if (lbl.Documents.Count == 0 || templateFileName.IndexOf(lbl.ActiveDocument.Name) == -1)
            {
                lbl.Documents.Open(templateFileName, false);// 调用设计好的label文件

            }
            Document doc = lbl.ActiveDocument;
            try
            {
                for (int i = 1; i <= doc.Variables.FormVariables.Count; i++)
                {
                    string variableName = doc.Variables.FormVariables.Item(i).Name.ToString();
                    foreach (MandUnionFieldType mandUnionFieldType in mandUnionFieldTypeList)
                    {
                        if (mandUnionFieldType.FieldName.ToUpper() == variableName.ToUpper())
                        {
                            bool judge = false;
                            PropertyInfo[] propertyInfoARR = ctCode.GetType().GetProperties();
                            foreach (PropertyInfo propertyInfo in propertyInfoARR)
                            {
                                if(propertyInfo.Name == mandUnionFieldType.FieldName)
                                {
                                    string entityValue = ctCode.GetType().GetProperty(propertyInfo.Name).GetValue(ctCode, null).ToString();
                                    doc.Variables.FormVariables.Item(i).Value = entityValue;
                                    judge = true;
                                    break;
                                }


                            }
                            if (!judge)
                            {
                                
                                doc.Variables.FormVariables.Item(i).Value = mandUnionFieldType.FieldValue;
                                break;                               
                            }
                        }
                    }
                }

                int Num = 1;                      //打印数量
                doc.Printer.SwitchTo(DefaultPrinter());
                doc.PrintLabel(1, 1, 1, Num, 1, "");
                //doc.PrintDocument(Num);           //打印               

            }
            catch (Exception ex)
            {
                return false;                          //返回,後面代碼不執行
            }
            finally
            {
                //內存釋放和回收
                doc.FormFeed();
                lbl.Documents.CloseAll();
                lbl.Quit();
                lbl = null;
                doc = null;
                GC.Collect(0);

            }
            return true;

        }


        public bool BactchPrintBCByModel(string templateFileName, List<CTCode> ctCodeList, List<MandUnionFieldType> mandUnionFieldTypeList,String printerName)
        {
            if (lbl == null)
            {
                lbl = new ApplicationClass();
            }
            //未加載模板文件或者模板發生變更時，重新加載新的模板
            if (lbl.Documents.Count == 0 || templateFileName.IndexOf(lbl.ActiveDocument.Name) == -1)
            {
                lbl.Documents.Open(templateFileName, false);// 调用设计好的label文件

            }
            Document doc = lbl.ActiveDocument;
            bool isFirstParam = false;
            Dictionary<int, string> dic1 = new Dictionary<int, string>();
            Dictionary<int, bool> isObjectDic = new Dictionary<int, bool>(); 
            try
            {
                foreach (CTCode ctcode in ctCodeList)
                {
                    if (!isFirstParam)          //判断是否第一次设置打印参数
                    {
                        for (int i = 1; i <= doc.Variables.FormVariables.Count; i++)
                        {
                            string variableName = doc.Variables.FormVariables.Item(i).Name.ToString();
                            foreach (MandUnionFieldType mandUnionFieldType in mandUnionFieldTypeList)
                            {
                                if (mandUnionFieldType.FieldName.ToUpper() == variableName.ToUpper())
                                {
                                    bool judge = false;
                                    PropertyInfo[] propertyInfoARR = ctcode.GetType().GetProperties();
                                    foreach (PropertyInfo propertyInfo in propertyInfoARR)
                                    {
                                        if (propertyInfo.Name == mandUnionFieldType.FieldName)
                                        {
                                            string entityValue = ctcode.GetType().GetProperty(propertyInfo.Name).GetValue(ctcode, null).ToString();
                                            doc.Variables.FormVariables.Item(i).Value = entityValue;
                                            judge = true;
                                            dic1.Add(i, propertyInfo.Name);
                                            isObjectDic.Add(i, true);
                                            break;
                                        }


                                    }
                                    if (!judge)
                                    {

                                        doc.Variables.FormVariables.Item(i).Value = mandUnionFieldType.FieldValue;
                                        dic1.Add(i, mandUnionFieldType.FieldValue);
                                        isObjectDic.Add(i, false);
                                        break;
                                    }
                                }
                            }
                        }
                        isFirstParam = true;
                    }
                    else
                    {
                        for (int i = 1; i <= doc.Variables.FormVariables.Count; i++)
                        {
                            if (isObjectDic[i])
                            {
                                doc.Variables.FormVariables.Item(i).Value = ctcode.GetType().GetProperty(dic1[i]).GetValue(ctcode, null).ToString();
                            }
                            else
                            {
                                doc.Variables.FormVariables.Item(i).Value = dic1[i];
                            }
                        }
                    }

                    //doc.Variables.FormVariables.Item(1).Value = ctcode.Delmatno;
                    //doc.Variables.FormVariables.Item(2).Value = ctcode.Ctcode;
                    int Num = 1;                      //打印数量
                    //doc.Printer.SwitchTo(DefaultPrinter());
                    doc.Printer.SwitchTo(printerName);  //設置打印機
                    doc.PrintLabel(1, 1, 1, Num, 1, "");
                }
                doc.FormFeed();
                //doc.PrintDocument(Num);           //打印               

            }
            catch (Exception ex)
            {
                return false;                          //返回,後面代碼不執行
            }
            finally
            {
                //內存釋放和回收
                lbl.Documents.CloseAll();
                lbl.Quit();
                lbl = null;
                doc = null;
                GC.Collect(0);

            }
            return true;

        }


        public string PreviewPrintBC(string templateFileName)
        {
            string filePath = null;
            if (lbl == null)
            {
                lbl = new ApplicationClass();
            }
            //未加載模板文件或者模板發生變更時，重新加載新的模板
            if (lbl.Documents.Count == 0 || templateFileName.IndexOf(lbl.ActiveDocument.Name) == -1)
            {
                lbl.Documents.Open(templateFileName, false);// 调用设计好的label文件

            }
            Document doc = lbl.ActiveDocument;
            try
            {
                filePath = "D:\\" + System.DateTime.Now.Year + System.DateTime.Now.Month + System.DateTime.Now.Day + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second + ".bmp";
                string st = doc.CopyImageToFile(12, "BMP", 90, 60, filePath);          
            }
            catch (Exception ex)
            {
                                          //返回,後面代碼不執行
            }
            finally
            {
                //釋放資源/並回收
                lbl.Documents.CloseAll();
                lbl.Quit();
                lbl = null;
                doc = null;
                GC.Collect(0);
            }
            return filePath;

        }


    }
}
