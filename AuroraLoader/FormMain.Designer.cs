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
            this.ButtonInstallOrUpdate = new System.Windows.Forms.Button();
            this.ButtonMultiPlayer = new System.Windows.Forms.Button();
            this.ButtonInstallAurora = new System.Windows.Forms.Button();
            this.ButtonModBugs = new System.Windows.Forms.Button();
            this.TrackVolume = new System.Windows.Forms.TrackBar();
            this.CheckMusic = new System.Windows.Forms.CheckBox();
            this.TabApplyMods = new System.Windows.Forms.TabPage();
            this.ListUtilities = new System.Windows.Forms.CheckedListBox();
            this.GroupMods = new System.Windows.Forms.GroupBox();
            this.LabelUtilities = new System.Windows.Forms.Label();
            this.CheckEnableGameMods = new System.Windows.Forms.CheckBox();
            this.ListDatabaseMods = new System.Windows.Forms.CheckedListBox();
            this.LabelExeMod = new System.Windows.Forms.Label();
            this.ComboSelectLaunchExe = new System.Windows.Forms.ComboBox();
            this.CheckPower = new System.Windows.Forms.CheckBox();
            this.CheckPublic = new System.Windows.Forms.CheckBox();
            this.CheckApproved = new System.Windows.Forms.CheckBox();
            this.TabMods = new System.Windows.Forms.TabControl();
            this.TabManageMods = new System.Windows.Forms.TabPage();
            this.ButtonConfigureMod = new System.Windows.Forms.Button();
            this.ListManageMods = new System.Windows.Forms.ListView();
            this.LabelDatabaseMods = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.TrackVolume)).BeginInit();
            this.TabApplyMods.SuspendLayout();
            this.GroupMods.SuspendLayout();
            this.TabMods.SuspendLayout();
            this.TabManageMods.SuspendLayout();
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
            this.ButtonUpdateAurora.Click += new System.EventHandler(this.ButtonUpdateAurora_Click);
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
            // ButtonInstallOrUpdate
            // 
            this.ButtonInstallOrUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ButtonInstallOrUpdate.Location = new System.Drawing.Point(75, 590);
            this.ButtonInstallOrUpdate.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ButtonInstallOrUpdate.Name = "ButtonInstallOrUpdate";
            this.ButtonInstallOrUpdate.Size = new System.Drawing.Size(250, 75);
            this.ButtonInstallOrUpdate.TabIndex = 15;
            this.ButtonInstallOrUpdate.Text = "Install";
            this.ButtonInstallOrUpdate.UseVisualStyleBackColor = true;
            this.ButtonInstallOrUpdate.Click += new System.EventHandler(this.ButtonInstallOrUpdateMods_Click);
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
            this.TabApplyMods.BackColor = System.Drawing.SystemColors.Control;
            this.TabApplyMods.Controls.Add(this.ListUtilities);
            this.TabApplyMods.Controls.Add(this.GroupMods);
            this.TabApplyMods.Location = new System.Drawing.Point(4, 38);
            this.TabApplyMods.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.TabApplyMods.Name = "TabGameMods";
            this.TabApplyMods.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.TabApplyMods.Size = new System.Drawing.Size(770, 1046);
            this.TabApplyMods.TabIndex = 2;
            this.TabApplyMods.Text = "Apply Mods";
            // 
            // ListUtilities
            // 
            this.ListUtilities.FormattingEnabled = true;
            this.ListUtilities.Location = new System.Drawing.Point(5, 700);
            this.ListUtilities.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ListUtilities.Name = "ListUtilities";
            this.ListUtilities.Size = new System.Drawing.Size(765, 324);
            this.ListUtilities.TabIndex = 17;
            this.ListUtilities.SelectedIndexChanged += new System.EventHandler(this.ListUtilities_SelectedIndexChanged);
            // 
            // GroupMods
            // 
            this.GroupMods.AutoSize = true;
            this.GroupMods.Controls.Add(this.LabelDatabaseMods);
            this.GroupMods.Controls.Add(this.LabelUtilities);
            this.GroupMods.Controls.Add(this.CheckEnableGameMods);
            this.GroupMods.Controls.Add(this.ListDatabaseMods);
            this.GroupMods.Controls.Add(this.LabelExeMod);
            this.GroupMods.Controls.Add(this.ComboSelectLaunchExe);
            this.GroupMods.Controls.Add(this.CheckPower);
            this.GroupMods.Controls.Add(this.CheckPublic);
            this.GroupMods.Controls.Add(this.CheckApproved);
            this.GroupMods.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GroupMods.Location = new System.Drawing.Point(5, 23);
            this.GroupMods.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.GroupMods.Name = "GroupMods";
            this.GroupMods.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.GroupMods.Size = new System.Drawing.Size(775, 688);
            this.GroupMods.TabIndex = 0;
            this.GroupMods.TabStop = false;
            this.GroupMods.Text = "Game mods";
            // 
            // LabelUtilities
            // 
            this.LabelUtilities.AutoSize = true;
            this.LabelUtilities.Location = new System.Drawing.Point(0, 625);
            this.LabelUtilities.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.LabelUtilities.Name = "LabelUtilities";
            this.LabelUtilities.Size = new System.Drawing.Size(98, 29);
            this.LabelUtilities.TabIndex = 4;
            this.LabelUtilities.Text = "Utilities:";
            // 
            // CheckEnableGameMods
            // 
            this.CheckEnableGameMods.AutoSize = true;
            this.CheckEnableGameMods.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CheckEnableGameMods.Location = new System.Drawing.Point(0, 50);
            this.CheckEnableGameMods.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.CheckEnableGameMods.Name = "CheckEnableGameMods";
            this.CheckEnableGameMods.Size = new System.Drawing.Size(248, 33);
            this.CheckEnableGameMods.TabIndex = 1;
            this.CheckEnableGameMods.Text = "Enable game mods";
            this.CheckEnableGameMods.UseVisualStyleBackColor = true;
            this.CheckEnableGameMods.CheckedChanged += new System.EventHandler(this.CheckEnableGameMods_CheckChanged);
            // 
            // ListGameMods
            // 
            this.ListDatabaseMods.FormattingEnabled = true;
            this.ListDatabaseMods.Location = new System.Drawing.Point(0, 250);
            this.ListDatabaseMods.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ListDatabaseMods.Name = "ListGameMods";
            this.ListDatabaseMods.Size = new System.Drawing.Size(765, 324);
            this.ListDatabaseMods.Sorted = true;
            this.ListDatabaseMods.TabIndex = 6;
            // 
            // LabelExeMod
            // 
            this.LabelExeMod.AutoSize = true;
            this.LabelExeMod.Location = new System.Drawing.Point(0, 100);
            this.LabelExeMod.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.LabelExeMod.Name = "LabelExeMod";
            this.LabelExeMod.Size = new System.Drawing.Size(192, 29);
            this.LabelExeMod.TabIndex = 4;
            this.LabelExeMod.Text = "Executable mod:";
            // 
            // ComboSelectLaunchExe
            // 
            this.ComboSelectLaunchExe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboSelectLaunchExe.FormattingEnabled = true;
            this.ComboSelectLaunchExe.Items.AddRange(new object[] {
            "Base Game"});
            this.ComboSelectLaunchExe.Location = new System.Drawing.Point(0, 150);
            this.ComboSelectLaunchExe.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ComboSelectLaunchExe.Name = "ComboSelectLaunchExe";
            this.ComboSelectLaunchExe.Size = new System.Drawing.Size(348, 37);
            this.ComboSelectLaunchExe.TabIndex = 3;
            this.ComboSelectLaunchExe.SelectedIndexChanged += new System.EventHandler(this.ComboSelectLaunchExe_SelectedIndexChanged);
            // 
            // CheckPower
            // 
            this.CheckPower.AutoSize = true;
            this.CheckPower.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CheckPower.Location = new System.Drawing.Point(526, 150);
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
            this.CheckPublic.Location = new System.Drawing.Point(526, 100);
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
            this.CheckApproved.Location = new System.Drawing.Point(526, 50);
            this.CheckApproved.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.CheckApproved.Name = "CheckApproved";
            this.CheckApproved.Size = new System.Drawing.Size(209, 33);
            this.CheckApproved.TabIndex = 2;
            this.CheckApproved.Text = "Approved mods";
            this.CheckApproved.UseVisualStyleBackColor = true;
            this.CheckApproved.CheckedChanged += new System.EventHandler(this.CheckApproved_CheckedChanged);
            // 
            // TabMods
            // 
            this.TabMods.Controls.Add(this.TabApplyMods);
            this.TabMods.Controls.Add(this.TabManageMods);
            this.TabMods.Dock = System.Windows.Forms.DockStyle.Right;
            this.TabMods.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TabMods.Location = new System.Drawing.Point(857, 0);
            this.TabMods.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.TabMods.Name = "TabMods";
            this.TabMods.SelectedIndex = 2;
            this.TabMods.Size = new System.Drawing.Size(778, 1088);
            this.TabMods.TabIndex = 16;
            this.TabMods.SelectedIndexChanged += new System.EventHandler(this.TabMods_SelectedIndexChanged);
            // 
            // TabManageMods
            // 
            this.TabManageMods.AutoScroll = true;
            this.TabManageMods.Controls.Add(this.ButtonConfigureMod);
            this.TabManageMods.Controls.Add(this.ButtonInstallOrUpdate);
            this.TabManageMods.Controls.Add(this.ListManageMods);
            this.TabManageMods.Location = new System.Drawing.Point(4, 38);
            this.TabManageMods.Name = "TabManageMods";
            this.TabManageMods.Size = new System.Drawing.Size(770, 1046);
            this.TabManageMods.TabIndex = 3;
            this.TabManageMods.Text = "Manage Mods";
            // 
            // ButtonConfigureMod
            // 
            this.ButtonConfigureMod.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ButtonConfigureMod.Location = new System.Drawing.Point(376, 590);
            this.ButtonConfigureMod.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ButtonConfigureMod.Name = "ButtonConfigureMod";
            this.ButtonConfigureMod.Size = new System.Drawing.Size(250, 75);
            this.ButtonConfigureMod.TabIndex = 15;
            this.ButtonConfigureMod.Text = "Configure";
            this.ButtonConfigureMod.UseVisualStyleBackColor = true;
            this.ButtonConfigureMod.Click += new System.EventHandler(this.ButtonConfigureMod_Click);
            // 
            // ListManageMods
            // 
            this.ListManageMods.HideSelection = false;
            this.ListManageMods.Location = new System.Drawing.Point(0, 3);
            this.ListManageMods.MultiSelect = false;
            this.ListManageMods.Name = "ListManageMods";
            this.ListManageMods.Size = new System.Drawing.Size(767, 574);
            this.ListManageMods.TabIndex = 0;
            this.ListManageMods.UseCompatibleStateImageBehavior = false;
            this.ListManageMods.SelectedIndexChanged += new System.EventHandler(this.ListManageMods_SelectedIndexChanged);
            // 
            // LabelDatabaseMods
            // 
            this.LabelDatabaseMods.AutoSize = true;
            this.LabelDatabaseMods.Location = new System.Drawing.Point(0, 200);
            this.LabelDatabaseMods.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.LabelDatabaseMods.Name = "LabelDatabaseMods";
            this.LabelDatabaseMods.Size = new System.Drawing.Size(187, 29);
            this.LabelDatabaseMods.TabIndex = 4;
            this.LabelDatabaseMods.Text = "Database mods:";
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
            this.TabApplyMods.ResumeLayout(false);
            this.TabApplyMods.PerformLayout();
            this.GroupMods.ResumeLayout(false);
            this.GroupMods.PerformLayout();
            this.TabMods.ResumeLayout(false);
            this.TabManageMods.ResumeLayout(false);
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
        private System.Windows.Forms.Button ButtonInstallOrUpdate;
        private System.Windows.Forms.Button ButtonMultiPlayer;
        private System.Windows.Forms.Button ButtonInstallAurora;
        private System.Windows.Forms.Button ButtonModBugs;
        private System.Windows.Forms.TrackBar TrackVolume;
        private System.Windows.Forms.CheckBox CheckMusic;
        private System.Windows.Forms.TabPage TabApplyMods;
        private System.Windows.Forms.GroupBox GroupMods;
        private System.Windows.Forms.CheckedListBox ListDatabaseMods;
        private System.Windows.Forms.Label LabelExeMod;
        private System.Windows.Forms.ComboBox ComboSelectLaunchExe;
        private System.Windows.Forms.CheckBox CheckPower;
        private System.Windows.Forms.CheckBox CheckPublic;
        private System.Windows.Forms.CheckBox CheckApproved;
        private System.Windows.Forms.CheckBox CheckEnableGameMods;
        private System.Windows.Forms.TabControl TabMods;
        private System.Windows.Forms.TabPage TabManageMods;
        private System.Windows.Forms.ListView ListManageMods;
        private System.Windows.Forms.Button ButtonConfigureMod;
        private System.Windows.Forms.CheckedListBox ListUtilities;
        private System.Windows.Forms.Label LabelUtilities;
        private System.Windows.Forms.Label LabelDatabaseMods;
    }
}

