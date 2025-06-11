namespace GUI
{
    partial class SelectOligarchAndPoliticianForm
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
            this.oligarchsListBox = new System.Windows.Forms.ListBox();
            this.politiciansListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.viewDealsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // oligarchsListBox
            // 
            this.oligarchsListBox.FormattingEnabled = true;
            this.oligarchsListBox.ItemHeight = 16;
            this.oligarchsListBox.Location = new System.Drawing.Point(12, 38);
            this.oligarchsListBox.Name = "oligarchsListBox";
            this.oligarchsListBox.Size = new System.Drawing.Size(220, 292);
            this.oligarchsListBox.TabIndex = 0;
            // 
            // politiciansListBox
            // 
            this.politiciansListBox.FormattingEnabled = true;
            this.politiciansListBox.ItemHeight = 16;
            this.politiciansListBox.Location = new System.Drawing.Point(252, 38);
            this.politiciansListBox.Name = "politiciansListBox";
            this.politiciansListBox.Size = new System.Drawing.Size(220, 292);
            this.politiciansListBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Oligarchs";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(252, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Politicians";
            // 
            // viewDealsButton
            // 
            this.viewDealsButton.Location = new System.Drawing.Point(137, 349);
            this.viewDealsButton.Name = "viewDealsButton";
            this.viewDealsButton.Size = new System.Drawing.Size(210, 39);
            this.viewDealsButton.TabIndex = 4;
            this.viewDealsButton.Text = "View Deals / Make";
            this.viewDealsButton.UseVisualStyleBackColor = true;
            this.viewDealsButton.Click += new System.EventHandler(this.viewDealsButton_Click);
            // 
            // SelectOligarchAndPoliticianForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 400);
            this.Controls.Add(this.viewDealsButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.politiciansListBox);
            this.Controls.Add(this.oligarchsListBox);
            this.Name = "SelectOligarchAndPoliticianForm";
            this.Text = "Select Participants";
            this.Load += new System.EventHandler(this.DealSelectorForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ListBox oligarchsListBox;
        private System.Windows.Forms.ListBox politiciansListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button viewDealsButton;
    }
}