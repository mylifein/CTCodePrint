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
    public partial class CreateMenu : Form
    {
        public CreateMenu()
        {
            InitializeComponent();
        }

        private readonly MenuService menuService = new MenuService();

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text != null && this.textBox1.Text.Trim() != "")
            {
                MessageBox.Show("該菜單已經保存，請勿重複保存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            if (this.textBox2.Text == null || this.textBox2.Text.Trim() == "")
            {
                MessageBox.Show("菜單名稱不能為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Focus();
                return;
            }
            if (this.textBox3.Text == null || this.textBox3.Text.Trim() == "")
            {
                MessageBox.Show("菜單描述不能為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox3.Focus();
                return;
            }
            MenuInfo menuInfo = new MenuInfo();
            menuInfo.MenuName = this.textBox2.Text.Trim();
            menuInfo.MenuDescription = this.textBox3.Text.Trim();
            MenuInfo reMenuInfo = menuService.saveMenuInfo(menuInfo);
            if (reMenuInfo != null)
            {
                MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Text = reMenuInfo.MenuNo;
                this.textBox4.Text = reMenuInfo.Opuser;
                this.textBox5.Text = reMenuInfo.CreateTime;
            }
            else
            {
                MessageBox.Show("保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


    }
}
