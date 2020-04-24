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
            this.ButtonSinglePlayer = new System.Windows.Forms.Button();
            this.LabelVersion = new System.Windows.Forms.Label();
            this.LabelChecksum = new System.Windows.Forms.Label();
            this.ButtonAuroraForums = new System.Windows.Forms.Button();
            this.ButtonAuroraBugs = new System.Windows.Forms.Button();
            this.ButtonUpdateAurora = new System.Windows.Forms.Button();
            this.ButtonModSubreddit = new System.Windows.Forms.Button();
            this.ButtonUpdateMods = new System.Windows.Forms.Button();
            this.ButtonInstallMods = new System.Windows.Forms.Button();
            this.TabMods = new System.Windows.Forms.TabControl();
            this.TabThemeMods = new System.Windows.Forms.TabPage();
            this.TabUtilityMods = new System.Windows.Forms.TabPage();
            this.ButtonConfigureUtility = new System.Windows.Forms.Button();
            this.ListUtilityMods = new System.Windows.Forms.CheckedListBox();
            this.TabGameMods = new System.Windows.Forms.TabPage();
            this.ButtonMultiPlayer = new System.Windows.Forms.Button();
            this.ButtonInstallAurora = new System.Windows.Forms.Button();
            this.ButtonModBugs = new System.Windows.Forms.Button();
            this.TrackVolume = new System.Windows.Forms.TrackBar();
            this.CheckMusic = new System.Windows.Forms.CheckBox();
            this.GroupMods.SuspendLayout();
            this.TabMods.SuspendLayout();
            this.TabUtilityMods.SuspendLayout();
            this.TabGameMods.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackVolume)).BeginInit();
            this.SuspendLayout();
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
            this.GroupMods.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupMods.Location = new System.Drawing.Point(21, 48);
            this.GroupMods.Name = "GroupMods";
            this.GroupMods.Size = new System.Drawing.Size(419, 456);
            this.GroupMods.TabIndex = 0;
            this.GroupMods.TabStop = false;
            this.GroupMods.Text = "Game mods";
            // 
            // ButtonConfigureDB
            // 
            this.ButtonConfigureDB.Enabled = false;
            this.ButtonConfigureDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonConfigureDB.Location = new System.Drawing.Point(130, 124);
            this.ButtonConfigureDB.Name = "ButtonConfigureDB";
            this.ButtonConfigureDB.Size = new System.Drawing.Size(275, 33);
            this.ButtonConfigureDB.TabIndex = 16;
            this.ButtonConfigureDB.Text = "Configure selected";
            this.ButtonConfigureDB.UseVisualStyleBackColor = true;
            this.ButtonConfigureDB.Click += new System.EventHandler(this.ButtonConfigureDB_Click);
            // 
            // ButtonConfigureExe
            // 
            this.ButtonConfigureExe.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonConfigureExe.Location = new System.Drawing.Point(270, 72);
            this.ButtonConfigureExe.Name = "ButtonConfigureExe";
            this.ButtonConfigureExe.Size = new System.Drawing.Size(143, 33);
            this.ButtonConfigureExe.TabIndex = 15;
            this.ButtonConfigureExe.Text = "Configure";
            this.ButtonConfigureExe.UseVisualStyleBackColor = true;
            this.ButtonConfigureExe.Click += new System.EventHandler(this.ButtonConfigureExe_Click);
            // 
            // ListDBMods
            // 
            this.ListDBMods.FormattingEnabled = true;
            this.ListDBMods.Location = new System.Drawing.Point(2, 163);
            this.ListDBMods.Name = "ListDBMods";
            this.ListDBMods.Size = new System.Drawing.Size(403, 277);
            this.ListDBMods.TabIndex = 6;
            this.ListDBMods.SelectedIndexChanged += new System.EventHandler(this.ListDBMods_SelectedIndexChanged);
            // 
            // LabelDBMods
            // 
            this.LabelDBMods.AutoSize = true;
            this.LabelDBMods.Location = new System.Drawing.Point(-2, 130);
            this.LabelDBMods.Name = "LabelDBMods";
            this.LabelDBMods.Size = new System.Drawing.Size(126, 20);
            this.LabelDBMods.TabIndex = 5;
            this.LabelDBMods.Text = "Database mods:";
            // 
            // LabelExeMod
            // 
            this.LabelExeMod.AutoSize = true;
            this.LabelExeMod.Location = new System.Drawing.Point(2, 78);
            this.LabelExeMod.Name = "LabelExeMod";
            this.LabelExeMod.Size = new System.Drawing.Size(75, 20);
            this.LabelExeMod.TabIndex = 4;
            this.LabelExeMod.Text = "Exe mod:";
            // 
            // ComboExe
            // 
            this.ComboExe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboExe.FormattingEnabled = true;
            this.ComboExe.Items.AddRange(new object[] {
            "Base Game"});
            this.ComboExe.Location = new System.Drawing.Point(83, 75);
            this.ComboExe.Name = "ComboExe";
            this.ComboExe.Size = new System.Drawing.Size(181, 28);
            this.ComboExe.TabIndex = 3;
            this.ComboExe.SelectedIndexChanged += new System.EventHandler(this.ComboExe_SelectedIndexChanged);
            // 
            // CheckPower
            // 
            this.CheckPower.AutoSize = true;
            this.CheckPower.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckPower.Location = new System.Drawing.Point(270, 35);
            this.CheckPower.Name = "CheckPower";
            this.CheckPower.Size = new System.Drawing.Size(146, 24);
            this.CheckPower.TabIndex = 2;
            this.CheckPower.Text = "Poweruser mods";
            this.CheckPower.UseVisualStyleBackColor = true;
            this.CheckPower.CheckedChanged += new System.EventHandler(this.CheckPower_CheckedChanged);
            // 
            // CheckPublic
            // 
            this.CheckPublic.AutoSize = true;
            this.CheckPublic.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckPublic.Location = new System.Drawing.Point(151, 35);
            this.CheckPublic.Name = "CheckPublic";
            this.CheckPublic.Size = new System.Drawing.Size(113, 24);
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
            this.CheckApproved.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckApproved.Location = new System.Drawing.Point(6, 35);
            this.CheckApproved.Name = "CheckApproved";
            this.CheckApproved.Size = new System.Drawing.Size(139, 24);
            this.CheckApproved.TabIndex = 2;
            this.CheckApproved.Text = "Approved mods";
            this.CheckApproved.UseVisualStyleBackColor = true;
            this.CheckApproved.CheckedChanged += new System.EventHandler(this.CheckApproved_CheckedChanged);
            // 
            // CheckMods
            // 
            this.CheckMods.AutoSize = true;
            this.CheckMods.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckMods.Location = new System.Drawing.Point(21, 18);
            this.CheckMods.Name = "CheckMods";
            this.CheckMods.Size = new System.Drawing.Size(165, 24);
            this.CheckMods.TabIndex = 1;
            this.CheckMods.Text = "Enable game mods";
            this.CheckMods.UseVisualStyleBackColor = true;
            this.CheckMods.CheckedChanged += new System.EventHandler(this.CheckMods_CheckedChanged);
            // 
            // ButtonSinglePlayer
            // 
            this.ButtonSinglePlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonSinglePlayer.Location = new System.Drawing.Point(12, 111);
            this.ButtonSinglePlayer.Name = "ButtonSinglePlayer";
            this.ButtonSinglePlayer.Size = new System.Drawing.Size(224, 46);
            this.ButtonSinglePlayer.TabIndex = 2;
            this.ButtonSinglePlayer.Text = "Single Player";
            this.ButtonSinglePlayer.UseVisualStyleBackColor = true;
            this.ButtonSinglePlayer.Click += new System.EventHandler(this.ButtonSinglePlayer_Click);
            // 
            // LabelVersion
            // 
            this.LabelVersion.AutoSize = true;
            this.LabelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelVersion.Location = new System.Drawing.Point(25, 24);
            this.LabelVersion.Name = "LabelVersion";
            this.LabelVersion.Size = new System.Drawing.Size(186, 20);
            this.LabelVersion.TabIndex = 7;
            this.LabelVersion.Text = "Aurora version: Unknown";
            // 
            // LabelChecksum
            // 
            this.LabelChecksum.AutoSize = true;
            this.LabelChecksum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelChecksum.Location = new System.Drawing.Point(25, 50);
            this.LabelChecksum.Name = "LabelChecksum";
            this.LabelChecksum.Size = new System.Drawing.Size(137, 20);
            this.LabelChecksum.TabIndex = 8;
            this.LabelChecksum.Text = "Aurora checksum:";
            // 
            // ButtonAuroraForums
            // 
            this.ButtonAuroraForums.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonAuroraForums.Location = new System.Drawing.Point(12, 367);
            this.ButtonAuroraForums.Name = "ButtonAuroraForums";
            this.ButtonAuroraForums.Size = new System.Drawing.Size(224, 46);
            this.ButtonAuroraForums.TabIndex = 9;
            this.ButtonAuroraForums.Text = "Aurora Forums";
            this.ButtonAuroraForums.UseVisualStyleBackColor = true;
            this.ButtonAuroraForums.Click += new System.EventHandler(this.ButtonAuroraForums_Click);
            // 
            // ButtonAuroraBugs
            // 
            this.ButtonAuroraBugs.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonAuroraBugs.ForeColor = System.Drawing.Color.OrangeRed;
            this.ButtonAuroraBugs.Location = new System.Drawing.Point(12, 419);
            this.ButtonAuroraBugs.Name = "ButtonAuroraBugs";
            this.ButtonAuroraBugs.Size = new System.Drawing.Size(224, 46);
            this.ButtonAuroraBugs.TabIndex = 11;
            this.ButtonAuroraBugs.Text = "Report a bug";
            this.ButtonAuroraBugs.UseVisualStyleBackColor = true;
            this.ButtonAuroraBugs.Click += new System.EventHandler(this.ButtonAuroraBugs_Click);
            // 
            // ButtonUpdateAurora
            // 
            this.ButtonUpdateAurora.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonUpdateAurora.Location = new System.Drawing.Point(12, 315);
            this.ButtonUpdateAurora.Name = "ButtonUpdateAurora";
            this.ButtonUpdateAurora.Size = new System.Drawing.Size(224, 46);
            this.ButtonUpdateAurora.TabIndex = 12;
            this.ButtonUpdateAurora.Text = "Update Aurora";
            this.ButtonUpdateAurora.UseVisualStyleBackColor = true;
            this.ButtonUpdateAurora.Click += new System.EventHandler(this.ButtonAuroraUpdates_Click);
            // 
            // ButtonModSubreddit
            // 
            this.ButtonModSubreddit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonModSubreddit.Location = new System.Drawing.Point(242, 367);
            this.ButtonModSubreddit.Name = "ButtonModSubreddit";
            this.ButtonModSubreddit.Size = new System.Drawing.Size(224, 46);
            this.ButtonModSubreddit.TabIndex = 13;
            this.ButtonModSubreddit.Text = "Mod subreddit";
            this.ButtonModSubreddit.UseVisualStyleBackColor = true;
            this.ButtonModSubreddit.Click += new System.EventHandler(this.ButtonModsSubreddit_Click);
            // 
            // ButtonUpdateMods
            // 
            this.ButtonUpdateMods.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonUpdateMods.Location = new System.Drawing.Point(242, 315);
            this.ButtonUpdateMods.Name = "ButtonUpdateMods";
            this.ButtonUpdateMods.Size = new System.Drawing.Size(224, 46);
            this.ButtonUpdateMods.TabIndex = 14;
            this.ButtonUpdateMods.Text = "Update mods";
            this.ButtonUpdateMods.UseVisualStyleBackColor = true;
            this.ButtonUpdateMods.Click += new System.EventHandler(this.ButtonUpdateMods_Click);
            // 
            // ButtonInstallMods
            // 
            this.ButtonInstallMods.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonInstallMods.Location = new System.Drawing.Point(242, 263);
            this.ButtonInstallMods.Name = "ButtonInstallMods";
            this.ButtonInstallMods.Size = new System.Drawing.Size(224, 46);
            this.ButtonInstallMods.TabIndex = 15;
            this.ButtonInstallMods.Text = "Install mods";
            this.ButtonInstallMods.UseVisualStyleBackColor = true;
            this.ButtonInstallMods.Click += new System.EventHandler(this.ButtonInstallMods_Click);
            // 
            // TabMods
            // 
            this.TabMods.Controls.Add(this.TabThemeMods);
            this.TabMods.Controls.Add(this.TabUtilityMods);
            this.TabMods.Controls.Add(this.TabGameMods);
            this.TabMods.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabMods.Location = new System.Drawing.Point(504, 12);
            this.TabMods.Name = "TabMods";
            this.TabMods.SelectedIndex = 0;
            this.TabMods.Size = new System.Drawing.Size(467, 547);
            this.TabMods.TabIndex = 16;
            // 
            // TabThemeMods
            // 
            this.TabThemeMods.BackColor = System.Drawing.SystemColors.Control;
            this.TabThemeMods.Location = new System.Drawing.Point(4, 29);
            this.TabThemeMods.Name = "TabThemeMods";
            this.TabThemeMods.Padding = new System.Windows.Forms.Padding(3);
            this.TabThemeMods.Size = new System.Drawing.Size(459, 514);
            this.TabThemeMods.TabIndex = 0;
            this.TabThemeMods.Text = "Theme mods";
            // 
            // TabUtilityMods
            // 
            this.TabUtilityMods.BackColor = System.Drawing.SystemColors.Control;
            this.TabUtilityMods.Controls.Add(this.ButtonConfigureUtility);
            this.TabUtilityMods.Controls.Add(this.ListUtilityMods);
            this.TabUtilityMods.Location = new System.Drawing.Point(4, 29);
            this.TabUtilityMods.Name = "TabUtilityMods";
            this.TabUtilityMods.Padding = new System.Windows.Forms.Padding(3);
            this.TabUtilityMods.Size = new System.Drawing.Size(459, 514);
            this.TabUtilityMods.TabIndex = 1;
            this.TabUtilityMods.Text = "Utility mods";
            // 
            // ButtonConfigureUtility
            // 
            this.ButtonConfigureUtility.Enabled = false;
            this.ButtonConfigureUtility.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonConfigureUtility.Location = new System.Drawing.Point(158, 24);
            this.ButtonConfigureUtility.Name = "ButtonConfigureUtility";
            this.ButtonConfigureUtility.Size = new System.Drawing.Size(275, 33);
            this.ButtonConfigureUtility.TabIndex = 17;
            this.ButtonConfigureUtility.Text = "Configure selected";
            this.ButtonConfigureUtility.UseVisualStyleBackColor = true;
            this.ButtonConfigureUtility.Click += new System.EventHandler(this.ButtonConfigureUtility_Click);
            // 
            // ListUtilityMods
            // 
            this.ListUtilityMods.FormattingEnabled = true;
            this.ListUtilityMods.Location = new System.Drawing.Point(6, 63);
            this.ListUtilityMods.Name = "ListUtilityMods";
            this.ListUtilityMods.Size = new System.Drawing.Size(447, 445);
            this.ListUtilityMods.TabIndex = 17;
            this.ListUtilityMods.SelectedIndexChanged += new System.EventHandler(this.ListUtilityMods_SelectedIndexChanged);
            // 
            // TabGameMods
            // 
            this.TabGameMods.BackColor = System.Drawing.SystemColors.Control;
            this.TabGameMods.Controls.Add(this.GroupMods);
            this.TabGameMods.Controls.Add(this.CheckMods);
            this.TabGameMods.Location = new System.Drawing.Point(4, 29);
            this.TabGameMods.Name = "TabGameMods";
            this.TabGameMods.Padding = new System.Windows.Forms.Padding(3);
            this.TabGameMods.Size = new System.Drawing.Size(459, 514);
            this.TabGameMods.TabIndex = 2;
            this.TabGameMods.Text = "Game mods";
            // 
            // ButtonMultiPlayer
            // 
            this.ButtonMultiPlayer.Enabled = false;
            this.ButtonMultiPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonMultiPlayer.Location = new System.Drawing.Point(242, 111);
            this.ButtonMultiPlayer.Name = "ButtonMultiPlayer";
            this.ButtonMultiPlayer.Size = new System.Drawing.Size(224, 46);
            this.ButtonMultiPlayer.TabIndex = 17;
            this.ButtonMultiPlayer.Text = "Multi Player";
            this.ButtonMultiPlayer.UseVisualStyleBackColor = true;
            // 
            // ButtonInstallAurora
            // 
            this.ButtonInstallAurora.Enabled = false;
            this.ButtonInstallAurora.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonInstallAurora.Location = new System.Drawing.Point(12, 263);
            this.ButtonInstallAurora.Name = "ButtonInstallAurora";
            this.ButtonInstallAurora.Size = new System.Drawing.Size(224, 46);
            this.ButtonInstallAurora.TabIndex = 18;
            this.ButtonInstallAurora.Text = "Install Aurora";
            this.ButtonInstallAurora.UseVisualStyleBackColor = true;
            // 
            // ButtonModBugs
            // 
            this.ButtonModBugs.Enabled = false;
            this.ButtonModBugs.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonModBugs.Location = new System.Drawing.Point(242, 419);
            this.ButtonModBugs.Name = "ButtonModBugs";
            this.ButtonModBugs.Size = new System.Drawing.Size(224, 46);
            this.ButtonModBugs.TabIndex = 19;
            this.ButtonModBugs.Text = "Report a bug";
            this.ButtonModBugs.UseVisualStyleBackColor = true;
            this.ButtonModBugs.Click += new System.EventHandler(this.ButtonModBugs_Click);
            // 
            // TrackVolume
            // 
            this.TrackVolume.Enabled = false;
            this.TrackVolume.LargeChange = 1;
            this.TrackVolume.Location = new System.Drawing.Point(97, 167);
            this.TrackVolume.Name = "TrackVolume";
            this.TrackVolume.Size = new System.Drawing.Size(139, 45);
            this.TrackVolume.TabIndex = 20;
            // 
            // CheckMusic
            // 
            this.CheckMusic.AutoSize = true;
            this.CheckMusic.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckMusic.Location = new System.Drawing.Point(12, 167);
            this.CheckMusic.Name = "CheckMusic";
            this.CheckMusic.Size = new System.Drawing.Size(79, 28);
            this.CheckMusic.TabIndex = 2;
            this.CheckMusic.Text = "Music";
            this.CheckMusic.UseVisualStyleBackColor = true;
            this.CheckMusic.CheckedChanged += new System.EventHandler(this.CheckMusic_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 566);
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
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aurora Loader";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.GroupMods.ResumeLayout(false);
            this.GroupMods.PerformLayout();
            this.TabMods.ResumeLayout(false);
            this.TabUtilityMods.ResumeLayout(false);
            this.TabGameMods.ResumeLayout(false);
            this.TabGameMods.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackVolume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupMods;
        private System.Windows.Forms.CheckBox CheckPower;
        private System.Windows.Forms.CheckBox CheckPublic;
        private System.Windows.Forms.CheckBox CheckApproved;
        private System.Windows.Forms.CheckBox CheckMods;
        private System.Windows.Forms.Label LabelDBMods;
        private System.Windows.Forms.Label LabelExeMod;
        private System.Windows.Forms.ComboBox ComboExe;
        private System.Windows.Forms.CheckedListBox ListDBMods;
        private System.Windows.Forms.Button ButtonSinglePlayer;
        private System.Windows.Forms.Label LabelVersion;
        private System.Windows.Forms.Label LabelChecksum;
        private System.Windows.Forms.Button ButtonAuroraForums;
        private System.Windows.Forms.Button ButtonAuroraBugs;
        private System.Windows.Forms.Button ButtonUpdateAurora;
        private System.Windows.Forms.Button ButtonModSubreddit;
        private System.Windows.Forms.Button ButtonUpdateMods;
        private System.Windows.Forms.Button ButtonConfigureExe;
        private System.Windows.Forms.Button ButtonConfigureDB;
        private System.Windows.Forms.Button ButtonInstallMods;
        private System.Windows.Forms.TabControl TabMods;
        private System.Windows.Forms.TabPage TabThemeMods;
        private System.Windows.Forms.TabPage TabUtilityMods;
        private System.Windows.Forms.TabPage TabGameMods;
        private System.Windows.Forms.Button ButtonMultiPlayer;
        private System.Windows.Forms.Button ButtonInstallAurora;
        private System.Windows.Forms.Button ButtonModBugs;
        private System.Windows.Forms.CheckedListBox ListUtilityMods;
        private System.Windows.Forms.Button ButtonConfigureUtility;
        private System.Windows.Forms.TrackBar TrackVolume;
        private System.Windows.Forms.CheckBox CheckMusic;
    }
}

