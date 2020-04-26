using AuroraLoader.Registry;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace AuroraLoader
{
    public partial class FormInstallMod : Form
    {
        private readonly IConfiguration _configuration;
        private readonly ModRegistry _modRegistry;

        public FormInstallMod(IConfiguration configuration, ModRegistry modRegistry)
        {
            InitializeComponent();
            _configuration = configuration;
            _modRegistry = modRegistry;
        }

        private void FormInstallMod_Load(object sender, EventArgs e)
        {
            UpdateCombo();
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            ButtonOk.Enabled = false;
            ButtonCancel.Enabled = false;
            Cursor = Cursors.WaitCursor;

            var selectedItem = (string)ComboMods.SelectedItem;
            if (selectedItem != null)
            {
                _modRegistry.InstallOrUpdate(_modRegistry.Mods.Single(mod => mod.InstallText == selectedItem || mod.UpdateText == selectedItem));
                UpdateCombo();
            }

            ButtonOk.Enabled = true;
            ButtonCancel.Enabled = true;
        }

        private void UpdateCombo()
        {
            ComboMods.Items.Clear();
            foreach (var mod in _modRegistry.Mods.Where(mod => !mod.Installed))
            {
                ComboMods.Items.Add(mod.InstallText);
            }

            foreach (var mod in _modRegistry.Mods.Where(mod => mod.CanBeUpdated))
            {
                ComboMods.Items.Add(mod.UpdateText);
            }

            if (_modRegistry.Mods.Any(mod => !mod.Installed || mod.CanBeUpdated))
            {
                ComboMods.Enabled = true;
                ButtonOk.Enabled = true;
            }
            else
            {
                ComboMods.Items.Add($"Couldn't find any mods to install or update");
                ComboMods.Enabled = false;
                ButtonOk.Enabled = false;
            }
            ComboMods.SelectedIndex = 0;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        // TODO Replace 'add a mirror' functionality (likely elsewhere)
        private void ComboMods_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
