namespace CompetitionUI
{
    partial class CompetitionDashboardForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompetitionDashboardForm));
            this.headerLabel = new System.Windows.Forms.Label();
            this.loadExistingCompetitionDropDown = new System.Windows.Forms.ComboBox();
            this.loadExistingCompetitionLabel = new System.Windows.Forms.Label();
            this.loadCompetitionButton = new System.Windows.Forms.Button();
            this.createCompetitionButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // headerLabel
            // 
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = new System.Drawing.Font("Segoe UI Light", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.headerLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.headerLabel.Location = new System.Drawing.Point(107, 26);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(394, 50);
            this.headerLabel.TabIndex = 13;
            this.headerLabel.Text = "Competition Dashboard";
            // 
            // loadExistingCompetitionDropDown
            // 
            this.loadExistingCompetitionDropDown.FormattingEnabled = true;
            this.loadExistingCompetitionDropDown.Location = new System.Drawing.Point(153, 180);
            this.loadExistingCompetitionDropDown.Name = "loadExistingCompetitionDropDown";
            this.loadExistingCompetitionDropDown.Size = new System.Drawing.Size(303, 33);
            this.loadExistingCompetitionDropDown.TabIndex = 20;
            // 
            // loadExistingCompetitionLabel
            // 
            this.loadExistingCompetitionLabel.AutoSize = true;
            this.loadExistingCompetitionLabel.Font = new System.Drawing.Font("Segoe UI Light", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.loadExistingCompetitionLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.loadExistingCompetitionLabel.Location = new System.Drawing.Point(149, 121);
            this.loadExistingCompetitionLabel.Name = "loadExistingCompetitionLabel";
            this.loadExistingCompetitionLabel.Size = new System.Drawing.Size(310, 37);
            this.loadExistingCompetitionLabel.TabIndex = 19;
            this.loadExistingCompetitionLabel.Text = "Load Existing Competition";
            // 
            // loadCompetitionButton
            // 
            this.loadCompetitionButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.loadCompetitionButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.loadCompetitionButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.loadCompetitionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadCompetitionButton.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.loadCompetitionButton.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.loadCompetitionButton.Location = new System.Drawing.Point(211, 235);
            this.loadCompetitionButton.Name = "loadCompetitionButton";
            this.loadCompetitionButton.Size = new System.Drawing.Size(186, 52);
            this.loadCompetitionButton.TabIndex = 21;
            this.loadCompetitionButton.Text = "Load Competition";
            this.loadCompetitionButton.UseVisualStyleBackColor = true;
        
            // 
            // createCompetitionButton
            // 
            this.createCompetitionButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.createCompetitionButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.createCompetitionButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.createCompetitionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createCompetitionButton.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.createCompetitionButton.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.createCompetitionButton.Location = new System.Drawing.Point(163, 309);
            this.createCompetitionButton.Name = "createCompetitionButton";
            this.createCompetitionButton.Size = new System.Drawing.Size(283, 52);
            this.createCompetitionButton.TabIndex = 31;
            this.createCompetitionButton.Text = "Create Competition";
            this.createCompetitionButton.UseVisualStyleBackColor = true;
            this.createCompetitionButton.Click += new System.EventHandler(this.createCompetitionButton_Click_1);
            // 
            // CompetitionDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(611, 474);
            this.Controls.Add(this.createCompetitionButton);
            this.Controls.Add(this.loadCompetitionButton);
            this.Controls.Add(this.loadExistingCompetitionDropDown);
            this.Controls.Add(this.loadExistingCompetitionLabel);
            this.Controls.Add(this.headerLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "CompetitionDashboardForm";
            this.Text = "Competition Dashboard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label headerLabel;
        private ComboBox loadExistingCompetitionDropDown;
        private Label loadExistingCompetitionLabel;
        private Button loadCompetitionButton;
        private Button createCompetitionButton;
    }
}