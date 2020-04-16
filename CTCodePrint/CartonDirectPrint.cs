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
        private readonly ModelRelMandService modelRelMandService = new ModelRelMandService();  //查詢模板的字段規則
        private readonly ManRelFieldTypeService manRelFieldTypeService = new ManRelFieldTypeService();  //根據字段規則 查詢字段規則值
        private readonly ModelInfoService modelInfoService = new ModelInfoService();        //查詢模板內容並下載
        private readonly PrintModelQ printQ = new PrintModelQ();
        private readonly SelectQuery selectQ = new SelectQuery();
        private readonly DepartmentService departmentService = new DepartmentService();
        private readonly ProdLineService prodLineService = new ProdLineService();
        private readonly OracleQueryB queryB = new OracleQueryB();                      //Oracle 查询
        private readonly CapacityService capacityService = new CapacityService();

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
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }





        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if (this.textBox1.Text == null || this.textBox1.Text.Trim() == "")
            {
                //MessageBox.Show("規則描述不能為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            string workno = this.textBox1.Text.Trim();
            DataSet ds = queryB.getWorkInfoByNo(workno.ToUpper());
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    carton = new Carton();
                    this.comboBox3.DataSource = ds.Tables[0];
                    this.comboBox3.DisplayMember = "CUST_PO_NUMBER";                        //PO                    
                    this.comboBox3.ValueMember = "ORDER_QTY";                               //QTY
                    //initRow(index, ctcode);
                    //初始化信息
                    this.textBox4.Text = ds.Tables[0].Rows[0]["START_QUANTITY"].ToString(); ;
                    //根據工單號查詢已裝箱數量；
                    this.textBox6.Text = ds.Tables[0].Rows[0]["CUST_NAME"].ToString();
                    this.textBox7.Text = ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString();
                    this.textBox8.Text = ds.Tables[0].Rows[0]["ITEM_CODE"].ToString();
                    this.textBox13.Text = ds.Tables[0].Rows[0]["ORDER_QTY"].ToString();      //PO QTY

                    if (ds.Tables[0].Rows[0]["CUSTOMER_ITEM_NUMBER"] != null && ds.Tables[0].Rows[0]["CUSTOMER_ITEM_NUMBER"].ToString().Trim() != "")
                    {
                        this.textBox9.Text = ds.Tables[0].Rows[0]["CUSTOMER_ITEM_NUMBER"].ToString().Trim();
                        carton.Cusmatno = ds.Tables[0].Rows[0]["CUSTOMER_ITEM_NUMBER"].ToString();
                    }
                    else
                    {
                        DataSet dsCusMaterial = queryB.getCusMatInfo(workno);
                        if (dsCusMaterial != null && dsCusMaterial.Tables.Count > 0)
                        {
                            if (dsCusMaterial.Tables[0].Rows.Count > 0)
                            {
                                this.textBox9.Text = dsCusMaterial.Tables[0].Rows[0]["CUSTOMER_ITEM_NUMBER"].ToString();
                                carton.Cusmatno = dsCusMaterial.Tables[0].Rows[0]["CUSTOMER_ITEM_NUMBER"].ToString();
                                this.textBox11.Text = dsCusMaterial.Tables[0].Rows[0]["CUSTOMER_ITEM_DESC"].ToString();
                                carton.CusmatDesc = dsCusMaterial.Tables[0].Rows[0]["CUSTOMER_ITEM_DESC"].ToString();
                            }
                        }
                    }
                    CapacityRelCus capacityRel = new CapacityRelCus();
                    capacityRel.DelMatno = ds.Tables[0].Rows[0]["ITEM_CODE"].ToString();
                    capacityRel.CusNo = ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString();
                    capacityRel.CapacityType = "1";  //装箱单容量类型
                    Capacity capacity = capacityService.queryByRelation(capacityRel);
                    //下载打印模板 並初始化Carton 基本信息


                    DataSet dsRevision = queryB.getRevisionInfo(workno);
                    if (dsRevision != null && dsRevision.Tables.Count > 0)
                    {
                        if (dsRevision.Tables[0].Rows.Count > 0)
                        {
                            this.comboBox4.DisplayMember = "REVISION";
                            this.comboBox4.ValueMember = "REVISION";
                            this.comboBox4.DataSource = dsRevision.Tables[0];
                        }
                    }
                    this.comboBox5.DataSource = ds.Tables[0];
                    this.comboBox5.DisplayMember = "SO_ORDER";                        //SO                 
                    this.comboBox5.ValueMember = "SO_ORDER";
                    carton.Workno = workno.ToUpper();
                    carton.Delmatno = ds.Tables[0].Rows[0]["ITEM_CODE"].ToString();
                    carton.Cusno = ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString();
                    carton.PackType = "1";                  //包裝類型：0 整機， 1單出
                    carton.Cusname = ds.Tables[0].Rows[0]["CUST_NAME"].ToString();
                    carton.Orderqty = ds.Tables[0].Rows[0]["ORDER_QTY"].ToString();
                    //carton.Cusmatno = ds.Tables[0].Rows[0]["CUSTOMER_ITEM_NUMBER"].ToString();
                    carton.Delmatno = ds.Tables[0].Rows[0]["ITEM_CODE"].ToString();
                    carton.Woquantity = ds.Tables[0].Rows[0]["START_QUANTITY"].ToString();
                    carton.SoOrder = ds.Tables[0].Rows[0]["SO_ORDER"].ToString();
                    carton.Completedqty = ds.Tables[0].Rows[0]["QUANTITY_COMPLETED"].ToString();            //CUST_PO_NUMBER
                    carton.Cuspo = ds.Tables[0].Rows[0]["CUST_PO_NUMBER"].ToString();
                    carton.Verno = "A10";
                    if (capacity != null)
                    {
                        this.countBoxQty(capacity);
                    }

                    //查詢機種號設置編碼規則
                    DataSet dsMac = selectQ.getMacByCus(carton.Cusno, carton.Delmatno, "1");                //1代表裝箱單編碼規則
                    string ruleNo = null;
                    if (dsMac.Tables.Count > 0 && dsMac.Tables[0].Rows.Count > 0)
                    {
                        ruleNo = dsMac.Tables[0].Rows[0]["rule_no"].ToString();
                    }
                    if (ruleNo != null)
                    {
                        DataSet mactDS = printQ.queryCodeInfo(ruleNo);
                        if (mactDS.Tables.Count > 0 && mactDS.Tables[0].Rows.Count > 0)
                        {
                            this.textBox2.Text = mactDS.Tables[0].Rows[0]["rule_desc"].ToString();
                            carton.Ruleno = mactDS.Tables[0].Rows[0]["rule_no"].ToString();                 //浪潮默認規則 編號R007
                        }
                    }



                    ProdLine prodLine = new ProdLine();
                    carton.ProdLine = prodLine;
                    carton.ProdLine.ProdlineId = this.comboBox2.SelectedValue.ToString();
                    int currentNumber = cartonService.queryCurrentBoxQty(carton.Workno);
                    carton.BoxNo = currentNumber == 0 ? 1 : currentNumber;
                    carton.Modelno = cartonService.queryFileNo(carton.Cusno, carton.Delmatno, "1");                //TYPE= 1 為裝箱單模板
                    carton.CartonStatus = "0";
                    carton = GenerateCarton.generateCartonNo(carton);
                    if (carton.Cusno.Equals("56906"))
                    {
                        carton.UnionField = "BB" + carton.CartonNo + "||P" + carton.Cusmatno + "Q" + carton.CartonQty + "||1P" + carton.Delmatno;
                    }


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
            //打印装箱单号
            if (true)
            {

                ModelRelMand modelRelMand = modelRelMandService.queryMenuInfoByFileNo(carton.Modelno);
                if (modelRelMand == null)
                {
                    MessageBox.Show("未找到該客戶出貨料號對應的打印字段規則信息，請維護相關信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //查詢字段對應的規則信息
                List<MandUnionFieldType> mandUnionFieldTypeList = manRelFieldTypeService.queryMandUnionFieldTypeList(modelRelMand.ManNo);
                if (mandUnionFieldTypeList == null)
                {
                    MessageBox.Show("未找到該客戶出貨料號對應的打印字段規則信息，請維護相關信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                carton.ProdLine.ProdlineId = this.comboBox2.SelectedValue.ToString();                  //打印時 獲得線別編號
                int currentNumber = cartonService.queryCurrentBoxQty(carton.Workno);
                carton.BoxNo = currentNumber == 0 ? 1 : currentNumber;
                carton.CartonQty = (int)this.numericUpDown1.Value;                        //裝箱單數量
                carton.Cuspo = comboBox3.Text;
                carton.Datecode = DateTime.Now.ToString("yyyyMMdd");
                carton = GenerateCarton.generateCartonNo(carton);
                this.textBox10.Text = carton.CartonNo;
                StringBuilder special = new StringBuilder();
                foreach (MandUnionFieldType mandTYpe in mandUnionFieldTypeList)
                {
                    if (mandTYpe.FieldName.ToUpper().Equals("CusPN".ToUpper()))
                    {
                        special.Append(mandTYpe.FieldValue);
                    }
                }
                special.Append(" {{" + carton.CartonQty + " {{PCS {{041098 {{" + carton.Datecode + " {{CHN {{" + carton.Cuspo + " {{/ {{" + carton.CartonNo + " {{" + carton.Datecode + " {{XY {{" + carton.Delmatno + " {{");
                foreach (MandUnionFieldType mandTYpe in mandUnionFieldTypeList)
                {
                    if (mandTYpe.FieldName.ToUpper().Equals("versionNo".ToUpper()))
                    {
                        special.Append(mandTYpe.FieldValue);
                    }
                }
                carton.SpecialField = special.ToString();

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
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox3.SelectedValue != null && this.comboBox3.SelectedValue.ToString().Trim() != "")
            {
                this.textBox13.Text = this.comboBox3.SelectedValue.ToString().Trim();
                carton.Cuspo = this.comboBox3.Text;
            }
            carton = GenerateCarton.generateCartonNo(carton);
            this.textBox10.Text = carton.CartonNo;
        }
    }
}
