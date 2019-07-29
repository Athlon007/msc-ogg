namespace OggConverter
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
            this.btnApply.Location = new System.Drawing.Point(138, 31);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
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
            this.selectedFolder.Location = new System.Drawing.Point(11, 32);
            this.selectedFolder.Name = "selectedFolder";
            this.selectedFolder.Size = new System.Drawing.Size(121, 21);
            this.selectedFolder.TabIndex = 27;
            this.selectedFolder.SelectedIndexChanged += new System.EventHandler(this.SelectedFolder_SelectedIndexChanged);
            // 
            // strMessage
            // 
            this.strMessage.AutoSize = true;
            this.strMessage.Location = new System.Drawing.Point(13, 8);
            this.strMessage.Name = "strMessage";
            this.strMessage.Size = new System.Drawing.Size(181, 13);
            this.strMessage.TabIndex = 26;
            this.strMessage.Text = "Where do you want to move {i} files?";
            this.strMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MoveTo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(220, 63);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.selectedFolder);
            this.Controls.Add(this.strMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MoveTo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Move to";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.ComboBox selectedFolder;
        private System.Windows.Forms.Label strMessage;
    }
}