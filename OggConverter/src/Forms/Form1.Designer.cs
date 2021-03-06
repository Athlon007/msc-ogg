﻿namespace OggConverter
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.logOutput = new System.Windows.Forms.TextBox();
            this.defaultContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextDefaultUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.contextDefaultCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.contextDefaultPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.contextDefaultSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.songList = new System.Windows.Forms.ListBox();
            this.btnPlaySong = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.selectedFolder = new System.Windows.Forms.ComboBox();
            this.btnShuffle = new System.Windows.Forms.Button();
            this.btnCloneSong = new System.Windows.Forms.Button();
            this.labCounter = new System.Windows.Forms.Label();
            this.labNowPlaying = new System.Windows.Forms.Label();
            this.btnMoveSong = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnSort = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.menuTool = new System.Windows.Forms.ToolStripMenuItem();
            this.btnWebsite = new System.Windows.Forms.ToolStripMenuItem();
            this.btnGitLab = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSteam = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMSCMMAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFFmpegLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.btnQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLaunchGame = new System.Windows.Forms.ToolStripMenuItem();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.btnHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDonate = new System.Windows.Forms.ToolStripMenuItem();
            this.btnReportIssue = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDownloadUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadUpdateNowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOpenGameDir = new System.Windows.Forms.Button();
            this.btnDirectory = new System.Windows.Forms.Button();
            this.labelConvert = new System.Windows.Forms.Label();
            this.dragDropPanel = new System.Windows.Forms.Panel();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.tabDownload = new System.Windows.Forms.TabPage();
            this.labProgress = new System.Windows.Forms.Label();
            this.proYt = new System.Windows.Forms.ProgressBar();
            this.ytdlOutput = new System.Windows.Forms.TextBox();
            this.btnCancelDownload = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnDownload = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtboxVideo = new System.Windows.Forms.TextBox();
            this.tabMeta = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOpenWithAudacity = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSetName = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSongName = new System.Windows.Forms.TextBox();
            this.tabCoverArt = new System.Windows.Forms.TabPage();
            this.labImageInfo = new System.Windows.Forms.Label();
            this.picCoverArt = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCoverArtImage = new System.Windows.Forms.Button();
            this.labCurrentFont = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCdText = new System.Windows.Forms.TextBox();
            this.btnSelectFont = new System.Windows.Forms.Button();
            this.btnCreateCoverArt = new System.Windows.Forms.Button();
            this.tabRecycle = new System.Windows.Forms.TabPage();
            this.btnEmptyAll = new System.Windows.Forms.Button();
            this.labRecycle = new System.Windows.Forms.Label();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnRecycleDelete = new System.Windows.Forms.Button();
            this.trashList = new System.Windows.Forms.ListBox();
            this.downloadProgress = new System.Windows.Forms.ProgressBar();
            this.songListContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.contextDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMove = new System.Windows.Forms.ToolStripMenuItem();
            this.contextAll = new System.Windows.Forms.ToolStripMenuItem();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.defaultContext.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menu.SuspendLayout();
            this.dragDropPanel.SuspendLayout();
            this.tabs.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.tabDownload.SuspendLayout();
            this.tabMeta.SuspendLayout();
            this.tabCoverArt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCoverArt)).BeginInit();
            this.tabRecycle.SuspendLayout();
            this.songListContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // logOutput
            // 
            this.logOutput.BackColor = System.Drawing.SystemColors.Control;
            this.logOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logOutput.ContextMenuStrip = this.defaultContext;
            this.logOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logOutput.Location = new System.Drawing.Point(3, 3);
            this.logOutput.Multiline = true;
            this.logOutput.Name = "logOutput";
            this.logOutput.ReadOnly = true;
            this.logOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logOutput.Size = new System.Drawing.Size(476, 330);
            this.logOutput.TabIndex = 2;
            // 
            // defaultContext
            // 
            this.defaultContext.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.defaultContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextDefaultUndo,
            this.contextDefaultCopy,
            this.contextDefaultPaste,
            this.contextDefaultSelectAll});
            this.defaultContext.Name = "songListContext";
            this.defaultContext.Size = new System.Drawing.Size(123, 92);
            this.defaultContext.Opened += new System.EventHandler(this.defaultContext_Opened);
            // 
            // contextDefaultUndo
            // 
            this.contextDefaultUndo.Name = "contextDefaultUndo";
            this.contextDefaultUndo.Size = new System.Drawing.Size(122, 22);
            this.contextDefaultUndo.Text = "Undo";
            this.contextDefaultUndo.Click += new System.EventHandler(this.contextDefaultUndo_Click);
            // 
            // contextDefaultCopy
            // 
            this.contextDefaultCopy.Name = "contextDefaultCopy";
            this.contextDefaultCopy.ShortcutKeyDisplayString = "";
            this.contextDefaultCopy.Size = new System.Drawing.Size(122, 22);
            this.contextDefaultCopy.Text = "Copy";
            this.contextDefaultCopy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.contextDefaultCopy.Click += new System.EventHandler(this.contextDefaultCopy_Click);
            // 
            // contextDefaultPaste
            // 
            this.contextDefaultPaste.Name = "contextDefaultPaste";
            this.contextDefaultPaste.Size = new System.Drawing.Size(122, 22);
            this.contextDefaultPaste.Text = "Paste";
            this.contextDefaultPaste.Click += new System.EventHandler(this.contextDefaultPaste_Click);
            // 
            // contextDefaultSelectAll
            // 
            this.contextDefaultSelectAll.Name = "contextDefaultSelectAll";
            this.contextDefaultSelectAll.ShortcutKeyDisplayString = "";
            this.contextDefaultSelectAll.Size = new System.Drawing.Size(122, 22);
            this.contextDefaultSelectAll.Text = "Select All";
            this.contextDefaultSelectAll.Click += new System.EventHandler(this.contextDefaultSelectAll_Click);
            // 
            // songList
            // 
            this.songList.FormattingEnabled = true;
            this.songList.HorizontalScrollbar = true;
            this.songList.Location = new System.Drawing.Point(5, 23);
            this.songList.Name = "songList";
            this.songList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.songList.Size = new System.Drawing.Size(197, 264);
            this.songList.TabIndex = 8;
            this.songList.SelectedIndexChanged += new System.EventHandler(this.SongList_SelectedIndexChanged);
            this.songList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SongList_KeyDown);
            this.songList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SongList_MouseDown);
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
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.selectedFolder);
            this.panel1.Controls.Add(this.btnShuffle);
            this.panel1.Controls.Add(this.btnCloneSong);
            this.panel1.Controls.Add(this.labCounter);
            this.panel1.Controls.Add(this.labNowPlaying);
            this.panel1.Controls.Add(this.btnMoveSong);
            this.panel1.Controls.Add(this.btnDown);
            this.panel1.Controls.Add(this.btnUp);
            this.panel1.Controls.Add(this.btnSort);
            this.panel1.Controls.Add(this.btnDel);
            this.panel1.Controls.Add(this.songList);
            this.panel1.Controls.Add(this.btnPlaySong);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Location = new System.Drawing.Point(-1, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(256, 342);
            this.panel1.TabIndex = 13;
            // 
            // selectedFolder
            // 
            this.selectedFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectedFolder.FormattingEnabled = true;
            this.selectedFolder.Items.AddRange(new object[] {
            "Radio",
            "CD",
            "CD1",
            "CD2",
            "CD3"});
            this.selectedFolder.Location = new System.Drawing.Point(63, 1);
            this.selectedFolder.Name = "selectedFolder";
            this.selectedFolder.Size = new System.Drawing.Size(121, 21);
            this.selectedFolder.TabIndex = 23;
            this.selectedFolder.SelectedIndexChanged += new System.EventHandler(this.SelectedFolder_SelectedIndexChanged);
            // 
            // btnShuffle
            // 
            this.btnShuffle.Location = new System.Drawing.Point(204, 307);
            this.btnShuffle.Name = "btnShuffle";
            this.btnShuffle.Size = new System.Drawing.Size(48, 23);
            this.btnShuffle.TabIndex = 22;
            this.btnShuffle.Text = "Shuffle";
            this.btnShuffle.UseVisualStyleBackColor = true;
            this.btnShuffle.Click += new System.EventHandler(this.BtnShuffle_Click);
            // 
            // btnCloneSong
            // 
            this.btnCloneSong.Location = new System.Drawing.Point(204, 228);
            this.btnCloneSong.Name = "btnCloneSong";
            this.btnCloneSong.Size = new System.Drawing.Size(48, 23);
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
            // btnMoveSong
            // 
            this.btnMoveSong.Location = new System.Drawing.Point(204, 170);
            this.btnMoveSong.Name = "btnMoveSong";
            this.btnMoveSong.Size = new System.Drawing.Size(48, 23);
            this.btnMoveSong.TabIndex = 17;
            this.btnMoveSong.Text = "Move";
            this.btnMoveSong.UseVisualStyleBackColor = true;
            this.btnMoveSong.Click += new System.EventHandler(this.BtnMoveSong_Click);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(204, 52);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(48, 23);
            this.btnDown.TabIndex = 16;
            this.btnDown.Text = "v";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.BtnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(204, 23);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(48, 23);
            this.btnUp.TabIndex = 15;
            this.btnUp.Text = "^";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.BtnUp_Click);
            // 
            // btnSort
            // 
            this.btnSort.Location = new System.Drawing.Point(204, 105);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(48, 23);
            this.btnSort.TabIndex = 14;
            this.btnSort.Text = "Sort";
            this.btnSort.UseVisualStyleBackColor = true;
            this.btnSort.Click += new System.EventHandler(this.BtnSort_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(204, 260);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(48, 23);
            this.btnDel.TabIndex = 13;
            this.btnDel.Text = "Remove";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.BtnDel_Click);
            // 
            // menuTool
            // 
            this.menuTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnWebsite,
            this.btnGitLab,
            this.btnSteam,
            this.toolStripSeparator4,
            this.btnAbout,
            this.btnQuit});
            this.menuTool.Name = "menuTool";
            this.menuTool.Size = new System.Drawing.Size(46, 20);
            this.menuTool.Text = "Tools";
            // 
            // btnWebsite
            // 
            this.btnWebsite.Name = "btnWebsite";
            this.btnWebsite.Size = new System.Drawing.Size(232, 22);
            this.btnWebsite.Text = "Website";
            this.btnWebsite.Click += new System.EventHandler(this.BtnWebsite_Click);
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(229, 6);
            // 
            // btnAbout
            // 
            this.btnAbout.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMSCMMAbout,
            this.btnFFmpegLicense});
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(232, 22);
            this.btnAbout.Text = "About";
            // 
            // btnMSCMMAbout
            // 
            this.btnMSCMMAbout.Name = "btnMSCMMAbout";
            this.btnMSCMMAbout.Size = new System.Drawing.Size(184, 22);
            this.btnMSCMMAbout.Text = "MSC Music Manager";
            this.btnMSCMMAbout.Click += new System.EventHandler(this.MSCOGGToolStripMenuItem_Click);
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
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Size = new System.Drawing.Size(61, 20);
            this.menuSettings.Text = "Settings";
            this.menuSettings.Click += new System.EventHandler(this.MenuSettings_Click);
            // 
            // btnLaunchGame
            // 
            this.btnLaunchGame.Name = "btnLaunchGame";
            this.btnLaunchGame.Size = new System.Drawing.Size(92, 20);
            this.btnLaunchGame.Text = "Launch Game";
            this.btnLaunchGame.Click += new System.EventHandler(this.LaunchTheGameToolStripMenuItem_Click);
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.White;
            this.menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTool,
            this.menuSettings,
            this.btnLaunchGame,
            this.btnHelp,
            this.btnDonate,
            this.btnReportIssue,
            this.btnDownloadUpdate});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menu.Size = new System.Drawing.Size(745, 24);
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
            // btnDonate
            // 
            this.btnDonate.Name = "btnDonate";
            this.btnDonate.Size = new System.Drawing.Size(121, 20);
            this.btnDonate.Text = "Support the project";
            this.btnDonate.Click += new System.EventHandler(this.btnDonate_Click);
            // 
            // btnReportIssue
            // 
            this.btnReportIssue.Name = "btnReportIssue";
            this.btnReportIssue.Size = new System.Drawing.Size(94, 20);
            this.btnReportIssue.Text = "Report an Bug";
            this.btnReportIssue.Click += new System.EventHandler(this.btnReportIssue_Click);
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
            // tabs
            // 
            this.tabs.Controls.Add(this.tabLog);
            this.tabs.Controls.Add(this.tabDownload);
            this.tabs.Controls.Add(this.tabMeta);
            this.tabs.Controls.Add(this.tabCoverArt);
            this.tabs.Controls.Add(this.tabRecycle);
            this.tabs.HotTrack = true;
            this.tabs.Location = new System.Drawing.Point(255, 27);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(490, 362);
            this.tabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabs.TabIndex = 1;
            this.tabs.SelectedIndexChanged += new System.EventHandler(this.TabControl1_SelectedIndexChanged);
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
            this.tabLog.Size = new System.Drawing.Size(482, 336);
            this.tabLog.TabIndex = 0;
            this.tabLog.Text = "Log";
            // 
            // tabDownload
            // 
            this.tabDownload.BackColor = System.Drawing.Color.White;
            this.tabDownload.Controls.Add(this.labProgress);
            this.tabDownload.Controls.Add(this.proYt);
            this.tabDownload.Controls.Add(this.ytdlOutput);
            this.tabDownload.Controls.Add(this.btnCancelDownload);
            this.tabDownload.Controls.Add(this.label6);
            this.tabDownload.Controls.Add(this.btnDownload);
            this.tabDownload.Controls.Add(this.label5);
            this.tabDownload.Controls.Add(this.txtboxVideo);
            this.tabDownload.Location = new System.Drawing.Point(4, 22);
            this.tabDownload.Name = "tabDownload";
            this.tabDownload.Padding = new System.Windows.Forms.Padding(3);
            this.tabDownload.Size = new System.Drawing.Size(482, 336);
            this.tabDownload.TabIndex = 1;
            this.tabDownload.Text = "Download";
            // 
            // labProgress
            // 
            this.labProgress.AutoSize = true;
            this.labProgress.Location = new System.Drawing.Point(6, 128);
            this.labProgress.Name = "labProgress";
            this.labProgress.Size = new System.Drawing.Size(122, 13);
            this.labProgress.TabIndex = 21;
            this.labProgress.Text = "0% 0.00 KiB/s ETA 0:00";
            // 
            // proYt
            // 
            this.proYt.Location = new System.Drawing.Point(8, 102);
            this.proYt.Name = "proYt";
            this.proYt.Size = new System.Drawing.Size(466, 23);
            this.proYt.TabIndex = 20;
            // 
            // ytdlOutput
            // 
            this.ytdlOutput.BackColor = System.Drawing.SystemColors.Control;
            this.ytdlOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ytdlOutput.ContextMenuStrip = this.defaultContext;
            this.ytdlOutput.Location = new System.Drawing.Point(8, 145);
            this.ytdlOutput.Multiline = true;
            this.ytdlOutput.Name = "ytdlOutput";
            this.ytdlOutput.ReadOnly = true;
            this.ytdlOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ytdlOutput.Size = new System.Drawing.Size(466, 142);
            this.ytdlOutput.TabIndex = 17;
            // 
            // btnCancelDownload
            // 
            this.btnCancelDownload.Enabled = false;
            this.btnCancelDownload.Location = new System.Drawing.Point(111, 53);
            this.btnCancelDownload.Name = "btnCancelDownload";
            this.btnCancelDownload.Size = new System.Drawing.Size(99, 36);
            this.btnCancelDownload.TabIndex = 16;
            this.btnCancelDownload.Text = "Cancel";
            this.btnCancelDownload.UseVisualStyleBackColor = true;
            this.btnCancelDownload.Click += new System.EventHandler(this.BtnCancelDownload_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 301);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(405, 26);
            this.label6.TabIndex = 15;
            this.label6.Text = "Note:\r\nThe autor of this tool doesn\'t take any responsibility for the way how tha" +
    "t tool is used.";
            // 
            // btnDownload
            // 
            this.btnDownload.Enabled = false;
            this.btnDownload.Location = new System.Drawing.Point(6, 53);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(99, 36);
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
            this.txtboxVideo.ContextMenuStrip = this.defaultContext;
            this.txtboxVideo.Location = new System.Drawing.Point(6, 27);
            this.txtboxVideo.Name = "txtboxVideo";
            this.txtboxVideo.Size = new System.Drawing.Size(469, 20);
            this.txtboxVideo.TabIndex = 1;
            this.txtboxVideo.TextChanged += new System.EventHandler(this.txtboxVideo_TextChanged);
            this.txtboxVideo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtboxVideo_KeyDown);
            // 
            // tabMeta
            // 
            this.tabMeta.Controls.Add(this.label3);
            this.tabMeta.Controls.Add(this.btnOpenWithAudacity);
            this.tabMeta.Controls.Add(this.label2);
            this.tabMeta.Controls.Add(this.btnSetName);
            this.tabMeta.Controls.Add(this.label1);
            this.tabMeta.Controls.Add(this.txtSongName);
            this.tabMeta.Location = new System.Drawing.Point(4, 22);
            this.tabMeta.Name = "tabMeta";
            this.tabMeta.Size = new System.Drawing.Size(482, 336);
            this.tabMeta.TabIndex = 2;
            this.tabMeta.Text = "Edit";
            this.tabMeta.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Modify the song:";
            // 
            // btnOpenWithAudacity
            // 
            this.btnOpenWithAudacity.Location = new System.Drawing.Point(6, 158);
            this.btnOpenWithAudacity.Name = "btnOpenWithAudacity";
            this.btnOpenWithAudacity.Size = new System.Drawing.Size(99, 36);
            this.btnOpenWithAudacity.TabIndex = 19;
            this.btnOpenWithAudacity.Text = "Edit with Audacity";
            this.btnOpenWithAudacity.UseVisualStyleBackColor = true;
            this.btnOpenWithAudacity.Click += new System.EventHandler(this.button1_Click);
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
            this.btnSetName.Enabled = false;
            this.btnSetName.Location = new System.Drawing.Point(6, 74);
            this.btnSetName.Name = "btnSetName";
            this.btnSetName.Size = new System.Drawing.Size(99, 36);
            this.btnSetName.TabIndex = 17;
            this.btnSetName.Text = "Save";
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
            this.txtSongName.ContextMenuStrip = this.defaultContext;
            this.txtSongName.Location = new System.Drawing.Point(6, 48);
            this.txtSongName.Name = "txtSongName";
            this.txtSongName.Size = new System.Drawing.Size(469, 20);
            this.txtSongName.TabIndex = 2;
            this.txtSongName.TextChanged += new System.EventHandler(this.txtSongName_TextChanged);
            this.txtSongName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSongName_KeyDown);
            // 
            // tabCoverArt
            // 
            this.tabCoverArt.Controls.Add(this.labImageInfo);
            this.tabCoverArt.Controls.Add(this.picCoverArt);
            this.tabCoverArt.Controls.Add(this.label7);
            this.tabCoverArt.Controls.Add(this.btnCoverArtImage);
            this.tabCoverArt.Controls.Add(this.labCurrentFont);
            this.tabCoverArt.Controls.Add(this.label4);
            this.tabCoverArt.Controls.Add(this.txtCdText);
            this.tabCoverArt.Controls.Add(this.btnSelectFont);
            this.tabCoverArt.Controls.Add(this.btnCreateCoverArt);
            this.tabCoverArt.Location = new System.Drawing.Point(4, 22);
            this.tabCoverArt.Margin = new System.Windows.Forms.Padding(2);
            this.tabCoverArt.Name = "tabCoverArt";
            this.tabCoverArt.Size = new System.Drawing.Size(482, 336);
            this.tabCoverArt.TabIndex = 4;
            this.tabCoverArt.Text = "Cover Art";
            this.tabCoverArt.UseVisualStyleBackColor = true;
            // 
            // labImageInfo
            // 
            this.labImageInfo.AutoSize = true;
            this.labImageInfo.Location = new System.Drawing.Point(238, 291);
            this.labImageInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labImageInfo.Name = "labImageInfo";
            this.labImageInfo.Size = new System.Drawing.Size(80, 26);
            this.labImageInfo.TabIndex = 37;
            this.labImageInfo.Text = "Resolution: 0x0\r\nScale: 0x";
            this.labImageInfo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // picCoverArt
            // 
            this.picCoverArt.Location = new System.Drawing.Point(340, 176);
            this.picCoverArt.Margin = new System.Windows.Forms.Padding(2);
            this.picCoverArt.Name = "picCoverArt";
            this.picCoverArt.Size = new System.Drawing.Size(135, 146);
            this.picCoverArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCoverArt.TabIndex = 36;
            this.picCoverArt.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(112, 115);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(314, 13);
            this.label7.TabIndex = 35;
            this.label7.Text = "Use the default cover art image with no text added for best result.";
            // 
            // btnCoverArtImage
            // 
            this.btnCoverArtImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.btnCoverArtImage.Location = new System.Drawing.Point(8, 106);
            this.btnCoverArtImage.Margin = new System.Windows.Forms.Padding(2);
            this.btnCoverArtImage.Name = "btnCoverArtImage";
            this.btnCoverArtImage.Size = new System.Drawing.Size(99, 36);
            this.btnCoverArtImage.TabIndex = 34;
            this.btnCoverArtImage.Text = "No CD cover found";
            this.btnCoverArtImage.UseVisualStyleBackColor = true;
            this.btnCoverArtImage.Click += new System.EventHandler(this.btnCoverArtImage_Click);
            // 
            // labCurrentFont
            // 
            this.labCurrentFont.AutoSize = true;
            this.labCurrentFont.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labCurrentFont.Location = new System.Drawing.Point(113, 64);
            this.labCurrentFont.Name = "labCurrentFont";
            this.labCurrentFont.Size = new System.Drawing.Size(36, 16);
            this.labCurrentFont.TabIndex = 30;
            this.labCurrentFont.Text = "Arial";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Text on CD:";
            // 
            // txtCdText
            // 
            this.txtCdText.ContextMenuStrip = this.defaultContext;
            this.txtCdText.Location = new System.Drawing.Point(8, 24);
            this.txtCdText.MaxLength = 8;
            this.txtCdText.Name = "txtCdText";
            this.txtCdText.Size = new System.Drawing.Size(468, 20);
            this.txtCdText.TabIndex = 28;
            // 
            // btnSelectFont
            // 
            this.btnSelectFont.Location = new System.Drawing.Point(8, 53);
            this.btnSelectFont.Name = "btnSelectFont";
            this.btnSelectFont.Size = new System.Drawing.Size(99, 36);
            this.btnSelectFont.TabIndex = 27;
            this.btnSelectFont.Text = "Choose the font";
            this.btnSelectFont.UseVisualStyleBackColor = true;
            this.btnSelectFont.Click += new System.EventHandler(this.btnSelectFont_Click);
            // 
            // btnCreateCoverArt
            // 
            this.btnCreateCoverArt.Location = new System.Drawing.Point(8, 158);
            this.btnCreateCoverArt.Name = "btnCreateCoverArt";
            this.btnCreateCoverArt.Size = new System.Drawing.Size(99, 36);
            this.btnCreateCoverArt.TabIndex = 16;
            this.btnCreateCoverArt.Text = "Create new cover art";
            this.btnCreateCoverArt.UseVisualStyleBackColor = true;
            this.btnCreateCoverArt.Click += new System.EventHandler(this.btnCreateCoverArt_Click);
            // 
            // tabRecycle
            // 
            this.tabRecycle.Controls.Add(this.btnEmptyAll);
            this.tabRecycle.Controls.Add(this.labRecycle);
            this.tabRecycle.Controls.Add(this.btnRestore);
            this.tabRecycle.Controls.Add(this.btnRecycleDelete);
            this.tabRecycle.Controls.Add(this.trashList);
            this.tabRecycle.Location = new System.Drawing.Point(4, 22);
            this.tabRecycle.Margin = new System.Windows.Forms.Padding(2);
            this.tabRecycle.Name = "tabRecycle";
            this.tabRecycle.Size = new System.Drawing.Size(482, 336);
            this.tabRecycle.TabIndex = 3;
            this.tabRecycle.Text = "Recycle Bin";
            this.tabRecycle.UseVisualStyleBackColor = true;
            // 
            // btnEmptyAll
            // 
            this.btnEmptyAll.Location = new System.Drawing.Point(117, 290);
            this.btnEmptyAll.Name = "btnEmptyAll";
            this.btnEmptyAll.Size = new System.Drawing.Size(99, 36);
            this.btnEmptyAll.TabIndex = 28;
            this.btnEmptyAll.Text = "Empty All";
            this.btnEmptyAll.UseVisualStyleBackColor = true;
            this.btnEmptyAll.Click += new System.EventHandler(this.BtnEmptyAll_Click);
            // 
            // labRecycle
            // 
            this.labRecycle.AutoSize = true;
            this.labRecycle.Location = new System.Drawing.Point(10, 5);
            this.labRecycle.Name = "labRecycle";
            this.labRecycle.Size = new System.Drawing.Size(104, 13);
            this.labRecycle.TabIndex = 27;
            this.labRecycle.Text = "Recycle bin is empty";
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(373, 291);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(99, 36);
            this.btnRestore.TabIndex = 26;
            this.btnRestore.Text = "Restore";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.BtnRestore_Click);
            // 
            // btnRecycleDelete
            // 
            this.btnRecycleDelete.Location = new System.Drawing.Point(12, 290);
            this.btnRecycleDelete.Name = "btnRecycleDelete";
            this.btnRecycleDelete.Size = new System.Drawing.Size(99, 36);
            this.btnRecycleDelete.TabIndex = 25;
            this.btnRecycleDelete.Text = "Delete";
            this.btnRecycleDelete.UseVisualStyleBackColor = true;
            this.btnRecycleDelete.Click += new System.EventHandler(this.BtnRecycleDelete_Click);
            // 
            // trashList
            // 
            this.trashList.FormattingEnabled = true;
            this.trashList.HorizontalScrollbar = true;
            this.trashList.Location = new System.Drawing.Point(12, 20);
            this.trashList.Name = "trashList";
            this.trashList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.trashList.Size = new System.Drawing.Size(461, 264);
            this.trashList.Sorted = true;
            this.trashList.TabIndex = 24;
            this.trashList.SelectedIndexChanged += new System.EventHandler(this.trashList_SelectedIndexChanged);
            // 
            // downloadProgress
            // 
            this.downloadProgress.Location = new System.Drawing.Point(386, 1);
            this.downloadProgress.Name = "downloadProgress";
            this.downloadProgress.Size = new System.Drawing.Size(356, 23);
            this.downloadProgress.TabIndex = 19;
            this.downloadProgress.Visible = false;
            // 
            // songListContext
            // 
            this.songListContext.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.songListContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextCopy,
            this.contextDelete,
            this.contextMove,
            this.contextAll});
            this.songListContext.Name = "songListContext";
            this.songListContext.Size = new System.Drawing.Size(165, 92);
            // 
            // contextCopy
            // 
            this.contextCopy.Name = "contextCopy";
            this.contextCopy.ShortcutKeyDisplayString = "Ctrl+C";
            this.contextCopy.Size = new System.Drawing.Size(164, 22);
            this.contextCopy.Text = "Clone";
            this.contextCopy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.contextCopy.Click += new System.EventHandler(this.ContextCopy_Click);
            // 
            // contextDelete
            // 
            this.contextDelete.Name = "contextDelete";
            this.contextDelete.ShortcutKeyDisplayString = "Delete";
            this.contextDelete.Size = new System.Drawing.Size(164, 22);
            this.contextDelete.Text = "Delete";
            this.contextDelete.Click += new System.EventHandler(this.ContextDelete_Click);
            // 
            // contextMove
            // 
            this.contextMove.Name = "contextMove";
            this.contextMove.ShortcutKeyDisplayString = "Ctrl+X";
            this.contextMove.Size = new System.Drawing.Size(164, 22);
            this.contextMove.Text = "Move";
            this.contextMove.Click += new System.EventHandler(this.ContextMove_Click);
            // 
            // contextAll
            // 
            this.contextAll.Name = "contextAll";
            this.contextAll.ShortcutKeyDisplayString = "Ctrl+A";
            this.contextAll.Size = new System.Drawing.Size(164, 22);
            this.contextAll.Text = "Select All";
            this.contextAll.Click += new System.EventHandler(this.ContextAll_Click);
            // 
            // fontDialog1
            // 
            this.fontDialog1.Font = new System.Drawing.Font("Arial", 10F);
            this.fontDialog1.MaxSize = 10;
            this.fontDialog1.MinSize = 10;
            this.fontDialog1.ShowEffects = false;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 385);
            this.Controls.Add(this.downloadProgress);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.btnOpenGameDir);
            this.Controls.Add(this.btnDirectory);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.dragDropPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(761, 424);
            this.Name = "Form1";
            this.Text = "MSC Music Manager";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.DragLeave += new System.EventHandler(this.Form1_DragLeave);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.defaultContext.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.dragDropPanel.ResumeLayout(false);
            this.dragDropPanel.PerformLayout();
            this.tabs.ResumeLayout(false);
            this.tabLog.ResumeLayout(false);
            this.tabLog.PerformLayout();
            this.tabDownload.ResumeLayout(false);
            this.tabDownload.PerformLayout();
            this.tabMeta.ResumeLayout(false);
            this.tabMeta.PerformLayout();
            this.tabCoverArt.ResumeLayout(false);
            this.tabCoverArt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCoverArt)).EndInit();
            this.tabRecycle.ResumeLayout(false);
            this.tabRecycle.PerformLayout();
            this.songListContext.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox logOutput;
        private System.Windows.Forms.Button btnPlaySong;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnSort;
        private System.Windows.Forms.Button btnMoveSong;
        private System.Windows.Forms.ToolStripMenuItem menuTool;
        private System.Windows.Forms.ToolStripMenuItem btnGitLab;
        private System.Windows.Forms.ToolStripMenuItem btnSteam;
        private System.Windows.Forms.ToolStripMenuItem btnAbout;
        private System.Windows.Forms.ToolStripMenuItem btnMSCMMAbout;
        private System.Windows.Forms.ToolStripMenuItem btnFFmpegLicense;
        private System.Windows.Forms.ToolStripMenuItem btnQuit;
        private System.Windows.Forms.ToolStripMenuItem menuSettings;
        private System.Windows.Forms.ToolStripMenuItem btnLaunchGame;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem btnDownloadUpdate;
        private System.Windows.Forms.ToolStripMenuItem downloadUpdateNowToolStripMenuItem;
        private System.Windows.Forms.Button btnOpenGameDir;
        private System.Windows.Forms.Button btnDirectory;
        private System.Windows.Forms.Label labelConvert;
        private System.Windows.Forms.Panel dragDropPanel;
        private System.Windows.Forms.Label labNowPlaying;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.TabPage tabDownload;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtboxVideo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripMenuItem btnHelp;
        private System.Windows.Forms.Label labCounter;
        private System.Windows.Forms.TabPage tabMeta;
        private System.Windows.Forms.Button btnSetName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSongName;
        private System.Windows.Forms.Button btnCloneSong;
        private System.Windows.Forms.Button btnShuffle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ProgressBar downloadProgress;
        public System.Windows.Forms.ListBox songList;
        private System.Windows.Forms.ToolStripMenuItem btnWebsite;
        private System.Windows.Forms.Button btnCancelDownload;
        private System.Windows.Forms.ComboBox selectedFolder;
        private System.Windows.Forms.ContextMenuStrip songListContext;
        private System.Windows.Forms.ToolStripMenuItem contextCopy;
        private System.Windows.Forms.ToolStripMenuItem contextAll;
        private System.Windows.Forms.ToolStripMenuItem contextDelete;
        private System.Windows.Forms.ToolStripMenuItem contextMove;
        private System.Windows.Forms.TextBox ytdlOutput;
        private System.Windows.Forms.Label labProgress;
        private System.Windows.Forms.ProgressBar proYt;
        private System.Windows.Forms.TabPage tabRecycle;
        private System.Windows.Forms.Button btnRecycleDelete;
        public System.Windows.Forms.ListBox trashList;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Label labRecycle;
        private System.Windows.Forms.Button btnEmptyAll;
        private System.Windows.Forms.ToolStripMenuItem btnDonate;
        private System.Windows.Forms.Button btnOpenWithAudacity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabCoverArt;
        private System.Windows.Forms.Button btnCreateCoverArt;
        private System.Windows.Forms.Button btnSelectFont;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCdText;
        private System.Windows.Forms.Label labCurrentFont;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnCoverArtImage;
        private System.Windows.Forms.PictureBox picCoverArt;
        private System.Windows.Forms.Label labImageInfo;
        private System.Windows.Forms.ToolStripMenuItem contextDefaultCopy;
        private System.Windows.Forms.ToolStripMenuItem contextDefaultSelectAll;
        private System.Windows.Forms.ContextMenuStrip defaultContext;
        private System.Windows.Forms.ToolStripMenuItem contextDefaultUndo;
        private System.Windows.Forms.ToolStripMenuItem contextDefaultPaste;
        private System.Windows.Forms.ToolStripMenuItem btnReportIssue;
    }
}

