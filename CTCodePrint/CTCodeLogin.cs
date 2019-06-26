using CTCodePrint.common;
using DBUtility;
using GenerateCTCode;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CTCodePrint
{
    public partial class CTCodeLogin : Form
    {
        public CTCodeLogin()
        {
            InitializeComponent();
        }

        private readonly BLL.User userBLL = new BLL.User();

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = this.textBox1.Text.Trim();
            string password = this.textBox2.Text.Trim();
            if(username == "" || password == "")
            {
                MessageBox.Show("用戶名或密碼不能為空", "提示", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            if (!userBLL.checkLogin(username, password))
            {
                MessageBox.Show("用戶名或密碼錯誤", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Text = "";
                this.textBox2.Focus();
                return;
            }
            Auxiliary.loginName = username;
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Hide();

            //string templateFileName = System.IO.Directory.GetCurrentDirectory() + "\\SH17003H0161401-00CT.lab";            //“System.IO.Directory.GetCurrentDirectory”:获取当前应用程序的路径，最后不包含“\”；
            ////判斷文件存在否
            //if (!File.Exists(templateFileName))
            //{
            //    MessageBox.Show("打印模板文件不存在,無法打印外箱貼紙", "信息", MessageBoxButtons.OK,
            //        MessageBoxIcon.Exclamation);
            //    return ;
            //}
            //BarCodePrint bc = new BarCodePrint();
            //String[] sA = new String[27];
            //for (int c = 0; c < 27; c++)
            //{
            //    sA[c] = "";                             //為數組賦值為空串""
            //}
            //sA[0] = "123";
            //bc.PrintBC(templateFileName, sA);
            //string ctcode = new GenerateCode().generateCTNumber("PO123", "C001", "W001", "BU6", "CM001", "O001", "01").ToString();
         
        }
    }
}
