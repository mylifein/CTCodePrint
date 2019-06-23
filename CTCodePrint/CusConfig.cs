using BLL;
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
    public partial class CusConfig : Form
    {
        private readonly SelectQuery selectQ = new SelectQuery();
        public CusConfig()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string queryValue = this.textBox1.Text;
            string queryCondi = this.comboBox1.SelectedValue == null ? "1" : this.comboBox1.SelectedValue.ToString();
            DataSet ds = selectQ.getCusByCondition(queryCondi, queryValue);
            if(ds != null && ds.Tables.Count > 0)
            {
                this.dataGridView1.DataSource = ds.Tables[0];


                //DGV 改變列名,列寬
                this.dataGridView1.Columns[0].Visible = false;
                this.dataGridView1.Columns[0].Width = 0;
                this.dataGridView1.Columns[1].HeaderText = "客戶編號";
                this.dataGridView1.Columns[1].Width = 172;
                this.dataGridView1.Columns[2].HeaderText = "客戶名稱";
                this.dataGridView1.Columns[2].Width = 172;
                this.dataGridView1.Columns[3].HeaderText = "操作用戶";
                this.dataGridView1.Columns[3].Width = 105;
                this.dataGridView1.Columns[4].HeaderText = "創建時間";
                this.dataGridView1.Columns[4].Width = 105;
                this.dataGridView1.Columns[5].HeaderText = "更新時間";
                this.dataGridView1.Columns[5].Width = 112;
                this.dataGridView1.Columns[6].Visible = false;
                this.dataGridView1.Columns[6].Width = 0;
            }

        }

        private void CusConfig_Load(object sender, EventArgs e)
        {
            DataTable itemTable = new DataTable();   // construct selects value
            DataColumn column;
            DataRow row;
            column = new DataColumn("Name");
            itemTable.Columns.Add(column);
            column = new DataColumn("Value");
            itemTable.Columns.Add(column);
            row = itemTable.NewRow();
            row["Name"] = "客戶編號";
            row["Value"] = "1";
            itemTable.Rows.Add(row);
            row = itemTable.NewRow();
            row["Name"] = "客戶名稱";
            row["Value"] = "2";
            itemTable.Rows.Add(row);
            this.comboBox1.DisplayMember = "Name";
            this.comboBox1.ValueMember = "Value";
            this.comboBox1.DataSource = itemTable;

        }
    }
}
