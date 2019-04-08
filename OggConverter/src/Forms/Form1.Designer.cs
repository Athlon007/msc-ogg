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
            this.logOutput = new System.Windows.Forms.TextBox();
            this.songList = new System.Windows.Forms.ListBox();
            this.btnPlaySong = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.playerRadio = new System.Windows.Forms.RadioButton();
            this.playerCD = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labNowPlaying = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnMoveSong = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnSort = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.menuTool = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLogFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLastLog = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGitLab = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSteam = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCheckUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mSCOGGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFFmpegLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.btnQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRemMP3 = new System.Windows.Forms.ToolStripMenuItem();
            this.actionAfterConversionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAfterLaunchGame = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAfterClose = new System.Windows.Forms.ToolStripMenuItem();
            this.btnShowConversionLog = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAfterNone = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNoSteam = new System.Windows.Forms.ToolStripMenuItem();
            this.btnUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLogs = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLaunchGame = new System.Windows.Forms.ToolStripMenuItem();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.btnDownloadUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadUpdateNowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnConvert = new System.Windows.Forms.Button();
            this.txtboxPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOpenGameDir = new System.Windows.Forms.Button();
            this.btnDirectory = new System.Windows.Forms.Button();
            this.labelConvert = new System.Windows.Forms.Label();
            this.dragDropPanel = new System.Windows.Forms.Panel();
            this.btnAutoSort = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.menu.SuspendLayout();
            this.dragDropPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // logOutput
            // 
            this.logOutput.Location = new System.Drawing.Point(172, 100);
            this.logOutput.Multiline = true;
            this.logOutput.Name = "logOutput";
            this.logOutput.ReadOnly = true;
            this.logOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logOutput.Size = new System.Drawing.Size(572, 273);
            this.logOutput.TabIndex = 2;
            // 
            // songList
            // 
            this.songList.FormattingEnabled = true;
            this.songList.Location = new System.Drawing.Point(10, 23);
            this.songList.Name = "songList";
            this.songList.Size = new System.Drawing.Size(98, 199);
            this.songList.TabIndex = 8;
            // 
            // btnPlaySong
            // 
            this.btnPlaySong.Location = new System.Drawing.Point(10, 241);
            this.btnPlaySong.Name = "btnPlaySong";
            this.btnPlaySong.Size = new System.Drawing.Size(46, 23);
            this.btnPlaySong.TabIndex = 9;
            this.btnPlaySong.Text = "Play";
            this.btnPlaySong.UseVisualStyleBackColor = true;
            this.btnPlaySong.Click += new System.EventHandler(this.BtnPlay);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(62, 241);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(46, 23);
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
            this.panel1.Controls.Add(this.labNowPlaying);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnMoveSong);
            this.panel1.Controls.Add(this.label3);
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
            // labNowPlaying
            // 
            this.labNowPlaying.AutoSize = true;
            this.labNowPlaying.Location = new System.Drawing.Point(12, 225);
            this.labNowPlaying.Name = "labNowPlaying";
            this.labNowPlaying.Size = new System.Drawing.Size(137, 13);
            this.labNowPlaying.TabIndex = 19;
            this.labNowPlaying.Text = "Playing: Radio\\track15.ogg";
            this.labNowPlaying.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(115, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Move To:";
            // 
            // btnMoveSong
            // 
            this.btnMoveSong.Location = new System.Drawing.Point(118, 174);
            this.btnMoveSong.Name = "btnMoveSong";
            this.btnMoveSong.Size = new System.Drawing.Size(45, 23);
            this.btnMoveSong.TabIndex = 17;
            this.btnMoveSong.Text = "CD";
            this.btnMoveSong.UseVisualStyleBackColor = true;
            this.btnMoveSong.Click += new System.EventHandler(this.BtnMoveSong_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(122, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Order:";
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(118, 103);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(45, 23);
            this.btnDown.TabIndex = 16;
            this.btnDown.Text = "v";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.BtnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(118, 74);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(45, 23);
            this.btnUp.TabIndex = 15;
            this.btnUp.Text = "^";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.BtnUp_Click);
            // 
            // btnSort
            // 
            this.btnSort.Location = new System.Drawing.Point(118, 22);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(45, 23);
            this.btnSort.TabIndex = 14;
            this.btnSort.Text = "Sort";
            this.btnSort.UseVisualStyleBackColor = true;
            this.btnSort.Click += new System.EventHandler(this.BtnSort_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(118, 241);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(46, 23);
            this.btnDel.TabIndex = 13;
            this.btnDel.Text = "Delete";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.BtnDel_Click);
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
            this.btnCheckUpdate,
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
            this.btnLastLog.Click += new System.EventHandler(this.OpenLastConversionToolStripMenuItem_Click);
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
            this.btnGitLab.Click += new System.EventHandler(this.GitToolStripMenuItem_Click);
            // 
            // btnSteam
            // 
            this.btnSteam.Name = "btnSteam";
            this.btnSteam.Size = new System.Drawing.Size(232, 22);
            this.btnSteam.Text = "Steam Community discussion";
            this.btnSteam.Click += new System.EventHandler(this.SteamCommunityDiscussionToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(229, 6);
            // 
            // btnCheckUpdate
            // 
            this.btnCheckUpdate.Name = "btnCheckUpdate";
            this.btnCheckUpdate.Size = new System.Drawing.Size(232, 22);
            this.btnCheckUpdate.Text = "Check for update";
            this.btnCheckUpdate.Click += new System.EventHandler(this.BtnCheckUpdate_Click);
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
            this.btnQuit.Click += new System.EventHandler(this.QuitToolStripMenuItem_Click);
            // 
            // menuSettings
            // 
            this.menuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRemMP3,
            this.actionAfterConversionToolStripMenuItem,
            this.btnNoSteam,
            this.btnAutoSort,
            this.btnUpdates,
            this.btnLogs});
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
            this.btnRemMP3.Click += new System.EventHandler(this.RemoveOldMP3FilesToolStripMenuItem_Click);
            // 
            // actionAfterConversionToolStripMenuItem
            // 
            this.actionAfterConversionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAfterLaunchGame,
            this.btnAfterClose,
            this.btnShowConversionLog,
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
            this.btnAfterLaunchGame.Size = new System.Drawing.Size(184, 22);
            this.btnAfterLaunchGame.Text = "Launch the game";
            this.btnAfterLaunchGame.Click += new System.EventHandler(this.LaunchTheGameToolStripMenuItem1_Click);
            // 
            // btnAfterClose
            // 
            this.btnAfterClose.CheckOnClick = true;
            this.btnAfterClose.Name = "btnAfterClose";
            this.btnAfterClose.Size = new System.Drawing.Size(184, 22);
            this.btnAfterClose.Text = "Close the program";
            this.btnAfterClose.Click += new System.EventHandler(this.CloseTheProgramToolStripMenuItem_Click);
            // 
            // btnShowConversionLog
            // 
            this.btnShowConversionLog.CheckOnClick = true;
            this.btnShowConversionLog.Name = "btnShowConversionLog";
            this.btnShowConversionLog.Size = new System.Drawing.Size(184, 22);
            this.btnShowConversionLog.Text = "Show conversion log";
            this.btnShowConversionLog.Click += new System.EventHandler(this.BtnShowConversionLog_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(181, 6);
            // 
            // btnAfterNone
            // 
            this.btnAfterNone.CheckOnClick = true;
            this.btnAfterNone.Name = "btnAfterNone";
            this.btnAfterNone.Size = new System.Drawing.Size(184, 22);
            this.btnAfterNone.Text = "None";
            this.btnAfterNone.Click += new System.EventHandler(this.NoneToolStripMenuItem_Click);
            // 
            // btnNoSteam
            // 
            this.btnNoSteam.CheckOnClick = true;
            this.btnNoSteam.Name = "btnNoSteam";
            this.btnNoSteam.Size = new System.Drawing.Size(229, 22);
            this.btnNoSteam.Text = "Launch game without Steam";
            this.btnNoSteam.Click += new System.EventHandler(this.LaunchGameWithoutSteamToolStripMenuItem_Click);
            // 
            // btnUpdates
            // 
            this.btnUpdates.CheckOnClick = true;
            this.btnUpdates.Name = "btnUpdates";
            this.btnUpdates.Size = new System.Drawing.Size(229, 22);
            this.btnUpdates.Text = "Updates";
            this.btnUpdates.Click += new System.EventHandler(this.CheckBoxUpdates_Click);
            // 
            // btnLogs
            // 
            this.btnLogs.Name = "btnLogs";
            this.btnLogs.Size = new System.Drawing.Size(229, 22);
            this.btnLogs.Text = "Crash Logs";
            this.btnLogs.Click += new System.EventHandler(this.BtnLogs_Click);
            // 
            // btnLaunchGame
            // 
            this.btnLaunchGame.Name = "btnLaunchGame";
            this.btnLaunchGame.Size = new System.Drawing.Size(91, 20);
            this.btnLaunchGame.Text = "Launch game";
            this.btnLaunchGame.Click += new System.EventHandler(this.LaunchTheGameToolStripMenuItem_Click);
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.White;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTool,
            this.menuSettings,
            this.btnLaunchGame,
            this.btnDownloadUpdate});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menu.Size = new System.Drawing.Size(743, 24);
            this.menu.TabIndex = 7;
            this.menu.Text = "menu";
            // 
            // btnDownloadUpdate
            // 
            this.btnDownloadUpdate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDownloadUpdate.Name = "btnDownloadUpdate";
            this.btnDownloadUpdate.Size = new System.Drawing.Size(117, 20);
            this.btnDownloadUpdate.Text = "Get Update Now!";
            this.btnDownloadUpdate.Visible = false;
            this.btnDownloadUpdate.Click += new System.EventHandler(this.BtnDownloadUpdate_Click);
            // 
            // downloadUpdateNowToolStripMenuItem
            // 
            this.downloadUpdateNowToolStripMenuItem.Name = "downloadUpdateNowToolStripMenuItem";
            this.downloadUpdateNowToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            this.downloadUpdateNowToolStripMenuItem.Text = "Download Update Now!";
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(190, 70);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(172, 24);
            this.btnConvert.TabIndex = 1;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.BtnConvert_Click);
            // 
            // txtboxPath
            // 
            this.txtboxPath.Location = new System.Drawing.Point(10, 44);
            this.txtboxPath.Name = "txtboxPath";
            this.txtboxPath.ReadOnly = true;
            this.txtboxPath.Size = new System.Drawing.Size(322, 20);
            this.txtboxPath.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "My Summer Car Path:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(367, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(369, 65);
            this.label2.TabIndex = 6;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // btnOpenGameDir
            // 
            this.btnOpenGameDir.Location = new System.Drawing.Point(10, 70);
            this.btnOpenGameDir.Name = "btnOpenGameDir";
            this.btnOpenGameDir.Size = new System.Drawing.Size(172, 23);
            this.btnOpenGameDir.TabIndex = 5;
            this.btnOpenGameDir.Text = "Open game directory";
            this.btnOpenGameDir.UseVisualStyleBackColor = true;
            this.btnOpenGameDir.Click += new System.EventHandler(this.BtnOpenGameDir_Click);
            // 
            // btnDirectory
            // 
            this.btnDirectory.Location = new System.Drawing.Point(338, 43);
            this.btnDirectory.Name = "btnDirectory";
            this.btnDirectory.Size = new System.Drawing.Size(24, 22);
            this.btnDirectory.TabIndex = 4;
            this.btnDirectory.Text = "...";
            this.btnDirectory.UseVisualStyleBackColor = true;
            this.btnDirectory.Click += new System.EventHandler(this.BtnDirectory_Click);
            // 
            // labelConvert
            // 
            this.labelConvert.AutoSize = true;
            this.labelConvert.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.labelConvert.Location = new System.Drawing.Point(243, 151);
            this.labelConvert.Name = "labelConvert";
            this.labelConvert.Size = new System.Drawing.Size(136, 20);
            this.labelConvert.TabIndex = 0;
            this.labelConvert.Text = "Convert to {folder}";
            // 
            // dragDropPanel
            // 
            this.dragDropPanel.Controls.Add(this.labelConvert);
            this.dragDropPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dragDropPanel.Location = new System.Drawing.Point(0, 0);
            this.dragDropPanel.Name = "dragDropPanel";
            this.dragDropPanel.Size = new System.Drawing.Size(743, 373);
            this.dragDropPanel.TabIndex = 14;
            this.dragDropPanel.Visible = false;
            // 
            // btnAutoSort
            // 
            this.btnAutoSort.CheckOnClick = true;
            this.btnAutoSort.Name = "btnAutoSort";
            this.btnAutoSort.Size = new System.Drawing.Size(229, 22);
            this.btnAutoSort.Text = "Auto Sort";
            this.btnAutoSort.Click += new System.EventHandler(this.BtnAutoSort_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 373);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOpenGameDir);
            this.Controls.Add(this.btnDirectory);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.logOutput);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.txtboxPath);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.dragDropPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(660, 412);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MSC Music Manager";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.DragLeave += new System.EventHandler(this.Form1_DragLeave);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.dragDropPanel.ResumeLayout(false);
            this.dragDropPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox logOutput;
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
        private System.Windows.Forms.Button btnMoveSong;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem menuTool;
        private System.Windows.Forms.ToolStripMenuItem btnLogFolder;
        private System.Windows.Forms.ToolStripMenuItem btnLastLog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem btnGitLab;
        private System.Windows.Forms.ToolStripMenuItem btnSteam;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem btnAbout;
        private System.Windows.Forms.ToolStripMenuItem mSCOGGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnFFmpegLicense;
        private System.Windows.Forms.ToolStripMenuItem btnQuit;
        private System.Windows.Forms.ToolStripMenuItem menuSettings;
        private System.Windows.Forms.ToolStripMenuItem btnRemMP3;
        private System.Windows.Forms.ToolStripMenuItem actionAfterConversionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnAfterLaunchGame;
        private System.Windows.Forms.ToolStripMenuItem btnAfterClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem btnAfterNone;
        private System.Windows.Forms.ToolStripMenuItem btnNoSteam;
        private System.Windows.Forms.ToolStripMenuItem btnUpdates;
        private System.Windows.Forms.ToolStripMenuItem btnLogs;
        private System.Windows.Forms.ToolStripMenuItem btnLaunchGame;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem btnCheckUpdate;
        private System.Windows.Forms.ToolStripMenuItem btnDownloadUpdate;
        private System.Windows.Forms.ToolStripMenuItem downloadUpdateNowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnShowConversionLog;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.TextBox txtboxPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOpenGameDir;
        private System.Windows.Forms.Button btnDirectory;
        private System.Windows.Forms.Label labelConvert;
        private System.Windows.Forms.Panel dragDropPanel;
        private System.Windows.Forms.Label labNowPlaying;
        private System.Windows.Forms.ToolStripMenuItem btnAutoSort;
    }
}

