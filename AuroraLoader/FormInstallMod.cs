using Semver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace AuroraLoader
{
    public partial class FormInstallMod : Form
    {
        private readonly Dictionary<string, string> KnownMods = new Dictionary<string, string>();

        public FormInstallMod()
        {
            InitializeComponent();
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            ButtonOk.Enabled = false;
            ButtonCancel.Enabled = false;
            Cursor = Cursors.WaitCursor;

            var update = TextUrl.Text;
            var selected = (string)ComboMods.SelectedItem;
            if (selected != null && !selected.Equals("Use url"))
            {
                update = KnownMods[selected];
            }

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

                    if (KnownMods.ContainsKey(selected))
                    {
                        KnownMods.Remove(selected);
                        UpdateCombo();
                    }

                    Cursor = Cursors.Default;
                    MessageBox.Show("Mod installed");
                }
            }
            catch (Exception)
            {
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
            var installed = Mod.GetInstalledMods().Select(m => m.Name).ToList();

            try
            {
                var mods = Config.FromString(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mods", "mods.txt")));
                foreach (var kvp in mods)
                {
                    if (!installed.Contains(kvp.Key))
                    {
                        KnownMods[kvp.Key] = kvp.Value;
                    }
                }

                using (var client = new WebClient())
                {
                    foreach (var mirror in Program.MIRRORS)
                    {
                        mods = Config.FromString(client.DownloadString(mirror + "Mods/mods.txt"));
                        foreach (var kvp in mods)
                        {
                            if (!installed.Contains(kvp.Key))
                            {
                                KnownMods[kvp.Key] = kvp.Value;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }

            UpdateCombo();
        }

        private void UpdateCombo()
        {
            if (KnownMods.Count > 0)
            {
                ComboMods.Items.Clear();
                ComboMods.Items.Add("Use url");
                ComboMods.Items.AddRange(KnownMods.Keys.ToArray());
                ComboMods.SelectedIndex = 0;
                ComboMods.Enabled = true;
            }
            else
            {
                ComboMods.Items.Clear();
                ComboMods.Items.Add("Use url");
                ComboMods.SelectedIndex = 0;
                ComboMods.Enabled = false;
            }
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
    }
}
