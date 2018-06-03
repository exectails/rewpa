namespace rewpa
{
	partial class FrmMain
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
			this.TxtSources = new System.Windows.Forms.TextBox();
			this.BtnCreate = new System.Windows.Forms.Button();
			this.BtnHelp = new System.Windows.Forms.Button();
			this.ProgressBar = new System.Windows.Forms.ProgressBar();
			this.StatusStrip = new System.Windows.Forms.StatusStrip();
			this.LblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.SfdRegionInfo = new System.Windows.Forms.SaveFileDialog();
			this.StatusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// TxtSources
			// 
			this.TxtSources.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TxtSources.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtSources.Location = new System.Drawing.Point(12, 12);
			this.TxtSources.Multiline = true;
			this.TxtSources.Name = "TxtSources";
			this.TxtSources.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.TxtSources.Size = new System.Drawing.Size(468, 339);
			this.TxtSources.TabIndex = 0;
			this.TxtSources.WordWrap = false;
			// 
			// BtnCreate
			// 
			this.BtnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnCreate.Location = new System.Drawing.Point(317, 357);
			this.BtnCreate.Name = "BtnCreate";
			this.BtnCreate.Size = new System.Drawing.Size(134, 23);
			this.BtnCreate.TabIndex = 1;
			this.BtnCreate.Tag = "Generate regioninfo.dat";
			this.BtnCreate.Text = "Generate regioninfo.dat";
			this.BtnCreate.UseVisualStyleBackColor = true;
			this.BtnCreate.Click += new System.EventHandler(this.BtnCreate_Click);
			// 
			// BtnHelp
			// 
			this.BtnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnHelp.Location = new System.Drawing.Point(457, 357);
			this.BtnHelp.Name = "BtnHelp";
			this.BtnHelp.Size = new System.Drawing.Size(23, 23);
			this.BtnHelp.TabIndex = 2;
			this.BtnHelp.Text = "?";
			this.BtnHelp.UseVisualStyleBackColor = true;
			this.BtnHelp.Click += new System.EventHandler(this.BtnHelp_Click);
			// 
			// ProgressBar
			// 
			this.ProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ProgressBar.Location = new System.Drawing.Point(12, 357);
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(299, 23);
			this.ProgressBar.TabIndex = 3;
			// 
			// StatusStrip
			// 
			this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LblStatus});
			this.StatusStrip.Location = new System.Drawing.Point(0, 386);
			this.StatusStrip.Name = "StatusStrip";
			this.StatusStrip.Size = new System.Drawing.Size(492, 22);
			this.StatusStrip.TabIndex = 5;
			this.StatusStrip.Text = "statusStrip1";
			// 
			// LblStatus
			// 
			this.LblStatus.Name = "LblStatus";
			this.LblStatus.Size = new System.Drawing.Size(0, 17);
			// 
			// SfdRegionInfo
			// 
			this.SfdRegionInfo.FileName = "regioninfo.dat";
			this.SfdRegionInfo.Filter = "Region Info|regioninfo.dat";
			// 
			// FrmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(492, 408);
			this.Controls.Add(this.StatusStrip);
			this.Controls.Add(this.ProgressBar);
			this.Controls.Add(this.BtnHelp);
			this.Controls.Add(this.BtnCreate);
			this.Controls.Add(this.TxtSources);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FrmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "rewpa2";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
			this.Load += new System.EventHandler(this.FrmMain_Load);
			this.StatusStrip.ResumeLayout(false);
			this.StatusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox TxtSources;
		private System.Windows.Forms.Button BtnCreate;
		private System.Windows.Forms.Button BtnHelp;
		private System.Windows.Forms.ProgressBar ProgressBar;
		private System.Windows.Forms.StatusStrip StatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel LblStatus;
		private System.Windows.Forms.SaveFileDialog SfdRegionInfo;
	}
}

