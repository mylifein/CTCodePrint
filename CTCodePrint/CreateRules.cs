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
    public partial class CreateRules : Form
    {
        public CreateRules()
        {
            InitializeComponent();
        }
        private readonly PrintModelQ printQ = new PrintModelQ();

        private readonly SelectQuery selectQ = new SelectQuery();
        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Add();
        }

        private void CreateRules_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = "ID";
            column.DataPropertyName = "uuid";//对应数据源的字段
            column.HeaderText = "UUID";
            column.Width = 80;
            this.dataGridView1.Columns.Add(column);
            DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
            column1.Name = "ruleno";
            column1.DataPropertyName = "rule_no";//对应数据源的字段
            column1.HeaderText = "規則號";
            column1.Width = 80;
            this.dataGridView1.Columns.Add(column1);
            DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
            column2.Name = "seqno";
            column2.DataPropertyName = "seq_no";//对应数据源的字段
            column2.HeaderText = "序列號";
            column2.Width = 80;
            this.dataGridView1.Columns.Add(column2);

            DataGridViewComboBoxColumn column3 = new DataGridViewComboBoxColumn();
            column3.Name = "type_desc";
            column3.DataPropertyName = "type_no";//对应数据源的字段
            column3.DisplayMember = "type_desc";
            column3.ValueMember = "type_no";
            column3.HeaderText = "規則類型";
            column3.Width = 80;
            DataSet typesDs =selectQ.getAllRuleTypes();
            column3.DataSource = typesDs.Tables[0];
            this.dataGridView1.Columns.Add(column3);

            DataGridViewTextBoxColumn column4 = new DataGridViewTextBoxColumn();
            column4.Name = "rulevalue";
            column4.DataPropertyName = "rule_value";//对应数据源的字段
            column4.HeaderText = "規則值";
            column4.Width = 80;
            this.dataGridView1.Columns.Add(column4);

            DataGridViewTextBoxColumn column5 = new DataGridViewTextBoxColumn();
            column5.Name = "rulelength";
            column5.DataPropertyName = "rule_length";//对应数据源的字段
            column5.HeaderText = "規則長度";
            column5.Width = 80;
            this.dataGridView1.Columns.Add(column5);

            this.dataGridView1.Columns[0].ReadOnly = true;
            this.dataGridView1.Columns[1].ReadOnly = true;
        
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("請選中需要刪除的行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.dataGridView1.Rows.Remove(this.dataGridView1.CurrentRow);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.textBox2.Text == null || this.textBox2.Text.Trim() == "")
            {
                MessageBox.Show("規則描述不能為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Focus();
                return;
            }
            if(this.dataGridView1.RowCount == 0)
            {
                MessageBox.Show("規則不能為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.textBox1.Text != null && this.textBox1.Text.Trim() != "")
            {
                MessageBox.Show("該規則已經保存，請勿重複保存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CodeRule codeR = new CodeRule();
            codeR.RuleDesc = this.textBox2.Text;
            List<RuleItem> list = new List<RuleItem>();
            for (int i=0; i < this.dataGridView1.Rows.Count; i++)
            {
                RuleItem ruleItem = new RuleItem();
                ruleItem.Seqno = this.dataGridView1.Rows[i].Cells[2].Value == null ? "": this.dataGridView1.Rows[i].Cells[2].Value.ToString();
                ruleItem.Ruletype = this.dataGridView1.Rows[i].Cells[3].Value == null ? "":this.dataGridView1.Rows[i].Cells[3].Value.ToString();
                ruleItem.Rulevalue = this.dataGridView1.Rows[i].Cells[4].Value == null ? "" : this.dataGridView1.Rows[i].Cells[4].Value.ToString();
                ruleItem.Rulelength = int.Parse(this.dataGridView1.Rows[i].Cells[5].Value == null ? "0": this.dataGridView1.Rows[i].Cells[5].Value.ToString());
                list.Add(ruleItem);
            }
            codeR.RuleItem = list;
            string saveResult = printQ.saveRuleInfo(codeR);
            if (saveResult != null)
            {
                this.textBox1.Text = saveResult;
                MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("保存失敗！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
                
        }
    }
}
