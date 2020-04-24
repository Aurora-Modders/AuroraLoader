using Microsoft.Extensions.Configuration;
using NAudio.Wave;
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
        private GameVersion AuroraVersion => _gameInstallation.InstalledVersion;
        private readonly List<Mod> Mods = new List<Mod>();
        private readonly Dictionary<Mod, string> ModUpdates = new Dictionary<Mod, string>();
        
        private readonly IConfiguration _configuration;
        private readonly GameInstallation _gameInstallation;

        private Thread AuroraThread { get; set; } = null;

        public FormMain(IConfiguration configuration)
        {
            InitializeComponent();
            _configuration = configuration;
            _gameInstallation = new GameInstallation(configuration);
        }

        private void LoadVersion()
        {
            LabelChecksum.Text = $"Aurora checksum: {_gameInstallation.InstalledVersion.Checksum}";
            LabelVersion.Text = $"Aurora version: {_gameInstallation.InstalledVersion.Version}";
            if (AuroraVersion == null)
            {
                LabelVersion.Text = "Aurora version: Unknown";
                CheckMods.Enabled = false;
            }

            ButtonUpdateAurora.Text = "Update Aurora";
            ButtonUpdateAurora.ForeColor = Color.Black;
            ButtonUpdateAurora.Enabled = false;

            //if (highest != null)
            //{
            //    if (!highest.Equals(AuroraVersion))
            //    {
            //        ButtonUpdateAurora.Text = "Update Aurora: " + highest;
            //        ButtonUpdateAurora.ForeColor = Color.Green;
            //        ButtonUpdateAurora.Enabled = true;
            //    }
            //}
        }

        private void LoadMods()
        {
            Mods.Clear();
            ModUpdates.Clear();

            var mods = Mod.GetInstalledMods();
            var latest = new Dictionary<string, Mod>();

            foreach (var mod in mods)
            {
                if (mod.WorksForVersion(AuroraVersion))
                {
                    if (!latest.ContainsKey(mod.Name))
                    {
                        latest.Add(mod.Name, mod);
                    }
                    else if (mod.Version.CompareByPrecedence(latest[mod.Name].Version) == 1)
                    {
                        latest[mod.Name] = mod;
                    }
                }
            }

            Mods.AddRange(latest.Values);

            try
            {
                var urls = Updater.GetUpdateUrls(Mods);
                foreach (var kvp in urls)
                {
                    ModUpdates.Add(kvp.Key, kvp.Value);
                }
            }
            catch (Exception exc)
            {
                Log.Error("Failed to get mod updates.", exc);
            }

            ButtonUpdateMods.Enabled = false;
            ButtonUpdateMods.ForeColor = Color.Black;
            if (ModUpdates.Count > 0)
            {
                ButtonUpdateMods.Enabled = true;
                ButtonUpdateMods.ForeColor = Color.Green;
            }
        }

        private void UpdateLists()
        {
            var utility = Mods.Where(m => m.Type == Mod.ModType.UTILITY || m.Type == Mod.ModType.ROOT_UTILITY).ToList();

            var status_approved = CheckApproved.Checked;
            var status_public = CheckPublic.Checked;
            var status_poweruser = CheckPower.Checked;

            var exe = new List<Mod>();
            var db = new List<Mod>();

            if (status_approved)
            {
                exe.AddRange(Mods.Where(m => m.Type == Mod.ModType.EXE && m.Status == Mod.ModStatus.APPROVED));
                db.AddRange(Mods.Where(m => m.Type == Mod.ModType.DATABASE && m.Status == Mod.ModStatus.APPROVED));
            }

            if (status_public)
            {
                exe.AddRange(Mods.Where(m => m.Type == Mod.ModType.EXE && m.Status == Mod.ModStatus.PUBLIC));
                db.AddRange(Mods.Where(m => m.Type == Mod.ModType.DATABASE && m.Status == Mod.ModStatus.PUBLIC));
            }

            if (status_poweruser)
            {
                exe.AddRange(Mods.Where(m => m.Type == Mod.ModType.EXE && m.Status == Mod.ModStatus.POWERUSER));
                db.AddRange(Mods.Where(m => m.Type == Mod.ModType.DATABASE && m.Status == Mod.ModStatus.POWERUSER));
            }

            // exe

            exe.Sort((a, b) => a.Name.CompareTo(b.Name));
            exe.Insert(0, Mod.BaseGame);

            var selected_mod = (Mod)ComboExe.SelectedItem;
            ComboExe.Items.Clear();
            ComboExe.Items.AddRange(exe.ToArray());

            if (exe.Contains(selected_mod))
            {
                ComboExe.SelectedItem = selected_mod;
                if (selected_mod.ConfigFile == null)
                {
                    ButtonConfigureExe.Enabled = false;
                }
                else
                {
                    ButtonConfigureExe.Enabled = true;
                }
            }
            else
            {
                ComboExe.SelectedIndex = 0;
                ButtonConfigureExe.Enabled = false;
            }

            // db

            db.Sort((a, b) => a.Name.CompareTo(b.Name));

            var selected_indices = new List<int>();
            for (int i = 0; i < ListDBMods.CheckedItems.Count; i++)
            {
                for (int j = 0; j < db.Count; j++)
                {
                    if (ListDBMods.CheckedItems[i].Equals(db[j]))
                    {
                        selected_indices.Add(j);
                    }
                }
            }

            ListDBMods.Items.Clear();
            ListDBMods.Items.AddRange(db.ToArray());
            foreach (var index in selected_indices)
            {
                ListDBMods.SetItemChecked(index, true);
            }

            // utility

            utility.Sort((a, b) => a.Name.CompareTo(b.Name));

            selected_indices.Clear();
            for (int i = 0; i < ListUtilityMods.CheckedItems.Count; i++)
            {
                for (int j = 0; j < utility.Count; j++)
                {
                    if (ListUtilityMods.CheckedItems[i].Equals(utility[j]))
                    {
                        selected_indices.Add(j);
                    }
                }
            }

            ListUtilityMods.Items.Clear();
            ListUtilityMods.Items.AddRange(utility.ToArray());
            foreach (var index in selected_indices)
            {
                ListUtilityMods.SetItemChecked(index, true);
            }
        }

        private void UpdateButtons()
        {
            if (!(ListUtilityMods.SelectedItem is Mod utility) || utility.ConfigFile == null)
            {
                ButtonConfigureUtility.Enabled = false;
            }
            else
            {
                ButtonConfigureUtility.Enabled = true;
            }

            if (!(ComboExe.SelectedItem is Mod exe) || exe.ConfigFile == null)
            {
                ButtonConfigureExe.Enabled = false;
            }
            else
            {
                ButtonConfigureExe.Enabled = true;
            }

            if (!(ListDBMods.SelectedItem is Mod db) || db.ConfigFile == null)
            {
                ButtonConfigureDB.Enabled = false;
            }
            else
            {
                ButtonConfigureDB.Enabled = true;
            }

            if (CheckMods.Checked)
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

        private void ConfigureMod(Mod mod)
        {
            var file = Path.Combine(Path.GetDirectoryName(mod.DefFile), mod.ConfigFile);
            var info = new ProcessStartInfo()
            {
                FileName = file,
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
            ButtonUpdateMods.Enabled = false;

            TabThemeMods.Enabled = false;
            TabUtilityMods.Enabled = false;
            TabGameMods.Enabled = false;

            var exe = Mod.BaseGame;
            var others = new List<Mod>();

            if (CheckMods.Checked)
            {
                exe = ComboExe.SelectedItem as Mod;

                for (int i = 0; i < ListDBMods.CheckedItems.Count; i++)
                {
                    others.Add(ListDBMods.CheckedItems[i] as Mod);
                }
            }

            for (int i = 0; i < ListUtilityMods.CheckedItems.Count; i++)
            {
                others.Add(ListUtilityMods.CheckedItems[i] as Mod);
            }

            var process = Launcher.Launch(exe, others);

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
            TabUtilityMods.Enabled = true;
            TabGameMods.Enabled = true;

            LoadVersion();
            LoadMods();
            UpdateLists();
            UpdateButtons();

            Cursor = Cursors.Default;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //Icon = Properties.Resources.Aurora;
            MessageBox.Show("AuroraLoader will check for updates and then launch, this might take a moment.");
            Cursor = Cursors.WaitCursor;

            LoadVersion();
            LoadMods();
            UpdateLists();
            UpdateButtons();

            Cursor = Cursors.Default;

            TabThemeMods.Enabled = false;
            TabMods.SelectedTab = TabGameMods;
        }

        private void CheckMods_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckMods.Checked)
            {
                var result = MessageBox.Show("By using game mods you agree to not post bug reports to the official Aurora bug report channels.", "Warning!", MessageBoxButtons.OKCancel);
                if (result != DialogResult.OK)
                {
                    CheckMods.Checked = false;
                }
            }

            UpdateLists();
            UpdateButtons();
        }

        private void ButtonSinglePlayer_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void CheckApproved_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLists();
        }

        private void CheckPublic_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLists();
        }

        private void CheckPower_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLists();
        }

        private void ButtonAuroraForums_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://aurora2.pentarch.org/index.php?action=forum#c14");
        }

        private void ButtonAuroraBugs_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://aurora2.pentarch.org/index.php?board=273.0");
        }

        private void ButtonAuroraUpdates_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://aurora2.pentarch.org/index.php?board=276.0");
        }

        private void ButtonModsSubreddit_Click(object sender, EventArgs e)
        {
            Process.Start(@"https://www.reddit.com/r/aurora4x_mods/");
        }

        private void ButtonUpdateMods_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Log.Debug("Start updating");

            var urls = ModUpdates;
            if (urls.Count == 0)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("All mods are up to date");
            }
            else
            {
                foreach (var kvp in urls)
                {
                    Log.Debug("Updating: " + kvp.Key.Name + " at " + kvp.Value);
                    try
                    {
                        Updater.Update(kvp.Value);
                    }
                    catch (Exception exc)
                    {
                        Log.Error("Failed to update mod: " + kvp.Key.Name, exc);

                        Cursor = Cursors.Default;
                        MessageBox.Show("Failed to update " + kvp.Key.Name);
                        Cursor = Cursors.WaitCursor;
                    }
                }

                LoadMods();
                UpdateLists();
                UpdateButtons();

                Cursor = Cursors.Default;
                MessageBox.Show("Updated " + urls.Count + " mods.");
            }

            Log.Debug("Stop updating");
        }

        private void ComboExe_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void ButtonConfigureExe_Click(object sender, EventArgs e)
        {
            var selected = ComboExe.SelectedItem as Mod;
            ConfigureMod(selected);
        }

        private void ButtonConfigureDB_Click(object sender, EventArgs e)
        {
            var selected = ListDBMods.SelectedItem as Mod;
            ConfigureMod(selected);
        }

        private void ListDBMods_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void ButtonInstallMods_Click(object sender, EventArgs e)
        {
            var form = new FormInstallMod();
            form.ShowDialog();

            Cursor = Cursors.WaitCursor;

            LoadMods();
            UpdateLists();

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
            var selected = ListUtilityMods.SelectedItem as Mod;
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
    }
}
