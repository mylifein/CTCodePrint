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
            DataSet ds = cartonService.getCartonsInfo(queryCondi, queryValue);
            if (ds != null && ds.Tables.Count > 0)
            {
                this.dataGridView1.DataSource = ds.Tables[0];


                //DGV 改變列名,列寬
                this.dataGridView1.Columns[0].Visible = false;
                this.dataGridView1.Columns[0].Width = 0;
                this.dataGridView1.Columns[1].HeaderText = "裝箱單號";
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
            string cusNo = this.dataGridView1.Rows[index].Cells["cus_no"].Value.ToString();
            string delMatno = this.dataGridView1.Rows[index].Cells["del_matno"].Value.ToString();
            ModelFile modelFile = modelInfoService.queryModelFileByNo(modelNo);
            if (modelFile == null)
            {
                MessageBox.Show("未找到該客戶出貨料號對應的打印模板信息，請維護相關信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string filePath = Auxiliary.downloadModelFile(modelFile);
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
            string cartonNo = this.dataGridView1.Rows[index].Cells["cartonNo"].Value.ToString();
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
