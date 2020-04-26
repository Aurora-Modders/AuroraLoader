using AuroraLoader.Mods;
using AuroraLoader.Registry;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

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
            //Icon = Properties.Resources.Aurora;
            MessageBox.Show("AuroraLoader will check for updates and then launch, this might take a moment.");
            Cursor = Cursors.WaitCursor;

            RefreshAuroraInstallData();
            UpdateUtilitiesListView();
            UpdateLaunchExeCombo();
            UpdateGameModsListView();
            UpdateManageModsListView();

            Cursor = Cursors.Default;
            CheckEnableGameMods.Enabled = true;
            ComboSelectLaunchExe.Enabled = false;
            ListDatabaseMods.Enabled = false;
            CheckApproved.Enabled = false;
            CheckPower.Enabled = false;
            CheckPublic.Enabled = false;
            TabMods.SelectedTab = TabApplyMods;
        }

        private void ButtonUpdateAurora_Click(object sender, EventArgs e)
        {
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
            MessageBox.Show($"Installing AuroraLoader {auroraLoaderMod.Listing.LatestVersion}. Open {Path.Combine(Program.AuroraLoaderExecutableDirectory, $"{auroraLoaderMod.Name}.{auroraLoaderMod.Listing.LatestVersion}.exe")} when this window closes.");
            try
            {
                _modRegistry.UpdateAuroraLoader(auroraLoaderMod);
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
            _auroraVersionRegistry.Update();
            if (_auroraVersionRegistry.CurrentAuroraVersion == null)
            {
                LabelVersion.Text = "Aurora version: Unknown";
            }
            else
            {
                LabelChecksum.Text = $"Aurora checksum: {_auroraVersionRegistry.CurrentAuroraVersion.Checksum}";
                LabelVersion.Text = $"Aurora version: {_auroraVersionRegistry.CurrentAuroraVersion.Version}";
                LabelAuroraLoaderVersion.Text = $"AuroraLoader Version: {_modRegistry.Mods.Single(m => m.Name == "AuroraLoader").Installation.Version}";

                if (_auroraVersionRegistry.CurrentAuroraVersion.Version.CompareTo(_auroraVersionRegistry.AuroraVersions.Max().Version) < 0)
                {
                    ButtonUpdateAurora.Text = $"Update Aurora to {_auroraVersionRegistry.AuroraVersions.Max().Version}";
                    ButtonUpdateAurora.ForeColor = Color.Green;
                    ButtonUpdateAurora.Enabled = true;
                }
                else
                {
                    ButtonUpdateAurora.Text = "Aurora is up to date";
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
                }
                else
                {
                    ButtonUpdateAuroraLoader.Text = "AuroraLoader is up to date";
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
                && (m.Type == ModType.UTILITY || m.Type == ModType.ROOTUTILITY))
                .Select(mod => mod.Name).ToArray());
        }

        /* Game mods tab */

        private void CheckEnableGameMods_CheckChanged(object sender, EventArgs e)
        {
            if (CheckEnableGameMods.Checked)
            {
                var result = MessageBox.Show("By using game mods you agree to not post bug reports to the official Aurora bug report channels.", "Warning!", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    ButtonAuroraBugs.Enabled = false;
                    ButtonAuroraBugs.ForeColor = Color.Black;
                    ButtonModBugs.Enabled = true;
                    ButtonModBugs.ForeColor = Color.OrangeRed;

                    ComboSelectLaunchExe.Enabled = true;
                    ListDatabaseMods.Enabled = true;
                    CheckApproved.Enabled = true;
                    CheckPower.Enabled = true;
                    CheckPublic.Enabled = true;
                }
                else
                {
                    CheckEnableGameMods.Checked = false;
                }
            }
            if (!CheckEnableGameMods.Checked)
            {
                ButtonAuroraBugs.Enabled = true;
                ButtonAuroraBugs.ForeColor = Color.OrangeRed;
                ButtonModBugs.Enabled = false;
                ButtonModBugs.ForeColor = Color.Black;

                ComboSelectLaunchExe.SelectedItem = ComboSelectLaunchExe.Items[0];

                CheckEnableGameMods.Enabled = true;
                ComboSelectLaunchExe.Enabled = false;
                ListDatabaseMods.Enabled = false;
                CheckApproved.Enabled = false;
                CheckPower.Enabled = false;
                CheckPublic.Enabled = false;
            }
        }

        private IList<ModStatus> GetAllowedModStatuses()
        {
            var approvedStatuses = new List<ModStatus>();
            if (CheckApproved.Checked)
            {
                approvedStatuses.Add(ModStatus.APPROVED);
            }
            if (CheckPublic.Checked)
            {
                approvedStatuses.Add(ModStatus.PUBLIC);
            }
            if (CheckPower.Checked)
            {
                approvedStatuses.Add(ModStatus.POWERUSER);
            }
            return approvedStatuses;
        }

        /// <summary>
        /// Call to update the list of exe mods (enabled/disabled and filter by status)
        /// </summary>
        private void UpdateLaunchExeCombo()
        {
            ComboSelectLaunchExe.Items.Clear();
            ComboSelectLaunchExe.Items.Add("Base game");

            foreach (var mod in _modRegistry.Mods.Where(
                mod => mod.Installed
                && GetAllowedModStatuses().Contains(mod.Installation.Status)
                && mod.Type == ModType.EXE
                && mod.Name != "AuroraLoader"))
            {
                ComboSelectLaunchExe.Items.Add(mod.Name);
            }
            if (ComboSelectLaunchExe.Items.Count > 0)
            {
                ComboSelectLaunchExe.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Enable the Configure EXE button if the EXE is configurable
        /// </summary>
        private void ComboSelectLaunchExe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)ComboSelectLaunchExe.SelectedItem != "Base game")
            {
                var selectedMod = _modRegistry.Mods.Single(mod => mod.Name == (string)ComboSelectLaunchExe.SelectedItem);
            }
            else
            {
            }
        }

        /// <summary>
        /// Call to update the list of 'game mods' below the exe mod combo
        /// Currently this is the Database modtype, no mods use this yet. Always empty.
        /// </summary>
        private void UpdateGameModsListView()
        {
            ListDatabaseMods.Items.Clear();
            ListDatabaseMods.Items.AddRange(_modRegistry.Mods.Where(
                mod => mod.Installed
                && GetAllowedModStatuses().Contains(mod.Installation.Status)
                && mod.Type == ModType.DATABASE).Select(mod => mod.Name).ToArray());
        }

        /// <summary>
        /// Call this to update the list of mods in the Manage tab
        /// Note that this implies a mod registry update
        /// </summary>
        public void UpdateManageModsListView()
        {
            _modRegistry.Update();
            ListManageMods.BeginUpdate();
            ListManageMods.Clear();
            ListManageMods.AllowColumnReorder = true;
            ListManageMods.FullRowSelect = true;
            ListManageMods.Dock = DockStyle.Top;
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
                        mod.Installation?.TargetAuroraVersion.ToString(),
                        mod.Installation?.Version.ToString(),
                        mod.Listing?.LatestVersion.ToString() ?? "Not found"
                    });
                    ListManageMods.Items.Add(li);
                }
            }
            ListManageMods.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            ListManageMods.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            ButtonInstallOrUpdate.Enabled = false;
            ListManageMods.EndUpdate();
            ListManageMods.Focus();
            if (ListManageMods.Items.Count > 0)
            {
                ListManageMods.Items[0].Selected = true;
            }
            else
            {
                ButtonConfigureMod.Enabled = false;
            }
        }

        private void ListManageMods_SelectedIndexChanged(object sender, EventArgs e)
        {
            ButtonInstallOrUpdate.Enabled = false;
            ButtonConfigureMod.Enabled = false;

            if (ListManageMods.SelectedItems.Count > 0)
            {
                var selected = _modRegistry.Mods.Single(mod => mod.Name == ListManageMods.SelectedItems[0].Text);
                if (selected.Installed)
                {
                    ButtonInstallOrUpdate.Text = "Update";
                    if (selected.CanBeUpdated)
                    {
                        ButtonInstallOrUpdate.Enabled = true;
                    }
                    if (selected.Installation.ModInternalConfigFile != null)
                    {
                        ButtonConfigureMod.Enabled = true;
                    }
                }
                else
                {
                    ButtonInstallOrUpdate.Text = "Install";
                    ButtonInstallOrUpdate.Enabled = true;
                }
            }
            else
            {
                ButtonInstallOrUpdate.Text = "Update";
                ButtonInstallOrUpdate.Enabled = false;
            }

        }

        private void ButtonInstallOrUpdateMods_Click(object sender, EventArgs e)
        {

            Cursor = Cursors.WaitCursor;
            var mod = _modRegistry.Mods.Single(mod => mod.Name == ListManageMods.SelectedItems[0].Text);
            _modRegistry.InstallOrUpdate(mod);
            UpdateManageModsListView();
            UpdateGameModsListView();
            UpdateUtilitiesListView();
            UpdateLaunchExeCombo();
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Open selected mod's ModInternalConfigFile (defined in mod configuration)
        /// </summary>
        private void ButtonConfigureMod_Click(object sender, EventArgs e)
        {
            var mod = _modRegistry.Mods.Single(mod => mod.Name == ListManageMods.SelectedItems[0].Text);

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

            Log.Debug("Running: " + mod.Installation.ModInternalConfigFile);
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
            ButtonMultiPlayer.Enabled = false;
            ButtonInstallAurora.Enabled = false;
            ButtonUpdateAurora.Enabled = false;

            var mods = _modRegistry.Mods.Where(mod =>
            (ListDatabaseMods.CheckedItems != null && ListDatabaseMods.CheckedItems.Contains(mod.Name))
            || (ListUtilities.CheckedItems != null && ListUtilities.CheckedItems.Contains(mod.Name))).ToList();

            Mod executableMod;
            if (ComboSelectLaunchExe.SelectedItem != null && (string)ComboSelectLaunchExe.SelectedItem != "Base game")
            {
                executableMod = _modRegistry.Mods.Single(mod => mod.Name == (string)ComboSelectLaunchExe.SelectedItem);
            }
            else
            {
                executableMod = null;
            }
            var process = Launcher.Launch(mods, executableMod);

            AuroraThread = new Thread(() => RunGame(process))
            {
                IsBackground = true
            };

            AuroraThread.Start();
        }

        private void RunGame(Process process)
        {
            var songs = new List<Song>();
            var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Music");
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
                if (CheckMusic.Checked && songs.Count > 0)
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
                            current.Volume = TrackVolume.Value / 10d;
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
            ButtonInstallOrUpdate.Enabled = true;
        }

        private void ButtonSinglePlayer_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void CheckApproved_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGameModsListView();
            UpdateLaunchExeCombo();
        }

        private void CheckPublic_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGameModsListView();
            UpdateLaunchExeCombo();
        }

        private void CheckPower_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGameModsListView();
            UpdateLaunchExeCombo();
        }

        private void ButtonAuroraForums_Click(object sender, EventArgs e)
        {
            Program.OpenBrowser(@"http://aurora2.pentarch.org/index.php?action=forum#c14");
        }

        private void ButtonAuroraBugs_Click(object sender, EventArgs e)
        {
            Program.OpenBrowser(@"http://aurora2.pentarch.org/index.php?board=273.0");
        }

        private void ButtonModsSubreddit_Click(object sender, EventArgs e)
        {
            Program.OpenBrowser(@"https://www.reddit.com/r/aurora4x_mods/");
        }

        private void ButtonModBugs_Click(object sender, EventArgs e)
        {
            Process.Start(@"https://www.reddit.com/r/aurora4x_mods/");
        }

        private void ButtonReadme_Click(object sender, EventArgs e)
        {
            var info = new ProcessStartInfo()
            {
                FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "README.md"),
                UseShellExecute = true
            };
            Process.Start(info);
        }

        private void CheckMusic_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckMusic.Checked)
            {
                TrackVolume.Value = 5;
                TrackVolume.Enabled = true;
            }
            else
            {
                TrackVolume.Enabled = false;
            }
        }

        private void TabMods_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ListUtilities_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
