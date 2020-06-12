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

        private readonly PrintModelQ printQ = new PrintModelQ();
        private readonly OracleQueryB queryB = new OracleQueryB();                           //查询ERP信息
        private readonly BarCodePrint barPrint = BarCodePrint.getInstance();
        private readonly MandRelDelService mandRelDelService = new MandRelDelService();     //查詢模板的参数字段
        private readonly FileRelDelService fileRelDelService = new FileRelDelService();     //查詢模板的文件編號
        private readonly ModelInfoService modelInfoService = new ModelInfoService();        //查詢模板內容並下載
        private readonly CodeRuleService codeRuleService = new CodeRuleService();           //查询编码规则
        private readonly CusRuleService cusRuleService = new CusRuleService();
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
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                comboBox8.Items.Add(PrinterSettings.InstalledPrinters[i]);
            }
            comboBox8.SelectedIndex = 0;

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (!this.checkUIInfo())
            {
                return;
            }
            if (this.textBox15.Text == null || this.textBox15.Text.Trim() == "")
            {
                MessageBox.Show("客戶编码规则为空，请先维护该出货料号/子阶料号的编码规则!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            this.generateCTList();
            MandRelDel mandRelDel = mandRelDelService.queryManNoByDel(ctCodeInfo.Cusno, ctCodeInfo.Delmatno, "0");
            if (mandRelDel == null)
            {
                MessageBox.Show("未找到該客戶出貨料號對應的打印字段規則信息，請維護相關信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Focus();
                return;
            }
            //查詢字段對應的規則信息
            List<MandUnionFieldType> mandUnionFieldTypeList = manRelFieldTypeService.queryMandUnionFieldTypeList(mandRelDel.ManNo);
            if (mandUnionFieldTypeList == null)
            {
                MessageBox.Show("未找到該客戶出貨料號對應的打印字段規則信息，請維護相關信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Focus();
                return;
            }
            if (comboBox8.SelectedItem == null || this.comboBox8.SelectedItem.ToString().Trim() == "")
            {
                MessageBox.Show("請選擇/設置打印機", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.comboBox8.Focus();
                return;
            }
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
                List<WorkOrderInfo> workOrderInfos = queryB.getWorkInfoByNo(workno.ToUpper());
                if (workOrderInfos != null && workOrderInfos.Count > 0)
                {
                    this.comboBox1.DataSource = workOrderInfos;
                    this.comboBox1.DisplayMember = "CustPO";                                          
                    this.comboBox1.ValueMember = "OrderQty";                              
                    this.comboBox5.DataSource = workOrderInfos;
                    this.comboBox5.DisplayMember = "SoOrder";                                       
                    this.comboBox5.ValueMember = "SoOrder";
                    this.textBox10.Text = workOrderInfos[0].CustName;                  
                    this.textBox4.Text = workOrderInfos[0].StartQty;                 
                    this.textBox9.Text = workOrderInfos[0].CompletedQty;
                    this.textBox3.Text = workOrderInfos[0].ItemCode;
                    this.textBox11.Text = workOrderInfos[0].CustId;
                    this.textBox7.Text = this.getCountWorkNoCT(workno);                //CT Count
                    if (workOrderInfos[0].CusItemNum != null && workOrderInfos[0].CusItemNum.Trim() != "")
                    {
                        this.comboBox4.DisplayMember = "CusItemNum";
                        this.comboBox4.ValueMember = "CusItemNum";
                        this.comboBox4.DataSource = workOrderInfos;
                    }
                    else
                    {
                        List<CusMatInfo> cusMatInfos = queryB.getCusMatInfo(workno);
                        if (cusMatInfos != null && cusMatInfos.Count > 0)
                        {
                            this.comboBox4.DisplayMember = "CusItemCode";
                            this.comboBox4.ValueMember = "CusItemCode";
                            this.comboBox4.DataSource = cusMatInfos;
                            this.textBox14.Text = cusMatInfos[0].ItemDesc;
                        }
                    }

                    if (this.comboBox1.Text != null && this.comboBox1.Text.Trim() != "")
                    {
                        this.textBox8.Text = this.getCountPOCT(workno, this.comboBox1.Text.Trim());
                    }
                    this.textBox2.Text = workOrderInfos[0].OrderQty;    

                    this.comboBox3.DisplayMember = "Revision";
                    this.comboBox3.ValueMember = "Revision";
                    this.comboBox3.DataSource = queryB.getRevisionInfo(workno);

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
                   
                    this.updateMactype();   //更新编码规则
                    this.checkPrintInfo();  //更新CTCodeinfo信息
                    this.updateUIInfo();   //更新打印模板和預覽CT碼信息

                }
            }
        }




        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedValue != null && this.comboBox1.SelectedValue.ToString().Trim() != "")
            {
                this.textBox2.Text = this.comboBox1.SelectedValue.ToString().Trim();
                this.textBox7.Text = this.getCountWorkNoCT(this.textBox1.Text);
                List<WorkOrderInfo> workOrders = (List<WorkOrderInfo>)this.comboBox1.DataSource;
                WorkOrderInfo workOrder = workOrders[this.comboBox1.SelectedIndex];
                this.textBox10.Text = workOrder.CustName;
                this.textBox11.Text = workOrder.CustId;
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
                this.textBox13.Text = printQ.getCTCountBySubMat(this.textBox1.Text, this.comboBox6.SelectedValue.ToString());
            }
            else
            {
                this.textBox13.Text = "0";
            }
            //判斷是否以子階工單計算剩餘打印數量
            if (this.comboBox7.SelectedValue.ToString() == "1")
            {
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
            if (this.textBox15.Text == null || this.textBox15.Text.Trim() == "")
            {
                MessageBox.Show("請在本系統維護客戶和编码规则關係！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            CodeRule codeRule = codeRuleService.queryRuleById(ctCodeInfo.Ruleno);
            if (codeRule != null && codeRule.RuleItem.Count > 0 )
            {
                foreach (RuleItem ruleItem in codeRule.RuleItem)
                {
                    string ruleType = ruleItem.Ruletype;
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
            }
            else
            {
                if(this.comboBox6.SelectedValue != null)
                {
                    ctCodeInfo.Delmatno = this.comboBox6.SelectedValue.ToString();
                }
            }
            ctCodeInfo.Cusno = this.textBox11.Text.Trim();          //customer number 
            ctCodeInfo.Cusname = this.textBox10.Text.Trim();        //customer name
            ctCodeInfo.Offino = this.textBox6.Text.Trim();          //officialNo
            ctCodeInfo.SoOrder = this.comboBox5.Text == null ? "" : this.comboBox5.Text.ToString().Trim();   //so_order
            ctCodeInfo.Verno = this.comboBox3.SelectedValue == null ? "" : this.comboBox3.SelectedValue.ToString().Trim();      //version number
            ctCodeInfo.Woquantity = this.textBox4.Text.Trim();  //wnquantity
            ctCodeInfo.Cusmatno = this.comboBox4.SelectedValue == null ? "" : this.comboBox4.SelectedValue.ToString().Trim();   //customer material number 
            this.numericUpDown1.Value = getPrintCeiling(int.Parse(ctCodeInfo.Orderqty), int.Parse(ctCodeInfo.Woquantity));
            FileRelDel fileRelDel = fileRelDelService.queryFileRelDelCusNo(ctCodeInfo.Cusno, ctCodeInfo.Delmatno, "0");
            if (fileRelDel != null)
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
            ctCodeInfo.Cuspo = this.comboBox1.Text == null ? "" : this.comboBox1.Text.ToString().Trim();            //Cuspo
            ctCodeInfo.SoOrder = this.comboBox5.Text == null ? "" : this.comboBox5.Text.ToString().Trim();          //so_order
            ctList = generateC.generateCTNumber(ctCodeInfo, printQty, dateTimePicker1.Value);
            if (ctList.Count > 0)
            {
                this.textBox12.Text = ctList[0].Ctcode;
                this.textBox5.Text = ctList[ctList.Count - 1].Ctcode;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (ctCodeInfo.Ruleno != null)
            {
                this.generateCTList();
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
            FileRelDel fileRelDel = fileRelDelService.queryFileRelDelCusNo(this.textBox11.Text, delMatno, "0");
            if (fileRelDel != null)
            {
                //下載模板並預覽   1.查询模板是否存在， 若存在不下载  2.若不存在下载模板
                filePath = modelInfoService.previewModelFile(fileRelDel.FileNo);
                if (filePath != null)
                {
                    string pictureFile = barPrint.PreviewPrintBC(filePath);
                    this.pictureBox1.Load(pictureFile);
                }
            }
            this.generateCTList();
        }


        /// <summary>
        /// 查询编码规则
        /// </summary>
        private void updateMactype()
        {

            string comboxValue = this.textBox11.Text;
            string delmatno = "";
            if (this.comboBox7.SelectedValue.ToString() == "1")
            {
                delmatno = this.textBox3.Text == null ? "" : this.textBox3.Text.Trim();
            }
            else
            {
                delmatno = this.comboBox6.SelectedValue == null ? "" : this.comboBox6.SelectedValue.ToString().Trim();
            }
            CusRule cusRule = cusRuleService.queryCusRuleByCond(comboxValue, delmatno, "0");                //0代表CT碼編碼規則
     
            if (cusRule != null && cusRule.Ruleno != null)
            {
                CodeRule codeRule = codeRuleService.queryRuleById(cusRule.Ruleno);
                this.textBox15.Text = codeRule.RuleDesc;
                ctCodeInfo.Ruleno = codeRule.Ruleno;
            }
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
            this.textBox15.Text = "";
            this.comboBox1.DataSource = null;
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
