using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;

using Microsoft.Extensions.Configuration;

using AuroraLoader.Mods;
using AuroraLoader.Registry;

namespace AuroraLoader
{
    public partial class FormModDownload : Form
    {
        private readonly IConfiguration _configuration;
        private readonly AuroraVersionRegistry _auroraVersionRegistry;
        private readonly ModRegistry _modRegistry;

        public FormModDownload(IConfiguration configuration)
        {
            InitializeComponent();
            _configuration = configuration;
            _auroraVersionRegistry = new AuroraVersionRegistry(configuration);
            _modRegistry = new ModRegistry(configuration);
        }

        private void FormModDownload_Load(object sender, EventArgs e)
        {
            _auroraVersionRegistry.Update(_modRegistry.Mirrors);
            _modRegistry.Update(_auroraVersionRegistry.CurrentAuroraVersion, true);
            UpdateManageModsListView();
        }

        private void ListManageMods_SelectedIndexChanged(object sender, EventArgs e)
        {
            ButtonGetMod.Enabled = false;
            ButtonConfigMod.Enabled = false;
            ButtonChangelog.Enabled = false;

            if (ListViewRegistryMods.SelectedItems.Count > 0)
            {
                var selected = _modRegistry.Mods.Single(mod => mod.Name == ListViewRegistryMods.SelectedItems[0].Text);
                RichTextBoxDescription.Text = selected.Description;
                if (selected.Installed)
                {
                    ButtonGetMod.Text = "Update";
                    if (selected.CanBeUpdated(_auroraVersionRegistry.CurrentAuroraVersion))
                    {
                        ButtonGetMod.Enabled = true;
                    }
                    if (selected.Installed && selected.ConfigurationFile != null)
                    {
                        ButtonConfigMod.Enabled = true;
                    }
                    if (selected.Installed && selected.ChangelogFile != null)
                    {
                        ButtonChangelog.Enabled = true;
                    }
                }
                else if (selected.LatestVersionCompatibleWith(_auroraVersionRegistry.CurrentAuroraVersion) != null)
                {
                    ButtonGetMod.Text = "Install";
                    ButtonGetMod.Enabled = true;
                }
                else if (selected.LatestVersion != null && !_auroraVersionRegistry.CurrentAuroraVersion.CompatibleWith(selected.LatestVersion.TargetAuroraVersion))
                {
                    ButtonGetMod.Text = "Incompatible";
                    ButtonGetMod.Enabled = false;
                }
                else
                {
                    ButtonGetMod.Text = "Install";
                    ButtonGetMod.Enabled = false;
                }
            }
            else
            {
                ButtonGetMod.Text = "Update";
                ButtonGetMod.Enabled = false;
            }
        }

        private void ButtonGetMod_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var mod = _modRegistry.Mods.Single(mod => mod.Name == ListViewRegistryMods.SelectedItems[0].Text);
            mod.LatestVersionCompatibleWith(_auroraVersionRegistry.CurrentAuroraVersion).Download();
            UpdateManageModsListView();
            Cursor = Cursors.Default;
        }

        public void UpdateManageModsListView()
        {
            ListViewRegistryMods.BeginUpdate();
            ListViewRegistryMods.Clear();
            ListViewRegistryMods.AllowColumnReorder = true;
            ListViewRegistryMods.FullRowSelect = true;
            ListViewRegistryMods.View = View.Details;
            ListViewRegistryMods.Columns.Add("Name");
            ListViewRegistryMods.Columns.Add("Status");
            ListViewRegistryMods.Columns.Add("Type");
            ListViewRegistryMods.Columns.Add("Current");
            ListViewRegistryMods.Columns.Add("Latest");
            ListViewRegistryMods.Columns.Add("Aurora Compatibility");
            ///ListViewRegistryMods.Columns.Add("Description");

            foreach (var mod in _modRegistry.Mods)
            {
                if (mod.Name != "AuroraLoader")
                {
                    var li = new ListViewItem(new string[] {
                        mod.Name,
                        mod.Status.ToString(),
                        mod.Type.ToString(),
                        mod.LatestInstalledVersion?.Version?.ToString() ?? "Not Installed",
                        mod.LatestInstalledVersionCompatibleWith(_auroraVersionRegistry.CurrentAuroraVersion)?.Version == mod.LatestVersion?.Version
                            ? "Up to date"
                            : mod.LatestVersion?.Version?.ToString()
                            ?? "-",
                        mod.LatestVersion.TargetAuroraVersion?.Pretty(),
                        //mod.Description
                    });
                    ListViewRegistryMods.Items.Add(li);
                }
            }

            ListViewRegistryMods.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            ListViewRegistryMods.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            ListViewRegistryMods.EndUpdate();

            ButtonGetMod.Enabled = false;
            ButtonConfigMod.Enabled = false;
            ButtonChangelog.Enabled = false;
        }

        private void ButtonConfigMod_Click(object sender, EventArgs e)
        {
            try
            {
                var mod = _modRegistry.Mods.Single(mod => mod.Name == ListViewRegistryMods.SelectedItems[0].Text);
                if (mod.ModFolder == null || mod.ConfigurationFile == null)
                {
                    throw new Exception("Invalid mod selected for configuration");
                }

                var pieces = mod.ConfigurationFile.Split(' ');
                var exe = pieces[0];
                var args = "";
                if (pieces.Length > 1)
                {
                    for (int i = 1; i < pieces.Length; i++)
                    {
                        args += " " + pieces[i];
                    }

                    args = args.Substring(1);
                }

                var modVersion = mod.LatestInstalledVersionCompatibleWith(_auroraVersionRegistry.CurrentAuroraVersion);
                Log.Debug($"{mod.Name} config file: run {exe} in {modVersion.DownloadPath} with args {args}");
                if (!File.Exists(Path.Combine(modVersion.DownloadPath, exe)))
                {
                    MessageBox.Show($"Couldn't launch {Path.Combine(modVersion.DownloadPath, exe)} - make sure {Path.Combine(mod.ModFolder, "mod.json")} is correctly configured.");
                    return;
                }
                var info = new ProcessStartInfo()
                {
                    WorkingDirectory = modVersion.DownloadPath,
                    FileName = exe,
                    Arguments = args,
                    UseShellExecute = true,
                    CreateNoWindow = true
                };

                Process.Start(info);
            }
            catch (Exception exc)
            {
                Log.Error($"Failed while trying to open {ListViewRegistryMods.SelectedItems[0]} config file", exc);
            }
        }

        private void ButtonChangelog_Click(object sender, EventArgs e)
        {
            try
            {
                var mod = _modRegistry.Mods.Single(mod => mod.Name == ListViewRegistryMods.SelectedItems[0].Text);
                if (mod.ModFolder == null || mod.ChangelogFile == null)
                {
                    throw new Exception("Invalid mod selected for changelog");
                }

                var pieces = mod.ChangelogFile.Split(' ');
                var exe = pieces[0];
                var args = "";
                if (pieces.Length > 1)
                {
                    for (int i = 1; i < pieces.Length; i++)
                    {
                        args += " " + pieces[i];
                    }

                    args = args.Substring(1);
                }

                var modVersion = mod.LatestInstalledVersionCompatibleWith(_auroraVersionRegistry.CurrentAuroraVersion);
                Log.Debug($"{mod.Name} changelog file: run {exe} in {modVersion.DownloadPath} with args {args}");
                if (!File.Exists(Path.Combine(modVersion.DownloadPath, exe)))
                {
                    MessageBox.Show($"Couldn't launch {Path.Combine(modVersion.DownloadPath, exe)} - make sure {Path.Combine(mod.ModFolder, "mod.json")} is correctly configured.");
                    return;
                }
                var info = new ProcessStartInfo()
                {
                    WorkingDirectory = modVersion.DownloadPath,
                    FileName = exe,
                    Arguments = args,
                    UseShellExecute = true,
                    CreateNoWindow = true
                };

                Process.Start(info);
            }
            catch (Exception exc)
            {
                Log.Error($"Failed while trying to open {ListViewRegistryMods.SelectedItems[0]} changelog file", exc);
            }
        }
    }
}
