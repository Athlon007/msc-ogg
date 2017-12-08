namespace OggConverter
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtboxPath = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.log = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLOGFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLastConversionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.gitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.steamCommunityDiscussionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.remMP3 = new System.Windows.Forms.ToolStripMenuItem();
            this.actionAfterConversionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.launchTheGameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.closeTheProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.launchGameWithoutSteamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CheckBoxUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.launchTheGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtboxPath
            // 
            this.txtboxPath.Location = new System.Drawing.Point(12, 126);
            this.txtboxPath.Name = "txtboxPath";
            this.txtboxPath.ReadOnly = true;
            this.txtboxPath.Size = new System.Drawing.Size(282, 20);
            this.txtboxPath.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(167, 152);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Convert";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // log
            // 
            this.log.Location = new System.Drawing.Point(12, 181);
            this.log.Multiline = true;
            this.log.Name = "log";
            this.log.ReadOnly = true;
            this.log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.log.Size = new System.Drawing.Size(312, 180);
            this.log.TabIndex = 2;
            this.log.TextChanged += new System.EventHandler(this.log_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "My Summer Car Path:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(300, 125);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(24, 22);
            this.button2.TabIndex = 4;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 152);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(149, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Open game directory";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(293, 52);
            this.label2.TabIndex = 6;
            this.label2.Text = "How to use:\r\n- Select My Summer Car directory\r\n- Put all your music (in MP3 forma" +
    "t) into CD and Radio folders\r\n- Click Convert button";
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.launchTheGameToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menu.Size = new System.Drawing.Size(336, 24);
            this.menu.TabIndex = 7;
            this.menu.Text = "menu";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openLOGFolderToolStripMenuItem,
            this.openLastConversionToolStripMenuItem,
            this.toolStripSeparator1,
            this.gitToolStripMenuItem,
            this.steamCommunityDiscussionToolStripMenuItem,
            this.toolStripSeparator3,
            this.infoToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.aboutToolStripMenuItem.Text = "Tool";
            // 
            // openLOGFolderToolStripMenuItem
            // 
            this.openLOGFolderToolStripMenuItem.Name = "openLOGFolderToolStripMenuItem";
            this.openLOGFolderToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.openLOGFolderToolStripMenuItem.Text = "Open LOG folder";
            this.openLOGFolderToolStripMenuItem.Click += new System.EventHandler(this.openLOGFolderToolStripMenuItem_Click);
            // 
            // openLastConversionToolStripMenuItem
            // 
            this.openLastConversionToolStripMenuItem.Name = "openLastConversionToolStripMenuItem";
            this.openLastConversionToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.openLastConversionToolStripMenuItem.Text = "Open last conversion log";
            this.openLastConversionToolStripMenuItem.Click += new System.EventHandler(this.openLastConversionToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(229, 6);
            // 
            // gitToolStripMenuItem
            // 
            this.gitToolStripMenuItem.Name = "gitToolStripMenuItem";
            this.gitToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.gitToolStripMenuItem.Text = "Project\'s repository on GitLab";
            this.gitToolStripMenuItem.Click += new System.EventHandler(this.gitToolStripMenuItem_Click);
            // 
            // steamCommunityDiscussionToolStripMenuItem
            // 
            this.steamCommunityDiscussionToolStripMenuItem.Name = "steamCommunityDiscussionToolStripMenuItem";
            this.steamCommunityDiscussionToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.steamCommunityDiscussionToolStripMenuItem.Text = "Steam Community discussion";
            this.steamCommunityDiscussionToolStripMenuItem.Click += new System.EventHandler(this.steamCommunityDiscussionToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(229, 6);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.infoToolStripMenuItem.Text = "About";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.remMP3,
            this.actionAfterConversionToolStripMenuItem,
            this.launchGameWithoutSteamToolStripMenuItem,
            this.CheckBoxUpdates});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // remMP3
            // 
            this.remMP3.CheckOnClick = true;
            this.remMP3.Name = "remMP3";
            this.remMP3.Size = new System.Drawing.Size(256, 22);
            this.remMP3.Text = "Remove MP3 files after conversion";
            this.remMP3.Click += new System.EventHandler(this.removeOldMP3FilesToolStripMenuItem_Click);
            // 
            // actionAfterConversionToolStripMenuItem
            // 
            this.actionAfterConversionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.launchTheGameToolStripMenuItem1,
            this.closeTheProgramToolStripMenuItem,
            this.toolStripSeparator2,
            this.noneToolStripMenuItem});
            this.actionAfterConversionToolStripMenuItem.Name = "actionAfterConversionToolStripMenuItem";
            this.actionAfterConversionToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.actionAfterConversionToolStripMenuItem.Text = "Action after conversion";
            // 
            // launchTheGameToolStripMenuItem1
            // 
            this.launchTheGameToolStripMenuItem1.CheckOnClick = true;
            this.launchTheGameToolStripMenuItem1.Name = "launchTheGameToolStripMenuItem1";
            this.launchTheGameToolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
            this.launchTheGameToolStripMenuItem1.Text = "Launch the game";
            this.launchTheGameToolStripMenuItem1.Click += new System.EventHandler(this.launchTheGameToolStripMenuItem1_Click);
            // 
            // closeTheProgramToolStripMenuItem
            // 
            this.closeTheProgramToolStripMenuItem.CheckOnClick = true;
            this.closeTheProgramToolStripMenuItem.Name = "closeTheProgramToolStripMenuItem";
            this.closeTheProgramToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.closeTheProgramToolStripMenuItem.Text = "Close the program";
            this.closeTheProgramToolStripMenuItem.Click += new System.EventHandler(this.closeTheProgramToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(169, 6);
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.CheckOnClick = true;
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.noneToolStripMenuItem.Text = "None";
            this.noneToolStripMenuItem.Click += new System.EventHandler(this.noneToolStripMenuItem_Click);
            // 
            // launchGameWithoutSteamToolStripMenuItem
            // 
            this.launchGameWithoutSteamToolStripMenuItem.CheckOnClick = true;
            this.launchGameWithoutSteamToolStripMenuItem.Name = "launchGameWithoutSteamToolStripMenuItem";
            this.launchGameWithoutSteamToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.launchGameWithoutSteamToolStripMenuItem.Text = "Launch game without Steam";
            this.launchGameWithoutSteamToolStripMenuItem.Click += new System.EventHandler(this.launchGameWithoutSteamToolStripMenuItem_Click);
            // 
            // CheckBoxUpdates
            // 
            this.CheckBoxUpdates.CheckOnClick = true;
            this.CheckBoxUpdates.Name = "CheckBoxUpdates";
            this.CheckBoxUpdates.Size = new System.Drawing.Size(256, 22);
            this.CheckBoxUpdates.Text = "Check for updates";
            this.CheckBoxUpdates.Click += new System.EventHandler(this.CheckBoxUpdates_Click);
            // 
            // launchTheGameToolStripMenuItem
            // 
            this.launchTheGameToolStripMenuItem.Name = "launchTheGameToolStripMenuItem";
            this.launchTheGameToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
            this.launchTheGameToolStripMenuItem.Text = "Launch game";
            this.launchTheGameToolStripMenuItem.Click += new System.EventHandler(this.launchTheGameToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 373);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.log);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtboxPath);
            this.Controls.Add(this.menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MSC OGG Converter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtboxPath;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox log;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem remMP3;
        private System.Windows.Forms.ToolStripMenuItem openLOGFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLastConversionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem launchTheGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem steamCommunityDiscussionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionAfterConversionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem launchTheGameToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem closeTheProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem launchGameWithoutSteamToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem CheckBoxUpdates;
    }
}

