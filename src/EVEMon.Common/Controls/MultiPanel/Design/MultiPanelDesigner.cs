using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace EVEMon.Common.Controls.MultiPanel.Design
{
    /// <summary>
    /// A designer hosting a <see cref="MultiPanel"/>.
    /// </summary>
    /// <remarks>
    /// Based on the work from Liron Levi on Code Project, under public domain. 
    /// See http://www.codeproject.com/KB/cs/multipanelcontrol.aspx
    /// </remarks>
    public class MultiPanelDesigner : ParentControlDesigner
    {
        private MultiPanel m_panel;

        /// <summary>
        /// Overridden. Gets the collection of verbs that are available to this designer.
        /// </summary>
        public override DesignerVerbCollection Verbs
        {
            get
            {
                IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
                return MultiPanelDesignerHelper.GetDesignerVerbs(host, m_panel);
            }
        }

        /// <summary>
        /// Overriden. Gets the collection of components associated with the component managed by the designer.
        /// </summary>
        public override ICollection AssociatedComponents => m_panel.Controls;

        /// <summary>
        /// Initializes the components
        /// </summary>
        /// <param name="component"></param>
        public override void Initialize(IComponent component)
        {
            // Should not happen
            m_panel = component as MultiPanel;
            if (m_panel == null)
            {
                DisplayError(
                    new ArgumentException(
                        "Tried to use the MultiPanelControlDesign with a class that does not inherit from MultiPanel.",
                        "component"));
                return;
            }

            m_panel.SelectionChange += OnPanelSelectionChange;
            base.Initialize(component);

            // Subscribe events 
            IComponentChangeService componentChangeService =
                (IComponentChangeService)GetService(typeof(IComponentChangeService));
            if (componentChangeService != null)
                componentChangeService.ComponentRemoved += OnComponentRemoved;

            ISelectionService selectionService = (ISelectionService)GetService(typeof(ISelectionService));
            if (selectionService != null)
                selectionService.SelectionChanged += OnServiceSelectionChanged;
        }

        /// <summary>
        /// Overridden. Inherited from <see cref="IDesigner.DoDefaultAction()"/>.
        /// </summary>
        public override void DoDefaultAction()
        {
        }

        /// <summary>
        /// Prevents the user to add anything else than MultiPanelPages to this control.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public override bool CanParent(Control control) => control is MultiPanelPage && !Control.Contains(control);


        #region Private Methods

        /// <summary>
        /// Occurs when the service's selection changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnServiceSelectionChanged(object sender, EventArgs e)
        {
            ISelectionService service = (ISelectionService)GetService(typeof(ISelectionService));

            if (service?.PrimarySelection == null)
                return;

            MultiPanelPage page = GetMultiPanelPage((Control)service.PrimarySelection);

            if (page != null)
                m_panel.SelectedPage = page;
        }

        /// <summary>
        /// When the panel's selection change, notify to the designer the property changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnPanelSelectionChange(object sender, MultiPanelSelectionChangeEventArgs args)
        {
            RaiseComponentChanging(TypeDescriptor.GetProperties(Control)["SelectedPage"]);
            RaiseComponentChanged(TypeDescriptor.GetProperties(Control)["SelectedPage"], args.OldPage, args.NewPage);
        }

        /// <summary>
        /// Occurs when a component has been removed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="cea"></param>
        private static void OnComponentRemoved(object sender, ComponentEventArgs cea)
        {
        }

        /// <summary>
        /// Return the page hosting the provided control, or the control itself it it is a page. If the matching page is not bound to this represented panel, returns null.
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        private MultiPanelPage GetMultiPanelPage(Control ctrl)
        {
            while (true)
            {
                // If the control is a page, return it if the parent is the represented panel, null otherwise.
                MultiPanelPage page = ctrl as MultiPanelPage;

                if (page != null)
                    return ReferenceEquals(m_panel, page.Parent) ? page : null;

                // If the control has a parent, browser its ancestry
                if (ctrl.Parent == null)
                    return null;

                ctrl = ctrl.Parent;
            }
        }

        #endregion
    }
}
