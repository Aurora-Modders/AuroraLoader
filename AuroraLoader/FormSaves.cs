using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AuroraLoader
{
    public partial class FormSaves : Form
    {
        internal string Game { get; private set; } = null;
        private AuroraInstallation _auroraInstallation;

        public FormSaves(AuroraInstallation auroraInstallation)
        {
            InitializeComponent();
            _auroraInstallation = auroraInstallation;
        }

        private void FormSaves_Load(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void ListViewSaves_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ButtonLoadSaves_Click(object sender, EventArgs e)
        {
            Game = ListViewSaves.SelectedItems[0].Text;
            Close();
        }

        private void ButtonResetSaves_Click(object sender, EventArgs e)
        {

        }

        private void TextNewGame_Changed(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextNewGame.Text))
            {
                ButtonNewGame.Enabled = true;
            }
            else
            {
                ButtonNewGame.Enabled = false;
            }

            for (int i = 0; i < ListViewSaves.Items.Count; i++)
            {
                var item = ListViewSaves.Items[i];
                if (item.Text == TextNewGame.Text)
                {
                    ButtonNewGame.Enabled = false;
                    break;
                }
            }
        }

        private void ButtonNewGame_Click(object sender, EventArgs e)
        {
            var dialog = MessageBox.Show($"Create a new game in a fresh Aurora {_auroraInstallation.InstalledVersion} database called '{TextNewGame.Text}'?", "Create New Game", MessageBoxButtons.YesNo);
            if (dialog != DialogResult.Yes)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            var folder = Path.Combine(Program.AuroraLoaderExecutableDirectory, "Games", TextNewGame.Text);
            Installer.CopyClean(folder);
            UpdateList();
            TextNewGame.Text = null;
            Cursor = Cursors.Default;
        }

        private void UpdateList()
        {
            var games = new List<string>();

            var folder = Path.Combine(Program.AuroraLoaderExecutableDirectory, "Games");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            foreach (var game in Directory.EnumerateDirectories(folder))
            {
                games.Add(Path.GetRelativePath(folder, game));
            }

            ListViewSaves.Items.Clear();
            ListViewSaves.SelectedIndices.Clear();
            ListViewSaves.Items.AddRange(games.Select(g => new ListViewItem(g)).ToArray());

            if (games.Count > 0)
            {
                ListViewSaves.SelectedIndices.Add(0);
                ButtonLoadSaves.Enabled = true;
            }
            else
            {
                ButtonLoadSaves.Enabled = false;
            }
        }
    }
}
