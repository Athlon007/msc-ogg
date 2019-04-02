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
            this.btnConvert = new System.Windows.Forms.Button();
            this.logOutput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDirectory = new System.Windows.Forms.Button();
            this.btnOpenGameDir = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.menuTool = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLogFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLastLog = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGitLab = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSteam = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mSCOGGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFFmpegLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.btnQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRemMP3 = new System.Windows.Forms.ToolStripMenuItem();
            this.actionAfterConversionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAfterLaunchGame = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAfterClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAfterNone = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNoSteam = new System.Windows.Forms.ToolStripMenuItem();
            this.btnUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLaunchGame = new System.Windows.Forms.ToolStripMenuItem();
            this.songList = new System.Windows.Forms.ListBox();
            this.btnPlaySong = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.playerRadio = new System.Windows.Forms.RadioButton();
            this.playerCD = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnSort = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.menu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtboxPath
            // 
            this.txtboxPath.Location = new System.Drawing.Point(286, 44);
            this.txtboxPath.Name = "txtboxPath";
            this.txtboxPath.ReadOnly = true;
            this.txtboxPath.Size = new System.Drawing.Size(322, 20);
            this.txtboxPath.TabIndex = 0;
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(466, 70);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(172, 24);
            this.btnConvert.TabIndex = 1;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // logOutput
            // 
            this.logOutput.Location = new System.Drawing.Point(172, 100);
            this.logOutput.Multiline = true;
            this.logOutput.Name = "logOutput";
            this.logOutput.ReadOnly = true;
            this.logOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logOutput.Size = new System.Drawing.Size(473, 273);
            this.logOutput.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(283, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "My Summer Car Path:";
            // 
            // btnDirectory
            // 
            this.btnDirectory.Location = new System.Drawing.Point(614, 43);
            this.btnDirectory.Name = "btnDirectory";
            this.btnDirectory.Size = new System.Drawing.Size(24, 22);
            this.btnDirectory.TabIndex = 4;
            this.btnDirectory.Text = "...";
            this.btnDirectory.UseVisualStyleBackColor = true;
            this.btnDirectory.Click += new System.EventHandler(this.btnDirectory_Click);
            // 
            // btnOpenGameDir
            // 
            this.btnOpenGameDir.Location = new System.Drawing.Point(286, 70);
            this.btnOpenGameDir.Name = "btnOpenGameDir";
            this.btnOpenGameDir.Size = new System.Drawing.Size(172, 23);
            this.btnOpenGameDir.TabIndex = 5;
            this.btnOpenGameDir.Text = "Open game directory";
            this.btnOpenGameDir.UseVisualStyleBackColor = true;
            this.btnOpenGameDir.Click += new System.EventHandler(this.btnOpenGameDir_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(238, 65);
            this.label2.TabIndex = 6;
            this.label2.Text = "How to use:\r\n- Select My Summer Car directory\r\n- Put all your music into CD and R" +
    "adio folders\r\n- Click Convert button\r\nSupported formats: .mp3, .wav, .aac, .m4a," +
    " .wma";
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTool,
            this.menuSettings,
            this.btnLaunchGame});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menu.Size = new System.Drawing.Size(644, 24);
            this.menu.TabIndex = 7;
            this.menu.Text = "menu";
            // 
            // menuTool
            // 
            this.menuTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLogFolder,
            this.btnLastLog,
            this.toolStripSeparator1,
            this.btnGitLab,
            this.btnSteam,
            this.toolStripSeparator3,
            this.btnAbout,
            this.btnQuit});
            this.menuTool.Name = "menuTool";
            this.menuTool.Size = new System.Drawing.Size(42, 20);
            this.menuTool.Text = "Tool";
            // 
            // btnLogFolder
            // 
            this.btnLogFolder.Name = "btnLogFolder";
            this.btnLogFolder.Size = new System.Drawing.Size(232, 22);
            this.btnLogFolder.Text = "Open LOG folder";
            this.btnLogFolder.Click += new System.EventHandler(this.BtnLogFolder_Click);
            // 
            // btnLastLog
            // 
            this.btnLastLog.Name = "btnLastLog";
            this.btnLastLog.Size = new System.Drawing.Size(232, 22);
            this.btnLastLog.Text = "Open last conversion log";
            this.btnLastLog.Click += new System.EventHandler(this.openLastConversionToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(229, 6);
            // 
            // btnGitLab
            // 
            this.btnGitLab.Name = "btnGitLab";
            this.btnGitLab.Size = new System.Drawing.Size(232, 22);
            this.btnGitLab.Text = "GitLab repository";
            this.btnGitLab.Click += new System.EventHandler(this.gitToolStripMenuItem_Click);
            // 
            // btnSteam
            // 
            this.btnSteam.Name = "btnSteam";
            this.btnSteam.Size = new System.Drawing.Size(232, 22);
            this.btnSteam.Text = "Steam Community discussion";
            this.btnSteam.Click += new System.EventHandler(this.steamCommunityDiscussionToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(229, 6);
            // 
            // btnAbout
            // 
            this.btnAbout.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mSCOGGToolStripMenuItem,
            this.btnFFmpegLicense});
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(232, 22);
            this.btnAbout.Text = "About";
            // 
            // mSCOGGToolStripMenuItem
            // 
            this.mSCOGGToolStripMenuItem.Name = "mSCOGGToolStripMenuItem";
            this.mSCOGGToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.mSCOGGToolStripMenuItem.Text = "MSC OGG";
            this.mSCOGGToolStripMenuItem.Click += new System.EventHandler(this.MSCOGGToolStripMenuItem_Click);
            // 
            // btnFFmpegLicense
            // 
            this.btnFFmpegLicense.Name = "btnFFmpegLicense";
            this.btnFFmpegLicense.Size = new System.Drawing.Size(127, 22);
            this.btnFFmpegLicense.Text = "FFmpeg";
            this.btnFFmpegLicense.Click += new System.EventHandler(this.BtnFFmpegLicense_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(232, 22);
            this.btnQuit.Text = "Quit";
            this.btnQuit.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // menuSettings
            // 
            this.menuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRemMP3,
            this.actionAfterConversionToolStripMenuItem,
            this.btnNoSteam,
            this.btnUpdates});
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Size = new System.Drawing.Size(61, 20);
            this.menuSettings.Text = "Settings";
            // 
            // btnRemMP3
            // 
            this.btnRemMP3.CheckOnClick = true;
            this.btnRemMP3.Name = "btnRemMP3";
            this.btnRemMP3.Size = new System.Drawing.Size(229, 22);
            this.btnRemMP3.Text = "Remove files after conversion";
            this.btnRemMP3.Click += new System.EventHandler(this.removeOldMP3FilesToolStripMenuItem_Click);
            // 
            // actionAfterConversionToolStripMenuItem
            // 
            this.actionAfterConversionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAfterLaunchGame,
            this.btnAfterClose,
            this.toolStripSeparator2,
            this.btnAfterNone});
            this.actionAfterConversionToolStripMenuItem.Name = "actionAfterConversionToolStripMenuItem";
            this.actionAfterConversionToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.actionAfterConversionToolStripMenuItem.Text = "Action after conversion";
            // 
            // btnAfterLaunchGame
            // 
            this.btnAfterLaunchGame.CheckOnClick = true;
            this.btnAfterLaunchGame.Name = "btnAfterLaunchGame";
            this.btnAfterLaunchGame.Size = new System.Drawing.Size(172, 22);
            this.btnAfterLaunchGame.Text = "Launch the game";
            this.btnAfterLaunchGame.Click += new System.EventHandler(this.launchTheGameToolStripMenuItem1_Click);
            // 
            // btnAfterClose
            // 
            this.btnAfterClose.CheckOnClick = true;
            this.btnAfterClose.Name = "btnAfterClose";
            this.btnAfterClose.Size = new System.Drawing.Size(172, 22);
            this.btnAfterClose.Text = "Close the program";
            this.btnAfterClose.Click += new System.EventHandler(this.closeTheProgramToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(169, 6);
            // 
            // btnAfterNone
            // 
            this.btnAfterNone.CheckOnClick = true;
            this.btnAfterNone.Name = "btnAfterNone";
            this.btnAfterNone.Size = new System.Drawing.Size(172, 22);
            this.btnAfterNone.Text = "None";
            this.btnAfterNone.Click += new System.EventHandler(this.noneToolStripMenuItem_Click);
            // 
            // btnNoSteam
            // 
            this.btnNoSteam.CheckOnClick = true;
            this.btnNoSteam.Name = "btnNoSteam";
            this.btnNoSteam.Size = new System.Drawing.Size(229, 22);
            this.btnNoSteam.Text = "Launch game without Steam";
            this.btnNoSteam.Click += new System.EventHandler(this.launchGameWithoutSteamToolStripMenuItem_Click);
            // 
            // btnUpdates
            // 
            this.btnUpdates.CheckOnClick = true;
            this.btnUpdates.Name = "btnUpdates";
            this.btnUpdates.Size = new System.Drawing.Size(229, 22);
            this.btnUpdates.Text = "Disable update check";
            this.btnUpdates.Click += new System.EventHandler(this.CheckBoxUpdates_Click);
            // 
            // btnLaunchGame
            // 
            this.btnLaunchGame.Name = "btnLaunchGame";
            this.btnLaunchGame.Size = new System.Drawing.Size(91, 20);
            this.btnLaunchGame.Text = "Launch game";
            this.btnLaunchGame.Click += new System.EventHandler(this.launchTheGameToolStripMenuItem_Click);
            // 
            // songList
            // 
            this.songList.FormattingEnabled = true;
            this.songList.Location = new System.Drawing.Point(10, 23);
            this.songList.Name = "songList";
            this.songList.Size = new System.Drawing.Size(98, 212);
            this.songList.TabIndex = 8;
            // 
            // btnPlaySong
            // 
            this.btnPlaySong.Location = new System.Drawing.Point(12, 241);
            this.btnPlaySong.Name = "btnPlaySong";
            this.btnPlaySong.Size = new System.Drawing.Size(45, 23);
            this.btnPlaySong.TabIndex = 9;
            this.btnPlaySong.Text = "Play";
            this.btnPlaySong.UseVisualStyleBackColor = true;
            this.btnPlaySong.Click += new System.EventHandler(this.btnPlay);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(63, 241);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(45, 23);
            this.btnStop.TabIndex = 10;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // playerRadio
            // 
            this.playerRadio.AutoSize = true;
            this.playerRadio.Location = new System.Drawing.Point(30, 3);
            this.playerRadio.Name = "playerRadio";
            this.playerRadio.Size = new System.Drawing.Size(53, 17);
            this.playerRadio.TabIndex = 11;
            this.playerRadio.TabStop = true;
            this.playerRadio.Text = "Radio";
            this.playerRadio.UseVisualStyleBackColor = true;
            this.playerRadio.Click += new System.EventHandler(this.PlayerRadio_Click);
            // 
            // playerCD
            // 
            this.playerCD.AutoSize = true;
            this.playerCD.Location = new System.Drawing.Point(89, 3);
            this.playerCD.Name = "playerCD";
            this.playerCD.Size = new System.Drawing.Size(40, 17);
            this.playerCD.TabIndex = 12;
            this.playerCD.TabStop = true;
            this.playerCD.Text = "CD";
            this.playerCD.UseVisualStyleBackColor = true;
            this.playerCD.Click += new System.EventHandler(this.PlayerCD_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnDown);
            this.panel1.Controls.Add(this.btnUp);
            this.panel1.Controls.Add(this.btnSort);
            this.panel1.Controls.Add(this.btnDel);
            this.panel1.Controls.Add(this.songList);
            this.panel1.Controls.Add(this.playerCD);
            this.panel1.Controls.Add(this.btnPlaySong);
            this.panel1.Controls.Add(this.playerRadio);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Location = new System.Drawing.Point(-1, 100);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(174, 273);
            this.panel1.TabIndex = 13;
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(114, 241);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(45, 23);
            this.btnDel.TabIndex = 13;
            this.btnDel.Text = "Del";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.BtnDel_Click);
            // 
            // btnSort
            // 
            this.btnSort.Location = new System.Drawing.Point(114, 26);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(45, 23);
            this.btnSort.TabIndex = 14;
            this.btnSort.Text = "Sort";
            this.btnSort.UseVisualStyleBackColor = true;
            this.btnSort.Click += new System.EventHandler(this.BtnSort_Click);
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(114, 96);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(45, 23);
            this.btnUp.TabIndex = 15;
            this.btnUp.Text = "^";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.BtnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(114, 125);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(45, 23);
            this.btnDown.TabIndex = 16;
            this.btnDown.Text = "v";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.BtnDown_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 373);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOpenGameDir);
            this.Controls.Add(this.btnDirectory);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.logOutput);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.txtboxPath);
            this.Controls.Add(this.menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MSC OGG Converter";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtboxPath;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.TextBox logOutput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDirectory;
        private System.Windows.Forms.Button btnOpenGameDir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem menuTool;
        private System.Windows.Forms.ToolStripMenuItem btnAbout;
        private System.Windows.Forms.ToolStripMenuItem btnGitLab;
        private System.Windows.Forms.ToolStripMenuItem menuSettings;
        private System.Windows.Forms.ToolStripMenuItem btnRemMP3;
        private System.Windows.Forms.ToolStripMenuItem btnLogFolder;
        private System.Windows.Forms.ToolStripMenuItem btnLastLog;
        private System.Windows.Forms.ToolStripMenuItem btnLaunchGame;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem btnQuit;
        private System.Windows.Forms.ToolStripMenuItem btnSteam;
        private System.Windows.Forms.ToolStripMenuItem actionAfterConversionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnAfterLaunchGame;
        private System.Windows.Forms.ToolStripMenuItem btnAfterClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem btnAfterNone;
        private System.Windows.Forms.ToolStripMenuItem btnNoSteam;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem btnUpdates;
        private System.Windows.Forms.ToolStripMenuItem btnFFmpegLicense;
        private System.Windows.Forms.ToolStripMenuItem mSCOGGToolStripMenuItem;
        private System.Windows.Forms.ListBox songList;
        private System.Windows.Forms.Button btnPlaySong;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.RadioButton playerRadio;
        private System.Windows.Forms.RadioButton playerCD;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnSort;
    }
}

