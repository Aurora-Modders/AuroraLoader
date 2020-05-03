using System.Windows.Forms;

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
            this.LabelAuroraVersion = new System.Windows.Forms.Label();
            this.ButtonUpdateAurora = new System.Windows.Forms.Button();
            this.TrackMusicVolume = new System.Windows.Forms.TrackBar();
            this.CheckEnableMusic = new System.Windows.Forms.CheckBox();
            this.ButtonUpdateAuroraLoader = new System.Windows.Forms.Button();
            this.ButtonReadme = new System.Windows.Forms.Button();
            this.LabelAuroraLoaderVersion = new System.Windows.Forms.Label();
            this.CheckEnableMods = new System.Windows.Forms.CheckBox();
            this.CheckEnablePoweruserMods = new System.Windows.Forms.CheckBox();
            this.ComboSelectExecutableMod = new System.Windows.Forms.ComboBox();
            this.ListDatabaseMods = new System.Windows.Forms.CheckedListBox();
            this.ListUtilities = new System.Windows.Forms.CheckedListBox();
            this.ButtonInstallOrUpdateMod = new System.Windows.Forms.Button();
            this.ButtonConfigureMod = new System.Windows.Forms.Button();
            this.ListManageMods = new System.Windows.Forms.ListView();
            this.LinkForums = new System.Windows.Forms.LinkLabel();
            this.LinkReportBug = new System.Windows.Forms.LinkLabel();
            this.LinkSubreddit = new System.Windows.Forms.LinkLabel();
            this.LinkDiscord = new System.Windows.Forms.LinkLabel();
            this.LabelUtilities = new System.Windows.Forms.Label();
            this.LabelDatabaseMods = new System.Windows.Forms.Label();
            this.ManageMods = new System.Windows.Forms.Label();
            this.LinkModSubreddit = new System.Windows.Forms.LinkLabel();
            this.ButtonMultiplayer = new System.Windows.Forms.Button();
            this.ButtonManageMods = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TrackMusicVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonSinglePlayer
            // 
            this.ButtonSinglePlayer.Location = new System.Drawing.Point(28, 27);
            this.ButtonSinglePlayer.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonSinglePlayer.Name = "ButtonSinglePlayer";
            this.ButtonSinglePlayer.Size = new System.Drawing.Size(84, 24);
            this.ButtonSinglePlayer.TabIndex = 2;
            this.ButtonSinglePlayer.Text = "Play";
            this.ButtonSinglePlayer.UseVisualStyleBackColor = true;
            this.ButtonSinglePlayer.Click += new System.EventHandler(this.ButtonSinglePlayer_Click);
            // 
            // LabelAuroraVersion
            // 
            this.LabelAuroraVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelAuroraVersion.Location = new System.Drawing.Point(479, 581);
            this.LabelAuroraVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelAuroraVersion.Name = "LabelAuroraVersion";
            this.LabelAuroraVersion.Size = new System.Drawing.Size(133, 15);
            this.LabelAuroraVersion.TabIndex = 7;
            this.LabelAuroraVersion.Text = "Aurora v#.##.#";
            this.LabelAuroraVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ButtonUpdateAurora
            // 
            this.ButtonUpdateAurora.Enabled = false;
            this.ButtonUpdateAurora.Location = new System.Drawing.Point(424, 538);
            this.ButtonUpdateAurora.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonUpdateAurora.Name = "ButtonUpdateAurora";
            this.ButtonUpdateAurora.Size = new System.Drawing.Size(164, 27);
            this.ButtonUpdateAurora.TabIndex = 12;
            this.ButtonUpdateAurora.Text = "Update Aurora";
            this.ButtonUpdateAurora.UseVisualStyleBackColor = true;
            this.ButtonUpdateAurora.Click += new System.EventHandler(this.ButtonUpdateAurora_Click);
            // 
            // TrackMusicVolume
            // 
            this.TrackMusicVolume.Enabled = false;
            this.TrackMusicVolume.LargeChange = 1;
            this.TrackMusicVolume.Location = new System.Drawing.Point(265, 59);
            this.TrackMusicVolume.Margin = new System.Windows.Forms.Padding(4);
            this.TrackMusicVolume.Name = "TrackMusicVolume";
            this.TrackMusicVolume.Size = new System.Drawing.Size(162, 45);
            this.TrackMusicVolume.TabIndex = 20;
            this.TrackMusicVolume.Value = 4;
            // 
            // CheckEnableMusic
            // 
            this.CheckEnableMusic.AutoSize = true;
            this.CheckEnableMusic.Location = new System.Drawing.Point(146, 59);
            this.CheckEnableMusic.Margin = new System.Windows.Forms.Padding(4);
            this.CheckEnableMusic.Name = "CheckEnableMusic";
            this.CheckEnableMusic.Size = new System.Drawing.Size(107, 19);
            this.CheckEnableMusic.TabIndex = 2;
            this.CheckEnableMusic.Text = "In-Game Music";
            this.CheckEnableMusic.UseVisualStyleBackColor = true;
            this.CheckEnableMusic.CheckedChanged += new System.EventHandler(this.CheckMusic_CheckedChanged);
            // 
            // ButtonUpdateAuroraLoader
            // 
            this.ButtonUpdateAuroraLoader.Location = new System.Drawing.Point(251, 538);
            this.ButtonUpdateAuroraLoader.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonUpdateAuroraLoader.Name = "ButtonUpdateAuroraLoader";
            this.ButtonUpdateAuroraLoader.Size = new System.Drawing.Size(164, 27);
            this.ButtonUpdateAuroraLoader.TabIndex = 12;
            this.ButtonUpdateAuroraLoader.Text = "Update Loader";
            this.ButtonUpdateAuroraLoader.UseVisualStyleBackColor = true;
            this.ButtonUpdateAuroraLoader.Click += new System.EventHandler(this.ButtonUpdateAuroraLoader_Click);
            // 
            // ButtonReadme
            // 
            this.ButtonReadme.Location = new System.Drawing.Point(504, 27);
            this.ButtonReadme.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonReadme.Name = "ButtonReadme";
            this.ButtonReadme.Size = new System.Drawing.Size(84, 24);
            this.ButtonReadme.TabIndex = 13;
            this.ButtonReadme.Text = "Readme";
            this.ButtonReadme.UseVisualStyleBackColor = true;
            this.ButtonReadme.Click += new System.EventHandler(this.ButtonReadme_Click);
            // 
            // LabelAuroraLoaderVersion
            // 
            this.LabelAuroraLoaderVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelAuroraLoaderVersion.Location = new System.Drawing.Point(479, 566);
            this.LabelAuroraLoaderVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelAuroraLoaderVersion.Name = "LabelAuroraLoaderVersion";
            this.LabelAuroraLoaderVersion.Size = new System.Drawing.Size(133, 15);
            this.LabelAuroraLoaderVersion.TabIndex = 7;
            this.LabelAuroraLoaderVersion.Text = "Loader v#.##.#";
            this.LabelAuroraLoaderVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.LabelAuroraLoaderVersion.Click += new System.EventHandler(this.LabelAuroraLoaderVersion_Click);
            // 
            // CheckEnableMods
            // 
            this.CheckEnableMods.AutoSize = true;
            this.CheckEnableMods.Location = new System.Drawing.Point(28, 109);
            this.CheckEnableMods.Name = "CheckEnableMods";
            this.CheckEnableMods.Size = new System.Drawing.Size(94, 19);
            this.CheckEnableMods.TabIndex = 21;
            this.CheckEnableMods.Text = "Enable Mods";
            this.CheckEnableMods.UseVisualStyleBackColor = true;
            this.CheckEnableMods.CheckedChanged += new System.EventHandler(this.CheckEnableMods_CheckChanged);
            // 
            // CheckEnablePoweruserMods
            // 
            this.CheckEnablePoweruserMods.AutoSize = true;
            this.CheckEnablePoweruserMods.Location = new System.Drawing.Point(146, 109);
            this.CheckEnablePoweruserMods.Name = "CheckEnablePoweruserMods";
            this.CheckEnablePoweruserMods.Size = new System.Drawing.Size(152, 19);
            this.CheckEnablePoweruserMods.TabIndex = 23;
            this.CheckEnablePoweruserMods.Text = "Enable Poweruser Mods";
            this.CheckEnablePoweruserMods.UseVisualStyleBackColor = true;
            this.CheckEnablePoweruserMods.CheckedChanged += new System.EventHandler(this.CheckEnablePoweruserMod_CheckChanged);
            // 
            // ComboSelectExecutableMod
            // 
            this.ComboSelectExecutableMod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboSelectExecutableMod.FormattingEnabled = true;
            this.ComboSelectExecutableMod.Location = new System.Drawing.Point(146, 27);
            this.ComboSelectExecutableMod.Name = "ComboSelectExecutableMod";
            this.ComboSelectExecutableMod.Size = new System.Drawing.Size(241, 23);
            this.ComboSelectExecutableMod.TabIndex = 25;
            // 
            // ListDatabaseMods
            // 
            this.ListDatabaseMods.FormattingEnabled = true;
            this.ListDatabaseMods.Location = new System.Drawing.Point(326, 167);
            this.ListDatabaseMods.Name = "ListDatabaseMods";
            this.ListDatabaseMods.Size = new System.Drawing.Size(264, 76);
            this.ListDatabaseMods.TabIndex = 26;
            // 
            // ListUtilities
            // 
            this.ListUtilities.FormattingEnabled = true;
            this.ListUtilities.Location = new System.Drawing.Point(28, 167);
            this.ListUtilities.Name = "ListUtilities";
            this.ListUtilities.Size = new System.Drawing.Size(264, 76);
            this.ListUtilities.TabIndex = 27;
            this.ListUtilities.SelectedIndexChanged += new System.EventHandler(this.ListUtilityMods_SelectedIndexChanged);
            // 
            // ButtonInstallOrUpdateMod
            // 
            this.ButtonInstallOrUpdateMod.Location = new System.Drawing.Point(28, 433);
            this.ButtonInstallOrUpdateMod.Name = "ButtonInstallOrUpdateMod";
            this.ButtonInstallOrUpdateMod.Size = new System.Drawing.Size(94, 27);
            this.ButtonInstallOrUpdateMod.TabIndex = 29;
            this.ButtonInstallOrUpdateMod.Text = "Install";
            this.ButtonInstallOrUpdateMod.UseVisualStyleBackColor = true;
            this.ButtonInstallOrUpdateMod.Click += new System.EventHandler(this.ButtonInstallOrUpdateMods_Click);
            // 
            // ButtonConfigureMod
            // 
            this.ButtonConfigureMod.Location = new System.Drawing.Point(128, 433);
            this.ButtonConfigureMod.Name = "ButtonConfigureMod";
            this.ButtonConfigureMod.Size = new System.Drawing.Size(94, 27);
            this.ButtonConfigureMod.TabIndex = 30;
            this.ButtonConfigureMod.Text = "Configure";
            this.ButtonConfigureMod.UseVisualStyleBackColor = true;
            this.ButtonConfigureMod.Click += new System.EventHandler(this.ButtonConfigureMod_Click);
            // 
            // ListManageMods
            // 
            this.ListManageMods.HideSelection = false;
            this.ListManageMods.Location = new System.Drawing.Point(28, 293);
            this.ListManageMods.Name = "ListManageMods";
            this.ListManageMods.Size = new System.Drawing.Size(562, 134);
            this.ListManageMods.TabIndex = 32;
            this.ListManageMods.UseCompatibleStateImageBehavior = false;
            this.ListManageMods.SelectedIndexChanged += new System.EventHandler(this.ListManageMods_SelectedIndexChanged);
            // 
            // LinkForums
            // 
            this.LinkForums.AutoSize = true;
            this.LinkForums.Location = new System.Drawing.Point(496, 51);
            this.LinkForums.Name = "LinkForums";
            this.LinkForums.Size = new System.Drawing.Size(88, 15);
            this.LinkForums.TabIndex = 33;
            this.LinkForums.TabStop = true;
            this.LinkForums.Text = "Official Forums";
            this.LinkForums.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkForums_LinkClicked);
            // 
            // LinkReportBug
            // 
            this.LinkReportBug.AutoSize = true;
            this.LinkReportBug.Location = new System.Drawing.Point(508, 111);
            this.LinkReportBug.Name = "LinkReportBug";
            this.LinkReportBug.Size = new System.Drawing.Size(75, 15);
            this.LinkReportBug.TabIndex = 34;
            this.LinkReportBug.TabStop = true;
            this.LinkReportBug.Text = "Report a Bug";
            this.LinkReportBug.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkVanillaBug_LinkClicked);
            // 
            // LinkSubreddit
            // 
            this.LinkSubreddit.AutoSize = true;
            this.LinkSubreddit.Location = new System.Drawing.Point(484, 66);
            this.LinkSubreddit.Name = "LinkSubreddit";
            this.LinkSubreddit.Size = new System.Drawing.Size(97, 15);
            this.LinkSubreddit.TabIndex = 35;
            this.LinkSubreddit.TabStop = true;
            this.LinkSubreddit.Text = "Aurora Subreddit";
            this.LinkSubreddit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkVanillaSubreddit_LinkClicked);
            // 
            // LinkDiscord
            // 
            this.LinkDiscord.AutoSize = true;
            this.LinkDiscord.Location = new System.Drawing.Point(537, 96);
            this.LinkDiscord.Name = "LinkDiscord";
            this.LinkDiscord.Size = new System.Drawing.Size(47, 15);
            this.LinkDiscord.TabIndex = 36;
            this.LinkDiscord.TabStop = true;
            this.LinkDiscord.Text = "Discord";
            this.LinkDiscord.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkDiscord_LinkClicked);
            // 
            // LabelUtilities
            // 
            this.LabelUtilities.AutoSize = true;
            this.LabelUtilities.Location = new System.Drawing.Point(28, 149);
            this.LabelUtilities.Name = "LabelUtilities";
            this.LabelUtilities.Size = new System.Drawing.Size(183, 15);
            this.LabelUtilities.TabIndex = 38;
            this.LabelUtilities.Text = "Launch utilities alongside Aurora:";
            // 
            // LabelDatabaseMods
            // 
            this.LabelDatabaseMods.AutoSize = true;
            this.LabelDatabaseMods.Location = new System.Drawing.Point(326, 149);
            this.LabelDatabaseMods.Name = "LabelDatabaseMods";
            this.LabelDatabaseMods.Size = new System.Drawing.Size(124, 15);
            this.LabelDatabaseMods.TabIndex = 39;
            this.LabelDatabaseMods.Text = "Apply database mods:";
            // 
            // ManageMods
            // 
            this.ManageMods.AutoSize = true;
            this.ManageMods.Location = new System.Drawing.Point(28, 275);
            this.ManageMods.Name = "ManageMods";
            this.ManageMods.Size = new System.Drawing.Size(86, 15);
            this.ManageMods.TabIndex = 40;
            this.ManageMods.Text = "Manage mods:";
            // 
            // LinkModSubreddit
            // 
            this.LinkModSubreddit.AutoSize = true;
            this.LinkModSubreddit.Location = new System.Drawing.Point(495, 81);
            this.LinkModSubreddit.Name = "LinkModSubreddit";
            this.LinkModSubreddit.Size = new System.Drawing.Size(86, 15);
            this.LinkModSubreddit.TabIndex = 35;
            this.LinkModSubreddit.TabStop = true;
            this.LinkModSubreddit.Text = "Mod Subreddit";
            this.LinkModSubreddit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkVanillaSubreddit_LinkClicked);
            // 
            // ButtonMultiplayer
            // 
            this.ButtonMultiplayer.Location = new System.Drawing.Point(28, 59);
            this.ButtonMultiplayer.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonMultiplayer.Name = "ButtonMultiplayer";
            this.ButtonMultiplayer.Size = new System.Drawing.Size(105, 24);
            this.ButtonMultiplayer.TabIndex = 2;
            this.ButtonMultiplayer.Text = "Play Multiplayer";
            this.ButtonMultiplayer.UseVisualStyleBackColor = true;
            this.ButtonMultiplayer.Click += new System.EventHandler(this.ButtonSinglePlayer_Click);
            // 
            // ButtonManageMods
            // 
            this.ButtonManageMods.Location = new System.Drawing.Point(80, 538);
            this.ButtonManageMods.Name = "ButtonManageMods";
            this.ButtonManageMods.Size = new System.Drawing.Size(164, 27);
            this.ButtonManageMods.TabIndex = 41;
            this.ButtonManageMods.Text = "Manage Mods";
            this.ButtonManageMods.UseVisualStyleBackColor = true;
            this.ButtonManageMods.Click += new System.EventHandler(this.ButtonManageMods_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 607);
            this.Controls.Add(this.ButtonManageMods);
            this.Controls.Add(this.ButtonMultiplayer);
            this.Controls.Add(this.LinkModSubreddit);
            this.Controls.Add(this.ManageMods);
            this.Controls.Add(this.LabelDatabaseMods);
            this.Controls.Add(this.LabelUtilities);
            this.Controls.Add(this.LinkDiscord);
            this.Controls.Add(this.LinkSubreddit);
            this.Controls.Add(this.LinkReportBug);
            this.Controls.Add(this.LinkForums);
            this.Controls.Add(this.ListManageMods);
            this.Controls.Add(this.ButtonConfigureMod);
            this.Controls.Add(this.ButtonInstallOrUpdateMod);
            this.Controls.Add(this.ListUtilities);
            this.Controls.Add(this.ListDatabaseMods);
            this.Controls.Add(this.ComboSelectExecutableMod);
            this.Controls.Add(this.CheckEnablePoweruserMods);
            this.Controls.Add(this.CheckEnableMods);
            this.Controls.Add(this.LabelAuroraLoaderVersion);
            this.Controls.Add(this.ButtonReadme);
            this.Controls.Add(this.ButtonUpdateAuroraLoader);
            this.Controls.Add(this.CheckEnableMusic);
            this.Controls.Add(this.TrackMusicVolume);
            this.Controls.Add(this.ButtonUpdateAurora);
            this.Controls.Add(this.LabelAuroraVersion);
            this.Controls.Add(this.ButtonSinglePlayer);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(635, 646);
            this.Name = "FormMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aurora Loader";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TrackMusicVolume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ButtonSinglePlayer;
        private System.Windows.Forms.Label LabelAuroraVersion;
        private System.Windows.Forms.Button ButtonUpdateAurora;
        private System.Windows.Forms.TrackBar TrackMusicVolume;
        private System.Windows.Forms.CheckBox CheckEnableMusic;
        private System.Windows.Forms.Button ButtonUpdateAuroraLoader;
        private System.Windows.Forms.Button ButtonReadme;
        private System.Windows.Forms.Label LabelAuroraLoaderVersion;
        private System.Windows.Forms.CheckBox CheckEnableMods;
        private System.Windows.Forms.CheckBox CheckEnablePoweruserMods;
        private System.Windows.Forms.ComboBox ComboSelectExecutableMod;
        private System.Windows.Forms.CheckedListBox ListDatabaseMods;
        private System.Windows.Forms.CheckedListBox ListUtilities;
        private System.Windows.Forms.Button ButtonInstallOrUpdateMod;
        private System.Windows.Forms.Button ButtonConfigureMod;
        private System.Windows.Forms.ListView ListManageMods;
        private System.Windows.Forms.LinkLabel LinkForums;
        private System.Windows.Forms.LinkLabel LinkReportBug;
        private System.Windows.Forms.LinkLabel LinkSubreddit;
        private System.Windows.Forms.LinkLabel LinkDiscord;
        private System.Windows.Forms.Label LabelUtilities;
        private System.Windows.Forms.Label LabelDatabaseMods;
        private System.Windows.Forms.Label ManageMods;
        private LinkLabel LinkModSubreddit;
        private Button ButtonMultiplayer;
        private Button ButtonManageMods;
    }
}

