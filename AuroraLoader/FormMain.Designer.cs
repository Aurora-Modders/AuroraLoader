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
            this.ButtonUpdateAurora = new System.Windows.Forms.Button();
            this.ButtonMultiPlayer = new System.Windows.Forms.Button();
            this.ButtonInstallAurora = new System.Windows.Forms.Button();
            this.TrackVolume = new System.Windows.Forms.TrackBar();
            this.CheckMusic = new System.Windows.Forms.CheckBox();
            this.ButtonUpdateAuroraLoader = new System.Windows.Forms.Button();
            this.ButtonReadme = new System.Windows.Forms.Button();
            this.LabelAuroraLoaderVersion = new System.Windows.Forms.Label();
            this.CheckEnableGameMods = new System.Windows.Forms.CheckBox();
            this.CheckPublic = new System.Windows.Forms.CheckBox();
            this.CheckPower = new System.Windows.Forms.CheckBox();
            this.CheckApproved = new System.Windows.Forms.CheckBox();
            this.ComboSelectLaunchExe = new System.Windows.Forms.ComboBox();
            this.ListDatabaseMods = new System.Windows.Forms.CheckedListBox();
            this.ListUtilities = new System.Windows.Forms.CheckedListBox();
            this.ButtonInstallOrUpdate = new System.Windows.Forms.Button();
            this.ButtonConfigureMod = new System.Windows.Forms.Button();
            this.ListManageMods = new System.Windows.Forms.ListView();
            this.LinkFourms = new System.Windows.Forms.LinkLabel();
            this.LinkVanillaBug = new System.Windows.Forms.LinkLabel();
            this.LinkSubreddit = new System.Windows.Forms.LinkLabel();
            this.LinkModdedBug = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.TrackVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonSinglePlayer
            // 
            this.ButtonSinglePlayer.Location = new System.Drawing.Point(286, 13);
            this.ButtonSinglePlayer.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonSinglePlayer.Name = "ButtonSinglePlayer";
            this.ButtonSinglePlayer.Size = new System.Drawing.Size(98, 63);
            this.ButtonSinglePlayer.TabIndex = 2;
            this.ButtonSinglePlayer.Text = "Single Player";
            this.ButtonSinglePlayer.UseVisualStyleBackColor = true;
            this.ButtonSinglePlayer.Click += new System.EventHandler(this.ButtonSinglePlayer_Click);
            // 
            // LabelVersion
            // 
            this.LabelVersion.AutoSize = true;
            this.LabelVersion.Location = new System.Drawing.Point(29, 24);
            this.LabelVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelVersion.Name = "LabelVersion";
            this.LabelVersion.Size = new System.Drawing.Size(141, 15);
            this.LabelVersion.TabIndex = 7;
            this.LabelVersion.Text = "Aurora version: Unknown";
            // 
            // LabelChecksum
            // 
            this.LabelChecksum.AutoSize = true;
            this.LabelChecksum.Location = new System.Drawing.Point(29, 58);
            this.LabelChecksum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelChecksum.Name = "LabelChecksum";
            this.LabelChecksum.Size = new System.Drawing.Size(103, 15);
            this.LabelChecksum.TabIndex = 8;
            this.LabelChecksum.Text = "Aurora checksum:";
            // 
            // ButtonUpdateAurora
            // 
            this.ButtonUpdateAurora.Location = new System.Drawing.Point(14, 257);
            this.ButtonUpdateAurora.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonUpdateAurora.Name = "ButtonUpdateAurora";
            this.ButtonUpdateAurora.Size = new System.Drawing.Size(152, 46);
            this.ButtonUpdateAurora.TabIndex = 12;
            this.ButtonUpdateAurora.Text = "Update Aurora";
            this.ButtonUpdateAurora.UseVisualStyleBackColor = true;
            this.ButtonUpdateAurora.Click += new System.EventHandler(this.ButtonUpdateAurora_Click);
            // 
            // ButtonMultiPlayer
            // 
            this.ButtonMultiPlayer.Enabled = false;
            this.ButtonMultiPlayer.Location = new System.Drawing.Point(387, 87);
            this.ButtonMultiPlayer.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonMultiPlayer.Name = "ButtonMultiPlayer";
            this.ButtonMultiPlayer.Size = new System.Drawing.Size(66, 95);
            this.ButtonMultiPlayer.TabIndex = 17;
            this.ButtonMultiPlayer.Text = "Multi Player";
            this.ButtonMultiPlayer.UseVisualStyleBackColor = true;
            // 
            // ButtonInstallAurora
            // 
            this.ButtonInstallAurora.Enabled = false;
            this.ButtonInstallAurora.Location = new System.Drawing.Point(616, 193);
            this.ButtonInstallAurora.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonInstallAurora.Name = "ButtonInstallAurora";
            this.ButtonInstallAurora.Size = new System.Drawing.Size(118, 60);
            this.ButtonInstallAurora.TabIndex = 18;
            this.ButtonInstallAurora.Text = "Install Aurora";
            this.ButtonInstallAurora.UseVisualStyleBackColor = true;
            // 
            // TrackVolume
            // 
            this.TrackVolume.Enabled = false;
            this.TrackVolume.LargeChange = 1;
            this.TrackVolume.Location = new System.Drawing.Point(113, 193);
            this.TrackVolume.Margin = new System.Windows.Forms.Padding(4);
            this.TrackVolume.Name = "TrackVolume";
            this.TrackVolume.Size = new System.Drawing.Size(162, 45);
            this.TrackVolume.TabIndex = 20;
            // 
            // CheckMusic
            // 
            this.CheckMusic.AutoSize = true;
            this.CheckMusic.Location = new System.Drawing.Point(14, 193);
            this.CheckMusic.Margin = new System.Windows.Forms.Padding(4);
            this.CheckMusic.Name = "CheckMusic";
            this.CheckMusic.Size = new System.Drawing.Size(58, 19);
            this.CheckMusic.TabIndex = 2;
            this.CheckMusic.Text = "Music";
            this.CheckMusic.UseVisualStyleBackColor = true;
            this.CheckMusic.CheckedChanged += new System.EventHandler(this.CheckMusic_CheckedChanged);
            // 
            // ButtonUpdateAuroraLoader
            // 
            this.ButtonUpdateAuroraLoader.Location = new System.Drawing.Point(259, 225);
            this.ButtonUpdateAuroraLoader.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonUpdateAuroraLoader.Name = "ButtonUpdateAuroraLoader";
            this.ButtonUpdateAuroraLoader.Size = new System.Drawing.Size(143, 69);
            this.ButtonUpdateAuroraLoader.TabIndex = 12;
            this.ButtonUpdateAuroraLoader.Text = "Update AuroraLoader";
            this.ButtonUpdateAuroraLoader.UseVisualStyleBackColor = true;
            this.ButtonUpdateAuroraLoader.Click += new System.EventHandler(this.ButtonUpdateAuroraLoader_Click);
            // 
            // ButtonReadme
            // 
            this.ButtonReadme.Location = new System.Drawing.Point(577, 53);
            this.ButtonReadme.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonReadme.Name = "ButtonReadme";
            this.ButtonReadme.Size = new System.Drawing.Size(106, 39);
            this.ButtonReadme.TabIndex = 13;
            this.ButtonReadme.Text = "Readme";
            this.ButtonReadme.UseVisualStyleBackColor = true;
            this.ButtonReadme.Click += new System.EventHandler(this.ButtonReadme_Click);
            // 
            // LabelAuroraLoaderVersion
            // 
            this.LabelAuroraLoaderVersion.AutoSize = true;
            this.LabelAuroraLoaderVersion.Location = new System.Drawing.Point(29, 90);
            this.LabelAuroraLoaderVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelAuroraLoaderVersion.Name = "LabelAuroraLoaderVersion";
            this.LabelAuroraLoaderVersion.Size = new System.Drawing.Size(177, 15);
            this.LabelAuroraLoaderVersion.TabIndex = 7;
            this.LabelAuroraLoaderVersion.Text = "AuroraLoader Version: Unknown";
            // 
            // CheckEnableGameMods
            // 
            this.CheckEnableGameMods.AutoSize = true;
            this.CheckEnableGameMods.Location = new System.Drawing.Point(62, 433);
            this.CheckEnableGameMods.Name = "CheckEnableGameMods";
            this.CheckEnableGameMods.Size = new System.Drawing.Size(94, 19);
            this.CheckEnableGameMods.TabIndex = 21;
            this.CheckEnableGameMods.Text = "Enable Mods";
            this.CheckEnableGameMods.UseVisualStyleBackColor = true;
            this.CheckEnableGameMods.CheckedChanged += new System.EventHandler(this.CheckEnableGameMod_CheckChanged);
            // 
            // CheckPublic
            // 
            this.CheckPublic.AutoSize = true;
            this.CheckPublic.Location = new System.Drawing.Point(150, 570);
            this.CheckPublic.Name = "CheckPublic";
            this.CheckPublic.Size = new System.Drawing.Size(56, 19);
            this.CheckPublic.TabIndex = 22;
            this.CheckPublic.Text = "Pubic";
            this.CheckPublic.UseVisualStyleBackColor = true;
            // 
            // CheckPower
            // 
            this.CheckPower.AutoSize = true;
            this.CheckPower.Location = new System.Drawing.Point(150, 625);
            this.CheckPower.Name = "CheckPower";
            this.CheckPower.Size = new System.Drawing.Size(81, 19);
            this.CheckPower.TabIndex = 23;
            this.CheckPower.Text = "Poweruser";
            this.CheckPower.UseVisualStyleBackColor = true;
            // 
            // CheckApproved
            // 
            this.CheckApproved.AutoSize = true;
            this.CheckApproved.Location = new System.Drawing.Point(146, 528);
            this.CheckApproved.Name = "CheckApproved";
            this.CheckApproved.Size = new System.Drawing.Size(78, 19);
            this.CheckApproved.TabIndex = 24;
            this.CheckApproved.Text = "Approved";
            this.CheckApproved.UseVisualStyleBackColor = true;
            // 
            // ComboSelectLaunchExe
            // 
            this.ComboSelectLaunchExe.FormattingEnabled = true;
            this.ComboSelectLaunchExe.Location = new System.Drawing.Point(259, 465);
            this.ComboSelectLaunchExe.Name = "ComboSelectLaunchExe";
            this.ComboSelectLaunchExe.Size = new System.Drawing.Size(241, 23);
            this.ComboSelectLaunchExe.TabIndex = 25;
            this.ComboSelectLaunchExe.SelectedIndexChanged += new System.EventHandler(this.ComboSelectLaunchExe_SelectedIndexChanged);
            // 
            // ListDatabaseMods
            // 
            this.ListDatabaseMods.FormattingEnabled = true;
            this.ListDatabaseMods.Location = new System.Drawing.Point(538, 465);
            this.ListDatabaseMods.Name = "ListDatabaseMods";
            this.ListDatabaseMods.Size = new System.Drawing.Size(221, 202);
            this.ListDatabaseMods.TabIndex = 26;
            // 
            // ListUtilities
            // 
            this.ListUtilities.FormattingEnabled = true;
            this.ListUtilities.Location = new System.Drawing.Point(355, 528);
            this.ListUtilities.Name = "ListUtilities";
            this.ListUtilities.Size = new System.Drawing.Size(136, 148);
            this.ListUtilities.TabIndex = 27;
            this.ListUtilities.SelectedIndexChanged += new System.EventHandler(this.ListUtilityMods_SelectedIndexChanged);
            // 
            // ButtonInstallOrUpdate
            // 
            this.ButtonInstallOrUpdate.Location = new System.Drawing.Point(782, 395);
            this.ButtonInstallOrUpdate.Name = "ButtonInstallOrUpdate";
            this.ButtonInstallOrUpdate.Size = new System.Drawing.Size(93, 48);
            this.ButtonInstallOrUpdate.TabIndex = 29;
            this.ButtonInstallOrUpdate.Text = "Install";
            this.ButtonInstallOrUpdate.UseVisualStyleBackColor = true;
            this.ButtonInstallOrUpdate.Click += new System.EventHandler(this.ButtonInstallOrUpdateMods_Click);
            // 
            // ButtonConfigureMod
            // 
            this.ButtonConfigureMod.Location = new System.Drawing.Point(980, 386);
            this.ButtonConfigureMod.Name = "ButtonConfigureMod";
            this.ButtonConfigureMod.Size = new System.Drawing.Size(162, 39);
            this.ButtonConfigureMod.TabIndex = 30;
            this.ButtonConfigureMod.Text = "Configure";
            this.ButtonConfigureMod.UseVisualStyleBackColor = true;
            this.ButtonConfigureMod.Click += new System.EventHandler(this.ButtonConfigureMod_Click);
            // 
            // ListManageMods
            // 
            this.ListManageMods.HideSelection = false;
            this.ListManageMods.Location = new System.Drawing.Point(869, 43);
            this.ListManageMods.Name = "ListManageMods";
            this.ListManageMods.Size = new System.Drawing.Size(403, 204);
            this.ListManageMods.TabIndex = 32;
            this.ListManageMods.UseCompatibleStateImageBehavior = false;
            this.ListManageMods.SelectedIndexChanged += new System.EventHandler(this.ListManageMods_SelectedIndexChanged);
            // 
            // LinkFourms
            // 
            this.LinkFourms.AutoSize = true;
            this.LinkFourms.Location = new System.Drawing.Point(410, 336);
            this.LinkFourms.Name = "LinkFourms";
            this.LinkFourms.Size = new System.Drawing.Size(86, 15);
            this.LinkFourms.TabIndex = 33;
            this.LinkFourms.TabStop = true;
            this.LinkFourms.Text = "Aurora Fourms";
            this.LinkFourms.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkFourms_LinkClicked);
            // 
            // LinkVanillaBug
            // 
            this.LinkVanillaBug.AutoSize = true;
            this.LinkVanillaBug.Location = new System.Drawing.Point(526, 335);
            this.LinkVanillaBug.Name = "LinkVanillaBug";
            this.LinkVanillaBug.Size = new System.Drawing.Size(75, 15);
            this.LinkVanillaBug.TabIndex = 34;
            this.LinkVanillaBug.TabStop = true;
            this.LinkVanillaBug.Text = "Report a Bug";
            this.LinkVanillaBug.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkVanillaBug_LinkClicked);
            // 
            // LinkSubreddit
            // 
            this.LinkSubreddit.AutoSize = true;
            this.LinkSubreddit.Location = new System.Drawing.Point(422, 368);
            this.LinkSubreddit.Name = "LinkSubreddit";
            this.LinkSubreddit.Size = new System.Drawing.Size(86, 15);
            this.LinkSubreddit.TabIndex = 35;
            this.LinkSubreddit.TabStop = true;
            this.LinkSubreddit.Text = "Mod Subreddit";
            this.LinkSubreddit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkSubreddit_LinkClicked);
            // 
            // LinkModdedBug
            // 
            this.LinkModdedBug.AutoSize = true;
            this.LinkModdedBug.Location = new System.Drawing.Point(526, 368);
            this.LinkModdedBug.Name = "LinkModdedBug";
            this.LinkModdedBug.Size = new System.Drawing.Size(75, 15);
            this.LinkModdedBug.TabIndex = 36;
            this.LinkModdedBug.TabStop = true;
            this.LinkModdedBug.Text = "Report a Bug";
            this.LinkModdedBug.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkModdedBug_LinkClicked);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1369, 880);
            this.Controls.Add(this.LinkModdedBug);
            this.Controls.Add(this.LinkSubreddit);
            this.Controls.Add(this.LinkVanillaBug);
            this.Controls.Add(this.LinkFourms);
            this.Controls.Add(this.ListManageMods);
            this.Controls.Add(this.ButtonConfigureMod);
            this.Controls.Add(this.ButtonInstallOrUpdate);
            this.Controls.Add(this.ListUtilities);
            this.Controls.Add(this.ListDatabaseMods);
            this.Controls.Add(this.ComboSelectLaunchExe);
            this.Controls.Add(this.CheckApproved);
            this.Controls.Add(this.CheckPower);
            this.Controls.Add(this.CheckPublic);
            this.Controls.Add(this.CheckEnableGameMods);
            this.Controls.Add(this.LabelAuroraLoaderVersion);
            this.Controls.Add(this.ButtonReadme);
            this.Controls.Add(this.ButtonUpdateAuroraLoader);
            this.Controls.Add(this.CheckMusic);
            this.Controls.Add(this.TrackVolume);
            this.Controls.Add(this.ButtonInstallAurora);
            this.Controls.Add(this.ButtonMultiPlayer);
            this.Controls.Add(this.ButtonUpdateAurora);
            this.Controls.Add(this.LabelChecksum);
            this.Controls.Add(this.LabelVersion);
            this.Controls.Add(this.ButtonSinglePlayer);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aurora Loader";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TrackVolume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ButtonSinglePlayer;
        private System.Windows.Forms.Label LabelVersion;
        private System.Windows.Forms.Label LabelChecksum;
        private System.Windows.Forms.Button ButtonUpdateAurora;
        private System.Windows.Forms.Button ButtonMultiPlayer;
        private System.Windows.Forms.Button ButtonInstallAurora;
        private System.Windows.Forms.TrackBar TrackVolume;
        private System.Windows.Forms.CheckBox CheckMusic;
        private System.Windows.Forms.Button ButtonUpdateAuroraLoader;
        private System.Windows.Forms.Button ButtonReadme;
        private System.Windows.Forms.Label LabelAuroraLoaderVersion;
        private System.Windows.Forms.CheckBox CheckEnableGameMods;
        private System.Windows.Forms.CheckBox CheckPublic;
        private System.Windows.Forms.CheckBox CheckPower;
        private System.Windows.Forms.CheckBox CheckApproved;
        private System.Windows.Forms.ComboBox ComboSelectLaunchExe;
        private System.Windows.Forms.CheckedListBox ListDatabaseMods;
        private System.Windows.Forms.CheckedListBox ListUtilities;
        private System.Windows.Forms.Button ButtonInstallOrUpdate;
        private System.Windows.Forms.Button ButtonConfigureMod;
        private System.Windows.Forms.ListView ListManageMods;
        private System.Windows.Forms.LinkLabel LinkFourms;
        private System.Windows.Forms.LinkLabel LinkVanillaBug;
        private System.Windows.Forms.LinkLabel LinkSubreddit;
        private System.Windows.Forms.LinkLabel LinkModdedBug;
    }
}

