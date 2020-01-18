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
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.編碼配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.編碼查詢ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.模板上傳ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.字段類型配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.字段規則配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.模板字段規則綁定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.模板文件綁定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.機種信息配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.機種信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.機種關係綁定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.料號子階維護ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UserManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.創建角色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.用戶角色分配ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.部門創建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.線別設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.裝箱單號打印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.editMenu,
            this.viewMenu,
            this.toolsMenu,
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
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.裝箱單號打印ToolStripMenuItem});
            this.fileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(69, 24);
            this.fileMenu.Text = "菜單(&F)";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(181, 26);
            this.toolStripMenuItem1.Text = "產生CT碼";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(181, 26);
            this.toolStripMenuItem2.Text = "重印CT碼";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // editMenu
            // 
            this.editMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.編碼配置ToolStripMenuItem,
            this.編碼查詢ToolStripMenuItem});
            this.editMenu.Name = "editMenu";
            this.editMenu.Size = new System.Drawing.Size(103, 24);
            this.editMenu.Text = "CT碼配置(&E)";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(144, 26);
            this.toolStripMenuItem3.Text = "客戶配置";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // 編碼配置ToolStripMenuItem
            // 
            this.編碼配置ToolStripMenuItem.Name = "編碼配置ToolStripMenuItem";
            this.編碼配置ToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.編碼配置ToolStripMenuItem.Text = "編碼配置";
            this.編碼配置ToolStripMenuItem.Click += new System.EventHandler(this.編碼配置ToolStripMenuItem_Click);
            // 
            // 編碼查詢ToolStripMenuItem
            // 
            this.編碼查詢ToolStripMenuItem.Name = "編碼查詢ToolStripMenuItem";
            this.編碼查詢ToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.編碼查詢ToolStripMenuItem.Text = "編碼查詢";
            this.編碼查詢ToolStripMenuItem.Click += new System.EventHandler(this.編碼查詢ToolStripMenuItem_Click);
            // 
            // viewMenu
            // 
            this.viewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.模板上傳ToolStripMenuItem,
            this.字段類型配置ToolStripMenuItem,
            this.字段規則配置ToolStripMenuItem,
            this.模板字段規則綁定ToolStripMenuItem,
            this.模板文件綁定ToolStripMenuItem});
            this.viewMenu.Name = "viewMenu";
            this.viewMenu.Size = new System.Drawing.Size(101, 24);
            this.viewMenu.Text = "模板配置(&V)";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(204, 26);
            this.toolStripMenuItem4.Text = "模板字段配置";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // 模板上傳ToolStripMenuItem
            // 
            this.模板上傳ToolStripMenuItem.Name = "模板上傳ToolStripMenuItem";
            this.模板上傳ToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.模板上傳ToolStripMenuItem.Text = "模板上傳";
            this.模板上傳ToolStripMenuItem.Click += new System.EventHandler(this.模板上傳ToolStripMenuItem_Click);
            // 
            // 字段類型配置ToolStripMenuItem
            // 
            this.字段類型配置ToolStripMenuItem.Name = "字段類型配置ToolStripMenuItem";
            this.字段類型配置ToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.字段類型配置ToolStripMenuItem.Text = "字段類型配置";
            this.字段類型配置ToolStripMenuItem.Click += new System.EventHandler(this.字段類型配置ToolStripMenuItem_Click);
            // 
            // 字段規則配置ToolStripMenuItem
            // 
            this.字段規則配置ToolStripMenuItem.Name = "字段規則配置ToolStripMenuItem";
            this.字段規則配置ToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.字段規則配置ToolStripMenuItem.Text = "字段規則配置";
            this.字段規則配置ToolStripMenuItem.Click += new System.EventHandler(this.字段規則配置ToolStripMenuItem_Click);
            // 
            // 模板字段規則綁定ToolStripMenuItem
            // 
            this.模板字段規則綁定ToolStripMenuItem.Name = "模板字段規則綁定ToolStripMenuItem";
            this.模板字段規則綁定ToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.模板字段規則綁定ToolStripMenuItem.Text = "模板字段規則綁定";
            this.模板字段規則綁定ToolStripMenuItem.Click += new System.EventHandler(this.模板字段規則綁定ToolStripMenuItem_Click);
            // 
            // 模板文件綁定ToolStripMenuItem
            // 
            this.模板文件綁定ToolStripMenuItem.Name = "模板文件綁定ToolStripMenuItem";
            this.模板文件綁定ToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.模板文件綁定ToolStripMenuItem.Text = "模板文件綁定";
            this.模板文件綁定ToolStripMenuItem.Click += new System.EventHandler(this.模板文件綁定ToolStripMenuItem_Click);
            // 
            // toolsMenu
            // 
            this.toolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.機種信息配置ToolStripMenuItem,
            this.機種信息ToolStripMenuItem,
            this.機種關係綁定ToolStripMenuItem,
            this.料號子階維護ToolStripMenuItem});
            this.toolsMenu.Name = "toolsMenu";
            this.toolsMenu.Size = new System.Drawing.Size(100, 24);
            this.toolsMenu.Text = "機種配置(&T)";
            // 
            // 機種信息配置ToolStripMenuItem
            // 
            this.機種信息配置ToolStripMenuItem.Name = "機種信息配置ToolStripMenuItem";
            this.機種信息配置ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.機種信息配置ToolStripMenuItem.Text = "機種信息維護";
            this.機種信息配置ToolStripMenuItem.Click += new System.EventHandler(this.機種信息配置ToolStripMenuItem_Click);
            // 
            // 機種信息ToolStripMenuItem
            // 
            this.機種信息ToolStripMenuItem.Name = "機種信息ToolStripMenuItem";
            this.機種信息ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.機種信息ToolStripMenuItem.Text = "機種信息查詢";
            this.機種信息ToolStripMenuItem.Click += new System.EventHandler(this.機種信息ToolStripMenuItem_Click);
            // 
            // 機種關係綁定ToolStripMenuItem
            // 
            this.機種關係綁定ToolStripMenuItem.Name = "機種關係綁定ToolStripMenuItem";
            this.機種關係綁定ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.機種關係綁定ToolStripMenuItem.Text = "機種關係維護";
            this.機種關係綁定ToolStripMenuItem.Click += new System.EventHandler(this.機種關係綁定ToolStripMenuItem_Click);
            // 
            // 料號子階維護ToolStripMenuItem
            // 
            this.料號子階維護ToolStripMenuItem.Name = "料號子階維護ToolStripMenuItem";
            this.料號子階維護ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.料號子階維護ToolStripMenuItem.Text = "料號子階維護";
            this.料號子階維護ToolStripMenuItem.Click += new System.EventHandler(this.料號子階維護ToolStripMenuItem_Click);
            // 
            // UserManagement
            // 
            this.UserManagement.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.創建角色ToolStripMenuItem,
            this.用戶角色分配ToolStripMenuItem,
            this.部門創建ToolStripMenuItem,
            this.線別設定ToolStripMenuItem});
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
            // 裝箱單號打印ToolStripMenuItem
            // 
            this.裝箱單號打印ToolStripMenuItem.Name = "裝箱單號打印ToolStripMenuItem";
            this.裝箱單號打印ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.裝箱單號打印ToolStripMenuItem.Text = "裝箱單號打印";
            this.裝箱單號打印ToolStripMenuItem.Click += new System.EventHandler(this.裝箱單號打印ToolStripMenuItem_Click);
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
        private System.Windows.Forms.ToolStripMenuItem editMenu;
        private System.Windows.Forms.ToolStripMenuItem viewMenu;
        private System.Windows.Forms.ToolStripMenuItem toolsMenu;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem 編碼配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 編碼查詢ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem 模板上傳ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 機種信息配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 機種信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 機種關係綁定ToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem 字段類型配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 字段規則配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 模板字段規則綁定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 模板文件綁定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 料號子階維護ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 部門創建ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 線別設定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 裝箱單號打印ToolStripMenuItem;
    }
}



