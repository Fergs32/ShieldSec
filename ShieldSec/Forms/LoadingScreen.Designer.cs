namespace ShieldSec.Design
{
    partial class LoadingScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadingScreen));
            lbTextProgression = new Krypton.Toolkit.KryptonLabel();
            SuspendLayout();
            // 
            // lbTextProgression
            // 
            lbTextProgression.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            lbTextProgression.Location = new Point(12, 171);
            lbTextProgression.Name = "lbTextProgression";
            lbTextProgression.Size = new Size(113, 26);
            lbTextProgression.StateCommon.ShortText.Color1 = Color.White;
            lbTextProgression.StateCommon.ShortText.Color2 = Color.White;
            lbTextProgression.StateCommon.ShortText.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbTextProgression.TabIndex = 0;
            lbTextProgression.Values.Text = "ProgressLabel";
            // 
            // LoadingScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(65, 67, 106);
            ClientSize = new Size(316, 203);
            Controls.Add(lbTextProgression);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "LoadingScreen";
            StateCommon.Header.Border.Rounding = 16F;
            StateCommon.Header.Border.Width = 1;
            Text = "Loading...";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Krypton.Toolkit.KryptonLabel lbTextProgression;
    }
}