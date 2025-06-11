namespace GUI
{
    partial class PersonDealsDetailForm
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
            this.nameLabel = new System.Windows.Forms.Label();
            this.ageLabel = new System.Windows.Forms.Label();
            this.rolesLabel = new System.Windows.Forms.Label();
            this.roleSpecificLabel = new System.Windows.Forms.Label();
            this.dealsDataGridView = new System.Windows.Forms.DataGridView();
            this.closeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dealsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.nameLabel.Location = new System.Drawing.Point(12, 9);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(73, 28);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Name:";
            // 
            // ageLabel
            // 
            this.ageLabel.AutoSize = true;
            this.ageLabel.Location = new System.Drawing.Point(17, 41);
            this.ageLabel.Name = "ageLabel";
            this.ageLabel.Size = new System.Drawing.Size(35, 16);
            this.ageLabel.TabIndex = 1;
            this.ageLabel.Text = "Age:";
            // 
            // rolesLabel
            // 
            this.rolesLabel.AutoSize = true;
            this.rolesLabel.Location = new System.Drawing.Point(17, 66);
            this.rolesLabel.Name = "rolesLabel";
            this.rolesLabel.Size = new System.Drawing.Size(46, 16);
            this.rolesLabel.TabIndex = 2;
            this.rolesLabel.Text = "Roles:";
            // 
            // roleSpecificLabel
            // 
            this.roleSpecificLabel.AutoSize = true;
            this.roleSpecificLabel.Location = new System.Drawing.Point(17, 91);
            this.roleSpecificLabel.Name = "roleSpecificLabel";
            this.roleSpecificLabel.Size = new System.Drawing.Size(100, 16);
            this.roleSpecificLabel.TabIndex = 3;
            this.roleSpecificLabel.Text = "Role-Specific Info";
            // 
            // dealsDataGridView
            // 
            this.dealsDataGridView.AllowUserToAddRows = false;
            this.dealsDataGridView.AllowUserToDeleteRows = false;
            this.dealsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dealsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dealsDataGridView.Location = new System.Drawing.Point(12, 161);
            this.dealsDataGridView.Name = "dealsDataGridView";
            this.dealsDataGridView.ReadOnly = true;
            this.dealsDataGridView.RowHeadersWidth = 51;
            this.dealsDataGridView.Size = new System.Drawing.Size(776, 243);
            this.dealsDataGridView.TabIndex = 4;
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(694, 410);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(94, 29);
            this.closeButton.TabIndex = 5;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Associated Deals";
            // 
            // PersonDealsDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.dealsDataGridView);
            this.Controls.Add(this.roleSpecificLabel);
            this.Controls.Add(this.rolesLabel);
            this.Controls.Add(this.ageLabel);
            this.Controls.Add(this.nameLabel);
            this.Name = "PersonDealsDetailForm";
            this.Text = "Person Details";
            this.Load += new System.EventHandler(this.PersonDealsDetailForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dealsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label ageLabel;
        private System.Windows.Forms.Label rolesLabel;
        private System.Windows.Forms.Label roleSpecificLabel;
        private System.Windows.Forms.DataGridView dealsDataGridView;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label label1;
    }
}