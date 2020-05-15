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
    public partial class QualityCheckPrint : Form
    {
        public QualityCheckPrint()
        {
            InitializeComponent();
        }

        private readonly BarCodePrint barPrint = BarCodePrint.getInstance();
        private readonly ModelInfoService modelInfoService = new ModelInfoService();                    //查詢模板內容並下載
        private readonly DepartmentService departmentService = new DepartmentService();
        private readonly ProdLineService prodLineService = new ProdLineService();
        private readonly OracleQueryB queryB = new OracleQueryB();                      //Oracle 查询

        private string filePath = null;


        private void button1_Click(object sender, EventArgs e)
        {

               printCarton();
            
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
                string workno = this.textBox1.Text.Trim();
                WoInfo woInfo = queryB.queryWoInfoByWo(workno.ToUpper());
                if (woInfo != null)
                {

                    this.textBox4.Text = woInfo.WoQty;
                    this.textBox8.Text = woInfo.DelMatno;
                    this.textBox5.Text = woInfo.DeptId;
                    this.textBox7.Text = woInfo.DeptCode;
                    this.textBox2.Text = woInfo.ModelNo;
                    this.textBox6.Text = woInfo.ClassCode;
                    this.textBox11.Text = woInfo.CompletionSub;
                    this.textBox12.Text = woInfo.DelMatnoDesc;

                    //下載模板並預覽
                    ModelFile modelFile = modelInfoService.queryModelFileByNo("F042");
                    if (modelFile != null)
                    {
                        filePath = Auxiliary.downloadModelFile(modelFile);
                        string pictureFile = barPrint.PreviewPrintBC(filePath);
                        this.pictureBox1.Load(pictureFile);

                    }
                }
            }

        }



        private void clearAll()
        {
            this.textBox4.Text = "";
        }



        /// <summary>
        /// TODO 打印裝箱單
        /// </summary>
        private void printCarton()
        {

            


            bool judgePrint = true;

            //judgePrint = barPrint.printCatonByModel(filePath, carton, mandUnionFieldTypeList);
            
            if (judgePrint)
            {
                if (true)
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
