using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CTCodePrint
{
    public partial class ManRelFieldTypeConfig : Form
    {
        public ManRelFieldTypeConfig()
        {
            InitializeComponent();
        }
        private readonly ManRelFieldTypeService manRelFieldTypeService = new ManRelFieldTypeService();
        private readonly FieldTypeService fieldTypeService = new FieldTypeService();
        private readonly MandatoryFieldService mandatoryFieldService = new MandatoryFieldService();

        private void clearAll()
        {
            this.textBox3.Text = "";
            this.textBox4.Text = "";
            this.textBox5.Text = "";
        }

        private void displayList(string manNo)
        {
            this.dataGridView1.DataSource = manRelFieldTypeService.queryMandUnionFieldTypeList(manNo);
            if (this.dataGridView1.DataSource != null)
            {
                this.dataGridView1.Columns[0].HeaderText = "UUID";
                this.dataGridView1.Columns[1].HeaderText = "字段規則編號";
                this.dataGridView1.Columns[2].HeaderText = "字段規則描述";
                this.dataGridView1.Columns[3].HeaderText = "字段類型編號";
                this.dataGridView1.Columns[4].HeaderText = "字段類型值";
                this.dataGridView1.Columns[5].HeaderText = "字段類型值描述";
                this.dataGridView1.Columns[6].HeaderText = "创建者";
                this.dataGridView1.Columns[7].HeaderText = "创建时间";
                this.dataGridView1.Columns[8].HeaderText = "更新時間";
                this.dataGridView1.Columns[9].HeaderText = "字段類型名稱";
            }

        }

        private void RoleRelUserConfig_Load(object sender, EventArgs e)
        {
            this.comboBox2.DataSource = mandatoryFieldService.queryMandatoryInfoList("");
            this.comboBox2.DisplayMember = "Mandesc";
            this.comboBox2.ValueMember = "Manno";

            this.comboBox1.DataSource = fieldTypeService.queryFieldTypeList("");
            this.comboBox1.ValueMember = "FieldNo";
            this.comboBox1.DisplayMember = "FieldDesc";

            this.displayList(this.comboBox1.SelectedValue.ToString());
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox3.Text == null || this.textBox3.Text.ToString().Trim() == "")
            {
                MessageBox.Show("字段規則編號不能為空，請選擇字段規則！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.clearAll();
                this.comboBox2.Focus();
                return;
            }
            ManRelFieldType manRelFieldType = new ManRelFieldType();
            manRelFieldType.ManNo = this.textBox3.Text.ToString().Trim();
            manRelFieldType.FieldNo = this.comboBox1.SelectedValue.ToString().Trim();

            if (!manRelFieldTypeService.checkAdd(manRelFieldType) && checkFieldNameRepeat(manRelFieldType.FieldNo))
            {
                if (manRelFieldTypeService.saveManRelFieldType(manRelFieldType) != null)
                {
                    this.displayList(manRelFieldType.ManNo);
                    MessageBox.Show("字段類型值分配成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("該字段規則已經存在該字段類型值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("請選中需要刪除的行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string uuid = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            if (manRelFieldTypeService.deleteManRelFieldType(uuid))
            {
                this.displayList(this.textBox3.Text.ToString().Trim());
                MessageBox.Show("刪除字段類型值成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("刪除字段類型值失敗！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox2.SelectedValue == null || this.comboBox2.SelectedValue.ToString().Trim() == "")
            {
                MessageBox.Show("字段規則不能為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.clearAll();
                this.comboBox2.Focus();
                return;
            }
            MandatoryInfo mandatoryInfo = mandatoryFieldService.queryMandatoryInfoByNo(this.comboBox2.SelectedValue.ToString().Trim());
            if (mandatoryInfo != null)
            {
                //查詢用戶實體內容
                this.textBox3.Text = mandatoryInfo.Manno;
                this.textBox4.Text = mandatoryInfo.Opuser;
                this.textBox5.Text = mandatoryInfo.Createtime;

                displayList(mandatoryInfo.Manno);

            }
            else
            {
                this.clearAll();
                this.comboBox2.Focus();
                return;
            }
        }

        public bool checkFieldNameRepeat(string fieldNo)
        {
            FieldType fieldType = fieldTypeService.queryFieldType(fieldNo);
            if (fieldType == null)
            {
                return false;
            }
            else
            {
                if (this.dataGridView1.DataSource == null)
                {
                    return true;
                }
                List<MandUnionFieldType> mandUnionFieldTypes = (List<MandUnionFieldType>)this.dataGridView1.DataSource;
                foreach(MandUnionFieldType mandUnionFieldType in mandUnionFieldTypes)
                {
                    if (fieldType.FieldName.Equals(mandUnionFieldType.FieldName))
                    {
                        return false;
                    }
                }
                return true;
            }

        }
    }
}
