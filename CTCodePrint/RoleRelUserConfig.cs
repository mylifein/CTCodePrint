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
    public partial class RoleRelUserConfig : Form
    {
        public RoleRelUserConfig()
        {
            InitializeComponent();
        }
        private readonly BLL.User userService = new BLL.User();
        private readonly RoleRelUserService roleRelUserService = new RoleRelUserService();
        private readonly RoleService roleService = new RoleService();

        private void textBox2_Enter(object sender, EventArgs e)
        {

        }


        private void clearAll()
        {
            this.textBox2.Text = "";
            this.textBox3.Text = "";
            this.textBox4.Text = "";
            this.textBox5.Text = "";
            this.textBox6.Text = "";
        }

        private void displayList(string userId)
        {
            this.dataGridView1.DataSource = roleRelUserService.queryRoleUionUserList(userId);;
            if (this.dataGridView1.DataSource != null)
            {
                this.dataGridView1.Columns[0].HeaderText = "UUID";
                this.dataGridView1.Columns[1].HeaderText = "角色編號";
                this.dataGridView1.Columns[2].HeaderText = "角色名稱";
                this.dataGridView1.Columns[3].HeaderText = "用戶名稱";
                this.dataGridView1.Columns[4].HeaderText = "所有者";
                this.dataGridView1.Columns[5].HeaderText = "创建者";
                this.dataGridView1.Columns[6].HeaderText = "创建时间";
                this.dataGridView1.Columns[7].HeaderText = "更新時間";
            }

        }

        private void RoleRelUserConfig_Load(object sender, EventArgs e)
        {
            this.comboBox1.DataSource = roleService.queryRoleInfoList("");
            this.comboBox1.DisplayMember = "Roledesc";
            this.comboBox1.ValueMember = "Roleno";
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (this.textBox2.Text == null || this.textBox2.Text.Trim() == "")
            {
                MessageBox.Show("用戶名不能為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.clearAll();
                this.textBox2.Focus();
                return;
            }
            Model.User userEntity = userService.queryByUsername(this.textBox2.Text.Trim());
            if (userEntity != null)
            {
                //查詢用戶實體內容
                this.textBox3.Text = userEntity.Userid;
                this.textBox4.Text = userEntity.Userdesc;
                this.textBox5.Text = userEntity.Opuser;
                this.textBox6.Text = userEntity.Createtime;

                //查詢用戶對應的角色
                displayList(userEntity.Userid);

            }
            else
            {
                this.clearAll();
                this.textBox2.Focus();
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RoleRelUser roleRelUser = new RoleRelUser();
            roleRelUser.Userid = this.textBox3.Text.ToString().Trim();
            roleRelUser.Roleno = this.comboBox1.SelectedValue.ToString().Trim();
            if (!roleRelUserService.checkAdd(roleRelUser))
            {
                if (roleRelUserService.saveRoleRelUser(roleRelUser))
                {
                    this.displayList(roleRelUser.Userid);
                    MessageBox.Show("角色分配成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("該用戶已經存在該角色！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (roleRelUserService.deleteRoleRelMenu(uuid))
            {
                this.displayList(this.textBox3.Text.ToString().Trim());
                MessageBox.Show("刪除角色成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("刪除角色失敗！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
