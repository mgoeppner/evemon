using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EVEMon.Common;
using EVEMon.Common.Controls;
using EVEMon.Common.CustomEventArgs;
using EVEMon.Common.Data;
using EVEMon.Common.Enumerations;
using EVEMon.Common.Extensions;
using EVEMon.Common.Factories;
using EVEMon.Common.Helpers;
using EVEMon.Common.Interfaces;
using EVEMon.Common.Models;
using System.IO;
using System.Reflection;

namespace EVEMon.SkillPlanner
{
    /// <summary>
    /// UserControl to display a tree of masteries.
    /// </summary>
    public sealed partial class MasteryTreeDisplayControl : UserControl
    {
        private const string TrainedIcon = "Trained";
        private const string TrainableIcon = "Trainable";
        private const string UntrainableIcon = "Untrainable";
        private const string SkillBookIcon = "Skillbook";
        private const string PlannedIcon = "Planned";

        // Blank image list for 'Safe for work' setting
        private readonly ImageList m_emptyImageList = new ImageList();
        private readonly Font m_boldFont;

        private Plan m_plan;
        private Character m_character;
        private MasteryShip m_masteryShip;

        private bool m_allExpanded;


        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MasteryTreeDisplayControl"/> class.
        /// </summary>
        public MasteryTreeDisplayControl()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.DoubleBuffer |
                     ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.ContainerControl |
                     ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();

            InitializeComponent();

            treeView.DrawNode += treeView_DrawNode;
            treeView.MouseDown += treeView_MouseDown;
            treeView.MouseMove += treeView_MouseMove;

            cmListSkills.Opening += cmListSkills_Opening;


            // Fix for grey background on transparent icons
            // Reset ImageStream to remove any designer-imposed 8-bit limitations
            imageListCertLevels.ImageStream = null;
            imageListCertLevels.ColorDepth = ColorDepth.Depth32Bit;
            imageListCertLevels.ImageSize = new Size(24, 24);
            imageListCertLevels.TransparentColor = Color.Transparent;

            imageListMasteryLevels.ImageStream = null;
            imageListMasteryLevels.ColorDepth = ColorDepth.Depth32Bit;
            imageListMasteryLevels.ImageSize = new Size(24, 24);
            imageListMasteryLevels.TransparentColor = Color.Transparent;

            imageList.ColorDepth = ColorDepth.Depth32Bit; // Also for standard icons if needed

            // Manually reload images from embedded resources to ensure quality and correct file usage
            ReloadImageListFromResources(imageListCertLevels, "level{0}_24.png");
            ReloadImageListFromResources(imageListMasteryLevels, "masteryLevel{0}_24.png");

            m_boldFont = FontFactory.GetFont(Font, FontStyle.Bold);
            treeView.Font = FontFactory.GetFont("Microsoft Sans Serif", 8.25F);
            treeView.ItemHeight = treeView.Font.Height * 2 + 6;

            m_emptyImageList.ImageSize = new Size(30, 24);
            m_emptyImageList.Images.Add(new Bitmap(30, 24));
        }

        #endregion


        #region Public Properties

        /// <summary>
        /// Gets or sets the current plan.
        /// </summary>
        internal Plan Plan
        {
            get { return m_plan; }
            set
            {
                m_plan = value;
                if (m_plan == null)
                    return;

                m_character = (Character)m_plan.Character;
                UpdateTree();
            }
        }

        /// <summary>
        /// Gets or sets the mastery ship.
        /// </summary>
        internal MasteryShip MasteryShip
        {
            get { return m_masteryShip; }
            set
            {
                if (value == m_masteryShip)
                    return;

                m_masteryShip = value;
                m_character = m_masteryShip?.Character;
                UpdateTree();
            }
        }

        /// <summary>
        /// Gets mastery of the displayed class which contains the current selection.
        /// </summary>
        private Mastery SelectedMasteryLevel
        {
            get
            {
                TreeNode node = treeView.SelectedNode;
                while (node != null)
                {
                    Mastery masteryLevel = node.Tag as Mastery;
                    if (masteryLevel != null)
                        return masteryLevel;

                    node = node.Parent;
                }
                return null;
            }
        }
        
        #endregion


        #region Event Handlers

        /// <summary>
        /// On load, complete the initialization.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Return on design mode
            if (DesignMode || this.IsDesignModeHosted())
                return;

            EveMonClient.SettingsChanged += EveMonClient_SettingsChanged;
            EveMonClient.CharacterUpdated += EveMonClient_CharacterUpdated;
            EveMonClient.PlanChanged += EveMonClient_PlanChanged;
            Disposed += OnDisposed;
        }

        /// <summary>
        /// Unsubscribe events on disposing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDisposed(object sender, EventArgs e)
        {
            EveMonClient.SettingsChanged -= EveMonClient_SettingsChanged;
            EveMonClient.CharacterUpdated -= EveMonClient_CharacterUpdated;
            EveMonClient.PlanChanged -= EveMonClient_PlanChanged;
            Disposed -= OnDisposed;
        }

        /// <summary>
        /// On settings change, we update the tree.
        /// </summary>
        /// <remarks>Relates to safe for work setting</remarks>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EveMonClient_SettingsChanged(object sender, EventArgs e)
        {
            UpdateTree();
        }

        /// <summary>
        /// Fired when one of the character changed (skill completion, update from CCP, etc).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EveMonClient_CharacterUpdated(object sender, CharacterChangedEventArgs e)
        {
            if (m_plan == null)
                return;

            if (e.Character != m_plan.Character)
                return;

            UpdateTree();
        }

        /// <summary>
        /// Occurs when the plan changes, we update the tree.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EveMonClient_PlanChanged(object sender, PlanChangedEventArgs e)
        {
            if ((e.Plan != m_plan) || (e.Plan.Character != m_plan.Character))
                return;

            UpdateTree();
        }

        /// <summary>
        /// When the treeview is clicked, we manually select nodes since the bounding boxes are incorrect due to custom draw.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                treeView.Cursor = Cursors.Default;

            // Perform the selection manually since the bound's width and x are incorrect in owndraw
            TreeNode selection = null;
            for (TreeNode node = treeView.TopNode; node != null; node = node.NextVisibleNode)
            {
                if (node.Bounds.Top > e.Y || node.Bounds.Bottom < e.Y)
                    continue;

                // If the user clicked the "arrow zone", we do not change the selection and just return
                if (e.X < node.Bounds.Left - 32)
                    return;

                selection = node;
                break;
            }

            treeView.SelectedNode = selection;
        }

        /// <summary>
        /// When the mouse moves over the list, we change the cursor.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void treeView_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;

            treeView.Cursor = CustomCursors.ContextMenu;
        }

        /// <summary>
        /// Forces the selection update when a node is right-clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                treeView.SelectedNode = e.Node;
        }

        /// <summary>
        /// Event handler for treenode double click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is Mastery)
            {
                if (e.Node.IsExpanded)
                {
                    e.Node.Collapse();
                    return;
                }

                e.Node.Expand();
            }

            showInBrowserMenu_Click(sender, e);
        }

        #endregion


        #region Tree building

        /// <summary>
        /// Updates the shown treecontrol
        /// </summary>
        private void UpdateTree()
        {
            // Multiple copies of "CPU Management IV" etc. could exist in the tree. To restore
            // the same selection, the full path to the selection must be stored
            string path = treeView.SelectedNode?.FullPath ?? "";

            treeView.ImageList = Settings.UI.SafeForWork ? m_emptyImageList : imageList;
            treeView.BeginUpdate();
            try
            {
                // Clear the old items
                treeView.Nodes.Clear();

                // No update when not fully initialized
                if (m_character == null || m_masteryShip == null)
                    return;

                // Create the nodes when not done, yet
                if (treeView.Nodes.Count == 0)
                    foreach (Mastery masteryLevel in m_masteryShip)
                    {
                        var node = CreateNode(masteryLevel);
                        treeView.Nodes.Add(node);
                    }

                // Update the nodes
                foreach (TreeNode node in treeView.Nodes)
                {
                    UpdateNode(node);
                }

                // If old selection exists, select it
                if (!string.IsNullOrEmpty(path))
                {
                    var nodes = treeView.Nodes;
                    // Iterate through all components in the path
                    string[] components = path.Split(new string[] { treeView.PathSeparator },
                        StringSplitOptions.None);
                    int nc = components.Length;
                    for (int index = 0; index < nc && nodes != null; index++)
                    {
                        TreeNode child = null;
                        string component = components[index];
                        int n = nodes.Count;
                        // Search nodes for a node with the same text
                        for (int i = 0; i < n && child == null; i++)
                        {
                            var candidate = nodes[i];
                            if (candidate.Text?.Equals(component) ?? false)
                                child = candidate;
                        }
                        nodes = child?.Nodes;
                        if (index == nc - 1 && child != null)
                        {
                            // Found node, select it
                            child.EnsureVisible();
                            treeView.SelectedNode = child;
                        }
                    }
                }
            }
            finally
            {
                treeView.EndUpdate();
            }
        }

        /// <summary>
        /// Create a node from a mastery.
        /// </summary>
        /// <param name="masteryLevel">The mastery level.</param>
        /// <returns>The node created.</returns>
        private TreeNode CreateNode(Mastery masteryLevel)
        {
            var node = new TreeNode
            {
                Text = masteryLevel.ToString(),
                Tag = masteryLevel
            };

            foreach (var cert in masteryLevel.OrderBy(cert => cert.Certificate.Class.Name))
            {
                var certificate = cert.ToCharacter(m_character).GetCertificateLevel(
                    masteryLevel.Level);
                node.Nodes.Add(CreateNode(certificate));
            }

            return node;
        }

        /// <summary>
        /// Create a node from a certificate.
        /// </summary>
        /// <param name="certificateLevel">The certificate level.</param>
        /// <returns></returns>
        private static TreeNode CreateNode(CertificateLevel certificateLevel)
        {
            TreeNode node = new TreeNode
            {
                Text = certificateLevel.Certificate.Class.ToString(),
                Tag = certificateLevel
            };

            // Add this certificate's prerequisites
            foreach (SkillLevel prereqSkill in certificateLevel.PrerequisiteSkills)
            {
                node.Nodes.Add(CreateNode(prereqSkill));
            }

            return node;
        }

        /// <summary>
        /// Create a node from a prerequisite skill.
        /// </summary>
        /// <param name="skillPrereq">The skill prerequesites</param>
        /// <returns></returns>
        private static TreeNode CreateNode(SkillLevel skillPrereq)
        {
            TreeNode node = new TreeNode
            {
                Text = skillPrereq.ToString(),
                Tag = skillPrereq
            };

            // Add this skill's prerequisites
            foreach (var prereqSkill in skillPrereq.Skill.Prerequisites)
            {
                if (prereqSkill.Skill != skillPrereq.Skill)
                    node.Nodes.Add(CreateNode(prereqSkill));
            }

            return node;
        }

        /// <summary>
        /// Updates the specified node and its children.
        /// </summary>
        /// <param name="node">The Treenode</param>
        private void UpdateNode(TreeNode node)
        {
            Mastery masteryLevel = node.Tag as Mastery;
            CertificateLevel certLevel = node.Tag as CertificateLevel;

            // The node represents a mastery level
            if (masteryLevel != null)
            {
                if (masteryLevel.IsTrained)
                    node.ImageIndex = imageList.Images.IndexOfKey(TrainedIcon);
                else if (masteryLevel.IsPartiallyTrained)
                    node.ImageIndex = imageList.Images.IndexOfKey(TrainableIcon);
                else
                    node.ImageIndex = imageList.Images.IndexOfKey(UntrainableIcon);
            }
            // The node represents a certificate level
            else if (certLevel != null)
            {
                if (certLevel.IsTrained)
                    node.ImageIndex = imageList.Images.IndexOfKey(TrainedIcon);
                else if (certLevel.IsPartiallyTrained)
                    node.ImageIndex = imageList.Images.IndexOfKey(TrainableIcon);
                else
                    node.ImageIndex = imageList.Images.IndexOfKey(UntrainableIcon);
            }
            // The node represents a skill prerequisite
            else
            {
                SkillLevel skillPrereq = (SkillLevel)node.Tag;
                Skill skill = m_character.Skills[skillPrereq.Skill.ID];

                if (m_plan != null)
                {
                    if (skillPrereq.IsTrained)
                        node.ImageIndex = imageList.Images.IndexOfKey(TrainedIcon);
                    else if (m_plan.IsPlanned(skill, skillPrereq.Level))
                        node.ImageIndex = imageList.Images.IndexOfKey(PlannedIcon);
                    else if (skill.IsKnown)
                        node.ImageIndex = imageList.Images.IndexOfKey(TrainableIcon);
                    else
                        node.ImageIndex = imageList.Images.IndexOfKey(UntrainableIcon);
                }
            }

            // When selected, the image remains the same
            node.SelectedImageIndex = node.ImageIndex;

            // Update the children
            foreach (TreeNode child in node.Nodes)
            {
                UpdateNode(child);
            }
        }

        /// <summary>
        /// Custom draw for the label.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            // Prevents a bug that causes every item to be redrawn at the top left corner
            if (e.Bounds.Left <= 10)
            {
                e.DrawDefault = true;
                return;
            }

            string line2 = "-";
            int supIcon = -1;
            ImageList il;

            Mastery masteryLevel = e.Node.Tag as Mastery;
            CertificateLevel certLevel = e.Node.Tag as CertificateLevel;

            // Is it a mastery level ?
            if (masteryLevel != null)
            {
                if (!Settings.UI.SafeForWork)
                {
                    // Untrained icons at indices 0-5, trained icons at indices 6-11
                    supIcon = masteryLevel.Level + (masteryLevel.IsTrained ? 6 : 0);
                }

                il = imageListMasteryLevels;

                // When not trained, let's display the training time of all certificates of this level
                if (!masteryLevel.IsTrained)
                {
                    line2 = masteryLevel.GetTrainingTime().ToDescriptiveText(
                        DescriptiveTextOptions.IncludeCommas);
                }
            }
            else if (certLevel != null)
            {
                if (!Settings.UI.SafeForWork)
                {
                    // Untrained icons at indices 0-5, trained icons at indices 6-11
                    supIcon = (int)certLevel.Level + (certLevel.IsTrained ? 6 : 0);
                }

                il = imageListCertLevels;

                // When not trained, let's display the training time
                if (!certLevel.IsTrained)
                {
                    line2 = certLevel.GetTrainingTime.ToDescriptiveText(DescriptiveTextOptions.
                        IncludeCommas);
                }
            }
            // Or a skill prerequisite ?
            else
            {
                if (!Settings.UI.SafeForWork)
                    supIcon = imageList.Images.IndexOfKey(SkillBookIcon);

                il = imageList;
                SkillLevel skillPrereq = (SkillLevel)e.Node.Tag;

                // When not known to the require level, let's display the training time
                if (!skillPrereq.IsTrained)
                {
                    TimeSpan time = skillPrereq.Skill.GetLeftTrainingTimeToLevel(skillPrereq.Level);
                    line2 = time.ToDescriptiveText(DescriptiveTextOptions.IncludeCommas);
                }
            }

            // Choose colors according to selection
            bool isSelected = (e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected;
            Color backColor = isSelected ? SystemColors.Highlight : treeView.BackColor;
            Color foreColor = isSelected ? SystemColors.HighlightText : treeView.ForeColor;
            Color lightForeColor = isSelected ? SystemColors.HighlightText : SystemColors.GrayText;

            // Draws the background
            using (SolidBrush background = new SolidBrush(backColor))
            {
                int width = treeView.ClientSize.Width - e.Bounds.Left;
                e.Graphics.FillRectangle(background, new Rectangle(e.Bounds.Left, e.Bounds.Top, width, e.Bounds.Height));
            }

            // Performs the drawing
            using (SolidBrush foreground = new SolidBrush(foreColor))
            {
                float offset;
                int left = e.Bounds.Left + il.ImageSize.Width + 2;
                Size line1Size = TextRenderer.MeasureText(e.Node.Text, m_boldFont);

                if (!string.IsNullOrEmpty(line2))
                {
                    Size line2Size = TextRenderer.MeasureText(line2, Font);

                    offset = (float)(e.Bounds.Height - line1Size.Height - line2Size.Height) / 2;
                    e.Graphics.DrawString(e.Node.Text, m_boldFont, foreground, new PointF(left, e.Bounds.Top + offset));

                    using (SolidBrush lightForeground = new SolidBrush(lightForeColor))
                    {
                        e.Graphics.DrawString(line2, Font, lightForeground, new PointF(left, e.Bounds.Top + offset + line1Size.Height));
                    }
                }
                else
                {
                    offset = (float)(e.Bounds.Height - line1Size.Height) / 2;
                    e.Graphics.DrawString(e.Node.Text, m_boldFont, foreground, new PointF(left, e.Bounds.Top + offset));
                }
            }

            // Draws the icon for skill/cert on the far right
            if (supIcon == -1)
                return;

            int imgOfssetX = e.Bounds.Left;
            float imgOffsetY = Math.Max(0.0f, (e.Bounds.Height - il.ImageSize.Height) * 0.5f);
            e.Graphics.DrawImageUnscaled(il.Images[supIcon], imgOfssetX, (int)(e.Bounds.Top + imgOffsetY));
        }

        #endregion


        #region Context menus

        /// <summary>
        /// Context menu opening, we update the menus' statuses.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmListSkills_Opening(object sender, CancelEventArgs e)
        {
            TreeNode node = treeView.SelectedNode;
            Mastery masteryLevel = node?.Tag as Mastery;
            CertificateLevel certLevel = node?.Tag as CertificateLevel;

            planToLevel.Visible = planToLevelSeparator.Visible = m_plan != null && node != null;

            if (m_plan != null)
            {
                if (masteryLevel != null)
                {
                    // Update "add to" menu
                    planToLevel.Enabled = !m_plan.WillGrantEligibilityFor(masteryLevel);
                    planToLevel.Text = $"Plan \"{masteryLevel}\"";
                }
                // When a certificate is selected
                else if (certLevel != null)
                {
                    // Update "add to" menu
                    planToLevel.Enabled = !m_plan.WillGrantEligibilityFor(certLevel);
                    planToLevel.Text = $"Plan \"{certLevel.Certificate.Name}\"";
                }
                // When a skill is selected
                else if (node != null)
                {
                    // Update "add to" menu
                    var prereq = node.Tag as SkillLevel;
                    if (prereq != null)
                    {
                        Skill skill = prereq.Skill;
                        planToLevel.Enabled = skill.Level < prereq.Level && !m_plan.
                            IsPlanned(skill, prereq.Level);
                        planToLevel.Text = $"Plan \"{skill} {Skill.GetRomanFromInt(prereq.Level)}\"";
                    }
                }
            }
            // Update "show in skill browser" text
            showInBrowserMenu.Text = certLevel != null ? "Show in Certificate Browser" :
                "Show in Skill Browser";

            // Update "show in skill browser" menu
            showInBrowserMenu.Visible = (node != null && masteryLevel == null);

            // Update "show in skill explorer" menu
            showInExplorerMenu.Visible = (node != null && masteryLevel == null && certLevel == null);

            // Update "show in" menu
            showInMenuSeparator.Visible = (node != null && masteryLevel == null);

            // "Collapse" and "Expand" menus
            int subNodeCount = node?.GetNodeCount(true) ?? 0;
            tsmCollapseSelected.Visible = subNodeCount > 0 && node.IsExpanded;
            tsmExpandSelected.Visible = subNodeCount > 0 && !node.IsExpanded;

            tsmExpandSelected.Text = (subNodeCount > 0 && !node.IsExpanded) ?
                $"Expand \"{node.Text}\"" : string.Empty;
            tsmCollapseSelected.Text = (subNodeCount > 0 && node.IsExpanded) ?
                $"Collapse \"{node.Text}\"" : string.Empty;

            toggleSeparator.Visible = subNodeCount > 0;

            // "Expand All" and "Collapse All" menus
            tsmCollapseAll.Enabled = tsmCollapseAll.Visible = m_allExpanded;
            tsmExpandAll.Enabled = tsmExpandAll.Visible = !tsmCollapseAll.Enabled;
        }

        /// <summary>
        /// Treeview's context menu > Plan "(selection)".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmAddToPlan_Click(object sender, EventArgs e)
        {
            var masteryLevel = treeView.SelectedNode.Tag as Mastery;
            var certLevel = treeView.SelectedNode.Tag as CertificateLevel;
            IPlanOperation operation = null;

            if (masteryLevel != null)
                operation = m_plan.TryPlanTo(masteryLevel);
            else if (certLevel != null)
                operation = m_plan.TryPlanTo(certLevel);
            else
            {
                var prereq = treeView.SelectedNode.Tag as SkillLevel;
                if (prereq != null)
                    operation = m_plan.TryPlanTo(prereq.Skill, prereq.Level);
            }

            if (operation != null)
            {
                var planWindow = ParentForm as PlanWindow;
                if (planWindow != null)
                    PlanHelper.SelectPerform(new PlanToOperationWindow(operation), planWindow,
                        operation);
            }
        }

        /// <summary>
        /// Treeview's context menu > Expand.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmExpandSelected_Click(object sender, EventArgs e)
        {
            treeView.SelectedNode.Expand();
        }

        /// <summary>
        /// Treeview's context menu > Collapse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmCollapseSelected_Click(object sender, EventArgs e)
        {
            treeView.SelectedNode.Collapse();
        }

        /// <summary>
        /// Treeview's context menu > Expand All.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmExpandAll_Click(object sender, EventArgs e)
        {
            treeView.ExpandAll();
            m_allExpanded = true;
        }

        /// <summary>
        /// Treeview's context menu > Collapse All.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmCollapseAll_Click(object sender, EventArgs e)
        {
            treeView.CollapseAll();
            m_allExpanded = false;
        }

        /// <summary>
        /// Context menu > Show "skill" in browser | Show "certificate class" certificates.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showInBrowserMenu_Click(object sender, EventArgs e)
        {
            // Return when nothing is selected
            if (treeView.SelectedNode == null)
                return;

            var certLevel = treeView.SelectedNode.Tag as CertificateLevel;

            // When a certificate is selected, we select its class in the left tree
            if (certLevel != null)
            {
                // Open the certificate browser
                PlanWindow.ShowPlanWindow(m_character, m_plan).ShowCertificateInBrowser(certLevel);
            }
            // When a skill is selected, we select it in the skill browser
            else
            {
                var skill = (treeView.SelectedNode?.Tag as SkillLevel)?.Skill;

                // Open the skill browser
                if (skill != null)
                    PlanWindow.ShowPlanWindow(m_character, m_plan).ShowSkillInBrowser(skill);
            }
        }

        /// <summary>
        /// Context menu > Show "skill" in explorer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showInExplorerMenu_Click(object sender, EventArgs e)
        {
            var skill = (treeView.SelectedNode?.Tag as SkillLevel)?.Skill;

            // Open the skill explorer
            if (skill != null)
                SkillExplorerWindow.ShowSkillExplorerWindow(m_character, m_plan).
                    ShowSkillInExplorer(skill);
        }

        #endregion
        /// <summary>
        /// Reloads images from embedded resources.
        /// Loads untrained icons at indices 0-5, trained icons at indices 6-11.
        /// </summary>
        private void ReloadImageListFromResources(ImageList il, string pattern)
        {
            il.Images.Clear();
            var asm = typeof(EVEMon.Common.Properties.Resources).Assembly;

            // Load untrained icons (indices 0-5)
            for (int i = 0; i <= 5; i++)
            {
                string resourceName = $"EVEMon.Common.Resources.Images.{string.Format(pattern, i)}";
                using (var stream = asm.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        using (var img = Image.FromStream(stream))
                        {
                            il.Images.Add(new Bitmap(img));
                        }
                    }
                }
            }

            // Load trained icons (indices 6-11) - pattern with _1 suffix
            string trainedPattern = pattern.Replace(".png", "_1.png");
            for (int i = 0; i <= 5; i++)
            {
                string resourceName = $"EVEMon.Common.Resources.Images.{string.Format(trainedPattern, i)}";
                using (var stream = asm.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        using (var img = Image.FromStream(stream))
                        {
                            il.Images.Add(new Bitmap(img));
                        }
                    }
                }
            }
        }
    }
}
