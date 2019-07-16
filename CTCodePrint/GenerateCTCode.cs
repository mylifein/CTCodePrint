using BLL;
using CTCodePrint.common;
using GenerateCTCode;
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
    public partial class GenerateCTCode : Form
    {
        public GenerateCTCode()
        {
            InitializeComponent();
        }

        private bool suspendB = false;
        private readonly SelectQuery selectQ = new SelectQuery();
        private readonly PrintModelQ printQ = new PrintModelQ();
        private readonly OracleQueryB queryB = new OracleQueryB();
        private readonly BarCodePrint barPrint = BarCodePrint.getInstance();
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
            if (this.textBox1.Text == null || this.textBox1.Text.Trim() == "")
            {
                MessageBox.Show("請輸入工單號！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            if (this.textBox11.Text == null || this.textBox11.Text.Trim() == "")
            {
                MessageBox.Show("請在本系統維護客戶信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox11.Focus();
                return;
            }
            if (this.comboBox2.SelectedValue == null || this.comboBox2.SelectedValue.ToString() == "")
            {
                MessageBox.Show("請在本系統維護客戶和幾種類型關係！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int printQty = int.Parse(this.textBox4.Text.Trim().ToString()) - int.Parse(printQ.getGeneratedCTCount(this.textBox1.Text.Trim()));
            if (printQty <= 0)
            {
                MessageBox.Show("此工單已經生成所需數量的CT碼！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CTCode ctCodeInfo = new CTCode();
            ctCodeInfo.Workno = this.textBox1.Text.Trim();           //workNo
            ctCodeInfo.Cuspo = this.textBox2.Text.Trim();            //Cuspo
            ctCodeInfo.Delmatno = this.textBox3.Text.Trim();        //string deliveryMat
            ctCodeInfo.Cusno = this.textBox11.Text.Trim();          //customer number 
            ctCodeInfo.Offino = this.textBox6.Text.Trim();  //officialNo
            ctCodeInfo.Mactype = this.comboBox2.SelectedValue == null ? "" : this.comboBox2.SelectedValue.ToString().Trim();  //macType
            ctCodeInfo.Verno = this.comboBox3.SelectedValue.ToString().Trim();      //version number
            ctCodeInfo.Woquantity = this.textBox4.Text.Trim();  //wnquantity
            ctCodeInfo.Completedqty = this.textBox9.Text.Trim();    //completed quantity
            ctCodeInfo.Cusmatno = this.comboBox4.SelectedValue == null ? "" : this.comboBox4.SelectedValue.ToString().Trim();   //customer material number 
            DataSet dsCheck = selectQ.getRulesByNo(ctCodeInfo.Mactype);
            if(dsCheck != null && dsCheck.Tables.Count > 0 && dsCheck.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsCheck.Tables[0].Rows)
                {
                    string ruleType = dr["rule_type"].ToString();
                    switch (ruleType.Trim())
                    {
                        case "T001":
                            break;
                        case "T002":
                            break;
                        case "T003":
                            if(this.comboBox4.SelectedValue == null || this.comboBox4.SelectedValue.ToString() == "")
                            {
                                MessageBox.Show("此編碼規則需要客戶料號信息，請在ERP中維護！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            break;
                        case "T004":
                            if (this.textBox6.Text == null || this.textBox6.Text.Trim() == "")
                            {
                                MessageBox.Show("此編碼規則需要正式編號信息，請輸入正式編號！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.textBox6.Focus();
                                return;
                            }
                            break;
                        case "T005":
                            if (this.comboBox3.SelectedValue == null || this.comboBox3.SelectedValue.ToString() == "")
                            {
                                MessageBox.Show("此編碼規則需要版本號，請在ERP中維護！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            break;

                    }

                }
            }
            else
            {
                MessageBox.Show("未找到編碼規則，請維護編碼規則", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            /// Check 必填項
            ///流水碼自增  並調用打印 
            ///查詢模板號
            string modelNo = printQ.checkPrintModelRel(ctCodeInfo.Cusno, ctCodeInfo.Delmatno);
            if (modelNo == null || modelNo == "")
            {
                MessageBox.Show("未找到該客戶出貨料號對應的打印模板", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Focus();
                return;
            }
            ctCodeInfo.Modelno = modelNo;
            List<CTCode> ctList = new List<CTCode>();
            ctList = generateC.generateCTNumber(ctCodeInfo,printQty);
            
           DataSet ds = printQ.getModelInfo(modelNo);
           if(ds == null || ds.Tables[0].Rows.Count < 1)
            {
                MessageBox.Show("未找到該客戶出貨料號對應的打印模板信息，請維護相關信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Focus();
                return;
            }
            string templateFile = System.IO.Directory.GetCurrentDirectory() + "\\" + ds.Tables[0].Rows[0]["model_name"].ToString();
            ///查詢該模板必填字段
            MandatoryField manF = printQ.getMandInfoByMod(modelNo);

            if (manF != null)
            {
                int i = 1;
                foreach(CTCode ctTemp in ctList)
                {
                    this.textBox5.Text = i.ToString();
                    PrintContent printContent = new PrintContent();
                    PrintRecord record = new PrintRecord();
                    printContent.CtCode = ctTemp.Ctcode;
                    record.Ctcode = ctTemp.Ctcode;
                    this.textBox8.Text = ctTemp.Ctcode;
                    bool judge = barPrint.PrintBC(templateFile, printContent, manF);
                    if (judge)
                    {
                        printQ.saveCTCodeInfo(ctTemp);
                        printQ.savePrintRecord(record);
                    
                    }
                    else
                    {
                        MessageBox.Show("打印失敗請聯係管理員！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (suspendB)
                    {
                        return;
                    }
                    i++;
                }

            }



        }



        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                string workno = this.textBox1.Text;
                DataSet ds = queryB.getWorkInfoByNo(workno);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.textBox2.Text = ds.Tables[0].Rows[0]["CUST_PO_NUMBER"].ToString();     //PO
                        this.textBox11.Text = ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString();      //customer number 
                        this.textBox10.Text = ds.Tables[0].Rows[0]["CUST_NAME"].ToString();     //customer name
                        this.textBox4.Text = ds.Tables[0].Rows[0]["START_QUANTITY"].ToString();      //work number quantity
                        this.textBox9.Text = ds.Tables[0].Rows[0]["QUANTITY_COMPLETED"].ToString();      //completed quantity
                        this.textBox3.Text = ds.Tables[0].Rows[0]["ITEM_CODE"].ToString();          //delivery code
                        this.textBox7.Text = printQ.getGeneratedCTCount(workno);
                    }
                }
                ds = queryB.getCusMatInfo(workno);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.comboBox4.DisplayMember = "CUSTOMER_ITEM_NUMBER";
                        this.comboBox4.ValueMember = "CUSTOMER_ITEM_NUMBER";
                        this.comboBox4.DataSource = ds.Tables[0];
                    }
                }
                ds = queryB.getRevisionInfo(workno);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.comboBox3.DisplayMember = "REVISION";
                        this.comboBox3.ValueMember = "REVISION";
                        this.comboBox3.DataSource = ds.Tables[0];
                    }
                }
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            string comboxValue = this.textBox11.Text;
            string delmatno = this.textBox2.Text == null ? "" : this.textBox2.Text.Trim();
            DataSet ds = selectQ.getMacByCus(comboxValue,delmatno);
            DataTable itemTable = null;
            string mactypeno = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                mactypeno = ds.Tables[0].Rows[0]["mactypeno"].ToString();
            }
            if(mactypeno != null)
            {
                DataSet mactDS = printQ.queryMacType(mactypeno);
                if (mactDS.Tables.Count > 0 && mactDS.Tables[0].Rows.Count > 0)
                {
                    itemTable = mactDS.Tables[0];
                }
            }
            this.comboBox2.DisplayMember = "mactypename";
            this.comboBox2.ValueMember = "mactypeno";
            this.comboBox2.DataSource = itemTable;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.suspendB = true;
        }




    }
}
