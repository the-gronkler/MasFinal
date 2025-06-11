namespace GUI
{
    partial class ProveEligibilityForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.politiciansCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.submitProofButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // politiciansCheckedListBox
            // 
            this.politiciansCheckedListBox.FormattingEnabled = true;
            this.politiciansCheckedListBox.Location = new System.Drawing.Point(12, 41);
            this.politiciansCheckedListBox.Name = "politiciansCheckedListBox";
            this.politiciansCheckedListBox.Size = new System.Drawing.Size(359, 293);
            this.politiciansCheckedListBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(242, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select up to 3 politicians as proof:";
            // 
            // submitProofButton
            // 
            this.submitProofButton.Location = new System.Drawing.Point(245, 349);
            this.submitProofButton.Name = "submitProofButton";
            this.submitProofButton.Size = new System.Drawing.Size(126, 36);
            this.submitProofButton.TabIndex = 2;
            this.submitProofButton.Text = "Submit Proof";
            this.submitProofButton.UseVisualStyleBackColor = true;
            this.submitProofButton.Click += new System.EventHandler(this.submitProofButton_Click);
            // 
            // ProveEligibilityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 397);
            this.Controls.Add(this.submitProofButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.politiciansCheckedListBox);
            this.Name = "ProveEligibilityForm";
            this.Text = "Prove Eligibility";
            this.Load += new System.EventHandler(this.ProveEligibilityForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.CheckedListBox politiciansCheckedListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button submitProofButton;
    }
}