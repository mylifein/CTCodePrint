using BLL;
using CTCodePrint.common;
using GenerateCTCode;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CTCodePrint
{
    public partial class CTCodeLogin : Form
    {
        public CTCodeLogin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = this.textBox1.Text;
            string password = this.textBox2.Text;
            //判斷
            if (username != "admin" || password != "123")
            {
                MessageBox.Show("密碼或者用戶名錯誤！");
            }
            else
            {
                MainMenu mainMenu = new MainMenu();
                mainMenu.Show();
                this.Hide();
            }
            GregorianCalendar gc = new GregorianCalendar();
            DateTime dt = new DateTime(2019, 1, 1);
            string week = "";
            if(gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday).ToString().Length < 2)
            {
                week = "0" + gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday).ToString();
            }
            else
            {
                week = gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday).ToString();
            }
            string yearString = DateTime.Now.Year.ToString();
            yearString.Substring(yearString.Length - 1);
            //test connection mysql
            string connectString = "server=127.0.0.1;port=3306;database=datahub;user=root;password=root;";
            MySqlConnection conn = new MySqlConnection(connectString);
            DataTable dtable = new DataTable();

            DataSet ds = new DataSet();
            ds = new OracleQueryB().getWorkInfoByNo("K1927483");
            string templateFileName = System.IO.Directory.GetCurrentDirectory() + "\\SH17003H0161401-00CT.lab";            //“System.IO.Directory.GetCurrentDirectory”:获取当前应用程序的路径，最后不包含“\”；
            //判斷文件存在否
            if (!File.Exists(templateFileName))
            {
                MessageBox.Show("打印模板文件不存在,無法打印外箱貼紙", "信息", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return ;
            }
            BarCodePrint bc = new BarCodePrint();
            String[] sA = new String[27];
            for (int c = 0; c < 27; c++)
            {
                sA[c] = "";                             //為數組賦值為空串""
            }
            sA[0] = "123";
            bc.PrintBC(templateFileName, sA);
            string ctcode = new GenerateCode().generateCTNumber("PO123", "C001", "W001", "BU6", "CM001", "O001", "01").ToString();
            
            try
            {
                conn.Open();
                string sqlStr = "select * from t_user where user_id = '1'";
                MySqlCommand cmd = new MySqlCommand(sqlStr,conn);
                cmd.CommandType = CommandType.Text;
                MySqlDataAdapter msda = new MySqlDataAdapter(cmd);
                msda.Fill(ds);
                dtable = ds.Tables[0];


            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }finally
            {
                conn.Close();
            }
     
            //end test mysql connection
            this.textBox1.Text = week;
            this.textBox2.Text = System.Guid.NewGuid().ToString("N"); 
            
            

        }
    }
}
