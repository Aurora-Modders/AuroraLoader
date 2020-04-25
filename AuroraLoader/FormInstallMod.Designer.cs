namespace AuroraLoader
{
    partial class FormInstallMod
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
			this.TextUrl = new System.Windows.Forms.TextBox();
			this.ButtonOk = new System.Windows.Forms.Button();
			this.ButtonCancel = new System.Windows.Forms.Button();
			this.LabelUrl = new System.Windows.Forms.Label();
			this.ComboMods = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// TextUrl
			// 
			this.TextUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.TextUrl.Location = new System.Drawing.Point(93, 223);
			this.TextUrl.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.TextUrl.Name = "TextUrl";
			this.TextUrl.Size = new System.Drawing.Size(1094, 35);
			this.TextUrl.TabIndex = 0;
			this.TextUrl.TextChanged += new System.EventHandler(this.TextUrl_TextChanged);
			// 
			// ButtonOk
			// 
			this.ButtonOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ButtonOk.Location = new System.Drawing.Point(93, 333);
			this.ButtonOk.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.ButtonOk.Name = "ButtonOk";
			this.ButtonOk.Size = new System.Drawing.Size(343, 152);
			this.ButtonOk.TabIndex = 1;
			this.ButtonOk.Text = "Install";
			this.ButtonOk.UseVisualStyleBackColor = true;
			this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
			// 
			// ButtonCancel
			// 
			this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ButtonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ButtonCancel.Location = new System.Drawing.Point(487, 333);
			this.ButtonCancel.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.ButtonCancel.Name = "ButtonCancel";
			this.ButtonCancel.Size = new System.Drawing.Size(343, 152);
			this.ButtonCancel.TabIndex = 2;
			this.ButtonCancel.Text = "Cancel";
			this.ButtonCancel.UseVisualStyleBackColor = true;
			this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
			// 
			// LabelUrl
			// 
			this.LabelUrl.AutoSize = true;
			this.LabelUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.LabelUrl.Location = new System.Drawing.Point(87, 179);
			this.LabelUrl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.LabelUrl.Name = "LabelUrl";
			this.LabelUrl.Size = new System.Drawing.Size(353, 29);
			this.LabelUrl.TabIndex = 3;
			this.LabelUrl.Text = "Or enter the mod installation url:";
			// 
			// ComboMods
			// 
			this.ComboMods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ComboMods.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ComboMods.FormattingEnabled = true;
			this.ComboMods.Location = new System.Drawing.Point(268, 112);
			this.ComboMods.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.ComboMods.Name = "ComboMods";
			this.ComboMods.Size = new System.Drawing.Size(919, 37);
			this.ComboMods.TabIndex = 4;
			this.ComboMods.SelectedIndexChanged += new System.EventHandler(this.ComboMods_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.Location = new System.Drawing.Point(87, 117);
			this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(157, 29);
			this.label1.TabIndex = 5;
			this.label1.Text = "Choose mod:";
			// 
			// FormInstallMod
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1333, 527);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ComboMods);
			this.Controls.Add(this.LabelUrl);
			this.Controls.Add(this.ButtonCancel);
			this.Controls.Add(this.ButtonOk);
			this.Controls.Add(this.TextUrl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.Name = "FormInstallMod";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Install Mods";
			this.Load += new System.EventHandler(this.FormInstallMod_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextUrl;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Label LabelUrl;
        private System.Windows.Forms.ComboBox ComboMods;
        private System.Windows.Forms.Label label1;
    }
}