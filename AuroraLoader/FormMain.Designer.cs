namespace AuroraLoader
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ButtonSinglePlayer = new System.Windows.Forms.Button();
            this.LabelVersion = new System.Windows.Forms.Label();
            this.LabelChecksum = new System.Windows.Forms.Label();
            this.ButtonAuroraForums = new System.Windows.Forms.Button();
            this.ButtonAuroraBugs = new System.Windows.Forms.Button();
            this.ButtonUpdateAurora = new System.Windows.Forms.Button();
            this.ButtonModSubreddit = new System.Windows.Forms.Button();
            this.ButtonUpdateMods = new System.Windows.Forms.Button();
            this.ButtonInstallMods = new System.Windows.Forms.Button();
            this.ButtonMultiPlayer = new System.Windows.Forms.Button();
            this.ButtonInstallAurora = new System.Windows.Forms.Button();
            this.ButtonModBugs = new System.Windows.Forms.Button();
            this.TrackVolume = new System.Windows.Forms.TrackBar();
            this.CheckMusic = new System.Windows.Forms.CheckBox();
            this.TabGameMods = new System.Windows.Forms.TabPage();
            this.GroupMods = new System.Windows.Forms.GroupBox();
            this.ButtonConfigureDB = new System.Windows.Forms.Button();
            this.ButtonConfigureExe = new System.Windows.Forms.Button();
            this.ListDBMods = new System.Windows.Forms.CheckedListBox();
            this.LabelDBMods = new System.Windows.Forms.Label();
            this.LabelExeMod = new System.Windows.Forms.Label();
            this.ComboExe = new System.Windows.Forms.ComboBox();
            this.CheckPower = new System.Windows.Forms.CheckBox();
            this.CheckPublic = new System.Windows.Forms.CheckBox();
            this.CheckApproved = new System.Windows.Forms.CheckBox();
            this.CheckMods = new System.Windows.Forms.CheckBox();
            this.TabUtilityMods = new System.Windows.Forms.TabPage();
            this.ButtonConfigureUtility = new System.Windows.Forms.Button();
            this.ListUtilityMods = new System.Windows.Forms.CheckedListBox();
            this.TabMods = new System.Windows.Forms.TabControl();
            this.modsTab = new System.Windows.Forms.TabPage();
            this.allModsListView = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.TrackVolume)).BeginInit();
            this.TabGameMods.SuspendLayout();
            this.GroupMods.SuspendLayout();
            this.TabUtilityMods.SuspendLayout();
            this.TabMods.SuspendLayout();
            this.modsTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonSinglePlayer
            // 
            this.ButtonSinglePlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ButtonSinglePlayer.Location = new System.Drawing.Point(20, 213);
            this.ButtonSinglePlayer.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ButtonSinglePlayer.Name = "ButtonSinglePlayer";
            this.ButtonSinglePlayer.Size = new System.Drawing.Size(373, 88);
            this.ButtonSinglePlayer.TabIndex = 2;
            this.ButtonSinglePlayer.Text = "Single Player";
            this.ButtonSinglePlayer.UseVisualStyleBackColor = true;
            this.ButtonSinglePlayer.Click += new System.EventHandler(this.ButtonSinglePlayer_Click);
            // 
            // LabelVersion
            // 
            this.LabelVersion.AutoSize = true;
            this.LabelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelVersion.Location = new System.Drawing.Point(42, 46);
            this.LabelVersion.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.LabelVersion.Name = "LabelVersion";
            this.LabelVersion.Size = new System.Drawing.Size(280, 29);
            this.LabelVersion.TabIndex = 7;
            this.LabelVersion.Text = "Aurora version: Unknown";
            // 
            // LabelChecksum
            // 
            this.LabelChecksum.AutoSize = true;
            this.LabelChecksum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelChecksum.Location = new System.Drawing.Point(42, 96);
            this.LabelChecksum.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.LabelChecksum.Name = "LabelChecksum";
            this.LabelChecksum.Size = new System.Drawing.Size(204, 29);
            this.LabelChecksum.TabIndex = 8;
            this.LabelChecksum.Text = "Aurora checksum:";
            // 
            // ButtonAuroraForums
            // 
            this.ButtonAuroraForums.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ButtonAuroraForums.Location = new System.Drawing.Point(20, 706);
            this.ButtonAuroraForums.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ButtonAuroraForums.Name = "ButtonAuroraForums";
            this.ButtonAuroraForums.Size = new System.Drawing.Size(373, 88);
            this.ButtonAuroraForums.TabIndex = 9;
            this.ButtonAuroraForums.Text = "Aurora Forums";
            this.ButtonAuroraForums.UseVisualStyleBackColor = true;
            this.ButtonAuroraForums.Click += new System.EventHandler(this.ButtonAuroraForums_Click);
            // 
            // ButtonAuroraBugs
            // 
            this.ButtonAuroraBugs.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ButtonAuroraBugs.ForeColor = System.Drawing.Color.OrangeRed;
            this.ButtonAuroraBugs.Location = new System.Drawing.Point(20, 806);
            this.ButtonAuroraBugs.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ButtonAuroraBugs.Name = "ButtonAuroraBugs";
            this.ButtonAuroraBugs.Size = new System.Drawing.Size(373, 88);
            this.ButtonAuroraBugs.TabIndex = 11;
            this.ButtonAuroraBugs.Text = "Report a bug";
            this.ButtonAuroraBugs.UseVisualStyleBackColor = true;
            this.ButtonAuroraBugs.Click += new System.EventHandler(this.ButtonAuroraBugs_Click);
            // 
            // ButtonUpdateAurora
            // 
            this.ButtonUpdateAurora.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ButtonUpdateAurora.Location = new System.Drawing.Point(20, 606);
            this.ButtonUpdateAurora.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ButtonUpdateAurora.Name = "ButtonUpdateAurora";
            this.ButtonUpdateAurora.Size = new System.Drawing.Size(373, 88);
            this.ButtonUpdateAurora.TabIndex = 12;
            this.ButtonUpdateAurora.Text = "Update Aurora";
            this.ButtonUpdateAurora.UseVisualStyleBackColor = true;
            this.ButtonUpdateAurora.Click += new System.EventHandler(this.ButtonAuroraUpdates_Click);
            // 
            // ButtonModSubreddit
            // 
            this.ButtonModSubreddit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ButtonModSubreddit.Location = new System.Drawing.Point(403, 706);
            this.ButtonModSubreddit.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ButtonModSubreddit.Name = "ButtonModSubreddit";
            this.ButtonModSubreddit.Size = new System.Drawing.Size(373, 88);
            this.ButtonModSubreddit.TabIndex = 13;
            this.ButtonModSubreddit.Text = "Mod subreddit";
            this.ButtonModSubreddit.UseVisualStyleBackColor = true;
            this.ButtonModSubreddit.Click += new System.EventHandler(this.ButtonModsSubreddit_Click);
            // 
            // ButtonUpdateMods
            // 
            this.ButtonUpdateMods.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ButtonUpdateMods.Location = new System.Drawing.Point(403, 606);
            this.ButtonUpdateMods.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ButtonUpdateMods.Name = "ButtonUpdateMods";
            this.ButtonUpdateMods.Size = new System.Drawing.Size(373, 88);
            this.ButtonUpdateMods.TabIndex = 14;
            this.ButtonUpdateMods.Text = "Update mods";
            this.ButtonUpdateMods.UseVisualStyleBackColor = true;
            this.ButtonUpdateMods.Click += new System.EventHandler(this.ButtonUpdateMods_Click);
            // 
            // ButtonInstallMods
            // 
            this.ButtonInstallMods.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ButtonInstallMods.Location = new System.Drawing.Point(403, 506);
            this.ButtonInstallMods.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ButtonInstallMods.Name = "ButtonInstallMods";
            this.ButtonInstallMods.Size = new System.Drawing.Size(373, 88);
            this.ButtonInstallMods.TabIndex = 15;
            this.ButtonInstallMods.Text = "Install mods";
            this.ButtonInstallMods.UseVisualStyleBackColor = true;
            this.ButtonInstallMods.Click += new System.EventHandler(this.ButtonInstallMods_Click);
            // 
            // ButtonMultiPlayer
            // 
            this.ButtonMultiPlayer.Enabled = false;
            this.ButtonMultiPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ButtonMultiPlayer.Location = new System.Drawing.Point(403, 213);
            this.ButtonMultiPlayer.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ButtonMultiPlayer.Name = "ButtonMultiPlayer";
            this.ButtonMultiPlayer.Size = new System.Drawing.Size(373, 88);
            this.ButtonMultiPlayer.TabIndex = 17;
            this.ButtonMultiPlayer.Text = "Multi Player";
            this.ButtonMultiPlayer.UseVisualStyleBackColor = true;
            // 
            // ButtonInstallAurora
            // 
            this.ButtonInstallAurora.Enabled = false;
            this.ButtonInstallAurora.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ButtonInstallAurora.Location = new System.Drawing.Point(20, 506);
            this.ButtonInstallAurora.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ButtonInstallAurora.Name = "ButtonInstallAurora";
            this.ButtonInstallAurora.Size = new System.Drawing.Size(373, 88);
            this.ButtonInstallAurora.TabIndex = 18;
            this.ButtonInstallAurora.Text = "Install Aurora";
            this.ButtonInstallAurora.UseVisualStyleBackColor = true;
            // 
            // ButtonModBugs
            // 
            this.ButtonModBugs.Enabled = false;
            this.ButtonModBugs.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ButtonModBugs.Location = new System.Drawing.Point(403, 806);
            this.ButtonModBugs.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ButtonModBugs.Name = "ButtonModBugs";
            this.ButtonModBugs.Size = new System.Drawing.Size(373, 88);
            this.ButtonModBugs.TabIndex = 19;
            this.ButtonModBugs.Text = "Report a bug";
            this.ButtonModBugs.UseVisualStyleBackColor = true;
            this.ButtonModBugs.Click += new System.EventHandler(this.ButtonModBugs_Click);
            // 
            // TrackVolume
            // 
            this.TrackVolume.Enabled = false;
            this.TrackVolume.LargeChange = 1;
            this.TrackVolume.Location = new System.Drawing.Point(162, 321);
            this.TrackVolume.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.TrackVolume.Name = "TrackVolume";
            this.TrackVolume.Size = new System.Drawing.Size(232, 69);
            this.TrackVolume.TabIndex = 20;
            // 
            // CheckMusic
            // 
            this.CheckMusic.AutoSize = true;
            this.CheckMusic.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CheckMusic.Location = new System.Drawing.Point(20, 321);
            this.CheckMusic.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.CheckMusic.Name = "CheckMusic";
            this.CheckMusic.Size = new System.Drawing.Size(118, 37);
            this.CheckMusic.TabIndex = 2;
            this.CheckMusic.Text = "Music";
            this.CheckMusic.UseVisualStyleBackColor = true;
            this.CheckMusic.CheckedChanged += new System.EventHandler(this.CheckMusic_CheckedChanged);
            // 
            // TabGameMods
            // 
            this.TabGameMods.BackColor = System.Drawing.SystemColors.Control;
            this.TabGameMods.Controls.Add(this.GroupMods);
            this.TabGameMods.Controls.Add(this.CheckMods);
            this.TabGameMods.Location = new System.Drawing.Point(4, 38);
            this.TabGameMods.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.TabGameMods.Name = "TabGameMods";
            this.TabGameMods.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.TabGameMods.Size = new System.Drawing.Size(770, 1010);
            this.TabGameMods.TabIndex = 2;
            this.TabGameMods.Text = "Game mods";
            // 
            // GroupMods
            // 
            this.GroupMods.Controls.Add(this.ButtonConfigureDB);
            this.GroupMods.Controls.Add(this.ButtonConfigureExe);
            this.GroupMods.Controls.Add(this.ListDBMods);
            this.GroupMods.Controls.Add(this.LabelDBMods);
            this.GroupMods.Controls.Add(this.LabelExeMod);
            this.GroupMods.Controls.Add(this.ComboExe);
            this.GroupMods.Controls.Add(this.CheckPower);
            this.GroupMods.Controls.Add(this.CheckPublic);
            this.GroupMods.Controls.Add(this.CheckApproved);
            this.GroupMods.Enabled = false;
            this.GroupMods.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GroupMods.Location = new System.Drawing.Point(35, 92);
            this.GroupMods.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.GroupMods.Name = "GroupMods";
            this.GroupMods.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.GroupMods.Size = new System.Drawing.Size(698, 877);
            this.GroupMods.TabIndex = 0;
            this.GroupMods.TabStop = false;
            this.GroupMods.Text = "Game mods";
            // 
            // ButtonConfigureDB
            // 
            this.ButtonConfigureDB.Enabled = false;
            this.ButtonConfigureDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ButtonConfigureDB.Location = new System.Drawing.Point(217, 238);
            this.ButtonConfigureDB.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ButtonConfigureDB.Name = "ButtonConfigureDB";
            this.ButtonConfigureDB.Size = new System.Drawing.Size(458, 63);
            this.ButtonConfigureDB.TabIndex = 16;
            this.ButtonConfigureDB.Text = "Configure selected";
            this.ButtonConfigureDB.UseVisualStyleBackColor = true;
            this.ButtonConfigureDB.Click += new System.EventHandler(this.ButtonConfigureDB_Click);
            // 
            // ButtonConfigureExe
            // 
            this.ButtonConfigureExe.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ButtonConfigureExe.Location = new System.Drawing.Point(450, 138);
            this.ButtonConfigureExe.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ButtonConfigureExe.Name = "ButtonConfigureExe";
            this.ButtonConfigureExe.Size = new System.Drawing.Size(238, 63);
            this.ButtonConfigureExe.TabIndex = 15;
            this.ButtonConfigureExe.Text = "Configure";
            this.ButtonConfigureExe.UseVisualStyleBackColor = true;
            this.ButtonConfigureExe.Click += new System.EventHandler(this.ButtonConfigureExe_Click);
            // 
            // ListDBMods
            // 
            this.ListDBMods.FormattingEnabled = true;
            this.ListDBMods.Location = new System.Drawing.Point(3, 313);
            this.ListDBMods.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ListDBMods.Name = "ListDBMods";
            this.ListDBMods.Size = new System.Drawing.Size(669, 516);
            this.ListDBMods.TabIndex = 6;
            this.ListDBMods.SelectedIndexChanged += new System.EventHandler(this.ListDBMods_SelectedIndexChanged);
            // 
            // LabelDBMods
            // 
            this.LabelDBMods.AutoSize = true;
            this.LabelDBMods.Location = new System.Drawing.Point(-3, 250);
            this.LabelDBMods.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.LabelDBMods.Name = "LabelDBMods";
            this.LabelDBMods.Size = new System.Drawing.Size(187, 29);
            this.LabelDBMods.TabIndex = 5;
            this.LabelDBMods.Text = "Database mods:";
            // 
            // LabelExeMod
            // 
            this.LabelExeMod.AutoSize = true;
            this.LabelExeMod.Location = new System.Drawing.Point(3, 150);
            this.LabelExeMod.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.LabelExeMod.Name = "LabelExeMod";
            this.LabelExeMod.Size = new System.Drawing.Size(114, 29);
            this.LabelExeMod.TabIndex = 4;
            this.LabelExeMod.Text = "Exe mod:";
            // 
            // ComboExe
            // 
            this.ComboExe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboExe.FormattingEnabled = true;
            this.ComboExe.Items.AddRange(new object[] {
            "Base Game"});
            this.ComboExe.Location = new System.Drawing.Point(138, 144);
            this.ComboExe.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ComboExe.Name = "ComboExe";
            this.ComboExe.Size = new System.Drawing.Size(299, 37);
            this.ComboExe.TabIndex = 3;
            this.ComboExe.SelectedIndexChanged += new System.EventHandler(this.ComboExe_SelectedIndexChanged);
            // 
            // CheckPower
            // 
            this.CheckPower.AutoSize = true;
            this.CheckPower.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CheckPower.Location = new System.Drawing.Point(450, 67);
            this.CheckPower.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.CheckPower.Name = "CheckPower";
            this.CheckPower.Size = new System.Drawing.Size(222, 33);
            this.CheckPower.TabIndex = 2;
            this.CheckPower.Text = "Poweruser mods";
            this.CheckPower.UseVisualStyleBackColor = true;
            this.CheckPower.CheckedChanged += new System.EventHandler(this.CheckPower_CheckedChanged);
            // 
            // CheckPublic
            // 
            this.CheckPublic.AutoSize = true;
            this.CheckPublic.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CheckPublic.Location = new System.Drawing.Point(252, 67);
            this.CheckPublic.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.CheckPublic.Name = "CheckPublic";
            this.CheckPublic.Size = new System.Drawing.Size(172, 33);
            this.CheckPublic.TabIndex = 2;
            this.CheckPublic.Text = "Public mods";
            this.CheckPublic.UseVisualStyleBackColor = true;
            this.CheckPublic.CheckedChanged += new System.EventHandler(this.CheckPublic_CheckedChanged);
            // 
            // CheckApproved
            // 
            this.CheckApproved.AutoSize = true;
            this.CheckApproved.Checked = true;
            this.CheckApproved.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckApproved.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CheckApproved.Location = new System.Drawing.Point(10, 67);
            this.CheckApproved.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.CheckApproved.Name = "CheckApproved";
            this.CheckApproved.Size = new System.Drawing.Size(209, 33);
            this.CheckApproved.TabIndex = 2;
            this.CheckApproved.Text = "Approved mods";
            this.CheckApproved.UseVisualStyleBackColor = true;
            this.CheckApproved.CheckedChanged += new System.EventHandler(this.CheckApproved_CheckedChanged);
            // 
            // CheckMods
            // 
            this.CheckMods.AutoSize = true;
            this.CheckMods.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CheckMods.Location = new System.Drawing.Point(35, 35);
            this.CheckMods.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.CheckMods.Name = "CheckMods";
            this.CheckMods.Size = new System.Drawing.Size(248, 33);
            this.CheckMods.TabIndex = 1;
            this.CheckMods.Text = "Enable game mods";
            this.CheckMods.UseVisualStyleBackColor = true;
            this.CheckMods.CheckedChanged += new System.EventHandler(this.CheckMods_CheckedChanged);
            // 
            // TabUtilityMods
            // 
            this.TabUtilityMods.BackColor = System.Drawing.SystemColors.Control;
            this.TabUtilityMods.Controls.Add(this.ButtonConfigureUtility);
            this.TabUtilityMods.Controls.Add(this.ListUtilityMods);
            this.TabUtilityMods.Location = new System.Drawing.Point(4, 38);
            this.TabUtilityMods.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.TabUtilityMods.Name = "TabUtilityMods";
            this.TabUtilityMods.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.TabUtilityMods.Size = new System.Drawing.Size(770, 1010);
            this.TabUtilityMods.TabIndex = 1;
            this.TabUtilityMods.Text = "Utility mods";
            // 
            // ButtonConfigureUtility
            // 
            this.ButtonConfigureUtility.Enabled = false;
            this.ButtonConfigureUtility.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ButtonConfigureUtility.Location = new System.Drawing.Point(263, 46);
            this.ButtonConfigureUtility.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ButtonConfigureUtility.Name = "ButtonConfigureUtility";
            this.ButtonConfigureUtility.Size = new System.Drawing.Size(458, 63);
            this.ButtonConfigureUtility.TabIndex = 17;
            this.ButtonConfigureUtility.Text = "Configure selected";
            this.ButtonConfigureUtility.UseVisualStyleBackColor = true;
            this.ButtonConfigureUtility.Click += new System.EventHandler(this.ButtonConfigureUtility_Click);
            // 
            // ListUtilityMods
            // 
            this.ListUtilityMods.FormattingEnabled = true;
            this.ListUtilityMods.Location = new System.Drawing.Point(10, 121);
            this.ListUtilityMods.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ListUtilityMods.Name = "ListUtilityMods";
            this.ListUtilityMods.Size = new System.Drawing.Size(742, 836);
            this.ListUtilityMods.TabIndex = 17;
            this.ListUtilityMods.SelectedIndexChanged += new System.EventHandler(this.ListUtilityMods_SelectedIndexChanged);
            // 
            // TabMods
            // 
            this.TabMods.Controls.Add(this.TabUtilityMods);
            this.TabMods.Controls.Add(this.TabGameMods);
            this.TabMods.Controls.Add(this.modsTab);
            this.TabMods.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TabMods.Location = new System.Drawing.Point(840, 23);
            this.TabMods.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.TabMods.Name = "TabMods";
            this.TabMods.SelectedIndex = 2;
            this.TabMods.Size = new System.Drawing.Size(778, 1052);
            this.TabMods.TabIndex = 16;
            this.TabMods.SelectedIndexChanged += new System.EventHandler(this.TabMods_SelectedIndexChanged);
            // 
            // modsTab
            // 
            this.modsTab.Controls.Add(this.allModsListView);
            this.modsTab.Location = new System.Drawing.Point(4, 38);
            this.modsTab.Name = "modsTab";
            this.modsTab.Size = new System.Drawing.Size(770, 1010);
            this.modsTab.TabIndex = 3;
            this.modsTab.Text = "Mods";
            // 
            // allModsListView
            // 
            this.allModsListView.HideSelection = false;
            this.allModsListView.Location = new System.Drawing.Point(0, 3);
            this.allModsListView.Name = "allModsListView";
            this.allModsListView.Size = new System.Drawing.Size(767, 574);
            this.allModsListView.TabIndex = 0;
            this.allModsListView.UseCompatibleStateImageBehavior = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1635, 1088);
            this.Controls.Add(this.CheckMusic);
            this.Controls.Add(this.TrackVolume);
            this.Controls.Add(this.ButtonModBugs);
            this.Controls.Add(this.ButtonInstallAurora);
            this.Controls.Add(this.ButtonUpdateMods);
            this.Controls.Add(this.ButtonInstallMods);
            this.Controls.Add(this.ButtonMultiPlayer);
            this.Controls.Add(this.TabMods);
            this.Controls.Add(this.ButtonModSubreddit);
            this.Controls.Add(this.ButtonUpdateAurora);
            this.Controls.Add(this.ButtonAuroraBugs);
            this.Controls.Add(this.ButtonAuroraForums);
            this.Controls.Add(this.LabelChecksum);
            this.Controls.Add(this.LabelVersion);
            this.Controls.Add(this.ButtonSinglePlayer);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aurora Loader";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TrackVolume)).EndInit();
            this.TabGameMods.ResumeLayout(false);
            this.TabGameMods.PerformLayout();
            this.GroupMods.ResumeLayout(false);
            this.GroupMods.PerformLayout();
            this.TabUtilityMods.ResumeLayout(false);
            this.TabMods.ResumeLayout(false);
            this.modsTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ButtonSinglePlayer;
        private System.Windows.Forms.Label LabelVersion;
        private System.Windows.Forms.Label LabelChecksum;
        private System.Windows.Forms.Button ButtonAuroraForums;
        private System.Windows.Forms.Button ButtonAuroraBugs;
        private System.Windows.Forms.Button ButtonUpdateAurora;
        private System.Windows.Forms.Button ButtonModSubreddit;
        private System.Windows.Forms.Button ButtonUpdateMods;
        private System.Windows.Forms.Button ButtonInstallMods;
        private System.Windows.Forms.Button ButtonMultiPlayer;
        private System.Windows.Forms.Button ButtonInstallAurora;
        private System.Windows.Forms.Button ButtonModBugs;
        private System.Windows.Forms.TrackBar TrackVolume;
        private System.Windows.Forms.CheckBox CheckMusic;
        private System.Windows.Forms.TabPage TabGameMods;
        private System.Windows.Forms.GroupBox GroupMods;
        private System.Windows.Forms.Button ButtonConfigureDB;
        private System.Windows.Forms.Button ButtonConfigureExe;
        private System.Windows.Forms.CheckedListBox ListDBMods;
        private System.Windows.Forms.Label LabelDBMods;
        private System.Windows.Forms.Label LabelExeMod;
        private System.Windows.Forms.ComboBox ComboExe;
        private System.Windows.Forms.CheckBox CheckPower;
        private System.Windows.Forms.CheckBox CheckPublic;
        private System.Windows.Forms.CheckBox CheckApproved;
        private System.Windows.Forms.CheckBox CheckMods;
        private System.Windows.Forms.TabPage TabUtilityMods;
        private System.Windows.Forms.Button ButtonConfigureUtility;
        private System.Windows.Forms.CheckedListBox ListUtilityMods;
        private System.Windows.Forms.TabControl TabMods;
        private System.Windows.Forms.TabPage modsTab;
        private System.Windows.Forms.ListView allModsListView;
    }
}

