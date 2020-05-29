using BLL;
using CTCodePrint.common;
using DBUtility;
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
    public partial class QualityCheck : Form
    {
        public QualityCheck()
        {
            InitializeComponent();
        }

        private readonly QualityInfoService qualityInfoService = new QualityInfoService();
        private readonly WoInfoService woInfoService = new WoInfoService();


        private List<QualityInfo> qualityInfos = new List<QualityInfo>();

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("請選中需要质检的单号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            int index = this.dataGridView1.CurrentRow.Index;
            string qualityNo = this.dataGridView1.Rows[index].Cells["QualiatyNo"].Value.ToString();
            QualityInfo qualityInfo = qualityInfos[index];
            qualityInfo.Status = "2";
            if (qualityInfoService.updateEndQualityInfo(qualityInfo))
            {
                WoInfo woInfo = woInfoService.queryWoInfoByNo(qualityInfo.WoNo);
                woInfo.Status = "2";
                woInfo.CheckTimes = woInfo.CheckTimes + 1;
                if (woInfoService.updateWoInfoStatusAndTimes(woInfo))
                {
                    this.qualityInfos.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                    this.dataGridView1.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                }
                   
            }else
            {
                MessageBox.Show("质检结果更新失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (this.dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("請選中需要质检的单号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            if (this.textBox2.Text == null || this.textBox2.Text.Trim() == "")
            {
                MessageBox.Show("質檢不合格，請輸入不合格原因！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Focus();
                return;
            }
            int index = this.dataGridView1.CurrentRow.Index;
            string qualityNo = this.dataGridView1.Rows[index].Cells["QualiatyNo"].Value.ToString();
            QualityInfo qualityInfo = qualityInfos[index];
            qualityInfo.Status = "3";
            qualityInfo.Remark = this.textBox2.Text;
            if (qualityInfoService.updateEndQualityInfo(qualityInfo))
            {
                WoInfo woInfo = woInfoService.queryWoInfoByNo(qualityInfo.WoNo);
                woInfo.Status = "3";
                woInfo.CheckTimes = woInfo.CheckTimes + 1;
                if (woInfoService.updateWoInfoStatusAndTimes(woInfo))
                {
                    this.qualityInfos.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                    this.dataGridView1.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                }
            }
            else
            {
                MessageBox.Show("质检结果更新失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }




        private void QualityCheck_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = "QualiatyNo";
            column.DataPropertyName = "QualiatyNo";//对应数据源的字段
            column.HeaderText = "质检编号";
            column.Width = 100;
            this.dataGridView1.Columns.Add(column);
            DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
            column1.Name = "WoNo";
            column1.DataPropertyName = "WoNo";//对应数据源的字段
            column1.HeaderText = "工单号";
            column1.Width = 100;
            this.dataGridView1.Columns.Add(column1);
            DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
            column2.Name = "StartTime";
            column2.DataPropertyName = "StartTime";//对应数据源的字段
            column2.HeaderText = "质检开始时间";
            column2.Width = 180;
            this.dataGridView1.Columns.Add(column2);

            DataGridViewTextBoxColumn column3 = new DataGridViewTextBoxColumn();
            column3.Name = "EndTime";
            column3.DataPropertyName = "EndTime";//对应数据源的字段
            column3.HeaderText = "质检结束时间";
            column3.Width = 180;
            this.dataGridView1.Columns.Add(column3);

            DataGridViewTextBoxColumn column4 = new DataGridViewTextBoxColumn();
            column4.Name = "DuringTime";
            column4.DataPropertyName = "DuringTime";//对应数据源的字段
            column4.HeaderText = "质检时长";
            column4.Width = 100;
            this.dataGridView1.Columns.Add(column4);

            DataGridViewTextBoxColumn column5 = new DataGridViewTextBoxColumn();
            column5.Name = "woQuantity";
            column5.DataPropertyName = "wo_quantity";//对应数据源的字段
            column5.HeaderText = "质检状态";
            column5.Width = 80;
            this.dataGridView1.Columns.Add(column5);

            DataGridViewTextBoxColumn column6 = new DataGridViewTextBoxColumn();
            column6.Name = "create_time";
            column6.DataPropertyName = "create_time";//对应数据源的字段
            column6.HeaderText = "送检时间";
            column6.Width = 180;
            this.dataGridView1.Columns.Add(column6);


            DataGridViewTextBoxColumn column7 = new DataGridViewTextBoxColumn();
            column7.Name = "deliverMan";
            column7.DataPropertyName = "deliverMan";//对应数据源的字段
            column7.HeaderText = "送检人";
            column7.Width = 80;
            this.dataGridView1.Columns.Add(column7);

        }

        public void initRow(int index, QualityInfo qualityInfo)
        {
            this.dataGridView1.Rows[index].Cells[0].Value = qualityInfo.QualiatyNo;
            this.dataGridView1.Rows[index].Cells[1].Value = qualityInfo.WoNo;
            this.dataGridView1.Rows[index].Cells[2].Value = qualityInfo.StartTime;
            this.dataGridView1.Rows[index].Cells[3].Value = qualityInfo.EndTime;
            this.dataGridView1.Rows[index].Cells[4].Value = qualityInfo.DuringTime;
            this.dataGridView1.Rows[index].Cells[5].Value = qualityInfo.Status == "1" ? "质检中" :"未开始";
            this.dataGridView1.Rows[index].Cells[6].Value = qualityInfo.Createtime;
            this.dataGridView1.Rows[index].Cells[7].Value = qualityInfo.DeliverMan;
            this.textBox1.Text = "";
            qualityInfos.Add(qualityInfo);

        }

        /// <summary>
        /// 检查质检单号是否存在
        /// </summary>
        /// <param name="qualityNo"></param>
        /// <returns></returns>
        public bool checkRepeat(string qualityNo)
        {
            foreach (QualityInfo compareValue in qualityInfos)
            {
                if (compareValue.QualiatyNo.Equals(qualityNo))
                {
                    return true;
                }
            }
            return false;

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
                QualityInfo qualityInfo = qualityInfoService.queryQualityInfoByNo(this.textBox1.Text.Trim());
                if (qualityInfo !=null)
                {
                    switch (qualityInfo.Status)
                    {
                        case "0":
                            QualityInfo updateQualInfo = new QualityInfo();
                            updateQualInfo.StartTime = DateTime.Now;
                            updateQualInfo.QualiatyNo = qualityInfo.QualiatyNo;
                            updateQualInfo.Status = "1";
                            QualityInfo reQualityInfo = qualityInfoService.updateStartQualityInfo(updateQualInfo);
                            //更新工单状态
                            WoInfo updatedWoInfo = new WoInfo();
                            updatedWoInfo.Status = "1";
                            updatedWoInfo.WoNo = reQualityInfo.WoNo;
                            woInfoService.updateWoInfoStatus(updatedWoInfo);            //更新工单状态.
                            if (!checkRepeat(reQualityInfo.QualiatyNo))
                            {
                                int index = this.dataGridView1.Rows.Add();
                                initRow(index, reQualityInfo);
                            }
                            break;
                        case "1":                           //质检中
                            if (!checkRepeat(qualityInfo.QualiatyNo))
                            {
                                int index = this.dataGridView1.Rows.Add();
                                initRow(index, qualityInfo);
                            }
                            break;
                        case "2":                          //质检Ok
                            MessageBox.Show("该工单首件质检标签已完成，不能再进行质检操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.textBox1.Text = "";
                            this.textBox1.Focus();
                            break;
                        case "3":                           //质检NG
                            MessageBox.Show("该工单首件质检标签已完成，不能再进行质检操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.textBox1.Text = "";
                            this.textBox1.Focus();
                            break;
                    }
                }else
                {
                    this.textBox1.Text = "";
                    this.textBox1.Focus();
                    return;
                }

            }
        }
    }
}
