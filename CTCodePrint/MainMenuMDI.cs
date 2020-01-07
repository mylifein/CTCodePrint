﻿using BLL;
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
            toolStripStatusLabel4.Text =CTCodeLogin.user;
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

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            CusConfig cusConfig = new CusConfig();
            cusConfig.MdiParent = this;
            cusConfig.Show();
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

        private void 模板字段維護ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModelConfig modelConfig = new ModelConfig();
            modelConfig.MdiParent = this;
            modelConfig.Show();
        }

        private void 模板綁定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BoundModel boundModel = new BoundModel();
            boundModel.MdiParent = this;
            boundModel.Show();
        }

        private void 模板上傳ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UploadFile uploadFile = new UploadFile();
            uploadFile.MdiParent = this;
            uploadFile.Show();
        }

        private void 機種信息配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MachineTypeConfig macTypeConfig = new MachineTypeConfig();
            macTypeConfig.MdiParent = this;
            macTypeConfig.Show();
        }

        private void 機種信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {

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

        private void 創建權限菜單ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateMenu createMenu = new CreateMenu();
            createMenu.MdiParent = this;
            createMenu.Show();
        }

        private void 菜單權限分配ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RoleRelMenuConfig roleRelMenuConfig = new RoleRelMenuConfig();
            roleRelMenuConfig.MdiParent = this;
            roleRelMenuConfig.Show();
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
                for (int i = 0; i < this.menuStrip.Items.Count; i++)
                {

                    if (!(this.menuStrip.Items[i] is ToolStripSeparator))
                    {
                        this.menuStrip.Items[i].Visible= false;
                        foreach (RoleUnionMenu roleUnionMenu in roleUnionMenuList)
                        {
                            if (roleUnionMenu.Menuname == this.menuStrip.Items[i].Name.ToString().Trim())
                            {
                                this.menuStrip.Items[i].Visible= true;
                            }
                        }

                    }
                }
            }
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
            ModelBoundField modelBoundField = new ModelBoundField();
            modelBoundField.MdiParent = this;
            modelBoundField.Show();
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

        private void 料號子階維護ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SubmatMaintain submatMaintain = new SubmatMaintain();
            submatMaintain.MdiParent = this;
            submatMaintain.Show();
        }
    }
}
