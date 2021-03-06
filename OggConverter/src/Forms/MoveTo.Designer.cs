﻿namespace OggConverter
{
    partial class MoveTo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveTo));
            this.btnApply = new System.Windows.Forms.Button();
            this.selectedFolder = new System.Windows.Forms.ComboBox();
            this.strMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(184, 38);
            this.btnApply.Margin = new System.Windows.Forms.Padding(4);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(100, 28);
            this.btnApply.TabIndex = 28;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.BtnApply_Click);
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
            this.selectedFolder.Location = new System.Drawing.Point(15, 39);
            this.selectedFolder.Margin = new System.Windows.Forms.Padding(4);
            this.selectedFolder.Name = "selectedFolder";
            this.selectedFolder.Size = new System.Drawing.Size(160, 24);
            this.selectedFolder.TabIndex = 27;
            this.selectedFolder.SelectedIndexChanged += new System.EventHandler(this.SelectedFolder_SelectedIndexChanged);
            // 
            // strMessage
            // 
            this.strMessage.AutoSize = true;
            this.strMessage.Location = new System.Drawing.Point(17, 10);
            this.strMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.strMessage.Name = "strMessage";
            this.strMessage.Size = new System.Drawing.Size(238, 17);
            this.strMessage.TabIndex = 26;
            this.strMessage.Text = "Where do you want to move {i} files?";
            this.strMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MoveTo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 78);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.selectedFolder);
            this.Controls.Add(this.strMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MoveTo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Move to";
            this.Load += new System.EventHandler(this.MoveTo_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MoveTo_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.ComboBox selectedFolder;
        private System.Windows.Forms.Label strMessage;
    }
}