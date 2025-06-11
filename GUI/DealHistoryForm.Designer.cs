namespace GUI
{
    partial class DealHistoryForm
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
            this.dealsDataGridView = new System.Windows.Forms.DataGridView();
            this.oligarchLabel = new System.Windows.Forms.Label();
            this.politicianLabel = new System.Windows.Forms.Label();
            this.makeNewDealButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dealsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dealsDataGridView
            // 
            this.dealsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dealsDataGridView.Location = new System.Drawing.Point(12, 70);
            this.dealsDataGridView.Name = "dealsDataGridView";
            this.dealsDataGridView.RowHeadersWidth = 51;
            this.dealsDataGridView.Size = new System.Drawing.Size(776, 319);
            this.dealsDataGridView.TabIndex = 0;
            // 
            // oligarchLabel
            // 
            this.oligarchLabel.AutoSize = true;
            this.oligarchLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.oligarchLabel.Location = new System.Drawing.Point(12, 9);
            this.oligarchLabel.Name = "oligarchLabel";
            this.oligarchLabel.Size = new System.Drawing.Size(99, 28);
            this.oligarchLabel.TabIndex = 1;
            this.oligarchLabel.Text = "Oligarch:";
            // 
            // politicianLabel
            // 
            this.politicianLabel.AutoSize = true;
            this.politicianLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.politicianLabel.Location = new System.Drawing.Point(12, 37);
            this.politicianLabel.Name = "politicianLabel";
            this.politicianLabel.Size = new System.Drawing.Size(99, 28);
            this.politicianLabel.TabIndex = 2;
            this.politicianLabel.Text = "Politician:";
            // 
            // makeNewDealButton
            // 
            this.makeNewDealButton.Location = new System.Drawing.Point(623, 404);
            this.makeNewDealButton.Name = "makeNewDealButton";
            this.makeNewDealButton.Size = new System.Drawing.Size(165, 34);
            this.makeNewDealButton.TabIndex = 3;
            this.makeNewDealButton.Text = "Make New Deal";
            this.makeNewDealButton.UseVisualStyleBackColor = true;
            this.makeNewDealButton.Click += new System.EventHandler(this.makeNewDealButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(12, 404);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(112, 34);
            this.closeButton.TabIndex = 4;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // DealHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.makeNewDealButton);
            this.Controls.Add(this.politicianLabel);
            this.Controls.Add(this.oligarchLabel);
            this.Controls.Add(this.dealsDataGridView);
            this.Name = "DealHistoryForm";
            this.Text = "Deal History";
            this.Load += new System.EventHandler(this.DealHistoryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dealsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dealsDataGridView;
        private System.Windows.Forms.Label oligarchLabel;
        private System.Windows.Forms.Label politicianLabel;
        private System.Windows.Forms.Button makeNewDealButton;
        private System.Windows.Forms.Button closeButton;
    }
}