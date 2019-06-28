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
            string ipAddress = LocalIP.GetLocalIP();
            userBLL.saveLoginInfo(username, ipAddress);
            Auxiliary.loginName = username;
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Hide();

         
        }
    }
}
