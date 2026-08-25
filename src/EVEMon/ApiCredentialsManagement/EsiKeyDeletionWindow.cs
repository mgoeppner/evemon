using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EVEMon.Common;
using EVEMon.Common.Constants;
using EVEMon.Common.Controls;
using EVEMon.Common.Extensions;
using EVEMon.Common.Models;

namespace EVEMon.ApiCredentialsManagement
{
    public sealed partial class EsiKeyDeletionWindow : EVEMonForm
    {
        private readonly ESIKey m_apiKey;

        /// <summary>
        /// Constructor.
        /// </summary>
        private EsiKeyDeletionWindow()
        {
            InitializeComponent();
            deletionLabel.Font = new Font(deletionLabel.Font, FontStyle.Bold);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="esiKey">The ESI key.</param>
        /// <exception cref="System.ArgumentNullException">esiKey</exception>
        public EsiKeyDeletionWindow(ESIKey esiKey)
            : this()
        {
            esiKey.ThrowIfNull(nameof(esiKey), "ESI key can't be null");

            m_apiKey = esiKey;
        }

        /// <summary>
        /// Occurs when the control loads.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode)
                return;

            charactersListView.ItemCheck += charactersListView_ItemCheck;
            deletionLabel.Text = string.Format(CultureConstants.DefaultCulture, deletionLabel.Text, m_apiKey.ID);

            // Add characters
            charactersListView.Items.Clear();

            foreach (ListViewItem item in m_apiKey.CharacterIdentities.Select(
                id => new ListViewItem(id.CharacterName)
                          {
                              Tag = id.CCPCharacter,
                              Checked = id.CCPCharacter != null &&
                                        id.CCPCharacter.Identity.ESIKeys.All(key => key == m_apiKey),
                          }))
            {
                // Gray out a character with another associated API key
                if (!item.Checked)
                    item.ForeColor = SystemColors.GrayText;

                // Strikeout and gray out a character in the API key's ignored list
                if (item.Tag == null)
                {
                    item.Font = new Font(Font, FontStyle.Strikeout);
                    item.ForeColor = SystemColors.GrayText;
                    item.Checked = true;
                }

                charactersListView.Items.Add(item);
            }

            // If character list is empty resize the window
            if (charactersListView.Items.Count != 0)
                return;

            charactersListGroupBox.Visible = false;
            Size = new Size(Size.Width - charactersListGroupBox.Height / 2, Size.Height - charactersListGroupBox.Height);
        }

        /// <summary>
        /// Handles the ItemCheck event of the charactersListView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.ItemCheckEventArgs"/> instance containing the event data.</param>
        private void charactersListView_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CCPCharacter ccpCharacter = charactersListView.Items[e.Index].Tag as CCPCharacter;
            if (ccpCharacter == null)
                e.NewValue = CheckState.Checked;

            if (ccpCharacter != null && ccpCharacter.Identity.ESIKeys.Any(key => key != m_apiKey))
                e.NewValue = CheckState.Unchecked;
        }

        /// <summary>
        /// "Delete" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            // Remove the API key
            EveMonClient.ESIKeys.Remove(m_apiKey);

            // Remove the characters from the collection
            foreach (CCPCharacter ccpCharacter in charactersListView.Items.Cast<ListViewItem>().Where(
                item => item.Checked).Select(item => item.Tag as CCPCharacter).Where(
                    ccpCharacter => ccpCharacter != null).Where(
                        ccpCharacter => !ccpCharacter.Identity.ESIKeys.Any()))
            {
                EveMonClient.Characters.Remove(ccpCharacter);
            }

            // Closes the window
            Close();
        }
    }
}
