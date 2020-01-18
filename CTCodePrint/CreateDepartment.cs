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
    public partial class CreateDepartment : Form
    {
        public CreateDepartment()
        {
            InitializeComponent();
        }
        private readonly DepartmentService departmentService = new DepartmentService();

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text != null && this.textBox1.Text.Trim() != "")
            {
                MessageBox.Show("該部門已經保存，請勿重複保存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            if (this.textBox3.Text == null || this.textBox3.Text.Trim() == "")
            {
                MessageBox.Show("部門描述不能為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox3.Focus();
                return;
            }
            if (this.textBox6.Text == null || this.textBox6.Text.Trim() == "")
            {
                MessageBox.Show("部門名稱不能為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox6.Focus();
                return;
            }
            Department department = new Department();
            department.DeptName = this.textBox6.Text.Trim();
            department.DeptDesc = this.textBox3.Text.Trim();
            Department reDepartment = departmentService.saveDepartment(department);
            if (reDepartment != null)
            {
                MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Text = reDepartment.DeptId;
                this.textBox4.Text = reDepartment.Opuser;
                this.textBox5.Text = reDepartment.Createtime;
            }
            else
            {
                MessageBox.Show("保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
