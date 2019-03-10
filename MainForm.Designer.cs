namespace MySchemes
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tool = new System.Windows.Forms.ToolStrip();
            this.toolBtnAnd = new System.Windows.Forms.ToolStripButton();
            this.toolBtnOr = new System.Windows.Forms.ToolStripButton();
            this.toolBtnNot = new System.Windows.Forms.ToolStripButton();
            this.treeView = new System.Windows.Forms.TreeView();
            this.menu.SuspendLayout();
            this.tool.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(658, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.saveToolStripMenuItem.Text = "&Save...";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // tool
            // 
            this.tool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnAnd,
            this.toolBtnOr,
            this.toolBtnNot});
            this.tool.Location = new System.Drawing.Point(0, 24);
            this.tool.Name = "tool";
            this.tool.Size = new System.Drawing.Size(658, 25);
            this.tool.TabIndex = 1;
            this.tool.Text = "tool";
            // 
            // toolBtnAnd
            // 
            this.toolBtnAnd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolBtnAnd.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnAnd.Image")));
            this.toolBtnAnd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnAnd.Name = "toolBtnAnd";
            this.toolBtnAnd.Size = new System.Drawing.Size(36, 22);
            this.toolBtnAnd.Text = "AND";
            // 
            // toolBtnOr
            // 
            this.toolBtnOr.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolBtnOr.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnOr.Image")));
            this.toolBtnOr.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnOr.Name = "toolBtnOr";
            this.toolBtnOr.Size = new System.Drawing.Size(27, 22);
            this.toolBtnOr.Text = "OR";
            // 
            // toolBtnNot
            // 
            this.toolBtnNot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolBtnNot.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnNot.Image")));
            this.toolBtnNot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnNot.Name = "toolBtnNot";
            this.toolBtnNot.Size = new System.Drawing.Size(36, 22);
            this.toolBtnNot.Text = "NOT";
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Right;
            this.treeView.Location = new System.Drawing.Point(492, 49);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(166, 300);
            this.treeView.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 349);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.tool);
            this.Controls.Add(this.menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.Name = "MainForm";
            this.Text = "Scheme Designer";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.tool.ResumeLayout(false);
            this.tool.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStrip tool;
        private System.Windows.Forms.ToolStripButton toolBtnAnd;
        private System.Windows.Forms.ToolStripButton toolBtnOr;
        private System.Windows.Forms.ToolStripButton toolBtnNot;
        private System.Windows.Forms.TreeView treeView;
    }
}

