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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void CreateRules_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("rule_type：", typeof(DataGridViewComboBoxColumn));
            
            ///dt.Columns.Add(new DataColumn("uuid：",typeof(string)));
            ///dt.Columns.Add(new DataColumn("rule_no：", typeof(string)));
            ///dt.Columns.Add(new DataColumn("seq_no：", typeof(string)));
            //dt.Columns.Add(new DataColumn("rule_type：", typeof(DataGridViewComboBoxColumn)));
            //dt.Columns.Add(new DataColumn("rule_value：", typeof(string)));
            //dt.Columns.Add(new DataColumn("rule_length：", typeof(string)));
            //dt.Columns.Add(new DataColumn("op_user：", typeof(string)));
            //dt.Columns.Add(new DataColumn("create_time：", typeof(string)));
            //dt.Columns.Add(new DataColumn("update_time：", typeof(string)));
            //dt.Columns.Add(new DataColumn("del_flag：", typeof(string)));
            this.dataGridView1.ColumnCount = 10;
            this.dataGridView1.ColumnHeadersVisible = true;
            this.dataGridView1.Columns[0].Name = "規則號";
            this.dataGridView1.Columns[0].Name = "規則號";
            this.dataGridView1.Columns[0].Name = "規則號";
            this.dataGridView1.Columns[0].Name = "規則號";
            this.dataGridView1.Columns[0].Name = "規則號";
            this.dataGridView1.Columns[0].Name = "規則號";
            this.dataGridView1.Columns[0].Name = "規則號";
            this.dataGridView1.Columns[0].Name = "規則號";
            this.dataGridView1.Columns[0].Name = "規則號";
            this.dataGridView1.Columns[0].Name = "規則號";
            this.dataGridView1.Columns[0].Name = "規則號";
            //this.dataGridView1.DataSource = dt;
            //this.dataGridView1.Columns[0].Visible = false;
            //this.dataGridView1.Columns[0].Width = 0;
            //this.dataGridView1.Columns[1].HeaderText = "規則編號";
            //this.dataGridView1.Columns[1].Width = 150;
            //this.dataGridView1.Columns[2].HeaderText = "序列號";
            //this.dataGridView1.Columns[2].Width = 172;
            //this.dataGridView1.Columns[3].HeaderText = "規則類型";
            //this.dataGridView1.Columns[3].Width = 105;
            //this.dataGridView1.Columns[4].HeaderText = "規則值";
            //this.dataGridView1.Columns[4].Width = 105;
            //this.dataGridView1.Columns[5].HeaderText = "規則長度";
            //this.dataGridView1.Columns[5].Width = 105;
            //this.dataGridView1.Columns[6].HeaderText = "操作用戶";
            //this.dataGridView1.Columns[6].Width = 105;
            //this.dataGridView1.Columns[7].HeaderText = "創建時間";
            //this.dataGridView1.Columns[7].Width = 105;
            //this.dataGridView1.Columns[8].HeaderText = "更新時間";
            //this.dataGridView1.Columns[8].Width = 112;
            //this.dataGridView1.Columns[9].Visible = false;
            //this.dataGridView1.Columns[9].Width = 0;

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
