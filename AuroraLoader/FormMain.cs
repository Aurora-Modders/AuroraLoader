using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using AuroraLoader.Mods;
using AuroraLoader.Registry;
using Microsoft.Extensions.Configuration;

namespace AuroraLoader
{
    public partial class FormMain : Form
    {
        private readonly IConfiguration _configuration;

        private Thread AuroraThread { get; set; } = null;

        private readonly AuroraVersionRegistry _auroraVersionRegistry;
        private readonly ModRegistry _modRegistry;

        public FormMain(IConfiguration configuration, AuroraVersionRegistry auroraVersionRegistry, ModRegistry modRegistry)
        {
            InitializeComponent();
            _configuration = configuration;
            _auroraVersionRegistry = auroraVersionRegistry;
            _modRegistry = modRegistry;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                Icon = new Icon(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Aurora.ico"));
            }
            catch
            {
                Log.Debug("Failed to load icon");
            }

            // Show message on top
            _ = MessageBox.Show(new Form { TopMost = true }, "AuroraLoader will check for updates and then launch, this might take a moment.");
            Cursor = Cursors.WaitCursor;

            CheckEnableMods.Enabled = true;
            ComboSelectExecutableMod.Enabled = false;
            ListDatabaseMods.Enabled = false;
            CheckEnablePoweruserMods.Enabled = false;
            ButtonMultiplayer.Enabled = false;

            RefreshAuroraInstallData();
            UpdateUtilitiesListView();
            UpdateExecutableModCombo();
            UpdateDatabaseModListView();
            UpdateManageModsListView();

            Cursor = Cursors.Default;
        }

        private void ButtonUpdateAurora_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Updating Aurora will wipe out your saves! Are you sure you want to continue?", "Warning!", MessageBoxButtons.OKCancel);
            if (result != DialogResult.OK)
            {
                return;
            }

            try
            {
                var installation = new GameInstallation(_auroraVersionRegistry.CurrentAuroraVersion, Program.AuroraLoaderExecutableDirectory);
                var thread = new Thread(() =>
                {
                    var aurora_files = Installer.GetLatestAuroraFiles();
                    Installer.UpdateAurora(installation, aurora_files);
                });
                thread.Start();

                var progress = new FormProgress(thread) { Text = "Updating Aurora" };
                progress.ShowDialog();
                RefreshAuroraInstallData();
            }
            catch (Exception ecp)
            {
                Log.Error("Failed to update Aurora", ecp);
                Program.OpenBrowser(@"http://aurora2.pentarch.org/index.php?board=276.0");
            }
        }

        private void ButtonUpdateAuroraLoader_Click(object sender, EventArgs e)
        {
            var auroraLoaderMod = _modRegistry.Mods.Single(mod => mod.Name == "AuroraLoader");
            MessageBox.Show($"Installing AuroraLoader {auroraLoaderMod.Listing.LatestVersion}");
            try
            {
                var thread = new Thread(() => _modRegistry.UpdateAuroraLoader(auroraLoaderMod, _auroraVersionRegistry.CurrentAuroraVersion));
                thread.Start();
                var progress = new FormProgress(thread);
                progress.ShowDialog();
                Process.Start(Path.Combine(Program.AuroraLoaderExecutableDirectory, "update_loader.bat"));
                Application.Exit();
                return;
            }
            catch (Exception exc)
            {
                Log.Error("Failed to update AuroraLoader", exc);
                MessageBox.Show("Update failed.");
            }
        }

        /// <summary>
        /// Sets current install's version and checksum, and whether the update button is enabled
        /// </summary>
        private void RefreshAuroraInstallData()
        {
            _auroraVersionRegistry.Update(_auroraVersionRegistry.CurrentAuroraVersion);
            if (_auroraVersionRegistry.CurrentAuroraVersion == null)
            {
                LabelAuroraVersion.Text = "Aurora version: Unknown";
            }
            else
            {
                _modRegistry.Update(_auroraVersionRegistry.CurrentAuroraVersion);
                LabelAuroraVersion.Text = $"Aurora v{_auroraVersionRegistry.CurrentAuroraVersion.Version} ({_auroraVersionRegistry.CurrentAuroraVersion.Checksum})";
                LabelAuroraLoaderVersion.Text = $"Loader v{_modRegistry.Mods.Single(m => m.Name == "AuroraLoader").Installation.Version}";

                if (_auroraVersionRegistry.CurrentAuroraVersion.Version.CompareTo(_auroraVersionRegistry.AuroraVersions.Max().Version) < 0)
                {
                    ButtonUpdateAurora.Text = $"Update Aurora to {_auroraVersionRegistry.AuroraVersions.Max().Version}";
                    ButtonUpdateAurora.ForeColor = Color.Green;
                    ButtonUpdateAurora.Enabled = true;
                }
                else
                {
                    ButtonUpdateAurora.Text = "Aurora up to date";
                    ButtonUpdateAurora.ForeColor = Color.Black;
                    ButtonUpdateAurora.Enabled = false;
                }
            }

            try
            {
                var auroraLoaderMod = _modRegistry.Mods.Single(mod => mod.Name == "AuroraLoader");
                if (auroraLoaderMod.CanBeUpdated)
                {
                    ButtonUpdateAuroraLoader.Text = $"Update AuroraLoader to {auroraLoaderMod.Listing.LatestVersion}";
                    ButtonUpdateAuroraLoader.ForeColor = Color.Green;
                    ButtonUpdateAuroraLoader.Enabled = true;

                    // Disable 'update aurora' button if there is a loader update that should be grabbed first
                    if (ButtonUpdateAurora.Enabled == true)
                    {
                        ButtonUpdateAurora.Text = "Update loader";
                        ButtonUpdateAurora.ForeColor = Color.Black;
                        ButtonUpdateAurora.Enabled = false;
                    }
                }
                else
                {
                    ButtonUpdateAuroraLoader.Text = "Loader up to date";
                    ButtonUpdateAuroraLoader.ForeColor = Color.Black;
                    ButtonUpdateAuroraLoader.Enabled = false;
                }
            }
            catch (Exception exc)
            {
                Log.Error("Unable to check AuroraLoader updates", exc);
            }
        }

        /* Utilities tab */

        /// <summary>
        /// Populates the Utilities tab
        /// </summary>
        private void UpdateUtilitiesListView()
        {
            ListUtilities.Items.Clear();
            ListUtilities.Items.AddRange(_modRegistry.Mods.Where(
                m => m.Installed
                && (m.Type == ModType.UTILITY || m.Type == ModType.ROOTUTILITY || m.Type == ModType.THEME)
                && m.Installation.WorksForVersion(_auroraVersionRegistry.CurrentAuroraVersion))
                .Select(mod => mod.Name).ToArray());
        }

        private IList<ModStatus> GetAllowedModStatuses()
        {
            var approvedStatuses = new List<ModStatus>();
            if (CheckEnableMods.Checked)
            {
                approvedStatuses.Add(ModStatus.APPROVED);
                approvedStatuses.Add(ModStatus.PUBLIC);
            }
            if (CheckEnablePoweruserMods.Checked)
            {
                approvedStatuses.Add(ModStatus.POWERUSER);
            }
            return approvedStatuses;
        }

        /// <summary>
        /// Call to update the list of exe mods (enabled/disabled and filter by status)
        /// </summary>
        private void UpdateExecutableModCombo()
        {
            ComboSelectExecutableMod.Items.Clear();
            ComboSelectExecutableMod.Items.Add("Base game");

            foreach (var mod in _modRegistry.Mods.Where(
                mod => mod.Installed
                && GetAllowedModStatuses().Contains(mod.Installation.Status)
                && mod.Type == ModType.EXE
                && mod.Name != "AuroraLoader"
                && mod.Installation.WorksForVersion(_auroraVersionRegistry.CurrentAuroraVersion)))
            {
                ComboSelectExecutableMod.Items.Add(mod.Name);
            }
            if (ComboSelectExecutableMod.Items.Count > 0)
            {
                ComboSelectExecutableMod.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Call to update the list of 'game mods' below the exe mod combo
        /// Currently this is the Database modtype, no mods use this yet. Always empty.
        /// </summary>
        private void UpdateDatabaseModListView()
        {
            ListDatabaseMods.Items.Clear();
            ListDatabaseMods.Items.AddRange(_modRegistry.Mods.Where(
                mod => mod.Installed
                && GetAllowedModStatuses().Contains(mod.Installation.Status)
                && mod.Type == ModType.DATABASE
                && mod.Installation.WorksForVersion(_auroraVersionRegistry.CurrentAuroraVersion))
                .Select(mod => mod.Name).ToArray());
        }

        /// <summary>
        /// Call this to update the list of mods in the Manage tab
        /// Note that this implies a mod registry update
        /// </summary>
        public void UpdateManageModsListView()
        {
            _modRegistry.Update(_auroraVersionRegistry.CurrentAuroraVersion);
            ListManageMods.BeginUpdate();
            ListManageMods.Clear();
            ListManageMods.AllowColumnReorder = true;
            ListManageMods.FullRowSelect = true;
            ListManageMods.View = View.Details;
            ListManageMods.Columns.Add("Name");
            ListManageMods.Columns.Add("Type");
            ListManageMods.Columns.Add("Aurora Version");
            ListManageMods.Columns.Add("Installed");
            ListManageMods.Columns.Add("Latest");


            foreach (var mod in _modRegistry.Mods)
            {
                if (mod.Name != "AuroraLoader")
                {
                    var li = new ListViewItem(new string[] {
                        mod.Name,
                        mod.Type.ToString(),
                        mod.Installation?.TargetAuroraVersion.ToString() == "1" ? "Any" : mod.Installation?.TargetAuroraVersion.ToString(),
                        mod.Installation?.HighestInstalledVersion.ToString(),
                        mod.Listing?.LatestVersion.ToString() ?? "Not found"
                    });
                    ListManageMods.Items.Add(li);
                }
            }
            ListManageMods.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            ListManageMods.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            ListManageMods.EndUpdate();

            ButtonInstallOrUpdateMod.Enabled = false;
            ButtonConfigureMod.Enabled = false;
        }

        private void ListManageMods_SelectedIndexChanged(object sender, EventArgs e)
        {
            ButtonInstallOrUpdateMod.Enabled = false;
            ButtonConfigureMod.Enabled = false;

            if (ListManageMods.SelectedItems.Count > 0)
            {
                var selected = _modRegistry.Mods.Single(mod => mod.Name == ListManageMods.SelectedItems[0].Text);
                if (selected.Installed)
                {
                    ButtonInstallOrUpdateMod.Text = "Update";
                    if (selected.CanBeUpdated)
                    {
                        ButtonInstallOrUpdateMod.Enabled = true;
                    }
                    if (selected.Installation.ModInternalConfigFile != null)
                    {
                        ButtonConfigureMod.Enabled = true;
                    }
                }
                else
                {
                    ButtonInstallOrUpdateMod.Text = "Install";
                    ButtonInstallOrUpdateMod.Enabled = true;
                }
            }
            else
            {
                ButtonInstallOrUpdateMod.Text = "Update";
                ButtonInstallOrUpdateMod.Enabled = false;
            }
        }

        private void ButtonInstallOrUpdateMods_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var mod = _modRegistry.Mods.Single(mod => mod.Name == ListManageMods.SelectedItems[0].Text);
            _modRegistry.InstallOrUpdate(mod, _auroraVersionRegistry.CurrentAuroraVersion);
            UpdateManageModsListView();
            UpdateDatabaseModListView();
            UpdateUtilitiesListView();
            UpdateExecutableModCombo();
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Open selected mod's ModInternalConfigFile (defined in mod configuration)
        /// </summary>
        private void ButtonConfigureMod_Click(object sender, EventArgs e)
        {
            try
            {
                var mod = _modRegistry.Mods.Single(mod => mod.Name == ListManageMods.SelectedItems[0].Text);
                if (mod.Installation == null || mod.Installation.ModFolder == null || mod.Installation.ModInternalConfigFile == null)
                {
                    throw new Exception("Invalid mod selected for configuration");
                }

                var pieces = mod.Installation.ModInternalConfigFile.Split(' ');
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

                Log.Debug($"{mod.Name} config file: run {exe} in {mod.Installation.ModFolder} with args {args}");
                if (!File.Exists(Path.Combine(mod.Installation.ModFolder, exe)))
                {
                    MessageBox.Show($"Couldn't launch {Path.Combine(mod.Installation.ModFolder, exe)} - make sure {Path.Combine(mod.Installation.ModFolder, "mod.ini")} is correctly configured.");
                    return;
                }
                var info = new ProcessStartInfo()
                {
                    WorkingDirectory = mod.Installation.ModFolder,
                    FileName = exe,
                    Arguments = args,
                    UseShellExecute = true,
                    CreateNoWindow = true
                };

                Process.Start(info);
            }
            catch (Exception exc)
            {
                Log.Error($"Failed while trying to open {ListManageMods.SelectedItems[0]} config file", exc);
            }

        }

        private void ButtonSinglePlayer_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void StartGame()
        {
            lock (this)
            {
                if (AuroraThread != null)
                {
                    MessageBox.Show("Already running Aurora.");
                    return;
                }
            }

            ButtonSinglePlayer.Enabled = false;
            ButtonUpdateAurora.Enabled = false;

            var mods = _modRegistry.Mods.Where(mod =>
            (ListDatabaseMods.CheckedItems != null && ListDatabaseMods.CheckedItems.Contains(mod.Name))
            || (ListUtilities.CheckedItems != null && ListUtilities.CheckedItems.Contains(mod.Name))).ToList();

            Mod executableMod;
            if (ComboSelectExecutableMod.SelectedItem != null && (string)ComboSelectExecutableMod.SelectedItem != "Base game")
            {
                executableMod = _modRegistry.Mods.Single(mod => mod.Name == (string)ComboSelectExecutableMod.SelectedItem);
            }
            else
            {
                executableMod = null;
            }
            var installation = new GameInstallation(_auroraVersionRegistry.CurrentAuroraVersion, Program.AuroraLoaderExecutableDirectory);
            var process = Launcher.Launch(installation, _modRegistry, mods, executableMod)[0];

            AuroraThread = new Thread(() => RunGame(process))
            {
                IsBackground = true
            };

            AuroraThread.Start();
        }

        private void RunGame(Process process)
        {
            var songs = new List<Song>();
            var folder = Path.Combine(Program.AuroraLoaderExecutableDirectory, "Music");
            if (Directory.Exists(folder))
            {
                foreach (var mp3 in Directory.EnumerateFiles(folder, "*.mp3", SearchOption.AllDirectories))
                {
                    songs.Add(new Song(mp3));
                }
            }

            var rng = new Random();

            while (!process.HasExited)
            {
                if (CheckEnableMusic.Checked && songs.Count > 0)
                {
                    var current = songs.Where(s => s.Playing).FirstOrDefault();

                    if (current == null)
                    {
                        current = songs[rng.Next(songs.Count)];

                        Log.Debug("Playing song: " + Path.GetFileNameWithoutExtension(current.File));
                        current.Play();
                    }
                    else
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            current.Volume = TrackMusicVolume.Value / 10d;
                        });
                    }
                }
                else
                {
                    foreach (var song in songs)
                    {
                        song.Stop();
                    }
                }
                Thread.Sleep(1000);
            }

            foreach (var song in songs)
            {
                song.Stop();
            }

            Invoke((MethodInvoker)delegate
            {
                EndGame();
            });

            lock (this)
            {
                AuroraThread = null;
            }
        }

        private void EndGame()
        {
            MessageBox.Show("Game ended.");
            ButtonSinglePlayer.Enabled = true;
            RefreshAuroraInstallData();
            ButtonInstallOrUpdateMod.Enabled = true;
        }

        private void ButtonReadme_Click(object sender, EventArgs e)
        {
            try
            {
                var file = Path.Combine(Program.AuroraLoaderExecutableDirectory, "loader_readme.txt");
                if (!File.Exists(file))
                {
                    File.Copy(Path.Combine(Program.AuroraLoaderExecutableDirectory, "README.md"), file);
                }

                var info = new ProcessStartInfo()
                {
                    FileName = file,
                    UseShellExecute = true
                };
                Process.Start(info);
            }
            catch (Exception exc)
            {
                Log.Error($"Couldn't load readme from {Path.Combine(Program.AuroraLoaderExecutableDirectory, "README.md")}", exc);
                Program.OpenBrowser("https://github.com/Aurora-Modders/AuroraLoader/blob/master/README.md");
            }
        }

        private void CheckMusic_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckEnableMusic.Checked)
            {
                TrackMusicVolume.Enabled = true;
            }
            else
            {
                TrackMusicVolume.Enabled = false;
            }
        }

        private void ListUtilityMods_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO display description
        }

        private void LinkModSubreddit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Program.OpenBrowser(@"https://www.reddit.com/r/aurora4x_mods/");
        }

        private void LinkVanillaSubreddit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Program.OpenBrowser(@"https://www.reddit.com/r/aurora/");
        }

        private void LinkForums_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Program.OpenBrowser(@"http://aurora2.pentarch.org/");
        }

        private void LinkVanillaBug_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Program.OpenBrowser(@"http://aurora2.pentarch.org/index.php?board=273.0");
        }

        private void LinkDiscord_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Program.OpenBrowser(@"https://discordapp.com/channels/314031775892373504/701885084646506628");
        }

        private void CheckEnableMods_CheckChanged(object sender, EventArgs e)
        {
            if (CheckEnableMods.Checked)
            {
                var result = MessageBox.Show("By using game mods you agree to not post bug reports to the official Aurora bug report channels.", "Warning!", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    LinkReportBug.Enabled = false;

                    ComboSelectExecutableMod.Enabled = true;
                    ListDatabaseMods.Enabled = true;
                    CheckEnablePoweruserMods.Enabled = true;
                }
                else
                {
                    CheckEnableMods.Checked = false;
                }
            }
            if (!CheckEnableMods.Checked)
            {
                LinkReportBug.Enabled = true;

                ComboSelectExecutableMod.SelectedItem = ComboSelectExecutableMod.Items[0];
                for (int i = 0; i < ListDatabaseMods.Items.Count; i++)
                {
                    ListDatabaseMods.SetItemChecked(i, false);
                }

                ComboSelectExecutableMod.Enabled = false;
                ListDatabaseMods.Enabled = false;
                CheckEnablePoweruserMods.Enabled = false;
            }
            UpdateDatabaseModListView();
            UpdateUtilitiesListView();
            UpdateExecutableModCombo();
        }

        private void CheckEnablePoweruserMod_CheckChanged(object sender, EventArgs e)
        {
            UpdateDatabaseModListView();
            UpdateUtilitiesListView();
            UpdateExecutableModCombo();
        }
    }
}
