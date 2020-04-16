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
    public partial class CreateCapacity : Form
    {
        public CreateCapacity()
        {
            InitializeComponent();
        }
        private readonly CapacityService capacityService = new CapacityService();

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text != null && this.textBox1.Text.Trim() != "")
            {
                MessageBox.Show("該容量已經保存，請勿重複保存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            if (this.textBox3.Text == null || this.textBox3.Text.Trim() == "")
            {
                MessageBox.Show("容量描述不能為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox3.Focus();
                return;
            }
            int capacityQty = (int)this.numericUpDown1.Value;                        //裝箱單容量
            if (capacityQty == null || capacityQty == 0)
            {
                MessageBox.Show("容量描不能為0！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox3.Focus();
                return;
            }
            Capacity capacity = new Capacity();
            capacity.Capacitydesc = this.textBox3.Text.Trim();
            capacity.Capacityqty = capacityQty;
            Capacity reCapacity = capacityService.saveCapacity(capacity);
            if (reCapacity != null)
            {
                MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Text = reCapacity.Capacityno;
                this.textBox4.Text = reCapacity.Opuser;
                this.textBox5.Text = reCapacity.Createtime;
            }
            else
            {
                MessageBox.Show("保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
