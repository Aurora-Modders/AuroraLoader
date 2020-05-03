namespace AuroraLoader
{
    partial class FormModDownload
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
            this.ManageModslabel = new System.Windows.Forms.Label();
            this.ListViewRegistryMods = new System.Windows.Forms.ListView();
            this.ButtonGetMod = new System.Windows.Forms.Button();
            this.ButtonConfigMod = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ManageModslabel
            // 
            this.ManageModslabel.AutoSize = true;
            this.ManageModslabel.Location = new System.Drawing.Point(12, 9);
            this.ManageModslabel.Name = "ManageModslabel";
            this.ManageModslabel.Size = new System.Drawing.Size(86, 15);
            this.ManageModslabel.TabIndex = 40;
            this.ManageModslabel.Text = "Manage mods:";
            // 
            // ListViewRegistryMods
            // 
            this.ListViewRegistryMods.HideSelection = false;
            this.ListViewRegistryMods.Location = new System.Drawing.Point(12, 27);
            this.ListViewRegistryMods.Name = "ListViewRegistryMods";
            this.ListViewRegistryMods.Size = new System.Drawing.Size(561, 239);
            this.ListViewRegistryMods.TabIndex = 32;
            this.ListViewRegistryMods.UseCompatibleStateImageBehavior = false;
            this.ListViewRegistryMods.SelectedIndexChanged += new System.EventHandler(this.ListManageMods_SelectedIndexChanged);
            // 
            // ButtonGetMod
            // 
            this.ButtonGetMod.Location = new System.Drawing.Point(12, 272);
            this.ButtonGetMod.Name = "ButtonGetMod";
            this.ButtonGetMod.Size = new System.Drawing.Size(94, 27);
            this.ButtonGetMod.TabIndex = 29;
            this.ButtonGetMod.Text = "Install";
            this.ButtonGetMod.UseVisualStyleBackColor = true;
            this.ButtonGetMod.Click += new System.EventHandler(this.ButtonGetMod_Click);
            // 
            // ButtonConfigMod
            // 
            this.ButtonConfigMod.Location = new System.Drawing.Point(112, 272);
            this.ButtonConfigMod.Name = "ButtonConfigMod";
            this.ButtonConfigMod.Size = new System.Drawing.Size(94, 27);
            this.ButtonConfigMod.TabIndex = 30;
            this.ButtonConfigMod.Text = "Configure";
            this.ButtonConfigMod.UseVisualStyleBackColor = true;
            this.ButtonConfigMod.Click += new System.EventHandler(this.ButtonConfigMod_Click);
            // 
            // FormModDownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ButtonConfigMod);
            this.Controls.Add(this.ButtonGetMod);
            this.Controls.Add(this.ListViewRegistryMods);
            this.Controls.Add(this.ManageModslabel);
            this.Name = "FormModDownload";
            this.Text = "FormModDownload";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ManageModslabel;
        private System.Windows.Forms.ListView ListViewRegistryMods;
        private System.Windows.Forms.Button ButtonGetMod;
        private System.Windows.Forms.Button ButtonConfigMod;
    }
}