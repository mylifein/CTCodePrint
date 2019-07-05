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
    public partial class RuleConfig : Form
    {
        public RuleConfig()
        {
            InitializeComponent();
        }

        private readonly SelectQuery selectQ = new SelectQuery();
        private readonly PrintModelQ printM = new PrintModelQ();
        private void button1_Click(object sender, EventArgs e)
        {
            string queryValue = this.textBox1.Text;
            CodeRule codeR = printM.queryCodeByNo(queryValue);
            DataSet ds = selectQ.getRulesByRuleNo(queryValue);
            if (codeR != null )
            {
                this.textBox2.Text = codeR.RuleDesc;
                this.textBox3.Text = codeR.Opuser;
                this.textBox4.Text = codeR.Createtime;
                this.dataGridView1.DataSource = codeR.RuleItem;


                //DGV 改變列名,列寬
                this.dataGridView1.Columns[0].Visible = false;
                this.dataGridView1.Columns[0].Width = 0;
                this.dataGridView1.Columns[1].HeaderText = "規則編號";
                this.dataGridView1.Columns[1].Width = 80;
                this.dataGridView1.Columns[2].HeaderText = "流水編號";
                this.dataGridView1.Columns[2].Width = 80;
                this.dataGridView1.Columns[3].HeaderText = "規則類型";
                this.dataGridView1.Columns[3].Width = 80;
                this.dataGridView1.Columns[4].HeaderText = "規則值";
                this.dataGridView1.Columns[4].Width = 80;
                this.dataGridView1.Columns[5].HeaderText = "規則長度";
                this.dataGridView1.Columns[5].Width = 80;
                this.dataGridView1.Columns[6].HeaderText = "操作用戶";
                this.dataGridView1.Columns[6].Width = 80;
                this.dataGridView1.Columns[7].HeaderText = "創建時間";
                this.dataGridView1.Columns[7].Width = 140;
                this.dataGridView1.Columns[8].HeaderText = "修改時間";
                this.dataGridView1.Columns[8].Width = 140;
                this.dataGridView1.Columns[9].Visible = false;
                this.dataGridView1.Columns[9].Width = 0;
            }
        }


    }
}
