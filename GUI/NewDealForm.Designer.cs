namespace GUI
{
    partial class NewDealForm
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
            this.oligarchLabel = new System.Windows.Forms.Label();
            this.politicianLabel = new System.Windows.Forms.Label();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dealLevelUpDown = new System.Windows.Forms.NumericUpDown();
            this.submitDealButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dealLevelUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // oligarchLabel
            // 
            this.oligarchLabel.AutoSize = true;
            this.oligarchLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.oligarchLabel.Location = new System.Drawing.Point(12, 9);
            this.oligarchLabel.Name = "oligarchLabel";
            this.oligarchLabel.Size = new System.Drawing.Size(99, 28);
            this.oligarchLabel.TabIndex = 0;
            this.oligarchLabel.Text = "Oligarch:";
            // 
            // politicianLabel
            // 
            this.politicianLabel.AutoSize = true;
            this.politicianLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.politicianLabel.Location = new System.Drawing.Point(12, 37);
            this.politicianLabel.Name = "politicianLabel";
            this.politicianLabel.Size = new System.Drawing.Size(99, 28);
            this.politicianLabel.TabIndex = 1;
            this.politicianLabel.Text = "Politician:";
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(17, 102);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(462, 137);
            this.descriptionTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Deal Description";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 256);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Deal Level";
            // 
            // dealLevelUpDown
            // 
            this.dealLevelUpDown.Location = new System.Drawing.Point(95, 254);
            this.dealLevelUpDown.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            this.dealLevelUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.dealLevelUpDown.Name = "dealLevelUpDown";
            this.dealLevelUpDown.Size = new System.Drawing.Size(120, 22);
            this.dealLevelUpDown.TabIndex = 5;
            this.dealLevelUpDown.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // submitDealButton
            // 
            this.submitDealButton.Location = new System.Drawing.Point(369, 303);
            this.submitDealButton.Name = "submitDealButton";
            this.submitDealButton.Size = new System.Drawing.Size(110, 35);
            this.submitDealButton.TabIndex = 6;
            this.submitDealButton.Text = "Submit Deal";
            this.submitDealButton.UseVisualStyleBackColor = true;
            this.submitDealButton.Click += new System.EventHandler(this.submitDealButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(253, 303);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(110, 35);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // NewDealForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 354);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.submitDealButton);
            this.Controls.Add(this.dealLevelUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.politicianLabel);
            this.Controls.Add(this.oligarchLabel);
            this.Name = "NewDealForm";
            this.Text = "Propose New Deal";
            ((System.ComponentModel.ISupportInitialize)(this.dealLevelUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label oligarchLabel;
        private System.Windows.Forms.Label politicianLabel;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown dealLevelUpDown;
        private System.Windows.Forms.Button submitDealButton;
        private System.Windows.Forms.Button cancelButton;
    }
}