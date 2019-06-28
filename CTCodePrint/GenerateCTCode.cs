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
        private readonly SelectQuery selectQ = new SelectQuery();
        private readonly PrintModelQ printQ = new PrintModelQ();
        private readonly BarCodePrint barPrint = BarCodePrint.getInstance();
        private GenerateCode generateC = new GenerateCode();

        private void GenerateCTCode_Load(object sender, EventArgs e)
        {

            //設置combox 初始值
            DataSet ds = selectQ.getCusSelect();
            DataTable itemTable = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) 
            {
                itemTable = ds.Tables[0];
            }
            this.comboBox1.DisplayMember = "cus_name";
            this.comboBox1.ValueMember = "cus_no";
            this.comboBox1.DataSource = itemTable;
           
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //獲得界面信息
            CTCode ctCodeInfo = new CTCode();
            ctCodeInfo.Workno = this.textBox1.Text.Trim();    //workNo
            ctCodeInfo.Cuspo = this.textBox2.Text.Trim();    //Cuspo
            ctCodeInfo.Delmatno = this.textBox3.Text.Trim(); //string deliveryMat
            ctCodeInfo.Cusno = this.comboBox1.SelectedValue.ToString(); //cusNo
            ctCodeInfo.Cusmatno = this.textBox5.Text.Trim();    //cusMatNo
            ctCodeInfo.Offino = this.textBox6.Text.Trim();  //officialNo
            ctCodeInfo.Verno = this.textBox7.Text.Trim();       //verNo
            ctCodeInfo.Mactype = this.comboBox2.SelectedValue.ToString().Trim();  //macType
            ctCodeInfo.Woquantity = this.textBox4.Text.Trim();  //wnquantity
            List<CTCode> ctList = new List<CTCode>();
            ctList = generateC.generateCTNumber(ctCodeInfo);
            
            /// Check 必填項
            ///流水碼自增  並調用打印 
            ///查詢模板號
            string modelNo = printQ.checkPrintModelRel(ctCodeInfo.Cusno, ctCodeInfo.Delmatno);
            if(modelNo == null || modelNo == "")
            {
                MessageBox.Show("未找到該客戶出貨料號對應的打印模板", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Focus();
                return;
            }
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
                foreach(CTCode ctTemp in ctList)
                {
                    PrintContent printContent = new PrintContent();
                    printContent.CtCode = ctTemp.Ctcode;
                    this.textBox8.Text = ctTemp.Ctcode;
                    bool judge = barPrint.PrintBC(templateFile, printContent, manF);
                    if (judge)
                    {
                        printQ.saveCTCodeInfo(ctTemp);
                    }
                    else
                    {
                        MessageBox.Show("打印失敗請聯係管理員！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

            }



        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            string comboxValue = "";
            if(this.comboBox1.SelectedValue != null)
            {
                comboxValue = this.comboBox1.SelectedValue.ToString();
            }
            DataSet ds = selectQ.getMacByCus(comboxValue);
            DataTable itemTable = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                itemTable = ds.Tables[0];
            }
            this.comboBox2.DisplayMember = "cus_mactype";
            this.comboBox2.ValueMember = "cus_mactype";
            this.comboBox2.DataSource = itemTable;

        }


    }
}
