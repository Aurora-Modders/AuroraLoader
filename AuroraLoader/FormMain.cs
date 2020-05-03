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
using Semver;

namespace AuroraLoader
{
    public partial class FormMain : Form
    {
        private readonly IConfiguration _configuration;

        private Thread auroraThread = null;
        private AuroraInstallation auroraInstallation;

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

            // Only check mirrors for new versions at app startup
            _auroraVersionRegistry.Update(_modRegistry.Mirrors);
            auroraInstallation = new AuroraInstallation(_auroraVersionRegistry.CurrentAuroraVersion, Program.AuroraLoaderExecutableDirectory);

            _modRegistry.Update(true);
            RefreshAuroraInstallData();
            UpdateListViews();
            UpdateManageModsListView();

            Cursor = Cursors.Default;
        }

        private void ButtonUpdateAurora_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show($"Most Aurora updates are not save-game compatible!{Environment.NewLine}We'll back up your database.{Environment.NewLine}Are you sure you want to continue?", "Warning!", MessageBoxButtons.OKCancel);
            if (result != DialogResult.OK)
            {
                return;
            }

            try
            {
                auroraInstallation.CreateBackup();
                var thread = new Thread(() =>
                {
                    var aurora_files = Installer.GetLatestAuroraFiles();
                    auroraInstallation.UpdateAurora(aurora_files);
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
            MessageBox.Show($"Installing AuroraLoader {_modRegistry.AuroraLoaderMod.LatestVersion.Version}");
            try
            {
                var thread = new Thread(() => _modRegistry.UpdateAuroraLoader());
                thread.Start();
                var progress = new FormProgress(thread);
                progress.ShowDialog();
                _modRegistry.AuroraLoaderMod.UpdateCache();
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
            _auroraVersionRegistry.Update();
            if (_auroraVersionRegistry.CurrentAuroraVersion == null)
            {
                LabelAuroraVersion.Text = "Aurora version: Unknown";
            }
            else
            {
                if (_auroraVersionRegistry.CurrentAuroraVersion.Version == SemVersion.Parse("1.0.0"))
                {
                    LabelAuroraVersion.Text = $"Aurora.exe ({_auroraVersionRegistry.CurrentAuroraVersion.Checksum})";
                }
                else
                {
                    LabelAuroraVersion.Text = $"Aurora v{_auroraVersionRegistry.CurrentAuroraVersion.Version} ({_auroraVersionRegistry.CurrentAuroraVersion.Checksum})";
                }

                if (_modRegistry.AuroraLoaderMod.LatestInstalledVersion != null)
                {
                    LabelAuroraLoaderVersion.Text = $"Loader v{_modRegistry.AuroraLoaderMod.LatestInstalledVersion.Version}";
                }

                if (_auroraVersionRegistry.CurrentAuroraVersion.Version.CompareTo(_auroraVersionRegistry.AuroraVersions.Max()?.Version) < 0)
                {
                    ButtonUpdateAurora.Text = $"Update to {_auroraVersionRegistry.AuroraVersions.Max().Version}";
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
                    ButtonUpdateAuroraLoader.Text = $"Update Loader to {auroraLoaderMod.LatestVersion.Version}";
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
        /// Populates the Utilities and Database Mod tabs
        /// </summary>
        private void UpdateListViews()
        {
            // TODO retain checked state
            ListUtilities.Items.Clear();
            ListUtilities.Items.AddRange(_modRegistry.Mods.Where(mod =>
                (mod.Type == ModType.UTILITY || mod.Type == ModType.ROOTUTILITY)
                && mod.Name != "AuroraLoader"
                && mod.LatestInstalledVersionCompatibleWith(_auroraVersionRegistry.CurrentAuroraVersion) != null)
                .Select(mod => mod.Name).ToArray());

            ListDatabaseMods.Items.Clear();
            ListDatabaseMods.Items.AddRange(_modRegistry.Mods.Where(
                mod => mod.Type == ModType.DATABASE
                && GetAllowedModStatuses().Contains(mod.Status)
                && mod.LatestInstalledVersionCompatibleWith(_auroraVersionRegistry.CurrentAuroraVersion) != null)
                .Select(mod => mod.Name).ToArray());

            ComboSelectExecutableMod.Items.Clear();
            ComboSelectExecutableMod.Items.Add("Base game");

            foreach (var mod in _modRegistry.Mods.Where(
                mod => mod.Type == ModType.EXECUTABLE
                && GetAllowedModStatuses().Contains(mod.Status)
                && mod.LatestInstalledVersionCompatibleWith(_auroraVersionRegistry.CurrentAuroraVersion) != null))
            {
                ComboSelectExecutableMod.Items.Add(mod.Name);
            }
            if (ComboSelectExecutableMod.Items.Count > 0)
            {
                ComboSelectExecutableMod.SelectedIndex = 0;
            }
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
            UpdateListViews();
        }

        private void CheckEnablePoweruserMod_CheckChanged(object sender, EventArgs e)
        {
            UpdateListViews();
        }

        /// <summary>
        /// Call this to update the list of mods in the Manage tab
        /// Note that this implies a mod registry update
        /// </summary>
        public void UpdateManageModsListView()
        {
            ListManageMods.BeginUpdate();
            ListManageMods.Clear();
            ListManageMods.AllowColumnReorder = true;
            ListManageMods.FullRowSelect = true;
            ListManageMods.View = View.Details;
            ListManageMods.Columns.Add("Name");
            ListManageMods.Columns.Add("Status");
            ListManageMods.Columns.Add("Type");
            ListManageMods.Columns.Add("Current");
            ListManageMods.Columns.Add("Latest");
            ListManageMods.Columns.Add("Aurora Compatibility");
            ListManageMods.Columns.Add("Description");

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
                        mod.Description
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
                    if (selected.ConfigurationFile != null)
                    {
                        ButtonConfigureMod.Enabled = true;
                    }
                }
                else if (selected.LatestVersionCompatibleWith(_auroraVersionRegistry.CurrentAuroraVersion) != null)
                {
                    ButtonInstallOrUpdateMod.Text = "Install";
                    ButtonInstallOrUpdateMod.Enabled = true;
                }
                else if (selected.LatestVersion != null && !_auroraVersionRegistry.CurrentAuroraVersion.CompatibleWith(selected.LatestVersion.TargetAuroraVersion))
                {
                    ButtonInstallOrUpdateMod.Text = "Incompatible";
                    ButtonInstallOrUpdateMod.Enabled = false;
                }
                else
                {
                    ButtonInstallOrUpdateMod.Text = "Install";
                    ButtonInstallOrUpdateMod.Enabled = false;
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
            mod.LatestVersionCompatibleWith(_auroraVersionRegistry.CurrentAuroraVersion).Download();
            mod.UpdateCache();
            UpdateManageModsListView();
            UpdateListViews();
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
                if (auroraThread != null)
                {
                    MessageBox.Show("Already running Aurora.");
                    return;
                }
            }

            if (CheckEnableMusic.Checked && !Directory.Exists(Path.Combine(auroraInstallation.InstallationPath, "Music")))
            {
                var thread = new Thread(() =>
                {
                    Log.Debug("Installing music");
                    var aurora_files = Installer.GetLatestAuroraFiles();
                    Installer.DownloadAuroraPieces(auroraInstallation.InstallationPath, new Dictionary<string, string> { { "Music", aurora_files["Music"] } });
                });
                thread.Start();

                var progress = new FormProgress(thread) { Text = "Installing music" };
                progress.ShowDialog();
            }

            ButtonSinglePlayer.Enabled = false;
            ButtonUpdateAurora.Enabled = false;

            var modVersions = _modRegistry.Mods.Where(mod =>
            (ListDatabaseMods.CheckedItems != null && ListDatabaseMods.CheckedItems.Contains(mod.Name))
            || (ListUtilities.CheckedItems != null && ListUtilities.CheckedItems.Contains(mod.Name)))
                .Select(mod => mod.LatestInstalledVersionCompatibleWith(auroraInstallation.InstalledVersion)).ToList();

            ModVersion executableModVersion = null;
            if (ComboSelectExecutableMod.SelectedItem != null && (string)ComboSelectExecutableMod.SelectedItem != "Base game")
            {
                executableModVersion = _modRegistry.Mods.Single(mod => mod.Name == (string)ComboSelectExecutableMod.SelectedItem).LatestInstalledVersionCompatibleWith(auroraInstallation.InstalledVersion);
            }

            var process = auroraInstallation.Launch(modVersions, executableModVersion);
            auroraThread = new Thread(() => RunGame(process))
            {
                IsBackground = true
            };

            auroraThread.Start();
        }

        private void RunGame(Process process)
        {
            var songs = new List<Song>();
            var folder = Path.Combine(auroraInstallation.InstallationPath, "Music");
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
                auroraThread = null;
            }
        }

        private void EndGame()
        {
            MessageBox.Show("Game ended. Those filthy xenos never saw you coming.");
            ButtonSinglePlayer.Enabled = true;
            RefreshAuroraInstallData();
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

        private void LabelAuroraLoaderVersion_Click(object sender, EventArgs e)
        {

        }
    }
}
