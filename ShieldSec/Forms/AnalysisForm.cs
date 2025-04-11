using Krypton.Toolkit;
using ShieldSec.Core.Analysis.PostProcessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShieldSec.Design
{
    /// <summary>
    ///  This form is used to display the analysis results of potentially infected files.
    /// </summary>
    public partial class AnalysisForm : KryptonForm
    {
        public KryptonTreeView analysisTreeNodeViewInstance;
        /// <summary>
        ///  Constructor for the AnalysisForm.
        /// </summary>
        public AnalysisForm()
        {
            InitializeComponent();
            analysisTreeNodeViewInstance = analysisTreeNodeView;
            FormClosed += OnFormClosed;
        }
        /// <summary>
        ///  Clears the TreeView of all nodes.
        /// </summary>
        public void ClearTree()
        {
            analysisTreeNodeView.BeginUpdate();
            analysisTreeNodeView.Nodes.Clear();
            analysisTreeNodeView.EndUpdate();
            analysisTreeNodeView.Refresh(); 
        }
        /// <summary>
        ///  Clears & disposes of the TreeView when the form is closed.
        /// </summary>
        /// <param name="sender"> The object that triggered the event. </param>
        /// <param name="e"> The event arguments. </param>
        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            ClearTree();
            Dispose();
        }
        /// <summary>
        /// Populates the TreeView with analysis for each potentially infected file.
        /// </summary>
        /// <param name="infectedFilePaths">A collection of file paths flagged as potentially infected.</param>
        public void PopulateTree(IEnumerable<string> infectedFilePaths)
        {
            analysisTreeNodeView.BeginUpdate();
            try
            {
                ClearTree();
                TreeNode rootNode = new TreeNode("Potentially Infected Files");

                foreach (var filePath in infectedFilePaths)
                {
                    var fileNode = StaticAnalysis.AnalyzePEFileToTreeNode(filePath);
                    ProcessThreatScores(fileNode);
                    rootNode.Nodes.Add(fileNode);
                }

                analysisTreeNodeView.Nodes.Add(rootNode);
                rootNode.Expand(); 
            }
            finally
            {
                analysisTreeNodeView.EndUpdate(); 
            }
        }
        /// <summary>
        ///  Proccesses the threat scores of each file node.
        /// </summary>
        /// <param name="fileNode"> The file node to process. </param>
        private void ProcessThreatScores(TreeNode fileNode)
        {
            foreach (TreeNode node in fileNode.Nodes)
            {
                if (node.Text.StartsWith("Threat Score:"))
                {
                    // extract it from the node text, probs another way to do this
                    var match = Regex.Match(node.Text, @"\d+");
                    if (match.Success && int.TryParse(match.Value, out int score))
                    {
                        node.ForeColor = score switch
                        {
                            < 30 => Color.LimeGreen,
                            < 60 => Color.Orange,
                            _ => Color.Red
                        };
                        node.NodeFont = new Font(analysisTreeNodeView.Font, FontStyle.Bold);
                    }
                }
            }
        }
    }
}
