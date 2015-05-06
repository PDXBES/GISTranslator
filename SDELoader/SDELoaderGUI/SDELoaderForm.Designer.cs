namespace SDELoader
{
    partial class SDELoaderForm
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SDELoaderForm));
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FeatureClass", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FeatureClassID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UploadCommandID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SourceFC", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DestFC");
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
            this.btnLoadToSDE = new Infragistics.Win.Misc.UltraButton();
            this.btnLoadConfig = new Infragistics.Win.Misc.UltraButton();
            this.btnSaveConfig = new Infragistics.Win.Misc.UltraButton();
            this.openConfigFile = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.txtConfigDescription = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.sdeLoaderConfig = new SDELoader.SDELoaderConfig();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
            this.txtPGDB = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtSDEServer = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
            this.txtSDEInstance = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
            this.txtSDEDatabase = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
            this.txtSDEUser = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
            this.txtSDEVersion = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
            this.txtSDEPassword = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnImportSDE = new Infragistics.Win.Misc.UltraButton();
            this.btnBrowsePGDB = new Infragistics.Win.Misc.UltraButton();
            this.browsePGDBDialog = new System.Windows.Forms.OpenFileDialog();
            this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraGrid1 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtConfigDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sdeLoaderConfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPGDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSDEServer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSDEInstance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSDEDatabase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSDEUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSDEVersion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSDEPassword)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ultraButton1);
            this.panel1.Controls.Add(this.btnLoadToSDE);
            this.panel1.Controls.Add(this.btnLoadConfig);
            this.panel1.Controls.Add(this.btnSaveConfig);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 497);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(796, 53);
            this.panel1.TabIndex = 1;
            // 
            // ultraButton1
            // 
            this.ultraButton1.Enabled = false;
            this.ultraButton1.Location = new System.Drawing.Point(498, 10);
            this.ultraButton1.Name = "ultraButton1";
            this.ultraButton1.Size = new System.Drawing.Size(185, 31);
            this.ultraButton1.TabIndex = 3;
            this.ultraButton1.Text = "Update PGDB from SDE";
            // 
            // btnLoadToSDE
            // 
            this.btnLoadToSDE.Location = new System.Drawing.Point(307, 10);
            this.btnLoadToSDE.Name = "btnLoadToSDE";
            this.btnLoadToSDE.Size = new System.Drawing.Size(185, 31);
            this.btnLoadToSDE.TabIndex = 2;
            this.btnLoadToSDE.Text = "Update SDE from PGDB";
            this.btnLoadToSDE.Click += new System.EventHandler(this.btnLoadToSDE_Click);
            // 
            // btnLoadConfig
            // 
            this.btnLoadConfig.Location = new System.Drawing.Point(4, 10);
            this.btnLoadConfig.Name = "btnLoadConfig";
            this.btnLoadConfig.Size = new System.Drawing.Size(141, 31);
            this.btnLoadConfig.TabIndex = 0;
            this.btnLoadConfig.Text = "Load Config File";
            this.btnLoadConfig.Click += new System.EventHandler(this.btnLoadConfig_Click);
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.Location = new System.Drawing.Point(151, 10);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(151, 31);
            this.btnSaveConfig.TabIndex = 1;
            this.btnSaveConfig.Text = "Save Config File";
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            // 
            // openConfigFile
            // 
            this.openConfigFile.FileName = "openConfigFile";
            this.openConfigFile.Filter = "*.xml|*.xml";
            this.openConfigFile.Title = "Open SDEUploader Config File:";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "*.xml|*.xml";
            this.saveFileDialog1.Title = "Save SDEUploader Config File as:";
            // 
            // txtConfigDescription
            // 
            this.txtConfigDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConfigDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.sdeLoaderConfig, "UploadCommand.Description", true));
            this.txtConfigDescription.Location = new System.Drawing.Point(3, 43);
            this.txtConfigDescription.Name = "txtConfigDescription";
            this.txtConfigDescription.Size = new System.Drawing.Size(789, 24);
            this.txtConfigDescription.TabIndex = 0;
            // 
            // sdeLoaderConfig
            // 
            this.sdeLoaderConfig.DataSetName = "sdeUploadConfig";
            this.sdeLoaderConfig.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ultraLabel1
            // 
            this.ultraLabel1.Location = new System.Drawing.Point(3, 17);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(230, 20);
            this.ultraLabel1.TabIndex = 3;
            this.ultraLabel1.Text = "Config File Description";
            // 
            // ultraLabel2
            // 
            this.ultraLabel2.Location = new System.Drawing.Point(4, 76);
            this.ultraLabel2.Name = "ultraLabel2";
            this.ultraLabel2.Size = new System.Drawing.Size(150, 20);
            this.ultraLabel2.TabIndex = 5;
            this.ultraLabel2.Text = "PGDB";
            // 
            // txtPGDB
            // 
            this.txtPGDB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPGDB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.sdeLoaderConfig, "UploadCommand.UploadCommand_PGDBWorkspace.PGDB", true));
            this.txtPGDB.Location = new System.Drawing.Point(4, 102);
            this.txtPGDB.Name = "txtPGDB";
            this.txtPGDB.Size = new System.Drawing.Size(756, 24);
            this.txtPGDB.TabIndex = 1;
            // 
            // txtSDEServer
            // 
            this.txtSDEServer.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.sdeLoaderConfig, "UploadCommand.UploadCommand_SDEWorkspace.Server", true));
            this.txtSDEServer.Location = new System.Drawing.Point(92, 24);
            this.txtSDEServer.Name = "txtSDEServer";
            this.txtSDEServer.Size = new System.Drawing.Size(230, 24);
            this.txtSDEServer.TabIndex = 0;
            // 
            // ultraLabel4
            // 
            this.ultraLabel4.Location = new System.Drawing.Point(10, 24);
            this.ultraLabel4.Name = "ultraLabel4";
            this.ultraLabel4.Size = new System.Drawing.Size(76, 20);
            this.ultraLabel4.TabIndex = 8;
            this.ultraLabel4.Text = "Server";
            // 
            // ultraLabel5
            // 
            this.ultraLabel5.Location = new System.Drawing.Point(353, 23);
            this.ultraLabel5.Name = "ultraLabel5";
            this.ultraLabel5.Size = new System.Drawing.Size(76, 20);
            this.ultraLabel5.TabIndex = 10;
            this.ultraLabel5.Text = "Instance";
            // 
            // txtSDEInstance
            // 
            this.txtSDEInstance.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.sdeLoaderConfig, "UploadCommand.UploadCommand_SDEWorkspace.Instance", true));
            this.txtSDEInstance.Location = new System.Drawing.Point(435, 24);
            this.txtSDEInstance.Name = "txtSDEInstance";
            this.txtSDEInstance.Size = new System.Drawing.Size(177, 24);
            this.txtSDEInstance.TabIndex = 1;
            // 
            // ultraLabel6
            // 
            this.ultraLabel6.Location = new System.Drawing.Point(10, 88);
            this.ultraLabel6.Name = "ultraLabel6";
            this.ultraLabel6.Size = new System.Drawing.Size(76, 20);
            this.ultraLabel6.TabIndex = 14;
            this.ultraLabel6.Text = "Database";
            // 
            // txtSDEDatabase
            // 
            this.txtSDEDatabase.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.sdeLoaderConfig, "UploadCommand.UploadCommand_SDEWorkspace.Database", true));
            this.txtSDEDatabase.Location = new System.Drawing.Point(92, 88);
            this.txtSDEDatabase.Name = "txtSDEDatabase";
            this.txtSDEDatabase.Size = new System.Drawing.Size(230, 24);
            this.txtSDEDatabase.TabIndex = 4;
            // 
            // ultraLabel7
            // 
            this.ultraLabel7.Location = new System.Drawing.Point(10, 54);
            this.ultraLabel7.Name = "ultraLabel7";
            this.ultraLabel7.Size = new System.Drawing.Size(76, 20);
            this.ultraLabel7.TabIndex = 12;
            this.ultraLabel7.Text = "User";
            // 
            // txtSDEUser
            // 
            this.txtSDEUser.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.sdeLoaderConfig, "UploadCommand.UploadCommand_SDEWorkspace.User", true));
            this.txtSDEUser.Location = new System.Drawing.Point(92, 54);
            this.txtSDEUser.Name = "txtSDEUser";
            this.txtSDEUser.Size = new System.Drawing.Size(230, 24);
            this.txtSDEUser.TabIndex = 2;
            // 
            // ultraLabel8
            // 
            this.ultraLabel8.Location = new System.Drawing.Point(353, 82);
            this.ultraLabel8.Name = "ultraLabel8";
            this.ultraLabel8.Size = new System.Drawing.Size(76, 20);
            this.ultraLabel8.TabIndex = 18;
            this.ultraLabel8.Text = "Version";
            // 
            // txtSDEVersion
            // 
            this.txtSDEVersion.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.sdeLoaderConfig, "UploadCommand.UploadCommand_SDEWorkspace.Version", true));
            this.txtSDEVersion.Location = new System.Drawing.Point(435, 81);
            this.txtSDEVersion.Name = "txtSDEVersion";
            this.txtSDEVersion.Size = new System.Drawing.Size(177, 24);
            this.txtSDEVersion.TabIndex = 5;
            // 
            // ultraLabel9
            // 
            this.ultraLabel9.Location = new System.Drawing.Point(353, 51);
            this.ultraLabel9.Name = "ultraLabel9";
            this.ultraLabel9.Size = new System.Drawing.Size(76, 20);
            this.ultraLabel9.TabIndex = 16;
            this.ultraLabel9.Text = "Password";
            // 
            // txtSDEPassword
            // 
            this.txtSDEPassword.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.sdeLoaderConfig, "UploadCommand.UploadCommand_SDEWorkspace.Password", true));
            this.txtSDEPassword.Location = new System.Drawing.Point(435, 51);
            this.txtSDEPassword.Name = "txtSDEPassword";
            this.txtSDEPassword.Size = new System.Drawing.Size(177, 24);
            this.txtSDEPassword.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnImportSDE);
            this.groupBox1.Controls.Add(this.txtSDEServer);
            this.groupBox1.Controls.Add(this.ultraLabel8);
            this.groupBox1.Controls.Add(this.ultraLabel4);
            this.groupBox1.Controls.Add(this.txtSDEVersion);
            this.groupBox1.Controls.Add(this.txtSDEInstance);
            this.groupBox1.Controls.Add(this.ultraLabel9);
            this.groupBox1.Controls.Add(this.ultraLabel5);
            this.groupBox1.Controls.Add(this.txtSDEPassword);
            this.groupBox1.Controls.Add(this.txtSDEUser);
            this.groupBox1.Controls.Add(this.ultraLabel6);
            this.groupBox1.Controls.Add(this.ultraLabel7);
            this.groupBox1.Controls.Add(this.txtSDEDatabase);
            this.groupBox1.Location = new System.Drawing.Point(4, 135);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(788, 148);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SDE";
            // 
            // btnImportSDE
            // 
            this.btnImportSDE.Enabled = false;
            this.btnImportSDE.Location = new System.Drawing.Point(176, 118);
            this.btnImportSDE.Name = "btnImportSDE";
            this.btnImportSDE.Size = new System.Drawing.Size(312, 24);
            this.btnImportSDE.TabIndex = 22;
            this.btnImportSDE.Text = "Import SDE parameters from .sde file";
            // 
            // btnBrowsePGDB
            // 
            this.btnBrowsePGDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.ImageBackground = ((System.Drawing.Image)(resources.GetObject("appearance1.ImageBackground")));
            this.btnBrowsePGDB.Appearance = appearance1;
            this.btnBrowsePGDB.Location = new System.Drawing.Point(766, 102);
            this.btnBrowsePGDB.Name = "btnBrowsePGDB";
            this.btnBrowsePGDB.Size = new System.Drawing.Size(26, 24);
            this.btnBrowsePGDB.TabIndex = 21;
            this.btnBrowsePGDB.Click += new System.EventHandler(this.btnBrowsePGDB_Click);
            // 
            // browsePGDBDialog
            // 
            this.browsePGDBDialog.FileName = "*.mdb";
            this.browsePGDBDialog.Filter = "*.mdb|*.mdb";
            this.browsePGDBDialog.Title = "Locate an Access-based PGDB:";
            // 
            // ultraLabel3
            // 
            this.ultraLabel3.Location = new System.Drawing.Point(4, 289);
            this.ultraLabel3.Name = "ultraLabel3";
            this.ultraLabel3.Size = new System.Drawing.Size(198, 20);
            this.ultraLabel3.TabIndex = 23;
            this.ultraLabel3.Text = "FeatureClass List";
            // 
            // ultraGrid1
            // 
            this.ultraGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGrid1.DataMember = "UploadCommand.SDEWorkspace_SDEFeatureClass";
            this.ultraGrid1.DataSource = this.sdeLoaderConfig;
            this.ultraGrid1.DisplayLayout.AddNewBox.Hidden = false;
            appearance2.BackColor = System.Drawing.SystemColors.Window;
            appearance2.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraGrid1.DisplayLayout.Appearance = appearance2;
            this.ultraGrid1.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn1.Width = 274;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn2.Width = 413;
            ultraGridColumn3.Header.Caption = "Source FC";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 480;
            ultraGridColumn4.Header.Caption = "Destination FC";
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Width = 287;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4});
            this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.ultraGrid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraGrid1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance3.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance3.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGrid1.DisplayLayout.GroupByBox.Appearance = appearance3;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGrid1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance4;
            this.ultraGrid1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance5.BackColor2 = System.Drawing.SystemColors.Control;
            appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGrid1.DisplayLayout.GroupByBox.PromptAppearance = appearance5;
            this.ultraGrid1.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraGrid1.DisplayLayout.MaxRowScrollRegions = 1;
            appearance6.BackColor = System.Drawing.SystemColors.Window;
            appearance6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraGrid1.DisplayLayout.Override.ActiveCellAppearance = appearance6;
            appearance7.BackColor = System.Drawing.SystemColors.Highlight;
            appearance7.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraGrid1.DisplayLayout.Override.ActiveRowAppearance = appearance7;
            this.ultraGrid1.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.Yes;
            this.ultraGrid1.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.True;
            this.ultraGrid1.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
            this.ultraGrid1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraGrid1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance8.BackColor = System.Drawing.SystemColors.Window;
            this.ultraGrid1.DisplayLayout.Override.CardAreaAppearance = appearance8;
            appearance9.BorderColor = System.Drawing.Color.Silver;
            appearance9.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraGrid1.DisplayLayout.Override.CellAppearance = appearance9;
            this.ultraGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraGrid1.DisplayLayout.Override.CellPadding = 0;
            appearance10.BackColor = System.Drawing.SystemColors.Control;
            appearance10.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance10.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance10.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGrid1.DisplayLayout.Override.GroupByRowAppearance = appearance10;
            appearance11.TextHAlignAsString = "Left";
            this.ultraGrid1.DisplayLayout.Override.HeaderAppearance = appearance11;
            this.ultraGrid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraGrid1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance12.BackColor = System.Drawing.SystemColors.Window;
            appearance12.BorderColor = System.Drawing.Color.Silver;
            this.ultraGrid1.DisplayLayout.Override.RowAppearance = appearance12;
            this.ultraGrid1.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.SeparateElement;
            this.ultraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance13.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraGrid1.DisplayLayout.Override.TemplateAddRowAppearance = appearance13;
            this.ultraGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGrid1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraGrid1.Location = new System.Drawing.Point(4, 316);
            this.ultraGrid1.Name = "ultraGrid1";
            this.ultraGrid1.Size = new System.Drawing.Size(788, 175);
            this.ultraGrid1.TabIndex = 2;
            this.ultraGrid1.Text = "ultraGrid1";
            // 
            // SDELoaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 550);
            this.Controls.Add(this.ultraGrid1);
            this.Controls.Add(this.ultraLabel3);
            this.Controls.Add(this.btnBrowsePGDB);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ultraLabel2);
            this.Controls.Add(this.txtPGDB);
            this.Controls.Add(this.ultraLabel1);
            this.Controls.Add(this.txtConfigDescription);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SDELoaderForm";
            this.Text = "SDELoader";
            this.Load += new System.EventHandler(this.SDELoaderForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtConfigDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sdeLoaderConfig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPGDB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSDEServer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSDEInstance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSDEDatabase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSDEUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSDEVersion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSDEPassword)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SDELoaderConfig sdeLoaderConfig;
        private System.Windows.Forms.Panel panel1;
        private Infragistics.Win.Misc.UltraButton btnLoadToSDE;
        private Infragistics.Win.Misc.UltraButton btnLoadConfig;
        private Infragistics.Win.Misc.UltraButton btnSaveConfig;
        private System.Windows.Forms.OpenFileDialog openConfigFile;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtConfigDescription;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private Infragistics.Win.Misc.UltraLabel ultraLabel2;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtPGDB;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtSDEServer;
        private Infragistics.Win.Misc.UltraButton ultraButton1;
        private Infragistics.Win.Misc.UltraLabel ultraLabel4;
        private Infragistics.Win.Misc.UltraLabel ultraLabel5;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtSDEInstance;
        private Infragistics.Win.Misc.UltraLabel ultraLabel6;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtSDEDatabase;
        private Infragistics.Win.Misc.UltraLabel ultraLabel7;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtSDEUser;
        private Infragistics.Win.Misc.UltraLabel ultraLabel8;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtSDEVersion;
        private Infragistics.Win.Misc.UltraLabel ultraLabel9;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtSDEPassword;
        private System.Windows.Forms.GroupBox groupBox1;
        private Infragistics.Win.Misc.UltraButton btnBrowsePGDB;
        private System.Windows.Forms.OpenFileDialog browsePGDBDialog;
        private Infragistics.Win.Misc.UltraButton btnImportSDE;
        private Infragistics.Win.Misc.UltraLabel ultraLabel3;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid1;
    }
}

