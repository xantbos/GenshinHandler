namespace GenshinHandler
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.IconMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.MenuMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.LaunchItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.SilentItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.RedditItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WikiItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // IconMain
            // 
            this.IconMain.ContextMenuStrip = this.MenuMain;
            this.IconMain.Icon = ((System.Drawing.Icon)(resources.GetObject("IconMain.Icon")));
            this.IconMain.Text = "Genshin Impact Coddler 9000";
            this.IconMain.Visible = true;
            // 
            // MenuMain
            // 
            this.MenuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LaunchItem,
            this.toolStripMenuItem1,
            this.SilentItem,
            this.CloseItem,
            this.toolStripMenuItem2,
            this.RedditItem,
            this.WikiItem,
            this.toolStripMenuItem3,
            this.ExitItem});
            this.MenuMain.Name = "MenuMain";
            this.MenuMain.Size = new System.Drawing.Size(207, 154);
            // 
            // LaunchItem
            // 
            this.LaunchItem.Name = "LaunchItem";
            this.LaunchItem.Size = new System.Drawing.Size(206, 22);
            this.LaunchItem.Text = "Launch Genshin";
            this.LaunchItem.Click += new System.EventHandler(this.LaunchItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(203, 6);
            // 
            // SilentItem
            // 
            this.SilentItem.Name = "SilentItem";
            this.SilentItem.Size = new System.Drawing.Size(206, 22);
            this.SilentItem.Text = "Silent Mode";
            this.SilentItem.Click += new System.EventHandler(this.SilentItem_Click);
            // 
            // CloseItem
            // 
            this.CloseItem.Name = "CloseItem";
            this.CloseItem.Size = new System.Drawing.Size(206, 22);
            this.CloseItem.Text = "Close Launcher w/ Game";
            this.CloseItem.Click += new System.EventHandler(this.CloseItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(203, 6);
            // 
            // RedditItem
            // 
            this.RedditItem.Name = "RedditItem";
            this.RedditItem.Size = new System.Drawing.Size(206, 22);
            this.RedditItem.Text = "Reddit";
            this.RedditItem.Click += new System.EventHandler(this.RedditItem_Click);
            // 
            // WikiItem
            // 
            this.WikiItem.Name = "WikiItem";
            this.WikiItem.Size = new System.Drawing.Size(206, 22);
            this.WikiItem.Text = "Wiki";
            this.WikiItem.Click += new System.EventHandler(this.WikiItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(203, 6);
            // 
            // ExitItem
            // 
            this.ExitItem.Name = "ExitItem";
            this.ExitItem.Size = new System.Drawing.Size(206, 22);
            this.ExitItem.Text = "Exit";
            this.ExitItem.Click += new System.EventHandler(this.ExitItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GenshinHandler";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.MenuMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon IconMain;
        private System.Windows.Forms.ContextMenuStrip MenuMain;
        private System.Windows.Forms.ToolStripMenuItem LaunchItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem SilentItem;
        private System.Windows.Forms.ToolStripMenuItem CloseItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem RedditItem;
        private System.Windows.Forms.ToolStripMenuItem WikiItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem ExitItem;
    }
}

