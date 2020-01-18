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
    public partial class CartonPrint : Form
    {
        public CartonPrint()
        {
            InitializeComponent();
        }
        private CTCode firstScan;
        private Carton carton;
        private List<String> ctCodeList = new List<string>();
        CartonService cartonService = new CartonService();
        private readonly CTCodeService ctCodeService = new CTCodeService();
        private readonly GenerateCarton generateCarton = new GenerateCarton();
        private readonly BarCodePrint barPrint = BarCodePrint.getInstance();
        private readonly ModelRelMandService modelRelMandService = new ModelRelMandService();  //查詢模板的字段規則
        private readonly ManRelFieldTypeService manRelFieldTypeService = new ManRelFieldTypeService();  //根據字段規則 查詢字段規則值
        private readonly ModelInfoService modelInfoService = new ModelInfoService();        //查詢模板內容並下載
        private readonly PrintModelQ printQ = new PrintModelQ();
        private readonly SelectQuery selectQ = new SelectQuery();
        private readonly DepartmentService departmentService = new DepartmentService();
        private readonly ProdLineService prodLineService = new ProdLineService();

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
            
        }

        private void CreateRules_Load(object sender, EventArgs e)
        {
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

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

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


        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if (this.textBox1.Text == null || this.textBox1.Text.Trim() == "")
            {
                //MessageBox.Show("規則描述不能為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            CTCode ctcode = ctCodeService.queryCTCodeByCtcode(this.textBox1.Text.Trim());
            if (cartonService.exists(ctcode.Ctcode))
            {
                MessageBox.Show("該CT碼已綁定，請檢查！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Text = "";
                this.textBox1.Focus();
                return;
            }
            if (ctcode != null)
            {
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
                    carton.Ruleno = "R007";                 //浪潮默認規則 編號R007
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
                    carton.Completedqty = firstScan.Completedqty;
                    ProdLine prodLine = new ProdLine();
                    carton.ProdLine = prodLine;
                    carton.ProdLine.ProdlineId = this.comboBox2.SelectedValue.ToString();
                    carton.Modelno = "F003";                //浪潮默認使用的打印模板
                    carton.CartonStatus = "0";
                    carton = GenerateCarton.generateCartonNo(carton);
                    carton.Ct1 = "";
                    carton.Ct2 = "";
                    carton.Ct3 = "";
                    carton.Ct4 = "";
                    carton.Ct5 = "";
                    carton.Ct6 = "";
                    carton.Ct7 = "";
                    carton.Ct8 = "";
                    carton.Ct9 = "";
                    carton.Ct10 = "";

                    //顯示箱號：
                    this.textBox10.Text = carton.CartonNo;
                    this.textBox5.Text = cartonService.getCartonQtyByWO(carton.Workno);
                    //下載模板並預覽
                    ModelFile modelFile = modelInfoService.queryModelFileByNo(carton.Modelno);
                    if (modelFile != null)
                    {
                        filePath = Auxiliary.downloadModelFile(modelFile);
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
                if(this.dataGridView1.Rows.Count == 10)
                {
                    printCarton();
                }
            }else
            {
                this.textBox1.Text = "";
            }


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
            foreach(string compareValue in ctCodeList)
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
            for(int i = 0; i < ctcodeList.Count; i++)
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

        private void clearAll()
        {
            this.dataGridView1.Rows.Clear();                //清除視圖
            this.textBox2.Text = "";                        //清除文本內容
            this.textBox4.Text = "";
            

        }

        /// <summary>
        /// TODO 打印裝箱單
        /// </summary>
        private void printCarton()
        {
            //打印装箱单号
            if (this.dataGridView1.Rows.Count > 0)
            {

                ModelRelMand modelRelMand = modelRelMandService.queryMenuInfoByFileNo(carton.Modelno);
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
                carton.ProdLine.ProdlineId = this.comboBox2.SelectedValue.ToString();                  //打印時 獲得線別編號
                carton.CartonQty = Convert.ToInt32(this.textBox3.Text.Trim());                         //裝箱單數量
                initCTSeq(ctCodeList);
                carton.CtCodeList = ctCodeList;
                bool judgePrint = barPrint.printCatonByModel(filePath, carton, mandUnionFieldTypeList);
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
        }
    }
}
