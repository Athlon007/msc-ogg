namespace OggConverter
{
    partial class ErrorMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorMessage));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnMoreDetail = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnLog = new System.Windows.Forms.Button();
            this.logOutput = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.defaultContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextDefaultCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.contextDefaultSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.defaultContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.label1.Location = new System.Drawing.Point(181, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 48);
            this.label1.TabIndex = 0;
            this.label1.Text = "Uhh-ohh!";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(187, 92);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(422, 34);
            this.label2.TabIndex = 1;
            this.label2.Text = "An error has occured and the info has been saved to {fileName}. \r\nIf it happens a" +
    "gain, please send the log to the MSCMM developer.";
            // 
            // btnMoreDetail
            // 
            this.btnMoreDetail.Location = new System.Drawing.Point(16, 176);
            this.btnMoreDetail.Margin = new System.Windows.Forms.Padding(4);
            this.btnMoreDetail.Name = "btnMoreDetail";
            this.btnMoreDetail.Size = new System.Drawing.Size(156, 28);
            this.btnMoreDetail.TabIndex = 2;
            this.btnMoreDetail.Text = "Show More Detail";
            this.btnMoreDetail.UseVisualStyleBackColor = true;
            this.btnMoreDetail.Click += new System.EventHandler(this.BtnMoreDetail_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(504, 176);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(156, 28);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnLog
            // 
            this.btnLog.Location = new System.Drawing.Point(340, 176);
            this.btnLog.Margin = new System.Windows.Forms.Padding(4);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(156, 28);
            this.btnLog.TabIndex = 4;
            this.btnLog.Text = "Open Log";
            this.btnLog.UseVisualStyleBackColor = true;
            this.btnLog.Click += new System.EventHandler(this.BtnLog_Click);
            // 
            // logOutput
            // 
            this.logOutput.BackColor = System.Drawing.SystemColors.Control;
            this.logOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logOutput.ContextMenuStrip = this.defaultContext;
            this.logOutput.Location = new System.Drawing.Point(16, 212);
            this.logOutput.Margin = new System.Windows.Forms.Padding(4);
            this.logOutput.Multiline = true;
            this.logOutput.Name = "logOutput";
            this.logOutput.ReadOnly = true;
            this.logOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logOutput.Size = new System.Drawing.Size(643, 288);
            this.logOutput.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::OggConverter.Properties.Resources.err_emoji;
            this.pictureBox1.Location = new System.Drawing.Point(16, 15);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(147, 135);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(590, 15);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(70, 28);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // defaultContext
            // 
            this.defaultContext.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.defaultContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextDefaultCopy,
            this.contextDefaultSelectAll});
            this.defaultContext.Name = "songListContext";
            this.defaultContext.Size = new System.Drawing.Size(211, 80);
            // 
            // contextDefaultCopy
            // 
            this.contextDefaultCopy.Name = "contextDefaultCopy";
            this.contextDefaultCopy.ShortcutKeyDisplayString = "";
            this.contextDefaultCopy.Size = new System.Drawing.Size(210, 24);
            this.contextDefaultCopy.Text = "Copy";
            this.contextDefaultCopy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.contextDefaultCopy.Click += new System.EventHandler(this.contextDefaultCopy_Click);
            // 
            // contextDefaultSelectAll
            // 
            this.contextDefaultSelectAll.Name = "contextDefaultSelectAll";
            this.contextDefaultSelectAll.ShortcutKeyDisplayString = "";
            this.contextDefaultSelectAll.Size = new System.Drawing.Size(210, 24);
            this.contextDefaultSelectAll.Text = "Select All";
            this.contextDefaultSelectAll.Click += new System.EventHandler(this.contextDefaultSelectAll_Click);
            // 
            // ErrorMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 208);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.logOutput);
            this.Controls.Add(this.btnLog);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnMoreDetail);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ErrorMessage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Oopsie!";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.defaultContext.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnMoreDetail;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnLog;
        private System.Windows.Forms.TextBox logOutput;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ContextMenuStrip defaultContext;
        private System.Windows.Forms.ToolStripMenuItem contextDefaultCopy;
        private System.Windows.Forms.ToolStripMenuItem contextDefaultSelectAll;
    }
}