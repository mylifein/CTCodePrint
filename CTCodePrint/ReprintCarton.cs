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
    public partial class ReprintCarton : Form
    {
        public ReprintCarton()
        {
            InitializeComponent();
        }

        private readonly PrintModelQ printQ = new PrintModelQ();
        private readonly BarCodePrint barPrint = BarCodePrint.getInstance();
        private readonly CTCodeService cTCodeService = new CTCodeService();
        private readonly ModelInfoService modelInfoService = new ModelInfoService();                    //查詢模板內容並下載
        private readonly MandRelDelService mandRelDelService = new MandRelDelService();                 //查詢模板的字段規則
        private readonly ManRelFieldTypeService manRelFieldTypeService = new ManRelFieldTypeService();  //根據字段規則 查詢字段規則值
        CartonService cartonService = new CartonService();

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
            row["Name"] = "裝箱單";
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
            List<Carton> cartons = cartonService.getCartonsInfo(queryCondi, queryValue);
            if (cartons != null)
            {
                this.dataGridView1.DataSource = cartons;


                //DGV 改變列名,列寬
                this.dataGridView1.Columns[0].Visible = false;
                this.dataGridView1.Columns[0].Width = 0;
                this.dataGridView1.Columns[1].HeaderText = "箱号";
                this.dataGridView1.Columns[1].Width = 150;
                this.dataGridView1.Columns[2].HeaderText = "装箱数量";
                this.dataGridView1.Columns[2].Width = 112;
                this.dataGridView1.Columns[3].Visible = false;
                this.dataGridView1.Columns[4].Visible = false;
                this.dataGridView1.Columns[5].Visible = false;
                this.dataGridView1.Columns[6].HeaderText = "工单号";
                this.dataGridView1.Columns[6].Width = 112;
                this.dataGridView1.Columns[7].Visible = false;
                this.dataGridView1.Columns[8].HeaderText = "客户PO";
                this.dataGridView1.Columns[8].Width = 112;
                this.dataGridView1.Columns[9].Visible = false;
                this.dataGridView1.Columns[10].HeaderText = "客户料号";
                this.dataGridView1.Columns[10].Width = 112;
                this.dataGridView1.Columns[11].HeaderText = "出货料号";
                this.dataGridView1.Columns[11].Width = 112;
                this.dataGridView1.Columns[12].Visible = false;
                this.dataGridView1.Columns[13].Visible = false;
                this.dataGridView1.Columns[14].HeaderText = "工单数量";
                this.dataGridView1.Columns[14].Width = 112;
                this.dataGridView1.Columns[15].Visible = false;
                this.dataGridView1.Columns[16].Visible = false;
                this.dataGridView1.Columns[17].Visible = false;
                this.dataGridView1.Columns[18].Visible = false;
                this.dataGridView1.Columns[19].Visible = false;
                this.dataGridView1.Columns[20].Visible = false;
                this.dataGridView1.Columns[21].Visible = false;
                this.dataGridView1.Columns[22].Visible = false;
                this.dataGridView1.Columns[23].Visible = false;
                this.dataGridView1.Columns[24].Visible = false;
                this.dataGridView1.Columns[25].Visible = false;
                this.dataGridView1.Columns[26].Visible = false;
                this.dataGridView1.Columns[27].Visible = false;
                this.dataGridView1.Columns[28].Visible = false;
                this.dataGridView1.Columns[29].Visible = false;
                this.dataGridView1.Columns[30].Visible = false;
                this.dataGridView1.Columns[31].Visible = false;
                this.dataGridView1.Columns[32].Visible = false;
                this.dataGridView1.Columns[33].HeaderText = "客户名称";
                this.dataGridView1.Columns[33].Width = 112;
                this.dataGridView1.Columns[34].Visible = false;
                this.dataGridView1.Columns[35].Visible = false;
                this.dataGridView1.Columns[36].Visible = false;
                this.dataGridView1.Columns[37].HeaderText = "批次号";
                this.dataGridView1.Columns[37].Width = 112;
                this.dataGridView1.Columns[38].HeaderText = "工单箱数";
                this.dataGridView1.Columns[38].Width = 112;
                this.dataGridView1.Columns[39].Visible = false;
                this.dataGridView1.Columns[40].Visible = false;
                this.dataGridView1.Columns[41].Visible = false;
                this.dataGridView1.Columns[42].Visible = false;
                this.dataGridView1.Columns[43].Visible = false;
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
            string filePath = modelInfoService.previewModelFile(modelNo);
            if (filePath == null)
            {
                MessageBox.Show("未找到該客戶出貨料號對應的打印模板信息，請維護相關信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //查詢打印模板的打印字段
            MandRelDel mandRelDel = mandRelDelService.queryManNoByDel(cusNo, delMatno, "1");
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
            string cartonNo = this.dataGridView1.Rows[index].Cells["CartonNo"].Value.ToString();
            Carton carton = cartonService.queryCartonDetailsByNo(cartonNo);
            if(carton.CtCodeList != null)
            {
                initCTSeq(carton);
            }
            bool judgePrint = barPrint.printCatonByModel(filePath, carton, mandUnionFieldTypeList);
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


        private void initCTSeq(Carton carton)
        {
            List<String> ctcodeList = carton.CtCodeList;
            for (int i = 0; i < ctcodeList.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        carton.Ct1 = ctcodeList[i];
                        break;
                    case 1:
                        carton.Ct2 = ctcodeList[i];
                        break;
                    case 2:
                        carton.Ct3 = ctcodeList[i];
                        break;
                    case 3:
                        carton.Ct4 = ctcodeList[i];
                        break;
                    case 4:
                        carton.Ct5 = ctcodeList[i];
                        break;
                    case 5:
                        carton.Ct6 = ctcodeList[i];
                        break;
                    case 6:
                        carton.Ct7 = ctcodeList[i];
                        break;
                    case 7:
                        carton.Ct8 = ctcodeList[i];
                        break;
                    case 8:
                        carton.Ct9 = ctcodeList[i];
                        break;
                    case 9:
                        carton.Ct10 = ctcodeList[i];
                        break;
                }

            }
        }
    }
}
