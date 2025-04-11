namespace ShieldSec.Design
{
    partial class GenericNotificationForm
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
            lbInformation = new Krypton.Toolkit.KryptonLabel();
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
            kryptonPanel1.Controls.Add(lbInformation);
            kryptonPanel1.Location = new Point(0, 49);
            kryptonPanel1.Name = "kryptonPanel1";
            kryptonPanel1.Size = new Size(309, 165);
            kryptonPanel1.StateCommon.Color1 = Color.FromArgb(59, 60, 96);
            kryptonPanel1.StateCommon.Color2 = Color.FromArgb(59, 60, 96);
            kryptonPanel1.TabIndex = 2;
            // 
            // lbInformation
            // 
            lbInformation.Location = new Point(12, 10);
            lbInformation.Name = "lbInformation";
            lbInformation.Size = new Size(22, 21);
            lbInformation.StateCommon.ShortText.Color1 = Color.White;
            lbInformation.StateCommon.ShortText.Color2 = Color.White;
            lbInformation.StateCommon.ShortText.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbInformation.TabIndex = 0;
            lbInformation.Values.Text = "pl";
            // 
            // GenericNotificationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(65, 67, 106);
            ClientSize = new Size(308, 162);
            Controls.Add(kryptonPanel1);
            Controls.Add(notiPictureBox);
            Controls.Add(lbTitle);
            FormBorderStyle = FormBorderStyle.None;
            Name = "GenericNotificationForm";
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
        private Krypton.Toolkit.KryptonLabel lbInformation;
    }
}