using BLL;
using CTCodePrint.common;
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
    public partial class ReprintCT : Form
    {
        public ReprintCT()
        {
            InitializeComponent();
        }

        private readonly PrintModelQ printQ = new PrintModelQ();
        private readonly BarCodePrint barPrint = BarCodePrint.getInstance();
        private void ReprintCT_Load(object sender, EventArgs e)
        {
            DataTable itemTable = new DataTable();   // construct selects value
            DataColumn column;
            DataRow row;
            column = new DataColumn("Name");
            itemTable.Columns.Add(column);
            column = new DataColumn("Value");
            itemTable.Columns.Add(column);
            row = itemTable.NewRow();
            row["Name"] = "工單號";
            row["Value"] = "1";
            itemTable.Rows.Add(row);
            row = itemTable.NewRow();
            row["Name"] = "CT碼";
            row["Value"] = "2";
            itemTable.Rows.Add(row);
            this.comboBox1.DisplayMember = "Name";
            this.comboBox1.ValueMember = "Value";
            this.comboBox1.DataSource = itemTable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string queryValue = this.textBox1.Text;
            string queryCondi = this.comboBox1.SelectedValue == null ? "1" : this.comboBox1.SelectedValue.ToString();
            DataSet ds = printQ.getCTInfo(queryCondi, queryValue);
            if (ds != null && ds.Tables.Count > 0)
            {
                this.dataGridView1.DataSource = ds.Tables[0];


                //DGV 改變列名,列寬
                this.dataGridView1.Columns[0].Visible = false;
                this.dataGridView1.Columns[0].Width = 0;
                this.dataGridView1.Columns[1].HeaderText = "CT碼";
                this.dataGridView1.Columns[1].Width = 172;
                this.dataGridView1.Columns[2].HeaderText = "規則號";
                this.dataGridView1.Columns[2].Width = 172;
                this.dataGridView1.Columns[3].HeaderText = "工單號";
                this.dataGridView1.Columns[3].Width = 105;
                this.dataGridView1.Columns[4].HeaderText = "客戶號";
                this.dataGridView1.Columns[4].Width = 105;
                this.dataGridView1.Columns[5].HeaderText = "客戶PO";
                this.dataGridView1.Columns[5].Width = 112;
                this.dataGridView1.Columns[6].HeaderText = "客戶料號";
                this.dataGridView1.Columns[6].Width = 112;
                this.dataGridView1.Columns[7].HeaderText = "出貨料號";
                this.dataGridView1.Columns[7].Width = 112;
                this.dataGridView1.Columns[8].HeaderText = "正式編號";
                this.dataGridView1.Columns[8].Width = 112;
                this.dataGridView1.Columns[9].HeaderText = "版本號";
                this.dataGridView1.Columns[9].Width = 112;
                this.dataGridView1.Columns[10].HeaderText = "工單數量";
                this.dataGridView1.Columns[10].Width = 112;
                this.dataGridView1.Columns[11].Visible = false;
                this.dataGridView1.Columns[11].Width = 0;
                this.dataGridView1.Columns[12].HeaderText = "模板號";
                this.dataGridView1.Columns[12].Width = 112;
                this.dataGridView1.Columns[13].HeaderText = "創建時間";
                this.dataGridView1.Columns[13].Width = 112;
                this.dataGridView1.Columns[14].Visible = false;
                this.dataGridView1.Columns[14].Width = 0;
                this.dataGridView1.Columns[15].Visible = false;
                this.dataGridView1.Columns[15].Width = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(this.dataGridView1.CurrentRow == null )
            {
                MessageBox.Show("請輸入正確的查詢條件或請選中表中的行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            int index = this.dataGridView1.CurrentRow.Index;
            string modelNo = this.dataGridView1.Rows[index].Cells["model_no"].Value.ToString();

            DataSet ds = printQ.getModelInfo(modelNo);
            if (ds == null || ds.Tables[0].Rows.Count < 1)
            {
                MessageBox.Show("未找到該客戶出貨料號對應的打印模板信息，請維護相關信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string templateFile = System.IO.Directory.GetCurrentDirectory() + "\\" + ds.Tables[0].Rows[0]["model_name"].ToString();

            ///查詢該模板必填字段
            MandatoryField manF = printQ.getMandInfoByMod(modelNo);
            if (manF == null)
            {
                MessageBox.Show("未找到打印模板必填字段数据，请检查维护", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            PrintContent printContent = new PrintContent();
            PrintRecord record = new PrintRecord();
            printContent.CtCode = this.dataGridView1.Rows[index].Cells["ct_code"].Value.ToString();
            record.Ctcode = this.dataGridView1.Rows[index].Cells["ct_code"].Value.ToString(); 
            bool judge = barPrint.PrintBC(templateFile, printContent, manF);
            if (judge)
            {
                printQ.savePrintRecord(record);

            }
            else
            {
                MessageBox.Show("打印失敗請聯係管理員！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }
    }
}
