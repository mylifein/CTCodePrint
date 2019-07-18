using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LabelManager2;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Windows.Forms;
using Model;

namespace CTCodePrint.common
{
    class BarCodePrint
    {
        ApplicationClass lbl;
        static BarCodePrint instance;
        private static PrintDocument fPrintDocument = new PrintDocument();

        //获取本机默认打印机名称
        public static String DefaultPrinter()
        {
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
        public string PreviewPrintBC(string templateFileName, PrintContent printContent, MandatoryField manF)
        {
            string filePath = null;
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
                filePath = "D:\\" + System.DateTime.Now.Year + System.DateTime.Now.Month + System.DateTime.Now.Day + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second + ".bmp";
                string st = doc.CopyImageToFile(12, "BMP", 90, 60, filePath);
                int Num = 1;                      //打印数量
                //doc.Printer.SwitchTo(DefaultPrinter());
                //doc.PrintLabel(1, 1, 1, Num, 1, "");
                //doc.PrintDocument(Num);           //打印               

            }
            catch (Exception ex)
            {
                                          //返回,後面代碼不執行
            }
            finally
            {
                doc.FormFeed();
            }
            return filePath;

        }


    }
}
