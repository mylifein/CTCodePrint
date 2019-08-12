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
            this.MenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GenerateCTToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ReprintCTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CTConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CusConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CodeRuleConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CodeRuleQueryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CodeRuleCreateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PrintModelConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PrintFieldConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModelBoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModelInfoMaintainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModelUploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.客戶機種配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.機種信息維護ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.機種信息查詢ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.客戶機種關係維護ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AuthorityConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuCreateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.創建角色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.分配菜單ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.為用戶分配角色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(645, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MenuToolStripMenuItem
            // 
            this.MenuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GenerateCTToolStripMenuItem1,
            this.ReprintCTToolStripMenuItem,
            this.CTConfigToolStripMenuItem,
            this.AuthorityConfigToolStripMenuItem});
            this.MenuToolStripMenuItem.Name = "MenuToolStripMenuItem";
            this.MenuToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.MenuToolStripMenuItem.Text = "菜单";
            // 
            // GenerateCTToolStripMenuItem1
            // 
            this.GenerateCTToolStripMenuItem1.Name = "GenerateCTToolStripMenuItem1";
            this.GenerateCTToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.GenerateCTToolStripMenuItem1.Text = "产生CT码";
            this.GenerateCTToolStripMenuItem1.Click += new System.EventHandler(this.产生CT码ToolStripMenuItem1_Click);
            // 
            // ReprintCTToolStripMenuItem
            // 
            this.ReprintCTToolStripMenuItem.Name = "ReprintCTToolStripMenuItem";
            this.ReprintCTToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ReprintCTToolStripMenuItem.Text = "重印CT";
            this.ReprintCTToolStripMenuItem.Click += new System.EventHandler(this.重印CTToolStripMenuItem_Click);
            // 
            // CTConfigToolStripMenuItem
            // 
            this.CTConfigToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CusConfigToolStripMenuItem,
            this.CodeRuleConfigToolStripMenuItem,
            this.PrintModelConfigToolStripMenuItem,
            this.客戶機種配置ToolStripMenuItem});
            this.CTConfigToolStripMenuItem.Name = "CTConfigToolStripMenuItem";
            this.CTConfigToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.CTConfigToolStripMenuItem.Text = "CT配置";
            // 
            // CusConfigToolStripMenuItem
            // 
            this.CusConfigToolStripMenuItem.Name = "CusConfigToolStripMenuItem";
            this.CusConfigToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.CusConfigToolStripMenuItem.Text = "客戶配置";
            this.CusConfigToolStripMenuItem.Click += new System.EventHandler(this.客戶配置ToolStripMenuItem_Click);
            // 
            // CodeRuleConfigToolStripMenuItem
            // 
            this.CodeRuleConfigToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CodeRuleQueryToolStripMenuItem,
            this.CodeRuleCreateToolStripMenuItem});
            this.CodeRuleConfigToolStripMenuItem.Name = "CodeRuleConfigToolStripMenuItem";
            this.CodeRuleConfigToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.CodeRuleConfigToolStripMenuItem.Text = "編碼規則配置";
            // 
            // CodeRuleQueryToolStripMenuItem
            // 
            this.CodeRuleQueryToolStripMenuItem.Name = "CodeRuleQueryToolStripMenuItem";
            this.CodeRuleQueryToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.CodeRuleQueryToolStripMenuItem.Text = "編碼規則查詢";
            this.CodeRuleQueryToolStripMenuItem.Click += new System.EventHandler(this.編碼規則查詢ToolStripMenuItem_Click);
            // 
            // CodeRuleCreateToolStripMenuItem
            // 
            this.CodeRuleCreateToolStripMenuItem.Name = "CodeRuleCreateToolStripMenuItem";
            this.CodeRuleCreateToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.CodeRuleCreateToolStripMenuItem.Text = "編碼規則創建";
            this.CodeRuleCreateToolStripMenuItem.Click += new System.EventHandler(this.編碼規則創建ToolStripMenuItem_Click);
            // 
            // PrintModelConfigToolStripMenuItem
            // 
            this.PrintModelConfigToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PrintFieldConfigToolStripMenuItem,
            this.ModelBoundToolStripMenuItem,
            this.ModelInfoMaintainToolStripMenuItem,
            this.ModelUploadToolStripMenuItem});
            this.PrintModelConfigToolStripMenuItem.Name = "PrintModelConfigToolStripMenuItem";
            this.PrintModelConfigToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.PrintModelConfigToolStripMenuItem.Text = "打印模板配置";
            // 
            // PrintFieldConfigToolStripMenuItem
            // 
            this.PrintFieldConfigToolStripMenuItem.Name = "PrintFieldConfigToolStripMenuItem";
            this.PrintFieldConfigToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.PrintFieldConfigToolStripMenuItem.Text = "模板字段配置";
            this.PrintFieldConfigToolStripMenuItem.Click += new System.EventHandler(this.模板字段配置ToolStripMenuItem_Click);
            // 
            // ModelBoundToolStripMenuItem
            // 
            this.ModelBoundToolStripMenuItem.Name = "ModelBoundToolStripMenuItem";
            this.ModelBoundToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ModelBoundToolStripMenuItem.Text = "模板綁定";
            this.ModelBoundToolStripMenuItem.Click += new System.EventHandler(this.模板綁定ToolStripMenuItem_Click);
            // 
            // ModelInfoMaintainToolStripMenuItem
            // 
            this.ModelInfoMaintainToolStripMenuItem.Name = "ModelInfoMaintainToolStripMenuItem";
            this.ModelInfoMaintainToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ModelInfoMaintainToolStripMenuItem.Text = "模板信息維護";
            this.ModelInfoMaintainToolStripMenuItem.Click += new System.EventHandler(this.模板信息維護ToolStripMenuItem_Click);
            // 
            // ModelUploadToolStripMenuItem
            // 
            this.ModelUploadToolStripMenuItem.Name = "ModelUploadToolStripMenuItem";
            this.ModelUploadToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ModelUploadToolStripMenuItem.Text = "模板上傳";
            this.ModelUploadToolStripMenuItem.Click += new System.EventHandler(this.模板上傳ToolStripMenuItem_Click);
            // 
            // 客戶機種配置ToolStripMenuItem
            // 
            this.客戶機種配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.機種信息維護ToolStripMenuItem,
            this.機種信息查詢ToolStripMenuItem,
            this.客戶機種關係維護ToolStripMenuItem});
            this.客戶機種配置ToolStripMenuItem.Name = "客戶機種配置ToolStripMenuItem";
            this.客戶機種配置ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.客戶機種配置ToolStripMenuItem.Text = "客戶機種配置";
            // 
            // 機種信息維護ToolStripMenuItem
            // 
            this.機種信息維護ToolStripMenuItem.Name = "機種信息維護ToolStripMenuItem";
            this.機種信息維護ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.機種信息維護ToolStripMenuItem.Text = "機種信息維護";
            this.機種信息維護ToolStripMenuItem.Click += new System.EventHandler(this.機種信息維護ToolStripMenuItem_Click);
            // 
            // 機種信息查詢ToolStripMenuItem
            // 
            this.機種信息查詢ToolStripMenuItem.Name = "機種信息查詢ToolStripMenuItem";
            this.機種信息查詢ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.機種信息查詢ToolStripMenuItem.Text = "機種信息查詢";
            // 
            // 客戶機種關係維護ToolStripMenuItem
            // 
            this.客戶機種關係維護ToolStripMenuItem.Name = "客戶機種關係維護ToolStripMenuItem";
            this.客戶機種關係維護ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.客戶機種關係維護ToolStripMenuItem.Text = "客戶機種關係維護";
            this.客戶機種關係維護ToolStripMenuItem.Click += new System.EventHandler(this.客戶機種關係維護ToolStripMenuItem_Click);
            // 
            // AuthorityConfigToolStripMenuItem
            // 
            this.AuthorityConfigToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuCreateToolStripMenuItem,
            this.創建角色ToolStripMenuItem,
            this.分配菜單ToolStripMenuItem,
            this.為用戶分配角色ToolStripMenuItem});
            this.AuthorityConfigToolStripMenuItem.Name = "AuthorityConfigToolStripMenuItem";
            this.AuthorityConfigToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.AuthorityConfigToolStripMenuItem.Text = "权限配置";
            // 
            // MenuCreateToolStripMenuItem
            // 
            this.MenuCreateToolStripMenuItem.Name = "MenuCreateToolStripMenuItem";
            this.MenuCreateToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.MenuCreateToolStripMenuItem.Text = "菜单创建";
            this.MenuCreateToolStripMenuItem.Click += new System.EventHandler(this.MenuCreateToolStripMenuItem_Click);
            // 
            // 創建角色ToolStripMenuItem
            // 
            this.創建角色ToolStripMenuItem.Name = "創建角色ToolStripMenuItem";
            this.創建角色ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.創建角色ToolStripMenuItem.Text = "創建角色";
            this.創建角色ToolStripMenuItem.Click += new System.EventHandler(this.創建角色ToolStripMenuItem_Click);
            // 
            // 分配菜單ToolStripMenuItem
            // 
            this.分配菜單ToolStripMenuItem.Name = "分配菜單ToolStripMenuItem";
            this.分配菜單ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.分配菜單ToolStripMenuItem.Text = "為角色分配菜單";
            this.分配菜單ToolStripMenuItem.Click += new System.EventHandler(this.分配菜單ToolStripMenuItem_Click);
            // 
            // 為用戶分配角色ToolStripMenuItem
            // 
            this.為用戶分配角色ToolStripMenuItem.Name = "為用戶分配角色ToolStripMenuItem";
            this.為用戶分配角色ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.為用戶分配角色ToolStripMenuItem.Text = "為用戶分配角色";
            this.為用戶分配角色ToolStripMenuItem.Click += new System.EventHandler(this.為用戶分配角色ToolStripMenuItem_Click);
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
        private System.Windows.Forms.ToolStripMenuItem MenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GenerateCTToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ReprintCTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CTConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CusConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CodeRuleConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CodeRuleQueryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CodeRuleCreateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PrintModelConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PrintFieldConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ModelBoundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ModelInfoMaintainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 客戶機種配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 機種信息維護ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 機種信息查詢ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 客戶機種關係維護ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ModelUploadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AuthorityConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuCreateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 創建角色ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 分配菜單ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 為用戶分配角色ToolStripMenuItem;
    }
}

