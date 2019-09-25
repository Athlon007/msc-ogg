namespace OggConverter
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
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.comboLang = new System.Windows.Forms.ComboBox();
            this.chkNoSteam = new System.Windows.Forms.CheckBox();
            this.chkShortcut = new System.Windows.Forms.CheckBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkNoMetafiles = new System.Windows.Forms.CheckBox();
            this.chkAutoSort = new System.Windows.Forms.CheckBox();
            this.chkRemoveSource = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnCheckYTDLUpdates = new System.Windows.Forms.Button();
            this.btnCheckUpdate = new System.Windows.Forms.Button();
            this.labVer = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbYoutubeDlUpdateFrequency = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radPreview = new System.Windows.Forms.RadioButton();
            this.radOfficial = new System.Windows.Forms.RadioButton();
            this.chkAutoUpdates = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
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
            this.tabPage4.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 450);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label2);
            this.tabPage4.Controls.Add(this.comboLang);
            this.tabPage4.Controls.Add(this.chkNoSteam);
            this.tabPage4.Controls.Add(this.chkShortcut);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(792, 421);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "General";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 27;
            this.label2.Text = "Language";
            // 
            // comboLang
            // 
            this.comboLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLang.FormattingEnabled = true;
            this.comboLang.Location = new System.Drawing.Point(29, 132);
            this.comboLang.Margin = new System.Windows.Forms.Padding(4);
            this.comboLang.Name = "comboLang";
            this.comboLang.Size = new System.Drawing.Size(160, 24);
            this.comboLang.TabIndex = 26;
            this.comboLang.SelectedIndexChanged += new System.EventHandler(this.ComboLang_SelectedIndexChanged);
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
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chkNoMetafiles);
            this.tabPage1.Controls.Add(this.chkAutoSort);
            this.tabPage1.Controls.Add(this.chkRemoveSource);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 421);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Files";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chkNoMetafiles
            // 
            this.chkNoMetafiles.AutoSize = true;
            this.chkNoMetafiles.Location = new System.Drawing.Point(29, 89);
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
            this.chkAutoSort.Location = new System.Drawing.Point(29, 62);
            this.chkAutoSort.Name = "chkAutoSort";
            this.chkAutoSort.Size = new System.Drawing.Size(191, 21);
            this.chkAutoSort.TabIndex = 1;
            this.chkAutoSort.Text = "Sort files after conversion";
            this.chkAutoSort.UseVisualStyleBackColor = true;
            this.chkAutoSort.Click += new System.EventHandler(this.ChkAutoSort_Click);
            // 
            // chkRemoveSource
            // 
            this.chkRemoveSource.AutoSize = true;
            this.chkRemoveSource.Location = new System.Drawing.Point(29, 35);
            this.chkRemoveSource.Name = "chkRemoveSource";
            this.chkRemoveSource.Size = new System.Drawing.Size(264, 21);
            this.chkRemoveSource.TabIndex = 0;
            this.chkRemoveSource.Text = "Remove source files after conversion";
            this.chkRemoveSource.UseVisualStyleBackColor = true;
            this.chkRemoveSource.Click += new System.EventHandler(this.ChkRemoveSource_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnCheckYTDLUpdates);
            this.tabPage2.Controls.Add(this.btnCheckUpdate);
            this.tabPage2.Controls.Add(this.labVer);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.cbYoutubeDlUpdateFrequency);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.radPreview);
            this.tabPage2.Controls.Add(this.radOfficial);
            this.tabPage2.Controls.Add(this.chkAutoUpdates);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 421);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Updates";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnCheckYTDLUpdates
            // 
            this.btnCheckYTDLUpdates.Location = new System.Drawing.Point(26, 268);
            this.btnCheckYTDLUpdates.Name = "btnCheckYTDLUpdates";
            this.btnCheckYTDLUpdates.Size = new System.Drawing.Size(132, 35);
            this.btnCheckYTDLUpdates.TabIndex = 28;
            this.btnCheckYTDLUpdates.Text = "Check for Update";
            this.btnCheckYTDLUpdates.UseVisualStyleBackColor = true;
            this.btnCheckYTDLUpdates.Click += new System.EventHandler(this.BtnCheckYTDLUpdates_ClickAsync);
            // 
            // btnCheckUpdate
            // 
            this.btnCheckUpdate.Location = new System.Drawing.Point(29, 157);
            this.btnCheckUpdate.Name = "btnCheckUpdate";
            this.btnCheckUpdate.Size = new System.Drawing.Size(132, 35);
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
            this.labVer.Text = "Your Version: 0.0.0.0 \r\nInternal Version: 19405";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 216);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(198, 17);
            this.label3.TabIndex = 25;
            this.label3.Text = "Check for youtube-dl updates:";
            // 
            // cbYoutubeDlUpdateFrequency
            // 
            this.cbYoutubeDlUpdateFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbYoutubeDlUpdateFrequency.FormattingEnabled = true;
            this.cbYoutubeDlUpdateFrequency.Items.AddRange(new object[] {
            "Upon every start",
            "Daily",
            "Weekly",
            "Monthly",
            "Never"});
            this.cbYoutubeDlUpdateFrequency.Location = new System.Drawing.Point(29, 237);
            this.cbYoutubeDlUpdateFrequency.Margin = new System.Windows.Forms.Padding(4);
            this.cbYoutubeDlUpdateFrequency.Name = "cbYoutubeDlUpdateFrequency";
            this.cbYoutubeDlUpdateFrequency.Size = new System.Drawing.Size(160, 24);
            this.cbYoutubeDlUpdateFrequency.TabIndex = 24;
            this.cbYoutubeDlUpdateFrequency.SelectedIndexChanged += new System.EventHandler(this.CbYoutubeDlUpdateFrequency_SelectedIndexChanged);
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
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.btnDelLogs);
            this.tabPage3.Controls.Add(this.btnDelHis);
            this.tabPage3.Controls.Add(this.labNotice);
            this.tabPage3.Controls.Add(this.btLogFolder);
            this.tabPage3.Controls.Add(this.btnOpenLog);
            this.tabPage3.Controls.Add(this.btnOpenHistory);
            this.tabPage3.Controls.Add(this.chkHistory);
            this.tabPage3.Controls.Add(this.chkCrashLog);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(792, 421);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Logging & Privacy";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 168);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 17);
            this.label5.TabIndex = 32;
            this.label5.Text = "Logs";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 17);
            this.label4.TabIndex = 31;
            this.label4.Text = "History";
            // 
            // btnDelLogs
            // 
            this.btnDelLogs.Location = new System.Drawing.Point(305, 188);
            this.btnDelLogs.Name = "btnDelLogs";
            this.btnDelLogs.Size = new System.Drawing.Size(132, 35);
            this.btnDelLogs.TabIndex = 30;
            this.btnDelLogs.Text = "Delete all logs";
            this.btnDelLogs.UseVisualStyleBackColor = true;
            this.btnDelLogs.Click += new System.EventHandler(this.BtnDelLogs_Click);
            // 
            // btnDelHis
            // 
            this.btnDelHis.Location = new System.Drawing.Point(167, 118);
            this.btnDelHis.Name = "btnDelHis";
            this.btnDelHis.Size = new System.Drawing.Size(132, 35);
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
            this.btLogFolder.Location = new System.Drawing.Point(167, 188);
            this.btLogFolder.Name = "btLogFolder";
            this.btLogFolder.Size = new System.Drawing.Size(132, 35);
            this.btLogFolder.TabIndex = 5;
            this.btLogFolder.Text = "Open log folder";
            this.btLogFolder.UseVisualStyleBackColor = true;
            this.btLogFolder.Click += new System.EventHandler(this.BtLogFolder_Click);
            // 
            // btnOpenLog
            // 
            this.btnOpenLog.Location = new System.Drawing.Point(29, 188);
            this.btnOpenLog.Name = "btnOpenLog";
            this.btnOpenLog.Size = new System.Drawing.Size(132, 35);
            this.btnOpenLog.TabIndex = 4;
            this.btnOpenLog.Text = "Open last log";
            this.btnOpenLog.UseVisualStyleBackColor = true;
            this.btnOpenLog.Click += new System.EventHandler(this.BtnOpenLog_Click);
            // 
            // btnOpenHistory
            // 
            this.btnOpenHistory.Location = new System.Drawing.Point(29, 118);
            this.btnOpenHistory.Name = "btnOpenHistory";
            this.btnOpenHistory.Size = new System.Drawing.Size(132, 35);
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
            this.tabControl.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox chkRemoveSource;
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
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.CheckBox chkNoSteam;
        private System.Windows.Forms.CheckBox chkShortcut;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboLang;
        private System.Windows.Forms.Button btnDelHis;
        private System.Windows.Forms.Button btnDelLogs;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}