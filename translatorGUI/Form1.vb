Imports ESRI.ArcGIS.esriSystem

Imports translator
Imports dotnetutils
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
'Imports ESRI.ArcGIS.Utility

Public Class Form1
  Inherits System.Windows.Forms.Form

  'Private Shared m_AOLicenseInitializer As LicenseInitializer = New translatorGUI.LicenseInitializer()
  <STAThread()> Shared Sub Main()
    Try
      Dim bound As Boolean
      bound = ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop)

      Dim aoInitialize As IAoInitialize
      aoInitialize = New AoInitialize
      Dim status As esriLicenseStatus
      status = aoInitialize.Initialize(esriLicenseProductCode.esriLicenseProductCodeAdvanced)

      Application.Run(New Form1())

      aoInitialize.Shutdown()

    Catch ex As Exception
      MessageBox.Show("Unable to initialize ArcGIS license. Do you have ArcGIS 10.2 installed and licensed?")
    End Try        

  End Sub
#Region " Windows Form Designer generated code "

  Public Sub New()
    MyBase.New()

    'This call is required by the Windows Form Designer.
    InitializeComponent()

    'Add any initialization after the InitializeComponent() call

  End Sub

  'Form overrides dispose to clean up the component list.
  Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    log = New translationLog(txtLogFile.Text)
    log.writeLog("Clean Exit")
    log = Nothing
    If disposing Then
      If Not (components Is Nothing) Then
        components.Dispose()
      End If
    End If
    MyBase.Dispose(disposing)
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
  Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
  Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
  Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
  Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
  Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents lblLogFile As System.Windows.Forms.Label
  Friend WithEvents txtLogFile As System.Windows.Forms.TextBox
  Friend WithEvents lblInput As System.Windows.Forms.Label
  Friend WithEvents txtInput As System.Windows.Forms.TextBox
  Friend WithEvents cmdBrowseLogFile As System.Windows.Forms.Button
  Friend WithEvents cmdBrowseOutput As System.Windows.Forms.Button
  Friend WithEvents cmdBrowseInput As System.Windows.Forms.Button
  Friend WithEvents grpMultiTranslate As System.Windows.Forms.GroupBox
  Friend WithEvents rdbOneFile As System.Windows.Forms.RadioButton
  Friend WithEvents rdbOneDirectory As System.Windows.Forms.RadioButton
  Friend WithEvents rdbDirectoryTree As System.Windows.Forms.RadioButton
  Friend WithEvents grpTranslationType As System.Windows.Forms.GroupBox
  Friend WithEvents rdbTypePoint As System.Windows.Forms.RadioButton
  Friend WithEvents rdbTypeLine As System.Windows.Forms.RadioButton
  Friend WithEvents rdbTypePolygon As System.Windows.Forms.RadioButton
  Friend WithEvents cmdTranslate As System.Windows.Forms.Button
  Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
  Friend WithEvents lblOutputLocation As System.Windows.Forms.Label
  Friend WithEvents txtOutputLocation As System.Windows.Forms.TextBox
  Friend WithEvents lblOutputName As System.Windows.Forms.Label
  Friend WithEvents txtOutputName As System.Windows.Forms.TextBox
  Friend WithEvents cmdExit As System.Windows.Forms.Button
  Friend WithEvents chkOverwritePGDB As System.Windows.Forms.CheckBox
  Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
  Friend WithEvents rdbTypeAuto As System.Windows.Forms.RadioButton
  Friend WithEvents rdbWorkspace As System.Windows.Forms.RadioButton
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
    Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
    Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
    Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
    Me.MenuItem1 = New System.Windows.Forms.MenuItem()
    Me.MenuItem3 = New System.Windows.Forms.MenuItem()
    Me.MenuItem2 = New System.Windows.Forms.MenuItem()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.ComboBox2 = New System.Windows.Forms.ComboBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.ComboBox1 = New System.Windows.Forms.ComboBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.chkOverwritePGDB = New System.Windows.Forms.CheckBox()
    Me.cmdExit = New System.Windows.Forms.Button()
    Me.txtOutputName = New System.Windows.Forms.TextBox()
    Me.lblOutputName = New System.Windows.Forms.Label()
    Me.cmdTranslate = New System.Windows.Forms.Button()
    Me.grpTranslationType = New System.Windows.Forms.GroupBox()
    Me.rdbTypePolygon = New System.Windows.Forms.RadioButton()
    Me.rdbTypeLine = New System.Windows.Forms.RadioButton()
    Me.rdbTypePoint = New System.Windows.Forms.RadioButton()
    Me.rdbTypeAuto = New System.Windows.Forms.RadioButton()
    Me.grpMultiTranslate = New System.Windows.Forms.GroupBox()
    Me.rdbWorkspace = New System.Windows.Forms.RadioButton()
    Me.rdbDirectoryTree = New System.Windows.Forms.RadioButton()
    Me.rdbOneDirectory = New System.Windows.Forms.RadioButton()
    Me.rdbOneFile = New System.Windows.Forms.RadioButton()
    Me.lblOutputLocation = New System.Windows.Forms.Label()
    Me.lblLogFile = New System.Windows.Forms.Label()
    Me.txtLogFile = New System.Windows.Forms.TextBox()
    Me.cmdBrowseLogFile = New System.Windows.Forms.Button()
    Me.cmdBrowseOutput = New System.Windows.Forms.Button()
    Me.txtOutputLocation = New System.Windows.Forms.TextBox()
    Me.lblInput = New System.Windows.Forms.Label()
    Me.txtInput = New System.Windows.Forms.TextBox()
    Me.cmdBrowseInput = New System.Windows.Forms.Button()
    Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
    Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
    Me.Panel1.SuspendLayout()
    Me.grpTranslationType.SuspendLayout()
    Me.grpMultiTranslate.SuspendLayout()
    Me.SuspendLayout()
    '
    'MainMenu1
    '
    Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2})
    '
    'MenuItem1
    '
    Me.MenuItem1.Index = 0
    Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem3})
    Me.MenuItem1.Text = "File"
    '
    'MenuItem3
    '
    Me.MenuItem3.Index = 0
    Me.MenuItem3.Text = "Close"
    '
    'MenuItem2
    '
    Me.MenuItem2.Index = 1
    Me.MenuItem2.Text = "Help"
    '
    'Panel1
    '
    Me.Panel1.Controls.Add(Me.ComboBox2)
    Me.Panel1.Controls.Add(Me.Label2)
    Me.Panel1.Controls.Add(Me.ComboBox1)
    Me.Panel1.Controls.Add(Me.Label1)
    Me.Panel1.Controls.Add(Me.chkOverwritePGDB)
    Me.Panel1.Controls.Add(Me.cmdExit)
    Me.Panel1.Controls.Add(Me.txtOutputName)
    Me.Panel1.Controls.Add(Me.lblOutputName)
    Me.Panel1.Controls.Add(Me.cmdTranslate)
    Me.Panel1.Controls.Add(Me.grpTranslationType)
    Me.Panel1.Controls.Add(Me.grpMultiTranslate)
    Me.Panel1.Controls.Add(Me.lblOutputLocation)
    Me.Panel1.Controls.Add(Me.lblLogFile)
    Me.Panel1.Controls.Add(Me.txtLogFile)
    Me.Panel1.Controls.Add(Me.cmdBrowseLogFile)
    Me.Panel1.Controls.Add(Me.cmdBrowseOutput)
    Me.Panel1.Controls.Add(Me.txtOutputLocation)
    Me.Panel1.Controls.Add(Me.lblInput)
    Me.Panel1.Controls.Add(Me.txtInput)
    Me.Panel1.Controls.Add(Me.cmdBrowseInput)
    Me.Panel1.Location = New System.Drawing.Point(8, 16)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(576, 328)
    Me.Panel1.TabIndex = 11
    '
    'ComboBox2
    '
    Me.ComboBox2.Items.AddRange(New Object() {"PGDB (*.mdb)"})
    Me.ComboBox2.Location = New System.Drawing.Point(344, 72)
    Me.ComboBox2.Name = "ComboBox2"
    Me.ComboBox2.Size = New System.Drawing.Size(112, 21)
    Me.ComboBox2.TabIndex = 30
    Me.ComboBox2.Text = "PGDB (*.mdb)"
    '
    'Label2
    '
    Me.Label2.Location = New System.Drawing.Point(264, 72)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(72, 16)
    Me.Label2.TabIndex = 29
    Me.Label2.Text = "To Format:"
    '
    'ComboBox1
    '
    Me.ComboBox1.Items.AddRange(New Object() {"MapInfo (*.tab)"})
    Me.ComboBox1.Location = New System.Drawing.Point(128, 72)
    Me.ComboBox1.Name = "ComboBox1"
    Me.ComboBox1.Size = New System.Drawing.Size(121, 21)
    Me.ComboBox1.TabIndex = 28
    Me.ComboBox1.Text = "MapInfo (*.tab)"
    '
    'Label1
    '
    Me.Label1.Location = New System.Drawing.Point(8, 72)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(80, 24)
    Me.Label1.TabIndex = 27
    Me.Label1.Text = "From Format:"
    '
    'chkOverwritePGDB
    '
    Me.chkOverwritePGDB.Location = New System.Drawing.Point(296, 224)
    Me.chkOverwritePGDB.Name = "chkOverwritePGDB"
    Me.chkOverwritePGDB.Size = New System.Drawing.Size(160, 24)
    Me.chkOverwritePGDB.TabIndex = 13
    Me.chkOverwritePGDB.Text = "Delete Existing PGDB?"
    '
    'cmdExit
    '
    Me.cmdExit.Location = New System.Drawing.Point(288, 256)
    Me.cmdExit.Name = "cmdExit"
    Me.cmdExit.Size = New System.Drawing.Size(104, 24)
    Me.cmdExit.TabIndex = 15
    Me.cmdExit.Text = "Exit"
    '
    'txtOutputName
    '
    Me.txtOutputName.Location = New System.Drawing.Point(128, 224)
    Me.txtOutputName.Name = "txtOutputName"
    Me.txtOutputName.Size = New System.Drawing.Size(144, 20)
    Me.txtOutputName.TabIndex = 12
    '
    'lblOutputName
    '
    Me.lblOutputName.Location = New System.Drawing.Point(8, 224)
    Me.lblOutputName.Name = "lblOutputName"
    Me.lblOutputName.Size = New System.Drawing.Size(88, 24)
    Me.lblOutputName.TabIndex = 26
    Me.lblOutputName.Text = "PGDB Name:"
    '
    'cmdTranslate
    '
    Me.cmdTranslate.Location = New System.Drawing.Point(128, 256)
    Me.cmdTranslate.Name = "cmdTranslate"
    Me.cmdTranslate.Size = New System.Drawing.Size(152, 24)
    Me.cmdTranslate.TabIndex = 14
    Me.cmdTranslate.Text = "Translate!"
    '
    'grpTranslationType
    '
    Me.grpTranslationType.Controls.Add(Me.rdbTypePolygon)
    Me.grpTranslationType.Controls.Add(Me.rdbTypeLine)
    Me.grpTranslationType.Controls.Add(Me.rdbTypePoint)
    Me.grpTranslationType.Controls.Add(Me.rdbTypeAuto)
    Me.grpTranslationType.Location = New System.Drawing.Point(128, 136)
    Me.grpTranslationType.Name = "grpTranslationType"
    Me.grpTranslationType.Size = New System.Drawing.Size(408, 48)
    Me.grpTranslationType.TabIndex = 24
    Me.grpTranslationType.TabStop = False
    Me.grpTranslationType.Text = "I am interested in feature classes of type..."
    '
    'rdbTypePolygon
    '
    Me.rdbTypePolygon.Location = New System.Drawing.Point(328, 16)
    Me.rdbTypePolygon.Name = "rdbTypePolygon"
    Me.rdbTypePolygon.Size = New System.Drawing.Size(64, 24)
    Me.rdbTypePolygon.TabIndex = 9
    Me.rdbTypePolygon.Text = "Polygon"
    '
    'rdbTypeLine
    '
    Me.rdbTypeLine.Location = New System.Drawing.Point(208, 16)
    Me.rdbTypeLine.Name = "rdbTypeLine"
    Me.rdbTypeLine.Size = New System.Drawing.Size(88, 24)
    Me.rdbTypeLine.TabIndex = 8
    Me.rdbTypeLine.Text = "Line/Polyline"
    '
    'rdbTypePoint
    '
    Me.rdbTypePoint.Location = New System.Drawing.Point(128, 16)
    Me.rdbTypePoint.Name = "rdbTypePoint"
    Me.rdbTypePoint.Size = New System.Drawing.Size(48, 24)
    Me.rdbTypePoint.TabIndex = 7
    Me.rdbTypePoint.Text = "Point"
    '
    'rdbTypeAuto
    '
    Me.rdbTypeAuto.Checked = True
    Me.rdbTypeAuto.Location = New System.Drawing.Point(8, 16)
    Me.rdbTypeAuto.Name = "rdbTypeAuto"
    Me.rdbTypeAuto.Size = New System.Drawing.Size(88, 24)
    Me.rdbTypeAuto.TabIndex = 6
    Me.rdbTypeAuto.TabStop = True
    Me.rdbTypeAuto.Text = "Auto Detect"
    '
    'grpMultiTranslate
    '
    Me.grpMultiTranslate.Controls.Add(Me.rdbWorkspace)
    Me.grpMultiTranslate.Controls.Add(Me.rdbDirectoryTree)
    Me.grpMultiTranslate.Controls.Add(Me.rdbOneDirectory)
    Me.grpMultiTranslate.Controls.Add(Me.rdbOneFile)
    Me.grpMultiTranslate.Location = New System.Drawing.Point(128, 8)
    Me.grpMultiTranslate.Name = "grpMultiTranslate"
    Me.grpMultiTranslate.Size = New System.Drawing.Size(408, 48)
    Me.grpMultiTranslate.TabIndex = 23
    Me.grpMultiTranslate.TabStop = False
    Me.grpMultiTranslate.Text = "I want to translate..."
    '
    'rdbWorkspace
    '
    Me.rdbWorkspace.Enabled = False
    Me.rdbWorkspace.Location = New System.Drawing.Point(296, 16)
    Me.rdbWorkspace.Name = "rdbWorkspace"
    Me.rdbWorkspace.Size = New System.Drawing.Size(104, 24)
    Me.rdbWorkspace.TabIndex = 3
    Me.rdbWorkspace.Text = "One Workspace"
    '
    'rdbDirectoryTree
    '
    Me.rdbDirectoryTree.Location = New System.Drawing.Point(192, 16)
    Me.rdbDirectoryTree.Name = "rdbDirectoryTree"
    Me.rdbDirectoryTree.Size = New System.Drawing.Size(96, 24)
    Me.rdbDirectoryTree.TabIndex = 2
    Me.rdbDirectoryTree.Text = "Directory Tree"
    '
    'rdbOneDirectory
    '
    Me.rdbOneDirectory.Location = New System.Drawing.Point(88, 16)
    Me.rdbOneDirectory.Name = "rdbOneDirectory"
    Me.rdbOneDirectory.Size = New System.Drawing.Size(96, 24)
    Me.rdbOneDirectory.TabIndex = 1
    Me.rdbOneDirectory.Text = "One Directory"
    '
    'rdbOneFile
    '
    Me.rdbOneFile.Checked = True
    Me.rdbOneFile.Location = New System.Drawing.Point(8, 16)
    Me.rdbOneFile.Name = "rdbOneFile"
    Me.rdbOneFile.Size = New System.Drawing.Size(72, 24)
    Me.rdbOneFile.TabIndex = 0
    Me.rdbOneFile.TabStop = True
    Me.rdbOneFile.Text = "One File"
    '
    'lblOutputLocation
    '
    Me.lblOutputLocation.Location = New System.Drawing.Point(8, 192)
    Me.lblOutputLocation.Name = "lblOutputLocation"
    Me.lblOutputLocation.Size = New System.Drawing.Size(120, 24)
    Me.lblOutputLocation.TabIndex = 22
    Me.lblOutputLocation.Text = "Output Location:"
    '
    'lblLogFile
    '
    Me.lblLogFile.Location = New System.Drawing.Point(8, 296)
    Me.lblLogFile.Name = "lblLogFile"
    Me.lblLogFile.Size = New System.Drawing.Size(120, 23)
    Me.lblLogFile.TabIndex = 21
    Me.lblLogFile.Text = "Log File:"
    '
    'txtLogFile
    '
    Me.txtLogFile.Enabled = False
    Me.txtLogFile.Location = New System.Drawing.Point(128, 296)
    Me.txtLogFile.Name = "txtLogFile"
    Me.txtLogFile.Size = New System.Drawing.Size(336, 20)
    Me.txtLogFile.TabIndex = 16
    Me.txtLogFile.Text = "c:\GISTranslator\log_10.2.txt"
    '
    'cmdBrowseLogFile
    '
    Me.cmdBrowseLogFile.Enabled = False
    Me.cmdBrowseLogFile.Location = New System.Drawing.Point(472, 296)
    Me.cmdBrowseLogFile.Name = "cmdBrowseLogFile"
    Me.cmdBrowseLogFile.Size = New System.Drawing.Size(75, 23)
    Me.cmdBrowseLogFile.TabIndex = 17
    Me.cmdBrowseLogFile.Text = "Browse..."
    '
    'cmdBrowseOutput
    '
    Me.cmdBrowseOutput.Location = New System.Drawing.Point(472, 192)
    Me.cmdBrowseOutput.Name = "cmdBrowseOutput"
    Me.cmdBrowseOutput.Size = New System.Drawing.Size(75, 23)
    Me.cmdBrowseOutput.TabIndex = 11
    Me.cmdBrowseOutput.Text = "Browse..."
    '
    'txtOutputLocation
    '
    Me.txtOutputLocation.Location = New System.Drawing.Point(128, 192)
    Me.txtOutputLocation.Name = "txtOutputLocation"
    Me.txtOutputLocation.Size = New System.Drawing.Size(336, 20)
    Me.txtOutputLocation.TabIndex = 10
    '
    'lblInput
    '
    Me.lblInput.Location = New System.Drawing.Point(8, 104)
    Me.lblInput.Name = "lblInput"
    Me.lblInput.Size = New System.Drawing.Size(120, 23)
    Me.lblInput.TabIndex = 14
    Me.lblInput.Text = "Input File or Directory:"
    '
    'txtInput
    '
    Me.txtInput.Location = New System.Drawing.Point(128, 104)
    Me.txtInput.Name = "txtInput"
    Me.txtInput.Size = New System.Drawing.Size(336, 20)
    Me.txtInput.TabIndex = 4
    '
    'cmdBrowseInput
    '
    Me.cmdBrowseInput.Location = New System.Drawing.Point(472, 104)
    Me.cmdBrowseInput.Name = "cmdBrowseInput"
    Me.cmdBrowseInput.Size = New System.Drawing.Size(75, 23)
    Me.cmdBrowseInput.TabIndex = 5
    Me.cmdBrowseInput.Text = "Browse..."
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Location = New System.Drawing.Point(8, 344)
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(576, 23)
    Me.ProgressBar1.TabIndex = 12
    Me.ProgressBar1.Visible = False
    '
    'Form1
    '
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.ClientSize = New System.Drawing.Size(592, 369)
    Me.Controls.Add(Me.ProgressBar1)
    Me.Controls.Add(Me.Panel1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Menu = Me.MainMenu1
    Me.Name = "Form1"
    Me.Text = "GIS Translator - ArcGIS v. 10.2"
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.grpTranslationType.ResumeLayout(False)
    Me.grpMultiTranslate.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

#End Region
  Dim translator As GISTranslator
  Dim log As translationLog

  Private Sub MenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem3.Click
    Close()
  End Sub

  Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
    log = New translationLog(txtLogFile.Text)
    translator = New GISTranslator
    log.writeLog("*******************************************************************")
    log.writeLog("GISTranslator accessed by: '" & Environment.UserDomainName() & "\" & _
        Environment.UserName() & "' at: " & DateString() & " " & TimeString())
  End Sub

  Private Sub cmdBrowseInput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseInput.Click
    If rdbOneFile.Checked Then
      OpenFileDialog1.Title = "Browse to the file to be translated"
      OpenFileDialog1.Filter = "MapInfo file (*.tab)|*.tab"
      OpenFileDialog1.ShowDialog()
      txtInput.Text = OpenFileDialog1.FileName()
      txtOutputName.Text = FileUtils.removeExtension(FileUtils.extractFileName(txtInput.Text))
    ElseIf rdbWorkspace.Checked Then
      OpenFileDialog1.Title = "Browse to the workspace to be translated"
      OpenFileDialog1.Filter = "MapInfo workspace (*.wor)|*.wor"
      OpenFileDialog1.ShowDialog()
      txtInput.Text = OpenFileDialog1.FileName()
      txtOutputName.Text = FileUtils.removeExtension(FileUtils.extractFileName(txtInput.Text))
    Else
      FolderBrowserDialog1.Description = "Browse to the directory to be translated"
      FolderBrowserDialog1.ShowDialog()
      txtInput.Text = FolderBrowserDialog1.SelectedPath
    End If
  End Sub

  Private Sub cmdBrowseOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseOutput.Click
    FolderBrowserDialog1.Description = "Browse to the path of the output PGDB"
    FolderBrowserDialog1.ShowDialog()
    txtOutputLocation.Text = FolderBrowserDialog1.SelectedPath
  End Sub

  Private Sub cmdBrowseLogFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseLogFile.Click
    SaveFileDialog1.Title = "Select the location of the log file"
    SaveFileDialog1.ShowDialog()
    txtLogFile.Text = SaveFileDialog1.FileName
  End Sub

  Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
    Close()
  End Sub

  Private Sub cmdTranslate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTranslate.Click
    Dim blTranslateDirectory As Boolean
    Dim blRecurseDirectory As Boolean
    Dim blOverwritePGDB As Boolean
    Dim blTranslateWorkspace As Boolean
    log = New translationLog(txtLogFile.Text)

    ProgressBar1.Visible = True
    ProgressBar1.Maximum = 100
    ProgressBar1.Step = 20

    If rdbOneFile.Checked Or rdbWorkspace.Checked Then
      blTranslateDirectory = False
    Else
      blTranslateDirectory = True
    End If
    If rdbWorkspace.Checked Then
      blTranslateWorkspace = True
    End If
    If rdbDirectoryTree.Checked Then
      blRecurseDirectory = True
    Else
      blRecurseDirectory = False
    End If
    blOverwritePGDB = chkOverwritePGDB.Checked

    Dim inputFile As String
    Dim outputDir As String
    Dim outputPGDB As String
    Dim returnCode As Integer

    outputDir = txtOutputLocation.Text
    outputPGDB = txtOutputName.Text
    If Microsoft.VisualBasic.Right(outputPGDB, 4) <> ".mdb" Then
      outputPGDB = outputPGDB & ".mdb"
    End If
    Dim lockFI As System.IO.FileInfo
    lockFI = New System.IO.FileInfo(outputDir & "\" & FileUtils.removeExtension(outputPGDB) & ".ldb")
    If lockFI.Exists Then
      Try
        lockFI.Delete()
      Catch
        MsgBox("Output PGDB is locked. Please resolve lock (verify no other programs are accessing this PGDB) and re-try. It may be necesary to restart GISTranslator.")
        Exit Sub
      End Try
    End If

    inputFile = txtInput.Text
    If Not blTranslateDirectory And Not blTranslateWorkspace Then
      ProgressBar1.Visible = True
      ProgressBar1.Maximum = 100
      ProgressBar1.Step = 20
      log.writeLog("Attempting translation: '" & inputFile & "' to '" & outputPGDB & "' located at '" & outputDir & "'.")
      returnCode = TranslateOneFile(inputFile, outputDir, outputPGDB, blOverwritePGDB)
      If returnCode = 1 Then
        log.writeLog("Translation Successful.")
        MsgBox("Translation Successful!")
      End If
    ElseIf blTranslateWorkspace Then
      'Dim fis() As System.IO.FileInfo
      'fis = tabFromWorkspace(inputFile)
      'For Each fi As System.IO.FileInfo In fis
      MsgBox("Not implemented yet")
      'Next
    ElseIf blTranslateDirectory Then
      If Not FileUtils.directoryExists(inputFile) Then
        MsgBox("Input path must be a directory to use Directory Translation mode")
      End If
      Dim intSuccess As Integer
      Dim di As System.IO.DirectoryInfo
      di = New System.IO.DirectoryInfo(inputFile)
      Dim fis() As System.IO.FileInfo
      fis = getFiles(di, "*.tab", blRecurseDirectory)
      Dim i As Integer = 1
      If MsgBox("Found " & fis.Length & " files to translate. Begin translation?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        log.writeLog("Begin batch translation: '" & di.FullName & ".")
        For Each fi As System.IO.FileInfo In fis
          log.writeLog("Attempting translation " & i & " of " & fis.Length & _
              ": '" & fi.FullName & "' to '" & outputPGDB & "' located at '" & outputDir & "'.")
          If i > 1 Then blOverwritePGDB = False
          returnCode = TranslateOneFile(fi.FullName, outputDir, outputPGDB, blOverwritePGDB)
          If returnCode = 1 Then intSuccess = intSuccess + 1
          i = i + 1
        Next
        MsgBox("Translated " & intSuccess & " of " & fis.Length & " files succesfully.  See log file for failures.")
        log.writeLog("Translated " & intSuccess & " of " & fis.Length & " files succesfully.")
      End If
    End If

    log = Nothing
    ProgressBar1.Visible = False
  End Sub

  Private Function TranslateOneFile(ByVal inputFile As String, ByVal outputDir As String, ByVal outputPGDB As String, Optional ByVal overwritePGDB As Boolean = True) As Integer
    Dim outputType As OutputTypes
    Dim returnCode As Integer
    Dim tmpDir As String
    Dim tmpShapefile As String
    Dim tableName As String
    log = New translationLog(txtLogFile.Text)
    On Error GoTo errHandler
    If Not FileUtils.fileExists(inputFile) Then
      MsgBox("Could not find input file: " & inputFile)
      log.writeLog("Could not find input file: " & inputFile)
      TranslateOneFile = -1
      Exit Function
    End If
    If Not FileUtils.directoryExists(outputDir) Then
      MsgBox("Could not find output directory: " & outputDir)
      log.writeLog("Could not find output directory: " & outputDir)
      TranslateOneFile = -1
      Exit Function
    End If

    tmpDir = Environment.GetEnvironmentVariable("TEMP") & "\IMUT\"
    Dim di As System.IO.DirectoryInfo
    di = New System.IO.DirectoryInfo(tmpDir)
    If Not di.Exists() Then
      di.Create()
    End If
    For Each fi As System.IO.FileInfo In di.GetFiles()
      If fi.Exists() Then
        On Error Resume Next
        fi.Delete()
        On Error GoTo errHandler
      End If
    Next

    tableName = FileUtils.removeExtension(FileUtils.extractFileName(inputFile))
    tmpShapefile = tmpDir & tableName & ".shp"
    If rdbTypeAuto.Checked Then outputType = OutputTypes.Automatic
    If rdbTypePoint.Checked Then outputType = OutputTypes.Point
    If rdbTypeLine.Checked Then outputType = OutputTypes.Polyline
    If rdbTypePolygon.Checked Then outputType = OutputTypes.Polygon
    ProgressBar1.PerformStep()
    returnCode = translator.callIMUT(inputFile, tmpDir, TranslationTypes.TABtoSHAPE, outputType)
    If returnCode = -5 And outputType <> OutputTypes.Automatic Then      
      log.writeLog("No objects of selected geometry '" & outputType.ToString & "' were found")
      TranslateOneFile = -1
      Exit Function
    End If
    If returnCode = -7 Then
      log.writeLog("Auto-Translation failed. '" & inputFile & "' probably contains no geographics objects")
      TranslateOneFile = -1
      Exit Function
    End If
    If returnCode <> 1 Then      
      log.writeLog("CallIMUT failed with error code: " & returnCode)
      TranslateOneFile = -1
      Exit Function
    End If
    ProgressBar1.PerformStep()
    returnCode = translator.shapeToPGDB(tmpShapefile, outputDir & "\" & outputPGDB, _
        tableName, overwritePGDB, inputFile, txtLogFile.Text)
    If returnCode = -2 Then
      log.writeLog("Convert to PGDB failed due to invalid geometry - Likely the source MI file had no .obj.")
      TranslateOneFile = -1
      Exit Function
    ElseIf returnCode <> 1 Then      
      log.writeLog("shapeToPGDB failed with error code: " & returnCode)
      TranslateOneFile = -1
      Exit Function
    End If

    translator.Dispose()
    translator = Nothing
    GC.Collect()
    translator = New GISTranslator

    ProgressBar1.PerformStep()
    'returnCode = translator.renamePGDBFromTAB(inputFile, outputDir & "\" & outputPGDB, tableName)
    'If returnCode <> 1 Then
    '    MsgBox("renamePGDBFromTAB failed with error code: " & returnCode)
    '    log.writeLog("renamePGDBFromTAB failed with error code: " & returnCode)
    '    TranslateOneFile = -1
    '    Exit Function
    'End If
    ProgressBar1.PerformStep()
    returnCode = translator.reprojectPGDB(outputDir & "\" & outputPGDB)
    If returnCode <> 1 Then      
      log.writeLog("reprojectPGDB failed with error code: " & returnCode)
      TranslateOneFile = -1
      Exit Function
    End If
    ProgressBar1.PerformStep()
    TranslateOneFile = 1
    Exit Function

errHandler:
    'MsgBox("Unhandled Error: " & Err.Description)
    log.writeLog("Unhandled Error: " & Err.Description)
    log.writeLog("Details: " & Err.GetException.StackTrace)
    log.writeLog("Attempting to continue (AKA blindly marching forward until we bump into a wall)...")
    TranslateOneFile = -1
    Exit Function
  End Function

  Private Function getFiles(ByVal di As System.IO.DirectoryInfo, ByVal strFilter As String, ByVal blRecurse As Boolean) As System.IO.FileInfo()
    Dim intFoundFiles As Integer
    If Not blRecurse Then
      getFiles = di.GetFiles(strFilter)
    Else
      Dim subDirs As System.IO.FileSystemInfo()
      subDirs = di.GetDirectories()
      For Each subDir As System.IO.DirectoryInfo In subDirs
        Dim fsi() As System.IO.FileSystemInfo
        fsi = subDir.GetFileSystemInfos(strFilter)
        For Each fs As System.IO.FileSystemInfo In fsi
          intFoundFiles = intFoundFiles + 1
        Next
      Next

      Dim foundFiles() As System.IO.FileInfo
      foundFiles = New System.IO.FileInfo(intFoundFiles - 1 + getFiles(di, strFilter, False).Length) {}
      intFoundFiles = 0
      For Each subDir As System.IO.DirectoryInfo In subDirs
        Dim fsi() As System.IO.FileSystemInfo
        fsi = subDir.GetFileSystemInfos(strFilter)
        For Each fs As System.IO.FileSystemInfo In fsi
          foundFiles(intFoundFiles) = fs
          intFoundFiles = intFoundFiles + 1
        Next
      Next
      For Each fs As System.IO.FileSystemInfo In getFiles(di, strFilter, False)
        foundFiles(intFoundFiles) = fs
        intFoundFiles = intFoundFiles + 1
      Next
      getFiles = foundFiles
    End If

  End Function
End Class

Public Class translationLog
  'Inherits dotnetutils.LogFile

  Const strDefaultLogPath = "c:\GISTranslator\log_10.2.txt"
  Dim fi As System.IO.FileInfo
  Dim strMyLogPath As String
  Dim intFileID As Integer
  Public Sub New(Optional ByVal strLogPath As String = strDefaultLogPath)
    strMyLogPath = strLogPath
    fi = New System.IO.FileInfo(strMyLogPath)
    If Not fi.Exists Then fi.Create()
    fi = Nothing
  End Sub
  Public Sub writeLog(ByVal strLogText As String)
    intFileID = FreeFile()
    FileOpen(intFileID, strMyLogPath, OpenMode.Append, OpenAccess.Default, OpenShare.Shared)
    PrintLine(intFileID, strLogText)
    FileClose(intFileID)
  End Sub
  Protected Sub dispose()
    FileClose(intFileID)
    MyBase.Finalize()
  End Sub
End Class