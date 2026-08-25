using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using EVEMon.Common;
using EVEMon.Common.Constants;
using EVEMon.Common.Interfaces;
using EVEMon.Common.Models;

namespace EVEMon.Controls
{
    public partial class ReadingPane : UserControl
    {
        private IEveMessage m_selectedObject;


        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadingPane"/> class.
        /// </summary>
        public ReadingPane()
        {
            InitializeComponent();

            lblMessageHeader.Font = new Font(SystemFonts.DefaultFont.FontFamily, 10F, FontStyle.Bold);
            flPanelHeader.ForeColor = SystemColors.ControlText;
        }


        #endregion


        #region Properties

        /// <summary>
        /// Gets or sets the selected object.
        /// </summary>
        /// <value>The selected object.</value>
        internal IEveMessage SelectedObject
        {
            get { return m_selectedObject; }
            set
            {
                m_selectedObject = value;
                UpdatePane();
            }
        }

        #endregion


        #region Main Methods

        /// <summary>
        /// Hides the reading pane.
        /// </summary>
        internal void HidePane()
        {
            Visible = false;
        }

        /// <summary>
        /// Updates the reading pane.
        /// </summary>
        internal void UpdatePane()
        {
            // Update the text on the header labels
            lblMessageHeader.Text = m_selectedObject.Title;
            lblSender.Text = $"From: {string.Join(", ", m_selectedObject.SenderName)}";
            lblSendDate.Text = $"Sent: {m_selectedObject.SentDate.ToLocalTime():ddd} {m_selectedObject.SentDate.ToLocalTime():G}";
            lblRecipient.Text = $"To: {string.Join(", ", m_selectedObject.Recipient)}";

            // Parce the mail body text to the web browser
            // so for the text to be formatted accordingly
            wbMailBody.DocumentText = TidyUpHTML();
            
            // We need to wait for the Document to be loaded
            do
            {
                Application.DoEvents();
            } while (wbMailBody.IsBusy);

            // Show the controls
            bool visible = ((m_selectedObject as EveMailMessage)?.EVEMailBody?.MessageID ?? 0L) !=
                0L || ((m_selectedObject as EveNotification)?.EVENotificationText?.
                NotificationID ?? 0L) != 0L;
            Visible = visible;

            // WebBrowser errata sometimes causes a COMException to be thrown if this is set
            // in the designer before the window is visible
            if (visible)
                wbMailBody.AllowWebBrowserDrop = false;
        }

        /// <summary>
        /// Prepares the text to be shown as common HTML.
        /// </summary>
        /// <returns></returns>
        private string TidyUpHTML()
        {
            var replacements = new Dictionary<string, string>();

            FormatLinks(replacements);
            FormatHTMLColorToRGB(replacements);
            FixFontSize(replacements);

            return replacements.Aggregate(m_selectedObject.Text, (formatted, replacement) =>
                formatted.Replace(replacement.Key, replacement.Value));
        }

        #endregion


        #region Formatting Methods

        /// <summary>
        /// Formats the links.
        /// </summary>
        /// <param name="replacements">The replacements.</param>
        private void FormatLinks(IDictionary<string, string> replacements)
        {
            // Regular expression for all HTML links
            Regex regexLinks = new Regex(@"<a\shref=""(.+?)"">(.+?)</a>", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            // Regular expression for clickable/valid URLs
            Regex regexWebProtocol = new Regex(@"(?:f|ht)tps?://", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            foreach (Match match in regexLinks.Matches(m_selectedObject.Text))
            {
                string matchValue = match.Groups[1].Value;
                string matchText = match.Groups[2].Value.TrimEnd("<br>".ToCharArray());
                string url = string.Empty;
                bool igbOnly = false;

                if (regexWebProtocol.IsMatch(matchValue))
                    url = matchValue;
                else
                    igbOnly = true;

                if (!igbOnly)
                {
                    replacements[match.ToString()] =
                        $"<a href=\"{url}\" title=\"{url}{Environment.NewLine}Click to follow the link\">{matchText}</a>";
                }
                else
                {
                    replacements[match.ToString()] =
                        $"<span style=\"text-decoration: underline; cursor: pointer;\" title=\"{matchValue}{Environment.NewLine}" +
                        $"Link works only in IGB\">{matchText}</span>";
                }
            }
        }

        /// <summary>
        /// Formats the color to RGB.
        /// </summary>
        /// <param name="replacements">The replacements.</param>
        private void FormatHTMLColorToRGB(IDictionary<string, string> replacements)
        {
            Color backColor = flPanelHeader.BackColor;

            // Regular expression for fixing text color
            Regex regexColor = new Regex(@"color(?:=""|:\s*)#[0-9a-f]{2}([0-9a-f]{6})(?:;|"")", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (Match match in regexColor.Matches(m_selectedObject.Text))
            {
                replacements[match.ToString()] = $"color=\"#{CheckTextColorNotMatchBackColor(backColor, match)}\"";
            }
        }

        /// <summary>
        /// Checks the text color does not match the background color.
        /// </summary>
        /// <param name="backColor">The controls' background back.</param>
        /// <param name="match">The text color.</param>
        /// <returns>The text color as it was or a black colored text</returns>
        private static string CheckTextColorNotMatchBackColor(Color backColor, Match match)
        {
            string color = match.Groups[1].Value;
            Color textColor = ColorTranslator.FromHtml($"#{color}");
            bool textColorIsShadeOfWhite = textColor.R == textColor.G && textColor.G == textColor.B;
            bool backColorIsShadeOfWhite = backColor.R == backColor.G && backColor.G == backColor.B;
            if (!textColorIsShadeOfWhite || !backColorIsShadeOfWhite)
                return color;

            const int ContrastDiff = 64;
            int colorValue = textColor.R <= backColor.R - ContrastDiff ? textColor.R : 0;
            string colorElement = Convert.ToString(colorValue, 16);
            colorElement = colorElement.Length == 1 ? $"0{colorElement}" : colorElement;
            return $"{colorElement}{colorElement}{colorElement}";
        }

        /// <summary>
        /// Fixes the size of the font.
        /// </summary>
        /// <param name="replacements">The replacements.</param>
        private void FixFontSize(IDictionary<string, string> replacements)
        {
            Regex regexFontSize = new Regex(@"size(?:=""|:\s*)([0-9]+)(?:;|"")", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (Match match in regexFontSize.Matches(m_selectedObject.Text))
            {
                int newFontSize = Convert.ToByte(match.Groups[1].Value, CultureConstants.InvariantCulture) / 4;
                replacements[match.ToString()] = $"size=\"{newFontSize}\"";
            }
        }

        #endregion


        #region Local Events

        /// <summary>
        /// Every time the mail header panel gets painted we add a line at the bottom.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        private void flPanelHeader_Paint(object sender, PaintEventArgs e)
        {
            // Calculate the height of the panel
            flPanelHeader.Height = lblMessageHeader.Height + lblSender.Height + lblSendDate.Height + lblRecipient.Height + 10;

            // Draw a line at the bottom of the panel
            using (Graphics g = flPanelHeader.CreateGraphics())
            {
                using (Pen blackPen = new Pen(Color.Black))
                {
                    g.DrawLine(blackPen, 5, flPanelHeader.Height - 1, flPanelHeader.Width - 5, flPanelHeader.Height - 1);
                }
            }
        }

        /// <summary>
        /// Handles the Navigating event of the wbMailBody control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.WebBrowserNavigatingEventArgs"/> instance containing the event data.</param>
        private void wbMailBody_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            // We assure that the internal browser will initialize and
            // any other attempt to navigate to a non valid link will fail
            if (e.Url.AbsoluteUri == "about:blank" && wbMailBody.DocumentText != m_selectedObject.Text)
                return;

            // If the link complies with HTTP or HTTPS, open the link on the system's default browser
            if (e.Url.Scheme == Uri.UriSchemeHttp || e.Url.Scheme == Uri.UriSchemeHttps)
                Util.OpenURL(e.Url);

            // Prevents the browser to navigate past the shown page
            e.Cancel = true;
        }

        /// <summary>
        /// Handles the PreviewKeyDown event of the wbMailBody control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PreviewKeyDownEventArgs"/> instance containing the event data.</param>
        private void wbMailBody_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // Disables the reload shortcut key
            wbMailBody.WebBrowserShortcutsEnabled = e.KeyData != Keys.F5;
        }

        #endregion
    }
}
