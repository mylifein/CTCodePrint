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
    public partial class MandatoryConfig : Form
    {
        public MandatoryConfig()
        {
            InitializeComponent();
        }

        private readonly PrintModelQ printQ = new PrintModelQ();
        private void MandatoryConfig_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox2.Text == null || this.textBox2.Text.Trim() == "")
            {
                MessageBox.Show("必填描述不能為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Focus();
                return;
            }
            if (this.textBox1.Text != null && this.textBox1.Text != "")
            {
                MessageBox.Show("该模板必填规则已经保存！請勿重複保存", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }else
            {
                MandatoryInfo manInfo = new MandatoryInfo();
                manInfo.Mandesc = this.textBox2.Text == null ? "" : this.textBox2.Text.Trim();
                MandatoryInfo reMandatory = printQ.saveManField(manInfo);
                if (reMandatory != null)
                {
                    this.textBox1.Text = reMandatory.Manno;
                    this.textBox3.Text = reMandatory.Opuser;
                    this.textBox4.Text = reMandatory.Createtime;
                    MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }else
                {
                    MessageBox.Show("保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
