namespace ShieldSec.Design
{
    partial class ToastNotificationForm
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
            lbTitle = new Krypton.Toolkit.KryptonLabel();
            notiPictureBox = new Krypton.Toolkit.KryptonPictureBox();
            kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            btnQuarantine = new Krypton.Toolkit.KryptonButton();
            btnIgnoreAlert = new Krypton.Toolkit.KryptonButton();
            lbLocation = new Krypton.Toolkit.KryptonLabel();
            placeholderLabelLocation = new Krypton.Toolkit.KryptonLabel();
            placeholderLabel = new Krypton.Toolkit.KryptonLabel();
            lbDetected = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)notiPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).BeginInit();
            kryptonPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // lbTitle
            // 
            lbTitle.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            lbTitle.Location = new Point(51, 12);
            lbTitle.Name = "lbTitle";
            lbTitle.Size = new Size(138, 26);
            lbTitle.StateCommon.ShortText.Color1 = Color.White;
            lbTitle.StateCommon.ShortText.Color2 = Color.White;
            lbTitle.StateCommon.ShortText.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbTitle.TabIndex = 0;
            lbTitle.Values.Text = "promptMessage";
            // 
            // notiPictureBox
            // 
            notiPictureBox.Image = Properties.Resources.ShieldSec;
            notiPictureBox.InitialImage = Properties.Resources.ShieldSec;
            notiPictureBox.Location = new Point(12, 12);
            notiPictureBox.Name = "notiPictureBox";
            notiPictureBox.Size = new Size(33, 31);
            notiPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            notiPictureBox.TabIndex = 1;
            notiPictureBox.TabStop = false;
            // 
            // kryptonPanel1
            // 
            kryptonPanel1.Controls.Add(btnQuarantine);
            kryptonPanel1.Controls.Add(btnIgnoreAlert);
            kryptonPanel1.Controls.Add(lbLocation);
            kryptonPanel1.Controls.Add(placeholderLabelLocation);
            kryptonPanel1.Controls.Add(placeholderLabel);
            kryptonPanel1.Controls.Add(lbDetected);
            kryptonPanel1.Location = new Point(0, 49);
            kryptonPanel1.Name = "kryptonPanel1";
            kryptonPanel1.Size = new Size(324, 207);
            kryptonPanel1.StateCommon.Color1 = Color.FromArgb(59, 60, 96);
            kryptonPanel1.StateCommon.Color2 = Color.FromArgb(59, 60, 96);
            kryptonPanel1.TabIndex = 2;
            // 
            // btnQuarantine
            // 
            btnQuarantine.Location = new Point(85, 127);
            btnQuarantine.Name = "btnQuarantine";
            btnQuarantine.OverrideDefault.Back.Color1 = Color.FromArgb(246, 70, 104);
            btnQuarantine.OverrideDefault.Back.Color2 = Color.FromArgb(246, 70, 104);
            btnQuarantine.OverrideDefault.Back.ColorAngle = 45F;
            btnQuarantine.OverrideDefault.Back.GraphicsHint = Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            btnQuarantine.OverrideDefault.Border.Color1 = Color.FromArgb(59, 60, 96);
            btnQuarantine.OverrideDefault.Border.Color2 = Color.FromArgb(59, 60, 96);
            btnQuarantine.OverrideDefault.Border.Rounding = 16F;
            btnQuarantine.OverrideDefault.Border.Width = 1;
            btnQuarantine.OverrideDefault.Content.ShortText.Color1 = Color.White;
            btnQuarantine.OverrideDefault.Content.ShortText.Color2 = Color.White;
            btnQuarantine.OverrideDefault.Content.ShortText.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnQuarantine.Size = new Size(136, 42);
            btnQuarantine.StateCommon.Back.Color1 = Color.FromArgb(246, 70, 104);
            btnQuarantine.StateCommon.Back.Color2 = Color.FromArgb(246, 70, 104);
            btnQuarantine.StateCommon.Back.ColorAngle = 45F;
            btnQuarantine.StateCommon.Back.GraphicsHint = Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            btnQuarantine.StateCommon.Border.Color1 = Color.FromArgb(59, 60, 96);
            btnQuarantine.StateCommon.Border.Color2 = Color.FromArgb(59, 60, 96);
            btnQuarantine.StateCommon.Border.Rounding = 16F;
            btnQuarantine.StateCommon.Border.Width = 1;
            btnQuarantine.StateCommon.Content.ShortText.Color1 = Color.White;
            btnQuarantine.StateCommon.Content.ShortText.Color2 = Color.White;
            btnQuarantine.StateCommon.Content.ShortText.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnQuarantine.StateNormal.Back.Color1 = Color.FromArgb(246, 70, 104);
            btnQuarantine.StateNormal.Back.Color2 = Color.FromArgb(246, 70, 104);
            btnQuarantine.StateNormal.Border.Color1 = Color.FromArgb(59, 60, 96);
            btnQuarantine.StateNormal.Border.Color2 = Color.FromArgb(59, 60, 96);
            btnQuarantine.StateNormal.Border.Rounding = 16F;
            btnQuarantine.StateNormal.Border.Width = 1;
            btnQuarantine.StateNormal.Content.ShortText.Color1 = Color.White;
            btnQuarantine.StateNormal.Content.ShortText.Color2 = Color.White;
            btnQuarantine.StateNormal.Content.ShortText.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnQuarantine.StatePressed.Back.Color1 = Color.FromArgb(236, 55, 100);
            btnQuarantine.StatePressed.Back.Color2 = Color.FromArgb(236, 55, 100);
            btnQuarantine.StatePressed.Back.GraphicsHint = Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            btnQuarantine.StatePressed.Border.Color1 = Color.FromArgb(59, 60, 96);
            btnQuarantine.StatePressed.Border.Color2 = Color.FromArgb(59, 60, 96);
            btnQuarantine.StatePressed.Content.ShortText.Color1 = Color.White;
            btnQuarantine.StatePressed.Content.ShortText.Color2 = Color.White;
            btnQuarantine.StatePressed.Content.ShortText.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnQuarantine.TabIndex = 5;
            btnQuarantine.ToolTipValues.Description = "Ignores the alert.";
            btnQuarantine.ToolTipValues.EnableToolTips = true;
            btnQuarantine.Values.DropDownArrowColor = Color.Empty;
            btnQuarantine.Values.Text = "Quarantine";
            btnQuarantine.Click += btnQuarantine_Click;
            // 
            // btnIgnoreAlert
            // 
            btnIgnoreAlert.Location = new Point(85, 79);
            btnIgnoreAlert.Name = "btnIgnoreAlert";
            btnIgnoreAlert.OverrideDefault.Back.Color1 = Color.FromArgb(246, 70, 104);
            btnIgnoreAlert.OverrideDefault.Back.Color2 = Color.FromArgb(246, 70, 104);
            btnIgnoreAlert.OverrideDefault.Back.ColorAngle = 45F;
            btnIgnoreAlert.OverrideDefault.Back.GraphicsHint = Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            btnIgnoreAlert.OverrideDefault.Border.Color1 = Color.FromArgb(59, 60, 96);
            btnIgnoreAlert.OverrideDefault.Border.Color2 = Color.FromArgb(59, 60, 96);
            btnIgnoreAlert.OverrideDefault.Border.Rounding = 16F;
            btnIgnoreAlert.OverrideDefault.Border.Width = 1;
            btnIgnoreAlert.OverrideDefault.Content.ShortText.Color1 = Color.White;
            btnIgnoreAlert.OverrideDefault.Content.ShortText.Color2 = Color.White;
            btnIgnoreAlert.OverrideDefault.Content.ShortText.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnIgnoreAlert.Size = new Size(136, 42);
            btnIgnoreAlert.StateCommon.Back.Color1 = Color.FromArgb(246, 70, 104);
            btnIgnoreAlert.StateCommon.Back.Color2 = Color.FromArgb(246, 70, 104);
            btnIgnoreAlert.StateCommon.Back.ColorAngle = 45F;
            btnIgnoreAlert.StateCommon.Back.GraphicsHint = Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            btnIgnoreAlert.StateCommon.Border.Color1 = Color.FromArgb(59, 60, 96);
            btnIgnoreAlert.StateCommon.Border.Color2 = Color.FromArgb(59, 60, 96);
            btnIgnoreAlert.StateCommon.Border.Rounding = 16F;
            btnIgnoreAlert.StateCommon.Border.Width = 1;
            btnIgnoreAlert.StateCommon.Content.ShortText.Color1 = Color.White;
            btnIgnoreAlert.StateCommon.Content.ShortText.Color2 = Color.White;
            btnIgnoreAlert.StateCommon.Content.ShortText.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnIgnoreAlert.StateNormal.Back.Color1 = Color.FromArgb(246, 70, 104);
            btnIgnoreAlert.StateNormal.Back.Color2 = Color.FromArgb(246, 70, 104);
            btnIgnoreAlert.StateNormal.Border.Color1 = Color.FromArgb(59, 60, 96);
            btnIgnoreAlert.StateNormal.Border.Color2 = Color.FromArgb(59, 60, 96);
            btnIgnoreAlert.StateNormal.Border.Rounding = 16F;
            btnIgnoreAlert.StateNormal.Border.Width = 1;
            btnIgnoreAlert.StateNormal.Content.ShortText.Color1 = Color.White;
            btnIgnoreAlert.StateNormal.Content.ShortText.Color2 = Color.White;
            btnIgnoreAlert.StateNormal.Content.ShortText.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnIgnoreAlert.StatePressed.Back.Color1 = Color.FromArgb(236, 55, 100);
            btnIgnoreAlert.StatePressed.Back.Color2 = Color.FromArgb(236, 55, 100);
            btnIgnoreAlert.StatePressed.Back.GraphicsHint = Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            btnIgnoreAlert.StatePressed.Border.Color1 = Color.FromArgb(59, 60, 96);
            btnIgnoreAlert.StatePressed.Border.Color2 = Color.FromArgb(59, 60, 96);
            btnIgnoreAlert.StatePressed.Content.ShortText.Color1 = Color.White;
            btnIgnoreAlert.StatePressed.Content.ShortText.Color2 = Color.White;
            btnIgnoreAlert.StatePressed.Content.ShortText.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnIgnoreAlert.TabIndex = 4;
            btnIgnoreAlert.ToolTipValues.Description = "Ignores the alert.";
            btnIgnoreAlert.ToolTipValues.EnableToolTips = true;
            btnIgnoreAlert.Values.DropDownArrowColor = Color.Empty;
            btnIgnoreAlert.Values.Text = "Ignore";
            btnIgnoreAlert.Click += btnIgnoreAlert_Click;
            // 
            // lbLocation
            // 
            lbLocation.Location = new Point(74, 37);
            lbLocation.Name = "lbLocation";
            lbLocation.Size = new Size(22, 21);
            lbLocation.StateCommon.ShortText.Color1 = Color.White;
            lbLocation.StateCommon.ShortText.Color2 = Color.White;
            lbLocation.StateCommon.ShortText.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbLocation.TabIndex = 3;
            lbLocation.Values.Text = "pl";
            // 
            // placeholderLabelLocation
            // 
            placeholderLabelLocation.Location = new Point(12, 37);
            placeholderLabelLocation.Name = "placeholderLabelLocation";
            placeholderLabelLocation.Size = new Size(64, 21);
            placeholderLabelLocation.StateCommon.ShortText.Color1 = Color.White;
            placeholderLabelLocation.StateCommon.ShortText.Color2 = Color.White;
            placeholderLabelLocation.StateCommon.ShortText.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            placeholderLabelLocation.TabIndex = 2;
            placeholderLabelLocation.Values.Text = "Location:";
            // 
            // placeholderLabel
            // 
            placeholderLabel.Location = new Point(12, 19);
            placeholderLabel.Name = "placeholderLabel";
            placeholderLabel.Size = new Size(67, 21);
            placeholderLabel.StateCommon.ShortText.Color1 = Color.White;
            placeholderLabel.StateCommon.ShortText.Color2 = Color.White;
            placeholderLabel.StateCommon.ShortText.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            placeholderLabel.TabIndex = 1;
            placeholderLabel.Values.Text = "Detected:";
            // 
            // lbDetected
            // 
            lbDetected.Location = new Point(74, 19);
            lbDetected.Name = "lbDetected";
            lbDetected.Size = new Size(22, 21);
            lbDetected.StateCommon.ShortText.Color1 = Color.White;
            lbDetected.StateCommon.ShortText.Color2 = Color.White;
            lbDetected.StateCommon.ShortText.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbDetected.TabIndex = 0;
            lbDetected.Values.Text = "pl";
            // 
            // ToastNotificationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(65, 67, 106);
            ClientSize = new Size(308, 241);
            Controls.Add(kryptonPanel1);
            Controls.Add(notiPictureBox);
            Controls.Add(lbTitle);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ToastNotificationForm";
            StateCommon.Back.Color1 = Color.FromArgb(152, 64, 98);
            StateCommon.Back.Color2 = Color.FromArgb(152, 64, 98);
            StateCommon.Header.Back.Color1 = Color.FromArgb(152, 64, 98);
            StateCommon.Header.Back.Color2 = Color.FromArgb(152, 64, 98);
            ((System.ComponentModel.ISupportInitialize)notiPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).EndInit();
            kryptonPanel1.ResumeLayout(false);
            kryptonPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Krypton.Toolkit.KryptonLabel lbTitle;
        private Krypton.Toolkit.KryptonPictureBox notiPictureBox;
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonLabel placeholderLabel;
        private Krypton.Toolkit.KryptonLabel lbDetected;
        private Krypton.Toolkit.KryptonButton btnIgnoreAlert;
        private Krypton.Toolkit.KryptonLabel lbLocation;
        private Krypton.Toolkit.KryptonLabel placeholderLabelLocation;
        private Krypton.Toolkit.KryptonButton btnQuarantine;
    }
}