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



        private void 編碼規則創建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateRules createRule = new CreateRules();
            createRule.Show();
        }

        private void 重印CTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReprintCT printCT = new ReprintCT();
            printCT.Show();
        }

        private void 編碼規則查詢ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RuleConfig ruleConfig = new RuleConfig();
            ruleConfig.Show();
        }



        private void 模板字段配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MandatoryConfig manConfig = new MandatoryConfig();
            manConfig.Show();
        }

        private void 模板信息維護ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModelConfig modelConfig = new ModelConfig();
            modelConfig.Show();
        }

        private void 機種信息維護ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MachineTypeConfig macTypeConfig = new MachineTypeConfig();
            macTypeConfig.Show();
        }

        private void 客戶機種關係維護ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BoundRule boundRule = new BoundRule();
            boundRule.Show();
        }

        private void 模板綁定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BoundModel boundModel = new BoundModel();
            boundModel.Show();
        }
    }
}
