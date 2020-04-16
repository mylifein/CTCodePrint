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
    public partial class BoundCapacity : Form
    {
        private readonly OracleQueryB oracleQueryB = new OracleQueryB();
        private readonly CapacityService capacityService = new CapacityService();

        public BoundCapacity()
        {
            InitializeComponent();
        }

        private void BoundRule_Load(object sender, EventArgs e)
        {
            DataSet dsCus = oracleQueryB.getCusInfo();
            DataTable itemTable = null;
            if (dsCus != null && dsCus.Tables.Count > 0 && dsCus.Tables[0].Rows.Count > 0)
            {
                this.comboBox1.DisplayMember = "PARTY_NAME";
                this.comboBox1.ValueMember = "CUST_ACCOUNT_ID";
                this.comboBox1.DataSource = dsCus.Tables[0];
            }
               
            DataSet dsCapacity = capacityService.queryCapacityAll("");
            if (dsCapacity != null && dsCapacity.Tables.Count > 0 && dsCapacity.Tables[0].Rows.Count > 0)
            {
                this.comboBox2.DisplayMember = "capacity_desc";
                this.comboBox2.ValueMember = "capacity_no";
                this.comboBox2.DataSource = dsCapacity.Tables[0];
            }

            DataTable itemTable2 = new DataTable();   // construct selects value
            DataColumn columnType;
            DataRow rowType;
            columnType = new DataColumn("Name");
            itemTable2.Columns.Add(columnType);
            columnType = new DataColumn("Value");
            itemTable2.Columns.Add(columnType);
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
            if(this.textBox1.Text == null || this.textBox1.Text.Trim() == "")
            {
                MessageBox.Show("出貨料號不能為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            string boundType = this.comboBox1.SelectedValue == null ? "1" : this.comboBox3.SelectedValue.ToString();
            CapacityRelCus capacityRelCus = new CapacityRelCus();
            capacityRelCus.DelMatno = this.textBox1.Text.Trim();
            capacityRelCus.CusNo = this.comboBox1.SelectedValue.ToString().Trim();
            capacityRelCus.CapacityNo = this.comboBox2.SelectedValue.ToString().Trim();
            capacityRelCus.CapacityType = boundType;
            if (capacityService.exists(capacityRelCus))
            {
                MessageBox.Show("該客戶和出貨料號已經綁定容量！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (capacityService.saveCapacityRelCus(capacityRelCus))
            {
                MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }else
            {
                MessageBox.Show("保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
