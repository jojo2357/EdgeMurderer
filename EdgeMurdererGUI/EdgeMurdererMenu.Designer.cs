using System.Windows.Forms;

namespace EdgeMurdererGUI
{
	partial class EdgeMurdererMenu
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EdgeMurdererMenu));
			this.SelectedPrograms = new System.Windows.Forms.ComboBox();
			this.ProgramSelector = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.AddButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.RemoveButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// SelectedPrograms
			// 
			this.SelectedPrograms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.SelectedPrograms.Location = new System.Drawing.Point(322, 103);
			this.SelectedPrograms.Name = "SelectedPrograms";
			this.SelectedPrograms.Size = new System.Drawing.Size(261, 21);
			this.SelectedPrograms.TabIndex = 0;
			// 
			// ProgramSelector
			// 
			this.ProgramSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ProgramSelector.Location = new System.Drawing.Point(22, 103);
			this.ProgramSelector.Name = "ProgramSelector";
			this.ProgramSelector.Size = new System.Drawing.Size(261, 21);
			this.ProgramSelector.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(22, 81);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(261, 19);
			this.label1.TabIndex = 1;
			this.label1.Text = "Recently Opened";
			// 
			// AddButton
			// 
			this.AddButton.BackColor = System.Drawing.Color.Transparent;
			this.AddButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.AddButton.ForeColor = System.Drawing.SystemColors.ControlText;
			this.AddButton.Image = ((System.Drawing.Image)(resources.GetObject("AddButton.Image")));
			this.AddButton.Location = new System.Drawing.Point(287, 99);
			this.AddButton.Margin = new System.Windows.Forms.Padding(1);
			this.AddButton.Name = "AddButton";
			this.AddButton.Size = new System.Drawing.Size(31, 28);
			this.AddButton.TabIndex = 2;
			this.AddButton.UseVisualStyleBackColor = false;
			this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(322, 81);
			this.label2.Name = "label2";
			this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.label2.Size = new System.Drawing.Size(261, 19);
			this.label2.TabIndex = 3;
			this.label2.Text = "Blacklisted Apps";
			// 
			// RemoveButton
			// 
			this.RemoveButton.Image = ((System.Drawing.Image)(resources.GetObject("RemoveButton.Image")));
			this.RemoveButton.Location = new System.Drawing.Point(589, 99);
			this.RemoveButton.Name = "RemoveButton";
			this.RemoveButton.Size = new System.Drawing.Size(33, 28);
			this.RemoveButton.TabIndex = 4;
			this.RemoveButton.UseVisualStyleBackColor = true;
			this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
			// 
			// EdgeMurdererMenu
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(659, 203);
			this.Controls.Add(this.RemoveButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.AddButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ProgramSelector);
			this.Controls.Add(this.SelectedPrograms);
			this.MaximizeBox = false;
			this.Name = "EdgeMurdererMenu";
			this.ShowIcon = false;
			this.Text = "EdgeMurdererMenu";
			this.ResumeLayout(false);
		}

		private System.Windows.Forms.Button RemoveButton;

		private System.Windows.Forms.Label label2;

		private System.Windows.Forms.Button AddButton;

		private System.Windows.Forms.Button button1;

		private System.Windows.Forms.Label label1;

		private System.Windows.Forms.ComboBox ProgramSelector;
		private System.Windows.Forms.ComboBox SelectedPrograms;

		#endregion
		
		public void initData()
		{
			this.ProgramSelector.DataSource = Program.openedPrograms.ToArray();
			this.SelectedPrograms.DataSource = Program.blacklistedPrograms.ToArray();
		}
	}
}