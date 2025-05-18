using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WebStepper.Core.Domain;
using WebStepper.Core.Interfaces;

namespace WebStepper.UI.Controls
{
    public partial class StepTreeControl : UserControl
    {
        private Template _currentTemplate;
        private TreeNode _highlightedNode;

        // Events
        public event EventHandler<StepEventArgs> StepSelected;
        public event EventHandler<TemplateChangedEventArgs> TemplateChanged;

        public StepTreeControl()
        {
            InitializeComponent();

            // Configure the tree view
            treeView.ImageList = new ImageList();
            treeView.ImageList.Images.Add("template", SystemIcons.Application.ToBitmap());
            treeView.ImageList.Images.Add("page", SystemIcons.Information.ToBitmap());
            treeView.ImageList.Images.Add("step", SystemIcons.WinLogo.ToBitmap());

            // Set up events
            treeView.AfterSelect += OnTreeViewAfterSelect;
            treeView.NodeMouseDoubleClick += OnTreeViewNodeMouseDoubleClick;
        }

        public void SetTemplate(Template template)
        {
            _currentTemplate = template;
            RefreshTreeView();
        }

        public void HighlightStep(string stepId)
        {
            if (string.IsNullOrEmpty(stepId) || _currentTemplate == null)
            {
                return;
            }

            // Remove highlight from the previously highlighted node
            if (_highlightedNode != null)
            {
                _highlightedNode.BackColor = Color.White;
                _highlightedNode.ForeColor = Color.Black;
            }

            // Find the node for the step
            TreeNode stepNode = FindStepNode(stepId);

            if (stepNode != null)
            {
                // Highlight the node
                stepNode.BackColor = Color.Blue;
                stepNode.ForeColor = Color.White;
                _highlightedNode = stepNode;

                // Ensure the node is visible
                stepNode.EnsureVisible();

                // Select the node
                treeView.SelectedNode = stepNode;
            }
        }

        public void ExpandPage(string pageId)
        {
            if (string.IsNullOrEmpty(pageId) || _currentTemplate == null)
            {
                return;
            }

            // Find the node for the page
            foreach (TreeNode templateNode in treeView.Nodes)
            {
                foreach (TreeNode pageNode in templateNode.Nodes)
                {
                    if (pageNode.Tag is Page page && page.Id == pageId)
                    {
                        // Expand the page node
                        templateNode.Expand();
                        pageNode.Expand();
                        return;
                    }
                }
            }
        }

        private void RefreshTreeView()
        {
            treeView.Nodes.Clear();

            if (_currentTemplate == null)
            {
                return;
            }

            // Create template node
            TreeNode templateNode = new TreeNode(_currentTemplate.Name, 0, 0);
            templateNode.Tag = _currentTemplate;

            // Add pages
            foreach (var page in _currentTemplate.Pages)
            {
                TreeNode pageNode = new TreeNode(page.Name, 1, 1);
                pageNode.Tag = page;

                // Add steps
                foreach (var step in page.Steps)
                {
                    TreeNode stepNode = new TreeNode(step.Name, 2, 2);
                    stepNode.Tag = step;
                    pageNode.Nodes.Add(stepNode);
                }

                templateNode.Nodes.Add(pageNode);
            }

            treeView.Nodes.Add(templateNode);
            templateNode.Expand();
        }

        private TreeNode FindStepNode(string stepId)
        {
            if (string.IsNullOrEmpty(stepId) || treeView.Nodes.Count == 0)
            {
                return null;
            }

            // Search through all template nodes
            foreach (TreeNode templateNode in treeView.Nodes)
            {
                // Search through all page nodes
                foreach (TreeNode pageNode in templateNode.Nodes)
                {
                    // Search through all step nodes
                    foreach (TreeNode stepNode in pageNode.Nodes)
                    {
                        if (stepNode.Tag is Step step && step.Id == stepId)
                        {
                            return stepNode;
                        }
                    }
                }
            }

            return null;
        }

        public bool AddStep(Step step)
        {
            if (step == null || _currentTemplate == null)
            {
                return false;
            }

            // Get the currently selected node
            TreeNode selectedNode = treeView.SelectedNode;
            if (selectedNode == null)
            {
                // If no node is selected, add to the first page
                if (_currentTemplate.Pages.Count > 0)
                {
                    Page firstPage = _currentTemplate.Pages[0];
                    firstPage.Steps.Add(step);

                    // Find the page node and add the step
                    if (treeView.Nodes.Count > 0)
                    {
                        TreeNode templateNode = treeView.Nodes[0];
                        if (templateNode.Nodes.Count > 0)
                        {
                            TreeNode pageNode = templateNode.Nodes[0];

                            // Add step node
                            TreeNode stepNode = new TreeNode(step.Name, 2, 2);
                            stepNode.Tag = step;
                            pageNode.Nodes.Add(stepNode);

                            // Notify that the template has changed
                            NotifyTemplateChanged();
                            return true;
                        }
                    }
                }
                return false;
            }

            // Handle different node types
            if (selectedNode.Tag is Template)
            {
                // Template node - add to the first page
                if (_currentTemplate.Pages.Count > 0)
                {
                    Page firstPage = _currentTemplate.Pages[0];
                    firstPage.Steps.Add(step);

                    // Find the first page node
                    if (selectedNode.Nodes.Count > 0)
                    {
                        TreeNode pageNode = selectedNode.Nodes[0];

                        // Add step node
                        TreeNode stepNode = new TreeNode(step.Name, 2, 2);
                        stepNode.Tag = step;
                        pageNode.Nodes.Add(stepNode);

                        // Notify that the template has changed
                        NotifyTemplateChanged();
                        return true;
                    }
                }
            }
            else if (selectedNode.Tag is Page page)
            {
                // Page node - add to this page
                page.Steps.Add(step);

                // Add step node
                TreeNode stepNode = new TreeNode(step.Name, 2, 2);
                stepNode.Tag = step;
                selectedNode.Nodes.Add(stepNode);

                // Notify that the template has changed
                NotifyTemplateChanged();
                return true;
            }
            else if (selectedNode.Tag is Step)
            {
                // Step node - add after this step
                TreeNode pageNode = selectedNode.Parent;
                if (pageNode != null && pageNode.Tag is Page parentPage)
                {
                    // Find the index of the selected step
                    int index = -1;
                    for (int i = 0; i < parentPage.Steps.Count; i++)
                    {
                        if (parentPage.Steps[i] == selectedNode.Tag)
                        {
                            index = i;
                            break;
                        }
                    }

                    if (index >= 0)
                    {
                        // Insert the step after the selected one
                        parentPage.Steps.Insert(index + 1, step);

                        // Insert the step node after the selected one
                        TreeNode stepNode = new TreeNode(step.Name, 2, 2);
                        stepNode.Tag = step;
                        pageNode.Nodes.Insert(pageNode.Nodes.IndexOf(selectedNode) + 1, stepNode);

                        // Notify that the template has changed
                        NotifyTemplateChanged();
                        return true;
                    }
                }
            }

            return false;
        }

        private void NotifyTemplateChanged()
        {
            if (_currentTemplate != null)
            {
                TemplateChanged?.Invoke(this, new TemplateChangedEventArgs { Template = _currentTemplate });
            }
        }

        private void OnTreeViewAfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null || e.Node.Tag == null)
            {
                return;
            }

            // If a step node is selected
            if (e.Node.Tag is Step step)
            {
                // Find the parent page
                TreeNode pageNode = e.Node.Parent;
                if (pageNode != null && pageNode.Tag is Page page)
                {
                    // Raise the step selected event
                    StepSelected?.Invoke(this, new StepEventArgs { Page = page, Step = step });
                }
            }
        }

        private void OnTreeViewNodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == null || e.Node.Tag == null)
            {
                return;
            }

            // If a step node is double-clicked
            if (e.Node.Tag is Step step)
            {
                // Find the parent page
                TreeNode pageNode = e.Node.Parent;
                if (pageNode != null && pageNode.Tag is Page page)
                {
                    // Raise the step selected event
                    StepSelected?.Invoke(this, new StepEventArgs { Page = page, Step = step });
                }
            }
        }
    }

    public class StepEventArgs : EventArgs
    {
        public Page Page { get; set; }
        public Step Step { get; set; }
    }

    public class TemplateChangedEventArgs : EventArgs
    {
        public Template Template { get; set; }
    }
}
