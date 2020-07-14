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
using System.Threading;
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
        private readonly CapacityService capacityService = new CapacityService();
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
        private Capacity capacity;
        private CodeRule codeRule;
        private FileRelDel fileRelDel;
        private List<MandUnionFieldType> mandUnionFieldTypeList;

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

            this.numericUpDown2.Value = 1;
            this.numericUpDown2.Minimum = 1;

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (codeRule == null)
            {
                MessageBox.Show("请联系管理员，绑定箱号的编码规则", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            //查询标签模板
            if (fileRelDel == null)
            {
                MessageBox.Show("请联系管理员,绑定标签模板！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            //查詢字段對應的規則信息
            if (mandUnionFieldTypeList == null)
            {
                MessageBox.Show("未找到該客戶出貨料號對應的打印字段規則信息，請維護相關信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (comboBox8.SelectedItem == null || this.comboBox8.SelectedItem.ToString().Trim() == "")
            {
                MessageBox.Show("請選擇/設置打印機", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.comboBox8.Focus();
                return;
            }
            generateCTList();
            string printerName = comboBox8.SelectedItem.ToString();
            Thread threadPrint = new Thread(() => {
                if (!barPrint.BactchPrintCTByModel(filePath, ctsToDicList(ctList, mandUnionFieldTypeList),printerName))
                {
                    MessageBox.Show("打印失敗請聯係管理員！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            });
            threadPrint.Start();
            Thread threadSave = new Thread(() => {
                if (!ctCodeService.saveCTCodeList(ctList))
                {
                    MessageBox.Show("保存CT碼列表失敗,請重新保存數據！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            });
            threadSave.Start();

        }



        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.textBox1.Text != null && !"".Equals(this.textBox1.Text))
                {
                    string workno = this.textBox1.Text.ToUpper();
                    List<WorkOrderInfo> workOrderInfos = queryB.getWorkInfoByNo(workno.ToUpper());
                    if (workOrderInfos != null && workOrderInfos.Count > 0)
                    {
                        this.comboBox1.DataSource = workOrderInfos;
                        this.comboBox1.DisplayMember = "CustPO";
                        this.comboBox1.ValueMember = "OrderQty";
                        this.textBox4.Text = workOrderInfos[0].StartQty;                                            //工單數量，  不隨客戶PO 改變
                        this.textBox10.Text = workOrderInfos[0].CustName;                                           //客戶名稱,   隨客戶PO 改變  
                        this.textBox3.Text = workOrderInfos[0].ItemCode;                                           // 出貨料號,   不隨客戶PO 改變
                        this.textBox11.Text = workOrderInfos[0].CustId;                                            //客戶編號     隨客戶PO 改變
                        this.textBox2.Text = workOrderInfos[0].OrderQty;                                          //PO數量        隨客戶PO改變
                        this.textBox7.Text = ctCodeService.getGeneratedCTCount(workno);                           //工單已產生CT碼數量  ，不隨工單改變
                        if (workOrderInfos[0].CusItemNum != null && workOrderInfos[0].CusItemNum.Trim() != "")
                        {
                            this.textBox18.Text = workOrderInfos[0].CusItemNum;                                  //客戶料號，       隨客戶PO 改變
                        }
                        else
                        {
                            List<CusMatInfo> cusMatInfos = queryB.getCusMatInfo(workno);
                            if (cusMatInfos != null && cusMatInfos.Count > 0)
                            {
                                this.textBox18.Text = cusMatInfos[0].CusItemCode;
                                this.textBox14.Text = cusMatInfos[0].CusItemDesc;
                            }
                        }

                        this.textBox8.Text = ctCodeService.getGeneratedCTCountByPO(workno, workOrderInfos[0].CustPO);       //工單、客戶PO已產生CT數量，  隨客戶PO改變   

                        this.comboBox3.DisplayMember = "Revision";                                                          //版本號   不隨客戶PO改變
                        this.comboBox3.ValueMember = "Revision";
                        this.comboBox3.DataSource = queryB.getRevisionInfo(workno);

                        //查询子阶料号
                        if (workOrderInfos[0].ItemCode != null && workOrderInfos[0].ItemCode != "")
                        {
                            List<SubMatInfo> subMatInfoList = subMatInfoService.querySubMatInfoList(workOrderInfos[0].ItemCode);
                            if (subMatInfoList != null && subMatInfoList.Count > 0)
                            {
                                this.comboBox6.DataSource = subMatInfoList;
                                this.comboBox6.DisplayMember = "Submatno";
                                this.comboBox6.ValueMember = "Submatno";
                            }
                        }

                        //ctCodeInfo.Offino = this.textBox6.Text.Trim();          //officialNo
                        //ctCodeInfo.Verno = this.comboBox3.SelectedValue == null ? "" : this.comboBox3.SelectedValue.ToString().Trim();      //version number
                        if (this.comboBox7.SelectedValue.ToString() == "1")         //如果等於出貨料號
                        {
                            ctCodeInfo.Delmatno = workOrderInfos[0].ItemCode;        //string deliveryMat
                        }
                        else
                        {
                            if (this.comboBox6.SelectedValue != null)
                            {
                                ctCodeInfo.Delmatno = this.comboBox6.SelectedValue.ToString();
                            }
                        }
                        ctCodeInfo.Workno = workno;                                     //工單號
                        ctCodeInfo.Woquantity = workOrderInfos[0].StartQty;             //工單數量
                        ctCodeInfo.Delmatno = workOrderInfos[0].ItemCode;               //出貨料號
                        ctCodeInfo.Cusno = workOrderInfos[0].CustId;                    //customer number 
                        ctCodeInfo.Cusname = workOrderInfos[0].CustName;                //customer name
                        ctCodeInfo.Cuspo = workOrderInfos[0].CustPO;                    //Cuspo
                        ctCodeInfo.Orderqty = workOrderInfos[0].OrderQty;               //order qty
                        ctCodeInfo.SoOrder = workOrderInfos[0].SoOrder;               //soOrder
                        ctCodeInfo.Cusmatno = workOrderInfos[0].CusItemNum;
                        loadResources(workOrderInfos[0].CustId, workOrderInfos[0].ItemCode, "0", workno, workOrderInfos[0].CustPO);         //加载打印资源，并计算可打印数量

                    }
                }
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
            //ctCodeInfo.Cuspo = this.comboBox1.Text == null ? "" : this.comboBox1.Text.ToString().Trim();            //Cuspo
            ctCodeInfo.Quantity = (int)this.numericUpDown2.Value;
            ctList = generateC.generateCTNumber(ctCodeInfo, printQty, dateTimePicker1.Value);
            if (ctList.Count > 0)
            {
                this.textBox12.Text = ctList[0].Ctcode;
                this.textBox5.Text = ctList[ctList.Count - 1].Ctcode;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (codeRule != null)
            {
                this.generateCTList();
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
            this.textBox10.Text = "";
            this.textBox11.Text = "";
            this.textBox12.Text = "";
            this.textBox13.Text = "";
            this.textBox15.Text = "";
            this.comboBox1.DataSource = null;
            this.comboBox3.DataSource = null;

        }

        private List<Dictionary<string, string>> ctsToDicList(List<CTCode> ctCodeList, List<MandUnionFieldType> mandUnionFieldTypeList)
        {
            List<Dictionary<string, string>> ctList = new List<Dictionary<string, string>>();

            //获得打印属性.
            Dictionary<string, string> printProperties = new Dictionary<string, string>();
            List<string> dynamicProperties = new List<string>();
            foreach (MandUnionFieldType mandUnionFieldType in mandUnionFieldTypeList)
            {
                printProperties.Add(mandUnionFieldType.FieldName, mandUnionFieldType.FieldValue);
            }
            if (ctCodeList.Count > 0)
            {
                PropertyInfo[] propertyInfoARR = ctCodeList[0].GetType().GetProperties();
                foreach (CTCode ctcode in ctCodeList)
                {
                    Dictionary<string, string> ctDict = new Dictionary<string, string>();
                    if (dynamicProperties.Count == 0)
                    {
                        foreach (PropertyInfo propertyInfo in propertyInfoARR)
                        {
                            dynamicProperties.Add(propertyInfo.Name);
                        }
                    }
                    foreach (var key in printProperties.Keys)
                    {
                        if (dynamicProperties.Contains(key))
                        {
                            Object refValue = ctcode.GetType().GetProperty(key).GetValue(ctcode, null);
                            ctDict.Add(key, refValue == null ? "" : refValue.ToString());
                        }
                        else
                        {
                            ctDict.Add(key, printProperties[key]);
                        }
                    }
                    ctList.Add(ctDict);
                }
            }
            return ctList;
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedValue != null && this.comboBox1.SelectedValue.ToString().Trim() != "")
            {
                List<WorkOrderInfo> workOrders = (List<WorkOrderInfo>)this.comboBox1.DataSource;
                WorkOrderInfo workOrder = workOrders[this.comboBox1.SelectedIndex];
                this.textBox10.Text = workOrder.CustName;
                this.textBox11.Text = workOrder.CustId;
                this.textBox17.Text = workOrder.SoOrder;
                this.textBox18.Text = workOrder.CusItemNum;
                this.textBox2.Text = workOrder.OrderQty;
                this.textBox8.Text = ctCodeService.getGeneratedCTCountByPO(workOrder.WorkNo, workOrder.CustPO);       //工單、客戶PO已產生CT數量，  隨客戶PO改變  
                ctCodeInfo.Cusname = workOrder.CustName;
                ctCodeInfo.Cusno = workOrder.CustId;
                ctCodeInfo.SoOrder = workOrder.SoOrder;
                ctCodeInfo.Cusmatno = workOrder.CusItemNum;
                ctCodeInfo.Orderqty = workOrder.OrderQty;
                ctCodeInfo.Cuspo = workOrder.CustPO;
                loadResources(workOrder.CustId, workOrder.ItemCode, "0", workOrder.WorkNo, workOrder.CustPO);         //加载打印资源，并计算可打印数量

            }
        }



        public void loadResources(string custId, string delMatno, string boundType, string workno, string cusPo)
        {
            capacity = capacityService.queryByRelation(custId, delMatno, boundType);
            if (capacity != null)
            {
                this.countBoxQty(capacity.Capacityqty, workno, cusPo);
                //this.numericUpDown1.Value = getPrintCeiling(int.Parse(ctCodeInfo.Orderqty), int.Parse(ctCodeInfo.Woquantity));
                this.textBox16.Text = capacity.Capacitydesc;
                this.textBox19.Text = capacity.Capacityqty.ToString();
                ctCodeInfo.CapacityNo = capacity.Capacityno;


            }
            else
            {
                this.countBoxQty(1, workno, cusPo);                                 
            }

            codeRule = codeRuleService.queryRuleByCond(custId, delMatno, boundType);  //1代表Carton碼編碼規則
            if (codeRule != null)
            {
                this.textBox15.Text = codeRule.RuleDesc;
                ctCodeInfo.Ruleno = codeRule.Ruleno;
            }
            mandUnionFieldTypeList = manRelFieldTypeService.queryFieldListByCond(custId, delMatno, boundType);
            fileRelDel = fileRelDelService.queryFileRelDelCusNo(custId, delMatno, boundType);
            //下載模板並預覽   1.查询模板是否存在， 若存在不下载  2.若不存在下载模板
            if (fileRelDel != null)
            {
                ctCodeInfo.Modelno = fileRelDel.FileNo;
                filePath = modelInfoService.previewModelFile(fileRelDel.FileNo);
                if (filePath != null)
                {
                    string pictureFile = barPrint.PreviewPrintBC(filePath);
                    this.pictureBox1.Load(pictureFile);
                }
            }
        }


        private void countBoxQty(int capacity, string workNo, string cusPo)
        {

            int poPackedQty = int.Parse(ctCodeService.getCTQtyByWoAndCusPo(workNo, cusPo));                   //查询工单、PO已装箱数量
            int poRecord = int.Parse(ctCodeService.getGeneratedCTCountByPO(workNo, cusPo));
            //判断包装数量是否为0 并且CT 条码数是否为0
            int packedQty = 0;
            if (poPackedQty == 0 && poRecord != 0)
            {
                packedQty = poRecord;
            }
            else
            {
                packedQty = poPackedQty;
            }

            int woQty = int.Parse(this.textBox4.Text);
            int poQty = int.Parse(this.textBox2.Text);
            int surplus = 0;
            if (poQty > woQty)
            {
                surplus = woQty - packedQty;
            }
            else
            {
                surplus = poQty - packedQty;
            }
            //计算剩余可装箱数
            int countBoxs = (int)Math.Ceiling((double)surplus / capacity);

            //this.numericUpDown2.Maximum = countBoxs;
            this.numericUpDown1.Value = countBoxs;
            this.textBox15.Text = poRecord.ToString();                                                         //计算工单，PO 已产生CT条码数
            this.textBox16.Text = countBoxs.ToString();
            if (surplus > capacity)
            {
                this.numericUpDown2.Maximum = capacity;
                this.numericUpDown2.Value = capacity;
            }
            else
            {
                //this.numericUpDown1.Maximum = surplus;
                if(surplus > 0)
                {
                    this.numericUpDown2.Value = surplus;
                }else
                {
                    this.numericUpDown2.Value = 0;
                }
            }

            ////獲得子階料號
            //if (this.comboBox6.SelectedValue != null && this.comboBox6.SelectedValue.ToString().Trim() != "")
            //{
            //    this.textBox13.Text = printQ.getCTCountBySubMat(this.textBox1.Text, this.comboBox6.SelectedValue.ToString());
            //}
            //else
            //{
            //    this.textBox13.Text = "0";
            //}
            ////判斷是否以子階工單計算剩餘打印數量
            //if (this.comboBox7.SelectedValue.ToString() == "1")
            //{
            //    if (poQty > woQty)
            //    {
            //        return woQty - int.Parse(this.textBox7.Text.Trim().ToString());
            //    }
            //    else
            //    {
            //        return poQty - int.Parse(this.textBox8.Text.Trim().ToString());
            //    }
            //}
            //else
            //{
            //    if (poQty > woQty)
            //    {
            //        return woQty - int.Parse(this.textBox13.Text.Trim().ToString());
            //    }
            //    else
            //    {
            //        return poQty - int.Parse(this.textBox13.Text.Trim().ToString());
            //    }
            //}

        }


    }
}
