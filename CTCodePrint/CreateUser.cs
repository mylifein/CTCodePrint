using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CTCodePrint
{
    public partial class CreateUser : Form
    {
        public CreateUser()
        {
            InitializeComponent();
        }
        private readonly UserService userService = new UserService();
        private readonly DepartmentService departmentService = new DepartmentService();
        private readonly ProdLineService prodLineService = new ProdLineService();

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text != null && this.textBox1.Text.Trim() != "")
            {
                MessageBox.Show("該用户已經创建，請勿重複保存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            if (this.textBox2.Text == null || this.textBox2.Text.Trim() == "")
            {
                MessageBox.Show("用户名不能為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Focus();
                return;
            }
            if (this.textBox3.Text == null || this.textBox3.Text.Trim() == "")
            {
                MessageBox.Show("密码不能為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox3.Focus();
                return;
            }
            if (this.textBox6.Text == null || this.textBox6.Text.Trim() == "")
            {
                MessageBox.Show("确认密码不能為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox3.Focus();
                return;
            }
            if (this.textBox6.Text != this.textBox3.Text)
            {
                MessageBox.Show("两次密码不能一致！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox3.Focus();
                return;
            }
            if (userService.isExist(this.textBox2.Text.ToUpper().Trim()))
            {
                MessageBox.Show("该用户名已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Focus();
                return;
            }
            User user = new User();
            user.Username = this.textBox2.Text.ToUpper().Trim();
            user.Password = this.textBox3.Text.Trim();
            user.Userdesc = this.textBox7.Text.Trim();
         /*   user.Department = this.textBox8.Text.Trim()*/;
            user.ProdLine = this.comboBox2.SelectedValue == null ? "" : this.comboBox2.SelectedValue.ToString();
            User reUser= userService.saveUser(user);
            if (reUser != null)
            {
                MessageBox.Show("创建成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Text = reUser.Userid;
                this.textBox4.Text = reUser.Opuser;
                this.textBox5.Text = reUser.Createtime;
            }
            else
            {
                MessageBox.Show("保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }



        private void CreateUser_Load(object sender, EventArgs e)
        {
            //初始化 部門下拉列表;
            List<Department> listDepartment = departmentService.queryDepartmentList("");
            listDepartment.Insert(0,new Department("0","請選擇"));
            this.comboBox1.ValueMember = "DeptId";
            this.comboBox1.DisplayMember = "DeptName";
            this.comboBox1.DataSource = listDepartment;
            this.comboBox1.SelectedIndex = 0;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string deptId = this.comboBox1.SelectedValue.ToString();
            List<ProdLine> prodLineList = prodLineService.queryPLByDeptId(deptId);
            if(prodLineList != null)
            {
                this.comboBox2.ValueMember = "ProdlineId";
                this.comboBox2.DisplayMember = "ProdlineName";
                this.comboBox2.DataSource = prodLineList;
                this.comboBox2.SelectedIndex = 0;
            }else
            {
                this.comboBox2.DataSource = null;
            }
        }
    }
}
