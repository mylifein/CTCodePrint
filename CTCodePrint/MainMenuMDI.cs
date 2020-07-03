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
using DBUtility;

namespace CTCodePrint
{
    public partial class MainMenuMDI : Form
    {
        private int childFormNumber = 0;
        public static List<itemEx> list;
        public MainMenuMDI()
        {
            InitializeComponent();
            toolStripStatusLabel4.Text = CTCodeLogin.user;
            ct();
        }
        private readonly RoleRelMenuService roleRelMenuService = new RoleRelMenuService();

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ReprintCT printCT = new ReprintCT();
            printCT.MdiParent = this;
            printCT.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GenerateCTCode generateCTCode = new GenerateCTCode();
            generateCTCode.MdiParent = this;
            generateCTCode.Show();
        }


        private void 編碼配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateRules createRule = new CreateRules();
            createRule.MdiParent = this;
            createRule.Show();
        }

        private void 編碼查詢ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RuleConfig ruleConfig = new RuleConfig();
            ruleConfig.MdiParent = this;
            ruleConfig.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            MandatoryConfig manConfig = new MandatoryConfig();
            manConfig.MdiParent = this;
            manConfig.Show();
        }



        private void 模板上傳ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UploadFile uploadFile = new UploadFile();
            uploadFile.MdiParent = this;
            uploadFile.Show();
        }



        private void 機種關係綁定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BoundRule boundRule = new BoundRule();
            boundRule.MdiParent = this;
            boundRule.Show();
        }

        private void 創建角色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateRole createRole = new CreateRole();
            createRole.MdiParent = this;
            createRole.Show();
        }

        private void 用戶角色分配ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RoleRelUserConfig roleRelUserConfig = new RoleRelUserConfig();
            roleRelUserConfig.MdiParent = this;
            roleRelUserConfig.Show();
        }



        private void MainMenuMDI_Load(object sender, EventArgs e)
        {
            this.disableMenuItems();
            this.Refresh();
        }
        public void ct()
        {
            foreach (ToolStripMenuItem item in menuStrip.Items)  //获取一级菜单
            {
                string text = item.Text;
                string name = item.Name;
                if (list == null) { list = new List<itemEx>(); }
                list.Add(new itemEx(text, name));
                foreach (ToolStripMenuItem items in item.DropDownItems)//获取二级菜单
                {
                    text = items.Text;
                    name = items.Name;
                    if (list == null) { list = new List<itemEx>(); }
                    list.Add(new itemEx(text, name));
                }
            }

            return;
        }
        public class itemEx
        {
            public string Name { get; set; }
            public object Tag { get; set; }
            public itemEx(string s, object o)
            {
                Name = s;
                Tag = o;
            }
        }
        private void disableMenuItems()
        {
            List<RoleUnionMenu> roleUnionMenuList = roleRelMenuService.queryRoleUionMenuList(Auxiliary.RoleNo);
            if (roleUnionMenuList != null)
            {
                foreach (ToolStripMenuItem item in this.menuStrip.Items)  //获取一级菜单
                {
                    string name = item.Name;
                    bool isVisible = this.compareMenu(name, roleUnionMenuList);
                    item.Visible = isVisible;
                    if (isVisible)
                    {
                        foreach (ToolStripMenuItem subItem in item.DropDownItems)//获取二级菜单
                        {
                            name = subItem.Name;
                            isVisible = this.compareMenu(name, roleUnionMenuList);
                            subItem.Visible = isVisible;
                        }
                    }

                }
            }
        }



        /// <summary>
        /// 比较角色中是否包含该菜单
        /// </summary>
        /// <param name="menuName"></param>
        /// <param name="roleUnionMenuList"></param>
        /// <returns></returns>
        private bool compareMenu(string menuName, List<RoleUnionMenu> roleUnionMenuList)
        {
            foreach (RoleUnionMenu roleUnionMenu in roleUnionMenuList)
            {
                if(menuName == roleUnionMenu.Menuname)
                {
                    return true;
                }
            }
            return false;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = DateTime.Now.ToString();
        }

        private void 字段類型配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateField createField = new CreateField();
            createField.MdiParent = this;
            createField.Show();
        }

        private void 字段規則配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManRelFieldTypeConfig manRelFieldTypeConfig = new ManRelFieldTypeConfig();
            manRelFieldTypeConfig.MdiParent = this;
            manRelFieldTypeConfig.Show();
        }

        private void 模板字段規則綁定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BoundModelParam boundModelParam = new BoundModelParam();
            boundModelParam.MdiParent = this;
            boundModelParam.Show();
        }

        private void 模板文件綁定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BoundModelFile boundModelFile = new BoundModelFile();
            boundModelFile.MdiParent = this;
            boundModelFile.Show();
        }

        private void MainMenuMDI_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
            Application.Exit();
        }



        private void 部門創建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateDepartment createDepartment = new CreateDepartment();
            createDepartment.MdiParent = this;
            createDepartment.Show();
        }

        private void 線別設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateProdLine createProdLine = new CreateProdLine();
            createProdLine.MdiParent = this;
            createProdLine.Show();
        }

        private void 裝箱單號打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CartonPrint cartonPrint = new CartonPrint();
            cartonPrint.MdiParent = this;
            cartonPrint.Show();
        }

        private void 单出件装箱单打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CartonDirectPrint cartonDirectPrint = new CartonDirectPrint();
            cartonDirectPrint.MdiParent = this;
            cartonDirectPrint.Show();
        }

        private void 棧板標籤打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PalletPrint palletPrint = new PalletPrint();
            palletPrint.MdiParent = this;
            palletPrint.Show();
        }

        private void 創建容量ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateCapacity createCapacity = new CreateCapacity();
            createCapacity.MdiParent = this;
            createCapacity.Show();
        }

        private void 容量綁定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BoundCapacity boundCapacity = new BoundCapacity();
            boundCapacity.MdiParent = this;
            boundCapacity.Show();
        }



        private void 創建用戶ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateUser createUser = new CreateUser();
            createUser.MdiParent = this;
            createUser.Show();
        }

        private void 模板修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModifyModelFile modifyModelFile = new ModifyModelFile();
            modifyModelFile.MdiParent = this;
            modifyModelFile.Show();
        }

        private void 重印裝箱單ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReprintCarton reprintCarton = new ReprintCarton();
            reprintCarton.MdiParent = this;
            reprintCarton.Show();
        }


        private void 子阶料号维护ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SubmatMaintain submatMaintain = new SubmatMaintain();
            submatMaintain.MdiParent = this;
            submatMaintain.Show();
        }

        private void BoundRuleToolStripMenuItem(object sender, EventArgs e)
        {
            BoundRule boundRule = new BoundRule();
            boundRule.MdiParent = this;
            boundRule.Show();
        }

        private void 角色权限分配ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RoleRelMenuConfig roleRelMenuConfig = new RoleRelMenuConfig();
            roleRelMenuConfig.MdiParent = this;
            roleRelMenuConfig.Show();
        }

        private void 品质首件质检标签ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QualityCheckPrint qualityCheckPrint = new QualityCheckPrint();
            qualityCheckPrint.MdiParent = this;
            qualityCheckPrint.Show();
        }

        private void 工单首件检验ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QualityCheck qualityCheck = new QualityCheck();
            qualityCheck.MdiParent = this;
            qualityCheck.Show();
        }

        private void ReprintPalletMenuItem_Click(object sender, EventArgs e)
        {
            ReprintPallet reprintPallet = new ReprintPallet();
            reprintPallet.MdiParent = this;
            reprintPallet.Show();
        }

        private void externalMenu_Click(object sender, EventArgs e)
        {
            CtExternalprint ctExternalprint = new CtExternalprint();
            ctExternalprint.MdiParent = this;
            ctExternalprint.Show();
        }
    }
}
