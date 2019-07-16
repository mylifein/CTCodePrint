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
    public partial class MachineTypeConfig : Form
    {
        public MachineTypeConfig()
        {
            InitializeComponent();
        }
        private readonly PrintModelQ printQ = new PrintModelQ();

        private void MachineTypeConfig_Load(object sender, EventArgs e)
        {
            DataSet dsRule = printQ.queryCodeInfo("");
            if (dsRule != null && dsRule.Tables.Count > 0 && dsRule.Tables[0].Rows.Count > 0)
            {
                this.comboBox1.DisplayMember = "rule_desc";
                this.comboBox1.ValueMember = "rule_no";
                this.comboBox1.DataSource = dsRule.Tables[0];
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text != null && this.textBox1.Text.Trim() != "")
            {
                MessageBox.Show("改機種類型已經保存,請勿重複保存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.textBox2.Text == null || this.textBox2.Text.Trim() == "")
            {
                MessageBox.Show("請維護機種類型名稱！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Focus();
            }
            MacTypeInfo mactypeinfo = new MacTypeInfo();
            mactypeinfo.Mactypename = this.textBox2.Text.Trim();
            mactypeinfo.Mactypedesc = this.textBox3.Text.Trim();
            mactypeinfo.Ruleno = this.comboBox1.SelectedValue.ToString();
            MacTypeInfo remactype = printQ.saveMacTypeInfo(mactypeinfo);
            if (remactype != null)
            {
                this.textBox1.Text = remactype.Mactypeno;
                this.textBox4.Text = remactype.Createtime;
                MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("保存失敗，請維護機種類型描述信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Focus();
            }
        }
    }
}
