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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CTCodePrint
{
    public partial class CartonDirectPrint : Form
    {
        public CartonDirectPrint()
        {
            InitializeComponent();
        }
        private Carton carton;
        CartonService cartonService = new CartonService();
        private readonly CTCodeService ctCodeService = new CTCodeService();
        private readonly GenerateCarton generateCarton = new GenerateCarton();
        private readonly BarCodePrint barPrint = BarCodePrint.getInstance();
        private readonly MandRelDelService mandRelDelService = new MandRelDelService();                 //查詢模板的字段規則
        private readonly ManRelFieldTypeService manRelFieldTypeService = new ManRelFieldTypeService();  //根據字段規則 查詢字段規則值
        private readonly ModelInfoService modelInfoService = new ModelInfoService();                    //查詢模板內容並下載
        private readonly DepartmentService departmentService = new DepartmentService();
        private readonly ProdLineService prodLineService = new ProdLineService();
        private readonly OracleQueryB queryB = new OracleQueryB();                      //Oracle 查询
        private readonly CapacityService capacityService = new CapacityService();
        private readonly CusRuleService cusRuleService = new CusRuleService();
        private readonly CodeRuleService codeRuleService = new CodeRuleService();

        private string filePath = null;


        private void button1_Click(object sender, EventArgs e)
        {
            printCarton();
        }

        private void CreateRules_Load(object sender, EventArgs e)
        {
            //初始化 部門下拉列表;
            List<Department> listDepartment = departmentService.queryDepartmentList("");
            this.comboBox1.ValueMember = "DeptId";
            this.comboBox1.DisplayMember = "DeptName";
            this.comboBox1.DataSource = listDepartment;
            this.comboBox1.SelectedIndex = 0;


            DataTable itemTable2 = new DataTable();   // construct selects value
            DataColumn columnType;
            DataRow rowType;
            columnType = new DataColumn("Name");
            itemTable2.Columns.Add(columnType);
            columnType = new DataColumn("Value");
            itemTable2.Columns.Add(columnType);
            rowType = itemTable2.NewRow();
            rowType["Name"] = "yyyyMMdd";
            rowType["Value"] = "yyyyMMdd";
            itemTable2.Rows.Add(rowType);
            rowType = itemTable2.NewRow();
            rowType["Name"] = "yyyy-MM-dd";
            rowType["Value"] = "yyyy-MM-dd";
            itemTable2.Rows.Add(rowType);
            rowType = itemTable2.NewRow();
            rowType["Name"] = "yyyy/MM/dd";
            rowType["Value"] = "yyyy/MM/dd";
            itemTable2.Rows.Add(rowType);
            this.comboBox6.DisplayMember = "Name";
            this.comboBox6.ValueMember = "Value";
            this.comboBox6.DataSource = itemTable2;
        }


        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if (this.textBox1.Text == null || this.textBox1.Text.Trim() == "")
            {
                this.textBox1.Focus();
                return;
            }
            string workno = this.textBox1.Text.Trim();
            List<WorkOrderInfo> workOrderInfos = queryB.getWorkInfoByNo(workno.ToUpper());
            if (workOrderInfos != null && workOrderInfos.Count > 0)
            {

                carton = new Carton();
                this.comboBox3.DataSource = workOrderInfos;
                this.comboBox3.DisplayMember = "CustPO";                                    //PO                    
                this.comboBox3.ValueMember = "OrderQty";                                    //QTY
                this.textBox4.Text = workOrderInfos[0].StartQty;
                this.textBox6.Text = workOrderInfos[0].CustName;                            
                this.textBox7.Text = workOrderInfos[0].CustId;                              
                this.textBox8.Text = workOrderInfos[0].ItemCode;                          
                this.textBox13.Text = workOrderInfos[0].OrderQty;                           
                if (workOrderInfos[0].CusItemNum != null && workOrderInfos[0].CusItemNum.Trim() != "")
                {
                    this.textBox9.Text = workOrderInfos[0].CusItemNum;
                    carton.Cusmatno = workOrderInfos[0].CusItemNum;
                }
                else
                {
                    List<CusMatInfo> cusMatInfos = queryB.getCusMatInfo(workno);
                    if (cusMatInfos.Count > 0)
                    {
                        this.textBox9.Text = cusMatInfos[0].CusItemCode;
                        carton.Cusmatno = cusMatInfos[0].CusItemCode;
                        this.textBox11.Text = cusMatInfos[0].CusItemDesc;
                        carton.CusmatDesc = cusMatInfos[0].CusItemDesc;

                    }
                }
                Capacity capacity = capacityService.queryByRelation(workOrderInfos[0].CustId, workOrderInfos[0].ItemCode,"1");
                //下载打印模板 並初始化Carton 基本信息
                this.comboBox4.DisplayMember = "Revision";
                this.comboBox4.ValueMember = "Revision";
                this.comboBox4.DataSource = queryB.getRevisionInfo(workno);

                this.comboBox5.DataSource = workOrderInfos;
                this.comboBox5.DisplayMember = "SoOrder";                        //SO                 
                this.comboBox5.ValueMember = "SoOrder";

                carton.Workno = workno.ToUpper();
                carton.Delmatno = workOrderInfos[0].ItemCode;
                carton.Cusno = workOrderInfos[0].CustId;
                carton.PackType = "1";                  //包裝類型：0 整機， 1單出
                carton.Cusname = workOrderInfos[0].CustName;
                carton.Orderqty = workOrderInfos[0].OrderQty;
                carton.Woquantity = workOrderInfos[0].StartQty;
                carton.SoOrder = workOrderInfos[0].SoOrder;
                carton.Completedqty = workOrderInfos[0].CompletedQty;            //CUST_PO_NUMBER
                carton.Cuspo = workOrderInfos[0].CustPO;
                carton.Verno = "A10";
                carton.CapacityNo = capacity.Capacityno;
                if (capacity != null)
                {
                    this.countBoxQty(capacity);
                }
                CusRule cusRule = cusRuleService.queryCusRuleByCond(carton.Cusno, carton.Delmatno, "1");                //1代表裝箱單編碼規則;
                if (cusRule != null && cusRule.Ruleno != "")
                {
                    CodeRule codeRule = codeRuleService.queryRuleById(cusRule.Ruleno);
                    this.textBox2.Text = codeRule.RuleDesc;
                    carton.Ruleno = codeRule.Ruleno;
                }
                ProdLine prodLine = new ProdLine();
                carton.ProdLine = prodLine;
                carton.ProdLine.ProdlineId = this.comboBox2.SelectedValue.ToString();
                carton.Modelno = cartonService.queryFileNo(carton.Cusno, carton.Delmatno, "1");                //TYPE= 1 為裝箱單模板
                carton.CartonStatus = "0";
                carton = GenerateCarton.generateCartonNo(carton);
                //顯示箱號：
                this.textBox10.Text = carton.CartonNo;
                this.textBox5.Text = cartonService.getCartonQtyByWO(carton.Workno);
                this.textBox3.Text = carton.BatchNo;
                //下載模板並預覽
                ModelFile modelFile = modelInfoService.queryModelFileByNo(carton.Modelno);
                if (modelFile != null)
                {
                    filePath = Auxiliary.downloadModelFile(modelFile);
                    string pictureFile = barPrint.PreviewPrintBC(filePath);
                    this.pictureBox1.Load(pictureFile);

                }

            }

        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string deptId = this.comboBox1.SelectedValue.ToString();
            List<ProdLine> prodLineList = prodLineService.queryPLByDeptId(deptId);
            this.comboBox2.ValueMember = "ProdlineId";
            this.comboBox2.DisplayMember = "ProdlineName";
            this.comboBox2.DataSource = prodLineList;
            this.comboBox2.SelectedIndex = 0;
        }

        private void clearAll()
        {
            this.textBox4.Text = "";
        }

        /// <summary>
        ///  计算装箱容量 上限
        /// </summary>
        /// <param name="capacity"></param>
        private void countBoxQty(Capacity capacity)
        {
            if (capacity != null)
            {
                int packedQty = int.Parse(cartonService.getCartonQtyByWO(carton.Workno));   //已装箱数量
                int workQty = int.Parse(this.textBox4.Text);
                int surplus = workQty - packedQty;
                if (surplus > capacity.Capacityqty)
                {
                    this.numericUpDown1.Maximum = capacity.Capacityqty;
                    this.numericUpDown1.Value = capacity.Capacityqty;
                }
                else
                {
                    this.numericUpDown1.Maximum = surplus;
                    this.numericUpDown1.Value = surplus;
                }

            }
        }


        /// <summary>
        /// TODO 打印裝箱單
        /// </summary>
        private void printCarton()
        {
            this.textBox5.Text = cartonService.getCartonQtyByWO(carton.Workno);
            MandRelDel mandRelDel = mandRelDelService.queryManNoByDel(carton.Cusno, carton.Delmatno, "1");
            if (mandRelDel == null)
            {
                MessageBox.Show("未找到該客戶出貨料號對應的打印字段規則信息，請維護相關信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //查詢字段對應的規則信息
            List<MandUnionFieldType> mandUnionFieldTypeList = manRelFieldTypeService.queryMandUnionFieldTypeList(mandRelDel.ManNo);
            if (mandUnionFieldTypeList == null)
            {
                MessageBox.Show("未找到該客戶出貨料號對應的打印字段規則信息，請維護相關信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            carton.ProdLine.ProdlineId = this.comboBox2.SelectedValue.ToString();                  //打印時 獲得線別編號
            carton.ProdLineVal = this.comboBox2.Text;
            carton.CartonQty = (int)this.numericUpDown1.Value;                        //裝箱單數量
            carton.Datecode = DateTime.Now.ToString(comboBox6.Text);
            carton = GenerateCarton.generateCartonNo(carton);
            this.textBox10.Text = carton.CartonNo;
            foreach (MandUnionFieldType mandTYpe in mandUnionFieldTypeList)
            {
                if (mandTYpe.FieldName.ToUpper().Equals("SpecialField".ToUpper()))
                {
                    StringBuilder special = new StringBuilder();
                    string cusPn = "";
                    string verNo = "";
                    foreach (MandUnionFieldType mandTYpe2 in mandUnionFieldTypeList)
                    {
                        if (mandTYpe2.FieldName.ToUpper().Equals("CusPN".ToUpper()))
                        {
                            cusPn = mandTYpe2.FieldValue;

                        }
                        if (mandTYpe2.FieldName.ToUpper().Equals("versionNo".ToUpper()))
                        {
                            verNo = mandTYpe2.FieldValue;
                        }
                    }
                    special.Append(cusPn);
                    special.Append(" {{" + carton.CartonQty + " {{PCS {{041098 {{" + carton.Datecode + " {{CHN {{" + carton.Cuspo + " {{/ {{" + carton.CartonNo + " {{" + carton.Datecode + " {{XY {{" + carton.Delmatno + " {{");
                    special.Append(verNo);
                    carton.SpecialField = special.ToString();
                }
                if (mandTYpe.FieldName.ToUpper().Equals("UnionField".ToUpper()))
                {
                    carton.UnionField = "BB" + carton.CartonNo + "||P" + carton.Cusmatno + "Q" + carton.CartonQty + "||1P" + carton.Delmatno;
                }
                if (mandTYpe.FieldName.ToUpper().Equals("BoxNo".ToUpper()))
                {

                    int currentNumber = cartonService.queryCurrentBoxQty(carton.Workno);
                    carton.BoxNo = currentNumber == 0 ? 1 : currentNumber;
                }
            }

            bool judgePrint = true;
            for (int i = 0; i < 2; i++)
            {
                judgePrint = barPrint.printCatonByModel(filePath, carton, mandUnionFieldTypeList);
            }
            if (judgePrint)
            {
                if (!cartonService.saveCarton(carton))
                {
                    MessageBox.Show("保存裝箱單失敗！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                MessageBox.Show("打印失敗請聯係管理員！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }



        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox3.SelectedValue != null && this.comboBox3.SelectedValue.ToString().Trim() != "")
            {
                this.textBox13.Text = this.comboBox3.SelectedValue.ToString().Trim();
                carton.Cuspo = this.comboBox3.Text;

                List<WorkOrderInfo> workOrders = (List<WorkOrderInfo>)this.comboBox3.DataSource;
                WorkOrderInfo workOrder = workOrders[this.comboBox3.SelectedIndex];
                this.textBox6.Text = workOrder.CustName;
                this.textBox7.Text = workOrder.CustId;
            }
            carton = GenerateCarton.generateCartonNo(carton);
            this.textBox10.Text = carton.CartonNo;
        }
    }
}
