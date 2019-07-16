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
    public partial class BoundRule : Form
    {
        private readonly PrintModelQ printQ = new PrintModelQ();
        private readonly SelectQuery selectQ = new SelectQuery(); 
        public BoundRule()
        {
            InitializeComponent();
        }

        private void BoundRule_Load(object sender, EventArgs e)
        {
            DataSet dsCus = selectQ.getCusSelect();
            DataTable itemTable = null;
            if (dsCus != null && dsCus.Tables.Count > 0 && dsCus.Tables[0].Rows.Count > 0)
            {
                this.comboBox1.DisplayMember = "cus_name";
                this.comboBox1.ValueMember = "cus_no";
                this.comboBox1.DataSource = dsCus.Tables[0];
            }
               
            DataSet dsRule = printQ.queryMacType("");
            if (dsRule != null && dsRule.Tables.Count > 0 && dsRule.Tables[0].Rows.Count > 0)
            {
                this.comboBox2.DisplayMember = "mactypename";
                this.comboBox2.ValueMember = "mactypeno";
                this.comboBox2.DataSource = dsRule.Tables[0];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.textBox1.Text == null || this.textBox1.Text.Trim() == "")
            {
                MessageBox.Show("規則已經綁定成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            CusRule cusRule = new CusRule();
            cusRule.Delmatno = this.textBox1.Text.Trim();
            cusRule.Cusno = this.comboBox1.SelectedValue.ToString();
            cusRule.Mactypeno = this.comboBox2.SelectedValue.ToString();
            if (printQ.saveCusCodeRule(cusRule))
            {
                MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }else
            {
                MessageBox.Show("保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
