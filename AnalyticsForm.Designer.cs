namespace EMS
{
    partial class AnalyticsForm
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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvNotifications = new System.Windows.Forms.DataGridView();
            this.dgvAnalyticsResults = new System.Windows.Forms.DataGridView();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNotifications)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnalyticsResults)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Yu Gothic UI", 13.8F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Location = new System.Drawing.Point(187, 569);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(107, 38);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvNotifications
            // 
            this.dgvNotifications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNotifications.Location = new System.Drawing.Point(42, 74);
            this.dgvNotifications.Name = "dgvNotifications";
            this.dgvNotifications.Size = new System.Drawing.Size(382, 414);
            this.dgvNotifications.TabIndex = 2;
            // 
            // dgvAnalyticsResults
            // 
            this.dgvAnalyticsResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAnalyticsResults.Location = new System.Drawing.Point(456, 74);
            this.dgvAnalyticsResults.Name = "dgvAnalyticsResults";
            this.dgvAnalyticsResults.Size = new System.Drawing.Size(356, 414);
            this.dgvAnalyticsResults.TabIndex = 4;
            // 
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("Yu Gothic UI", 13.8F, System.Drawing.FontStyle.Bold);
            this.btnBack.Location = new System.Drawing.Point(530, 569);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(219, 38);
            this.btnBack.TabIndex = 16;
            this.btnBack.Text = "Back to Dashboard";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // AnalyticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(859, 726);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.dgvAnalyticsResults);
            this.Controls.Add(this.dgvNotifications);
            this.Controls.Add(this.btnRefresh);
            this.ForeColor = System.Drawing.Color.MidnightBlue;
            this.Name = "AnalyticsForm";
            this.Text = "Analytics";
            this.Load += new System.EventHandler(this.AnalyticsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNotifications)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnalyticsResults)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvNotifications;
        private System.Windows.Forms.DataGridView dgvAnalyticsResults;
        private System.Windows.Forms.Button btnBack;
    }
}