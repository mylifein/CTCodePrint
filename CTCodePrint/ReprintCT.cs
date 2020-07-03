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
using System.Reflection;
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
        private readonly CTCodeService cTCodeService = new CTCodeService();
        private readonly ModelInfoService modelInfoService = new ModelInfoService();                    //查詢模板內容並下載
        private readonly MandRelDelService mandRelDelService = new MandRelDelService();                 //查詢模板的字段規則
        private readonly ManRelFieldTypeService manRelFieldTypeService = new ManRelFieldTypeService();  //根據字段規則 查詢字段規則值

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
            string cusNo = this.dataGridView1.Rows[index].Cells["cus_no"].Value.ToString();
            string delMatno = this.dataGridView1.Rows[index].Cells["del_matno"].Value.ToString();

            //下載模板並預覽   1.查询模板是否存在， 若存在不下载  2.若不存在下载模板
            string filePath = modelInfoService.previewModelFile(modelNo);
            if (filePath == null)
            {
                MessageBox.Show("未找到該客戶出貨料號對應的打印模板信息，請維護相關信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //查詢打印模板的打印字段
            MandRelDel mandRelDel = mandRelDelService.queryManNoByDel(cusNo, delMatno, "0");
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
            string ctcode = this.dataGridView1.Rows[index].Cells["ct_code"].Value.ToString();
            CTCode ctcodeEntity = cTCodeService.queryCTCodeByCtcode(ctcode);
            bool judge = barPrint.bactchPrintPalletByModel(filePath, ctcodeToDic(ctcodeEntity, mandUnionFieldTypeList));
            if (judge)
            {
                printQ.savePrintRecord(ctcodeEntity);

            }
            else
            {
                MessageBox.Show("打印失敗請聯係管理員！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }


        private Dictionary<string, string> ctcodeToDic(CTCode ctcode, List<MandUnionFieldType> mandUnionFieldTypeList)
        {

            PropertyInfo[] propertyInfoARR = ctcode.GetType().GetProperties();
            Dictionary<string, string> property = new Dictionary<string, string>();
            foreach (PropertyInfo propertyInfo in propertyInfoARR)
            {
                Object propertyVal = ctcode.GetType().GetProperty(propertyInfo.Name).GetValue(ctcode, null);
                if (propertyVal != null)
                {
                    property.Add(propertyInfo.Name.ToUpper(), propertyVal.ToString());
                }
            }

            Dictionary<string, string> ctDict = new Dictionary<string, string>();
            foreach (MandUnionFieldType mandUnionFieldType in mandUnionFieldTypeList)
            {
                string fieldName = mandUnionFieldType.FieldName.ToUpper();
                if (property.ContainsKey(fieldName))
                {
                    ctDict.Add(fieldName, property[fieldName]);
                }
                else
                {
                    ctDict.Add(fieldName, mandUnionFieldType.FieldValue);
                }
            }

            return ctDict;
        }
    }
}
