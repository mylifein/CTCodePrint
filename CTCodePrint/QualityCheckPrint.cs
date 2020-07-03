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
    public partial class QualityCheckPrint : Form
    {
        public QualityCheckPrint()
        {
            InitializeComponent();
        }

        private readonly BarCodePrint barPrint = BarCodePrint.getInstance();
        private readonly ModelInfoService modelInfoService = new ModelInfoService();                    //查詢模板內容並下載
        private readonly OracleQueryB queryB = new OracleQueryB();                                      //Oracle 查询
        private readonly ManRelFieldTypeService manRelFieldTypeService = new ManRelFieldTypeService();  //根據字段規則 查詢字段規則值
        private readonly QualityInfoService qualityInfoService = new QualityInfoService();              //质量信息查询
        private readonly WoInfoService woInfoService = new WoInfoService();
        private WoInfo woInfo = null;
        private string filePath = null;

        private void button1_Click(object sender, EventArgs e)
        {
            if (woInfo == null)
            {
                MessageBox.Show("请输入有效工单号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            if (this.textBox3.Text == null || this.textBox3.Text.Trim() == "")
            {
                MessageBox.Show("请维护送样检测人工号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox3.Focus();
                return;
            }
            if (qualityInfoService.checkQualNo(woInfo.WoNo))
            {
                //可以打印标签， 先检查是否需要保存工单.
                if (!woInfoService.exists(woInfo.WoNo))
                {
                    bool saveResult = woInfoService.saveWoInfo(woInfo);
                    if (!saveResult)
                    {
                        MessageBox.Show("保存工单信息失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                QualityInfo qualityInfo = new QualityInfo();
                qualityInfo.WoNo = woInfo.WoNo;
                qualityInfo.DeliverMan = this.textBox3.Text.Trim();
                qualityInfo.QualiatyNo = GenerateQuality.gennerateQualityNo("R040");
                qualityInfo.CheckNum = qualityInfoService.queryCheckNum(woInfo.WoNo) + 1;
                QualityInfo printQualInfo = qualityInfoService.saveQualityInfo(qualityInfo);
                printQualInfo.DelMatno = woInfo.DelMatno;
                printQualInfo.ModelNo = woInfo.ModelNo;
                printQualInfo.Datecode = printQualInfo.Createtime;
                printQualityLabel(printQualInfo);
            }
            else
            {
                MessageBox.Show("该工单首件质检标签已完成/进行中，不能再产生质检标签！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
            }


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
                woInfo = queryB.queryWoInfoByWo(workno.ToUpper());
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

                    filePath = modelInfoService.previewModelFile("F042");
                    if (filePath == null)
                    {
                        MessageBox.Show("未找到對應的打印模板信息，請維護相關信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                }
            }

        }



        private void clearAll()
        {
            this.textBox1.Text = "";
            this.textBox4.Text = "";
            this.textBox8.Text = "";
            this.textBox5.Text = "";
            this.textBox7.Text = "";
            this.textBox2.Text = "";
            this.textBox6.Text = "";
            this.textBox11.Text = "";
            this.textBox12.Text = "";
            this.textBox3.Text = "";
            woInfo = null;
        }



        /// <summary>
        /// TODO 打印裝箱單
        /// </summary>
        private void printQualityLabel(QualityInfo qualityInfo)
        {
            List<MandUnionFieldType> mandUnionFieldTypeList = manRelFieldTypeService.queryMandUnionFieldTypeList("MF0055");
           bool judgePrint = barPrint.bactchPrintPalletByModel(filePath, contentToDic(qualityInfo, mandUnionFieldTypeList));
            if (judgePrint)
            {
                //清空质检标打印签信息
                clearAll();
            }
            else
            {
                MessageBox.Show("打印失敗請聯係管理員！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }


        private Dictionary<string, string> contentToDic(QualityInfo qualityInfo, List<MandUnionFieldType> mandUnionFieldTypeList)
        {

            PropertyInfo[] propertyInfoARR = qualityInfo.GetType().GetProperties();
            Dictionary<string, string> property = new Dictionary<string, string>();
            foreach (PropertyInfo propertyInfo in propertyInfoARR)
            {
                Object propertyVal = qualityInfo.GetType().GetProperty(propertyInfo.Name).GetValue(qualityInfo, null);
                if (propertyVal != null)
                {
                    property.Add(propertyInfo.Name.ToUpper(), propertyVal.ToString());
                }
            }

            Dictionary<string, string> contentDict = new Dictionary<string, string>();
            foreach (MandUnionFieldType mandUnionFieldType in mandUnionFieldTypeList)
            {
                string fieldName = mandUnionFieldType.FieldName.ToUpper();
                if (property.ContainsKey(fieldName))
                {
                    contentDict.Add(fieldName, property[fieldName]);
                }
                else
                {
                    contentDict.Add(fieldName, mandUnionFieldType.FieldValue);
                }
            }

            return contentDict;
        }


    }
}
