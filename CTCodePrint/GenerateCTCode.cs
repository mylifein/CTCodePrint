using BLL;
using GenerateCTCode;
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
    public partial class GenerateCTCode : Form
    {
        public GenerateCTCode()
        {
            InitializeComponent();
        }
        private readonly SelectQuery selectQ = new SelectQuery();
        private readonly OracleQueryB queryB = new OracleQueryB();
        private GenerateCode generateC = new GenerateCode();

        private void GenerateCTCode_Load(object sender, EventArgs e)
        {
           
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //獲得界面信息
            string workNo = this.textBox1.Text;
            string po = this.textBox2.Text;
            string cusNo = this.textBox11.Text;
            string officialNo = this.textBox6.Text;
            string macType = this.comboBox2.SelectedValue.ToString();


        }



        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            string workno = this.textBox1.Text;
            DataSet ds = queryB.getWorkInfoByNo(workno);
            if(ds != null && ds.Tables.Count > 0 )
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                this.textBox2.Text = ds.Tables[0].Rows[0]["CUST_PO_NUMBER"].ToString();     //PO
                this.textBox11.Text = ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString();      //customer number 
                this.textBox10.Text = ds.Tables[0].Rows[0]["CUST_NAME"].ToString();     //customer name
                this.textBox4.Text = ds.Tables[0].Rows[0]["START_QUANTITY"].ToString();      //work number quantity
                this.textBox9.Text = ds.Tables[0].Rows[0]["QUANTITY_COMPLETED"].ToString();      //completed quantity
                this.textBox3.Text = ds.Tables[0].Rows[0]["ITEM_CODE"].ToString();          //delivery code
                }
            }
            ds = queryB.getCusMatInfo(workno);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.comboBox4.DisplayMember = "CUSTOMER_ITEM_DESC";
                    this.comboBox4.ValueMember = "CUSTOMER_ITEM_NUMBER";
                    this.comboBox4.DataSource = ds.Tables[0];
                }
            }
            ds = queryB.getRevisionInfo(workno);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.comboBox3.DisplayMember = "ITEM_DESC";
                    this.comboBox3.ValueMember = "ITEM_CODE";
                    this.comboBox3.DataSource = ds.Tables[0];
                }
            }

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            string comboxValue = this.textBox11.Text;
            DataSet ds = selectQ.getMacByCus(comboxValue);
            DataTable itemTable = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                itemTable = ds.Tables[0];
            }
            this.comboBox2.DisplayMember = "cus_mactype";
            this.comboBox2.ValueMember = "cus_mactype";
            this.comboBox2.DataSource = itemTable;
        }
    }
}
