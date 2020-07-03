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
    public partial class CtExternalprint : Form
    {
        public CtExternalprint()
        {
            InitializeComponent();
        }

        private readonly BarCodePrint barPrint = BarCodePrint.getInstance();
        private readonly ModelInfoService modelInfoService = new ModelInfoService();                    //查詢模板內容並下載
        private readonly ManRelFieldTypeService manRelFieldTypeService = new ManRelFieldTypeService();  //根據字段規則 查詢字段規則值
        private readonly CTCodeService ctCodeService = new CTCodeService();
        private string filePath = null;



        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.textBox1.Text == null || this.textBox1.Text.Trim() == "")
                {
                    this.textBox1.Focus();
                    clearAll();
                    return;
                }
                string ctcode = this.textBox1.Text.Trim();
                CTCode codeInfo = ctCodeService.queryCTCodeByCtcode(ctcode);
                if (codeInfo != null)
                {
                    filePath = modelInfoService.previewModelFile("F062");
                    if (filePath == null)
                    {
                        MessageBox.Show("未找到對應的打印模板信息，請維護相關信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    printLabel(codeInfo);
                }else
                {
                    this.textBox1.Focus();
                    clearAll();
                }
            }

        }



        private void clearAll()
        {
            this.textBox1.Text = "";
        }

        private void printLabel(CTCode content)
        {
            List<MandUnionFieldType> mandUnionFieldTypeList = manRelFieldTypeService.queryMandUnionFieldTypeList("MF0110");
            bool judgePrint = barPrint.bactchPrintPalletByModel(filePath, contentToDic(content, mandUnionFieldTypeList));
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


        private Dictionary<string, string> contentToDic(CTCode content, List<MandUnionFieldType> mandUnionFieldTypeList)
        {

            PropertyInfo[] propertyInfoARR = content.GetType().GetProperties();
            Dictionary<string, string> property = new Dictionary<string, string>();
            foreach (PropertyInfo propertyInfo in propertyInfoARR)
            {
                Object propertyVal = content.GetType().GetProperty(propertyInfo.Name).GetValue(content, null);
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
