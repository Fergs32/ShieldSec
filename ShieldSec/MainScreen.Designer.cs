namespace ShieldSec
{
    partial class MainScreen
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainScreen));
            kryptonCustomPaletteBase1 = new Krypton.Toolkit.KryptonCustomPaletteBase(components);
            buttonScan = new Krypton.Toolkit.KryptonButton();
            buttonPanel = new Krypton.Toolkit.KryptonPanel();
            hitsTotalLabel = new Krypton.Toolkit.KryptonLabel();
            buttonSettings = new Krypton.Toolkit.KryptonButton();
            kryptonButton3 = new Krypton.Toolkit.KryptonButton();
            buttonAnalytics = new Krypton.Toolkit.KryptonButton();
            buttonDashboard = new Krypton.Toolkit.KryptonButton();
            userDefaultPicture = new Krypton.Toolkit.KryptonPictureBox();
            progressBarLabel = new Krypton.Toolkit.KryptonLabel();
            fileUpdateLabel = new Krypton.Toolkit.KryptonLabel();
            lbFileProgress = new Krypton.Toolkit.KryptonLabel();
            lbCircularProgress = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)buttonPanel).BeginInit();
            buttonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)userDefaultPicture).BeginInit();
            SuspendLayout();
            // 
            // kryptonCustomPaletteBase1
            // 
            kryptonCustomPaletteBase1.BaseRenderMode = Krypton.Toolkit.RendererMode.VisualStudio;
            kryptonCustomPaletteBase1.ButtonSpecs.FormClose.Image = (Image)resources.GetObject("resource.Image");
            kryptonCustomPaletteBase1.ButtonSpecs.FormClose.ImageStates.ImageTracking = (Image)resources.GetObject("resource.ImageTracking");
            kryptonCustomPaletteBase1.ButtonSpecs.FormClose.ToolTipTitle = "Close this window";
            kryptonCustomPaletteBase1.ButtonSpecs.FormMin.Image = (Image)resources.GetObject("resource.Image1");
            kryptonCustomPaletteBase1.ButtonSpecs.FormMin.ImageStates.ImageTracking = (Image)resources.GetObject("resource.ImageTracking1");
            kryptonCustomPaletteBase1.ButtonSpecs.FormMin.ToolTipTitle = "Minimize this window";
            kryptonCustomPaletteBase1.ButtonStyles.ButtonForm.StateNormal.Back.Color1 = Color.FromArgb(65, 67, 106);
            kryptonCustomPaletteBase1.ButtonStyles.ButtonForm.StateNormal.Back.Color2 = Color.FromArgb(65, 67, 106);
            kryptonCustomPaletteBase1.ButtonStyles.ButtonForm.StateNormal.Border.Width = 0;
            kryptonCustomPaletteBase1.ButtonStyles.ButtonForm.StatePressed.Back.Color1 = Color.FromArgb(65, 67, 106);
            kryptonCustomPaletteBase1.ButtonStyles.ButtonForm.StatePressed.Back.Color2 = Color.FromArgb(65, 67, 106);
            kryptonCustomPaletteBase1.ButtonStyles.ButtonForm.StatePressed.Border.Width = 0;
            kryptonCustomPaletteBase1.ButtonStyles.ButtonForm.StateTracking.Back.Color1 = Color.FromArgb(65, 67, 106);
            kryptonCustomPaletteBase1.ButtonStyles.ButtonForm.StateTracking.Back.Color2 = Color.FromArgb(65, 67, 106);
            kryptonCustomPaletteBase1.ButtonStyles.ButtonForm.StateTracking.Border.Width = 0;
            kryptonCustomPaletteBase1.FormStyles.FormMain.StateCommon.Back.Color1 = Color.FromArgb(65, 67, 106);
            kryptonCustomPaletteBase1.FormStyles.FormMain.StateCommon.Back.Color2 = Color.FromArgb(65, 67, 106);
            kryptonCustomPaletteBase1.FormStyles.FormMain.StateCommon.Border.Color1 = Color.FromArgb(152, 64, 98);
            kryptonCustomPaletteBase1.FormStyles.FormMain.StateCommon.Border.Color2 = Color.FromArgb(152, 64, 98);
            kryptonCustomPaletteBase1.FormStyles.FormMain.StateCommon.Border.Rounding = 16F;
            kryptonCustomPaletteBase1.HeaderStyles.HeaderForm.StateCommon.Back.Color1 = Color.FromArgb(65, 67, 106);
            kryptonCustomPaletteBase1.HeaderStyles.HeaderForm.StateCommon.Back.Color2 = Color.FromArgb(65, 67, 106);
            kryptonCustomPaletteBase1.HeaderStyles.HeaderForm.StateCommon.Border.Color1 = Color.FromArgb(152, 64, 98);
            kryptonCustomPaletteBase1.HeaderStyles.HeaderForm.StateCommon.Border.Color2 = Color.FromArgb(152, 64, 98);
            kryptonCustomPaletteBase1.HeaderStyles.HeaderForm.StateCommon.ButtonEdgeInset = 12;
            kryptonCustomPaletteBase1.UseThemeFormChromeBorderWidth = Krypton.Toolkit.InheritBool.True;
            // 
            // buttonScan
            // 
            buttonScan.ButtonStyle = Krypton.Toolkit.ButtonStyle.Alternate;
            buttonScan.Location = new Point(719, 431);
            buttonScan.Margin = new Padding(4);
            buttonScan.Name = "buttonScan";
            buttonScan.OverrideDefault.Back.Color1 = Color.FromArgb(246, 70, 104);
            buttonScan.OverrideDefault.Back.Color2 = Color.FromArgb(246, 70, 104);
            buttonScan.OverrideDefault.Back.ColorAngle = 45F;
            buttonScan.OverrideDefault.Border.Color1 = Color.FromArgb(152, 64, 98);
            buttonScan.OverrideDefault.Border.Color2 = Color.FromArgb(152, 64, 98);
            buttonScan.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            buttonScan.Size = new Size(233, 69);
            buttonScan.StateCommon.Back.Color1 = Color.FromArgb(246, 70, 104);
            buttonScan.StateCommon.Back.Color2 = Color.FromArgb(246, 70, 104);
            buttonScan.StateCommon.Back.ColorAngle = 45F;
            buttonScan.StateCommon.Back.GraphicsHint = Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            buttonScan.StateCommon.Border.Color1 = Color.FromArgb(152, 64, 98);
            buttonScan.StateCommon.Border.Color2 = Color.FromArgb(152, 64, 98);
            buttonScan.StateCommon.Border.ColorAngle = 45F;
            buttonScan.StateCommon.Border.GraphicsHint = Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            buttonScan.StateCommon.Border.Rounding = 18F;
            buttonScan.StateCommon.Border.Width = 1;
            buttonScan.StateCommon.Content.ShortText.Color1 = Color.White;
            buttonScan.StateCommon.Content.ShortText.Color2 = Color.White;
            buttonScan.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonScan.StateNormal.Back.Color1 = Color.FromArgb(246, 70, 104);
            buttonScan.StateNormal.Back.Color2 = Color.FromArgb(246, 70, 104);
            buttonScan.StateNormal.Back.ColorAngle = 160F;
            buttonScan.StateNormal.Back.GraphicsHint = Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            buttonScan.StateNormal.Border.Color1 = Color.FromArgb(152, 64, 98);
            buttonScan.StateNormal.Border.Color2 = Color.FromArgb(152, 64, 98);
            buttonScan.StateNormal.Border.ColorAngle = 160F;
            buttonScan.StateNormal.Border.Rounding = 18F;
            buttonScan.StateNormal.Border.Width = 1;
            buttonScan.StateNormal.Content.ShortText.Color1 = Color.White;
            buttonScan.StateNormal.Content.ShortText.Color2 = Color.White;
            buttonScan.StatePressed.Back.Color1 = Color.FromArgb(152, 64, 98);
            buttonScan.StatePressed.Back.Color2 = Color.FromArgb(152, 90, 98);
            buttonScan.StatePressed.Back.ColorAngle = 130F;
            buttonScan.StatePressed.Border.Color1 = Color.FromArgb(152, 64, 98);
            buttonScan.StatePressed.Border.Color2 = Color.FromArgb(152, 64, 98);
            buttonScan.StatePressed.Border.ColorAngle = 130F;
            buttonScan.StatePressed.Border.Rounding = 18F;
            buttonScan.StatePressed.Border.Width = 1;
            buttonScan.TabIndex = 0;
            buttonScan.Values.DropDownArrowColor = Color.Empty;
            buttonScan.Values.Text = "Scan";
            buttonScan.Click += buttonScan_Click;
            // 
            // buttonPanel
            // 
            buttonPanel.Controls.Add(hitsTotalLabel);
            buttonPanel.Controls.Add(buttonSettings);
            buttonPanel.Controls.Add(kryptonButton3);
            buttonPanel.Controls.Add(buttonAnalytics);
            buttonPanel.Controls.Add(buttonDashboard);
            buttonPanel.Controls.Add(userDefaultPicture);
            buttonPanel.Location = new Point(1, 3);
            buttonPanel.Margin = new Padding(4);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            buttonPanel.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            buttonPanel.Size = new Size(249, 503);
            buttonPanel.StateCommon.Color1 = Color.FromArgb(152, 64, 98);
            buttonPanel.StateCommon.Color2 = Color.FromArgb(152, 64, 98);
            buttonPanel.StateCommon.GraphicsHint = Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            buttonPanel.TabIndex = 0;
            // 
            // hitsTotalLabel
            // 
            hitsTotalLabel.LabelStyle = Krypton.Toolkit.LabelStyle.BoldPanel;
            hitsTotalLabel.LocalCustomPalette = kryptonCustomPaletteBase1;
            hitsTotalLabel.Location = new Point(168, 286);
            hitsTotalLabel.Name = "hitsTotalLabel";
            hitsTotalLabel.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            hitsTotalLabel.Size = new Size(18, 20);
            hitsTotalLabel.StateNormal.Image.ImageColorMap = Color.FromArgb(152, 64, 98);
            hitsTotalLabel.StateNormal.Image.ImageColorTo = Color.FromArgb(152, 64, 98);
            hitsTotalLabel.StateNormal.ShortText.Color1 = Color.FromArgb(128, 255, 128);
            hitsTotalLabel.StateNormal.ShortText.Color2 = Color.FromArgb(128, 255, 128);
            hitsTotalLabel.TabIndex = 6;
            hitsTotalLabel.Values.ImageTransparentColor = Color.FromArgb(152, 64, 98);
            hitsTotalLabel.Values.Text = "0";
            // 
            // buttonSettings
            // 
            buttonSettings.ButtonStyle = Krypton.Toolkit.ButtonStyle.Alternate;
            buttonSettings.Location = new Point(4, 428);
            buttonSettings.Margin = new Padding(4);
            buttonSettings.Name = "buttonSettings";
            buttonSettings.OverrideDefault.Back.Color1 = Color.FromArgb(246, 70, 104);
            buttonSettings.OverrideDefault.Back.Color2 = Color.FromArgb(246, 70, 104);
            buttonSettings.OverrideDefault.Back.ColorAngle = 45F;
            buttonSettings.OverrideDefault.Border.Color1 = Color.FromArgb(152, 64, 98);
            buttonSettings.OverrideDefault.Border.Color2 = Color.FromArgb(152, 64, 98);
            buttonSettings.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            buttonSettings.Size = new Size(241, 69);
            buttonSettings.StateCommon.Back.Color1 = Color.FromArgb(246, 70, 104);
            buttonSettings.StateCommon.Back.Color2 = Color.FromArgb(246, 70, 104);
            buttonSettings.StateCommon.Back.ColorAngle = 45F;
            buttonSettings.StateCommon.Back.GraphicsHint = Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            buttonSettings.StateCommon.Border.Color1 = Color.FromArgb(152, 64, 98);
            buttonSettings.StateCommon.Border.Color2 = Color.FromArgb(152, 64, 98);
            buttonSettings.StateCommon.Border.ColorAngle = 45F;
            buttonSettings.StateCommon.Border.GraphicsHint = Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            buttonSettings.StateCommon.Border.Rounding = 18F;
            buttonSettings.StateCommon.Border.Width = 1;
            buttonSettings.StateCommon.Content.ShortText.Color1 = Color.White;
            buttonSettings.StateCommon.Content.ShortText.Color2 = Color.White;
            buttonSettings.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonSettings.StateNormal.Back.Color1 = Color.FromArgb(246, 70, 104);
            buttonSettings.StateNormal.Back.Color2 = Color.FromArgb(246, 70, 104);
            buttonSettings.StateNormal.Back.ColorAngle = 160F;
            buttonSettings.StateNormal.Back.GraphicsHint = Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            buttonSettings.StateNormal.Border.Color1 = Color.FromArgb(152, 64, 98);
            buttonSettings.StateNormal.Border.Color2 = Color.FromArgb(152, 64, 98);
            buttonSettings.StateNormal.Border.ColorAngle = 160F;
            buttonSettings.StateNormal.Border.Rounding = 18F;
            buttonSettings.StateNormal.Border.Width = 1;
            buttonSettings.StateNormal.Content.ShortText.Color1 = Color.White;
            buttonSettings.StateNormal.Content.ShortText.Color2 = Color.White;
            buttonSettings.StatePressed.Back.Color1 = Color.FromArgb(152, 64, 98);
            buttonSettings.StatePressed.Back.Color2 = Color.FromArgb(152, 90, 98);
            buttonSettings.StatePressed.Back.ColorAngle = 130F;
            buttonSettings.StatePressed.Border.Color1 = Color.FromArgb(152, 64, 98);
            buttonSettings.StatePressed.Border.Color2 = Color.FromArgb(152, 64, 98);
            buttonSettings.StatePressed.Border.ColorAngle = 130F;
            buttonSettings.StatePressed.Border.Rounding = 18F;
            buttonSettings.StatePressed.Border.Width = 1;
            buttonSettings.TabIndex = 5;
            buttonSettings.Values.DropDownArrowColor = Color.Empty;
            buttonSettings.Values.Text = "Settings";
            buttonSettings.Click += buttonSettings_Click;
            // 
            // kryptonButton3
            // 
            kryptonButton3.ButtonStyle = Krypton.Toolkit.ButtonStyle.Alternate;
            kryptonButton3.Location = new Point(4, 351);
            kryptonButton3.Margin = new Padding(4);
            kryptonButton3.Name = "kryptonButton3";
            kryptonButton3.OverrideDefault.Back.Color1 = Color.FromArgb(246, 70, 104);
            kryptonButton3.OverrideDefault.Back.Color2 = Color.FromArgb(246, 70, 104);
            kryptonButton3.OverrideDefault.Back.ColorAngle = 45F;
            kryptonButton3.OverrideDefault.Border.Color1 = Color.FromArgb(152, 64, 98);
            kryptonButton3.OverrideDefault.Border.Color2 = Color.FromArgb(152, 64, 98);
            kryptonButton3.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            kryptonButton3.Size = new Size(241, 69);
            kryptonButton3.StateCommon.Back.Color1 = Color.FromArgb(246, 70, 104);
            kryptonButton3.StateCommon.Back.Color2 = Color.FromArgb(246, 70, 104);
            kryptonButton3.StateCommon.Back.ColorAngle = 45F;
            kryptonButton3.StateCommon.Back.GraphicsHint = Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            kryptonButton3.StateCommon.Border.Color1 = Color.FromArgb(152, 64, 98);
            kryptonButton3.StateCommon.Border.Color2 = Color.FromArgb(152, 64, 98);
            kryptonButton3.StateCommon.Border.ColorAngle = 45F;
            kryptonButton3.StateCommon.Border.GraphicsHint = Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            kryptonButton3.StateCommon.Border.Rounding = 18F;
            kryptonButton3.StateCommon.Border.Width = 1;
            kryptonButton3.StateCommon.Content.ShortText.Color1 = Color.White;
            kryptonButton3.StateCommon.Content.ShortText.Color2 = Color.White;
            kryptonButton3.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            kryptonButton3.StateNormal.Back.Color1 = Color.FromArgb(246, 70, 104);
            kryptonButton3.StateNormal.Back.Color2 = Color.FromArgb(246, 70, 104);
            kryptonButton3.StateNormal.Back.ColorAngle = 160F;
            kryptonButton3.StateNormal.Back.GraphicsHint = Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            kryptonButton3.StateNormal.Border.Color1 = Color.FromArgb(152, 64, 98);
            kryptonButton3.StateNormal.Border.Color2 = Color.FromArgb(152, 64, 98);
            kryptonButton3.StateNormal.Border.ColorAngle = 160F;
            kryptonButton3.StateNormal.Border.Rounding = 18F;
            kryptonButton3.StateNormal.Border.Width = 1;
            kryptonButton3.StateNormal.Content.ShortText.Color1 = Color.White;
            kryptonButton3.StateNormal.Content.ShortText.Color2 = Color.White;
            kryptonButton3.StatePressed.Back.Color1 = Color.FromArgb(152, 64, 98);
            kryptonButton3.StatePressed.Back.Color2 = Color.FromArgb(152, 90, 98);
            kryptonButton3.StatePressed.Back.ColorAngle = 130F;
            kryptonButton3.StatePressed.Border.Color1 = Color.FromArgb(152, 64, 98);
            kryptonButton3.StatePressed.Border.Color2 = Color.FromArgb(152, 64, 98);
            kryptonButton3.StatePressed.Border.ColorAngle = 130F;
            kryptonButton3.StatePressed.Border.Rounding = 18F;
            kryptonButton3.StatePressed.Border.Width = 1;
            kryptonButton3.TabIndex = 3;
            kryptonButton3.Values.DropDownArrowColor = Color.Empty;
            kryptonButton3.Values.ExtraText = "™";
            kryptonButton3.Values.Text = "Placeholder";
            // 
            // buttonAnalytics
            // 
            buttonAnalytics.ButtonStyle = Krypton.Toolkit.ButtonStyle.Alternate;
            buttonAnalytics.Location = new Point(4, 274);
            buttonAnalytics.Margin = new Padding(4);
            buttonAnalytics.Name = "buttonAnalytics";
            buttonAnalytics.OverrideDefault.Back.Color1 = Color.FromArgb(246, 70, 104);
            buttonAnalytics.OverrideDefault.Back.Color2 = Color.FromArgb(246, 70, 104);
            buttonAnalytics.OverrideDefault.Back.ColorAngle = 45F;
            buttonAnalytics.OverrideDefault.Border.Color1 = Color.FromArgb(152, 64, 98);
            buttonAnalytics.OverrideDefault.Border.Color2 = Color.FromArgb(152, 64, 98);
            buttonAnalytics.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            buttonAnalytics.Size = new Size(241, 69);
            buttonAnalytics.StateCommon.Back.Color1 = Color.FromArgb(246, 70, 104);
            buttonAnalytics.StateCommon.Back.Color2 = Color.FromArgb(246, 70, 104);
            buttonAnalytics.StateCommon.Back.ColorAngle = 45F;
            buttonAnalytics.StateCommon.Back.GraphicsHint = Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            buttonAnalytics.StateCommon.Border.Color1 = Color.FromArgb(152, 64, 98);
            buttonAnalytics.StateCommon.Border.Color2 = Color.FromArgb(152, 64, 98);
            buttonAnalytics.StateCommon.Border.ColorAngle = 45F;
            buttonAnalytics.StateCommon.Border.GraphicsHint = Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            buttonAnalytics.StateCommon.Border.Rounding = 18F;
            buttonAnalytics.StateCommon.Border.Width = 1;
            buttonAnalytics.StateCommon.Content.ShortText.Color1 = Color.White;
            buttonAnalytics.StateCommon.Content.ShortText.Color2 = Color.White;
            buttonAnalytics.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonAnalytics.StateNormal.Back.Color1 = Color.FromArgb(246, 70, 104);
            buttonAnalytics.StateNormal.Back.Color2 = Color.FromArgb(246, 70, 104);
            buttonAnalytics.StateNormal.Back.ColorAngle = 160F;
            buttonAnalytics.StateNormal.Back.GraphicsHint = Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            buttonAnalytics.StateNormal.Border.Color1 = Color.FromArgb(152, 64, 98);
            buttonAnalytics.StateNormal.Border.Color2 = Color.FromArgb(152, 64, 98);
            buttonAnalytics.StateNormal.Border.ColorAngle = 160F;
            buttonAnalytics.StateNormal.Border.Rounding = 18F;
            buttonAnalytics.StateNormal.Border.Width = 1;
            buttonAnalytics.StateNormal.Content.ShortText.Color1 = Color.White;
            buttonAnalytics.StateNormal.Content.ShortText.Color2 = Color.White;
            buttonAnalytics.StatePressed.Back.Color1 = Color.FromArgb(152, 64, 98);
            buttonAnalytics.StatePressed.Back.Color2 = Color.FromArgb(152, 90, 98);
            buttonAnalytics.StatePressed.Back.ColorAngle = 130F;
            buttonAnalytics.StatePressed.Border.Color1 = Color.FromArgb(152, 64, 98);
            buttonAnalytics.StatePressed.Border.Color2 = Color.FromArgb(152, 64, 98);
            buttonAnalytics.StatePressed.Border.ColorAngle = 130F;
            buttonAnalytics.StatePressed.Border.Rounding = 18F;
            buttonAnalytics.StatePressed.Border.Width = 1;
            buttonAnalytics.TabIndex = 2;
            buttonAnalytics.Values.DropDownArrowColor = Color.Empty;
            buttonAnalytics.Values.Text = "Analysis";
            buttonAnalytics.Click += buttonAnalytics_Click;
            // 
            // buttonDashboard
            // 
            buttonDashboard.ButtonStyle = Krypton.Toolkit.ButtonStyle.Alternate;
            buttonDashboard.Location = new Point(4, 197);
            buttonDashboard.Margin = new Padding(4);
            buttonDashboard.Name = "buttonDashboard";
            buttonDashboard.OverrideDefault.Back.Color1 = Color.FromArgb(246, 70, 104);
            buttonDashboard.OverrideDefault.Back.Color2 = Color.FromArgb(246, 70, 104);
            buttonDashboard.OverrideDefault.Back.ColorAngle = 45F;
            buttonDashboard.OverrideDefault.Border.Color1 = Color.FromArgb(152, 64, 98);
            buttonDashboard.OverrideDefault.Border.Color2 = Color.FromArgb(152, 64, 98);
            buttonDashboard.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            buttonDashboard.Size = new Size(241, 69);
            buttonDashboard.StateCommon.Back.Color1 = Color.FromArgb(246, 70, 104);
            buttonDashboard.StateCommon.Back.Color2 = Color.FromArgb(246, 70, 104);
            buttonDashboard.StateCommon.Back.ColorAngle = 45F;
            buttonDashboard.StateCommon.Back.GraphicsHint = Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            buttonDashboard.StateCommon.Border.Color1 = Color.FromArgb(152, 64, 98);
            buttonDashboard.StateCommon.Border.Color2 = Color.FromArgb(152, 64, 98);
            buttonDashboard.StateCommon.Border.ColorAngle = 45F;
            buttonDashboard.StateCommon.Border.GraphicsHint = Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            buttonDashboard.StateCommon.Border.Rounding = 18F;
            buttonDashboard.StateCommon.Border.Width = 1;
            buttonDashboard.StateCommon.Content.ShortText.Color1 = Color.White;
            buttonDashboard.StateCommon.Content.ShortText.Color2 = Color.White;
            buttonDashboard.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonDashboard.StateNormal.Back.Color1 = Color.FromArgb(246, 70, 104);
            buttonDashboard.StateNormal.Back.Color2 = Color.FromArgb(246, 70, 104);
            buttonDashboard.StateNormal.Back.ColorAngle = 160F;
            buttonDashboard.StateNormal.Back.GraphicsHint = Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            buttonDashboard.StateNormal.Border.Color1 = Color.FromArgb(152, 64, 98);
            buttonDashboard.StateNormal.Border.Color2 = Color.FromArgb(152, 64, 98);
            buttonDashboard.StateNormal.Border.ColorAngle = 160F;
            buttonDashboard.StateNormal.Border.Rounding = 18F;
            buttonDashboard.StateNormal.Border.Width = 1;
            buttonDashboard.StateNormal.Content.ShortText.Color1 = Color.White;
            buttonDashboard.StateNormal.Content.ShortText.Color2 = Color.White;
            buttonDashboard.StatePressed.Back.Color1 = Color.FromArgb(152, 64, 98);
            buttonDashboard.StatePressed.Back.Color2 = Color.FromArgb(152, 90, 98);
            buttonDashboard.StatePressed.Back.ColorAngle = 130F;
            buttonDashboard.StatePressed.Border.Color1 = Color.FromArgb(152, 64, 98);
            buttonDashboard.StatePressed.Border.Color2 = Color.FromArgb(152, 64, 98);
            buttonDashboard.StatePressed.Border.ColorAngle = 130F;
            buttonDashboard.StatePressed.Border.Rounding = 18F;
            buttonDashboard.StatePressed.Border.Width = 1;
            buttonDashboard.TabIndex = 1;
            buttonDashboard.Values.DropDownArrowColor = Color.Empty;
            buttonDashboard.Values.Text = "Dashboard";
            // 
            // userDefaultPicture
            // 
            userDefaultPicture.Image = (Image)resources.GetObject("userDefaultPicture.Image");
            userDefaultPicture.Location = new Point(-46, 3);
            userDefaultPicture.Name = "userDefaultPicture";
            userDefaultPicture.Size = new Size(341, 195);
            userDefaultPicture.SizeMode = PictureBoxSizeMode.Zoom;
            userDefaultPicture.TabIndex = 0;
            userDefaultPicture.TabStop = false;
            // 
            // progressBarLabel
            // 
            progressBarLabel.Location = new Point(257, 481);
            progressBarLabel.Name = "progressBarLabel";
            progressBarLabel.Size = new Size(95, 19);
            progressBarLabel.StateCommon.ShortText.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            progressBarLabel.StateNormal.ShortText.Color1 = Color.White;
            progressBarLabel.StateNormal.ShortText.Color2 = Color.White;
            progressBarLabel.StateNormal.ShortText.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            progressBarLabel.TabIndex = 2;
            progressBarLabel.Values.Text = "Scanning file:";
            // 
            // fileUpdateLabel
            // 
            fileUpdateLabel.Location = new Point(435, 583);
            fileUpdateLabel.Name = "fileUpdateLabel";
            fileUpdateLabel.Size = new Size(6, 2);
            fileUpdateLabel.StateNormal.ShortText.Color1 = Color.White;
            fileUpdateLabel.StateNormal.ShortText.Color2 = Color.White;
            fileUpdateLabel.StateNormal.ShortText.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            fileUpdateLabel.TabIndex = 3;
            fileUpdateLabel.Values.Text = "";
            // 
            // lbFileProgress
            // 
            lbFileProgress.Location = new Point(347, 481);
            lbFileProgress.Name = "lbFileProgress";
            lbFileProgress.Size = new Size(6, 2);
            lbFileProgress.StateCommon.ShortText.Color1 = Color.FromArgb(128, 255, 128);
            lbFileProgress.StateCommon.ShortText.Color2 = Color.FromArgb(128, 255, 128);
            lbFileProgress.StateCommon.ShortText.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbFileProgress.TabIndex = 4;
            lbFileProgress.Values.Text = "";
            // 
            // lbCircularProgress
            // 
            lbCircularProgress.Location = new Point(471, 260);
            lbCircularProgress.Name = "lbCircularProgress";
            lbCircularProgress.Size = new Size(6, 2);
            lbCircularProgress.StateCommon.ShortText.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbCircularProgress.StateNormal.ShortText.Color1 = Color.White;
            lbCircularProgress.StateNormal.ShortText.Color2 = Color.White;
            lbCircularProgress.StateNormal.ShortText.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbCircularProgress.TabIndex = 5;
            lbCircularProgress.Values.Text = "";
            // 
            // MainScreen
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(65, 67, 106);
            ClientSize = new Size(989, 510);
            Controls.Add(lbCircularProgress);
            Controls.Add(lbFileProgress);
            Controls.Add(fileUpdateLabel);
            Controls.Add(progressBarLabel);
            Controls.Add(buttonPanel);
            Controls.Add(buttonScan);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            LocalCustomPalette = kryptonCustomPaletteBase1;
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "MainScreen";
            PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            StartPosition = FormStartPosition.CenterScreen;
            StateCommon.Header.Content.ShortText.Color1 = Color.White;
            StateCommon.Header.Content.ShortText.Color2 = Color.White;
            StateCommon.Header.Content.ShortText.ColorAngle = 45F;
            StateCommon.Header.Content.ShortText.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Dashed;
            StateCommon.Header.Content.ShortText.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            StateCommon.Header.Content.ShortText.ImageStyle = Krypton.Toolkit.PaletteImageStyle.TopLeft;
            StateCommon.OverlayHeaders = Krypton.Toolkit.InheritBool.True;
            Text = "  ShieldSec | Anti-Virus";
            ((System.ComponentModel.ISupportInitialize)buttonPanel).EndInit();
            buttonPanel.ResumeLayout(false);
            buttonPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)userDefaultPicture).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Krypton.Toolkit.KryptonCustomPaletteBase kryptonCustomPaletteBase1;
        private Krypton.Toolkit.KryptonButton buttonScan;
        private Krypton.Toolkit.KryptonPanel buttonPanel;
        private Krypton.Toolkit.KryptonPictureBox userDefaultPicture;
        private Krypton.Toolkit.KryptonButton buttonSettings;
        private Krypton.Toolkit.KryptonButton kryptonButton3;
        private Krypton.Toolkit.KryptonButton buttonAnalytics;
        private Krypton.Toolkit.KryptonButton buttonDashboard;
        private Krypton.Toolkit.KryptonLabel progressBarLabel;
        private Krypton.Toolkit.KryptonLabel fileUpdateLabel;
        private Krypton.Toolkit.KryptonLabel hitsTotalLabel;
        private Krypton.Toolkit.KryptonLabel lbFileProgress;
        private Krypton.Toolkit.KryptonLabel lbCircularProgress;
    }
}
