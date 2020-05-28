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
        private Thread auroraThread = null;
        private AuroraInstallation auroraInstallation;

        private readonly IConfiguration _configuration;
        private readonly AuroraVersionRegistry _auroraVersionRegistry;
        private readonly ModRegistry _modRegistry;

        private FormModDownload _modManagementWindow;
        private FormSaves _saveManagementWindow;

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

            Cursor = Cursors.WaitCursor;

            CheckEnableMods.Enabled = true;
            ComboSelectExecutableMod.Enabled = false;
            ListDatabaseMods.Enabled = false;
            CheckEnablePoweruserMods.Enabled = false;
            ButtonMultiplayer.Enabled = false;

            // Only check mirrors for new versions at app startup
            _auroraVersionRegistry.Update(_modRegistry.Mirrors);
            auroraInstallation = new AuroraInstallation(_auroraVersionRegistry.CurrentAuroraVersion, Path.Combine(Program.AuroraLoaderExecutableDirectory, "Clean"));

            _modRegistry.Update(_auroraVersionRegistry.CurrentAuroraVersion, true);
            RefreshAuroraInstallData();
            UpdateListViews();

            Cursor = Cursors.Default;
        }

        private void UpdateAurora()
        {
            var dialog = MessageBox.Show($"Aurora v{_auroraVersionRegistry.AuroraVersions.Max()?.Version} is available. Download now? This is safe and won't affect your existing games.", "Download new Aurora version", MessageBoxButtons.YesNo);
            if (dialog != DialogResult.Yes)
            {
                return;
            }

            try
            {
                var thread = new Thread(() =>
                {
                    var aurora_files = Installer.GetLatestAuroraFiles();
                    var clean = new AuroraInstallation(_auroraVersionRegistry.CurrentAuroraVersion, Path.Combine(Program.AuroraLoaderExecutableDirectory, "Clean"));
                    clean.UpdateAurora(aurora_files);
                });
                thread.Start();

                var progress = new FormProgress(thread) { Text = "Updating Aurora" };
                progress.ShowDialog();
                RefreshAuroraInstallData();
                MessageBox.Show($"Update complete - you can now start new games using Aurora {_auroraVersionRegistry.CurrentAuroraVersion.Version}!");
            }
            catch (Exception ecp)
            {
                Log.Error("Failed to update Aurora", ecp);
                Program.OpenBrowser(@"http://aurora2.pentarch.org/index.php?board=276.0");
            }
        }

        private void UpdateLoader()
        {
            var dialog = MessageBox.Show($"AuroraLoader version {_modRegistry.AuroraLoaderMod.LatestVersion.Version} available. Update?", "Update Loader", MessageBoxButtons.YesNo);
            if (dialog != DialogResult.Yes)
            {
                return;
            }

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

        private void ButtonUpdateAuroraLoader_Click(object sender, EventArgs e) { UpdateLoader(); }

        private void ButtonUpdateAurora_Click(object sender, EventArgs e) { UpdateAurora(); }

        private void SetCanUpdateAurora(bool update)
        {
            PictureBoxUpdateAurora.Enabled = update;
            PictureBoxUpdateAurora.Visible = update;
        }

        private void SetCanUpdateLoader(bool update)
        {
            PictureBoxUpdateLoader.Enabled = update;
            PictureBoxUpdateLoader.Visible = update;
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
                // Show only the checksum if we can't identify the version of Aurora
                if (_auroraVersionRegistry.CurrentAuroraVersion.Version == SemVersion.Parse("1.0.0"))
                {
                    LabelAuroraVersion.Text = $"Aurora.exe ({_auroraVersionRegistry.CurrentAuroraVersion.Checksum})";
                }
                // Default to showing the most recent installed Aurora version
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
                    SetCanUpdateAurora(true);
                }
                else
                {
                    SetCanUpdateAurora(false);
                }
            }

            try
            {
                var auroraLoaderMod = _modRegistry.Mods.Single(mod => mod.Name == "AuroraLoader");
                if (auroraLoaderMod.CanBeUpdated(auroraInstallation.InstalledVersion))
                {
                    SetCanUpdateLoader(true);

                    SetCanUpdateAurora(false);
                }
                else
                {
                    SetCanUpdateLoader(false);
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
                && mod.LatestInstalledVersionCompatibleWith(auroraInstallation.InstalledVersion) != null)
                .Select(mod => mod.Name).ToArray());

            ListDatabaseMods.Items.Clear();
            ListDatabaseMods.Items.AddRange(_modRegistry.Mods.Where(
                mod => mod.Type == ModType.DATABASE
                && GetAllowedModStatuses().Contains(mod.Status)
                && mod.LatestInstalledVersionCompatibleWith(auroraInstallation.InstalledVersion) != null)
                .Select(mod => mod.Name).ToArray());

            ComboSelectExecutableMod.Items.Clear();
            ComboSelectExecutableMod.Items.Add("Base game");

            foreach (var mod in _modRegistry.Mods.Where(
                mod => mod.Type == ModType.EXECUTABLE
                && GetAllowedModStatuses().Contains(mod.Status)
                && mod.LatestInstalledVersionCompatibleWith(auroraInstallation.InstalledVersion) != null))
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
            SetCanUpdateLoader(false);
            SetCanUpdateAurora(false);

            var modVersions = _modRegistry.Mods.Where(mod =>
            (ListDatabaseMods.CheckedItems != null && ListDatabaseMods.CheckedItems.Contains(mod.Name))
            || (ListUtilities.CheckedItems != null && ListUtilities.CheckedItems.Contains(mod.Name)))
                .Select(mod => mod.LatestInstalledVersionCompatibleWith(auroraInstallation.InstalledVersion)).ToList();

            ModVersion executableModVersion = null;
            if (ComboSelectExecutableMod.SelectedItem != null && (string)ComboSelectExecutableMod.SelectedItem != "Base game")
            {
                executableModVersion = _modRegistry.Mods.Single(mod => mod.Name == (string)ComboSelectExecutableMod.SelectedItem).LatestInstalledVersionCompatibleWith(auroraInstallation.InstalledVersion);
            }

            var processes = auroraInstallation.Launch(modVersions, executableModVersion);
            auroraThread = new Thread(() => RunGame(processes, modVersions))
            {
                IsBackground = true
            };

            auroraThread.Start();
        }

        private void RunGame(List<Process> processes, List<ModVersion> modVersions)
        {
            var aurora = processes[0];

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

            while (!aurora.HasExited)
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

            foreach (var process in processes)
            {
                if (!process.HasExited)
                {
                    process.Kill();
                }
            }

            Invoke((MethodInvoker)delegate
            {
                auroraInstallation.Cleanup(modVersions);
                Log.Debug("Game ended, cleaned up instalation");

                ButtonSinglePlayer.Enabled = true;
                RefreshAuroraInstallData();
            });

            lock (this)
            {
                auroraThread = null;
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

        private void ButtonReadme_Click(object sender, EventArgs e)
        {
            if (File.Exists(Path.Combine(Program.AuroraLoaderExecutableDirectory, "README.md")))
            {
                Process.Start(new ProcessStartInfo()
                {
                    WorkingDirectory = Program.AuroraLoaderExecutableDirectory,
                    FileName = "README.md",
                    UseShellExecute = true,
                    CreateNoWindow = true
                });
            }
            else
            {
                Program.OpenBrowser("https://github.com/Aurora-Modders/AuroraLoader/blob/master/README.md");
            }
        }

        private void ButtonChangelog_Click(object sender, EventArgs e)
        {
            if (File.Exists(Path.Combine(Program.AuroraLoaderExecutableDirectory, "CHANGELOG.md")))
            {
                Process.Start(new ProcessStartInfo()
                {
                    WorkingDirectory = Program.AuroraLoaderExecutableDirectory,
                    FileName = "CHANGELOG.md",
                    UseShellExecute = true,
                    CreateNoWindow = true
                });
            }
            else
            {
                Program.OpenBrowser("https://github.com/Aurora-Modders/AuroraLoader/blob/master/CHANGELOG.md");
            }
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

        private void ButtonManageMods_Click(object sender, EventArgs e)
        {
            UpdateListViews();
            if (_modManagementWindow != null)
            {
                _modManagementWindow.Close();
            }
            _modManagementWindow = new FormModDownload(_configuration);
            _modManagementWindow.Show();
        }

        private void ButtonManageSaves_Click(object sender, EventArgs e)
        {
            if (_saveManagementWindow != null)
            {
                _saveManagementWindow.Close();
            }
            _saveManagementWindow = new FormSaves(auroraInstallation);
            _saveManagementWindow.ShowDialog();

            var name = _saveManagementWindow.Game;
            if (name != null)
            {
                var exe = Path.Combine(Program.AuroraLoaderExecutableDirectory, "Games", name, "Aurora.exe");
                var bytes = File.ReadAllBytes(exe);
                var checksum = Program.GetChecksum(bytes);
                var version = _auroraVersionRegistry.AuroraVersions.First(v => v.Checksum == checksum);
                auroraInstallation = new AuroraInstallation(version, Path.GetDirectoryName(exe));

                UpdateListViews();
                RefreshAuroraInstallData();

                SelectedSavelabel.Text = $"Game: {name} (Aurora v{auroraInstallation.InstalledVersion.Version})";
                ButtonSinglePlayer.Enabled = true;
            }
            else
            {
                SelectedSavelabel.Text = "Game: XXXX";
                ButtonSinglePlayer.Enabled = false;
            }
        }
    }
}
