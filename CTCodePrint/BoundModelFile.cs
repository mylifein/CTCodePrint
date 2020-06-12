﻿using BLL;
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
    public partial class BoundModelFile : Form
    {
        public BoundModelFile()
        {
            InitializeComponent();
        }

        private readonly ModelInfoService modelInfoService = new ModelInfoService();
        private readonly FileRelDelService fileRelDelService = new FileRelDelService();
        private readonly OracleQueryB oracleQueryB = new OracleQueryB();

        private bool saveMark = false;

        private void BoundModel_Load(object sender, EventArgs e)
        {
            this.comboBox1.DataSource = modelInfoService.queryModelFileList();
            this.comboBox1.ValueMember = "Fileno";
            this.comboBox1.DisplayMember = "Filedescription";


            this.comboBox2.DisplayMember = "CusName";
            this.comboBox2.ValueMember = "CusNo";
            this.comboBox2.DataSource = oracleQueryB.getCusInfo();


            DataTable itemTable2 = new DataTable();   // construct selects value
            DataColumn columnType;
            DataRow rowType;
            columnType = new DataColumn("Name");
            itemTable2.Columns.Add(columnType);
            columnType = new DataColumn("Value");
            itemTable2.Columns.Add(columnType);
            rowType = itemTable2.NewRow();
            rowType["Name"] = "CT";
            rowType["Value"] = "0";
            itemTable2.Rows.Add(rowType);
            rowType = itemTable2.NewRow();
            rowType["Name"] = "裝箱";
            rowType["Value"] = "1";
            itemTable2.Rows.Add(rowType);
            rowType = itemTable2.NewRow();
            rowType["Name"] = "棧板";
            rowType["Value"] = "2";
            itemTable2.Rows.Add(rowType);
            this.comboBox3.DisplayMember = "Name";
            this.comboBox3.ValueMember = "Value";
            this.comboBox3.DataSource = itemTable2;
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
            string boundType = this.comboBox3.SelectedValue == null ? "0" : this.comboBox3.SelectedValue.ToString();
            FileRelDel fileRelDel = new FileRelDel();
            fileRelDel.FileNo = this.comboBox1.SelectedValue.ToString();
            fileRelDel.CusNo = this.comboBox2.SelectedValue.ToString().Trim();
            fileRelDel.DelMatno = this.textBox1.Text.Trim().ToString().Trim();
            fileRelDel.BoundType = boundType;

            if (fileRelDelService.checkAdd(fileRelDel))
            {
                MessageBox.Show("該客戶和出貨料號的打印模板已經綁定，請勿重複綁定！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            FileRelDel reFileRelDel = fileRelDelService.saveFileRelDel(fileRelDel);
            if (reFileRelDel != null)
            {
                this.textBox2.Text = reFileRelDel.CreateTime;
                saveMark = true;
                MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
