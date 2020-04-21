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
        private readonly CusRuleService cusRuleService = new CusRuleService();
        private readonly OracleQueryB oracleQueryB = new OracleQueryB();
        private readonly CodeRuleService codeRuleService = new CodeRuleService();

        public BoundRule()
        {
            InitializeComponent();
        }

        private void BoundRule_Load(object sender, EventArgs e)
        {

            this.comboBox1.DisplayMember = "CusName";
            this.comboBox1.ValueMember = "CusNo";
            this.comboBox1.DataSource = oracleQueryB.getCusInfo();



            this.comboBox4.DisplayMember = "RuleDesc";
            this.comboBox4.ValueMember = "Ruleno";
            this.comboBox4.DataSource = codeRuleService.queryRulesByRuleNo("");


            DataTable itemTable2 = new DataTable();   // construct selects value
            DataColumn columnType;
            DataRow rowType;
            columnType = new DataColumn("Name");
            itemTable2.Columns.Add(columnType);
            columnType = new DataColumn("Value");
            itemTable2.Columns.Add(columnType);
            rowType = itemTable2.NewRow();
            rowType["Name"] = "CT";
            rowType["Value"] = "0";
            itemTable2.Rows.Add(rowType);
            rowType = itemTable2.NewRow();
            rowType["Name"] = "裝箱";
            rowType["Value"] = "1";
            itemTable2.Rows.Add(rowType);
            rowType = itemTable2.NewRow();
            rowType["Name"] = "棧板";
            rowType["Value"] = "2";
            itemTable2.Rows.Add(rowType);
            this.comboBox3.DisplayMember = "Name";
            this.comboBox3.ValueMember = "Value";
            this.comboBox3.DataSource = itemTable2;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text == null || this.textBox1.Text.Trim() == "")
            {
                MessageBox.Show("出貨料號不能為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            string boundType = this.comboBox1.SelectedValue == null ? "0" : this.comboBox3.SelectedValue.ToString();
            CusRule cusRule = new CusRule();
            cusRule.Delmatno = this.textBox1.Text.Trim();
            cusRule.Cusno = this.comboBox1.SelectedValue.ToString().Trim();
            cusRule.Ruleno = this.comboBox4.SelectedValue.ToString().Trim();
            cusRule.Boundtype = boundType;
            if (cusRuleService.checkAdd(cusRule))
            {
                MessageBox.Show("該客戶和出貨料號已經綁定幾種！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cusRuleService.saveCusCodeRule(cusRule))
            {
                MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



    }
}
