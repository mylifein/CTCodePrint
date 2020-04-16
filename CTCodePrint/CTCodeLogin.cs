using BLL;
using CTCodePrint.common;
using DAL;
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
using System.Reflection;
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
        public static string user = "";
        public string User { get { return user;}set { user = value; } }
        private readonly BLL.UserService userBLL = new BLL.UserService();
        private readonly RoleRelUserService roleRelUserService = new RoleRelUserService();
        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // System.DateTime currentTime = System.DateTime.Now;                      //獲取當前時間
            //DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2020, 3, 9));
            //String Month = dtStart.ToString("dd");
            //int month = currentTime.Month;

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
            user = textBox1.Text.Trim();
            string ipAddress = LocalIP.GetLocalIP();
            userBLL.saveLoginInfo(username, ipAddress);
            Auxiliary.loginName = username;
            Auxiliary.RoleNo = this.comboBox1.SelectedValue.ToString().Trim();
            MainMenuMDI mainMenuMDI = new MainMenuMDI();
            mainMenuMDI.Show();
            this.Hide();

         
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            //测试产生箱号：
            /** Carton carton = new Carton();
            ProdLine prodLine = new ProdLine();
            prodLine.ProdlineId = "PL001";
            carton.ProdLine = prodLine;
            carton.Delmatno = "383-19855-3015A0";
            carton.Ruleno = "R007";
            Carton cartonNo = GenerateCarton.generateCartonNo(carton);  **/
            //SelectQuery selectQ = new SelectQuery();
            //PrintModelQ printQ = new PrintModelQ();
            //printQ.queryMacType("MT005");
            //DepartmentService depService = new DepartmentService();
            //List<Department> depList =  depService.queryDepartmentList("");
            // DataSet ds = selectQ.getRulesByNo("");
            //MacTypeDao macDao = new MacTypeDao();
           // macDao.queryDepartmentById("MT005");
            if (this.textBox1.Text == null || this.textBox1.Text.Trim() == "")
            {
                MessageBox.Show("用戶名不能為空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            Model.User userEntity = userBLL.queryByUsername(this.textBox1.Text.Trim());


            if (userEntity != null)
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
