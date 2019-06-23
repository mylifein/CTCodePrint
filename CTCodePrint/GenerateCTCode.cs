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
        private GenerateCode generateC = new GenerateCode();

        private void GenerateCTCode_Load(object sender, EventArgs e)
        {

            //設置combox 初始值
            DataSet ds = selectQ.getCusSelect();
            DataTable itemTable = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) 
            {
                itemTable = ds.Tables[0];
            }
            this.comboBox1.DisplayMember = "cus_name";
            this.comboBox1.ValueMember = "cus_no";
            this.comboBox1.DataSource = itemTable;
           
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //獲得界面信息
            string workNo = this.textBox1.Text;
            string po = this.textBox2.Text;
            string deliveryMat = this.textBox3.Text;
            string cusNo = this.comboBox1.SelectedValue.ToString();
            string cusMatNo = this.textBox5.Text;
            string officialNo = this.textBox6.Text;
            string verNo = this.textBox7.Text;
            string macType = this.comboBox2.SelectedValue.ToString();
            this.textBox8.Text = generateC.generateCTNumber(po, cusNo, workNo, macType, cusMatNo, officialNo, verNo).ToString();


        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            string comboxValue = "";
            if(this.comboBox1.SelectedValue != null)
            {
                comboxValue = this.comboBox1.SelectedValue.ToString();
            }
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
