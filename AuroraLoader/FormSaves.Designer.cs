namespace AuroraLoader
{
    partial class FormSaves
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
            this.ListViewSaves = new System.Windows.Forms.ListView();
            this.LabelSave = new System.Windows.Forms.Label();
            this.ButtonLoadSaves = new System.Windows.Forms.Button();
            this.ButtonResetSaves = new System.Windows.Forms.Button();
            this.ButtonNewGame = new System.Windows.Forms.Button();
            this.TextNewGame = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ListViewSaves
            // 
            this.ListViewSaves.HideSelection = false;
            this.ListViewSaves.Location = new System.Drawing.Point(12, 27);
            this.ListViewSaves.MultiSelect = false;
            this.ListViewSaves.Name = "ListViewSaves";
            this.ListViewSaves.Size = new System.Drawing.Size(571, 199);
            this.ListViewSaves.TabIndex = 0;
            this.ListViewSaves.UseCompatibleStateImageBehavior = false;
            this.ListViewSaves.View = System.Windows.Forms.View.List;
            this.ListViewSaves.SelectedIndexChanged += new System.EventHandler(this.ListViewSaves_SelectedIndexChanged);
            // 
            // LabelSave
            // 
            this.LabelSave.AutoSize = true;
            this.LabelSave.Location = new System.Drawing.Point(12, 9);
            this.LabelSave.Name = "LabelSave";
            this.LabelSave.Size = new System.Drawing.Size(65, 15);
            this.LabelSave.TabIndex = 1;
            this.LabelSave.Text = "Select Save";
            // 
            // ButtonLoadSaves
            // 
            this.ButtonLoadSaves.Location = new System.Drawing.Point(12, 232);
            this.ButtonLoadSaves.Name = "ButtonLoadSaves";
            this.ButtonLoadSaves.Size = new System.Drawing.Size(75, 23);
            this.ButtonLoadSaves.TabIndex = 2;
            this.ButtonLoadSaves.Text = "Load";
            this.ButtonLoadSaves.UseVisualStyleBackColor = true;
            this.ButtonLoadSaves.Click += new System.EventHandler(this.ButtonLoadSaves_Click);
            // 
            // ButtonResetSaves
            // 
            this.ButtonResetSaves.Enabled = false;
            this.ButtonResetSaves.Location = new System.Drawing.Point(93, 232);
            this.ButtonResetSaves.Name = "ButtonResetSaves";
            this.ButtonResetSaves.Size = new System.Drawing.Size(75, 23);
            this.ButtonResetSaves.TabIndex = 3;
            this.ButtonResetSaves.Text = "Reset";
            this.ButtonResetSaves.UseVisualStyleBackColor = true;
            this.ButtonResetSaves.Click += new System.EventHandler(this.ButtonResetSaves_Click);
            // 
            // ButtonNewGame
            // 
            this.ButtonNewGame.Location = new System.Drawing.Point(174, 232);
            this.ButtonNewGame.Name = "ButtonNewGame";
            this.ButtonNewGame.Size = new System.Drawing.Size(75, 23);
            this.ButtonNewGame.TabIndex = 4;
            this.ButtonNewGame.Text = "New Game";
            this.ButtonNewGame.UseVisualStyleBackColor = true;
            this.ButtonNewGame.Enabled = false;
            this.ButtonNewGame.Click += new System.EventHandler(this.ButtonNewGame_Click);
            // 
            // TextNewGame
            // 
            this.TextNewGame.Location = new System.Drawing.Point(255, 232);
            this.TextNewGame.Name = "TextNewGame";
            this.TextNewGame.Size = new System.Drawing.Size(328, 23);
            this.TextNewGame.TabIndex = 5;
            this.TextNewGame.PlaceholderText = "New game name";
            this.TextNewGame.TextChanged += new System.EventHandler(this.TextNewGame_Changed);
            // 
            // FormSaves
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 268);
            this.Controls.Add(this.TextNewGame);
            this.Controls.Add(this.ButtonNewGame);
            this.Controls.Add(this.ButtonResetSaves);
            this.Controls.Add(this.ButtonLoadSaves);
            this.Controls.Add(this.LabelSave);
            this.Controls.Add(this.ListViewSaves);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormSaves";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage Savegames";
            this.Load += new System.EventHandler(this.FormSaves_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView ListViewSaves;
        private System.Windows.Forms.Label LabelSave;
        private System.Windows.Forms.Button ButtonLoadSaves;
        private System.Windows.Forms.Button ButtonResetSaves;
        private System.Windows.Forms.Button ButtonNewGame;
        private System.Windows.Forms.TextBox TextNewGame;
    }
}