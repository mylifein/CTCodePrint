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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {



        }

        private void 产生CT码ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GenerateCTCode generateCTCode = new GenerateCTCode();
            generateCTCode.Show();
        }

        private void 客戶配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CusConfig cusConfig = new CusConfig();
            cusConfig.Show();
        }

        private void 編碼規則配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RuleConfig ruleConfig = new RuleConfig();
            ruleConfig.Show();
        }

        private void 編碼規則創建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateRules createRule = new CreateRules();
            createRule.Show();
        }
    }
}
