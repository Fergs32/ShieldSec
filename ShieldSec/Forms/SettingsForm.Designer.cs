namespace ShieldSec.Design
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            tbThreads = new Krypton.Toolkit.KryptonTrackBar();
            kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            lbThreadCount = new Krypton.Toolkit.KryptonLabel();
            kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            rbScheduledScans = new Krypton.Toolkit.KryptonRadioButton();
            dtpScheduledScans = new Krypton.Toolkit.KryptonDateTimePicker();
            SuspendLayout();
            // 
            // tbThreads
            // 
            tbThreads.BackStyle = Krypton.Toolkit.PaletteBackStyle.HeaderForm;
            tbThreads.DrawBackground = false;
            tbThreads.Location = new Point(31, 55);
            tbThreads.Maximum = 20;
            tbThreads.Name = "tbThreads";
            tbThreads.Size = new Size(256, 27);
            tbThreads.StateCommon.Tick.Color1 = Color.White;
            tbThreads.StateCommon.Tick.Color2 = Color.Empty;
            tbThreads.StateCommon.Tick.Color3 = Color.Empty;
            tbThreads.StateCommon.Tick.Color4 = Color.Empty;
            tbThreads.StateCommon.Tick.Color5 = Color.Empty;
            tbThreads.StateCommon.Track.Color1 = Color.White;
            tbThreads.StateCommon.Track.Color2 = Color.Empty;
            tbThreads.StateCommon.Track.Color3 = Color.Empty;
            tbThreads.StateCommon.Track.Color4 = Color.Empty;
            tbThreads.StateCommon.Track.Color5 = Color.Empty;
            tbThreads.StatePressed.Position.Color1 = Color.White;
            tbThreads.StatePressed.Position.Color2 = Color.Empty;
            tbThreads.StatePressed.Position.Color3 = Color.Empty;
            tbThreads.StatePressed.Position.Color4 = Color.Empty;
            tbThreads.StatePressed.Position.Color5 = Color.Empty;
            tbThreads.TabIndex = 0;
            tbThreads.ValueChanged += kryptonTrackBar1_ValueChanged;
            // 
            // kryptonLabel1
            // 
            kryptonLabel1.Location = new Point(31, 25);
            kryptonLabel1.Name = "kryptonLabel1";
            kryptonLabel1.Size = new Size(109, 24);
            kryptonLabel1.StateNormal.ShortText.Color1 = Color.White;
            kryptonLabel1.StateNormal.ShortText.Color2 = Color.White;
            kryptonLabel1.StateNormal.ShortText.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            kryptonLabel1.TabIndex = 1;
            kryptonLabel1.Values.Text = "Max Threads:";
            // 
            // lbThreadCount
            // 
            lbThreadCount.Location = new Point(131, 25);
            lbThreadCount.Name = "lbThreadCount";
            lbThreadCount.Size = new Size(19, 24);
            lbThreadCount.StateNormal.ShortText.Color1 = Color.White;
            lbThreadCount.StateNormal.ShortText.Color2 = Color.White;
            lbThreadCount.StateNormal.ShortText.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbThreadCount.TabIndex = 2;
            lbThreadCount.Values.Text = "c";
            // 
            // kryptonLabel2
            // 
            kryptonLabel2.Location = new Point(64, 78);
            kryptonLabel2.Name = "kryptonLabel2";
            kryptonLabel2.Size = new Size(182, 20);
            kryptonLabel2.StateCommon.ShortText.Color1 = Color.Black;
            kryptonLabel2.StateCommon.ShortText.Color2 = Color.Black;
            kryptonLabel2.TabIndex = 3;
            kryptonLabel2.Values.Text = "Hint: Drag to increase/decrease";
            // 
            // rbScheduledScans
            // 
            rbScheduledScans.Location = new Point(31, 127);
            rbScheduledScans.Name = "rbScheduledScans";
            rbScheduledScans.Size = new Size(152, 26);
            rbScheduledScans.StateCommon.ShortText.Color1 = Color.White;
            rbScheduledScans.StateCommon.ShortText.Color2 = Color.White;
            rbScheduledScans.StateCommon.ShortText.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rbScheduledScans.TabIndex = 4;
            rbScheduledScans.Values.Text = "Scheduled Scans";
            rbScheduledScans.CheckedChanged += rbScheduledScans_CheckedChanged;
            // 
            // dtpScheduledScans
            // 
            dtpScheduledScans.CalendarHeaderStyle = Krypton.Toolkit.HeaderStyle.Form;
            dtpScheduledScans.Location = new Point(47, 159);
            dtpScheduledScans.MinDate = new DateTime(2025, 3, 27, 0, 0, 0, 0);
            dtpScheduledScans.Name = "dtpScheduledScans";
            dtpScheduledScans.Size = new Size(190, 21);
            dtpScheduledScans.StateCommon.Back.Color1 = Color.White;
            dtpScheduledScans.StateCommon.Border.Color1 = Color.Black;
            dtpScheduledScans.StateCommon.Border.Color2 = Color.Black;
            dtpScheduledScans.StateNormal.Back.Color1 = Color.White;
            dtpScheduledScans.StateNormal.Border.Color1 = Color.Black;
            dtpScheduledScans.StateNormal.Border.Color2 = Color.Black;
            dtpScheduledScans.TabIndex = 5;
            dtpScheduledScans.ValueChanged += dtpScheduledScans_ValueChanged;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(65, 67, 106);
            ClientSize = new Size(328, 497);
            Controls.Add(dtpScheduledScans);
            Controls.Add(rbScheduledScans);
            Controls.Add(kryptonLabel2);
            Controls.Add(lbThreadCount);
            Controls.Add(kryptonLabel1);
            Controls.Add(tbThreads);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "SettingsForm";
            Text = "Settings";
            Load += SettingsForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Krypton.Toolkit.KryptonTrackBar tbThreads;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonLabel lbThreadCount;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private Krypton.Toolkit.KryptonRadioButton rbScheduledScans;
        private Krypton.Toolkit.KryptonDateTimePicker dtpScheduledScans;
    }
}