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
    public partial class RoleRelMenuConfig : Form
    {
        public RoleRelMenuConfig()
        {
            InitializeComponent();
        }
        private readonly RoleService roleService = new RoleService();
        private readonly RoleRelMenuService roleRelMenuService = new RoleRelMenuService();

        private void RoleRelMenuConfig_Load(object sender, EventArgs e)
        {
            this.comboBox1.DataSource = roleService.queryRoleInfoList("");
            this.comboBox1.DisplayMember = "Roledesc";
            this.comboBox1.ValueMember = "Roleno";


            this.comboBox2.DataSource = MainMenuMDI.list;
            this.comboBox2.ValueMember = "Tag";
            this.comboBox2.DisplayMember = "name";


            this.displayList(this.comboBox1.SelectedValue.ToString());
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string roleNo = this.comboBox1.SelectedValue.ToString().Trim();
            this.dataGridView1.DataSource = roleRelMenuService.queryRoleUionMenuList(roleNo);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RoleRelMenu roleRelMenu = new RoleRelMenu();
            roleRelMenu.Roleno = this.comboBox1.SelectedValue.ToString().Trim();
            roleRelMenu.MenuName = this.comboBox2.SelectedValue.ToString().Trim();
            roleRelMenu.MenuDesc = this.comboBox2.Text;

            if (!roleRelMenuService.checkAdd(roleRelMenu))
            {
                RoleRelMenu reRoleRelMenu = roleRelMenuService.saveRoleRelMenu(roleRelMenu);
                if(reRoleRelMenu != null)
                {
                    this.displayList(roleRelMenu.Roleno);
                    MessageBox.Show("菜單添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("該角色已經存在該菜單！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void displayList(string roleNo)
        {
            this.dataGridView1.DataSource = roleRelMenuService.queryRoleUionMenuList(roleNo);
            if(this.dataGridView1.DataSource != null)
            {
                this.dataGridView1.Columns[0].HeaderText = "UUID";
                this.dataGridView1.Columns[1].HeaderText = "角色編號";
                this.dataGridView1.Columns[2].HeaderText = "角色名稱";
                this.dataGridView1.Columns[3].HeaderText = "菜單編號";
                this.dataGridView1.Columns[4].HeaderText = "菜單名稱";
                this.dataGridView1.Columns[5].HeaderText = "菜單描述";
                this.dataGridView1.Columns[6].HeaderText = "创建者";
                this.dataGridView1.Columns[7].HeaderText = "创建时间";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("請選中需要刪除的行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string uuid = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            if (roleRelMenuService.deleteRoleRelMenu(uuid))
            {
                this.displayList(this.comboBox1.SelectedValue.ToString());
                MessageBox.Show("刪除菜單成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }else
            {
                MessageBox.Show("刪除失敗！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
