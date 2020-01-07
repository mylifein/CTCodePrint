using BLL;
using CTCodePrint.common;
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
    public partial class ModelBoundField : Form
    {
        public ModelBoundField()
        {
            InitializeComponent();
        }

        private readonly ModelRelMandService modelRelMandService = new ModelRelMandService();
        private readonly MandatoryFieldService mandatoryFieldService = new MandatoryFieldService();
        private readonly ModelInfoService modelInfoService = new ModelInfoService();

        private void ModelConfig_Load(object sender, EventArgs e)
        {
            this.comboBox1.DataSource = mandatoryFieldService.queryMandatoryInfoList("");
            this.comboBox1.DisplayMember = "Mandesc";
            this.comboBox1.ValueMember = "Manno";

            this.comboBox2.DataSource = modelInfoService.queryModelFileList("");
            this.comboBox2.ValueMember = "Fileno";
            this.comboBox2.DisplayMember = "Filedescription";


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox2.Text == null || this.textBox2.Text.Trim() == "")
            {
                MessageBox.Show("模板文件名不能為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Focus();
                return;
            }
            if (this.textBox3.Text == null || this.textBox3.Text.Trim() == "")
            {
                MessageBox.Show("模板編號不能為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox3.Focus();
                return;
            }
            if (this.textBox4.Text != null && this.textBox4.Text.Trim() != "")
            {
                MessageBox.Show("該模板已經保存成功,請勿重複保存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ModelRelMand modelRelMand = new ModelRelMand();
            modelRelMand.FileNo = this.comboBox2.SelectedValue.ToString();
            modelRelMand.ManNo = this.comboBox1.SelectedValue.ToString();
            if (modelRelMandService.checkAdd(modelRelMand.FileNo))
            {
                MessageBox.Show("該模板已經綁定字段規則,請勿重複綁定！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ModelRelMand reModelRelMand = modelRelMandService.saveModelRelMand(modelRelMand);
            if (reModelRelMand != null)
            {
                this.textBox1.Text = reModelRelMand.CreateTime;
                this.textBox4.Text = reModelRelMand.OpUser;
                MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("保存失敗！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Focus();
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
            ModelFile modelFile = modelInfoService.queryModelFileByNo(this.comboBox2.SelectedValue.ToString().Trim());
            if (modelFile != null)
            {
                //查詢用戶實體內容

                this.textBox2.Text = modelFile.Filename;
                this.textBox3.Text = modelFile.Fileno;

            }
            else
            {
                this.clearAll();
                this.comboBox2.Focus();
                return;
            }
        }

        private void clearAll()
        {
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.textBox3.Text = "";
            this.textBox4.Text = "";

        }
    }
}
