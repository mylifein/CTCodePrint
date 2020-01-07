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
    public partial class SubmatMaintain : Form
    {
        private readonly SubMatInfoService subMatInfoService = new SubMatInfoService();

        public SubmatMaintain()
        {
            InitializeComponent();
        }

        private void BoundRule_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.textBox2.Text == null || this.textBox2.Text.Trim() == "")
            {
                MessageBox.Show("出貨料號不能為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            if (this.textBox1.Text == null || this.textBox1.Text.Trim() == "")
            {
                MessageBox.Show("子階料號不能為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            SubMatInfo subMatInfo = new SubMatInfo();
            subMatInfo.Delmatno = this.textBox2.Text.Trim();
            subMatInfo.Submatno = this.textBox1.Text.Trim();

            if (subMatInfoService.checkAdd(subMatInfo))
            {
                MessageBox.Show("該出貨料號和子階料號關係已經綁定！請勿重複綁定", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SubMatInfo reSubMatInfo = subMatInfoService.saveSubMatInfoDao(subMatInfo);
            if (reSubMatInfo != null)
            {
                this.textBox3.Text = reSubMatInfo.Opuser;
                this.textBox4.Text = reSubMatInfo.Createtime;
                MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }else
            {
                MessageBox.Show("保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
