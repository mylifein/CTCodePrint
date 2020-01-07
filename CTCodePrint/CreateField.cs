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
    public partial class CreateField : Form
    {
        public CreateField()
        {
            InitializeComponent();
        }
        private readonly FieldTypeService fieldTypeService = new FieldTypeService();

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text != null && this.textBox1.Text.Trim() != "")
            {
                MessageBox.Show("該字段類型已經保存，請勿重複保存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            if (this.textBox2.Text == null || this.textBox2.Text.Trim() == "")
            {
                MessageBox.Show("字段值不能為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Focus();
                return;
            }
            if (this.textBox3.Text == null || this.textBox3.Text.Trim() == "")
            {
                MessageBox.Show("字段描述不能為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox3.Focus();
                return;
            }
            if (this.textBox6.Text == null || this.textBox3.Text.Trim() == "")
            {
                MessageBox.Show("字段名稱不能為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox3.Focus();
                return;
            }
            FieldType fieldType = new FieldType();
            fieldType.FieldName = this.textBox6.Text.Trim();
            fieldType.FieldValue = this.textBox2.Text.Trim();
            fieldType.FieldDesc = this.textBox3.Text.Trim();
            FieldType reFieldType = fieldTypeService.saveFieldType(fieldType);
            if (reFieldType != null)
            {
                MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Text = reFieldType.FieldNo;
                this.textBox4.Text = reFieldType.OpUser;
                this.textBox5.Text = reFieldType.CreateTime;
            }
            else
            {
                MessageBox.Show("保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
