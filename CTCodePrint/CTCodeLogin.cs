using BLL;
using CTCodePrint.common;
using DBUtility;
using GenerateCTCode;
using Model;
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
        private readonly RoleRelUserService roleRelUserService = new RoleRelUserService();
        

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
            if(this.comboBox1.SelectedValue == null || this.comboBox1.SelectedValue.ToString().Trim() == "")
            {
                MessageBox.Show("角色不能為空，請重新確認用戶名", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            string ipAddress = LocalIP.GetLocalIP();
            userBLL.saveLoginInfo(username, ipAddress);
            Auxiliary.loginName = username;
            Auxiliary.RoleNo = this.comboBox1.SelectedValue.ToString().Trim();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Hide();

         
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if(this.textBox1.Text == null || this.textBox1.Text.Trim() == "")
            {
                MessageBox.Show("用戶名不能為空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            Model.User userEntity = userBLL.queryByUsername(this.textBox1.Text.Trim());
            if(userEntity != null)
            {
                List<RoleUnionUser> roleUnionUserList = roleRelUserService.queryRoleUionUserList(userEntity.Userid);
                if(roleUnionUserList != null)
                {
                    this.comboBox1.DataSource = roleUnionUserList;
                    this.comboBox1.DisplayMember = "Rolename";
                    this.comboBox1.ValueMember = "Roleno";
                }
                else
                {
                    MessageBox.Show("該用戶未分配角色，請聯繫管理員", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.textBox1.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("用戶名不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
        }
    }
}
