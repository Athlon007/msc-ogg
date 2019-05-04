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
            this.btnShuffle = new System.Windows.Forms.Button();
            this.btnCloneSong = new System.Windows.Forms.Button();
            this.labCounter = new System.Windows.Forms.Label();
            this.labNowPlaying = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnMoveSong = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnSort = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.menuTool = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLastLog = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLogFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGitLab = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSteam = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCheckUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.btnYoutubeDlUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDesktopShortcut = new System.Windows.Forms.ToolStripMenuItem();
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
            this.btnAutoSort = new System.Windows.Forms.ToolStripMenuItem();
            this.btnUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLogs = new System.Windows.Forms.ToolStripMenuItem();
            this.btnHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDisableMetafiles = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLaunchGame = new System.Windows.Forms.ToolStripMenuItem();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.btnHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDownloadUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadUpdateNowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOpenGameDir = new System.Windows.Forms.Button();
            this.btnDirectory = new System.Windows.Forms.Button();
            this.labelConvert = new System.Windows.Forms.Label();
            this.dragDropPanel = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.tabDownload = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.btnDownload = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtboxVideo = new System.Windows.Forms.TextBox();
            this.tabMeta = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSetName = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSongName = new System.Windows.Forms.TextBox();
            this.downloadProgress = new System.Windows.Forms.ProgressBar();
            this.panel1.SuspendLayout();
            this.menu.SuspendLayout();
            this.dragDropPanel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.tabDownload.SuspendLayout();
            this.tabMeta.SuspendLayout();
            this.SuspendLayout();
            // 
            // logOutput
            // 
            this.logOutput.BackColor = System.Drawing.SystemColors.Control;
            this.logOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logOutput.Location = new System.Drawing.Point(3, 3);
            this.logOutput.Multiline = true;
            this.logOutput.Name = "logOutput";
            this.logOutput.ReadOnly = true;
            this.logOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logOutput.Size = new System.Drawing.Size(476, 321);
            this.logOutput.TabIndex = 2;
            // 
            // songList
            // 
            this.songList.FormattingEnabled = true;
            this.songList.HorizontalScrollbar = true;
            this.songList.Location = new System.Drawing.Point(5, 23);
            this.songList.Name = "songList";
            this.songList.Size = new System.Drawing.Size(197, 264);
            this.songList.TabIndex = 8;
            this.songList.SelectedIndexChanged += new System.EventHandler(this.SongList_SelectedIndexChanged);
            // 
            // btnPlaySong
            // 
            this.btnPlaySong.Location = new System.Drawing.Point(74, 307);
            this.btnPlaySong.Name = "btnPlaySong";
            this.btnPlaySong.Size = new System.Drawing.Size(43, 23);
            this.btnPlaySong.TabIndex = 9;
            this.btnPlaySong.Text = "Play";
            this.btnPlaySong.UseVisualStyleBackColor = true;
            this.btnPlaySong.Click += new System.EventHandler(this.BtnPlay);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(126, 307);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(43, 23);
            this.btnStop.TabIndex = 10;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // playerRadio
            // 
            this.playerRadio.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.playerRadio.AutoSize = true;
            this.playerRadio.Location = new System.Drawing.Point(72, 3);
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
            this.playerCD.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.playerCD.AutoSize = true;
            this.playerCD.Location = new System.Drawing.Point(131, 3);
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
            this.panel1.Controls.Add(this.btnShuffle);
            this.panel1.Controls.Add(this.btnCloneSong);
            this.panel1.Controls.Add(this.labCounter);
            this.panel1.Controls.Add(this.labNowPlaying);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnMoveSong);
            this.panel1.Controls.Add(this.btnDown);
            this.panel1.Controls.Add(this.btnUp);
            this.panel1.Controls.Add(this.btnSort);
            this.panel1.Controls.Add(this.btnDel);
            this.panel1.Controls.Add(this.songList);
            this.panel1.Controls.Add(this.playerCD);
            this.panel1.Controls.Add(this.btnPlaySong);
            this.panel1.Controls.Add(this.playerRadio);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Location = new System.Drawing.Point(-1, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(256, 333);
            this.panel1.TabIndex = 13;
            // 
            // btnShuffle
            // 
            this.btnShuffle.Location = new System.Drawing.Point(205, 307);
            this.btnShuffle.Name = "btnShuffle";
            this.btnShuffle.Size = new System.Drawing.Size(48, 23);
            this.btnShuffle.TabIndex = 22;
            this.btnShuffle.Text = "Shuffle";
            this.btnShuffle.UseVisualStyleBackColor = true;
            this.btnShuffle.Click += new System.EventHandler(this.BtnShuffle_Click);
            // 
            // btnCloneSong
            // 
            this.btnCloneSong.Location = new System.Drawing.Point(208, 228);
            this.btnCloneSong.Name = "btnCloneSong";
            this.btnCloneSong.Size = new System.Drawing.Size(43, 23);
            this.btnCloneSong.TabIndex = 21;
            this.btnCloneSong.Text = "Clone";
            this.btnCloneSong.UseVisualStyleBackColor = true;
            this.btnCloneSong.Click += new System.EventHandler(this.BtnCloneSong_Click);
            // 
            // labCounter
            // 
            this.labCounter.AutoSize = true;
            this.labCounter.BackColor = System.Drawing.SystemColors.Control;
            this.labCounter.ForeColor = System.Drawing.Color.Black;
            this.labCounter.Location = new System.Drawing.Point(2, 315);
            this.labCounter.Name = "labCounter";
            this.labCounter.Size = new System.Drawing.Size(48, 13);
            this.labCounter.TabIndex = 20;
            this.labCounter.Text = "Songs: x";
            // 
            // labNowPlaying
            // 
            this.labNowPlaying.AutoSize = true;
            this.labNowPlaying.Location = new System.Drawing.Point(81, 290);
            this.labNowPlaying.Name = "labNowPlaying";
            this.labNowPlaying.Size = new System.Drawing.Size(77, 13);
            this.labNowPlaying.TabIndex = 19;
            this.labNowPlaying.Text = "labNowPlaying";
            this.labNowPlaying.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(204, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Move To:";
            // 
            // btnMoveSong
            // 
            this.btnMoveSong.Location = new System.Drawing.Point(208, 170);
            this.btnMoveSong.Name = "btnMoveSong";
            this.btnMoveSong.Size = new System.Drawing.Size(43, 23);
            this.btnMoveSong.TabIndex = 17;
            this.btnMoveSong.Text = "CD";
            this.btnMoveSong.UseVisualStyleBackColor = true;
            this.btnMoveSong.Click += new System.EventHandler(this.BtnMoveSong_Click);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(208, 52);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(43, 23);
            this.btnDown.TabIndex = 16;
            this.btnDown.Text = "v";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.BtnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(208, 23);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(43, 23);
            this.btnUp.TabIndex = 15;
            this.btnUp.Text = "^";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.BtnUp_Click);
            // 
            // btnSort
            // 
            this.btnSort.Location = new System.Drawing.Point(208, 105);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(43, 23);
            this.btnSort.TabIndex = 14;
            this.btnSort.Text = "Sort";
            this.btnSort.UseVisualStyleBackColor = true;
            this.btnSort.Click += new System.EventHandler(this.BtnSort_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(208, 260);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(43, 23);
            this.btnDel.TabIndex = 13;
            this.btnDel.Text = "Delete";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.BtnDel_Click);
            // 
            // menuTool
            // 
            this.menuTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLastLog,
            this.btnLogFolder,
            this.toolStripSeparator1,
            this.btnGitLab,
            this.btnSteam,
            this.toolStripSeparator3,
            this.btnCheckUpdate,
            this.btnYoutubeDlUpdate,
            this.toolStripSeparator4,
            this.btnDesktopShortcut,
            this.btnAbout,
            this.btnQuit});
            this.menuTool.Name = "menuTool";
            this.menuTool.Size = new System.Drawing.Size(42, 20);
            this.menuTool.Text = "Tool";
            // 
            // btnLastLog
            // 
            this.btnLastLog.Name = "btnLastLog";
            this.btnLastLog.Size = new System.Drawing.Size(232, 22);
            this.btnLastLog.Text = "Open History";
            this.btnLastLog.Click += new System.EventHandler(this.OpenLastConversionToolStripMenuItem_Click);
            // 
            // btnLogFolder
            // 
            this.btnLogFolder.Name = "btnLogFolder";
            this.btnLogFolder.Size = new System.Drawing.Size(232, 22);
            this.btnLogFolder.Text = "Open LOG folder";
            this.btnLogFolder.Click += new System.EventHandler(this.BtnLogFolder_Click);
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
            this.btnCheckUpdate.Text = "Check for Update";
            this.btnCheckUpdate.Click += new System.EventHandler(this.BtnCheckUpdate_Click);
            // 
            // btnYoutubeDlUpdate
            // 
            this.btnYoutubeDlUpdate.Name = "btnYoutubeDlUpdate";
            this.btnYoutubeDlUpdate.Size = new System.Drawing.Size(232, 22);
            this.btnYoutubeDlUpdate.Text = "Check for youtube-dl update";
            this.btnYoutubeDlUpdate.Click += new System.EventHandler(this.BtnYoutubeDlUpdate_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(229, 6);
            // 
            // btnDesktopShortcut
            // 
            this.btnDesktopShortcut.Name = "btnDesktopShortcut";
            this.btnDesktopShortcut.Size = new System.Drawing.Size(232, 22);
            this.btnDesktopShortcut.Text = "Create Destkop Shortcut";
            this.btnDesktopShortcut.Click += new System.EventHandler(this.BtnDesktopShortcut_Click);
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
            this.mSCOGGToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.mSCOGGToolStripMenuItem.Text = "MSC Music Manager";
            this.mSCOGGToolStripMenuItem.Click += new System.EventHandler(this.MSCOGGToolStripMenuItem_Click);
            // 
            // btnFFmpegLicense
            // 
            this.btnFFmpegLicense.Name = "btnFFmpegLicense";
            this.btnFFmpegLicense.Size = new System.Drawing.Size(184, 22);
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
            this.btnLogs,
            this.btnHistory,
            this.btnDisableMetafiles});
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
            this.btnAfterLaunchGame.Click += new System.EventHandler(this.LaunchTheGameToolStripMenuItem1_Click);
            // 
            // btnAfterClose
            // 
            this.btnAfterClose.CheckOnClick = true;
            this.btnAfterClose.Name = "btnAfterClose";
            this.btnAfterClose.Size = new System.Drawing.Size(172, 22);
            this.btnAfterClose.Text = "Close the program";
            this.btnAfterClose.Click += new System.EventHandler(this.CloseTheProgramToolStripMenuItem_Click);
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
            // btnAutoSort
            // 
            this.btnAutoSort.CheckOnClick = true;
            this.btnAutoSort.Name = "btnAutoSort";
            this.btnAutoSort.Size = new System.Drawing.Size(229, 22);
            this.btnAutoSort.Text = "Auto Sort";
            this.btnAutoSort.Click += new System.EventHandler(this.BtnAutoSort_Click);
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
            this.btnLogs.CheckOnClick = true;
            this.btnLogs.Name = "btnLogs";
            this.btnLogs.Size = new System.Drawing.Size(229, 22);
            this.btnLogs.Text = "Crash Logs";
            this.btnLogs.Click += new System.EventHandler(this.BtnLogs_Click);
            // 
            // btnHistory
            // 
            this.btnHistory.CheckOnClick = true;
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(229, 22);
            this.btnHistory.Text = "History";
            this.btnHistory.Click += new System.EventHandler(this.BtnHistory_Click);
            // 
            // btnDisableMetafiles
            // 
            this.btnDisableMetafiles.CheckOnClick = true;
            this.btnDisableMetafiles.Name = "btnDisableMetafiles";
            this.btnDisableMetafiles.Size = new System.Drawing.Size(229, 22);
            this.btnDisableMetafiles.Text = "Disable Metafiles";
            this.btnDisableMetafiles.Click += new System.EventHandler(this.BtnDisableMetafiles_Click);
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
            this.btnHelp,
            this.btnDownloadUpdate});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menu.Size = new System.Drawing.Size(743, 24);
            this.menu.TabIndex = 7;
            this.menu.Text = "menu";
            // 
            // btnHelp
            // 
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(44, 20);
            this.btnHelp.Text = "Help";
            this.btnHelp.Click += new System.EventHandler(this.BtnHelp_Click);
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
            // btnOpenGameDir
            // 
            this.btnOpenGameDir.Location = new System.Drawing.Point(126, 26);
            this.btnOpenGameDir.Name = "btnOpenGameDir";
            this.btnOpenGameDir.Size = new System.Drawing.Size(130, 23);
            this.btnOpenGameDir.TabIndex = 5;
            this.btnOpenGameDir.Text = "Open in Explorer";
            this.btnOpenGameDir.UseVisualStyleBackColor = true;
            this.btnOpenGameDir.Click += new System.EventHandler(this.BtnOpenGameDir_Click);
            // 
            // btnDirectory
            // 
            this.btnDirectory.Location = new System.Drawing.Point(-1, 26);
            this.btnDirectory.Name = "btnDirectory";
            this.btnDirectory.Size = new System.Drawing.Size(129, 23);
            this.btnDirectory.TabIndex = 4;
            this.btnDirectory.Text = "Set Game Folder";
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
            this.labelConvert.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dragDropPanel
            // 
            this.dragDropPanel.BackColor = System.Drawing.Color.White;
            this.dragDropPanel.Controls.Add(this.labelConvert);
            this.dragDropPanel.Location = new System.Drawing.Point(579, 28);
            this.dragDropPanel.Name = "dragDropPanel";
            this.dragDropPanel.Size = new System.Drawing.Size(184, 65);
            this.dragDropPanel.TabIndex = 14;
            this.dragDropPanel.Visible = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabLog);
            this.tabControl1.Controls.Add(this.tabDownload);
            this.tabControl1.Controls.Add(this.tabMeta);
            this.tabControl1.HotTrack = true;
            this.tabControl1.Location = new System.Drawing.Point(255, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(490, 353);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControl1_SelectedIndexChanged);
            // 
            // tabLog
            // 
            this.tabLog.BackColor = System.Drawing.Color.Transparent;
            this.tabLog.Controls.Add(this.logOutput);
            this.tabLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tabLog.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(482, 327);
            this.tabLog.TabIndex = 0;
            this.tabLog.Text = "Log";
            // 
            // tabDownload
            // 
            this.tabDownload.BackColor = System.Drawing.Color.White;
            this.tabDownload.Controls.Add(this.label6);
            this.tabDownload.Controls.Add(this.btnDownload);
            this.tabDownload.Controls.Add(this.label5);
            this.tabDownload.Controls.Add(this.txtboxVideo);
            this.tabDownload.Location = new System.Drawing.Point(4, 22);
            this.tabDownload.Name = "tabDownload";
            this.tabDownload.Padding = new System.Windows.Forms.Padding(3);
            this.tabDownload.Size = new System.Drawing.Size(482, 327);
            this.tabDownload.TabIndex = 1;
            this.tabDownload.Text = "Download";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 301);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(408, 26);
            this.label6.TabIndex = 15;
            this.label6.Text = "Note:\r\nThe author of this tool doesn\'t take any responsibility of the way how tha" +
    "t tool is used.";
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(6, 53);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(172, 24);
            this.btnDownload.TabIndex = 15;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.BtnDownload_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Search Term/Video Link:";
            // 
            // txtboxVideo
            // 
            this.txtboxVideo.Location = new System.Drawing.Point(6, 27);
            this.txtboxVideo.Name = "txtboxVideo";
            this.txtboxVideo.Size = new System.Drawing.Size(469, 20);
            this.txtboxVideo.TabIndex = 1;
            this.txtboxVideo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtboxVideo_KeyDown);
            // 
            // tabMeta
            // 
            this.tabMeta.Controls.Add(this.label2);
            this.tabMeta.Controls.Add(this.btnSetName);
            this.tabMeta.Controls.Add(this.label1);
            this.tabMeta.Controls.Add(this.txtSongName);
            this.tabMeta.Location = new System.Drawing.Point(4, 22);
            this.tabMeta.Name = "tabMeta";
            this.tabMeta.Size = new System.Drawing.Size(482, 327);
            this.tabMeta.TabIndex = 2;
            this.tabMeta.Text = "Edit";
            this.tabMeta.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(301, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Select song from the list to the left and edit it\'s displayed name.";
            // 
            // btnSetName
            // 
            this.btnSetName.Location = new System.Drawing.Point(6, 74);
            this.btnSetName.Name = "btnSetName";
            this.btnSetName.Size = new System.Drawing.Size(172, 24);
            this.btnSetName.TabIndex = 17;
            this.btnSetName.Text = "Set";
            this.btnSetName.UseVisualStyleBackColor = true;
            this.btnSetName.Click += new System.EventHandler(this.BtnSetName_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Name:";
            // 
            // txtSongName
            // 
            this.txtSongName.Location = new System.Drawing.Point(6, 48);
            this.txtSongName.Name = "txtSongName";
            this.txtSongName.Size = new System.Drawing.Size(469, 20);
            this.txtSongName.TabIndex = 2;
            // 
            // downloadProgress
            // 
            this.downloadProgress.Location = new System.Drawing.Point(386, 1);
            this.downloadProgress.Name = "downloadProgress";
            this.downloadProgress.Size = new System.Drawing.Size(356, 23);
            this.downloadProgress.TabIndex = 19;
            this.downloadProgress.Visible = false;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 379);
            this.Controls.Add(this.downloadProgress);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnOpenGameDir);
            this.Controls.Add(this.btnDirectory);
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
            this.tabControl1.ResumeLayout(false);
            this.tabLog.ResumeLayout(false);
            this.tabLog.PerformLayout();
            this.tabDownload.ResumeLayout(false);
            this.tabDownload.PerformLayout();
            this.tabMeta.ResumeLayout(false);
            this.tabMeta.PerformLayout();
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
        private System.Windows.Forms.Button btnOpenGameDir;
        private System.Windows.Forms.Button btnDirectory;
        private System.Windows.Forms.Label labelConvert;
        private System.Windows.Forms.Panel dragDropPanel;
        private System.Windows.Forms.Label labNowPlaying;
        private System.Windows.Forms.ToolStripMenuItem btnAutoSort;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.TabPage tabDownload;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtboxVideo;
        private System.Windows.Forms.ToolStripMenuItem btnDesktopShortcut;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripMenuItem btnHelp;
        private System.Windows.Forms.ToolStripMenuItem btnHistory;
        private System.Windows.Forms.Label labCounter;
        private System.Windows.Forms.TabPage tabMeta;
        private System.Windows.Forms.Button btnSetName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSongName;
        private System.Windows.Forms.Button btnCloneSong;
        private System.Windows.Forms.Button btnShuffle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem btnDisableMetafiles;
        private System.Windows.Forms.ToolStripMenuItem btnYoutubeDlUpdate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ProgressBar downloadProgress;
    }
}

