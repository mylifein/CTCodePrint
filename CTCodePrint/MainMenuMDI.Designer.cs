namespace CTCodePrint
{
    partial class MainMenuMDI
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ctToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reprintCTtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cartonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.singCartonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reprintCartonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PalletToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.栈板标签补印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.品质首件质检标签ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ruleMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.codeRuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ruleQueryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ruleBoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maintainMatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.modelParamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.singFieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelParamUnionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelBoundParamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelBoundCusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.capacityCreateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.capacityBoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelUploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.userCreateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.roleCreateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.roleAssignToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deptCreateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prodLineMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.角色权限分配ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.工单首件检验ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.ruleMenu,
            this.modelMenu,
            this.userManagement});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(843, 28);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctToolStripMenuItem,
            this.reprintCTtoolStripMenuItem,
            this.cartonToolStripMenuItem,
            this.singCartonToolStripMenuItem,
            this.reprintCartonToolStripMenuItem,
            this.PalletToolStripMenuItem,
            this.栈板标签补印ToolStripMenuItem,
            this.品质首件质检标签ToolStripMenuItem,
            this.工单首件检验ToolStripMenuItem});
            this.fileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(69, 24);
            this.fileMenu.Text = "菜单(&F)";
            // 
            // ctToolStripMenuItem
            // 
            this.ctToolStripMenuItem.Name = "ctToolStripMenuItem";
            this.ctToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            this.ctToolStripMenuItem.Text = "CT码打印";
            this.ctToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // reprintCTtoolStripMenuItem
            // 
            this.reprintCTtoolStripMenuItem.Name = "reprintCTtoolStripMenuItem";
            this.reprintCTtoolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            this.reprintCTtoolStripMenuItem.Text = "CT码补印";
            this.reprintCTtoolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // cartonToolStripMenuItem
            // 
            this.cartonToolStripMenuItem.Name = "cartonToolStripMenuItem";
            this.cartonToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            this.cartonToolStripMenuItem.Text = "装箱单标签打印(整机）";
            this.cartonToolStripMenuItem.Click += new System.EventHandler(this.裝箱單號打印ToolStripMenuItem_Click);
            // 
            // singCartonToolStripMenuItem
            // 
            this.singCartonToolStripMenuItem.Name = "singCartonToolStripMenuItem";
            this.singCartonToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            this.singCartonToolStripMenuItem.Text = "装箱单标签打印(单出）";
            this.singCartonToolStripMenuItem.Click += new System.EventHandler(this.单出件装箱单打印ToolStripMenuItem_Click);
            // 
            // reprintCartonToolStripMenuItem
            // 
            this.reprintCartonToolStripMenuItem.Name = "reprintCartonToolStripMenuItem";
            this.reprintCartonToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            this.reprintCartonToolStripMenuItem.Text = "装箱单标签补印";
            this.reprintCartonToolStripMenuItem.Click += new System.EventHandler(this.重印裝箱單ToolStripMenuItem_Click);
            // 
            // PalletToolStripMenuItem
            // 
            this.PalletToolStripMenuItem.Name = "PalletToolStripMenuItem";
            this.PalletToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            this.PalletToolStripMenuItem.Text = "栈板标签打印";
            this.PalletToolStripMenuItem.Click += new System.EventHandler(this.棧板標籤打印ToolStripMenuItem_Click);
            // 
            // 栈板标签补印ToolStripMenuItem
            // 
            this.栈板标签补印ToolStripMenuItem.Name = "栈板标签补印ToolStripMenuItem";
            this.栈板标签补印ToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            this.栈板标签补印ToolStripMenuItem.Text = "栈板标签补印";
            // 
            // 品质首件质检标签ToolStripMenuItem
            // 
            this.品质首件质检标签ToolStripMenuItem.Name = "品质首件质检标签ToolStripMenuItem";
            this.品质首件质检标签ToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            this.品质首件质检标签ToolStripMenuItem.Text = "品质首件质检标签";
            this.品质首件质检标签ToolStripMenuItem.Click += new System.EventHandler(this.品质首件质检标签ToolStripMenuItem_Click);
            // 
            // ruleMenu
            // 
            this.ruleMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.codeRuleToolStripMenuItem,
            this.ruleQueryToolStripMenuItem,
            this.ruleBoundToolStripMenuItem,
            this.maintainMatToolStripMenuItem});
            this.ruleMenu.Name = "ruleMenu";
            this.ruleMenu.Size = new System.Drawing.Size(99, 24);
            this.ruleMenu.Text = "编码配置(&E)";
            // 
            // codeRuleToolStripMenuItem
            // 
            this.codeRuleToolStripMenuItem.Name = "codeRuleToolStripMenuItem";
            this.codeRuleToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.codeRuleToolStripMenuItem.Text = "编码配置";
            this.codeRuleToolStripMenuItem.Click += new System.EventHandler(this.編碼配置ToolStripMenuItem_Click);
            // 
            // ruleQueryToolStripMenuItem
            // 
            this.ruleQueryToolStripMenuItem.Name = "ruleQueryToolStripMenuItem";
            this.ruleQueryToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.ruleQueryToolStripMenuItem.Text = "编码查询";
            this.ruleQueryToolStripMenuItem.Click += new System.EventHandler(this.編碼查詢ToolStripMenuItem_Click);
            // 
            // ruleBoundToolStripMenuItem
            // 
            this.ruleBoundToolStripMenuItem.Name = "ruleBoundToolStripMenuItem";
            this.ruleBoundToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.ruleBoundToolStripMenuItem.Text = "编码规则绑定";
            this.ruleBoundToolStripMenuItem.Click += new System.EventHandler(this.BoundRuleToolStripMenuItem);
            // 
            // maintainMatToolStripMenuItem
            // 
            this.maintainMatToolStripMenuItem.Name = "maintainMatToolStripMenuItem";
            this.maintainMatToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.maintainMatToolStripMenuItem.Text = "子阶料号维护";
            this.maintainMatToolStripMenuItem.Click += new System.EventHandler(this.子阶料号维护ToolStripMenuItem_Click);
            // 
            // modelMenu
            // 
            this.modelMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modelParamToolStripMenuItem,
            this.singFieldToolStripMenuItem,
            this.modelParamUnionToolStripMenuItem,
            this.modelBoundParamToolStripMenuItem,
            this.modelBoundCusToolStripMenuItem,
            this.capacityCreateToolStripMenuItem,
            this.capacityBoundToolStripMenuItem,
            this.modelUploadToolStripMenuItem,
            this.modelUpdateToolStripMenuItem});
            this.modelMenu.Name = "modelMenu";
            this.modelMenu.Size = new System.Drawing.Size(101, 24);
            this.modelMenu.Text = "模板配置(&V)";
            // 
            // modelParamToolStripMenuItem
            // 
            this.modelParamToolStripMenuItem.Name = "modelParamToolStripMenuItem";
            this.modelParamToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.modelParamToolStripMenuItem.Text = "标签参数字段配置";
            this.modelParamToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // singFieldToolStripMenuItem
            // 
            this.singFieldToolStripMenuItem.Name = "singFieldToolStripMenuItem";
            this.singFieldToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.singFieldToolStripMenuItem.Text = "字段類型配置";
            this.singFieldToolStripMenuItem.Click += new System.EventHandler(this.字段類型配置ToolStripMenuItem_Click);
            // 
            // modelParamUnionToolStripMenuItem
            // 
            this.modelParamUnionToolStripMenuItem.Name = "modelParamUnionToolStripMenuItem";
            this.modelParamUnionToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.modelParamUnionToolStripMenuItem.Text = "标签参数与字段绑定";
            this.modelParamUnionToolStripMenuItem.Click += new System.EventHandler(this.字段規則配置ToolStripMenuItem_Click);
            // 
            // modelBoundParamToolStripMenuItem
            // 
            this.modelBoundParamToolStripMenuItem.Name = "modelBoundParamToolStripMenuItem";
            this.modelBoundParamToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.modelBoundParamToolStripMenuItem.Text = "标签参数綁定";
            this.modelBoundParamToolStripMenuItem.Click += new System.EventHandler(this.模板字段規則綁定ToolStripMenuItem_Click);
            // 
            // modelBoundCusToolStripMenuItem
            // 
            this.modelBoundCusToolStripMenuItem.Name = "modelBoundCusToolStripMenuItem";
            this.modelBoundCusToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.modelBoundCusToolStripMenuItem.Text = "标签模板文件綁定";
            this.modelBoundCusToolStripMenuItem.Click += new System.EventHandler(this.模板文件綁定ToolStripMenuItem_Click);
            // 
            // capacityCreateToolStripMenuItem
            // 
            this.capacityCreateToolStripMenuItem.Name = "capacityCreateToolStripMenuItem";
            this.capacityCreateToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.capacityCreateToolStripMenuItem.Text = "创建容量";
            this.capacityCreateToolStripMenuItem.Click += new System.EventHandler(this.創建容量ToolStripMenuItem_Click);
            // 
            // capacityBoundToolStripMenuItem
            // 
            this.capacityBoundToolStripMenuItem.Name = "capacityBoundToolStripMenuItem";
            this.capacityBoundToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.capacityBoundToolStripMenuItem.Text = "容量綁定";
            this.capacityBoundToolStripMenuItem.Click += new System.EventHandler(this.容量綁定ToolStripMenuItem_Click);
            // 
            // modelUploadToolStripMenuItem
            // 
            this.modelUploadToolStripMenuItem.Name = "modelUploadToolStripMenuItem";
            this.modelUploadToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.modelUploadToolStripMenuItem.Text = "标签模板上傳";
            this.modelUploadToolStripMenuItem.Click += new System.EventHandler(this.模板上傳ToolStripMenuItem_Click);
            // 
            // modelUpdateToolStripMenuItem
            // 
            this.modelUpdateToolStripMenuItem.Name = "modelUpdateToolStripMenuItem";
            this.modelUpdateToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.modelUpdateToolStripMenuItem.Text = "标签模板修改";
            this.modelUpdateToolStripMenuItem.Click += new System.EventHandler(this.模板修改ToolStripMenuItem_Click);
            // 
            // userManagement
            // 
            this.userManagement.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userCreateToolStripMenuItem,
            this.roleCreateToolStripMenuItem,
            this.roleAssignToolStripMenuItem,
            this.deptCreateMenuItem,
            this.prodLineMenuItem,
            this.角色权限分配ToolStripMenuItem});
            this.userManagement.Name = "userManagement";
            this.userManagement.Size = new System.Drawing.Size(102, 24);
            this.userManagement.Text = "用戶管理(&U)";
            // 
            // userCreateToolStripMenuItem
            // 
            this.userCreateToolStripMenuItem.Name = "userCreateToolStripMenuItem";
            this.userCreateToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.userCreateToolStripMenuItem.Text = "创建用户";
            this.userCreateToolStripMenuItem.Click += new System.EventHandler(this.創建用戶ToolStripMenuItem_Click);
            // 
            // roleCreateToolStripMenuItem
            // 
            this.roleCreateToolStripMenuItem.Name = "roleCreateToolStripMenuItem";
            this.roleCreateToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.roleCreateToolStripMenuItem.Text = "创建角色";
            this.roleCreateToolStripMenuItem.Click += new System.EventHandler(this.創建角色ToolStripMenuItem_Click);
            // 
            // roleAssignToolStripMenuItem
            // 
            this.roleAssignToolStripMenuItem.Name = "roleAssignToolStripMenuItem";
            this.roleAssignToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.roleAssignToolStripMenuItem.Text = "用戶角色分配";
            this.roleAssignToolStripMenuItem.Click += new System.EventHandler(this.用戶角色分配ToolStripMenuItem_Click);
            // 
            // deptCreateMenuItem
            // 
            this.deptCreateMenuItem.Name = "deptCreateMenuItem";
            this.deptCreateMenuItem.Size = new System.Drawing.Size(174, 26);
            this.deptCreateMenuItem.Text = "部门设定";
            this.deptCreateMenuItem.Click += new System.EventHandler(this.部門創建ToolStripMenuItem_Click);
            // 
            // prodLineMenuItem
            // 
            this.prodLineMenuItem.Name = "prodLineMenuItem";
            this.prodLineMenuItem.Size = new System.Drawing.Size(174, 26);
            this.prodLineMenuItem.Text = "线别设定";
            this.prodLineMenuItem.Click += new System.EventHandler(this.線別設定ToolStripMenuItem_Click);
            // 
            // 角色权限分配ToolStripMenuItem
            // 
            this.角色权限分配ToolStripMenuItem.Name = "角色权限分配ToolStripMenuItem";
            this.角色权限分配ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.角色权限分配ToolStripMenuItem.Text = "角色权限分配";
            this.角色权限分配ToolStripMenuItem.Click += new System.EventHandler(this.角色权限分配ToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4});
            this.statusStrip.Location = new System.Drawing.Point(0, 497);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip.Size = new System.Drawing.Size(843, 25);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(54, 20);
            this.toolStripStatusLabel.Text = "状态：";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(84, 20);
            this.toolStripStatusLabel1.Text = "当前时间：";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 20);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(84, 20);
            this.toolStripStatusLabel3.Text = "当前用户：";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(167, 20);
            this.toolStripStatusLabel4.Text = "toolStripStatusLabel4";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // 工单首件检验ToolStripMenuItem
            // 
            this.工单首件检验ToolStripMenuItem.Name = "工单首件检验ToolStripMenuItem";
            this.工单首件检验ToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            this.工单首件检验ToolStripMenuItem.Text = "工单首件检验";
            this.工单首件检验ToolStripMenuItem.Click += new System.EventHandler(this.工单首件检验ToolStripMenuItem_Click);
            // 
            // MainMenuMDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 522);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainMenuMDI";
            this.Text = "主界面";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainMenuMDI_FormClosed);
            this.Load += new System.EventHandler(this.MainMenuMDI_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem ruleMenu;
        private System.Windows.Forms.ToolStripMenuItem modelMenu;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem ctToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reprintCTtoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem codeRuleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ruleQueryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modelParamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modelUploadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userManagement;
        private System.Windows.Forms.ToolStripMenuItem roleCreateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem roleAssignToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem singFieldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modelParamUnionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modelBoundParamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modelBoundCusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deptCreateMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prodLineMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cartonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem singCartonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PalletToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem capacityCreateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem capacityBoundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userCreateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modelUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reprintCartonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ruleBoundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem maintainMatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 角色权限分配ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 栈板标签补印ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 品质首件质检标签ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工单首件检验ToolStripMenuItem;
    }
}



