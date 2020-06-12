using BLL;
using CTCodePrint.common;
using DBUtility;
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
    public partial class ReprintPallet : Form
    {
        public ReprintPallet()
        {
            InitializeComponent();
        }

        private readonly BarCodePrint barPrint = BarCodePrint.getInstance();
        private readonly ModelInfoService modelInfoService = new ModelInfoService();                    //查詢模板內容並下載
        private readonly MandRelDelService mandRelDelService = new MandRelDelService();                 //查詢模板的字段規則
        private readonly ManRelFieldTypeService manRelFieldTypeService = new ManRelFieldTypeService();  //根據字段規則 查詢字段規則值
        private readonly PalletService palletService = new PalletService();

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
            row["Name"] = "栈板号";
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
            List<Pallet> pallets = palletService.getPalletsByCond(queryCondi, queryValue);
            if (pallets != null)
            {
                this.dataGridView1.DataSource = pallets;


                //DGV 改變列名,列寬
                this.dataGridView1.Columns[0].Visible = false;
                this.dataGridView1.Columns[0].Width = 0;
                this.dataGridView1.Columns[1].HeaderText = "栈板号";
                this.dataGridView1.Columns[1].Width = 150;
                this.dataGridView1.Columns[2].HeaderText = "栈板数量";
                this.dataGridView1.Columns[2].Width = 112;
                this.dataGridView1.Columns[3].Visible = false;
                this.dataGridView1.Columns[4].HeaderText = "出货料号";
                this.dataGridView1.Columns[4].Width = 112;
                this.dataGridView1.Columns[5].Visible = false;
                this.dataGridView1.Columns[6].HeaderText = "创建人";
                this.dataGridView1.Columns[6].Width = 112;         
                this.dataGridView1.Columns[7].Visible = false;
                this.dataGridView1.Columns[8].Visible = false;
                this.dataGridView1.Columns[9].Visible = false;
                this.dataGridView1.Columns[10].Visible = false;
                this.dataGridView1.Columns[11].Visible = false;
                this.dataGridView1.Columns[12].Visible = false;
                this.dataGridView1.Columns[13].HeaderText = "批次号";
                this.dataGridView1.Columns[13].Width = 112;
                this.dataGridView1.Columns[14].HeaderText = "客户编号";
                this.dataGridView1.Columns[14].Width = 112;
                this.dataGridView1.Columns[15].HeaderText = "客户名称";
                this.dataGridView1.Columns[15].Width = 112;     
                this.dataGridView1.Columns[16].Visible = false;
                this.dataGridView1.Columns[17].HeaderText = "工单号";
                this.dataGridView1.Columns[17].Width = 112;
                this.dataGridView1.Columns[18].Visible = false;
                this.dataGridView1.Columns[19].Visible = false;
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
            string modelNo = this.dataGridView1.Rows[index].Cells["Modelno"].Value.ToString();
            string cusNo = this.dataGridView1.Rows[index].Cells["Cusno"].Value.ToString();
            string delMatno = this.dataGridView1.Rows[index].Cells["Delmatno"].Value.ToString();
            //下載模板並預覽   1.查询模板是否存在， 若存在不下载  2.若不存在下载模板
            string filePath = modelInfoService.previewModelFile(modelNo);
            if (filePath == null)
            {
                MessageBox.Show("未找到該客戶出貨料號對應的打印模板信息，請維護相關信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //查詢打印模板的打印字段
            MandRelDel mandRelDel = mandRelDelService.queryManNoByDel(cusNo, delMatno, "2");
            if (mandRelDel == null)
            {
                MessageBox.Show("未找到該客戶出貨料號對應的打印字段規則信息，請維護相關信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            List<MandUnionFieldType> mandUnionFieldTypeList = manRelFieldTypeService.queryMandUnionFieldTypeList(mandRelDel.ManNo);
            if (mandUnionFieldTypeList == null)
            {
                MessageBox.Show("未找到該客戶出貨料號對應的打印字段規則信息，請維護相關信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string palletNo = this.dataGridView1.Rows[index].Cells["PalletNo"].Value.ToString();
            Pallet pallet = palletService.queryPalletByPalletNo(palletNo);
            bool judgePrint = barPrint.printPalletByModel(filePath, pallet, mandUnionFieldTypeList);
            if (judgePrint)
            {
                //printQ.savePrintRecord(ctcodeEntity);

            }
            else
            {
                MessageBox.Show("打印失敗請聯係管理員！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }

    }
}
