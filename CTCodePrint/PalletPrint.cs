using BLL;
using Common;
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
    public partial class PalletPrint : Form
    {
        public PalletPrint()
        {
            InitializeComponent();
        }
        private Carton firstCarton;
        private Pallet pallet;
        private Capacity capacity;
        private FileRelDel fileRelDel;
        private CodeRule codeRule;
        private List<MandUnionFieldType> mandUnionFieldTypeList;


        private List<Carton> cartonList = new List<Carton>();
        private readonly CartonService cartonService = new CartonService();
        private readonly BarCodePrint barPrint = BarCodePrint.getInstance();
        private readonly MandRelDelService mandRelDelService = new MandRelDelService();                  //查詢模板的参数字段
        private readonly ManRelFieldTypeService manRelFieldTypeService = new ManRelFieldTypeService();  //根據字段規則 查詢字段規則值
        private readonly ModelInfoService modelInfoService = new ModelInfoService();                    //查詢模板內容並下載
        private readonly FileRelDelService fileRelDelService = new FileRelDelService();                 //查詢模板的文件編號
        private readonly CodeRuleService codeRuleService = new CodeRuleService();                       //查询编码规则
        private readonly CusRuleService cusRuleService = new CusRuleService();
        private readonly PalletService palletService = new PalletService();
        private readonly CapacityService capacityService = new CapacityService();


        private string filePath = null;


        private void button1_Click(object sender, EventArgs e)
        {
            printCarton();
        }

        private void CreateRules_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = "palletNo";
            column.DataPropertyName = "palletNo";//对应数据源的字段
            column.HeaderText = "裝箱單號";
            column.Width = 80;
            this.dataGridView1.Columns.Add(column);
            DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
            column1.Name = "delMatno";
            column1.DataPropertyName = "del_matno";//对应数据源的字段
            column1.HeaderText = "出貨料號";
            column1.Width = 120;
            this.dataGridView1.Columns.Add(column1);
            DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
            column2.Name = "cusName";
            column2.DataPropertyName = "cus_name";//对应数据源的字段
            column2.HeaderText = "客戶名稱";
            column2.Width = 140;
            this.dataGridView1.Columns.Add(column2);

            DataGridViewTextBoxColumn column3 = new DataGridViewTextBoxColumn();
            column3.Name = "cusNo";
            column3.DataPropertyName = "cus_no";//对应数据源的字段
            column3.HeaderText = "客戶編號";
            column3.Width = 65;
            this.dataGridView1.Columns.Add(column3);

            DataGridViewTextBoxColumn column4 = new DataGridViewTextBoxColumn();
            column4.Name = "workNo";
            column4.DataPropertyName = "work_no";//对应数据源的字段
            column4.HeaderText = "工單號";
            column4.Width = 60;
            this.dataGridView1.Columns.Add(column4);

            DataGridViewTextBoxColumn column5 = new DataGridViewTextBoxColumn();
            column5.Name = "woQuantity";
            column5.DataPropertyName = "wo_quantity";//对应数据源的字段
            column5.HeaderText = "工單數量";
            column5.Width = 65;
            this.dataGridView1.Columns.Add(column5);

            DataGridViewTextBoxColumn column6 = new DataGridViewTextBoxColumn();
            column6.Name = "cusMatno";
            column6.DataPropertyName = "cus_matno";//对应数据源的字段
            column6.HeaderText = "客戶料號";
            column6.Width = 120;
            this.dataGridView1.Columns.Add(column6);

            DataTable itemTable = new DataTable();   // construct selects value
            DataColumn columnCombox;
            DataRow row;
            columnCombox = new DataColumn("Name");
            itemTable.Columns.Add(columnCombox);
            columnCombox = new DataColumn("Value");
            itemTable.Columns.Add(columnCombox);
            row = itemTable.NewRow();
            row["Name"] = "同工單";
            row["Value"] = "1";
            itemTable.Rows.Add(row);
            row = itemTable.NewRow();
            row["Name"] = "混棧板";
            row["Value"] = "2";
            itemTable.Rows.Add(row);
            this.comboBox1.DisplayMember = "Name";
            this.comboBox1.ValueMember = "Value";
            this.comboBox1.DataSource = itemTable;

        }



        private void button3_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("請選中需要刪除的行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.cartonList.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
            this.dataGridView1.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
            this.textBox5.Text = countQty(cartonList).ToString();
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
                Carton carton = cartonService.queryCartonByCartonNo(this.textBox1.Text.Trim());
                if (carton != null)
                {
                    if (!carton.CartonStatus.Equals("0"))
                    {
                        MessageBox.Show("該裝箱單碼已綁定，請檢查！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.textBox1.Text = "";
                        this.textBox1.Focus();
                        return;
                    }
                    if (this.dataGridView1.Rows.Count == 0)
                    {
                        // 1.檢查是否綁定棧板模板     
                        fileRelDel = fileRelDelService.queryFileRelDelCusNo(carton.Cusno, carton.Delmatno, "2");
                        if (fileRelDel == null)
                        {
                            MessageBox.Show("請聯繫管理員綁定標籤模板！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.textBox1.Text = "";
                            this.textBox1.Focus();
                            CommonAuxiliary.playFail();
                            return;
                        }
                        //2. 檢查是否綁定編碼規則
                        codeRule = codeRuleService.queryRuleByCond(carton.Cusno, carton.Delmatno, "2");                 //2代表Pallet碼編碼規則
                        if (codeRule == null)
                        {
                            MessageBox.Show("請聯繫管理員綁定編碼規則！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.textBox1.Text = "";
                            this.textBox1.Focus();
                            CommonAuxiliary.playFail();
                            return;
                        }
                        //3.查询标签参数信息
                        mandUnionFieldTypeList = manRelFieldTypeService.queryFieldListByCond(carton.Cusno, carton.Delmatno, "2");
                        if (mandUnionFieldTypeList == null)
                        {
                            MessageBox.Show("未找到該客戶出貨料號對應的打印字段規則信息，請維護相關信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.textBox1.Text = "";
                            this.textBox1.Focus();
                            CommonAuxiliary.playFail();
                            return;
                        }
                        //4.查询标准栈板容量
                        capacity = capacityService.queryByRelation(carton.Cusno, carton.Delmatno, "2");
                        pallet = new Pallet();
                        if (capacity != null)
                        {
                            pallet.CapacityNo = capacity.Capacityno;
                            this.textBox12.Text = capacity.Capacitydesc;
                            this.textBox14.Text = capacity.Capacityqty.ToString();
                        }                      
                        firstCarton = carton;
                        this.textBox2.Text = carton.Workno;
                        int index = this.dataGridView1.Rows.Add();
                        initRow(index, carton);
                        //初始化信息
                        this.textBox3.Text = this.dataGridView1.Rows.Count.ToString();
                        this.textBox4.Text = firstCarton.Woquantity;
                        //根據工單號查詢已裝箱數量；
                        this.textBox6.Text = firstCarton.Cusname;
                        this.textBox7.Text = firstCarton.Cusno;
                        this.textBox8.Text = firstCarton.Delmatno;
                        this.textBox9.Text = firstCarton.Cusmatno;
                        this.textBox15.Text = firstCarton.Cuspo;
                        this.textBox5.Text = carton.CartonQty.ToString();
                        this.textBox13.Text = codeRule.RuleDesc;

                        //下载打印模板 並初始化Carton 基本信息

                        pallet.Workno = firstCarton.Workno;
                        pallet.Delmatno = firstCarton.Delmatno;                             //浪潮默認規則 編號R007         棧板規則使用默認編碼規則
                        pallet.Cusmatno = firstCarton.Cusmatno;
                        pallet.Cuspo = firstCarton.Cuspo;
                        pallet.Cusname = firstCarton.Cusname;
                        pallet.SoOrder = firstCarton.SoOrder;
                        pallet.Cusno = firstCarton.Cusno;
                        pallet.Modelno = fileRelDel.FileNo;
                        pallet.Ruleno = codeRule.Ruleno;

                        //pallet = GeneratePallet.generatePalletNo(pallet);
                        ////顯示棧板號：
                        //this.textBox10.Text = pallet.PalletNo;
                        //this.textBox11.Text = pallet.BatchNo;
                        //下載模板並預覽   1.查询模板是否存在， 若存在不下载  2.若不存在下载模板
                        filePath = modelInfoService.previewModelFile(pallet.Modelno);
                        //if (filePath != null)
                        //{
                        //    string pictureFile = barPrint.PreviewPrintBC(filePath);
                        //    this.pictureBox1.Load(pictureFile);
                        //}
                        CommonAuxiliary.playSuccess();
                    }
                    else
                    {
                        //第二箱掃描  檢查是否混棧板，同工單，檢查箱號是否同工單號
                        if (("1").Equals(this.comboBox1.SelectedValue.ToString()))
                        {
                            if (carton.Workno.Trim().Equals(this.firstCarton.Workno.Trim()))
                            {
                                if (!checkRepeat(carton.CartonNo))
                                {
                                    int index = this.dataGridView1.Rows.Add();
                                    initRow(index, carton);
                                    this.textBox3.Text = this.dataGridView1.Rows.Count.ToString();
                                    this.textBox5.Text = countQty(cartonList).ToString();
                                    CommonAuxiliary.playSuccess();
                                }else
                                {
                                    this.textBox1.Text = "";
                                    this.textBox1.Focus();
                                    CommonAuxiliary.playFail();
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("装箱单的工单号不同,请检查是否需要混栈板包装！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.textBox1.Text = "";
                                this.textBox1.Focus();
                                CommonAuxiliary.playFail();
                                return;
                            }
                        }else
                        {
                            //混棧板  只檢查箱號是否重複
                            if (!checkRepeat(carton.CartonNo))
                            {
                                int index = this.dataGridView1.Rows.Add();
                                initRow(index, carton);
                                this.textBox3.Text = this.dataGridView1.Rows.Count.ToString();
                                this.textBox5.Text = countQty(cartonList).ToString();
                                CommonAuxiliary.playSuccess();
                            }
                            else
                            {
                                this.textBox1.Text = "";
                                this.textBox1.Focus();
                                CommonAuxiliary.playFail();
                                return;
                            }
                        }
                    }
                    //判斷是否混棧板.
                    if (("1").Equals(this.comboBox1.SelectedValue.ToString()))
                    {
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
                            if (this.dataGridView1.Rows.Count == 100)
                            {
                                printCarton();
                            }
                        }
                    }
                }
                else
                {
                    this.textBox1.Text = "";
                }
            }


        }


        /// <summary>
        /// TODO 初始化行信息
        /// </summary>
        /// <param name="index"></param>
        /// <param name="ctcode"></param>
        public void initRow(int index, Carton carton)
        {
            this.dataGridView1.Rows[index].Cells[0].Value = carton.CartonNo;
            this.dataGridView1.Rows[index].Cells[1].Value = carton.Delmatno;
            this.dataGridView1.Rows[index].Cells[2].Value = carton.Cusname;
            this.dataGridView1.Rows[index].Cells[3].Value = carton.Cusno;
            this.dataGridView1.Rows[index].Cells[4].Value = carton.Workno;
            this.dataGridView1.Rows[index].Cells[5].Value = carton.Woquantity;
            this.dataGridView1.Rows[index].Cells[6].Value = carton.Cusmatno;
            this.textBox1.Text = "";
            cartonList.Add(carton);

        }

        public bool checkRepeat(string cartonNo)
        {
            foreach (Carton compareValue in cartonList)
            {
                if (compareValue.CartonNo.Equals(cartonNo))
                {
                    return true;
                }
            }
            return false;

        }





        private void clearAll()
        {
            cartonList = new List<Carton>();
            firstCarton = null;
            pallet = null;
            capacity = null;
            filePath = null;
            this.dataGridView1.Rows.Clear();                //清除視圖
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
            this.textBox13.Text = "";
            this.textBox14.Text = "";
            this.textBox15.Text = "";

        }

        /// <summary>
        /// TODO 打印裝箱單
        /// </summary>
        private void printCarton()
        {
            //打印装箱单号
            if (this.dataGridView1.Rows.Count > 0)
            {
                pallet.CartonList = cartonList;
                pallet.PalletQty = countQty(cartonList);
                pallet = GeneratePallet.generatePalletNo(pallet,codeRule);
                bool judgePrint = barPrint.bactchPrintPalletByModel(filePath,palletListToArray(pallet,mandUnionFieldTypeList));
                if (judgePrint)
                {
                    if (!palletService.savePallet(pallet))
                    {
                        MessageBox.Show("保存裝箱單失敗！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    clearAll();
                }
                else
                {
                    MessageBox.Show("打印失敗請聯係管理員！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        /// <summary>
        /// 計算棧板包裝數量
        /// </summary>
        /// <param name="cartonList"></param>
        /// <returns></returns>
        public int countQty(List<Carton> cartonList)
        {
            int countResult = 0;
            foreach (Carton carton in cartonList)
            {
                countResult = countResult + carton.CartonQty;
            }
            return countResult;
        }


        private Dictionary<string, string> palletListToArray(Pallet pallet, List<MandUnionFieldType> mandUnionFieldTypeList)
        {

            PropertyInfo[] propertyInfoARR = pallet.GetType().GetProperties();
            Dictionary<string, string> property = new Dictionary<string, string>();
            foreach (PropertyInfo propertyInfo in propertyInfoARR)
            {
                Object propertyVal = pallet.GetType().GetProperty(propertyInfo.Name).GetValue(pallet, null);
                if (propertyVal != null)
                {
                    property.Add(propertyInfo.Name.ToUpper(), propertyVal.ToString());
                }
            }
            Dictionary<string, string> palletDict = new Dictionary<string, string>();
            foreach (MandUnionFieldType mandUnionFieldType in mandUnionFieldTypeList)
            {
                string fieldName = mandUnionFieldType.FieldName.ToUpper();
                if (property.ContainsKey(fieldName))
                {
                    palletDict.Add(fieldName, property[fieldName]);
                }
                else
                {
                    palletDict.Add(fieldName, mandUnionFieldType.FieldValue);
                }
            }

            return palletDict;
        }

    }
}
