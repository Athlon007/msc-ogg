﻿namespace OggConverter
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.btnCreateCustomShortcut = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.chkTranslatorMode = new System.Windows.Forms.CheckBox();
            this.labTranslator = new System.Windows.Forms.Label();
            this.btnAudacity = new System.Windows.Forms.Button();
            this.labAudacity = new System.Windows.Forms.Label();
            this.txtAudacity = new System.Windows.Forms.TextBox();
            this.chkShowFfmpegOutput = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboLang = new System.Windows.Forms.ComboBox();
            this.chkNoSteam = new System.Windows.Forms.CheckBox();
            this.chkShortcut = new System.Windows.Forms.CheckBox();
            this.tabFiles = new System.Windows.Forms.TabPage();
            this.chkMono = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkRecommendedFrequency = new System.Windows.Forms.CheckBox();
            this.radQualityCompressed = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.radQualityAverage = new System.Windows.Forms.RadioButton();
            this.radQualityBest = new System.Windows.Forms.RadioButton();
            this.chkIgnoreLimits = new System.Windows.Forms.CheckBox();
            this.chkNoMetafiles = new System.Windows.Forms.CheckBox();
            this.chkAutoSort = new System.Windows.Forms.CheckBox();
            this.tabUpdates = new System.Windows.Forms.TabPage();
            this.txtChangelog = new System.Windows.Forms.RichTextBox();
            this.btnChangelogHistory = new System.Windows.Forms.Button();
            this.btnCheckYTDLUpdates = new System.Windows.Forms.Button();
            this.btnCheckUpdate = new System.Windows.Forms.Button();
            this.labVer = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbYoutubeDlUpdateFrequency = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radPreview = new System.Windows.Forms.RadioButton();
            this.radOfficial = new System.Windows.Forms.RadioButton();
            this.chkAutoUpdates = new System.Windows.Forms.CheckBox();
            this.tabLogging = new System.Windows.Forms.TabPage();
            this.chkCreateLocaleErrorLogs = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDelLogs = new System.Windows.Forms.Button();
            this.btnDelHis = new System.Windows.Forms.Button();
            this.labNotice = new System.Windows.Forms.Label();
            this.btLogFolder = new System.Windows.Forms.Button();
            this.btnOpenLog = new System.Windows.Forms.Button();
            this.btnOpenHistory = new System.Windows.Forms.Button();
            this.chkHistory = new System.Windows.Forms.CheckBox();
            this.chkCrashLog = new System.Windows.Forms.CheckBox();
            this.tabControl.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.tabFiles.SuspendLayout();
            this.tabUpdates.SuspendLayout();
            this.tabLogging.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabGeneral);
            this.tabControl.Controls.Add(this.tabFiles);
            this.tabControl.Controls.Add(this.tabUpdates);
            this.tabControl.Controls.Add(this.tabLogging);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 450);
            this.tabControl.TabIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.AutoScroll = true;
            this.tabGeneral.Controls.Add(this.label9);
            this.tabGeneral.Controls.Add(this.btnCreateCustomShortcut);
            this.tabGeneral.Controls.Add(this.label8);
            this.tabGeneral.Controls.Add(this.chkTranslatorMode);
            this.tabGeneral.Controls.Add(this.labTranslator);
            this.tabGeneral.Controls.Add(this.btnAudacity);
            this.tabGeneral.Controls.Add(this.labAudacity);
            this.tabGeneral.Controls.Add(this.txtAudacity);
            this.tabGeneral.Controls.Add(this.chkShowFfmpegOutput);
            this.tabGeneral.Controls.Add(this.label2);
            this.tabGeneral.Controls.Add(this.comboLang);
            this.tabGeneral.Controls.Add(this.chkNoSteam);
            this.tabGeneral.Controls.Add(this.chkShortcut);
            this.tabGeneral.Location = new System.Drawing.Point(4, 25);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Size = new System.Drawing.Size(792, 421);
            this.tabGeneral.TabIndex = 3;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 371);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(365, 34);
            this.label9.TabIndex = 36;
            this.label9.Text = "Creates a custom game shortcut that first starts MSCMM \r\nand shuffles all songs, " +
    "then starts the game.";
            // 
            // btnCreateCustomShortcut
            // 
            this.btnCreateCustomShortcut.Location = new System.Drawing.Point(30, 324);
            this.btnCreateCustomShortcut.Name = "btnCreateCustomShortcut";
            this.btnCreateCustomShortcut.Size = new System.Drawing.Size(132, 44);
            this.btnCreateCustomShortcut.TabIndex = 35;
            this.btnCreateCustomShortcut.Text = "Create";
            this.btnCreateCustomShortcut.UseVisualStyleBackColor = true;
            this.btnCreateCustomShortcut.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 300);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(172, 17);
            this.label8.TabIndex = 34;
            this.label8.Text = "Shuffle and Play Shortcut:";
            // 
            // chkTranslatorMode
            // 
            this.chkTranslatorMode.AutoSize = true;
            this.chkTranslatorMode.Location = new System.Drawing.Point(29, 221);
            this.chkTranslatorMode.Name = "chkTranslatorMode";
            this.chkTranslatorMode.Size = new System.Drawing.Size(134, 21);
            this.chkTranslatorMode.TabIndex = 33;
            this.chkTranslatorMode.Text = "Translator Mode";
            this.chkTranslatorMode.UseVisualStyleBackColor = true;
            this.chkTranslatorMode.Click += new System.EventHandler(this.chkTranslatorMode_Click);
            // 
            // labTranslator
            // 
            this.labTranslator.AutoSize = true;
            this.labTranslator.Location = new System.Drawing.Point(27, 163);
            this.labTranslator.Name = "labTranslator";
            this.labTranslator.Size = new System.Drawing.Size(77, 34);
            this.labTranslator.TabIndex = 32;
            this.labTranslator.Text = "Translator:\r\n{0}";
            // 
            // btnAudacity
            // 
            this.btnAudacity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnAudacity.Location = new System.Drawing.Point(454, 433);
            this.btnAudacity.Name = "btnAudacity";
            this.btnAudacity.Size = new System.Drawing.Size(44, 44);
            this.btnAudacity.TabIndex = 31;
            this.btnAudacity.Text = "...";
            this.btnAudacity.UseVisualStyleBackColor = true;
            this.btnAudacity.Click += new System.EventHandler(this.btnAudacity_Click);
            // 
            // labAudacity
            // 
            this.labAudacity.AutoSize = true;
            this.labAudacity.Location = new System.Drawing.Point(26, 424);
            this.labAudacity.Name = "labAudacity";
            this.labAudacity.Size = new System.Drawing.Size(139, 17);
            this.labAudacity.TabIndex = 30;
            this.labAudacity.Text = "Audacity Executable:";
            // 
            // txtAudacity
            // 
            this.txtAudacity.Location = new System.Drawing.Point(28, 444);
            this.txtAudacity.Name = "txtAudacity";
            this.txtAudacity.ReadOnly = true;
            this.txtAudacity.Size = new System.Drawing.Size(420, 22);
            this.txtAudacity.TabIndex = 29;
            // 
            // chkShowFfmpegOutput
            // 
            this.chkShowFfmpegOutput.AutoSize = true;
            this.chkShowFfmpegOutput.Location = new System.Drawing.Point(29, 248);
            this.chkShowFfmpegOutput.Name = "chkShowFfmpegOutput";
            this.chkShowFfmpegOutput.Size = new System.Drawing.Size(155, 21);
            this.chkShowFfmpegOutput.TabIndex = 28;
            this.chkShowFfmpegOutput.Text = "Show ffmpeg output";
            this.chkShowFfmpegOutput.UseVisualStyleBackColor = true;
            this.chkShowFfmpegOutput.Click += new System.EventHandler(this.ChkShowFfmpegOutput_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 27;
            this.label2.Text = "Language";
            // 
            // comboLang
            // 
            this.comboLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLang.FormattingEnabled = true;
            this.comboLang.Location = new System.Drawing.Point(29, 127);
            this.comboLang.Margin = new System.Windows.Forms.Padding(4);
            this.comboLang.Name = "comboLang";
            this.comboLang.Size = new System.Drawing.Size(160, 24);
            this.comboLang.TabIndex = 26;
            this.comboLang.SelectionChangeCommitted += new System.EventHandler(this.ComboLang_SelectionChangeCommitted);
            // 
            // chkNoSteam
            // 
            this.chkNoSteam.AutoSize = true;
            this.chkNoSteam.Location = new System.Drawing.Point(29, 62);
            this.chkNoSteam.Name = "chkNoSteam";
            this.chkNoSteam.Size = new System.Drawing.Size(215, 21);
            this.chkNoSteam.TabIndex = 7;
            this.chkNoSteam.Text = "Start the game without Steam";
            this.chkNoSteam.UseVisualStyleBackColor = true;
            this.chkNoSteam.Click += new System.EventHandler(this.ChkNoSteam_Click);
            // 
            // chkShortcut
            // 
            this.chkShortcut.AutoSize = true;
            this.chkShortcut.Location = new System.Drawing.Point(29, 35);
            this.chkShortcut.Name = "chkShortcut";
            this.chkShortcut.Size = new System.Drawing.Size(137, 21);
            this.chkShortcut.TabIndex = 6;
            this.chkShortcut.Text = "Desktop shortcut";
            this.chkShortcut.UseVisualStyleBackColor = true;
            this.chkShortcut.Click += new System.EventHandler(this.ChkShortcut_Click);
            // 
            // tabFiles
            // 
            this.tabFiles.Controls.Add(this.chkMono);
            this.tabFiles.Controls.Add(this.label7);
            this.tabFiles.Controls.Add(this.chkRecommendedFrequency);
            this.tabFiles.Controls.Add(this.radQualityCompressed);
            this.tabFiles.Controls.Add(this.label6);
            this.tabFiles.Controls.Add(this.radQualityAverage);
            this.tabFiles.Controls.Add(this.radQualityBest);
            this.tabFiles.Controls.Add(this.chkIgnoreLimits);
            this.tabFiles.Controls.Add(this.chkNoMetafiles);
            this.tabFiles.Controls.Add(this.chkAutoSort);
            this.tabFiles.Location = new System.Drawing.Point(4, 25);
            this.tabFiles.Name = "tabFiles";
            this.tabFiles.Padding = new System.Windows.Forms.Padding(3);
            this.tabFiles.Size = new System.Drawing.Size(792, 421);
            this.tabFiles.TabIndex = 0;
            this.tabFiles.Text = "Files";
            this.tabFiles.UseVisualStyleBackColor = true;
            // 
            // chkMono
            // 
            this.chkMono.AutoSize = true;
            this.chkMono.Location = new System.Drawing.Point(29, 313);
            this.chkMono.Name = "chkMono";
            this.chkMono.Size = new System.Drawing.Size(223, 21);
            this.chkMono.TabIndex = 29;
            this.chkMono.Text = "Convert song to mono channel";
            this.chkMono.UseVisualStyleBackColor = true;
            this.chkMono.Click += new System.EventHandler(this.chkMono_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 266);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 17);
            this.label7.TabIndex = 28;
            this.label7.Text = "Conversion:";
            // 
            // chkRecommendedFrequency
            // 
            this.chkRecommendedFrequency.AutoSize = true;
            this.chkRecommendedFrequency.Location = new System.Drawing.Point(29, 286);
            this.chkRecommendedFrequency.Name = "chkRecommendedFrequency";
            this.chkRecommendedFrequency.Size = new System.Drawing.Size(413, 21);
            this.chkRecommendedFrequency.TabIndex = 27;
            this.chkRecommendedFrequency.Text = "Set the music frequency to recomended 22050 Hz frequency";
            this.chkRecommendedFrequency.UseVisualStyleBackColor = true;
            this.chkRecommendedFrequency.Click += new System.EventHandler(this.chkRecommendedFrequency_Click);
            // 
            // radQualityCompressed
            // 
            this.radQualityCompressed.AutoSize = true;
            this.radQualityCompressed.Location = new System.Drawing.Point(29, 210);
            this.radQualityCompressed.Name = "radQualityCompressed";
            this.radQualityCompressed.Size = new System.Drawing.Size(108, 21);
            this.radQualityCompressed.TabIndex = 7;
            this.radQualityCompressed.TabStop = true;
            this.radQualityCompressed.Text = "Compressed";
            this.radQualityCompressed.UseVisualStyleBackColor = true;
            this.radQualityCompressed.Click += new System.EventHandler(this.radQualityCompressed_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(162, 17);
            this.label6.TabIndex = 6;
            this.label6.Text = "Music Download Quality:";
            // 
            // radQualityAverage
            // 
            this.radQualityAverage.AutoSize = true;
            this.radQualityAverage.Location = new System.Drawing.Point(29, 183);
            this.radQualityAverage.Name = "radQualityAverage";
            this.radQualityAverage.Size = new System.Drawing.Size(82, 21);
            this.radQualityAverage.TabIndex = 5;
            this.radQualityAverage.TabStop = true;
            this.radQualityAverage.Text = "Average";
            this.radQualityAverage.UseVisualStyleBackColor = true;
            this.radQualityAverage.Click += new System.EventHandler(this.radQualityAverage_Click);
            // 
            // radQualityBest
            // 
            this.radQualityBest.AutoSize = true;
            this.radQualityBest.Location = new System.Drawing.Point(29, 156);
            this.radQualityBest.Name = "radQualityBest";
            this.radQualityBest.Size = new System.Drawing.Size(57, 21);
            this.radQualityBest.TabIndex = 4;
            this.radQualityBest.TabStop = true;
            this.radQualityBest.Text = "Best";
            this.radQualityBest.UseVisualStyleBackColor = true;
            this.radQualityBest.Click += new System.EventHandler(this.radQualityBest_Click);
            // 
            // chkIgnoreLimits
            // 
            this.chkIgnoreLimits.AutoSize = true;
            this.chkIgnoreLimits.Location = new System.Drawing.Point(29, 89);
            this.chkIgnoreLimits.Name = "chkIgnoreLimits";
            this.chkIgnoreLimits.Size = new System.Drawing.Size(232, 21);
            this.chkIgnoreLimits.TabIndex = 3;
            this.chkIgnoreLimits.Text = "Ignore song limit folder limitation";
            this.chkIgnoreLimits.UseVisualStyleBackColor = true;
            this.chkIgnoreLimits.Click += new System.EventHandler(this.chkIgnoreLimits_Click);
            // 
            // chkNoMetafiles
            // 
            this.chkNoMetafiles.AutoSize = true;
            this.chkNoMetafiles.Location = new System.Drawing.Point(29, 62);
            this.chkNoMetafiles.Name = "chkNoMetafiles";
            this.chkNoMetafiles.Size = new System.Drawing.Size(178, 21);
            this.chkNoMetafiles.TabIndex = 2;
            this.chkNoMetafiles.Text = "Don\'t save song names";
            this.chkNoMetafiles.UseVisualStyleBackColor = true;
            this.chkNoMetafiles.Click += new System.EventHandler(this.ChkNoMetafiles_Click);
            // 
            // chkAutoSort
            // 
            this.chkAutoSort.AutoSize = true;
            this.chkAutoSort.Location = new System.Drawing.Point(29, 35);
            this.chkAutoSort.Name = "chkAutoSort";
            this.chkAutoSort.Size = new System.Drawing.Size(240, 21);
            this.chkAutoSort.TabIndex = 1;
            this.chkAutoSort.Text = "Automatically rearrange file order";
            this.chkAutoSort.UseVisualStyleBackColor = true;
            this.chkAutoSort.Click += new System.EventHandler(this.ChkAutoSort_Click);
            // 
            // tabUpdates
            // 
            this.tabUpdates.Controls.Add(this.txtChangelog);
            this.tabUpdates.Controls.Add(this.btnChangelogHistory);
            this.tabUpdates.Controls.Add(this.btnCheckYTDLUpdates);
            this.tabUpdates.Controls.Add(this.btnCheckUpdate);
            this.tabUpdates.Controls.Add(this.labVer);
            this.tabUpdates.Controls.Add(this.label3);
            this.tabUpdates.Controls.Add(this.cbYoutubeDlUpdateFrequency);
            this.tabUpdates.Controls.Add(this.label1);
            this.tabUpdates.Controls.Add(this.radPreview);
            this.tabUpdates.Controls.Add(this.radOfficial);
            this.tabUpdates.Controls.Add(this.chkAutoUpdates);
            this.tabUpdates.Location = new System.Drawing.Point(4, 25);
            this.tabUpdates.Name = "tabUpdates";
            this.tabUpdates.Padding = new System.Windows.Forms.Padding(3);
            this.tabUpdates.Size = new System.Drawing.Size(792, 421);
            this.tabUpdates.TabIndex = 1;
            this.tabUpdates.Text = "Updates";
            this.tabUpdates.UseVisualStyleBackColor = true;
            // 
            // txtChangelog
            // 
            this.txtChangelog.BackColor = System.Drawing.Color.White;
            this.txtChangelog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtChangelog.Location = new System.Drawing.Point(269, 88);
            this.txtChangelog.Name = "txtChangelog";
            this.txtChangelog.ReadOnly = true;
            this.txtChangelog.Size = new System.Drawing.Size(515, 325);
            this.txtChangelog.TabIndex = 30;
            this.txtChangelog.Text = "";
            this.txtChangelog.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtChangelog_LinkClicked);
            // 
            // btnChangelogHistory
            // 
            this.btnChangelogHistory.Location = new System.Drawing.Point(29, 207);
            this.btnChangelogHistory.Name = "btnChangelogHistory";
            this.btnChangelogHistory.Size = new System.Drawing.Size(132, 44);
            this.btnChangelogHistory.TabIndex = 29;
            this.btnChangelogHistory.Text = "View Changelog History";
            this.btnChangelogHistory.UseVisualStyleBackColor = true;
            this.btnChangelogHistory.Click += new System.EventHandler(this.btnChangelogHistory_Click);
            // 
            // btnCheckYTDLUpdates
            // 
            this.btnCheckYTDLUpdates.Location = new System.Drawing.Point(29, 336);
            this.btnCheckYTDLUpdates.Name = "btnCheckYTDLUpdates";
            this.btnCheckYTDLUpdates.Size = new System.Drawing.Size(132, 44);
            this.btnCheckYTDLUpdates.TabIndex = 28;
            this.btnCheckYTDLUpdates.Text = "Check for Update";
            this.btnCheckYTDLUpdates.UseVisualStyleBackColor = true;
            this.btnCheckYTDLUpdates.Click += new System.EventHandler(this.BtnCheckYTDLUpdates_ClickAsync);
            // 
            // btnCheckUpdate
            // 
            this.btnCheckUpdate.Location = new System.Drawing.Point(29, 157);
            this.btnCheckUpdate.Name = "btnCheckUpdate";
            this.btnCheckUpdate.Size = new System.Drawing.Size(132, 44);
            this.btnCheckUpdate.TabIndex = 27;
            this.btnCheckUpdate.Text = "Check for Update";
            this.btnCheckUpdate.UseVisualStyleBackColor = true;
            this.btnCheckUpdate.Click += new System.EventHandler(this.BtnCheckUpdate_Click);
            // 
            // labVer
            // 
            this.labVer.AutoSize = true;
            this.labVer.Location = new System.Drawing.Point(531, 35);
            this.labVer.Name = "labVer";
            this.labVer.Size = new System.Drawing.Size(155, 34);
            this.labVer.TabIndex = 26;
            this.labVer.Text = "Your Version: 0.0.0.0 \r\nInternal Version: 00000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 284);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(198, 17);
            this.label3.TabIndex = 25;
            this.label3.Text = "Check for youtube-dl updates:";
            // 
            // cbYoutubeDlUpdateFrequency
            // 
            this.cbYoutubeDlUpdateFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbYoutubeDlUpdateFrequency.FormattingEnabled = true;
            this.cbYoutubeDlUpdateFrequency.Location = new System.Drawing.Point(29, 305);
            this.cbYoutubeDlUpdateFrequency.Margin = new System.Windows.Forms.Padding(4);
            this.cbYoutubeDlUpdateFrequency.Name = "cbYoutubeDlUpdateFrequency";
            this.cbYoutubeDlUpdateFrequency.Size = new System.Drawing.Size(195, 24);
            this.cbYoutubeDlUpdateFrequency.TabIndex = 24;
            this.cbYoutubeDlUpdateFrequency.SelectionChangeCommitted += new System.EventHandler(this.CbYoutubeDlUpdateFrequency_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Update Channel:";
            // 
            // radPreview
            // 
            this.radPreview.AutoSize = true;
            this.radPreview.Location = new System.Drawing.Point(46, 115);
            this.radPreview.Name = "radPreview";
            this.radPreview.Size = new System.Drawing.Size(78, 21);
            this.radPreview.TabIndex = 2;
            this.radPreview.TabStop = true;
            this.radPreview.Text = "Preview";
            this.radPreview.UseVisualStyleBackColor = true;
            this.radPreview.Click += new System.EventHandler(this.RadPreview_Click);
            // 
            // radOfficial
            // 
            this.radOfficial.AutoSize = true;
            this.radOfficial.Location = new System.Drawing.Point(46, 88);
            this.radOfficial.Name = "radOfficial";
            this.radOfficial.Size = new System.Drawing.Size(72, 21);
            this.radOfficial.TabIndex = 1;
            this.radOfficial.TabStop = true;
            this.radOfficial.Text = "Official";
            this.radOfficial.UseVisualStyleBackColor = true;
            this.radOfficial.Click += new System.EventHandler(this.RadOfficial_Click);
            // 
            // chkAutoUpdates
            // 
            this.chkAutoUpdates.AutoSize = true;
            this.chkAutoUpdates.Location = new System.Drawing.Point(29, 35);
            this.chkAutoUpdates.Name = "chkAutoUpdates";
            this.chkAutoUpdates.Size = new System.Drawing.Size(219, 21);
            this.chkAutoUpdates.TabIndex = 0;
            this.chkAutoUpdates.Text = "Automatically look for updates";
            this.chkAutoUpdates.UseVisualStyleBackColor = true;
            this.chkAutoUpdates.Click += new System.EventHandler(this.ChkAutoUpdates_Click);
            // 
            // tabLogging
            // 
            this.tabLogging.Controls.Add(this.chkCreateLocaleErrorLogs);
            this.tabLogging.Controls.Add(this.label5);
            this.tabLogging.Controls.Add(this.label4);
            this.tabLogging.Controls.Add(this.btnDelLogs);
            this.tabLogging.Controls.Add(this.btnDelHis);
            this.tabLogging.Controls.Add(this.labNotice);
            this.tabLogging.Controls.Add(this.btLogFolder);
            this.tabLogging.Controls.Add(this.btnOpenLog);
            this.tabLogging.Controls.Add(this.btnOpenHistory);
            this.tabLogging.Controls.Add(this.chkHistory);
            this.tabLogging.Controls.Add(this.chkCrashLog);
            this.tabLogging.Location = new System.Drawing.Point(4, 25);
            this.tabLogging.Name = "tabLogging";
            this.tabLogging.Size = new System.Drawing.Size(792, 421);
            this.tabLogging.TabIndex = 2;
            this.tabLogging.Text = "Logging & Privacy";
            this.tabLogging.UseVisualStyleBackColor = true;
            // 
            // chkCreateLocaleErrorLogs
            // 
            this.chkCreateLocaleErrorLogs.AutoSize = true;
            this.chkCreateLocaleErrorLogs.Location = new System.Drawing.Point(29, 89);
            this.chkCreateLocaleErrorLogs.Name = "chkCreateLocaleErrorLogs";
            this.chkCreateLocaleErrorLogs.Size = new System.Drawing.Size(178, 21);
            this.chkCreateLocaleErrorLogs.TabIndex = 33;
            this.chkCreateLocaleErrorLogs.Text = "Create locale error logs";
            this.chkCreateLocaleErrorLogs.UseVisualStyleBackColor = true;
            this.chkCreateLocaleErrorLogs.Click += new System.EventHandler(this.chkCreateLocaleErrorLogs_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 200);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 17);
            this.label5.TabIndex = 32;
            this.label5.Text = "Logs";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 17);
            this.label4.TabIndex = 31;
            this.label4.Text = "History";
            // 
            // btnDelLogs
            // 
            this.btnDelLogs.Location = new System.Drawing.Point(305, 220);
            this.btnDelLogs.Name = "btnDelLogs";
            this.btnDelLogs.Size = new System.Drawing.Size(132, 44);
            this.btnDelLogs.TabIndex = 30;
            this.btnDelLogs.Text = "Delete all logs";
            this.btnDelLogs.UseVisualStyleBackColor = true;
            this.btnDelLogs.Click += new System.EventHandler(this.BtnDelLogs_Click);
            // 
            // btnDelHis
            // 
            this.btnDelHis.Location = new System.Drawing.Point(167, 150);
            this.btnDelHis.Name = "btnDelHis";
            this.btnDelHis.Size = new System.Drawing.Size(132, 44);
            this.btnDelHis.TabIndex = 29;
            this.btnDelHis.Text = "Delete history";
            this.btnDelHis.UseVisualStyleBackColor = true;
            this.btnDelHis.Click += new System.EventHandler(this.BtnDelHis_Click);
            // 
            // labNotice
            // 
            this.labNotice.AutoSize = true;
            this.labNotice.Location = new System.Drawing.Point(26, 370);
            this.labNotice.Name = "labNotice";
            this.labNotice.Size = new System.Drawing.Size(399, 34);
            this.labNotice.TabIndex = 27;
            this.labNotice.Text = "Notice: not a single log or any info is sent from your computer.\r\nEverything that" +
    "\'s being logged is saved on Your computer.";
            // 
            // btLogFolder
            // 
            this.btLogFolder.Location = new System.Drawing.Point(167, 220);
            this.btLogFolder.Name = "btLogFolder";
            this.btLogFolder.Size = new System.Drawing.Size(132, 44);
            this.btLogFolder.TabIndex = 5;
            this.btLogFolder.Text = "Open log folder";
            this.btLogFolder.UseVisualStyleBackColor = true;
            this.btLogFolder.Click += new System.EventHandler(this.BtLogFolder_Click);
            // 
            // btnOpenLog
            // 
            this.btnOpenLog.Location = new System.Drawing.Point(29, 220);
            this.btnOpenLog.Name = "btnOpenLog";
            this.btnOpenLog.Size = new System.Drawing.Size(132, 44);
            this.btnOpenLog.TabIndex = 4;
            this.btnOpenLog.Text = "Open last log";
            this.btnOpenLog.UseVisualStyleBackColor = true;
            this.btnOpenLog.Click += new System.EventHandler(this.BtnOpenLog_Click);
            // 
            // btnOpenHistory
            // 
            this.btnOpenHistory.Location = new System.Drawing.Point(29, 150);
            this.btnOpenHistory.Name = "btnOpenHistory";
            this.btnOpenHistory.Size = new System.Drawing.Size(132, 44);
            this.btnOpenHistory.TabIndex = 3;
            this.btnOpenHistory.Text = "Open history";
            this.btnOpenHistory.UseVisualStyleBackColor = true;
            this.btnOpenHistory.Click += new System.EventHandler(this.BtnOpenHistory_Click);
            // 
            // chkHistory
            // 
            this.chkHistory.AutoSize = true;
            this.chkHistory.Location = new System.Drawing.Point(29, 62);
            this.chkHistory.Name = "chkHistory";
            this.chkHistory.Size = new System.Drawing.Size(178, 21);
            this.chkHistory.TabIndex = 2;
            this.chkHistory.Text = "Save usage into history";
            this.chkHistory.UseVisualStyleBackColor = true;
            this.chkHistory.Click += new System.EventHandler(this.ChkHistory_Click);
            // 
            // chkCrashLog
            // 
            this.chkCrashLog.AutoSize = true;
            this.chkCrashLog.Location = new System.Drawing.Point(29, 35);
            this.chkCrashLog.Name = "chkCrashLog";
            this.chkCrashLog.Size = new System.Drawing.Size(174, 21);
            this.chkCrashLog.TabIndex = 1;
            this.chkCrashLog.Text = "Create logs after crash";
            this.chkCrashLog.UseVisualStyleBackColor = true;
            this.chkCrashLog.Click += new System.EventHandler(this.ChkCrashLog_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SettingsForm_KeyDown);
            this.tabControl.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.tabFiles.ResumeLayout(false);
            this.tabFiles.PerformLayout();
            this.tabUpdates.ResumeLayout(false);
            this.tabUpdates.PerformLayout();
            this.tabLogging.ResumeLayout(false);
            this.tabLogging.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabFiles;
        private System.Windows.Forms.TabPage tabUpdates;
        private System.Windows.Forms.TabPage tabLogging;
        private System.Windows.Forms.CheckBox chkAutoSort;
        private System.Windows.Forms.CheckBox chkNoMetafiles;
        private System.Windows.Forms.CheckBox chkAutoUpdates;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radPreview;
        private System.Windows.Forms.RadioButton radOfficial;
        private System.Windows.Forms.CheckBox chkCrashLog;
        private System.Windows.Forms.CheckBox chkHistory;
        private System.Windows.Forms.Button btnOpenLog;
        private System.Windows.Forms.Button btnOpenHistory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbYoutubeDlUpdateFrequency;
        private System.Windows.Forms.Button btLogFolder;
        private System.Windows.Forms.Label labVer;
        private System.Windows.Forms.Button btnCheckUpdate;
        private System.Windows.Forms.Button btnCheckYTDLUpdates;
        private System.Windows.Forms.Label labNotice;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.CheckBox chkNoSteam;
        private System.Windows.Forms.CheckBox chkShortcut;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboLang;
        private System.Windows.Forms.Button btnDelHis;
        private System.Windows.Forms.Button btnDelLogs;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkShowFfmpegOutput;
        private System.Windows.Forms.CheckBox chkIgnoreLimits;
        private System.Windows.Forms.Button btnChangelogHistory;
        private System.Windows.Forms.RadioButton radQualityCompressed;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton radQualityAverage;
        private System.Windows.Forms.RadioButton radQualityBest;
        private System.Windows.Forms.Button btnAudacity;
        private System.Windows.Forms.Label labAudacity;
        private System.Windows.Forms.TextBox txtAudacity;
        private System.Windows.Forms.RichTextBox txtChangelog;
        private System.Windows.Forms.CheckBox chkRecommendedFrequency;
        private System.Windows.Forms.CheckBox chkMono;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labTranslator;
        private System.Windows.Forms.CheckBox chkTranslatorMode;
        private System.Windows.Forms.Button btnCreateCustomShortcut;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkCreateLocaleErrorLogs;
    }
}