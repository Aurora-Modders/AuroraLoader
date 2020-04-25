using AuroraLoader.Registry;
using Microsoft.Extensions.Configuration;
using Semver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace AuroraLoader
{
    public partial class FormInstallMod : Form
    {
        private readonly Dictionary<string, string> KnownMods = new Dictionary<string, string>();
        private readonly IConfiguration _configuration;
        private readonly LocalModRegistry _localRegistry;
        private readonly RemoteModRegistry _remoteRegistry;

        public FormInstallMod(IConfiguration configuration, LocalModRegistry localRegistry, RemoteModRegistry remoteRegistry)
        {
            InitializeComponent();
            _configuration = configuration;
            _localRegistry = localRegistry;
            _remoteRegistry = remoteRegistry;
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            ButtonOk.Enabled = false;
            ButtonCancel.Enabled = false;
            Cursor = Cursors.WaitCursor;

            var update = TextUrl.Text;
            var selectedModName = (string)ComboMods.SelectedItem;
            if (selectedModName != null)
            {
                update = KnownMods[selectedModName];
            }

            // Call the Mod's install method in this bit

            Log.Debug(update);

            try
            {
                var url = "";
                using (var client = new WebClient())
                {
                    var updates = Config.FromString(client.DownloadString(update));
                    var highest = SemVersion.Parse("0.0.0");

                    foreach (var kvp in updates)
                    {
                        if (SemVersion.TryParse(kvp.Key, out SemVersion version, false))
                        {
                            if (version.CompareByPrecedence(highest) == 1)
                            {
                                highest = version;
                                url = kvp.Value;
                            }
                        }
                    }
                }

                if (!"".Equals(url))
                {
                    Updater.Update(url);
                    TextUrl.Text = "";

                    if (KnownMods.ContainsKey(selectedModName))
                    {
                        KnownMods.Remove(selectedModName);
                        UpdateCombo();
                    }

                    Cursor = Cursors.Default;
                    MessageBox.Show("Mod installed");
                }
            }
            catch (Exception exc)
            {
                Log.Error("Mod installation failed", exc);

                Cursor = Cursors.Default;
                MessageBox.Show("Failed to install mod");
            }

            ButtonOk.Enabled = true;
            ButtonCancel.Enabled = true;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormInstallMod_Load(object sender, EventArgs e)
        {
            UpdateCombo();
        }

        private void UpdateCombo()
        {
            ComboMods.Items.Clear();

            bool localModsFound = _localRegistry.ModInstallations.Any();
            if (localModsFound)
            {
                ComboMods.Items.AddRange(_localRegistry.ModInstallations.Select(mod => mod.Name).ToArray());
            }
            else
            {
                ComboMods.Items.Add($"Mods not found");
            }

            ComboMods.Enabled = localModsFound;
            ButtonOk.Enabled = localModsFound;
            ComboMods.SelectedIndex = 0;
        }

        private void ComboMods_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboMods.SelectedIndex == 0)
            {
                TextUrl.Enabled = true;
            }
            else
            {
                TextUrl.Enabled = false;
            }
        }

        private void TextUrl_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
