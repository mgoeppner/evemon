using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EVEMon.Common.Enumerations.CCPAPI;
using EVEMon.Common.Enumerations.UISettings;
using EVEMon.Common.Extensions;
using EVEMon.Common.Models.Extended;
using EVEMon.Common.Properties;
using EVEMon.Common.SettingsObjects;

namespace EVEMon.SettingsUI
{
    public partial class UpdateSettingsControl : UserControl
    {
        // Would have love to use tableLayoutPanel, unfortunately, they are just a piece of trash.
        private const int RowHeight = 28;

        private readonly List<ComboBox> m_combos = new List<ComboBox>();

        private UpdateSettings m_settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSettingsControl"/> class.
        /// </summary>
        public UpdateSettingsControl()
        {
            InitializeComponent();
            labelPeriod.Font = new Font(labelPeriod.Font, FontStyle.Bold);
            labelMethod.Font = new Font(labelMethod.Font, FontStyle.Bold);

            PopulateControl();
        }

        /// <summary>
        /// Gets or sets the settings to edit (should be a copy of the actual settings).
        /// </summary>
        [Browsable(false)]
        public UpdateSettings Settings
        {
            get { return m_settings; }
            set
            {
                if (value == null)
                    return;

                m_settings = value;

                foreach (ComboBox combo in m_combos)
                {
                    Enum method = (Enum)combo.Tag;
                    List<UpdatePeriod> periods = GetUpdatePeriods(method);
                    combo.SelectedIndex = Math.Max(0, periods.IndexOf(m_settings.Periods[method.ToString()]));
                }
            }
        }

        /// <summary>
        /// Gets the methods.
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Enum> Methods
        {
            get
            {
                List<Enum> apiMethods = ESIMethods.Methods.Where(x => x.HasHeader()).ToList();

                // Group the methods by usage
                List<Enum> methods = apiMethods.Where(method => method is ESIAPIGenericMethods).ToList();

                methods.AddRange(apiMethods.OfType<ESIAPICharacterMethods>().Where(
                    method => (long)method == ((long)method & (long)CCPAPIMethodsEnum.BasicCharacterFeatures)).Cast<Enum>());

                methods.AddRange(apiMethods.OfType<ESIAPICharacterMethods>().Where(
                    method => (long)method == ((long)method & (long)CCPAPIMethodsEnum.AdvancedCharacterFeatures)).Cast<Enum>().
                                     OrderBy(method => method.GetHeader()));
                
                return methods;
            }
        }

        /// <summary>
        /// When the selected index changes, we update the settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            Enum method = (Enum)combo.Tag;
            List<UpdatePeriod> periods = GetUpdatePeriods(method);

            if (combo.SelectedIndex < 0 || combo.SelectedIndex >= periods.Count)
                return;

            if (method.Equals(ESIAPICharacterMethods.Medals))
                m_settings.Periods[ESIAPICorporationMethods.CorporationMedals.ToString()] = periods[combo.SelectedIndex];
      
            if (method.Equals(ESIAPICharacterMethods.MarketOrders))
                m_settings.Periods[ESIAPICorporationMethods.CorporationMarketOrders.ToString()] = periods[combo.SelectedIndex];

            if (method.Equals(ESIAPICharacterMethods.Contracts))
                m_settings.Periods[ESIAPICorporationMethods.CorporationContracts.ToString()] = periods[combo.SelectedIndex];

            if (method.Equals(ESIAPICharacterMethods.IndustryJobs))
                m_settings.Periods[ESIAPICorporationMethods.CorporationIndustryJobs.ToString()] = periods[combo.SelectedIndex];

            m_settings.Periods[method.ToString()] = periods[combo.SelectedIndex];
        }

        /// <summary>
        /// Populates the control.
        /// </summary>
        private void PopulateControl()
        {
            int height = RowHeight;

            // Add the controls for every member of the enumeration
            foreach (Enum method in Methods)
            {
                // Add the icon
                AddIcon(method, height);

                // Add the label
                AddLabel(method, height);

                // Add the "system tray tooltip" combo box
                AddComboBox(method, height);

                // Updates the row ordinate
                height += RowHeight;
            }

            Height = height;
        }

        /// <summary>
        /// Adds the combo box.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="height">The height.</param>
        private void AddComboBox(Enum method, int height)
        {
            ComboBox tempCombo = null;
            try
            {
                tempCombo = new ComboBox();
                foreach (UpdatePeriod period in GetUpdatePeriods(method))
                {
                    string header = period.GetHeader();
                    if (period == UpdatePeriod.Never && method.HasForcedOnStartup())
                        header = "On Startup";

                    tempCombo.Items.Add(header);
                }
                tempCombo.Tag = method;
                tempCombo.SelectedIndex = 0;
                tempCombo.Margin = new Padding(3);
                tempCombo.Height = RowHeight - 4;
                tempCombo.Width = labelPeriod.Width;
                tempCombo.DropDownStyle = ComboBoxStyle.DropDownList;
                tempCombo.Location = new Point(labelPeriod.Location.X, height + 2);
                tempCombo.SelectedIndexChanged += combo_SelectedIndexChanged;

                ComboBox combo = tempCombo;
                tempCombo = null;

                Controls.Add(combo);
                m_combos.Add(combo);
            }
            finally
            {
                tempCombo?.Dispose();
            }
        }

        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="height">The height.</param>
        private void AddLabel(Enum method, int height)
        {
            Label tempLabel = null;
            try
            {
                tempLabel = new Label();
                toolTip.SetToolTip(tempLabel, method.GetDescription());
                tempLabel.AutoSize = false;
                tempLabel.Text = method.GetHeader();
                tempLabel.TextAlign = ContentAlignment.MiddleLeft;
                tempLabel.Location = new Point(labelMethod.Location.X, height);
                tempLabel.Width = labelMethod.Width;
                tempLabel.Height = RowHeight;

                Label label = tempLabel;
                tempLabel = null;

                Controls.Add(label);
            }
            finally
            {
                tempLabel?.Dispose();
            }
        }

        /// <summary>
        /// Adds the icon.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="height">The height.</param>
        private void AddIcon(Enum method, int height)
        {
            Bitmap icon = Resources.KeyGrey16;
            string iconToolTip = "This is a basic feature query.";
            if (method is ESIAPICharacterMethods)
            {
                ESIAPICharacterMethods apiMethod = (ESIAPICharacterMethods)method;
                if ((long)apiMethod == ((long)apiMethod & (long)CCPAPIMethodsEnum.AdvancedCharacterFeatures))
                {
                    icon = Resources.KeyGold16;
                    iconToolTip = "This is an advanced feature query.";
                }
            }
            
            PictureBox tempPicture = null;
            try
            {
                tempPicture = new PictureBox();
                toolTip.SetToolTip(tempPicture, iconToolTip);
                tempPicture.Location = new Point(0, height + (RowHeight - icon.Size.Height) / 2);
                tempPicture.Image = icon;
                tempPicture.Size = icon.Size;

                PictureBox picture = tempPicture;
                tempPicture = null;

                Controls.Add(picture);
            }
            finally
            {
                tempPicture?.Dispose();
            }
        }

        /// <summary>
        /// Gets the available update periods for the given method.
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        private static List<UpdatePeriod> GetUpdatePeriods(Enum method)
        {
            List<UpdatePeriod> periods = new List<UpdatePeriod> { UpdatePeriod.Never };

            int min = (int)method.GetUpdatePeriod().Minimum;
            int max = (int)method.GetUpdatePeriod().Maximum;

            periods.AddRange(Enum.GetValues(typeof(UpdatePeriod)).Cast<UpdatePeriod>().Where(
                period => period != UpdatePeriod.Never).Select(period => new { period, index = (int)period }).Where(
                    period => period.index >= min && period.index <= max).Select(period => period.period));

            return periods;
        }
    }
}
