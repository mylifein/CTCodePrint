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
            this.ruleMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.codeRuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ruleQueryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编码规则绑定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.子阶料号维护ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.modelParamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SingFieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModelParamUnionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.模板字段規則綁定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.模板文件綁定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.創建容量ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.容量綁定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.模板上傳ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.模板修改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UserManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.創建角色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.用戶角色分配ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.部門創建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.線別設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.創建用戶ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AuthorizationConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.創建權限菜單ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.菜單權限分配ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
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
            this.UserManagement,
            this.AuthorizationConfig});
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
            this.PalletToolStripMenuItem});
            this.fileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(69, 24);
            this.fileMenu.Text = "菜單(&F)";
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
            // ruleMenu
            // 
            this.ruleMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.codeRuleToolStripMenuItem,
            this.ruleQueryToolStripMenuItem,
            this.编码规则绑定ToolStripMenuItem,
            this.子阶料号维护ToolStripMenuItem});
            this.ruleMenu.Name = "ruleMenu";
            this.ruleMenu.Size = new System.Drawing.Size(99, 24);
            this.ruleMenu.Text = "編碼配置(&E)";
            // 
            // codeRuleToolStripMenuItem
            // 
            this.codeRuleToolStripMenuItem.Name = "codeRuleToolStripMenuItem";
            this.codeRuleToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.codeRuleToolStripMenuItem.Text = "編碼配置";
            this.codeRuleToolStripMenuItem.Click += new System.EventHandler(this.編碼配置ToolStripMenuItem_Click);
            // 
            // ruleQueryToolStripMenuItem
            // 
            this.ruleQueryToolStripMenuItem.Name = "ruleQueryToolStripMenuItem";
            this.ruleQueryToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.ruleQueryToolStripMenuItem.Text = "編碼查詢";
            this.ruleQueryToolStripMenuItem.Click += new System.EventHandler(this.編碼查詢ToolStripMenuItem_Click);
            // 
            // 编码规则绑定ToolStripMenuItem
            // 
            this.编码规则绑定ToolStripMenuItem.Name = "编码规则绑定ToolStripMenuItem";
            this.编码规则绑定ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.编码规则绑定ToolStripMenuItem.Text = "编码规则绑定";
            this.编码规则绑定ToolStripMenuItem.Click += new System.EventHandler(this.BoundRuleToolStripMenuItem);
            // 
            // 子阶料号维护ToolStripMenuItem
            // 
            this.子阶料号维护ToolStripMenuItem.Name = "子阶料号维护ToolStripMenuItem";
            this.子阶料号维护ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.子阶料号维护ToolStripMenuItem.Text = "子阶料号维护";
            this.子阶料号维护ToolStripMenuItem.Click += new System.EventHandler(this.子阶料号维护ToolStripMenuItem_Click);
            // 
            // modelMenu
            // 
            this.modelMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modelParamToolStripMenuItem,
            this.SingFieldToolStripMenuItem,
            this.ModelParamUnionToolStripMenuItem,
            this.模板字段規則綁定ToolStripMenuItem,
            this.模板文件綁定ToolStripMenuItem,
            this.創建容量ToolStripMenuItem,
            this.容量綁定ToolStripMenuItem,
            this.模板上傳ToolStripMenuItem,
            this.模板修改ToolStripMenuItem});
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
            // SingFieldToolStripMenuItem
            // 
            this.SingFieldToolStripMenuItem.Name = "SingFieldToolStripMenuItem";
            this.SingFieldToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.SingFieldToolStripMenuItem.Text = "字段類型配置";
            this.SingFieldToolStripMenuItem.Click += new System.EventHandler(this.字段類型配置ToolStripMenuItem_Click);
            // 
            // ModelParamUnionToolStripMenuItem
            // 
            this.ModelParamUnionToolStripMenuItem.Name = "ModelParamUnionToolStripMenuItem";
            this.ModelParamUnionToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.ModelParamUnionToolStripMenuItem.Text = "标签参数与字段绑定";
            this.ModelParamUnionToolStripMenuItem.Click += new System.EventHandler(this.字段規則配置ToolStripMenuItem_Click);
            // 
            // 模板字段規則綁定ToolStripMenuItem
            // 
            this.模板字段規則綁定ToolStripMenuItem.Name = "模板字段規則綁定ToolStripMenuItem";
            this.模板字段規則綁定ToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.模板字段規則綁定ToolStripMenuItem.Text = "标签与参数綁定";
            this.模板字段規則綁定ToolStripMenuItem.Click += new System.EventHandler(this.模板字段規則綁定ToolStripMenuItem_Click);
            // 
            // 模板文件綁定ToolStripMenuItem
            // 
            this.模板文件綁定ToolStripMenuItem.Name = "模板文件綁定ToolStripMenuItem";
            this.模板文件綁定ToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.模板文件綁定ToolStripMenuItem.Text = "标签模板文件綁定";
            this.模板文件綁定ToolStripMenuItem.Click += new System.EventHandler(this.模板文件綁定ToolStripMenuItem_Click);
            // 
            // 創建容量ToolStripMenuItem
            // 
            this.創建容量ToolStripMenuItem.Name = "創建容量ToolStripMenuItem";
            this.創建容量ToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.創建容量ToolStripMenuItem.Text = "創建容量";
            this.創建容量ToolStripMenuItem.Click += new System.EventHandler(this.創建容量ToolStripMenuItem_Click);
            // 
            // 容量綁定ToolStripMenuItem
            // 
            this.容量綁定ToolStripMenuItem.Name = "容量綁定ToolStripMenuItem";
            this.容量綁定ToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.容量綁定ToolStripMenuItem.Text = "容量綁定";
            this.容量綁定ToolStripMenuItem.Click += new System.EventHandler(this.容量綁定ToolStripMenuItem_Click);
            // 
            // 模板上傳ToolStripMenuItem
            // 
            this.模板上傳ToolStripMenuItem.Name = "模板上傳ToolStripMenuItem";
            this.模板上傳ToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.模板上傳ToolStripMenuItem.Text = "标签模板上傳";
            this.模板上傳ToolStripMenuItem.Click += new System.EventHandler(this.模板上傳ToolStripMenuItem_Click);
            // 
            // 模板修改ToolStripMenuItem
            // 
            this.模板修改ToolStripMenuItem.Name = "模板修改ToolStripMenuItem";
            this.模板修改ToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.模板修改ToolStripMenuItem.Text = "标签模板修改";
            this.模板修改ToolStripMenuItem.Click += new System.EventHandler(this.模板修改ToolStripMenuItem_Click);
            // 
            // UserManagement
            // 
            this.UserManagement.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.創建角色ToolStripMenuItem,
            this.用戶角色分配ToolStripMenuItem,
            this.部門創建ToolStripMenuItem,
            this.線別設定ToolStripMenuItem,
            this.創建用戶ToolStripMenuItem});
            this.UserManagement.Name = "UserManagement";
            this.UserManagement.Size = new System.Drawing.Size(102, 24);
            this.UserManagement.Text = "用戶管理(&U)";
            // 
            // 創建角色ToolStripMenuItem
            // 
            this.創建角色ToolStripMenuItem.Name = "創建角色ToolStripMenuItem";
            this.創建角色ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.創建角色ToolStripMenuItem.Text = "創建角色";
            this.創建角色ToolStripMenuItem.Click += new System.EventHandler(this.創建角色ToolStripMenuItem_Click);
            // 
            // 用戶角色分配ToolStripMenuItem
            // 
            this.用戶角色分配ToolStripMenuItem.Name = "用戶角色分配ToolStripMenuItem";
            this.用戶角色分配ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.用戶角色分配ToolStripMenuItem.Text = "用戶角色分配";
            this.用戶角色分配ToolStripMenuItem.Click += new System.EventHandler(this.用戶角色分配ToolStripMenuItem_Click);
            // 
            // 部門創建ToolStripMenuItem
            // 
            this.部門創建ToolStripMenuItem.Name = "部門創建ToolStripMenuItem";
            this.部門創建ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.部門創建ToolStripMenuItem.Text = "部門設定";
            this.部門創建ToolStripMenuItem.Click += new System.EventHandler(this.部門創建ToolStripMenuItem_Click);
            // 
            // 線別設定ToolStripMenuItem
            // 
            this.線別設定ToolStripMenuItem.Name = "線別設定ToolStripMenuItem";
            this.線別設定ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.線別設定ToolStripMenuItem.Text = "線別設定";
            this.線別設定ToolStripMenuItem.Click += new System.EventHandler(this.線別設定ToolStripMenuItem_Click);
            // 
            // 創建用戶ToolStripMenuItem
            // 
            this.創建用戶ToolStripMenuItem.Name = "創建用戶ToolStripMenuItem";
            this.創建用戶ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.創建用戶ToolStripMenuItem.Text = "創建用戶";
            this.創建用戶ToolStripMenuItem.Click += new System.EventHandler(this.創建用戶ToolStripMenuItem_Click);
            // 
            // AuthorizationConfig
            // 
            this.AuthorizationConfig.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.創建權限菜單ToolStripMenuItem,
            this.菜單權限分配ToolStripMenuItem});
            this.AuthorizationConfig.Name = "AuthorizationConfig";
            this.AuthorizationConfig.Size = new System.Drawing.Size(101, 24);
            this.AuthorizationConfig.Text = "權限配置(&K)";
            this.AuthorizationConfig.Click += new System.EventHandler(this.AuthorizationConfig_Click);
            // 
            // 創建權限菜單ToolStripMenuItem
            // 
            this.創建權限菜單ToolStripMenuItem.Name = "創建權限菜單ToolStripMenuItem";
            this.創建權限菜單ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.創建權限菜單ToolStripMenuItem.Text = "創建權限菜單";
            this.創建權限菜單ToolStripMenuItem.Click += new System.EventHandler(this.創建權限菜單ToolStripMenuItem_Click);
            // 
            // 菜單權限分配ToolStripMenuItem
            // 
            this.菜單權限分配ToolStripMenuItem.Name = "菜單權限分配ToolStripMenuItem";
            this.菜單權限分配ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.菜單權限分配ToolStripMenuItem.Text = "菜單權限分配";
            this.菜單權限分配ToolStripMenuItem.Click += new System.EventHandler(this.菜單權限分配ToolStripMenuItem_Click);
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
        private System.Windows.Forms.ToolStripMenuItem 模板上傳ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UserManagement;
        private System.Windows.Forms.ToolStripMenuItem 創建角色ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 用戶角色分配ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AuthorizationConfig;
        private System.Windows.Forms.ToolStripMenuItem 創建權限菜單ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 菜單權限分配ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem SingFieldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ModelParamUnionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 模板字段規則綁定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 模板文件綁定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 部門創建ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 線別設定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cartonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem singCartonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PalletToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 創建容量ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 容量綁定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 創建用戶ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 模板修改ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reprintCartonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编码规则绑定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 子阶料号维护ToolStripMenuItem;
    }
}



