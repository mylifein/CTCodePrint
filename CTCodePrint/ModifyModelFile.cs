using BLL;
using CTCodePrint.common;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace CTCodePrint
{
    public partial class ModifyModelFile : Form
    {
        private readonly BarCodePrint barPrint = BarCodePrint.getInstance();
        public ModifyModelFile()
        {
            InitializeComponent();
        }

        private readonly PrintModelQ printQ = new PrintModelQ();

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            this.textBox1.Text = ofd.FileName;
            if(ofd.FileName != null && ofd.FileName != "")
            {
                string filePath = barPrint.PreviewPrintBC(ofd.FileName);
                this.pictureBox1.Load(filePath);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(this.textBox4.Text != null && this.textBox4.Text.Trim() != "")
            {
                MessageBox.Show("該文件已經上傳更新請勿重複更新！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.textBox6.Text == null || this.textBox6.Text.Trim() == "")
            {
                MessageBox.Show("请输入需要更新上文件编号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.button1.Focus();
                return;
            }
            try
            {
                string filePath = this.textBox1.Text;
                FileInfo fi = new FileInfo(@filePath);
                //获得文件大小
                long fileSize = fi.Length;
                //提取文件名
                int lastIndex = fi.FullName.LastIndexOf(@"\");
                string completeName = fi.FullName.Substring(lastIndex + 1);
                //获得文件扩展名
                string fileType = fi.Extension.Replace(".", "");
                byte[] files = FileToBytes(filePath);
                if (fileSize > 0)
                {
                    string[] type = { "png","lab" };
                    bool exists = ((IList)type).Contains(fileType.ToLower());

                    if (!exists)
                    {
                        MessageBox.Show("文档格式不对！只能为pdf格式。", "提示对话框", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    ModelFile modelFile = new ModelFile();
                    modelFile.Fileno = this.textBox3.Text.Trim();
                    modelFile.Fileaddress = files;
                    modelFile.Filename = completeName;
                    modelFile.Filedescription = this.textBox2.Text;
                    ModelFile reModelFile = printQ.updateModelFile(modelFile);
                    if(reModelFile != null)
                    {
                        this.textBox4.Text = reModelFile.Updateuser;
                        this.textBox5.Text = reModelFile.Updatetime;
                        MessageBox.Show("更新文件成功！", "提示对话框", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("更新文件失敗！", "提示对话框", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }else
                {
                    MessageBox.Show("請確認是否選擇正確的文件上傳！", "提示对话框", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("选择文件时候发生了　　" + ex.Message);
            }           

        }

        private static byte[] FileToBytes(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            byte[] buffer = new byte[fi.Length];
            FileStream fs = fi.OpenRead();
            fs.Read(buffer, 0, Convert.ToInt32(fi.Length));
            fs.Close();
            return buffer;
        }

        //string templateFile = System.IO.Directory.GetCurrentDirectory() + "\\" + ds.Tables[0].Rows[0]["model_name"].ToString();
        private static void CreateFile(byte[] fileBuffer, string newFilePath)
        {
            if (File.Exists(newFilePath))
            {
                File.Delete(newFilePath);
            }
            FileStream fs = new FileStream(newFilePath, FileMode.CreateNew);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(fileBuffer, 0, fileBuffer.Length); //用文件流生成一个文件
            bw.Close();
            fs.Close();
        }



        private void UploadFile_Load(object sender, EventArgs e)
        {
        }



        private void textBox3_Enter(object sender, EventArgs e)
        {
            string fileNo = this.textBox3.Text;
            if (fileNo == null || fileNo.Trim().Equals(""))
            {
                this.textBox3.Focus();
                return;
            }
            ModelFile modelFile = printQ.queryModelFileByExactFileNo(fileNo);
            if (modelFile != null)
            {
                this.textBox1.Text = modelFile.Filename;
                this.textBox2.Text = modelFile.Filedescription;
                this.textBox6.Text = modelFile.Opuser;
                this.textBox7.Text = modelFile.Createtime;
            }
        }
    }
}
