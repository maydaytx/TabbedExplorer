using System;
using System.Drawing;
using System.Windows.Forms;

namespace TabbedExplorer
{
	internal class RenameTabForm : Form
	{
		private readonly TextBox newNameTextBox = new TextBox();
		private readonly Label newNameLabel = new Label();
		private string newName = string.Empty;

		internal string NewName
		{
			get { return newName; }
		}

		internal RenameTabForm()
		{
			InitializeLayout();
		}

		private void InitializeLayout()
		{
			SuspendLayout();

			newNameTextBox.Location = new Point(86, 10);
			newNameTextBox.Name = "newNameTextBox";
			newNameTextBox.Size = new Size(100, 20);
			newNameTextBox.TabIndex = 0;
			newNameTextBox.KeyUp += NewNameTextBoxKeyUp;
			newNameTextBox.TextChanged += NewNameTextBoxTextChanged;

			newNameLabel.AutoSize = true;
			newNameLabel.Location = new Point(13, 13);
			newNameLabel.Name = "newNameLabel";
			newNameLabel.Size = new Size(63, 13);
			newNameLabel.TabIndex = 1;
			newNameLabel.Text = "New Name:";
			newNameLabel.TextAlign = ContentAlignment.MiddleRight;

			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(198, 40);
			Controls.Add(newNameLabel);
			Controls.Add(newNameTextBox);
			MaximizeBox = false;
			MaximumSize = new Size(206, 74);
			MinimizeBox = false;
			MinimumSize = new Size(206, 74);
			Name = "fmRenameTab";
			ShowIcon = false;
			ShowInTaskbar = false;
			Text = "Rename Tab";
			ResumeLayout(false);

			PerformLayout();
		}

		private void NewNameTextBoxTextChanged(object sender, EventArgs e)
		{
			newName = newNameTextBox.Text;
		}

		private void NewNameTextBoxKeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				DialogResult = DialogResult.OK;
				Close();
			}
		}
	}
}