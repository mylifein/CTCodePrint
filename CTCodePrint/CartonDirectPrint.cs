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
using System.Reflection;
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
        private Capacity capacity;
        private CodeRule codeRule;
        private FileRelDel fileRelDel;
        private List<MandUnionFieldType> mandUnionFieldTypeList;
        private readonly CartonService cartonService = new CartonService();
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
        private readonly CodeRuleService codeRuleService = new CodeRuleService();
        private readonly UserService userService = new UserService();
        private readonly FileRelDelService fileRelDelService = new FileRelDelService();     //查詢模板的文件編號

        private string filePath = null;
        private ProdLine userProdLine = null;
        private string workNo = "";

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.numericUpDown2.Value; i++)
            {
                printCarton(true);
            }
        }

        private void CreateRules_Load(object sender, EventArgs e)
        {
            //初始化 部門下拉列表;
            List<Department> listDepartment = departmentService.queryDepartmentList("");
            this.comboBox1.ValueMember = "DeptId";
            this.comboBox1.DisplayMember = "DeptName";
            this.comboBox1.DataSource = listDepartment;
            this.comboBox1.SelectedIndex = 0;

            User user = userService.queryByUsername(Auxiliary.loginName);
            if (user.ProdLine != null && !("").Equals(user.ProdLine))
            {
                this.comboBox1.Enabled = false;
                userProdLine = prodLineService.queryProdLineById(user.ProdLine);
                if (userProdLine != null)
                {
                    var items = this.comboBox1.Items;
                    for (int i = 0; i < items.Count; i++)
                    {
                        Department iteratorDept = items[i] as Department;
                        if (userProdLine.Department.DeptId.Equals(iteratorDept.DeptId))
                        {
                            this.comboBox1.SelectedItem = iteratorDept;
                            break;
                        }
                    }
                }
            }
            string deptId = this.comboBox1.SelectedValue.ToString();
            List<ProdLine> prodLineList = prodLineService.queryPLByDeptId(deptId);
            this.comboBox2.ValueMember = "ProdlineId";
            this.comboBox2.DisplayMember = "ProdlineName";
            this.comboBox2.DataSource = prodLineList;
            this.comboBox2.SelectedIndex = 0;
            if (userProdLine != null)
            {
                this.comboBox2.Enabled = false;
                var items = this.comboBox2.Items;
                for (int i = 0; i < items.Count; i++)
                {
                    ProdLine iteratorProdLine = items[i] as ProdLine;
                    if (userProdLine.ProdlineId.Equals(iteratorProdLine.ProdlineId))
                    {
                        this.comboBox2.SelectedItem = iteratorProdLine;
                        break;
                    }
                }
            }
            DataTable itemTable2 = new DataTable();   // construct selects value
            DataColumn columnType;
            DataRow rowType;
            columnType = new DataColumn("Name");
            itemTable2.Columns.Add(columnType);
            columnType = new DataColumn("Value");
            itemTable2.Columns.Add(columnType);
            rowType = itemTable2.NewRow();
            rowType["Name"] = "yyyy-MM-dd";
            rowType["Value"] = "yyyy-MM-dd";
            itemTable2.Rows.Add(rowType);
            rowType = itemTable2.NewRow();
            rowType["Name"] = "yyyyMMdd";
            rowType["Value"] = "yyyyMMdd";
            itemTable2.Rows.Add(rowType);
            rowType = itemTable2.NewRow();
            rowType["Name"] = "yyyy/MM/dd";
            rowType["Value"] = "yyyy/MM/dd";
            itemTable2.Rows.Add(rowType);
            this.comboBox6.DisplayMember = "Name";
            this.comboBox6.ValueMember = "Value";
            this.comboBox6.DataSource = itemTable2;
            this.numericUpDown3.Value = 2;              //设置默认打印张数：2
            this.numericUpDown2.Value = 1;
        }


        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.textBox1.Text == null || this.textBox1.Text.Trim() == "")
                {
                    this.textBox1.Focus();
                    return;
                }
                string workno = this.textBox1.Text.Trim().ToUpper();
                List<WorkOrderInfo> workOrderInfos = queryB.getWorkInfoByNo(workno);
                if (workOrderInfos != null && workOrderInfos.Count > 0)
                {
                    this.comboBox3.DataSource = workOrderInfos;
                    this.comboBox3.DisplayMember = "CustPO";                                    //PO                    
                    this.comboBox3.ValueMember = "OrderQty";                                    //QTY
                    this.textBox4.Text = workOrderInfos[0].StartQty;
                    this.textBox6.Text = workOrderInfos[0].CustName;
                    this.textBox7.Text = workOrderInfos[0].CustId;
                    this.textBox8.Text = workOrderInfos[0].ItemCode;
                    this.textBox13.Text = workOrderInfos[0].OrderQty;
                    this.textBox17.Text = workOrderInfos[0].SoOrder;
                    workNo = workOrderInfos[0].WorkNo;
                    this.textBox5.Text = cartonService.getCartonQtyByWO(workno);
                    if (workOrderInfos[0].CusItemNum != null)                                   // && workOrderInfos[0].CusItemNum.Trim() != ""
                    {
                        this.textBox9.Text = workOrderInfos[0].CusItemNum;
                    }
                    //else
                    //{
                    //    List<CusMatInfo> cusMatInfos = queryB.getCusMatInfo(workno);
                    //    if (cusMatInfos != null && cusMatInfos.Count > 0)
                    //    {
                    //        this.textBox9.Text = cusMatInfos[0].CusItemCode;
                    //        this.textBox11.Text = cusMatInfos[0].CusItemDesc;
                    //    }
                    //}
                    this.comboBox4.DisplayMember = "Revision";
                    this.comboBox4.ValueMember = "Revision";
                    this.comboBox4.DataSource = queryB.getRevisionInfo(workno);
                    loadResources(workOrderInfos[0].CustId, workOrderInfos[0].ItemCode, "1", workno, workOrderInfos[0].CustPO);         //加载打印资源，并计算可打印数量

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
            if (userProdLine != null)
            {
                this.comboBox2.Enabled = false;
                var items = this.comboBox2.Items;
                for (int i = 0; i < items.Count; i++)
                {
                    ProdLine iteratorProdLine = items[i] as ProdLine;
                    if (userProdLine.ProdlineId.Equals(iteratorProdLine.ProdlineId))
                    {
                        this.comboBox2.SelectedItem = iteratorProdLine;
                        break;
                    }
                }
            }
        }


        public void loadResources(string custId, string delMatno, string boundType, string workno, string cusPo)
        {
            capacity = capacityService.queryByRelation(custId, delMatno, boundType);
            if (capacity != null)
            {
                this.countBoxQty(capacity.Capacityqty, workno, cusPo);
                this.textBox12.Text = capacity.Capacitydesc;
                this.textBox14.Text = capacity.Capacityqty.ToString();
            }

            codeRule = codeRuleService.queryRuleByCond(custId, delMatno, boundType);  //1代表Carton碼編碼規則
            if (codeRule != null)
            {
                this.textBox2.Text = codeRule.RuleDesc;
            }
            mandUnionFieldTypeList = manRelFieldTypeService.queryFieldListByCond(custId, delMatno, boundType);
            fileRelDel = fileRelDelService.queryFileRelDelCusNo(custId, delMatno, boundType);
            //下載模板並預覽   1.查询模板是否存在， 若存在不下载  2.若不存在下载模板
            if (fileRelDel != null)
            {
                filePath = modelInfoService.previewModelFile(fileRelDel.FileNo);
                //if (filePath != null)
                //{
                //    string pictureFile = barPrint.PreviewPrintBC(filePath);
                //    this.pictureBox1.Load(pictureFile);
                //}
            }
        }


        private void clearAll()
        {
            this.textBox4.Text = "";
        }

        /// <summary>
        ///  计算装箱容量 上限
        /// </summary>
        /// <param name="capacity"></param>
        private void countBoxQty(int capacity, string workNo, string cusPo)
        {
            if (capacity != null && this.textBox13.Text != null && this.textBox13.Text != "")
            {
                int packedQty = int.Parse(cartonService.getCartonQtyByWoAndCusPo(workNo, cusPo));                   //查询工单、PO已装箱数量
                int poQty = int.Parse(this.textBox13.Text);
                int surplus = poQty - packedQty;                                                                   //获得PO剩余可装箱数量
                //计算剩余可装箱数
                int countBoxs = (int)Math.Ceiling((double)surplus / capacity);
                this.numericUpDown2.Maximum = countBoxs;
                this.textBox15.Text = cartonService.getCartonsByWoAndCusPo(workNo, cusPo).ToString();             //计算工单，PO已装箱数
                this.textBox16.Text = countBoxs.ToString();
                if (surplus > capacity)
                {
                    this.numericUpDown1.Maximum = capacity;
                    this.numericUpDown1.Value = capacity;
                }
                else
                {
                    if (surplus > 0)
                    {
                        this.numericUpDown1.Maximum = surplus;
                        this.numericUpDown1.Value = surplus;
                    }else
                    {
                        this.numericUpDown1.Maximum = capacity;
                        this.numericUpDown1.Value = capacity;
                    }
                   
                }
            }
        }


        /// <summary>
        /// TODO 打印裝箱單
        /// </summary>
        private void printCarton(bool isSave)
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
            Carton carton = new Carton();
            carton.ProdId = this.comboBox2.SelectedValue.ToString();                  //打印時 獲得線別編號
            carton.ProdLineVal = this.comboBox2.Text;
            carton.CartonQty = (int)this.numericUpDown1.Value;                        //裝箱單數量
            carton.Datecode = dateTimePicker1.Value.ToString(comboBox6.Text);
            carton.Ruleno = codeRule.Ruleno;
            carton.Workno = workNo;
            carton.Delmatno = this.textBox8.Text;
            carton.Cusmatno = this.textBox9.Text;
            carton.Cusno = this.textBox7.Text;
            carton.PackType = "1";                                                   //包裝類型：0 整機， 1單出
            carton.Cusname = this.textBox6.Text;
            carton.Orderqty = this.textBox13.Text;
            carton.Woquantity = this.textBox4.Text;
            carton.SoOrder = this.textBox17.Text;
            carton.Cuspo = this.comboBox3.Text == null ? "" : this.comboBox3.Text.ToString().Trim();            //Cuspo
            carton.ProdId = this.comboBox2.SelectedValue.ToString();
            carton.Modelno = fileRelDel.FileNo;                                      //TYPE= 1 為裝箱單模板
            if (capacity != null)
            {
                carton.CapacityNo = capacity.Capacityno;
            }
            carton.CartonStatus = "0";
            carton = GenerateCarton.generateCartonNo(carton,codeRule, dateTimePicker1.Value);
            //顯示箱號：
            this.textBox10.Text = carton.CartonNo; ;
            this.textBox3.Text = carton.BatchNo;

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
                    carton.UnionField = "BB" + carton.CartonNo + "||P" + carton.Cusmatno + "||Q" + carton.CartonQty + "||1P" + carton.Delmatno;
                }
                if (mandTYpe.FieldName.ToUpper().Equals("BoxNo".ToUpper()))
                {
                    if (mandTYpe.FieldValue.ToUpper().Equals("WorkQty".ToUpper()))                                     //当特殊字段为 四位工单流水 +  P + 4位工单数量
                    {
                        int currentNumber = cartonService.currentBoxQtyByCuspo(carton.Cuspo, carton.Delmatno,carton.Workno);
                        currentNumber = currentNumber == 0 ? 1 : (currentNumber + 1);
                        string tempStr = "";
                        for (int i = currentNumber.ToString().Length; i < 4; i++)
                        {
                            tempStr = tempStr + "0";
                        }
                        string prefix = tempStr + currentNumber.ToString();
                        string tempStr2 = "";
                        string suffix = "";
                        if (int.Parse(carton.Woquantity) < int.Parse(carton.Orderqty))
                        {
                            for (int i = carton.Woquantity.Length; i < 4; i++)
                            {
                                tempStr2 = tempStr2 + "0";
                            }
                            suffix = tempStr2 + carton.Woquantity;
                        }
                        else
                        {
                            for (int i = carton.Orderqty.Length; i < 4; i++)
                            {
                                tempStr2 = tempStr2 + "0";
                            }
                            suffix = tempStr2 + carton.Orderqty;
                        }
                        carton.BoxNo = prefix + "P/" + suffix;
                    }
                    else
                    {
                        int currentNumber = cartonService.queryCurrentBoxQty(carton.Workno);
                        currentNumber = currentNumber == 0 ? 1 : (currentNumber + 1);
                        string tempStr = "";
                        for (int i = currentNumber.ToString().Length; i < 3; i++)
                        {
                            tempStr = tempStr + "0";
                        }
                        carton.BoxNo = tempStr + currentNumber.ToString();
                    }
                }
            }

            bool judgePrint = barPrint.bactchPrintCartonByModel(filePath, cartonListToArray(carton, mandUnionFieldTypeList, (int)this.numericUpDown3.Value));
            if (judgePrint)
            {
                if (isSave)
                {
                    if (!cartonService.saveCarton(carton))
                    {
                        MessageBox.Show("保存裝箱單失敗！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("打印失敗請聯係管理員！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }



        private void button2_Click(object sender, EventArgs e)
        {
            printCarton(false);
        }


        private List<Dictionary<string, string>> cartonListToArray(Carton carton, List<MandUnionFieldType> mandUnionFieldTypeList, int printNum)
        {
            List<Dictionary<string, string>> cartonList = new List<Dictionary<string, string>>();
            PropertyInfo[] propertyInfoARR = carton.GetType().GetProperties();
            Dictionary<string, string> property = new Dictionary<string, string>();
            foreach (PropertyInfo propertyInfo in propertyInfoARR)
            {
                Object propertyVal = carton.GetType().GetProperty(propertyInfo.Name).GetValue(carton, null);
                if (propertyVal != null)
                {
                    property.Add(propertyInfo.Name.ToUpper(), propertyVal.ToString());
                }
            }
            for (int i = 0; i < printNum; i++)
            {
                Dictionary<string, string> cartonDict = new Dictionary<string, string>();
                foreach (MandUnionFieldType mandUnionFieldType in mandUnionFieldTypeList)
                {
                    string fieldName = mandUnionFieldType.FieldName.ToUpper();
                    if (property.ContainsKey(fieldName))
                    {
                        cartonDict.Add(fieldName, property[fieldName]);
                    }
                    else
                    {
                        cartonDict.Add(fieldName, mandUnionFieldType.FieldValue);
                    }
                }
                cartonList.Add(cartonDict);
            }
            return cartonList;
        }

        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.comboBox3.SelectedValue != null && this.comboBox3.SelectedValue.ToString().Trim() != "")
            {
                List<WorkOrderInfo> workOrders = (List<WorkOrderInfo>)this.comboBox3.DataSource;
                WorkOrderInfo workOrder = workOrders[this.comboBox3.SelectedIndex];
                this.textBox6.Text = workOrder.CustName;
                this.textBox7.Text = workOrder.CustId;
                this.textBox17.Text = workOrder.SoOrder;
                this.textBox13.Text = workOrder.OrderQty;
                loadResources(workOrder.CustId, workOrder.ItemCode, "1", workOrder.WorkNo, workOrder.CustPO);         //加载打印资源，并计算可打印数量
            }
        }
    }
}
