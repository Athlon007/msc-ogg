namespace OggConverter
{
    partial class QuickConvert
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuickConvert));
            this.btnRadio = new System.Windows.Forms.Button();
            this.btnCD = new System.Windows.Forms.Button();
            this.strMessage = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRadio
            // 
            this.btnRadio.Location = new System.Drawing.Point(12, 33);
            this.btnRadio.Name = "btnRadio";
            this.btnRadio.Size = new System.Drawing.Size(75, 23);
            this.btnRadio.TabIndex = 0;
            this.btnRadio.Text = "Radio";
            this.btnRadio.UseVisualStyleBackColor = true;
            // 
            // btnCD
            // 
            this.btnCD.Location = new System.Drawing.Point(133, 33);
            this.btnCD.Name = "btnCD";
            this.btnCD.Size = new System.Drawing.Size(75, 23);
            this.btnCD.TabIndex = 1;
            this.btnCD.Text = "CD";
            this.btnCD.UseVisualStyleBackColor = true;
            // 
            // strMessage
            // 
            this.strMessage.AutoSize = true;
            this.strMessage.Location = new System.Drawing.Point(12, 9);
            this.strMessage.Name = "strMessage";
            this.strMessage.Size = new System.Drawing.Size(197, 13);
            this.strMessage.TabIndex = 2;
            this.strMessage.Text = "Where do you want to convert {i} file(s)?";
            this.strMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(73, 33);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Visible = false;
            // 
            // QuickConvert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(220, 63);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.strMessage);
            this.Controls.Add(this.btnCD);
            this.Controls.Add(this.btnRadio);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuickConvert";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MSCMM - Quick Convert";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRadio;
        private System.Windows.Forms.Button btnCD;
        private System.Windows.Forms.Label strMessage;
        private System.Windows.Forms.Button btnExit;
    }
}