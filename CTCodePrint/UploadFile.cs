using BLL;
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
    public partial class UploadFile : Form
    {
        public UploadFile()
        {
            InitializeComponent();
        }

        private readonly PrintModelQ printQ = new PrintModelQ();

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            this.textBox1.Text = ofd.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text == null || this.textBox1.Text.Trim() == "")
            {
                MessageBox.Show("請選擇需要上傳的文件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.button1.Focus();
                return;
            }
            if (this.textBox2.Text == null || this.textBox2.Text.Trim() == "")
            {
                MessageBox.Show("文件描述不能為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox2.Focus();
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
                    string[] type = { "png" };
                    bool exists = ((IList)type).Contains(fileType.ToLower());

                    if (!exists)
                    {
                        MessageBox.Show("文档格式不对！只能为pdf格式。", "提示对话框", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    ModelFile modelFile = new ModelFile();
                    modelFile.Fileaddress = files;
                    modelFile.Filename = completeName;
                    printQ.saveModelFile(modelFile);
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
        
       
       
             


    }
}
