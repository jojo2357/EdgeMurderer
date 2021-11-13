using System;
using System.Windows.Forms;

namespace EdgeMurdererGUI
{
	public partial class EdgeMurdererMenu : Form
	{
		public readonly callInvoke _callInvoke;

		public EdgeMurdererMenu()
		{
			InitializeComponent();
			_callInvoke = Refresh;
		}

		public delegate void callInvoke();

		private void AddButton_Click(object sender, EventArgs e)
		{
			if (this.ProgramSelector.SelectedItem != null && ((string)this.ProgramSelector.SelectedItem) != "" && !Program.blacklistedPrograms.Contains((string)this.ProgramSelector.SelectedItem))
			{
				Program.blacklistedPrograms.Add((string)this.ProgramSelector.SelectedItem);
				Program.openedPrograms.Remove((string)this.ProgramSelector.SelectedItem);
				initData();
				Program.SaveSettings();
			}
		}

		private void RemoveButton_Click(object sender, EventArgs e)
		{
			if (this.SelectedPrograms.SelectedItem != null && ((string)this.SelectedPrograms.SelectedItem) != "" && Program.blacklistedPrograms.Contains((string)this.SelectedPrograms.SelectedItem))
			{
				Program.blacklistedPrograms.Remove((string)this.SelectedPrograms.SelectedItem);
				Program.openedPrograms.Add((string)this.SelectedPrograms.SelectedItem);
				initData();
				Program.SaveSettings();
			}
		}
	}
}