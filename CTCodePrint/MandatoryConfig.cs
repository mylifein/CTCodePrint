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
            DataTable itemTable = new DataTable();   // construct selects value
            DataColumn column;
            DataRow row;
            column = new DataColumn("Name");
            itemTable.Columns.Add(column);
            column = new DataColumn("Value");
            itemTable.Columns.Add(column);
            row = itemTable.NewRow();
            row["Name"] = "打印";
            row["Value"] = "0";
            itemTable.Rows.Add(row);
            row = itemTable.NewRow();
            row["Name"] = "不打印";
            row["Value"] = "1";
            itemTable.Rows.Add(row);
            this.comboBox1.DisplayMember = "Name";
            this.comboBox1.ValueMember = "Value";
            this.comboBox1.DataSource = itemTable;

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
                manInfo.Ctcodem = this.comboBox1.SelectedValue.ToString();
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
