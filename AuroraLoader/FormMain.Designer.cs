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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.ButtonSinglePlayer = new System.Windows.Forms.Button();
            this.LabelAuroraVersion = new System.Windows.Forms.Label();
            this.TrackMusicVolume = new System.Windows.Forms.TrackBar();
            this.CheckEnableMusic = new System.Windows.Forms.CheckBox();
            this.ButtonReadme = new System.Windows.Forms.Button();
            this.ButtonChangelog = new System.Windows.Forms.Button();
            this.LabelAuroraLoaderVersion = new System.Windows.Forms.Label();
            this.CheckEnableMods = new System.Windows.Forms.CheckBox();
            this.CheckEnablePoweruserMods = new System.Windows.Forms.CheckBox();
            this.ComboSelectExecutableMod = new System.Windows.Forms.ComboBox();
            this.ListDatabaseMods = new System.Windows.Forms.CheckedListBox();
            this.ListUtilities = new System.Windows.Forms.CheckedListBox();
            this.LinkForums = new System.Windows.Forms.LinkLabel();
            this.LinkReportBug = new System.Windows.Forms.LinkLabel();
            this.LinkSubreddit = new System.Windows.Forms.LinkLabel();
            this.LinkDiscord = new System.Windows.Forms.LinkLabel();
            this.LabelUtilities = new System.Windows.Forms.Label();
            this.LabelDatabaseMods = new System.Windows.Forms.Label();
            this.LinkModSubreddit = new System.Windows.Forms.LinkLabel();
            this.ButtonMultiplayer = new System.Windows.Forms.Button();
            this.ButtonManageMods = new System.Windows.Forms.Button();
            this.SelectedSavelabel = new System.Windows.Forms.Label();
            this.ButtonMangeSaves = new System.Windows.Forms.Button();
            this.PictureBoxUpdateAurora = new System.Windows.Forms.PictureBox();
            this.PictureBoxUpdateLoader = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.TrackMusicVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxUpdateAurora)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxUpdateLoader)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonSinglePlayer
            // 
            this.ButtonSinglePlayer.Enabled = false;
            this.ButtonSinglePlayer.Location = new System.Drawing.Point(12, 46);
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
            this.LabelAuroraVersion.Location = new System.Drawing.Point(410, 252);
            this.LabelAuroraVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelAuroraVersion.Name = "LabelAuroraVersion";
            this.LabelAuroraVersion.Size = new System.Drawing.Size(133, 15);
            this.LabelAuroraVersion.TabIndex = 7;
            this.LabelAuroraVersion.Text = "Aurora v#.##.#";
            this.LabelAuroraVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TrackMusicVolume
            // 
            this.TrackMusicVolume.Enabled = false;
            this.TrackMusicVolume.LargeChange = 1;
            this.TrackMusicVolume.Location = new System.Drawing.Point(219, 46);
            this.TrackMusicVolume.Margin = new System.Windows.Forms.Padding(4);
            this.TrackMusicVolume.Name = "TrackMusicVolume";
            this.TrackMusicVolume.Size = new System.Drawing.Size(162, 45);
            this.TrackMusicVolume.TabIndex = 20;
            this.TrackMusicVolume.Value = 4;
            // 
            // CheckEnableMusic
            // 
            this.CheckEnableMusic.AutoSize = true;
            this.CheckEnableMusic.Location = new System.Drawing.Point(104, 51);
            this.CheckEnableMusic.Margin = new System.Windows.Forms.Padding(4);
            this.CheckEnableMusic.Name = "CheckEnableMusic";
            this.CheckEnableMusic.Size = new System.Drawing.Size(107, 19);
            this.CheckEnableMusic.TabIndex = 2;
            this.CheckEnableMusic.Text = "In-Game Music";
            this.CheckEnableMusic.UseVisualStyleBackColor = true;
            this.CheckEnableMusic.CheckedChanged += new System.EventHandler(this.CheckMusic_CheckedChanged);
            // 
            // ButtonReadme
            // 
            this.ButtonReadme.Location = new System.Drawing.Point(395, 12);
            this.ButtonReadme.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonReadme.Name = "ButtonReadme";
            this.ButtonReadme.Size = new System.Drawing.Size(84, 24);
            this.ButtonReadme.TabIndex = 13;
            this.ButtonReadme.Text = "Readme";
            this.ButtonReadme.UseVisualStyleBackColor = true;
            this.ButtonReadme.Click += new System.EventHandler(this.ButtonReadme_Click);
            // 
            // ButtonChangelog
            //
            this.ButtonChangelog.Location = new System.Drawing.Point(495, 12);
            this.ButtonChangelog.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonChangelog.Name = "ButtonChangelog";
            this.ButtonChangelog.Size = new System.Drawing.Size(84, 24);
            this.ButtonChangelog.TabIndex = 14;
            this.ButtonChangelog.Text = "Changelog";
            this.ButtonChangelog.UseVisualStyleBackColor = true;
            this.ButtonChangelog.Click += new System.EventHandler(this.ButtonChangelog_Click);
            //
            // LabelAuroraLoaderVersion
            // 
            this.LabelAuroraLoaderVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelAuroraLoaderVersion.Location = new System.Drawing.Point(231, 252);
            this.LabelAuroraLoaderVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelAuroraLoaderVersion.Name = "LabelAuroraLoaderVersion";
            this.LabelAuroraLoaderVersion.Size = new System.Drawing.Size(133, 15);
            this.LabelAuroraLoaderVersion.TabIndex = 7;
            this.LabelAuroraLoaderVersion.Text = "Loader v#.##.#";
            this.LabelAuroraLoaderVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // CheckEnableMods
            // 
            this.CheckEnableMods.AutoSize = true;
            this.CheckEnableMods.Location = new System.Drawing.Point(12, 81);
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
            this.CheckEnablePoweruserMods.Location = new System.Drawing.Point(104, 80);
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
            this.ComboSelectExecutableMod.Location = new System.Drawing.Point(262, 77);
            this.ComboSelectExecutableMod.Name = "ComboSelectExecutableMod";
            this.ComboSelectExecutableMod.Size = new System.Drawing.Size(216, 23);
            this.ComboSelectExecutableMod.TabIndex = 27;
            // 
            // ListDatabaseMods
            // 
            this.ListDatabaseMods.FormattingEnabled = true;
            this.ListDatabaseMods.Location = new System.Drawing.Point(317, 138);
            this.ListDatabaseMods.Name = "ListDatabaseMods";
            this.ListDatabaseMods.Size = new System.Drawing.Size(264, 94);
            this.ListDatabaseMods.TabIndex = 26;
            // 
            // ListUtilities
            // 
            this.ListUtilities.FormattingEnabled = true;
            this.ListUtilities.Location = new System.Drawing.Point(12, 138);
            this.ListUtilities.Name = "ListUtilities";
            this.ListUtilities.Size = new System.Drawing.Size(264, 94);
            this.ListUtilities.TabIndex = 27;
            this.ListUtilities.SelectedIndexChanged += new System.EventHandler(this.ListUtilityMods_SelectedIndexChanged);
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
            this.LabelUtilities.Location = new System.Drawing.Point(12, 111);
            this.LabelUtilities.Name = "LabelUtilities";
            this.LabelUtilities.Size = new System.Drawing.Size(183, 15);
            this.LabelUtilities.TabIndex = 38;
            this.LabelUtilities.Text = "Launch utilities alongside Aurora:";
            // 
            // LabelDatabaseMods
            // 
            this.LabelDatabaseMods.AutoSize = true;
            this.LabelDatabaseMods.Location = new System.Drawing.Point(317, 111);
            this.LabelDatabaseMods.Name = "LabelDatabaseMods";
            this.LabelDatabaseMods.Size = new System.Drawing.Size(124, 15);
            this.LabelDatabaseMods.TabIndex = 39;
            this.LabelDatabaseMods.Text = "Apply database mods:";
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
            this.ButtonMultiplayer.Location = new System.Drawing.Point(122, 243);
            this.ButtonMultiplayer.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonMultiplayer.Name = "ButtonMultiplayer";
            this.ButtonMultiplayer.Size = new System.Drawing.Size(103, 27);
            this.ButtonMultiplayer.TabIndex = 2;
            this.ButtonMultiplayer.Text = "Play Multiplayer";
            this.ButtonMultiplayer.UseVisualStyleBackColor = true;
            this.ButtonMultiplayer.Click += new System.EventHandler(this.ButtonSinglePlayer_Click);
            // 
            // ButtonManageMods
            // 
            this.ButtonManageMods.Location = new System.Drawing.Point(12, 243);
            this.ButtonManageMods.Name = "ButtonManageMods";
            this.ButtonManageMods.Size = new System.Drawing.Size(103, 27);
            this.ButtonManageMods.TabIndex = 41;
            this.ButtonManageMods.Text = "Manage Mods";
            this.ButtonManageMods.UseVisualStyleBackColor = true;
            this.ButtonManageMods.Click += new System.EventHandler(this.ButtonManageMods_Click);
            // 
            // SelectedSavelabel
            // 
            this.SelectedSavelabel.AutoSize = true;
            this.SelectedSavelabel.Location = new System.Drawing.Point(102, 18);
            this.SelectedSavelabel.Name = "SelectedSavelabel";
            this.SelectedSavelabel.Size = new System.Drawing.Size(72, 15);
            this.SelectedSavelabel.TabIndex = 42;
            this.SelectedSavelabel.Text = "No game selected";
            // 
            // ButtonMangeSaves
            // 
            this.ButtonMangeSaves.Location = new System.Drawing.Point(12, 12);
            this.ButtonMangeSaves.Name = "ButtonMangeSaves";
            this.ButtonMangeSaves.Size = new System.Drawing.Size(84, 27);
            this.ButtonMangeSaves.TabIndex = 41;
            this.ButtonMangeSaves.Text = "Select Game";
            this.ButtonMangeSaves.UseVisualStyleBackColor = true;
            this.ButtonMangeSaves.Click += new System.EventHandler(this.ButtonManageSaves_Click);
            // 
            // PictureBoxUpdateAurora
            // 
            this.PictureBoxUpdateAurora.Image = ((System.Drawing.Image)(resources.GetObject("PictureBoxUpdateAurora.Image")));
            this.PictureBoxUpdateAurora.Location = new System.Drawing.Point(547, 238);
            this.PictureBoxUpdateAurora.Name = "PictureBoxUpdateAurora";
            this.PictureBoxUpdateAurora.Size = new System.Drawing.Size(32, 32);
            this.PictureBoxUpdateAurora.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBoxUpdateAurora.TabIndex = 43;
            this.PictureBoxUpdateAurora.TabStop = false;
            this.PictureBoxUpdateAurora.Click += new System.EventHandler(this.ButtonUpdateAurora_Click);
            // 
            // PictureBoxUpdateLoader
            // 
            this.PictureBoxUpdateLoader.Image = ((System.Drawing.Image)(resources.GetObject("PictureBoxUpdateLoader.Image")));
            this.PictureBoxUpdateLoader.Location = new System.Drawing.Point(371, 238);
            this.PictureBoxUpdateLoader.Name = "PictureBoxUpdateLoader";
            this.PictureBoxUpdateLoader.Size = new System.Drawing.Size(32, 32);
            this.PictureBoxUpdateLoader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBoxUpdateLoader.TabIndex = 43;
            this.PictureBoxUpdateLoader.TabStop = false;
            this.PictureBoxUpdateLoader.Click += new System.EventHandler(this.ButtonUpdateAuroraLoader_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 287);
            this.Controls.Add(this.PictureBoxUpdateLoader);
            this.Controls.Add(this.PictureBoxUpdateAurora);
            this.Controls.Add(this.ButtonMangeSaves);
            this.Controls.Add(this.SelectedSavelabel);
            this.Controls.Add(this.ButtonManageMods);
            this.Controls.Add(this.ButtonMultiplayer);
            this.Controls.Add(this.LinkModSubreddit);
            this.Controls.Add(this.LabelDatabaseMods);
            this.Controls.Add(this.LabelUtilities);
            this.Controls.Add(this.LinkDiscord);
            this.Controls.Add(this.LinkSubreddit);
            this.Controls.Add(this.LinkReportBug);
            this.Controls.Add(this.LinkForums);
            this.Controls.Add(this.ListUtilities);
            this.Controls.Add(this.ListDatabaseMods);
            this.Controls.Add(this.ComboSelectExecutableMod);
            this.Controls.Add(this.CheckEnablePoweruserMods);
            this.Controls.Add(this.CheckEnableMods);
            this.Controls.Add(this.LabelAuroraLoaderVersion);
            this.Controls.Add(this.ButtonReadme);
            this.Controls.Add(this.ButtonChangelog);
            this.Controls.Add(this.CheckEnableMusic);
            this.Controls.Add(this.TrackMusicVolume);
            this.Controls.Add(this.LabelAuroraVersion);
            this.Controls.Add(this.ButtonSinglePlayer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aurora Loader";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TrackMusicVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxUpdateAurora)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxUpdateLoader)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ButtonSinglePlayer;
        private System.Windows.Forms.Label LabelAuroraVersion;
        private System.Windows.Forms.TrackBar TrackMusicVolume;
        private System.Windows.Forms.CheckBox CheckEnableMusic;
        private System.Windows.Forms.Button ButtonReadme;
        private System.Windows.Forms.Button ButtonChangelog;
        private System.Windows.Forms.Label LabelAuroraLoaderVersion;
        private System.Windows.Forms.CheckBox CheckEnableMods;
        private System.Windows.Forms.CheckBox CheckEnablePoweruserMods;
        private System.Windows.Forms.ComboBox ComboSelectExecutableMod;
        private System.Windows.Forms.CheckedListBox ListDatabaseMods;
        private System.Windows.Forms.CheckedListBox ListUtilities;
        private System.Windows.Forms.LinkLabel LinkForums;
        private System.Windows.Forms.LinkLabel LinkReportBug;
        private System.Windows.Forms.LinkLabel LinkSubreddit;
        private System.Windows.Forms.LinkLabel LinkDiscord;
        private System.Windows.Forms.Label LabelUtilities;
        private System.Windows.Forms.Label LabelDatabaseMods;
        private LinkLabel LinkModSubreddit;
        private Button ButtonMultiplayer;
        private Button ButtonManageMods;
        private Label SelectedSavelabel;
        private Button ButtonMangeSaves;
        private PictureBox PictureBoxUpdateAurora;
        private PictureBox PictureBoxUpdateLoader;
    }
}

