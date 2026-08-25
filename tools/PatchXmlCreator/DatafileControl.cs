using System;
using System.Windows.Forms;

namespace EVEMon.PatchXmlCreator
{
    internal partial class DatafileControl : UserControl
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public DatafileControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Occurs on control click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClick(object sender, EventArgs e)
        {
            Parent.Focus();
        }
    }
}