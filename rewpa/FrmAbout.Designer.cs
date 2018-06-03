namespace rewpa
{
	partial class FrmAbout
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAbout));
			this.BtnGotcha = new System.Windows.Forms.Button();
			this.BtnHuh = new System.Windows.Forms.Button();
			this.ImgPatreon = new System.Windows.Forms.PictureBox();
			this.ImgGitHub = new System.Windows.Forms.PictureBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.LblHelp = new System.Windows.Forms.Label();
			this.PnlHeader = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.ImgInfo = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.ImgPatreon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ImgGitHub)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.PnlHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ImgInfo)).BeginInit();
			this.SuspendLayout();
			// 
			// BtnGotcha
			// 
			this.BtnGotcha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnGotcha.Location = new System.Drawing.Point(320, 232);
			this.BtnGotcha.Name = "BtnGotcha";
			this.BtnGotcha.Size = new System.Drawing.Size(75, 23);
			this.BtnGotcha.TabIndex = 0;
			this.BtnGotcha.Text = "Gotcha";
			this.BtnGotcha.UseVisualStyleBackColor = true;
			this.BtnGotcha.Click += new System.EventHandler(this.BtnGotcha_Click);
			// 
			// BtnHuh
			// 
			this.BtnHuh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnHuh.Location = new System.Drawing.Point(401, 232);
			this.BtnHuh.Name = "BtnHuh";
			this.BtnHuh.Size = new System.Drawing.Size(75, 23);
			this.BtnHuh.TabIndex = 2;
			this.BtnHuh.Text = "... huh?";
			this.BtnHuh.UseVisualStyleBackColor = true;
			this.BtnHuh.Click += new System.EventHandler(this.BtnHuh_Click);
			// 
			// ImgPatreon
			// 
			this.ImgPatreon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ImgPatreon.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ImgPatreon.Image = ((System.Drawing.Image)(resources.GetObject("ImgPatreon.Image")));
			this.ImgPatreon.Location = new System.Drawing.Point(12, 223);
			this.ImgPatreon.Name = "ImgPatreon";
			this.ImgPatreon.Size = new System.Drawing.Size(189, 32);
			this.ImgPatreon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.ImgPatreon.TabIndex = 24;
			this.ImgPatreon.TabStop = false;
			this.ImgPatreon.Tag = "https://www.patreon.com/exectails";
			this.ImgPatreon.Click += new System.EventHandler(this.ImgLink_Click);
			// 
			// ImgGitHub
			// 
			this.ImgGitHub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ImgGitHub.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ImgGitHub.Image = ((System.Drawing.Image)(resources.GetObject("ImgGitHub.Image")));
			this.ImgGitHub.Location = new System.Drawing.Point(207, 223);
			this.ImgGitHub.Name = "ImgGitHub";
			this.ImgGitHub.Size = new System.Drawing.Size(32, 32);
			this.ImgGitHub.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.ImgGitHub.TabIndex = 23;
			this.ImgGitHub.TabStop = false;
			this.ImgGitHub.Tag = "https://github.com/exectails";
			this.ImgGitHub.Click += new System.EventHandler(this.ImgLink_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.LblHelp);
			this.groupBox1.Location = new System.Drawing.Point(12, 62);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
			this.groupBox1.Size = new System.Drawing.Size(464, 151);
			this.groupBox1.TabIndex = 28;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "How To";
			// 
			// LblHelp
			// 
			this.LblHelp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblHelp.Location = new System.Drawing.Point(12, 21);
			this.LblHelp.Name = "LblHelp";
			this.LblHelp.Size = new System.Drawing.Size(440, 122);
			this.LblHelp.TabIndex = 2;
			this.LblHelp.Text = resources.GetString("LblHelp.Text");
			// 
			// PnlHeader
			// 
			this.PnlHeader.BackColor = System.Drawing.Color.White;
			this.PnlHeader.Controls.Add(this.label3);
			this.PnlHeader.Controls.Add(this.label2);
			this.PnlHeader.Controls.Add(this.label1);
			this.PnlHeader.Controls.Add(this.ImgInfo);
			this.PnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.PnlHeader.Location = new System.Drawing.Point(0, 0);
			this.PnlHeader.Name = "PnlHeader";
			this.PnlHeader.Size = new System.Drawing.Size(488, 56);
			this.PnlHeader.TabIndex = 29;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.Color.Gray;
			this.label3.Location = new System.Drawing.Point(117, 15);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(37, 13);
			this.label3.TabIndex = 31;
			this.label3.Text = "v2.0.5";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.label2.Location = new System.Drawing.Point(52, 31);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(74, 13);
			this.label2.TabIndex = 30;
			this.label2.Text = "Region Parser";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.label1.Location = new System.Drawing.Point(50, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(70, 25);
			this.label1.TabIndex = 29;
			this.label1.Text = "rewpa";
			// 
			// ImgInfo
			// 
			this.ImgInfo.Image = ((System.Drawing.Image)(resources.GetObject("ImgInfo.Image")));
			this.ImgInfo.Location = new System.Drawing.Point(12, 12);
			this.ImgInfo.Name = "ImgInfo";
			this.ImgInfo.Size = new System.Drawing.Size(32, 32);
			this.ImgInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.ImgInfo.TabIndex = 28;
			this.ImgInfo.TabStop = false;
			// 
			// FrmAbout
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(488, 267);
			this.Controls.Add(this.PnlHeader);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.ImgPatreon);
			this.Controls.Add(this.ImgGitHub);
			this.Controls.Add(this.BtnHuh);
			this.Controls.Add(this.BtnGotcha);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmAbout";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About";
			((System.ComponentModel.ISupportInitialize)(this.ImgPatreon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ImgGitHub)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.PnlHeader.ResumeLayout(false);
			this.PnlHeader.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ImgInfo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button BtnGotcha;
		private System.Windows.Forms.Button BtnHuh;
		private System.Windows.Forms.PictureBox ImgPatreon;
		private System.Windows.Forms.PictureBox ImgGitHub;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label LblHelp;
		private System.Windows.Forms.Panel PnlHeader;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox ImgInfo;
	}
}