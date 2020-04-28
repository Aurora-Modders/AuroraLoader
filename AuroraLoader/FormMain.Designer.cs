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
            this.LabelVersion = new System.Windows.Forms.Label();
            this.LabelChecksum = new System.Windows.Forms.Label();
            this.ButtonUpdateAurora = new System.Windows.Forms.Button();
            this.ButtonMultiPlayer = new System.Windows.Forms.Button();
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
            this.LinkForums = new System.Windows.Forms.LinkLabel();
            this.LinkVanillaBug = new System.Windows.Forms.LinkLabel();
            this.LinkSubreddit = new System.Windows.Forms.LinkLabel();
            this.LinkModdedBug = new System.Windows.Forms.LinkLabel();
            this.LabelExeMod = new System.Windows.Forms.Label();
            this.LabelUtilities = new System.Windows.Forms.Label();
            this.LabelDBMods = new System.Windows.Forms.Label();
            this.ManageMods = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.TrackVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonSinglePlayer
            // 
            this.ButtonSinglePlayer.Location = new System.Drawing.Point(331, 63);
            this.ButtonSinglePlayer.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.ButtonSinglePlayer.Name = "ButtonSinglePlayer";
            this.ButtonSinglePlayer.Size = new System.Drawing.Size(143, 42);
            this.ButtonSinglePlayer.TabIndex = 2;
            this.ButtonSinglePlayer.Text = "Single Player";
            this.ButtonSinglePlayer.UseVisualStyleBackColor = true;
            this.ButtonSinglePlayer.Click += new System.EventHandler(this.ButtonSinglePlayer_Click);
            // 
            // LabelVersion
            // 
            this.LabelVersion.AutoSize = true;
            this.LabelVersion.Location = new System.Drawing.Point(40, 63);
            this.LabelVersion.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.LabelVersion.Name = "LabelVersion";
            this.LabelVersion.Size = new System.Drawing.Size(212, 25);
            this.LabelVersion.TabIndex = 7;
            this.LabelVersion.Text = "Aurora version: Unknown";
            this.LabelVersion.Click += new System.EventHandler(this.LabelVersion_Click);
            // 
            // LabelChecksum
            // 
            this.LabelChecksum.AutoSize = true;
            this.LabelChecksum.Location = new System.Drawing.Point(40, 105);
            this.LabelChecksum.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.LabelChecksum.Name = "LabelChecksum";
            this.LabelChecksum.Size = new System.Drawing.Size(153, 25);
            this.LabelChecksum.TabIndex = 8;
            this.LabelChecksum.Text = "Aurora checksum:";
            // 
            // ButtonUpdateAurora
            // 
            this.ButtonUpdateAurora.Enabled = false;
            this.ButtonUpdateAurora.Location = new System.Drawing.Point(40, 185);
            this.ButtonUpdateAurora.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.ButtonUpdateAurora.Name = "ButtonUpdateAurora";
            this.ButtonUpdateAurora.Size = new System.Drawing.Size(200, 42);
            this.ButtonUpdateAurora.TabIndex = 12;
            this.ButtonUpdateAurora.Text = "Update Aurora";
            this.ButtonUpdateAurora.UseVisualStyleBackColor = true;
            this.ButtonUpdateAurora.Click += new System.EventHandler(this.ButtonUpdateAurora_Click);
            // 
            // ButtonMultiPlayer
            // 
            this.ButtonMultiPlayer.Enabled = false;
            this.ButtonMultiPlayer.Location = new System.Drawing.Point(486, 63);
            this.ButtonMultiPlayer.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.ButtonMultiPlayer.Name = "ButtonMultiPlayer";
            this.ButtonMultiPlayer.Size = new System.Drawing.Size(143, 42);
            this.ButtonMultiPlayer.TabIndex = 17;
            this.ButtonMultiPlayer.Text = "Multi Player";
            this.ButtonMultiPlayer.UseVisualStyleBackColor = true;
            // 
            // TrackVolume
            // 
            this.TrackVolume.Enabled = false;
            this.TrackVolume.LargeChange = 1;
            this.TrackVolume.Location = new System.Drawing.Point(426, 228);
            this.TrackVolume.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.TrackVolume.Name = "TrackVolume";
            this.TrackVolume.Size = new System.Drawing.Size(231, 69);
            this.TrackVolume.TabIndex = 20;
            // 
            // CheckMusic
            // 
            this.CheckMusic.AutoSize = true;
            this.CheckMusic.Location = new System.Drawing.Point(331, 228);
            this.CheckMusic.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.CheckMusic.Name = "CheckMusic";
            this.CheckMusic.Size = new System.Drawing.Size(84, 29);
            this.CheckMusic.TabIndex = 2;
            this.CheckMusic.Text = "Music";
            this.CheckMusic.UseVisualStyleBackColor = true;
            this.CheckMusic.CheckedChanged += new System.EventHandler(this.CheckMusic_CheckedChanged);
            // 
            // ButtonUpdateAuroraLoader
            // 
            this.ButtonUpdateAuroraLoader.Location = new System.Drawing.Point(40, 245);
            this.ButtonUpdateAuroraLoader.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.ButtonUpdateAuroraLoader.Name = "ButtonUpdateAuroraLoader";
            this.ButtonUpdateAuroraLoader.Size = new System.Drawing.Size(200, 42);
            this.ButtonUpdateAuroraLoader.TabIndex = 12;
            this.ButtonUpdateAuroraLoader.Text = "Update Loader";
            this.ButtonUpdateAuroraLoader.UseVisualStyleBackColor = true;
            this.ButtonUpdateAuroraLoader.Click += new System.EventHandler(this.ButtonUpdateAuroraLoader_Click);
            // 
            // ButtonReadme
            // 
            this.ButtonReadme.Location = new System.Drawing.Point(486, 118);
            this.ButtonReadme.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.ButtonReadme.Name = "ButtonReadme";
            this.ButtonReadme.Size = new System.Drawing.Size(143, 42);
            this.ButtonReadme.TabIndex = 13;
            this.ButtonReadme.Text = "Readme";
            this.ButtonReadme.UseVisualStyleBackColor = true;
            this.ButtonReadme.Click += new System.EventHandler(this.ButtonReadme_Click);
            // 
            // LabelAuroraLoaderVersion
            // 
            this.LabelAuroraLoaderVersion.AutoSize = true;
            this.LabelAuroraLoaderVersion.Location = new System.Drawing.Point(40, 145);
            this.LabelAuroraLoaderVersion.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.LabelAuroraLoaderVersion.Name = "LabelAuroraLoaderVersion";
            this.LabelAuroraLoaderVersion.Size = new System.Drawing.Size(267, 25);
            this.LabelAuroraLoaderVersion.TabIndex = 7;
            this.LabelAuroraLoaderVersion.Text = "AuroraLoader Version: Unknown";
            // 
            // CheckEnableGameMods
            // 
            this.CheckEnableGameMods.AutoSize = true;
            this.CheckEnableGameMods.Location = new System.Drawing.Point(40, 307);
            this.CheckEnableGameMods.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CheckEnableGameMods.Name = "CheckEnableGameMods";
            this.CheckEnableGameMods.Size = new System.Drawing.Size(141, 29);
            this.CheckEnableGameMods.TabIndex = 21;
            this.CheckEnableGameMods.Text = "Enable Mods";
            this.CheckEnableGameMods.UseVisualStyleBackColor = true;
            this.CheckEnableGameMods.CheckedChanged += new System.EventHandler(this.CheckEnableGameMod_CheckChanged);
            // 
            // CheckPublic
            // 
            this.CheckPublic.AutoSize = true;
            this.CheckPublic.Location = new System.Drawing.Point(40, 390);
            this.CheckPublic.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CheckPublic.Name = "CheckPublic";
            this.CheckPublic.Size = new System.Drawing.Size(81, 29);
            this.CheckPublic.TabIndex = 22;
            this.CheckPublic.Text = "Pubic";
            this.CheckPublic.UseVisualStyleBackColor = true;
            this.CheckPublic.CheckedChanged += new System.EventHandler(this.CheckModStatus_CheckChanged);
            // 
            // CheckPower
            // 
            this.CheckPower.AutoSize = true;
            this.CheckPower.Location = new System.Drawing.Point(40, 432);
            this.CheckPower.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CheckPower.Name = "CheckPower";
            this.CheckPower.Size = new System.Drawing.Size(119, 29);
            this.CheckPower.TabIndex = 23;
            this.CheckPower.Text = "Poweruser";
            this.CheckPower.UseVisualStyleBackColor = true;
            this.CheckPower.CheckedChanged += new System.EventHandler(this.CheckModStatus_CheckChanged);
            // 
            // CheckApproved
            // 
            this.CheckApproved.AutoSize = true;
            this.CheckApproved.Location = new System.Drawing.Point(40, 348);
            this.CheckApproved.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CheckApproved.Name = "CheckApproved";
            this.CheckApproved.Size = new System.Drawing.Size(118, 29);
            this.CheckApproved.TabIndex = 24;
            this.CheckApproved.Text = "Approved";
            this.CheckApproved.UseVisualStyleBackColor = true;
            this.CheckApproved.CheckedChanged += new System.EventHandler(this.CheckModStatus_CheckChanged);
            // 
            // ComboSelectLaunchExe
            // 
            this.ComboSelectLaunchExe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboSelectLaunchExe.FormattingEnabled = true;
            this.ComboSelectLaunchExe.Location = new System.Drawing.Point(849, 468);
            this.ComboSelectLaunchExe.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ComboSelectLaunchExe.Name = "ComboSelectLaunchExe";
            this.ComboSelectLaunchExe.Size = new System.Drawing.Size(343, 33);
            this.ComboSelectLaunchExe.TabIndex = 25;
            this.ComboSelectLaunchExe.SelectedIndexChanged += new System.EventHandler(this.ComboSelectLaunchExe_SelectedIndexChanged);
            // 
            // ListDatabaseMods
            // 
            this.ListDatabaseMods.FormattingEnabled = true;
            this.ListDatabaseMods.Location = new System.Drawing.Point(929, 118);
            this.ListDatabaseMods.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ListDatabaseMods.Name = "ListDatabaseMods";
            this.ListDatabaseMods.Size = new System.Drawing.Size(263, 312);
            this.ListDatabaseMods.TabIndex = 26;
            // 
            // ListUtilities
            // 
            this.ListUtilities.FormattingEnabled = true;
            this.ListUtilities.Location = new System.Drawing.Point(657, 118);
            this.ListUtilities.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ListUtilities.Name = "ListUtilities";
            this.ListUtilities.Size = new System.Drawing.Size(261, 312);
            this.ListUtilities.TabIndex = 27;
            this.ListUtilities.SelectedIndexChanged += new System.EventHandler(this.ListUtilityMods_SelectedIndexChanged);
            // 
            // ButtonInstallOrUpdate
            // 
            this.ButtonInstallOrUpdate.Location = new System.Drawing.Point(40, 957);
            this.ButtonInstallOrUpdate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ButtonInstallOrUpdate.Name = "ButtonInstallOrUpdate";
            this.ButtonInstallOrUpdate.Size = new System.Drawing.Size(143, 42);
            this.ButtonInstallOrUpdate.TabIndex = 29;
            this.ButtonInstallOrUpdate.Text = "Install";
            this.ButtonInstallOrUpdate.UseVisualStyleBackColor = true;
            this.ButtonInstallOrUpdate.Click += new System.EventHandler(this.ButtonInstallOrUpdateMods_Click);
            // 
            // ButtonConfigureMod
            // 
            this.ButtonConfigureMod.Location = new System.Drawing.Point(191, 957);
            this.ButtonConfigureMod.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ButtonConfigureMod.Name = "ButtonConfigureMod";
            this.ButtonConfigureMod.Size = new System.Drawing.Size(143, 42);
            this.ButtonConfigureMod.TabIndex = 30;
            this.ButtonConfigureMod.Text = "Configure";
            this.ButtonConfigureMod.UseVisualStyleBackColor = true;
            this.ButtonConfigureMod.Click += new System.EventHandler(this.ButtonConfigureMod_Click);
            // 
            // ListManageMods
            // 
            this.ListManageMods.HideSelection = false;
            this.ListManageMods.Location = new System.Drawing.Point(40, 548);
            this.ListManageMods.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ListManageMods.Name = "ListManageMods";
            this.ListManageMods.Size = new System.Drawing.Size(1151, 396);
            this.ListManageMods.TabIndex = 32;
            this.ListManageMods.UseCompatibleStateImageBehavior = false;
            this.ListManageMods.SelectedIndexChanged += new System.EventHandler(this.ListManageMods_SelectedIndexChanged);
            // 
            // LinkForums
            // 
            this.LinkForums.AutoSize = true;
            this.LinkForums.Location = new System.Drawing.Point(331, 313);
            this.LinkForums.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LinkForums.Name = "LinkForums";
            this.LinkForums.Size = new System.Drawing.Size(131, 25);
            this.LinkForums.TabIndex = 33;
            this.LinkForums.TabStop = true;
            this.LinkForums.Text = "Aurora Forums";
            this.LinkForums.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkForums_LinkClicked);
            // 
            // LinkVanillaBug
            // 
            this.LinkVanillaBug.AutoSize = true;
            this.LinkVanillaBug.Location = new System.Drawing.Point(463, 313);
            this.LinkVanillaBug.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LinkVanillaBug.Name = "LinkVanillaBug";
            this.LinkVanillaBug.Size = new System.Drawing.Size(115, 25);
            this.LinkVanillaBug.TabIndex = 34;
            this.LinkVanillaBug.TabStop = true;
            this.LinkVanillaBug.Text = "Report a Bug";
            this.LinkVanillaBug.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkVanillaBug_LinkClicked);
            // 
            // LinkSubreddit
            // 
            this.LinkSubreddit.AutoSize = true;
            this.LinkSubreddit.Location = new System.Drawing.Point(331, 355);
            this.LinkSubreddit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LinkSubreddit.Name = "LinkSubreddit";
            this.LinkSubreddit.Size = new System.Drawing.Size(133, 25);
            this.LinkSubreddit.TabIndex = 35;
            this.LinkSubreddit.TabStop = true;
            this.LinkSubreddit.Text = "Mod Subreddit";
            this.LinkSubreddit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkSubreddit_LinkClicked);
            // 
            // LinkModdedBug
            // 
            this.LinkModdedBug.AutoSize = true;
            this.LinkModdedBug.Location = new System.Drawing.Point(463, 355);
            this.LinkModdedBug.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LinkModdedBug.Name = "LinkModdedBug";
            this.LinkModdedBug.Size = new System.Drawing.Size(115, 25);
            this.LinkModdedBug.TabIndex = 36;
            this.LinkModdedBug.TabStop = true;
            this.LinkModdedBug.Text = "Report a Bug";
            this.LinkModdedBug.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkModdedBug_LinkClicked);
            // 
            // LabelExeMod
            // 
            this.LabelExeMod.AutoSize = true;
            this.LabelExeMod.Location = new System.Drawing.Point(657, 473);
            this.LabelExeMod.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelExeMod.Name = "LabelExeMod";
            this.LabelExeMod.Size = new System.Drawing.Size(142, 25);
            this.LabelExeMod.TabIndex = 37;
            this.LabelExeMod.Text = "Executable mod:";
            // 
            // LabelUtilities
            // 
            this.LabelUtilities.AutoSize = true;
            this.LabelUtilities.Location = new System.Drawing.Point(657, 80);
            this.LabelUtilities.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelUtilities.Name = "LabelUtilities";
            this.LabelUtilities.Size = new System.Drawing.Size(69, 25);
            this.LabelUtilities.TabIndex = 38;
            this.LabelUtilities.Text = "Utilities";
            // 
            // LabelDBMods
            // 
            this.LabelDBMods.AutoSize = true;
            this.LabelDBMods.Location = new System.Drawing.Point(929, 72);
            this.LabelDBMods.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelDBMods.Name = "LabelDBMods";
            this.LabelDBMods.Size = new System.Drawing.Size(86, 25);
            this.LabelDBMods.TabIndex = 39;
            this.LabelDBMods.Text = "DB Mods";
            // 
            // ManageMods
            // 
            this.ManageMods.AutoSize = true;
            this.ManageMods.Location = new System.Drawing.Point(56, 518);
            this.ManageMods.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ManageMods.Name = "ManageMods";
            this.ManageMods.Size = new System.Drawing.Size(198, 25);
            this.ManageMods.TabIndex = 40;
            this.ManageMods.Text = "Manage Installed Mods";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1263, 1018);
            this.Controls.Add(this.ManageMods);
            this.Controls.Add(this.LabelDBMods);
            this.Controls.Add(this.LabelUtilities);
            this.Controls.Add(this.LabelExeMod);
            this.Controls.Add(this.LinkModdedBug);
            this.Controls.Add(this.LinkSubreddit);
            this.Controls.Add(this.LinkVanillaBug);
            this.Controls.Add(this.LinkForums);
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
            this.Controls.Add(this.ButtonMultiPlayer);
            this.Controls.Add(this.ButtonUpdateAurora);
            this.Controls.Add(this.LabelChecksum);
            this.Controls.Add(this.LabelVersion);
            this.Controls.Add(this.ButtonSinglePlayer);
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
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
        private System.Windows.Forms.LinkLabel LinkForums;
        private System.Windows.Forms.LinkLabel LinkVanillaBug;
        private System.Windows.Forms.LinkLabel LinkSubreddit;
        private System.Windows.Forms.LinkLabel LinkModdedBug;
        private System.Windows.Forms.Label LabelExeMod;
        private System.Windows.Forms.Label LabelUtilities;
        private System.Windows.Forms.Label LabelDBMods;
        private System.Windows.Forms.Label ManageMods;
    }
}

