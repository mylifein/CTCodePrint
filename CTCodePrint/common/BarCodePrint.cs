using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LabelManager2;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Windows.Forms;

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


        /*  要註冊32位的VB 6編寫的ActiveX DLL文件才行,但在64位的OS系統中,此DLL文件註冊后無法被調用,故用下面的方法.
        public bool PrintBC(string templateFileName, string[] BCArray)
        {
            //實際打印條碼
            //調用自定義類打印條碼
            try
            {
                LMCL_cgClass LMCL_cC = new LMCL_cgClass();


                LMCL_cC.bc_print24(templateFileName.Trim(), BCArray[0], BCArray[1], BCArray[2], BCArray[3], BCArray[4], BCArray[5],
                    BCArray[6], BCArray[7], BCArray[8], BCArray[9], BCArray[10], BCArray[11], BCArray[12], BCArray[13],
                        BCArray[14], BCArray[15], BCArray[16], BCArray[17], BCArray[18], BCArray[19], BCArray[20], BCArray[21],
                        BCArray[22], BCArray[23], BCArray[24], BCArray[25], BCArray[26], 1);

                


                //釋放 COM 對象實例,   在.NET环境(托管环境,Managed)中释放COM组件对象与释放.NET对象不同.            
                System.Runtime.InteropServices.Marshal.ReleaseComObject(LMCL_cC);

                //將對象變量實例毀掉.              
                LMCL_cC = null;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        */

        public bool PrintBC(string templateFileName, string[] BCArray)
        {
            //未加載模板文件或者模板發生變更時，重新加載新的模板
            if (lbl.Documents.Count == 0 || templateFileName.IndexOf(lbl.ActiveDocument.Name) == -1)
            {
                lbl.Documents.Open(templateFileName, false);// 调用设计好的label文件

            }
            Document doc = lbl.ActiveDocument;
            try
            {
                doc.Variables.FormVariables.Item("Var0").Value = BCArray[0].Trim();   //给参数传值             可以不傳參數


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

                /*
                lbl.Quit();                                         //退出
                //釋放 COM 對象實例
                System.Runtime.InteropServices.Marshal.ReleaseComObject(lbl);
                if (lbl != null)
                {                    
                    lbl = null;
                }
                */
            }


            return true;

        }
    }
}
