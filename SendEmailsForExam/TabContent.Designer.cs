﻿
namespace SendEmailsForExam
{
    partial class TabContent
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlTo = new System.Windows.Forms.FlowLayoutPanel();
            this.btnDeleteTab = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pnlTo
            // 
            this.pnlTo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTo.AutoScroll = true;
            this.pnlTo.Location = new System.Drawing.Point(3, 33);
            this.pnlTo.Name = "pnlTo";
            this.pnlTo.Size = new System.Drawing.Size(802, 335);
            this.pnlTo.TabIndex = 12;
            // 
            // btnDeleteTab
            // 
            this.btnDeleteTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnDeleteTab.Location = new System.Drawing.Point(724, 2);
            this.btnDeleteTab.Name = "btnDeleteTab";
            this.btnDeleteTab.Size = new System.Drawing.Size(81, 27);
            this.btnDeleteTab.TabIndex = 13;
            this.btnDeleteTab.Text = "Delete Tab";
            this.btnDeleteTab.UseVisualStyleBackColor = false;
            this.btnDeleteTab.Click += new System.EventHandler(this.btnDeleteTab_Click);
            // 
            // TabContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDeleteTab);
            this.Controls.Add(this.pnlTo);
            this.Name = "TabContent";
            this.Size = new System.Drawing.Size(808, 371);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.FlowLayoutPanel pnlTo;
        private System.Windows.Forms.Button btnDeleteTab;
    }
}
