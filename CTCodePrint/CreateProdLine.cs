using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CTCodePrint
{
    public partial class CreateProdLine : Form
    {
        public CreateProdLine()
        {
            InitializeComponent();
        }
        private readonly DepartmentService departmentService = new DepartmentService();
        private readonly ProdLineService prodLineService = new ProdLineService();

        private void MachineTypeConfig_Load(object sender, EventArgs e)
        {
       
            this.comboBox1.DataSource = departmentService.queryDepartmentList("");
            this.comboBox1.ValueMember = "DeptId";
            this.comboBox1.DisplayMember = "DeptName";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox2.Text == null || this.textBox2.Text.Trim() == "")
            {
                MessageBox.Show("線別名稱不能為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Focus();
                return;
            }
            if (this.textBox3.Text == null || this.textBox2.Text.Trim() == "")
            {
                MessageBox.Show("線別描述不能為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox3.Focus();
                return;
            }
            if (this.textBox1.Text != null && this.textBox1.Text.Trim() != "")
            {
                MessageBox.Show("線別已經保存,請勿重複保存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.textBox2.Text == null || this.textBox2.Text.Trim() == "")
            {
                MessageBox.Show("請維護線別名稱！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Focus();
            }
            ProdLine prodLine = new ProdLine();
            Department department = new Department();
            prodLine.ProdlineName = this.textBox2.Text.Trim();
            prodLine.ProdlineDesc = this.textBox3.Text.Trim();
            department.DeptId  = this.comboBox1.SelectedValue.ToString().Trim();
            prodLine.Department = department;
            ProdLine reProdLine = prodLineService.saveProdLine(prodLine);
            if (reProdLine != null)
            {
                this.textBox1.Text = reProdLine.ProdlineId;
                this.textBox4.Text = reProdLine.Createtime;
                MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("保存失敗，請維護線別描述信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Focus();
            }
        }
    }
}
