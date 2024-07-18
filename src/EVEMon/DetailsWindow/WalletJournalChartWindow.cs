using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using EVEMon.Common;
using EVEMon.Common.Controls;
using EVEMon.Common.CustomEventArgs;
using EVEMon.Common.Models;

namespace EVEMon.DetailsWindow
{
    public partial class WalletJournalChartWindow : EVEMonForm
    {
        private readonly CCPCharacter m_ccpCharacter;


        #region Constructor

        /// <summary>
        /// Prevents a default instance of the <see cref="WalletJournalChartWindow"/> class from being created.
        /// </summary>
        private WalletJournalChartWindow()
        {
            InitializeComponent();
            InitializeBalanceChart();
            InitializeAmountChart();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WalletJournalChartWindow"/> class.
        /// </summary>
        /// <param name="ccpCharacter">The CCP character.</param>
        public WalletJournalChartWindow(CCPCharacter ccpCharacter)
            : this()
        {
            m_ccpCharacter = ccpCharacter;
        }

        #endregion


        #region Inherited Events

        /// <summary>
        /// On load, restores the window rectangle from the settings.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            MinimumSize = Size;

            EveMonClient.CharacterWalletJournalUpdated += EveMonClient_CharacterWalletJournalUpdated;
            Disposed += OnDisposed;

            UpdateBalanceChart();
            UpdateAmountChart();
        }

        /// <summary>
        /// Called when disposed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnDisposed(object sender, EventArgs e)
        {
            EveMonClient.CharacterWalletJournalUpdated -= EveMonClient_CharacterWalletJournalUpdated;
        }

        #endregion


        #region Global Events

        /// <summary>
        /// Updates the charts when charcter wallet journal updates.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EVEMon.Common.CustomEventArgs.CharacterChangedEventArgs"/> instance containing the event data.</param>
        private void EveMonClient_CharacterWalletJournalUpdated(object sender, CharacterChangedEventArgs e)
        {
            if (m_ccpCharacter != e.Character)
                return;

            UpdateBalanceChart();
            UpdateAmountChart();
        }

        #endregion


        #region Charts Initialization

        /// <summary>
        /// Initializes the balance chart.
        /// </summary>
        private void InitializeBalanceChart()
        {
            // Configure the chart area
            BalanceChart.ChartAreas[0].BorderColor = Color.Black;
            BalanceChart.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;

            // Configure X axis
            BalanceChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Silver;
            BalanceChart.ChartAreas[0].AxisX.LabelAutoFitMaxFontSize = 8;
            BalanceChart.ChartAreas[0].AxisX.Interval = 1;

            // Configure Y axis
            BalanceChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Silver;
            BalanceChart.ChartAreas[0].AxisY.LabelStyle.Format = "N0";
            BalanceChart.ChartAreas[0].AxisY.LabelAutoFitMaxFontSize = 8;
            //BalanceChart.ChartAreas[0].AxisY.Interval = 1;

            // Configure series
            BalanceChart.Series[0].ChartType = SeriesChartType.Area;
            BalanceChart.Series[0].XValueType = ChartValueType.DateTime;
            BalanceChart.Series[0].YValueType = ChartValueType.Auto;
            BalanceChart.Series[0].Color = Color.LightSkyBlue;
            BalanceChart.Series[0].BorderColor = Color.Blue;
            BalanceChart.Series[0].MarkerSize = 3;
            BalanceChart.Series[0].MarkerColor = Color.Black;
            BalanceChart.Series[0].MarkerStyle = MarkerStyle.Diamond;
        }

        /// <summary>
        /// Initializes the amount chart.
        /// </summary>
        private void InitializeAmountChart()
        {
            foreach (ChartArea chartArea in AmountChart.ChartAreas)
            {
                // Configure the chart area
                chartArea.BorderColor = Color.Black;
                chartArea.BorderDashStyle = ChartDashStyle.Solid;

                // Configure X axis
                chartArea.AxisX.IsMarksNextToAxis = false;
                chartArea.AxisX.MajorGrid.LineColor = Color.Silver;
                chartArea.AxisX.LabelAutoFitMaxFontSize = 8;
                chartArea.AxisX.Interval = 1;

                // Configure Y axis
                chartArea.AxisY.MajorGrid.LineColor = Color.Silver;
                chartArea.AxisY.LabelStyle.Format = "N0";
                chartArea.AxisY.LabelAutoFitMaxFontSize = 8;
                chartArea.AxisY.Crossing = 0D;
            }

            // Disable the X axis labels and tick marks for the second chart
            AmountChart.ChartAreas[1].AxisX.MajorGrid.Enabled = false;
            AmountChart.ChartAreas[1].AxisX.MajorTickMark.Enabled = false;
            AmountChart.ChartAreas[1].AxisX.LabelStyle.Enabled = false;

            foreach (Series series in AmountChart.Series)
            {
                // Configure series
                series.CustomProperties = "PixelPointWidth=5";
                series.ChartType = SeriesChartType.Column;
                series.XValueType = ChartValueType.DateTime;
                series.YValueType = ChartValueType.Auto;
            }
        }

        #endregion


        #region Update Methods

        /// <summary>
        /// Updates the balance chart.
        /// </summary>
        private void UpdateBalanceChart()
        {
            BalanceChart.Series[0].Points.Clear();

            // Set the data points
            foreach (WalletJournal journal in m_ccpCharacter.WalletJournal.OrderByDescending(journal => journal.Date))
            {
                DataPoint dataPoint = new DataPoint();
                {
                    dataPoint.SetValueXY(journal.Date.ToLocalTime(), journal.Balance);
                    dataPoint.ToolTip = $"{journal.Date.ToLocalTime():G}{Environment.NewLine}{journal.Balance:N2} ISK";

                    BalanceChart.Series[0].Points.Add(dataPoint.Clone());
                }
            }
        }

        /// <summary>
        /// Updates the amount chart.
        /// </summary>
        private void UpdateAmountChart()
        {
            AmountChart.Series[0].Points.Clear();
            AmountChart.Series[1].Points.Clear();

            // Set the data points for the first chart
            foreach (WalletJournal journal in m_ccpCharacter.WalletJournal.OrderByDescending(journal => journal.Date))
            {
                DataPoint dataPoint = new DataPoint();
                {
                    dataPoint.SetValueXY(journal.Date.ToLocalTime(), journal.Amount);
                    dataPoint.Color = journal.Amount < 0 ? Color.DarkRed : Color.DarkGreen;
                    dataPoint.ToolTip = $"{journal.Date.ToLocalTime():G}{Environment.NewLine}{journal.Amount:N2} ISK";

                    // Add the data point to series
                    AmountChart.Series[0].Points.Add(dataPoint.Clone());
                }
            }

            // Set the data points for the second chart
            DataPoint positiveSumDataPoint = new DataPoint();
            {
                decimal positiveSum =
                    m_ccpCharacter.WalletJournal.Where(journal => journal.Amount > 0).Sum(journal => journal.Amount);
                positiveSumDataPoint.SetValueXY(0, positiveSum);
                positiveSumDataPoint.Color = Color.DarkGreen;
                positiveSumDataPoint.ToolTip = $"Inflow{Environment.NewLine}{positiveSum:N2} ISK";
                // Add the data point to series
                AmountChart.Series[1].Points.Add(positiveSumDataPoint.Clone());
            }

            DataPoint negativeSumDataPoint = new DataPoint();
            {
                decimal negativeSum =
                    m_ccpCharacter.WalletJournal.Where(journal => journal.Amount < 0).Sum(journal => journal.Amount);
                negativeSumDataPoint.SetValueXY(0, negativeSum);
                negativeSumDataPoint.Color = Color.DarkRed;
                negativeSumDataPoint.ToolTip = $"Outflow{Environment.NewLine}{negativeSum:N2} ISK";

                // Add the data point to series
                AmountChart.Series[1].Points.Add(negativeSumDataPoint.Clone());
            }
        }

        #endregion
    }
}
