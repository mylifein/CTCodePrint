using BLL;
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
    public partial class ModelConfig : Form
    {
        public ModelConfig()
        {
            InitializeComponent();
        }
        private readonly PrintModelQ printQ = new PrintModelQ();

        private void ModelConfig_Load(object sender, EventArgs e)
        {
            DataSet ds = printQ.queryMandatory();
            DataTable itemTable = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                itemTable = ds.Tables[0];
            }
            this.comboBox1.DisplayMember = "man_desc";
            this.comboBox1.ValueMember = "man_no";
            this.comboBox1.DataSource = itemTable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.textBox1.Text != null && this.textBox1.Text.Trim() != "")
            {
                MessageBox.Show("該模板已經保存成功,請勿重複保存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(this.textBox2.Text == null || this.textBox2.Text.Trim() == "")
            {
                MessageBox.Show("請維護打印模板文件名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Focus();
            }
            ModelInfo modelInfo = new ModelInfo();
            modelInfo.Modelname = this.textBox2.Text.Trim();
            modelInfo.Modeldesc = this.textBox3.Text.Trim();
            modelInfo.Manno = this.comboBox1.SelectedValue.ToString();
            ModelInfo reModel = printQ.saveModelInfo(modelInfo);
            if (reModel != null)
            {
                this.textBox1.Text = reModel.Modelno;
                MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("保存失敗，請維護打印模板文件名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Focus();
            }

        }
    }
}
