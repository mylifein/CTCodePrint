using BLL;
using CTCodePrint.common;
using DBUtility;
using GenerateCTCode;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
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
        private readonly PrintModelQ printQ = new PrintModelQ();
        private readonly OracleQueryB queryB = new OracleQueryB();
        private readonly BarCodePrint barPrint = BarCodePrint.getInstance();
        private readonly ModelRelMandService modelRelMandService = new ModelRelMandService();  //查詢模板的字段規則
        private readonly FileRelDelService fileRelDelService = new FileRelDelService();     //查詢模板的文件編號
        private readonly ModelInfoService modelInfoService = new ModelInfoService();        //查詢模板內容並下載
        private readonly ManRelFieldTypeService manRelFieldTypeService = new ManRelFieldTypeService();  //根據字段規則 查詢字段規則值
        private GenerateCode generateC = new GenerateCode();
        private readonly CTCodeService ctCodeService = new CTCodeService();
        private readonly SubMatInfoService subMatInfoService = new SubMatInfoService();
        private string filePath = null;
        private CTCode ctCodeInfo = new CTCode();
        private List<CTCode> ctList = new List<CTCode>();
        private void GenerateCTCode_Load(object sender, EventArgs e)
        {
            DataTable itemTable = new DataTable();   // construct selects value
            DataColumn column;
            DataRow row;
            column = new DataColumn("Name");
            itemTable.Columns.Add(column);
            column = new DataColumn("Value");
            itemTable.Columns.Add(column);
            row = itemTable.NewRow();
            row["Name"] = "出貨料號";
            row["Value"] = "1";
            itemTable.Rows.Add(row);
            row = itemTable.NewRow();
            row["Name"] = "子階料號";
            row["Value"] = "2";
            itemTable.Rows.Add(row);
            this.comboBox7.DisplayMember = "Name";
            this.comboBox7.ValueMember = "Value";
            this.comboBox7.DataSource = itemTable;
            //初始化選擇打印機
            for(int i = 0;i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                comboBox8.Items.Add(PrinterSettings.InstalledPrinters[i]);
            }
            comboBox8.SelectedIndex = 0;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!this.checkUIInfo())
            {
                return;
            }
            if (this.comboBox2.SelectedValue == null || this.comboBox2.SelectedValue.ToString().Trim() == "")
            {
                MessageBox.Show("客戶编码规则為空，請先維護該出貨料號/子階料號對應的機種類型!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            this.generateCTList();
            ModelRelMand modelRelMand = modelRelMandService.queryMenuInfoByFileNo(ctCodeInfo.Modelno);
            if (modelRelMand == null)
            {
                MessageBox.Show("未找到該客戶出貨料號對應的打印字段規則信息，請維護相關信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Focus();
                return;
            }
            //查詢字段對應的規則信息
            List<MandUnionFieldType> mandUnionFieldTypeList = manRelFieldTypeService.queryMandUnionFieldTypeList(modelRelMand.ManNo);
            if (mandUnionFieldTypeList == null)
            {
                MessageBox.Show("未找到該客戶出貨料號對應的打印字段規則信息，請維護相關信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Focus();
                return;
            }
            if(comboBox8.SelectedItem == null || this.comboBox8.SelectedItem.ToString().Trim() == "")
            {
                MessageBox.Show("請選擇/設置打印機", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.comboBox8.Focus();
                return;
            }
            // MessageBox.Show("正在打印中，不能做任何操作請耐心等待！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bool judgePrint = barPrint.BactchPrintCTByModel(filePath, ctListToArray(ctList, mandUnionFieldTypeList), comboBox8.SelectedItem.ToString()); //barPrint.BactchPrintBCByModel(filePath, ctList, mandUnionFieldTypeList,comboBox8.SelectedItem.ToString());
            if (judgePrint)
            {
                if (!ctCodeService.saveCTCodeList(ctList))
                {
                    MessageBox.Show("保存CT碼列表失敗！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                MessageBox.Show("打印失敗請聯係管理員！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


        }



        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.clearAll();
                string workno = this.textBox1.Text;
                DataSet ds = queryB.getWorkInfoByNo(workno.ToUpper());
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.comboBox1.DataSource = ds.Tables[0];
                        this.comboBox1.DisplayMember = "CUST_PO_NUMBER";                        //PO                    
                        this.comboBox1.ValueMember = "ORDER_QTY";                               //QTY
                        this.comboBox5.DataSource = ds.Tables[0];
                        this.comboBox5.DisplayMember = "SO_ORDER";                        //SO                 
                        this.comboBox5.ValueMember = "SO_ORDER";
                        this.textBox10.Text = ds.Tables[0].Rows[0]["CUST_NAME"].ToString();     //customer name
                        this.textBox4.Text = ds.Tables[0].Rows[0]["START_QUANTITY"].ToString();      //work number quantity
                        this.textBox9.Text = ds.Tables[0].Rows[0]["QUANTITY_COMPLETED"].ToString();      //completed quantity
                        this.textBox3.Text = ds.Tables[0].Rows[0]["ITEM_CODE"].ToString();          //delivery code
                        this.textBox11.Text = ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString();      //customer number 
                        this.textBox7.Text = this.getCountWorkNoCT(workno);                //CT Count
                        //DataSet dsCusMaterial = queryB.getCusMatInfo(workno);
                        //if (dsCusMaterial != null && dsCusMaterial.Tables.Count > 0)
                        //{
                        //    if (dsCusMaterial.Tables[0].Rows.Count > 0)
                        //    {
                        //        this.comboBox4.DisplayMember = "CUSTOMER_ITEM_NUMBER";
                        //        this.comboBox4.ValueMember = "CUSTOMER_ITEM_NUMBER";
                        //        this.comboBox4.DataSource = dsCusMaterial.Tables[0];
                        //        this.textBox14.Text = dsCusMaterial.Tables[0].Rows[0]["CUSTOMER_ITEM_DESC"].ToString();
                        //    }
                        //}
                        if (ds.Tables[0].Rows[0]["CUSTOMER_ITEM_NUMBER"] != null && ds.Tables[0].Rows[0]["CUSTOMER_ITEM_NUMBER"].ToString().Trim() != "")
                        {
                            this.comboBox4.DisplayMember = "CUSTOMER_ITEM_NUMBER";
                            this.comboBox4.ValueMember = "CUSTOMER_ITEM_NUMBER";
                            this.comboBox4.DataSource = ds.Tables[0];
                        }
                        else
                        {
                            DataSet dsCusMaterial = queryB.getCusMatInfo(workno);
                            if (dsCusMaterial != null && dsCusMaterial.Tables.Count > 0)
                            {
                                if (dsCusMaterial.Tables[0].Rows.Count > 0)
                                {
                                    this.comboBox4.DisplayMember = "CUSTOMER_ITEM_NUMBER";
                                    this.comboBox4.ValueMember = "CUSTOMER_ITEM_NUMBER";
                                    this.comboBox4.DataSource = dsCusMaterial.Tables[0];
                                    this.textBox14.Text = dsCusMaterial.Tables[0].Rows[0]["CUSTOMER_ITEM_DESC"].ToString();
                                }
                            }
                        }

                        if (this.comboBox1.Text != null && this.comboBox1.Text.Trim() != "")
                        {
                            this.textBox8.Text = this.getCountPOCT(workno, this.comboBox1.Text.Trim());
                        }
                        this.textBox2.Text = ds.Tables[0].Rows[0]["ORDER_QTY"].ToString();      //PO QTY


                    }
                    else
                    {
                        MessageBox.Show("該工單不存在,請確認輸入的是有效工單！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.textBox1.Focus();
                        return;
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
                //查询子阶料号
                if (this.textBox3.Text != null && this.textBox3.Text.Trim() != "")
                {
                    List<SubMatInfo> subMatInfoList = subMatInfoService.querySubMatInfoList(this.textBox3.Text.Trim());
                    if (subMatInfoList != null && subMatInfoList.Count > 0)
                    {
                        this.comboBox6.DataSource = subMatInfoList;
                        this.comboBox6.DisplayMember = "Submatno";
                        this.comboBox6.ValueMember = "Submatno";
                    }
                }
                this.updateMactype();   //更新機種
                this.checkPrintInfo();  //更新CTCodeinfo信息
                this.updateUIInfo();   //更新打印模板和預覽CT碼信息

            }
        }




        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedValue != null && this.comboBox1.SelectedValue.ToString().Trim() != "")
            {
                this.textBox2.Text = this.comboBox1.SelectedValue.ToString().Trim();
                this.textBox7.Text = this.getCountWorkNoCT(this.textBox1.Text);
            }
        }

        private string getCountWorkNoCT(string workno)
        {
            return printQ.getGeneratedCTCount(workno);
        }

        private string getCountPOCT(string workNo, string cusPO)
        {
            return printQ.getGeneratedCTCountByPO(workNo, cusPO);
        }

        private int getPrintCeiling(int poQty, int woQty)
        {
            //獲得子階料號
            if (this.comboBox6.SelectedValue != null && this.comboBox6.SelectedValue.ToString().Trim() != "")
            {
                this.textBox13.Text = printQ.getCTCountBySubMat(this.textBox1.Text,this.comboBox6.SelectedValue.ToString());
            }else
            {
                this.textBox13.Text = "0";
            }
            //判斷是否以子階工單計算剩餘打印數量
            if(this.comboBox7.SelectedValue.ToString() == "1"){
                if (poQty > woQty)
                {
                    return woQty - int.Parse(this.textBox7.Text.Trim().ToString());
                }
                else
                {
                    return poQty - int.Parse(this.textBox8.Text.Trim().ToString());
                }
            }
            else
            {
                if (poQty > woQty)
                {
                    return woQty - int.Parse(this.textBox13.Text.Trim().ToString());
                }
                else
                {
                    return poQty - int.Parse(this.textBox13.Text.Trim().ToString());
                }
            }

        }

        //檢查打印所需信息
        private bool checkUIInfo()
        {
            //獲得界面信息
            if (this.textBox1.Text == null || this.textBox1.Text.Trim() == "")
            {
                MessageBox.Show("請輸入工單號！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return false;
            }
            if (this.textBox11.Text == null || this.textBox11.Text.Trim() == "")
            {
                MessageBox.Show("請在本系統維護客戶信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox11.Focus();
                return false;
            }
            if (this.comboBox2.SelectedValue == null || this.comboBox2.SelectedValue.ToString() == "")
            {
                MessageBox.Show("請在本系統維護客戶和幾種類型關係！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            DataSet dsCheck = selectQ.getRulesByActualNo(ctCodeInfo.Ruleno);
            if (dsCheck != null && dsCheck.Tables.Count > 0 && dsCheck.Tables[0].Rows.Count > 0)
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
                            if (ctCodeInfo.Cusmatno.Trim() == "")
                            {
                                MessageBox.Show("此編碼規則需要客戶料號信息，請在ERP中維護！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return false;
                            }
                            break;
                        case "T004":
                            if (ctCodeInfo.Offino == null || ctCodeInfo.Offino.Trim() == "")
                            {
                                MessageBox.Show("此編碼規則需要正式編號信息，請輸入正式編號！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.textBox6.Focus();
                                return false;
                            }
                            break;
                        case "T005":
                            if (ctCodeInfo.Verno.Trim() == "")
                            {
                                MessageBox.Show("此編碼規則需要版本號，請在ERP中維護！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return false;
                            }
                            break;
                        case "T008":
                            if (ctCodeInfo.SoOrder.Trim() == "")
                            {
                                MessageBox.Show("此編碼規則需要销售订单，請在ERP中維護！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return false;
                            }
                            break;

                    }

                }
            }
            else
            {
                MessageBox.Show("未找到編碼規則，請維護編碼規則", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;

        }

        private void checkPrintInfo()
        {
            ctCodeInfo.Workno = this.textBox1.Text.Trim().ToUpper();           //workNo
            ctCodeInfo.Cuspo = this.comboBox1.Text == null ? "" : this.comboBox1.Text.ToString().Trim();            //Cuspo
            ctCodeInfo.Orderqty = this.textBox2.Text.Trim();        //order qty
           
            if (this.comboBox7.SelectedValue.ToString() == "1")         //如果等於出貨料號
            {
                ctCodeInfo.Delmatno = this.textBox3.Text.Trim();        //string deliveryMat
            }else
            {
                ctCodeInfo.Delmatno = this.comboBox6.SelectedValue.ToString();
            }
            ctCodeInfo.Cusno = this.textBox11.Text.Trim();          //customer number 
            ctCodeInfo.Cusname = this.textBox10.Text.Trim();        //customer name
            ctCodeInfo.Offino = this.textBox6.Text.Trim();  //officialNo
            ctCodeInfo.SoOrder = this.comboBox5.Text == null ? "" : this.comboBox5.Text.ToString().Trim();   //so_order
            ctCodeInfo.Ruleno = this.comboBox2.SelectedValue == null ? "" : this.comboBox2.SelectedValue.ToString().Trim();     //编码规则
            ctCodeInfo.Verno = this.comboBox3.SelectedValue == null ? "" : this.comboBox3.SelectedValue.ToString().Trim();      //version number
            ctCodeInfo.Woquantity = this.textBox4.Text.Trim();  //wnquantity
            ctCodeInfo.Cusmatno = this.comboBox4.SelectedValue == null ? "" : this.comboBox4.SelectedValue.ToString().Trim();   //customer material number 
            FileRelDel fileRelDel = fileRelDelService.queryFileRelDelCusNo(ctCodeInfo.Cusno, ctCodeInfo.Delmatno,"0");
            if(fileRelDel != null)
            {
                ctCodeInfo.Modelno = fileRelDel.FileNo;
            }
        }

        private void generateCTList()
        {
            int printQty = (int)this.numericUpDown1.Value;
            if (printQty <= 0)
            {
                MessageBox.Show("此工單已經生成所需數量的CT碼！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ctList = generateC.generateCTNumber(ctCodeInfo, printQty);
            if(ctList.Count > 0)
            {
                this.textBox12.Text = ctList[0].Ctcode;
                this.textBox5.Text = ctList[ctList.Count-1].Ctcode;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if(ctCodeInfo.Ruleno != null)
            {
                this.generateCTList();
            }
        }


        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.textBox1.Text != null && this.textBox1.Text.Trim() != "")
            {
                this.updateMactype();  //更新機種類型
                this.checkPrintInfo();  //更新打印模板
                this.updateUIInfo();   //更新UI信息 和打印模板
            }

        }

        //更新打印模板和打印數量信息
        private void updateUIInfo()
        {   

            //檢查是否建立模板
            string delMatno = "";
            if (this.comboBox7.SelectedValue.ToString() == "1")
            {
                delMatno = this.textBox3.Text;
            }
            else
            {
                delMatno = this.comboBox6.SelectedValue == null ? "" : this.comboBox6.SelectedValue.ToString().Trim();
            }
            FileRelDel fileRelDel = fileRelDelService.queryFileRelDelCusNo(this.textBox11.Text, delMatno,"0");
            if (fileRelDel != null)
            {
                //下載模板並預覽
                ModelFile modelFile = modelInfoService.queryModelFileByNo(fileRelDel.FileNo);
                if (modelFile != null)
                {
                    filePath = Auxiliary.downloadModelFile(modelFile);
                    string pictureFile = barPrint.PreviewPrintBC(filePath);
                    this.pictureBox1.Load(pictureFile);

                }
            }
            //計算剩餘打印數量
            //int ceilQty = this.getPrintCeiling(int.Parse(this.textBox2.Text.Trim().ToString()), int.Parse(this.textBox4.Text.Trim().ToString()));
            //this.numericUpDown1.Maximum = ceilQty;
            //this.numericUpDown1.Value = ceilQty;
            this.generateCTList();
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.textBox1.Text != null && this.textBox1.Text.Trim() != "")
            {
                this.updateMactype();  //更新機種類型
                this.checkPrintInfo();  //更新打印模板
                this.updateUIInfo();   //更新UI信息 和打印模板
            }
        }

        /// <summary>
        /// 查詢機種類型
        /// </summary>
        private void updateMactype()
        {   

            string comboxValue = this.textBox11.Text;
            string delmatno = "";
            if (this.comboBox7.SelectedValue.ToString() == "1")
            {
                delmatno = this.textBox3.Text == null ? "" : this.textBox3.Text.Trim();
            }else
            {
                delmatno = this.comboBox6.SelectedValue == null ? "" : this.comboBox6.SelectedValue.ToString().Trim();
            }           
            DataSet ds = selectQ.getMacByCus(comboxValue, delmatno,"0");                //0代表CT碼編碼規則
            DataTable itemTable = null;
            string ruleno = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ruleno = ds.Tables[0].Rows[0]["rule_no"].ToString();
            }
            if (ruleno != null)
            {
                DataSet mactDS = printQ.queryCodeInfo(ruleno);
                if (mactDS.Tables.Count > 0 && mactDS.Tables[0].Rows.Count > 0)
                {
                    itemTable = mactDS.Tables[0];
                }
            }
            this.comboBox2.DisplayMember = "rule_desc";
            this.comboBox2.ValueMember = "rule_no";
            this.comboBox2.DataSource = itemTable;
        }


        private void clearAll()
        {
            this.textBox2.Text = "";
            this.textBox3.Text = "";
            this.textBox4.Text = "";
            this.textBox5.Text = "";
            this.textBox7.Text = "";
            this.textBox8.Text = "";
            this.textBox9.Text = "";
            this.textBox10.Text = "";
            this.textBox11.Text = "";
            this.textBox12.Text = "";
            this.textBox13.Text = "";
            this.comboBox1.DataSource = null;
            this.comboBox2.DataSource = null;
            this.comboBox3.DataSource = null;
            this.comboBox4.DataSource = null;
            this.comboBox5.DataSource = null;
        }

        private List<Dictionary<string, string>> ctListToArray(List<CTCode> ctCodeList, List<MandUnionFieldType> mandUnionFieldTypeList)
        {
            List<Dictionary<string, string>> ctList = new List<Dictionary<string, string>>();

            foreach (CTCode ctcode in ctCodeList)
            {
                Dictionary<string, string> ctDict = new Dictionary<string, string>();

                foreach (MandUnionFieldType mandUnionFieldType in mandUnionFieldTypeList)
                {
                    bool judge = false;
                    string FieldName = mandUnionFieldType.FieldName.ToUpper();
                    PropertyInfo[] propertyInfoARR = ctcode.GetType().GetProperties();
                    foreach (PropertyInfo propertyInfo in propertyInfoARR)
                    {
                        if (propertyInfo.Name == mandUnionFieldType.FieldName)
                        {
                            string entityValue = ctcode.GetType().GetProperty(propertyInfo.Name).GetValue(ctcode, null).ToString();
                            ctDict.Add(FieldName, entityValue);
                            judge = true;
                            break;
                        }
                    }
                    if (!judge)
                    {
                        ctDict.Add(FieldName, mandUnionFieldType.FieldValue);
                    }
                }
                ctList.Add(ctDict);
            }
            return ctList;
        }
    }
}
