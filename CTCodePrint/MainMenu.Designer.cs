namespace CTCodePrint
{
    partial class MainMenu
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.产生CT码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.产生CT码ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.打印CTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重印CTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cT配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.客戶配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.編碼規則配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.編碼規則查詢ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.編碼規則創建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.产生CT码ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(645, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 产生CT码ToolStripMenuItem
            // 
            this.产生CT码ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.产生CT码ToolStripMenuItem1,
            this.打印CTToolStripMenuItem,
            this.重印CTToolStripMenuItem,
            this.cT配置ToolStripMenuItem});
            this.产生CT码ToolStripMenuItem.Name = "产生CT码ToolStripMenuItem";
            this.产生CT码ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.产生CT码ToolStripMenuItem.Text = "菜单";
            // 
            // 产生CT码ToolStripMenuItem1
            // 
            this.产生CT码ToolStripMenuItem1.Name = "产生CT码ToolStripMenuItem1";
            this.产生CT码ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.产生CT码ToolStripMenuItem1.Text = "产生CT码";
            this.产生CT码ToolStripMenuItem1.Click += new System.EventHandler(this.产生CT码ToolStripMenuItem1_Click);
            // 
            // 打印CTToolStripMenuItem
            // 
            this.打印CTToolStripMenuItem.Name = "打印CTToolStripMenuItem";
            this.打印CTToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.打印CTToolStripMenuItem.Text = "打印CT";
            // 
            // 重印CTToolStripMenuItem
            // 
            this.重印CTToolStripMenuItem.Name = "重印CTToolStripMenuItem";
            this.重印CTToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.重印CTToolStripMenuItem.Text = "重印CT";
            this.重印CTToolStripMenuItem.Click += new System.EventHandler(this.重印CTToolStripMenuItem_Click);
            // 
            // cT配置ToolStripMenuItem
            // 
            this.cT配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.客戶配置ToolStripMenuItem,
            this.編碼規則配置ToolStripMenuItem});
            this.cT配置ToolStripMenuItem.Name = "cT配置ToolStripMenuItem";
            this.cT配置ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cT配置ToolStripMenuItem.Text = "CT配置";
            // 
            // 客戶配置ToolStripMenuItem
            // 
            this.客戶配置ToolStripMenuItem.Name = "客戶配置ToolStripMenuItem";
            this.客戶配置ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.客戶配置ToolStripMenuItem.Text = "客戶配置";
            this.客戶配置ToolStripMenuItem.Click += new System.EventHandler(this.客戶配置ToolStripMenuItem_Click);
            // 
            // 編碼規則配置ToolStripMenuItem
            // 
            this.編碼規則配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.編碼規則查詢ToolStripMenuItem,
            this.編碼規則創建ToolStripMenuItem});
            this.編碼規則配置ToolStripMenuItem.Name = "編碼規則配置ToolStripMenuItem";
            this.編碼規則配置ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.編碼規則配置ToolStripMenuItem.Text = "編碼規則配置";
            this.編碼規則配置ToolStripMenuItem.Click += new System.EventHandler(this.編碼規則配置ToolStripMenuItem_Click);
            // 
            // 編碼規則查詢ToolStripMenuItem
            // 
            this.編碼規則查詢ToolStripMenuItem.Name = "編碼規則查詢ToolStripMenuItem";
            this.編碼規則查詢ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.編碼規則查詢ToolStripMenuItem.Text = "編碼規則查詢";
            // 
            // 編碼規則創建ToolStripMenuItem
            // 
            this.編碼規則創建ToolStripMenuItem.Name = "編碼規則創建ToolStripMenuItem";
            this.編碼規則創建ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.編碼規則創建ToolStripMenuItem.Text = "編碼規則創建";
            this.編碼規則創建ToolStripMenuItem.Click += new System.EventHandler(this.編碼規則創建ToolStripMenuItem_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 261);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainMenu";
            this.Text = "ChenBroCT";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 产生CT码ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 产生CT码ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 打印CTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重印CTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cT配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 客戶配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 編碼規則配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 編碼規則查詢ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 編碼規則創建ToolStripMenuItem;
    }
}

