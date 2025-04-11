namespace ShieldSec.Design
{
    partial class AnalysisForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnalysisForm));
            analysisTreeNodeView = new Krypton.Toolkit.KryptonTreeView();
            threatsLabel = new Krypton.Toolkit.KryptonLabel();
            SuspendLayout();
            // 
            // analysisTreeNodeView
            // 
            analysisTreeNodeView.ItemStyle = Krypton.Toolkit.ButtonStyle.BreadCrumb;
            analysisTreeNodeView.Location = new Point(12, 37);
            analysisTreeNodeView.Name = "analysisTreeNodeView";
            analysisTreeNodeView.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            analysisTreeNodeView.Size = new Size(301, 606);
            analysisTreeNodeView.Sorted = true;
            analysisTreeNodeView.StateCommon.Back.Color1 = Color.FromArgb(152, 64, 98);
            analysisTreeNodeView.StateCommon.Border.Color1 = Color.Black;
            analysisTreeNodeView.StateCommon.Border.Color2 = Color.FromArgb(64, 64, 64);
            analysisTreeNodeView.StateCommon.Border.ColorAngle = 160F;
            analysisTreeNodeView.TabIndex = 0;
            // 
            // threatsLabel
            // 
            threatsLabel.Location = new Point(12, 8);
            threatsLabel.Name = "threatsLabel";
            threatsLabel.Size = new Size(170, 23);
            threatsLabel.StateNormal.ShortText.Color1 = Color.White;
            threatsLabel.StateNormal.ShortText.Color2 = Color.White;
            threatsLabel.StateNormal.ShortText.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            threatsLabel.TabIndex = 1;
            threatsLabel.Values.Text = "Threat Analysis Tree";
            // 
            // AnalysisForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(65, 67, 106);
            ClientSize = new Size(1012, 649);
            Controls.Add(threatsLabel);
            Controls.Add(analysisTreeNodeView);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AnalysisForm";
            Text = "Malware Analysis";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Krypton.Toolkit.KryptonTreeView analysisTreeNodeView;
        private Krypton.Toolkit.KryptonLabel threatsLabel;
    }
}