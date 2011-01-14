using System.Drawing;

namespace TabbedExplorer
{
	partial class TabbedExplorer
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
			this.explorerTabs = new ExplorerTabControl();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// explorerTabs
			// 
			this.explorerTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.explorerTabs.Location = new System.Drawing.Point(0, 0);
			this.explorerTabs.Name = "explorerTabs";
			this.explorerTabs.SelectedIndex = 0;
			this.explorerTabs.Size = new System.Drawing.Size(640, 497);
			this.explorerTabs.TabIndex = 0;
			// 
			// buttonAdd
			// 
			this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonAdd.Location = new System.Drawing.Point(620, 2);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(18, 18);
			this.buttonAdd.TabIndex = 1;
			this.buttonAdd.Text = "+";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// TabbedExplorer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(640, 497);
			this.Controls.Add(this.buttonAdd);
			this.Controls.Add(this.explorerTabs);
			this.Name = "TabbedExplorer";
			this.Text = "Tabbed Explorer";
			this.ResumeLayout(false);

		}

		#endregion

		private ExplorerTabControl explorerTabs;
		private System.Windows.Forms.Button buttonAdd;

	}
}

