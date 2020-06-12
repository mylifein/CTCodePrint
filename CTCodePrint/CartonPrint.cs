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
    public partial class CartonPrint : Form
    {
        public CartonPrint()
        {
            InitializeComponent();
        }
        private CTCode firstScan;
        private Carton carton;
        private Capacity capacity;
        private List<String> ctCodeList = new List<string>();
        CartonService cartonService = new CartonService();
        private readonly CTCodeService ctCodeService = new CTCodeService();
        private readonly GenerateCarton generateCarton = new GenerateCarton();
        private readonly BarCodePrint barPrint = BarCodePrint.getInstance();
        private readonly MandRelDelService mandRelDelService = new MandRelDelService();                 //查詢模板的字段規則
        private readonly ManRelFieldTypeService manRelFieldTypeService = new ManRelFieldTypeService();  //根據字段規則 查詢字段規則值
        private readonly ModelInfoService modelInfoService = new ModelInfoService();                    //查詢模板內容並下載
        private readonly DepartmentService departmentService = new DepartmentService();
        private readonly ProdLineService prodLineService = new ProdLineService();
        private readonly CapacityService capacityService = new CapacityService();
        private readonly FileRelDelService fileRelDelService = new FileRelDelService();     //查詢模板的文件編號
        private readonly CusRuleService cusRuleService = new CusRuleService();
        private readonly CodeRuleService codeRuleService = new CodeRuleService();

        private string filePath = null;
        public CTCode FirstScan
        {
            get
            {
                return firstScan;
            }

            set
            {
                firstScan = value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printCarton();
        }

        private void CreateRules_Load(object sender, EventArgs e)
        {

            this.numericUpDown3.Value = 2;              //设置默认打印张数：2
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = "ctCode";
            column.DataPropertyName = "ct_code";//对应数据源的字段
            column.HeaderText = "CT碼";
            column.Width = 200;
            this.dataGridView1.Columns.Add(column);
            DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
            column1.Name = "delMatno";
            column1.DataPropertyName = "del_matno";//对应数据源的字段
            column1.HeaderText = "出貨料號";
            column1.Width = 200;
            this.dataGridView1.Columns.Add(column1);
            DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
            column2.Name = "cusName";
            column2.DataPropertyName = "cus_name";//对应数据源的字段
            column2.HeaderText = "客戶名稱";
            column2.Width = 200;
            this.dataGridView1.Columns.Add(column2);

            DataGridViewTextBoxColumn column3 = new DataGridViewTextBoxColumn();
            column3.Name = "cusNo";
            column3.DataPropertyName = "cus_no";//对应数据源的字段
            column3.HeaderText = "客戶編號";
            column3.Width = 80;
            this.dataGridView1.Columns.Add(column3);

            DataGridViewTextBoxColumn column4 = new DataGridViewTextBoxColumn();
            column4.Name = "workNo";
            column4.DataPropertyName = "work_no";//对应数据源的字段
            column4.HeaderText = "工單號";
            column4.Width = 100;
            this.dataGridView1.Columns.Add(column4);

            DataGridViewTextBoxColumn column5 = new DataGridViewTextBoxColumn();
            column5.Name = "woQuantity";
            column5.DataPropertyName = "wo_quantity";//对应数据源的字段
            column5.HeaderText = "工單數量";
            column5.Width = 80;
            this.dataGridView1.Columns.Add(column5);

            DataGridViewTextBoxColumn column6 = new DataGridViewTextBoxColumn();
            column6.Name = "cusMatno";
            column6.DataPropertyName = "cus_matno";//对应数据源的字段
            column6.HeaderText = "客戶料號";
            column6.Width = 200;
            this.dataGridView1.Columns.Add(column6);

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
            this.comboBox3.DisplayMember = "Name";
            this.comboBox3.ValueMember = "Value";
            this.comboBox3.DataSource = itemTable2;
        }





        private void button3_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("請選中需要刪除的行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.ctCodeList.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
            this.dataGridView1.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
        }




        /// <summary>
        /// TODO 初始化行信息
        /// </summary>
        /// <param name="index"></param>
        /// <param name="ctcode"></param>
        public void initRow(int index, CTCode ctcode)
        {
            this.dataGridView1.Rows[index].Cells[0].Value = ctcode.Ctcode;
            this.dataGridView1.Rows[index].Cells[1].Value = ctcode.Delmatno;
            this.dataGridView1.Rows[index].Cells[2].Value = ctcode.Cusname;
            this.dataGridView1.Rows[index].Cells[3].Value = ctcode.Cusno;
            this.dataGridView1.Rows[index].Cells[4].Value = ctcode.Workno;
            this.dataGridView1.Rows[index].Cells[5].Value = ctcode.Woquantity;
            this.dataGridView1.Rows[index].Cells[6].Value = ctcode.Cusmatno;
            this.textBox1.Text = "";
            ctCodeList.Add(ctcode.Ctcode);

        }

        public bool checkRepeat(string ctCode)
        {
            foreach (string compareValue in ctCodeList)
            {
                if (compareValue.Equals(ctCode))
                {
                    return true;
                }
            }
            return false;

        }

        /// <summary>
        /// TODO 將集合中的CT碼 賦值到carton中
        /// </summary>
        /// <param name="ctcodeList"></param>
        private void initCTSeq(List<String> ctcodeList)
        {
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string deptId = this.comboBox1.SelectedValue.ToString();
            List<ProdLine> prodLineList = prodLineService.queryPLByDeptId(deptId);
            this.comboBox2.ValueMember = "ProdlineId";
            this.comboBox2.DisplayMember = "ProdlineName";
            this.comboBox2.DataSource = prodLineList;
            this.comboBox2.SelectedIndex = 0;
        }



        /// <summary>
        /// TODO 打印裝箱單
        /// </summary>
        private void printCarton()
        {
            //打印装箱单号
            if (this.dataGridView1.Rows.Count > 0)
            {

                MandRelDel mandRelDel = mandRelDelService.queryManNoByDel(carton.Cusno, carton.Delmatno, "1");
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
                carton.ProdLine.ProdlineId = this.comboBox2.SelectedValue.ToString();                  //打印時 獲得線別編號
                carton.ProdLineVal = this.comboBox2.Text;
                carton.CartonQty = Convert.ToInt32(this.textBox3.Text.Trim());                         //裝箱單數量
                initCTSeq(ctCodeList);
                carton.CtCodeList = ctCodeList;
                carton.Datecode = dateTimePicker1.Value.ToString(comboBox3.Text);
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
                            if (mandTYpe2.FieldName.ToUpper().Equals("Versionno".ToUpper()))
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
                        if (mandTYpe.FieldValue.ToUpper().Equals("WorkQty".ToUpper()))                                     //当特殊字段为 四位工单流水 +  P + 4位工单数量
                        {
                            int currentNumber = cartonService.currentBoxQtyByCuspo(carton.Cuspo,carton.Delmatno);
                            currentNumber = currentNumber == 0 ? 1 : (currentNumber + 1);
                            string tempStr = "";
                            for (int i = currentNumber.ToString().Length; i < 4; i++)
                            {
                                tempStr = tempStr + "0";
                            }
                            string prefix = tempStr + currentNumber.ToString();
                            string tempStr2 = "";
                            for (int i = carton.Woquantity.Length; i < 4; i++)
                            {
                                tempStr2 = tempStr2 + "0";
                            }
                            string suffix = tempStr2 + carton.Orderqty;
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

                carton = GenerateCarton.generateCartonNo(carton);
                bool judgePrint = true;
                judgePrint = judgePrint = barPrint.bactchPrintCartonByModel(filePath, cartonListToArray(carton, mandUnionFieldTypeList, (int)this.numericUpDown3.Value));
                if (judgePrint)
                {
                    if (!cartonService.saveCarton(carton))
                    {
                        MessageBox.Show("保存裝箱單失敗！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    clear();

                }
                else
                {
                    MessageBox.Show("打印失敗請聯係管理員！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void clear()
        {
            firstScan = null;
            carton = null;
            capacity = null;
            ctCodeList = new List<string>();
            this.dataGridView1.Rows.Clear();
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.textBox3.Text = "";
            this.textBox4.Text = "";
            this.textBox5.Text = "";
            this.textBox6.Text = "";
            this.textBox7.Text = "";
            this.textBox8.Text = "";
            this.textBox9.Text = "";
            this.textBox10.Text = "";
            this.textBox11.Text = "";
            this.textBox12.Text = "";
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
                CTCode ctcode = ctCodeService.queryCTCodeByCtcode(this.textBox1.Text.Trim());
                if (ctcode == null)
                {
                    this.textBox1.Text = "";
                    this.textBox1.Focus();
                    return;
                }
                if (cartonService.exists(ctcode.Ctcode))
                {
                    MessageBox.Show("該CT碼已綁定，請檢查！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.textBox1.Text = "";
                    this.textBox1.Focus();
                    return;
                }
                if (this.dataGridView1.Rows.Count == 0)
                {
                    carton = new Carton();
                    firstScan = ctcode;
                    this.textBox2.Text = ctcode.Workno;
                    int index = this.dataGridView1.Rows.Add();
                    initRow(index, ctcode);
                    //初始化信息
                    this.textBox3.Text = this.dataGridView1.Rows.Count.ToString();
                    this.textBox4.Text = firstScan.Woquantity;
                    //根據工單號查詢已裝箱數量；
                    this.textBox6.Text = firstScan.Cusname;
                    this.textBox7.Text = firstScan.Cusno;
                    this.textBox8.Text = firstScan.Delmatno;
                    this.textBox9.Text = firstScan.Cusmatno;

                    //下载打印模板 並初始化Carton 基本信息
                    carton.Workno = firstScan.Workno;
                    carton.Delmatno = firstScan.Delmatno;
                    carton.Cusno = firstScan.Cusno;
                    carton.PackType = "0";                  //包裝類型：0 整機， 1單出
                    carton.Cusname = firstScan.Cusname;
                    carton.Cuspo = firstScan.Cuspo;
                    carton.Orderqty = firstScan.Orderqty;
                    carton.Cusmatno = firstScan.Cusmatno;
                    carton.Delmatno = firstScan.Delmatno;
                    carton.Offino = firstScan.Offino;
                    carton.Verno = firstScan.Verno;
                    carton.Woquantity = firstScan.Woquantity;
                    carton.SoOrder = firstScan.SoOrder;

                    //編碼規則查詢

                    CusRule cusRule = cusRuleService.queryCusRuleByCond(firstScan.Cusno, firstScan.Delmatno, "1");                //1代表Carton碼編碼規則
                    if (cusRule != null && cusRule.Ruleno != null)
                    {
                        CodeRule codeRule = codeRuleService.queryRuleById(cusRule.Ruleno);
                        this.textBox13.Text = codeRule.RuleDesc;
                        carton.Ruleno = codeRule.Ruleno;
                    }
                    //裝箱容量查詢
                    capacity = capacityService.queryByRelation(firstScan.Cusno, firstScan.Delmatno, "1");
                    if (capacity != null)
                    {
                        this.textBox11.Text = capacity.Capacityqty.ToString();
                        this.textBox12.Text = capacity.Capacitydesc;
                        carton.CapacityNo = capacity.Capacityno;
                    }
                    ProdLine prodLine = new ProdLine();
                    carton.ProdLine = prodLine;
                    carton.ProdLine.ProdlineId = this.comboBox2.SelectedValue.ToString();
                    carton.ProdLineVal = this.comboBox2.Text;

                    FileRelDel fileRelDel = fileRelDelService.queryFileRelDelCusNo(firstScan.Cusno, firstScan.Delmatno, "1");

                    if (fileRelDel == null)
                    {
                        return;
                    }
                    carton.Modelno = fileRelDel.FileNo;                //浪潮默認使用的打印模板
                    carton.CartonStatus = "0";
                    carton = GenerateCarton.generateCartonNo(carton);
 

                    //顯示箱號：
                    this.textBox10.Text = carton.CartonNo;
                    this.textBox5.Text = cartonService.getCartonQtyByWO(carton.Workno);
                    //下載模板並預覽   1.查询模板是否存在， 若存在不下载  2.若不存在下载模板
                    filePath = modelInfoService.previewModelFile(carton.Modelno);
                    if (filePath != null)
                    {
                        string pictureFile = barPrint.PreviewPrintBC(filePath);
                        this.pictureBox1.Load(pictureFile);
                    }

                }
                else
                {
                    if (ctcode.Workno.Trim().Equals(this.firstScan.Workno.Trim()))
                    {
                        if (!checkRepeat(ctcode.Ctcode))
                        {
                            int index = this.dataGridView1.Rows.Add();
                            initRow(index, ctcode);
                            this.textBox3.Text = this.dataGridView1.Rows.Count.ToString();
                        }

                    }
                }
                if (capacity != null)
                {
                    //扫码数量等于容量时打印标签
                    if (this.dataGridView1.Rows.Count == capacity.Capacityqty)
                    {
                        printCarton();
                    }
                }
                else
                {
                    if (this.dataGridView1.Rows.Count == 10)
                    {
                        printCarton();
                    }
                }
            }
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
    }
}
