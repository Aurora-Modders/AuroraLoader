using AuroraLoader.Mods;
using AuroraLoader.Registry;
using Microsoft.Extensions.Configuration;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Transactions;
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

            UpdateManageModsListView();
            RefreshAuroraInstallData();
            _modRegistry.Update();
            UpdateLaunchExeCombo();
            UpdateGameModsListView();
            UpdateUtilitiesListView();
            UpdateButtons();

            Cursor = Cursors.Default;
            TabMods.SelectedTab = modsTab;
        }

        private void RefreshAuroraInstallData()
        {
            _auroraVersionRegistry.Update();
            _auroraVersionRegistry.Update();
            LabelChecksum.Text = $"Aurora checksum: {_auroraVersionRegistry.CurrentAuroraVersion.Checksum}";
            LabelVersion.Text = $"Aurora version: {_auroraVersionRegistry.CurrentAuroraVersion.Version}";

            ButtonUpdateAurora.Text = "Update Aurora";
            ButtonUpdateAurora.ForeColor = Color.Black;
            ButtonUpdateAurora.Enabled = false;

            // Let it be known that the first elvis operator was added to the project at this very spot
            if (_auroraVersionRegistry.CurrentAuroraVersion?.Version?.Equals(_auroraVersionRegistry.AuroraVersions?.Max().Version) ?? false)
            {
                ButtonUpdateAurora.Text = $"Update Aurora to {_auroraVersionRegistry.AuroraVersions.Max().Version}!";
                ButtonUpdateAurora.ForeColor = Color.Green;
                ButtonUpdateAurora.Enabled = true;
            }
        }

        private void UpdateUtilitiesListView()
        {
            ListUtilityMods.Items.Clear();
            ListUtilityMods.Items.AddRange(_modRegistry.Mods.Where(
                m => m.Installed
                && (m.Type == ModType.UTILITY || m.Type == ModType.ROOTUTILITY))
                .Select(mod => mod.Name).ToArray());
        }

        private void CheckEnableGameMods_CheckChanged(object sender, EventArgs e)
        {
            if (CheckEnableGameMods.Checked)
            {
                var result = MessageBox.Show("By using game mods you agree to not post bug reports to the official Aurora bug report channels.", "Warning!", MessageBoxButtons.OKCancel);
                if (result != DialogResult.OK)
                {
                    CheckEnableGameMods.Checked = false;
                }
            }

            UpdateLaunchExeCombo();
            UpdateGameModsListView();
            UpdateButtons();
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

        private void UpdateLaunchExeCombo()
        {
            if (CheckEnableGameMods.Checked)
            {
                ComboSelectLaunchExe.Enabled = true;
                ComboSelectLaunchExe.Items.Clear();

                foreach (var mod in _modRegistry.Mods.Where(
                    mod => mod.Installed
                    && GetAllowedModStatuses().Contains(mod.Installation.Status)
                    && mod.Type == ModType.EXE))
                {
                    ComboSelectLaunchExe.Items.Add(mod.Name);
                }
                if (ComboSelectLaunchExe.Items.Count > 0)
                {
                    ComboSelectLaunchExe.SelectedIndex = 0;
                }
            }
            else
            {
                ComboSelectLaunchExe.Enabled = false;
            }
        }

        private void UpdateGameModsListView()
        {
            if (CheckEnableGameMods.Checked)
            {
                ListGameMods.Enabled = true;
                ListGameMods.Items.Clear();
                ListGameMods.Items.AddRange(_modRegistry.Mods.Where(
                    mod => mod.Installed
                    && GetAllowedModStatuses().Contains(mod.Installation.Status)
                    && mod.Type == ModType.DATABASE).Select(mod => mod.Name).ToArray());
            }
            else
            {
                ListGameMods.Enabled = false;
                ListGameMods.Items.Clear();
            }
        }

        public void UpdateManageModsListView()
        {
            _modRegistry.Update();
            allModsListView.Clear();
            allModsListView.AllowColumnReorder = true;
            allModsListView.View = View.Details;
            allModsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            allModsListView.Columns.Add("Name");
            allModsListView.Columns.Add("Type");
            allModsListView.Columns.Add("Aurora Version");
            allModsListView.Columns.Add("Installed");
            allModsListView.Columns.Add("Latest");

            foreach (var mod in _modRegistry.Mods)
            {
                var li = new ListViewItem(new string[] {
                    mod.Name,
                    mod.Type.ToString(),
                    mod.Installation?.TargetAuroraVersion.ToString(),
                    mod.Installation?.Version.ToString(),
                    mod.Listing?.LatestVersion.ToString() ?? "Not found"
                });
                allModsListView.Items.Add(li);
            }

            // TODO 'Install' button shown and enabled when !mod.Installed
            // TODO 'Update' button shown and enabled when mod.Installation.Version < mod.Listing.LatestVersion
            // TODO Launch/Configure/Enable/etc buttons
        }

        private void UpdateButtons()
        {
            if (!(ListUtilityMods.SelectedItem is ModConfiguration utility) || utility.ModInternalConfigFile == null)
            {
                ButtonConfigureUtility.Enabled = false;
            }
            else
            {
                ButtonConfigureUtility.Enabled = true;
            }

            if (!(ComboSelectLaunchExe.SelectedItem is ModConfiguration exe) || exe.ModInternalConfigFile == null)
            {
                ButtonConfigureExe.Enabled = false;
            }
            else
            {
                ButtonConfigureExe.Enabled = true;
            }

            if (CheckEnableGameMods.Checked)
            {
                GroupMods.Enabled = true;
                ButtonAuroraBugs.Enabled = false;
                ButtonAuroraBugs.ForeColor = Color.Black;
                ButtonModBugs.Enabled = true;
                ButtonModBugs.ForeColor = Color.OrangeRed;
            }
            else
            {
                GroupMods.Enabled = false;
                ButtonAuroraBugs.Enabled = true;
                ButtonAuroraBugs.ForeColor = Color.OrangeRed;
                ButtonModBugs.Enabled = false;
                ButtonModBugs.ForeColor = Color.Black;
            }
        }

        private void ConfigureMod(ModConfiguration mod)
        {
            var info = new ProcessStartInfo()
            {
                FileName = mod.ModFolder,
                UseShellExecute = true
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
            ButtonInstallMods.Enabled = false;

            TabUtilities.Enabled = false;
            TabGameMods.Enabled = false;


            // TODO 

            var mods = new List<Mod>();
            //if (CheckMods.Checked)
            //{
            //    exe = ComboExe.SelectedItem?.ToString();

            //    for (int i = 0; i < ListDBMods.CheckedItems.Count; i++)
            //    {
            //        mods.Add(ListDBMods.CheckedItems[i] as ModInstallation);
            //    }
            //}

            //for (int i = 0; i < ListUtilityMods.CheckedItems.Count; i++)
            //{
            //    mods.Add(ListUtilityMods.CheckedItems[i] as ModInstallation);
            //}

            var process = Launcher.Launch(mods);

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
            Cursor = Cursors.WaitCursor;

            ButtonSinglePlayer.Enabled = true;
            ButtonInstallMods.Enabled = true;
            TabUtilities.Enabled = true;
            TabGameMods.Enabled = true;

            RefreshAuroraInstallData();
            _modRegistry.Update();
            //UpdateLists();

            UpdateButtons();

            Cursor = Cursors.Default;
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

        private void ButtonAuroraUpdates_Click(object sender, EventArgs e)
        {
            Program.OpenBrowser(@"http://aurora2.pentarch.org/index.php?board=276.0");
        }

        private void ButtonModsSubreddit_Click(object sender, EventArgs e)
        {
            Program.OpenBrowser(@"https://www.reddit.com/r/aurora4x_mods/");
        }

        private void ButtonUpdateMods_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Log.Debug("Start updating");
            var count = _modRegistry.Mods.Where(mod => mod.CanBeUpdated).Count();
            foreach (var mod in _modRegistry.Mods.Where(mod => mod.CanBeUpdated))
            {
                Log.Debug($"Updating {mod.Name} from {mod.Listing.LatestVersionUrl}");
                try
                {
                    _modRegistry.InstallOrUpdate(mod);
                }
                catch (Exception exc)
                {
                    Log.Error($"Failed to update {mod.Name} from {mod.Listing.LatestVersionUrl}", exc);

                    Cursor = Cursors.Default;
                    MessageBox.Show($"Failed to update {mod.Name} from {mod.Listing.LatestVersionUrl}");
                    Cursor = Cursors.WaitCursor;
                }
            }

            _modRegistry.Update();
            //UpdateLists();
            UpdateButtons();

            Cursor = Cursors.Default;
            MessageBox.Show($"Updated {count} mods.");
            Log.Debug("Stop updating");
        }

        private void ComboExe_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void ButtonConfigureExe_Click(object sender, EventArgs e)
        {
            var selected = ComboSelectLaunchExe.SelectedItem as ModConfiguration;
            ConfigureMod(selected);
        }

        private void ListDBMods_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void ButtonInstallMods_Click(object sender, EventArgs e)
        {
            // TODO split up into separate buttons for my own personal convenience
            var form = new FormInstallMod(_configuration, _modRegistry);
            form.ShowDialog();

            Cursor = Cursors.WaitCursor;

            _modRegistry.Update();

            Cursor = Cursors.Default;
        }

        private void ListUtilityMods_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void ButtonModBugs_Click(object sender, EventArgs e)
        {
            Process.Start(@"https://www.reddit.com/r/aurora4x_mods/");
        }

        private void ButtonConfigureUtility_Click(object sender, EventArgs e)
        {
            var selected = ListUtilityMods.SelectedItem as ModConfiguration;
            ConfigureMod(selected);
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
    }
}
