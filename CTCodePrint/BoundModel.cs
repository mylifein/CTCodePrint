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
    public partial class BoundModel : Form
    {
        public BoundModel()
        {
            InitializeComponent();
        }

        private readonly PrintModelQ printQ = new PrintModelQ();
        private readonly SelectQuery selectQ = new SelectQuery();
        private bool saveMark = false;

        private void BoundModel_Load(object sender, EventArgs e)
        {
            DataSet ds = printQ.queryModelInfo();
            DataTable itemTable = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                itemTable = ds.Tables[0];
            }
            this.comboBox1.DisplayMember = "model_desc";
            this.comboBox1.ValueMember = "model_no";
            this.comboBox1.DataSource = itemTable;
            DataSet dsCus = selectQ.getCusSelect();
            if (dsCus != null && dsCus.Tables.Count > 0 && dsCus.Tables[0].Rows.Count > 0)
            {
                this.comboBox2.DisplayMember = "cus_name";
                this.comboBox2.ValueMember = "cus_no";
                this.comboBox2.DataSource = dsCus.Tables[0];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveMark)
            {
                MessageBox.Show("該模板關係已經保存，請勿重複保存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.textBox1.Text == null || this.textBox1.Text.Trim() == "")
            {
                MessageBox.Show("维护客户料号信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            ModelRelation modelRelation = new ModelRelation();
            modelRelation.Modelno = this.comboBox1.SelectedValue.ToString();
            modelRelation.Cusno = this.comboBox2.SelectedValue.ToString();
            modelRelation.Delmatno = this.textBox1.Text.Trim().ToString();
            if(!printQ.queryRepeatInModelRel(modelRelation.Cusno, modelRelation.Delmatno))
            {
                MessageBox.Show("該客戶和出貨料號的打印模板已經綁定，請勿重複綁定！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            saveMark = printQ.saveBoundModel(modelRelation);
            if (saveMark)
            {
                MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }else
            {
                MessageBox.Show("保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
