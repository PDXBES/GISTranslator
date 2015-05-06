Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ADODB
Imports dotnetutils
Imports ArcObjectsUtils
Imports System.Runtime.InteropServices
Imports System.IO
Imports System
Imports Access

'Imports ADOX

Imports System.Reflection

Public Enum TranslationTypes
  TABtoSHAPE
  SHAPEtoTAB
  TABtoE00
  E00toTAB
End Enum
Public Enum OutputTypes
  Automatic
  Point
  Polyline
  Polygon
  None
End Enum

<ComClass(GISTranslator.ClassId, GISTranslator.InterfaceId, GISTranslator.EventsId), ComVisible(True)> _
Public Class GISTranslator
  Inherits System.ComponentModel.Component

  Public Const ClassId As String = "44258487-4869-4b43-AAEE-59214DD50A45"
  Public Const InterfaceId As String = "BE785722-DF3B-4c95-ADF2-91EA411F0F6C"
  Public Const EventsId As String = "B791D528-5A60-41d7-85E8-1DB34CB5BD5E"


#Region " Component Designer generated code "

  Public Sub New(ByVal Container As System.ComponentModel.IContainer)
    MyClass.New()
    'Required for Windows.Forms Class Composition Designer support
    Container.Add(Me)
  End Sub

  Public Sub New()
    MyBase.New()
    'This call is required by the Component Designer.
    InitializeComponent()

    'Add any initialization after the InitializeComponent() call

  End Sub

  'Component overrides dispose to clean up the component list.
  Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing Then
      If Not (components Is Nothing) Then
        components.Dispose()
      End If
    End If
    MyBase.Dispose(disposing)
  End Sub

  'Required by the Component Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Component Designer
  'It can be modified using the Component Designer.
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    components = New System.ComponentModel.Container
  End Sub

#End Region

  Const strFMEFile1 = "FMEFile1.fme"
  Const strFMEFile2 = "FMEFile2.fme"
  Const strFMELogFile = "imutlog.txt"
  'Const IMUTPath = "C:\Program Files\MapInfo\Professional\UT\IMUT.EXE"
  Const strDefaultIMUTPath = "c:\GISTranslator\imut\IMUT.exe"

  'Calls IMUT.exe (the MapInfo Universal Translator
  'Input:
  '   strInputFile: Full path/file name of the file to be translated. Must be .tab, .shp, or .e00
  '   strOutputDirectory: Path of the directory to contain the translated files
  '   translationType: Enumerated integer specifying the type of translation to occur.  Can be used by calling the 
  '       enumeration class (TranslationTypes) or by passing an integer value defined as:
  '           1: .tab to .shp
  '           2: .shp to .tab
  '           3: .tab to .e00
  '           4" .e00 to .tab
  '   strIMUTPath: Optional parameter specifying the path to IMfileUtils.exe. Generally do not need to provide this for
  '       a default MapInfo install.  Default path is: "C:\Program Files\MapInfo\Professional\UT\IMfileUtils.EXE"
  'Output: Integer value specifying return status of function
  '           -1: Unhandled error
  '           -2: Input file not found
  '           -3: Output directory not found
  '           -4: IMfileUtils.exe not found
  '           -5: No translated files were found with the expected name
  '           -6: IMfileUtils.exe experienced unknown error - Check $TEMPDIR/imutlog.txt for details
  '            1: Translation executed correctly. Note: Depending on the behavior of IMfileUtils.exe,
  '               a return code of 0 does not guarantee the translation succeded. Calling program
  '               should verify existence of created file.
  Public Function callIMUT(ByVal strInputFile As String, ByVal strOutputDirectory As String, ByVal translationType As TranslationTypes, _
  Optional ByVal outputType As OutputTypes = OutputTypes.Automatic, Optional ByVal strIMUTPath As String = strDefaultIMUTPath) As Integer

    Dim strTmpDir As String         'Environment variable of system temp path
    Dim intIMUTStatus As Integer    'Return code from imfileUtils.exe
    Dim strFMEExtension As String   'Used to write extensions to custom FME file
    Dim strFMECommand As String     'Contains arguments passed to imfileUtils.exe
    Dim intFileID As Integer        'ID of custom FME file for writing
    Dim strCheckFile As String      'Used to check existence of i/o files

    On Error GoTo ErrHandler

    strTmpDir = Environment.GetEnvironmentVariable("TEMP") & "\"
    If File.Exists(strTmpDir & strFMEFile1) Then
      Kill(strTmpDir & strFMEFile1)
    End If
    If File.Exists(strTmpDir & strFMEFile2) Then
      Kill(strTmpDir & strFMEFile2)
    End If
    If File.Exists(strTmpDir & strFMELogFile) Then
      Kill(strTmpDir & strFMELogFile)
    End If

    If Right(strOutputDirectory, 1) <> "\" Then
      strOutputDirectory = strOutputDirectory & "\"
    End If

    If Not File.Exists(strInputFile) Then
      callIMUT = -2  'Input file not found error
      Throw New System.Exception("Exception: Input file not found")
      Exit Function
    End If
    If Not Directory.Exists(strOutputDirectory) Then
      callIMUT = -3  'Output dir not found error
      Throw New System.Exception("Exception: Output directory not found")
      Exit Function
    End If
    If Not File.Exists(strIMUTPath) Then
      callIMUT = -4 'IMUT not found error
      Throw New System.Exception("Exception: IMUT.exe not found")
      Exit Function
    End If

    'Create a standard FM Config file using the "CFGenerate" flag of IMUT.exe
    'This file will be located in the the system temporary directory
    strFMECommand = strIMUTPath & " CFGenerate MAPINFO SHAPE " & _
        Chr(34) & IO.Path.GetDirectoryName(strInputFile) & "/" & Chr(34) & _
        " " & Chr(34) & strTmpDir & strFMEFile1 & Chr(34) & _
        " LOG_STANDARDOUT YES LOG_FILENAME " & Chr(34) & _
        strTmpDir & strFMELogFile & Chr(34) & " LOG_APPEND " & Chr(34) & "NO" & Chr(34)

    intIMUTStatus = Shell(strFMECommand, AppWinStyle.Hide, True)

    'Now create a custom FM Config by setting the relevation extensions and then including
    'the standard FM Config file created above
    intFileID = FreeFile()
    On Error Resume Next
    FileOpen(intFileID, strTmpDir & strFMEFile2, OpenMode.Append)
    'On Error GoTo ErrHandler

    'strFMEExtension = "MACRO SourceDataset " & Chr(34) & FileUtils.extractDir(strInputFile) & "/" & Chr(34)
    strFMEExtension = "MACRO SourceDataset " & IO.Path.GetDirectoryName(strInputFile) & "/"
    PrintLine(intFileID, strFMEExtension)
    'strFMEExtension = "MACRO DestDataset " & Chr(34) & Left(strOutputDirectory, Len(strOutputDirectory) - 1) & Chr(34)
    strFMEExtension = "MACRO DestDataset " & Left(strOutputDirectory, Len(strOutputDirectory) - 1)
    PrintLine(intFileID, strFMEExtension)
    strFMEExtension = "LOG_STANDARDOUT YES"
    PrintLine(intFileID, strFMEExtension)
    strFMEExtension = "LOG_FILENAME " & Chr(34) & strTmpDir & strFMELogFile & Chr(34)
    PrintLine(intFileID, strFMEExtension)
    strFMEExtension = "LOG_APPEND " & Chr(34) & "YES" & Chr(34)
    PrintLine(intFileID, strFMEExtension)
    strFMEExtension = "INCLUDE " & strFMEFile1
    PrintLine(intFileID, strFMEExtension)
    strFMEExtension = "MAPINFO_IDs " & Chr(34) & IO.Path.GetFileNameWithoutExtension(strInputFile) & Chr(34)
    PrintLine(intFileID, strFMEExtension)
    strFMEExtension = "MAPINFO_IN_IDs " & Chr(34) & IO.Path.GetFileNameWithoutExtension(strInputFile) & Chr(34)
    PrintLine(intFileID, strFMEExtension)

    FileClose(intFileID)

    strFMECommand = strIMUTPath & " " & strTmpDir & strFMEFile2


    intIMUTStatus = Shell(strFMECommand, AppWinStyle.Hide, True)
    If checkIMUTStatus(strTmpDir & strFMELogFile) <> 1 Then
      callIMUT = -6      
      Exit Function
    End If

    'Deletes all files created by IMUT that are not of specified type and renames remaining files
    'to remove the type designation ("point", "polygon", etc)
    Dim filesFound As Integer
    Select Case outputType
      Case OutputTypes.Automatic        
        filesFound = cleanupOutput(strOutputDirectory, IO.Path.GetFileNameWithoutExtension(strInputFile), "automatic")
        If filesFound = 0 Then
          callIMUT = -7 'Autotranslate did not find valid .shp file
        Else
          callIMUT = 1
          File.Copy("c:\GISTranslator\bin\template.prj", strOutputDirectory & IO.Path.GetFileNameWithoutExtension(strInputFile) & ".prj", True)
        End If
        Exit Function
      Case OutputTypes.Point
        filesFound = cleanupOutput(strOutputDirectory, IO.Path.GetFileNameWithoutExtension(strInputFile), "point")
      Case OutputTypes.Polygon
        filesFound = cleanupOutput(strOutputDirectory, IO.Path.GetFileNameWithoutExtension(strInputFile), "region")
      Case OutputTypes.Polyline
        filesFound = cleanupOutput(strOutputDirectory, IO.Path.GetFileNameWithoutExtension(strInputFile), "polyline")
      Case OutputTypes.None
        filesFound = cleanupOutput(strOutputDirectory, IO.Path.GetFileNameWithoutExtension(strInputFile), "none")
    End Select
    'File.Delete(strOutputDirectory & FileUtils.removeExtension(FileUtils.extractFileName(strInputFile)) & ".prj")

    File.Copy("\\cassio\modeling\Model_Programs\GISTranslator\translator_10.1\bin\template.prj", strOutputDirectory & IO.Path.GetFileNameWithoutExtension(strInputFile) & ".prj", True)
    FileClose()
    If filesFound = 0 Then
      callIMUT = -5
      Exit Function
    End If
    callIMUT = 1
    Exit Function
ErrHandler:
    callIMUT = -1 'Unhandled error
    FileClose()

    'MsgBox(Err.Number & " " & Err.Description)
    Throw New System.Exception("CallIMUT Error: " & Err.Description & "." & Err.GetException.StackTrace, Err.GetException)
  End Function

  'Converts a shape file to a personal geodatabase
  'Inputs:
  '   strShapeFile
  '   strPGDBFile
  '   strTableName
  '   blOverwritePGDB: If true, then the .mdb file specified by strPGDBFile will be deleted (if it exists).
  '       If false, the shape file will be added to the .mdb (if it exists).
  '       If the .mdb does not already exist it will be created regardless of the value of blOverwritePGDB.
  'Outputs:
  '   Returns 1 if succesful
  '   Returns -1 if convertFeatureClass failed
  '   Returns -2 if input shp file has no or corrupt geometry (ie it came from a MapInfo file with no .obj)
  Public Function shapeToPGDB(ByVal strShapeFile As String, ByVal strPGDBFile As String, ByVal strTableName As String, ByVal blOverwritePGDB As Boolean, ByVal strTabFile As String, ByVal strLogFile As String) As Integer
    If File.Exists(strPGDBFile) And blOverwritePGDB Then
      File.Delete(strPGDBFile)
    End If

    Dim pPGDBWorkspaceFactory As IWorkspaceFactory
    pPGDBWorkspaceFactory = New AccessWorkspaceFactoryClass

    Dim pPGDBWorkspaceName As IWorkspaceName

    Directory.SetCurrentDirectory("C:\GISTranslator\bin")
    If Not File.Exists(strPGDBFile) Then
      File.Copy("template_10.1.mdb", strPGDBFile)
      'pPGDBWorkspaceName = pPGDBWorkspaceFactory.Create(IO.Path.GetDirectoryName(strPGDBFile), IO.Path.GetFileName(strPGDBFile), Nothing, 0)
    End If
    pPGDBWorkspaceName = New WorkspaceNameClass
    pPGDBWorkspaceName.PathName = strPGDBFile

    pPGDBWorkspaceName.WorkspaceFactoryProgID = "esriDataSourcesFile.AccessWorkspaceFactory.1"

    'If FCNameExists(pPGDBWorkspace, strTableName) Then  'Or TableNameExists(pPGDBWorkspace, strTableName)
    Dim pWS As IWorkspace
    Try
      pWS = pPGDBWorkspaceFactory.OpenFromFile(strPGDBFile, 0)
    Catch ex As Exception
      Dim thisErr As System.Exception
      thisErr = New Exception("Could not open PGDB: " & Err.GetException.Message, Err.GetException)
      Throw thisErr
    End Try

    Try
      If FeatureClassUtils.FCNameExists(pWS, strTableName) Then
        FeatureClassUtils.deleteFC(pWS, strTableName)
      End If
    Catch
      Dim thisErr As System.Exception
      Err.Description = "Could not overwrite existing FC: " & Err.Description
      thisErr = Err.GetException
      Throw thisErr
    End Try
    System.Runtime.InteropServices.Marshal.ReleaseComObject(pWS)

    Try
      ShpToFeatureDataset(strShapeFile, strPGDBFile, "", strTableName, strTabFile, strLogFile)
      shapeToPGDB = 1
      'pFDC.ConvertFeatureClass(pSHPFCName, Nothing, Nothing, pFCName, getGeomDef(pSHPFC), pSHPFC.Fields, "", 1000, 0)
    Catch
      pPGDBWorkspaceName = Nothing
      pPGDBWorkspaceFactory = Nothing
      shapeToPGDB = -1
      Dim thisErr As System.Exception
      Err.Description = "ConvertFeatureClass Error"
      thisErr = Err.GetException
      Throw thisErr
      'Exit Function
    End Try

    pPGDBWorkspaceFactory = Nothing
    pPGDBWorkspaceName = Nothing

    GC.Collect()
  End Function

  'Reprojects every feature class in a PGDB to Oregon State Plane North HARN 83
  Public Function reprojectPGDB(ByVal strPGDBFile As String) As Integer
    Dim pWSF As IWorkspaceFactory
    Dim pWS As IWorkspace
    Dim pDatasetEnum As IEnumDataset
    Dim pFC As IFeatureClass

    Dim pSpatialRefFactory As ISpatialReferenceFactory
    Dim pProjection As IProjectedCoordinateSystem
    Dim pGeoDatasetEdit As IGeoDatasetSchemaEdit

    pWSF = New AccessWorkspaceFactoryClass
    pWS = pWSF.OpenFromFile(strPGDBFile, 0)

    pSpatialRefFactory = New SpatialReferenceEnvironment
    pProjection = pSpatialRefFactory.CreateProjectedCoordinateSystem( _
        esriSRProjCS4Type.esriSRProjCS_NAD1983HARN_StatePlane_Oregon_North_FIPS_3601_Feet_Intl)

    pDatasetEnum = pWS.Datasets(esriDatasetType.esriDTFeatureClass)
    Dim pSpatRef As ISpatialReference
    pSpatRef = pSpatialRefFactory.CreateESRISpatialReferenceFromPRJFile("c:\GISTranslator\bin\template.prj")
    pFC = pDatasetEnum.Next()
    Do Until pFC Is Nothing
      Dim pGeometryDef As IGeometryDef
      pGeoDatasetEdit = pFC

      If pGeoDatasetEdit.CanAlterSpatialReference Then
        pGeoDatasetEdit.AlterSpatialReference(pSpatRef)
      End If
      pFC = pDatasetEnum.Next()
    Loop
    reprojectPGDB = 1
    pWS = Nothing
  End Function

  'Returns an array of strings containing the field names of a MapInfo .tab file
  Public Function getMapInfoFieldNames(ByVal strMITable As String) As String()
    Dim fi As FileInfo = New FileInfo(strMITable)
    Dim stream As StreamReader
    Dim line As String
    Dim validMITable As Boolean
    Dim token() As String
    Dim i As Integer = 0
    If Not fi.Exists Then
      getMapInfoFieldNames = Nothing
      Exit Function
    End If
    stream = New StreamReader(strMITable)
    line = stream.ReadLine()
    If line = "!table" Then validMITable = True
    Do
      line = stream.ReadLine()
      If stream Is Nothing Then
        getMapInfoFieldNames = Nothing
        Exit Function
      End If
      token = line.TrimStart(" ").Split(" ")
    Loop Until token(0).CompareTo("Fields") = 0

    Dim fieldName() As String = New String(CInt(token(1)) - 1) {}

    line = stream.ReadLine()
    For i = 0 To fieldName.Length - 1
      fieldName(i) = line.TrimStart(" ").Split(" ")(0)
      If UCase(fieldName(i)) = "SHAPE" Then
        fieldName(i) = fieldName(i) & "_1"
      End If
      line = stream.ReadLine()
    Next i
    getMapInfoFieldNames = fieldName

  End Function

  Public Function translateModel(ByVal strModelRoot As String, _
  Optional ByVal strLogFile As String = "c:\GISTranslator\log_10.2.txt", _
  Optional ByVal blQuiet As Boolean = True) As Integer

    translateModel = 1
    Dim pgdbfile As String = strModelRoot & "\mxd\mdl_components.mdb"
    Dim log As LogFile = New LogFile(strLogFile, True, blQuiet)
    Dim blUseModel As Boolean
    log.writeLog("*********************************************************************")
    log.writeLog("EMGAATS TranslateModel: '" & Environment.UserDomainName() & "\" & _
        Environment.UserName() & "' at: " & DateString() & " " & TimeString())
    log.writeLog("Called as 'GISTranslator.translateModel(" & strModelRoot & "," & strLogFile & "," & blQuiet & ")'.")
    Try
      log.writeLog("Searching for translation lookup table [ArcGISModelTranslation] in lookuptables.mdb")
      If TableExists(strModelRoot & "\mdbs\LookupTables.mdb", "ArcGISModelTranslation") Then
        log.writeLog("Found [ArcGISModelTranslation] in model.")
      Else
        Dim inputAccess As New Access.Application
        inputAccess.OpenCurrentDatabase("\\cassio\modeling\Model_Programs\Emgaats\binV2.1\mdbs\LookupTables.mdb")
        Dim outputAccess As New Access.Application
        outputAccess.OpenCurrentDatabase(strModelRoot & "\mdbs\LookupTables.mdb")
        outputAccess.DoCmd.CopyObject("\\cassio\modeling\Model_Programs\Emgaats\binV2.1\mdbs\LookupTables.mdb", "ArcGISModelTranslation", AcObjectType.acTable, "ArcGISModelTranslation")
        log.writeLog("Did not find [ArcGISModelTranslation] in model... Copying from master.")
      End If
      'conn.BeginTrans()

    Catch ex As Exception
      log.writeLog("Unhandled Error finding/creating TranslationTable: " & Err.Description)
      translateModel = -1
      Exit Function
    End Try

    Try
      log.writeLog("Entering translation block")
      translateFromAccessTable(strModelRoot & "\mdbs\lookuptables.mdb", "ArcGISModelTranslation", pgdbfile, strLogFile)
    Catch ex As Exception
      log.writeLog("Unhandled Translation Failure: " & Err.Description)
      translateModel = -1
      Exit Function
    End Try

    Try
      Dim di As DirectoryInfo
      copyDirectory("\\cassio\modeling\model_programs\emgaats\bin\mxd\", strModelRoot & "\mxd")
    Catch ex As Exception
      log.writeLog("Error copying mxd directory structure")
    End Try
    log.writeLog("Function TranslateModel exited OK!")
    translateModel = 1

  End Function

  Public Function copyDirectory(ByVal Src As String, ByVal Dst As String)
    Dim Files() As String

    If Not Dst.EndsWith(IO.Path.DirectorySeparatorChar) Then
      Dst = Dst & IO.Path.DirectorySeparatorChar
    End If
    If Not Directory.Exists(Dst) Then
      Directory.CreateDirectory(Dst)
    End If
    Files = Directory.GetFileSystemEntries(Src)
    For Each Element As String In Files
      If (Directory.Exists(Element)) Then
        copyDirectory(Element, Dst + IO.Path.GetFileName(Element))
      Else
        File.Copy(Element, Dst + IO.Path.GetFileName(Element), True)
      End If
    Next
  End Function

  Function TableExists(ByVal strDatabase As String, ByVal strTableName As String) As Boolean

    Dim tbl As DAO.TableDef
    Dim MyDB As DAO.Database

    Dim myAccess As New Access.Application
    myAccess.OpenCurrentDatabase(strDatabase)
    MyDB = myAccess.CurrentDb

    For Each tbl In MyDB.TableDefs
      If tbl.Name = strTableName Then TableExists = True
    Next tbl

    MyDB = Nothing
    myAccess = Nothing

  End Function

  Function UpdateAllDoubleType(ByVal updateDB As String, ByVal updateTable As String, ByVal sourceMITable As String, ByVal key As String)

    If (Not File.Exists(sourceMITable)) Then Exit Function
    Dim sourceTable As String = IO.Path.GetFileNameWithoutExtension(sourceMITable)
    Dim sourceDB As String = FindAccessDBFromTab(sourceMITable)
    If (Not File.Exists(sourceDB)) Then Exit Function

    If (File.Exists(updateDB)) Then

      Dim acc As Access.Application = New Access.Application
      acc.OpenCurrentDatabase(updateDB)

      For Each tbl As DAO.TableDef In acc.CurrentDb.TableDefs
        If tbl.Name = updateTable Then
          For Each fld As DAO.Field In tbl.Fields
            If fld.Type = 0 Then
              UpdateDoubleType(updateDB, sourceDB, updateTable, sourceTable, fld.Name, fld.Name, key, key)
            End If
          Next
        End If
      Next
    End If


  End Function

  Function FindAccessDBFromTab(ByVal strMITable As String) As String
    Dim fi As FileInfo = New FileInfo(strMITable)
    Dim stream As StreamReader
    Dim line As String
    Dim validMITable As Boolean
    Dim token() As String
    Dim i As Integer = 0
    If Not fi.Exists Then
      FindAccessDBFromTab = Nothing
      Exit Function
    End If
    stream = New StreamReader(strMITable)
    line = stream.ReadLine()
    If line = "!table" Then validMITable = True
    Do
      line = stream.ReadLine()
      If stream Is Nothing Then
        FindAccessDBFromTab = Nothing
        Exit Function
      End If
      token = line.TrimStart(" ").Split(" ")
    Loop Until token(0).CompareTo("Type") = 0

    Dim accessFileName As String
    accessFileName = Mid(token(4), 2, Len(token(4) - 1))

    If File.Exists(accessFileName + ".mdb") Then
      Return accessFileName + ".mdb"
    Else
      Return Nothing
    End If

  End Function
  Function UpdateDoubleType(ByVal sUpdateDB As String, ByVal sSourceDB As String, _
      ByVal sUpdateTbl As String, ByVal sSourceTbl As String, _
      ByVal sUpdateFld As String, ByVal sSourceFld As String, _
      ByVal sUpdateKey As String, ByVal sSourceKey As String)

    Dim updateTbl As DAO.TableDef
    Dim sourceTbl As DAO.TableDef

    Dim updateDB As DAO.Database
    Dim sourceDB As DAO.Database

    Dim updateFld As DAO.Field
    Dim sourceFld As DAO.Field

    Dim updateKeyFld As DAO.Field
    Dim sourceKeyFld As DAO.Field

    Dim updateAccess As New Access.Application
    updateAccess.OpenCurrentDatabase(sUpdateDB)
    updateDB = updateAccess.CurrentDb

    For Each tbl As DAO.TableDef In updateDB.TableDefs
      If tbl.Name = sUpdateTbl Then updateTbl = tbl
    Next tbl
    For Each fld As DAO.Field In updateTbl.Fields
      If fld.Name = sUpdateFld Then updateFld = fld
      If fld.Name = sUpdateKey Then updateKeyFld = fld
    Next fld

    For Each tbl As DAO.TableDef In updateDB.TableDefs
      If tbl.Name = sSourceTbl Then sourceTbl = tbl
    Next tbl
    For Each fld As DAO.Field In sourceTbl.Fields
      If fld.Name = sSourceFld Then sourceFld = fld
      If fld.Name = sSourceKey Then sourceKeyFld = fld
    Next fld

    Dim sourceDict As System.Collections.Specialized.ListDictionary
    sourceDict = New System.Collections.Specialized.ListDictionary
    Dim srcRS As DAO.Recordset
    srcRS = sourceTbl.OpenRecordset

    srcRS.MoveFirst()
    While (Not srcRS.EOF)
      sourceDict.Add(srcRS(sSourceKey), srcRS(sSourceFld))
      srcRS.MoveFirst()
    End While

    Dim updateRS As DAO.Recordset
    updateRS = updateTbl.OpenRecordset
    updateRS.MoveFirst()
    While (Not updateRS.EOF)
      Dim flds As DAO.Fields
      flds = updateRS.Fields
      Dim fld As DAO.Field

      fld = flds(sourceFld.OrdinalPosition)
      updateRS.Edit()
      fld.Value = sourceDict(updateRS(sUpdateKey))
      'updateRS.FieldsUpdateFld) = sourceDict(updateRS(sUpdateKey))
      updateRS.Update()
    End While


    Dim qry As String

  End Function


  Public Function translateModelResults(ByVal strModelRoot As String, _
  Optional ByVal strLogFile As String = "c:\GISTranslator\log_10.2.txt", _
  Optional ByVal blQuiet As Boolean = True) As Integer

    translateModelResults = 1
    Dim pgdbfile As String = strModelRoot & "\mxd\mdl_results.mdb"
    Dim log As LogFile = New LogFile(strLogFile, True, blQuiet)
    Dim blUseModel As Boolean
    log.writeLog("*********************************************************************")
    log.writeLog("EMGAATS TranslateResults: '" & Environment.UserDomainName() & "\" & _
        Environment.UserName() & "' at: " & DateString() & " " & TimeString())
    log.writeLog("Called as 'GISTranslator.translateModel(" & strModelRoot & "," & strLogFile & "," & blQuiet & ")'.")
    Try
      log.writeLog("Searching for translation lookup table [ArcGISResultsTranslation] in lookuptables.mdb")
      If TableExists(strModelRoot & "\mdbs\LookupTables.mdb", "ArcGISResultsTranslation") Then
        log.writeLog("Found [ArcGISResultsTranslation] in model.")
      Else

        Dim inputAccess As New Access.Application
        inputAccess.OpenCurrentDatabase("\\cassio\modeling\Model_Programs\Emgaats\binV2.1\mdbs\LookupTables.mdb")
        Dim outputAccess As New Access.Application
        outputAccess.OpenCurrentDatabase(strModelRoot & "\mdbs\LookupTables.mdb")
        outputAccess.DoCmd.CopyObject("\\cassio\modeling\Model_Programs\Emgaats\binV2.1\mdbs\LookupTables.mdb", "ArcGISResultsTranslation", AcObjectType.acTable, "ArcGISResultsTranslation")
        log.writeLog("Did not find [ArcGISModelTranslation] in model... Copying from master.")
      End If
      'conn.BeginTrans()

    Catch ex As Exception
      log.writeLog("Unhandled Error finding/creating TranslationTable: " & Err.Description)
      translateModelResults = -1
      Exit Function
    End Try

    Try
      log.writeLog("Entering translation block")
      translateFromAccessTable(strModelRoot & "\mdbs\lookuptables.mdb", "ArcGISResultsTranslation", pgdbfile, strLogFile)
    Catch ex As Exception
      log.writeLog("Unhandled Translation Failure: " & Err.Description)
      translateModelResults = -1
      Exit Function
    End Try

    Try
      copyDirectory("\\cassio\modeling\model_programs\emgaats\bin\mxd\", strModelRoot & "\mxd")
    Catch ex As Exception
      log.writeLog("Error copying mxd directory structure")
    End Try
    log.writeLog("Function TranslateModelResults exited OK!")

  End Function

  Public Function translateFromAccessTable(ByVal sTranslationDB As String, ByVal sTable As String, ByVal sOutputDB As String, Optional ByVal sLogFile As String = "c:\GISTranslator\log_10.2.txt")
    Dim rs As ADODB.Recordset = New ADODB.Recordset
    Dim conn As ADODB.Connection = New ADODB.Connection
    Dim result As Integer
    Dim tmpDir = Environ("TEMP") & "\IMUT"
    Dim createdTmpDir As Boolean = False
    Dim log As LogFile = New LogFile(sLogFile)
    conn.Mode = ConnectModeEnum.adModeRead
    conn.Open("Provider='Microsoft.Jet.OLEDB.4.0';Data Source='" & sTranslationDB & "';")
    rs.ActiveConnection = conn
    rs.Open(sTable, conn, CursorTypeEnum.adOpenForwardOnly, LockTypeEnum.adLockReadOnly)
    rs.MoveFirst()
    If Not Directory.Exists(tmpDir) Then
      Directory.CreateDirectory(tmpDir)
      createdTmpDir = True
    End If
    Try
      File.Delete(sOutputDB)
    Catch ex As Exception
      log.writeLog(ex.Message)
      Exit Function
    End Try

    Do Until rs.EOF
      Try
        Dim tblName, dbName, pathName, renameFile As String
        Dim outputType As OutputTypes
        tblName = rs.Fields("inputTable").Value
        dbName = rs.Fields("inputDatabase").Value
        pathName = rs.Fields("tablePath").Value
        renameFile = rs.Fields("renameOutput").Value
        log.writeLog("  Attempting to translate '" & tblName & "'......", False)
        Dim shpFile As String
        shpFile = Environ("TEMP") & "\IMUT\" & tblName & ".shp"
        Dim tabFile, mdbFile As String
        If rs.Fields("relativePath").Value Then
          tabFile = IO.Path.GetDirectoryName(sTranslationDB) & "\" & pathName & "\" & tblName & ".tab"
          mdbFile = IO.Path.GetDirectoryName(sTranslationDB) & "\" & pathName & "\" & dbName & ".mdb"
        Else
          tabFile = pathName & "\" & tblName & ".tab"
          mdbFile = pathName & "\" & dbName & ".mdb"
        End If

        Select Case UCase(rs.Fields("featureType").Value)
          Case "POINT"
            outputType = OutputTypes.Point
          Case "POLYLINE", "LINE"
            outputType = OutputTypes.Polyline
          Case "POLYGON"
            outputType = OutputTypes.Polygon
          Case "DATA"
            outputType = OutputTypes.None
        End Select
        If File.Exists(tabFile) And outputType <> OutputTypes.None Then
          result = callIMUT(tabFile, tmpDir, TranslationTypes.SHAPEtoTAB, outputType)
          If result <> 1 Then
            Throw New Exception("CallIMUT: Failed to convert table '" & tblName & "'.")
          End If
          result = shapeToPGDB(shpFile, sOutputDB, renameFile, False, tabFile, sLogFile)
          If result <> 1 Then
            Throw New Exception("ShapeToPGDB: Failed to convert table '" & tblName & "'. Error code: " & result & ".")
          End If
        ElseIf TableExists(mdbFile, tblName) And outputType = OutputTypes.None Then

          Dim pPGDBWorkspaceFactory As IWorkspaceFactory
          pPGDBWorkspaceFactory = New AccessWorkspaceFactoryClass
          Dim pPGDBWorkspaceName As IWorkspaceName
          If Not File.Exists(sOutputDB) Then
            pPGDBWorkspaceName = pPGDBWorkspaceFactory.Create(IO.Path.GetDirectoryName(sOutputDB), IO.Path.GetFileName(sOutputDB), Nothing, 0)
          End If
          Dim inputAccess As New Access.Application
          inputAccess.OpenCurrentDatabase(mdbFile)
          Dim outputAccess As New Access.Application
          outputAccess.OpenCurrentDatabase(sOutputDB)
          outputAccess.DoCmd.CopyObject(mdbFile, renameFile, AcObjectType.acTable, tblName)
          'AccessUtils.copyTable(mdbFile, tblName, sOutputDB, renameFile)
        Else
          Throw New Exception("Could not find table '" & tblName & "' - Skipping to next table.")
        End If
        log.writeLog("OK!")

      Catch ex As Exception
        log.writeLog("FAIL!")
        log.writeLog(ex.Message)
        log.writeLog("  " & ex.ToString)
        Exit Try
      Finally
        rs.MoveNext()
      End Try
    Loop
    If createdTmpDir Then
      Directory.Delete(tmpDir, True)
    End If
    result = reprojectPGDB(sOutputDB)
    rs.Close()
    rs.ActiveConnection = Nothing
    conn.Close()
    conn = Nothing
  End Function

  'Returns the GeometryDef object of a Feature Class
  Private Function getGeomDef(ByVal pFC As IFeatureClass) As GeometryDef
    Dim lGeomIndex As Long
    Dim sShpName As String
    Dim pFields As IFields
    Dim pField As IField
    Dim pGeometryDef As IGeometryDef
    sShpName = pFC.ShapeFieldName
    pFields = pFC.Fields
    lGeomIndex = pFields.FindField(sShpName)
    pField = pFields.Field(lGeomIndex)
    pGeometryDef = pField.GeometryDef
    getGeomDef = pGeometryDef
  End Function

  'Deletes all files created by IMUT that are not of specified type and renames remaining files
  'to remove the type designation ("point", "polygon", etc)
  'Returns the number of files that were renamed
  Private Function cleanupOutput(ByVal strOutputDir As String, ByVal strTableName As String, ByVal strOutputType As String) As Integer
    'Write me :)
    '1. Find all files in outputdir containing "tablename" and not containing "outputtype" - Delete
    Dim deleteFiles(3) As String
    Dim renameFile As String
    Dim filesFound As String

    Select Case strOutputType
      Case "automatic"
        cleanupOutput = autoClean(strOutputDir, strTableName)
        Exit Function
      Case "point"
        deleteFiles(0) = "polyline"
        deleteFiles(1) = "region"
        deleteFiles(2) = "none"
        renameFile = "point"
      Case "polyline"
        deleteFiles(0) = "point"
        deleteFiles(1) = "region"
        deleteFiles(2) = "none"
        renameFile = "polyline"
      Case "region"
        deleteFiles(0) = "polyline"
        deleteFiles(1) = "point"
        deleteFiles(2) = "none"
        renameFile = "region"
      Case "none"
        deleteFiles(0) = "polyline"
        deleteFiles(1) = "region"
        deleteFiles(2) = "point"
        renameFile = "none"
    End Select

    Dim shpExtensions() As String = {".dbf", ".prj", ".shp", ".shx"}
    Dim del, ext As String
    Dim fi As FileInfo

    For Each del In deleteFiles
      For Each ext In shpExtensions
        fi = New FileInfo(strOutputDir & strTableName & "_" & del & ext)
        If fi.Exists Then
          Try
            fi.Delete()
          Catch
          End Try
        End If
      Next ext
    Next del

    '2. Find all files in outputdir containing "table" and containing "outputtype" - Rename to remove "outputtype"
    Dim ren As String
    For Each ext In shpExtensions
      fi = New FileInfo(strOutputDir & strTableName & ext)
      If fi.Exists Then fi.Delete()
      fi = New FileInfo(strOutputDir & strTableName & "_" & renameFile & ext)
      If fi.Exists Then
        fi.MoveTo(strOutputDir & strTableName & ext)
        filesFound = filesFound + 1
      End If

    Next
    cleanupOutput = filesFound
  End Function

  'Deletes all shapefiles generated by IMUT, excluding the shapefile containing the most features.
  'The shapefile containing the most feature is renamed to remove the [_featureType] extension
  Private Function autoClean(ByVal strOutputDir As String, ByVal strTableName As String) As Integer

    Try
      Dim pWSF As IWorkspaceFactory
      pWSF = New ShapefileWorkspaceFactory
      Dim pWS As IWorkspace
      pWS = pWSF.OpenFromFile(strOutputDir, 0)
      Dim pEDS As IEnumDataset
      pEDS = pWS.Datasets(esriDatasetType.esriDTFeatureClass)
      Dim pFC As IFeatureClass
      pFC = pEDS.Next
      Dim intMaxFCLen As Integer

      Dim pBestFC As IFeatureClass
      Do Until pFC Is Nothing
        Dim intThisFCLen As Integer
        autoClean = autoClean + 1
        intThisFCLen = pFC.FeatureCount(Nothing)
        If intThisFCLen > intMaxFCLen Then
          pBestFC = pFC
          intMaxFCLen = intThisFCLen
        End If
        pFC = pEDS.Next
      Loop
      pEDS.Reset()
      pFC = pEDS.Next
      Do Until pFC Is Nothing
        If pFC.AliasName <> pBestFC.AliasName Then
          Dim pDS As IDataset
          pDS = pFC
          pDS.Delete()
        Else
          Dim pDS As IDataset
          pDS = pBestFC
          pDS.Rename(strTableName)
        End If
        pFC = pEDS.Next
      Loop
      pWS = Nothing


    Catch ex As Exception
      Throw New Exception("Error in auto-detect: " & ex.Message & " " & ex.StackTrace, ex)
    End Try


  End Function

  'Searches the IMUT status file for the code "ERROR" and fails if it is found
  Private Function checkIMUTStatus(ByVal FMEFile As String)
    Dim intFreeFile As Integer
    intFreeFile = FreeFile()
    FileOpen(intFreeFile, FMEFile, OpenMode.Input, OpenAccess.Read, OpenShare.Shared)
    Dim strFMELine As String
    Input(intFreeFile, strFMELine)
    Do While Not EOF(intFreeFile)
      Try
        If strFMELine.Length > 37 Then
          If strFMELine.Substring(33, 5) = "ERROR" Then
            checkIMUTStatus = -1
            FileClose(intFreeFile)
            Exit Function
          End If
        End If
        Input(intFreeFile, strFMELine)
      Catch
        Input(intFreeFile, strFMELine)
      End Try
    Loop
    checkIMUTStatus = 1
    FileClose(intFreeFile)

  End Function

  'Private Function ShpToFeatureDataset(ByVal shapeFile As String, ByVal GDBFile As String, ByVal FDSName As String, ByVal tblName As String, ByVal strtabfile As String) As Integer
  '    'Export a shape file to a feature dataset in a personal geodatabase 
  '    'path doesn't end with "\", check you XML file or your user input

  '    ' +++ Set connection properties. Change the properties to match your
  '    ' +++ server name, instance, user name and password for your Access database
  '    Dim pOutAccessPropset As IPropertySet
  '    pOutAccessPropset = New PropertySet
  '    pOutAccessPropset.SetProperty("DATABASE", GDBFile)

  '    ' +++ Create a new feature datset name object for the output Access feature dataset
  '    Dim pOutAccessWorkspaceName As IWorkspaceName
  '    pOutAccessWorkspaceName = New WorkspaceName
  '    pOutAccessWorkspaceName.ConnectionProperties = pOutAccessPropset
  '    pOutAccessWorkspaceName.WorkspaceFactoryProgID = "esriDataSourcesFile.AccessWorkspaceFactory.1"

  '    Dim pOutAccessFeatDSName As IFeatureDatasetName
  '    pOutAccessFeatDSName = New FeatureDatasetName

  '    Dim pAccessDSname As IDatasetName
  '    pAccessDSname = pOutAccessFeatDSName

  '    pAccessDSname.WorkspaceName = pOutAccessWorkspaceName
  '    pAccessDSname.Name = FDSName

  '    ' +++ Get the name object for the input shapefile workspace
  '    Dim pShpWSF As IWorkspaceFactory
  '    pShpWSF = New ShapefileWorkspaceFactory
  '    Dim pInShpFileNames As IFileNames
  '    pInShpFileNames = New FileNames
  '    pInShpFileNames.Add(shapeFile)

  '    Dim pInShpWorkspaceName As IWorkspaceName
  '    pInShpWorkspaceName = New WorkspaceName
  '    pInShpWorkspaceName.PathName = FileUtils.extractDir(shapeFile)
  '    pInShpWorkspaceName.WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory.1"

  '    Dim pInShpFeatCLSNm As IFeatureClassName
  '    pInShpFeatCLSNm = New FeatureClassName
  '    Dim pShpDatasetName As IDatasetName
  '    pShpDatasetName = pInShpFeatCLSNm
  '    pShpDatasetName.WorkspaceName = pInShpWorkspaceName 'pShpWSF.GetWorkspaceName(FileUtils.extractDir(shapeFile), pInShpFileNames)
  '    pShpDatasetName.Name = FileUtils.extractFileName(FileUtils.removeExtension(shapeFile))

  '    ' +++ Open the input Shapefile FeatureClass object, so that we can get its fields
  '    Dim pName As IName
  '    Dim pInShpFeatCls As IFeatureClass

  '    pName = pShpDatasetName
  '    Try
  '        pInShpFeatCls = pName.Open
  '    Catch ex As Exception
  '        Throw New System.Exception("IFeatureDataConverter Failure")
  '    End Try
  '    pInShpFeatCLSNm.ShapeFieldName = pInShpFeatCls.ShapeFieldName
  '    pInShpFeatCLSNm.ShapeType = pInShpFeatCls.ShapeType
  '    'pInShpFeatCLSNm.FeatureDatasetName = pInShpFeatCls.FeatureDataset.FullName

  '    'Moved two following blocks from above TRY block
  '    ' +++ create the new output FeatureClass name object that will be passed
  '    ' +++ into the conversion function
  '    Dim pOutputDSName As IDatasetName
  '    Dim pOutputFCName As IFeatureClassName
  '    pOutputFCName = New FeatureClassName
  '    pOutputDSName = pOutputFCName
  '    pOutputDSName.WorkspaceName = pOutAccessWorkspaceName

  '    ' +++ Set the new FeatureClass name to be the same as the input FeatureClass name
  '    Dim pInDSName As IDatasetName
  '    pInDSName = pInShpFeatCLSNm
  '    pOutputDSName.Name = tblName
  '    pOutputFCName.ShapeFieldName = pInShpFeatCls.ShapeFieldName
  '    pOutputFCName.ShapeType = pInShpFeatCls.ShapeType

  '    ' +++ Get the fields for the input feature class and run them through
  '    ' +++ field checker to make sure there are no illegal or duplicate field names
  '    Dim pOutAccessFlds As IFields
  '    Dim pInShpFlds As IFields
  '    Dim pFldChk As IFieldChecker
  '    Dim i As Long
  '    Dim pGeoField As IField
  '    Dim pOutAccessGeoDef As IGeometryDef
  '    Dim pOutAccessGeoDefEdit As IGeometryDefEdit
  '    pInShpFlds = pInShpFeatCls.Fields
  '    pFldChk = New FieldChecker
  '    pFldChk.Validate(pInShpFlds, Nothing, pOutAccessFlds)

  '    Dim tabFieldNames As String()

  '    tabFieldNames = getMapInfoFieldNames(strtabfile)
  '    Dim pRenamedFields As IFieldsEdit
  '    Dim pFields As IFields
  '    pFields = New Fields
  '    pRenamedFields = pFields
  '    pRenamedFields.FieldCount_2 = pOutAccessFlds.FieldCount
  '    pRenamedFields.Field_2(0) = pOutAccessFlds.Field(0)
  '    pRenamedFields.Field_2(1) = pOutAccessFlds.Field(1)
  '    For i = 0 To tabFieldNames.Length - 1
  '        Dim pField As IField
  '        pField = New Field

  '        Dim pRenamedField As IFieldEdit
  '        pRenamedField = pField
  '        pRenamedField = pOutAccessFlds.Field(i + 2)
  '        pRenamedField.Name_2 = tabFieldNames(i)
  '        pRenamedField.AliasName_2 = tabFieldNames(i)
  '        pRenamedFields.Field_2(i + 2) = pRenamedField
  '    Next
  '    ' +++ Loop through the output fields to find the geometry field
  '    For i = 0 To pRenamedFields.FieldCount - 1
  '        If pRenamedFields.Field(i).Type = esriFieldType.esriFieldTypeGeometry Then
  '            pGeoField = pRenamedFields.Field(i)
  '            Exit For
  '        End If
  '    Next i

  '    ' +++ Get the geometry field's geometry definition
  '    pOutAccessGeoDef = pGeoField.GeometryDef

  '    ' +++ Give the geometry definition a spatial index grid count and grid size
  '    pOutAccessGeoDefEdit = pOutAccessGeoDef
  '    pOutAccessGeoDefEdit.GridCount_2 = 1
  '    pOutAccessGeoDefEdit.GridSize_2(0) = 1500000

  '    ' +++ Now use IFeatureDataConverter::Convert to create the output FeatureDataset and
  '    ' +++ FeatureClass 'pRenamedFields
  '    Try
  '        Dim pShpToFc As IFeatureDataConverter
  '        pShpToFc = New FeatureDataConverter
  '        If FDSName = "" Or FDSName Is Nothing Then
  '            pShpToFc.ConvertFeatureClass(pInShpFeatCLSNm, Nothing, Nothing, _
  '                           pOutputFCName, getGeomDef(pInShpFeatCls), pRenamedFields, "", 1000, 0)
  '        Else
  '            pShpToFc.ConvertFeatureClass(pInShpFeatCLSNm, Nothing, pRenamedFields, _
  '                               pOutputFCName, Nothing, pOutAccessFlds, "", 1000, 0)
  '        End If
  '        'pShpToFc.ConvertFeatureClass(pSHPFCName, Nothing, Nothing, pFCName, getGeomDef(pSHPFC), pSHPFC.Fields, "", 1000, 0)
  '    Catch ex As Exception
  '        Throw New System.Exception("IFeatureDataConverter Failure")
  '    End Try
  '    ShpToFeatureDataset = 1

  '    Exit Function

  'End Function
  Private Function ShpToFeatureDataset(ByVal shapeFile As String, ByVal GDBFile As String, ByVal FDSName As String, ByVal tblName As String, ByVal sTabFile As String, ByVal sLogFile As String) As Integer
    Dim pInPropSet As IPropertySet
    pInPropSet = New PropertySet
    pInPropSet.SetProperty("DATABASE", IO.Path.GetDirectoryName(shapeFile))

    Dim pOutPropSet As IPropertySet
    pOutPropSet = New PropertySet
    pOutPropSet.SetProperty("DATABASE", GDBFile)

    Dim sInName As String
    Dim sOutName As String
    sInName = IO.Path.GetFileNameWithoutExtension(shapeFile)
    sOutName = tblName

    Dim sOutFields As String()
    sOutFields = getMapInfoFieldNames(sTabFile)

    Try
      ConvertData(pInPropSet, sInName, convDataType.convDataTypeShapefile, _
          pOutPropSet, sOutName, "", convDataType.convDataTypePersonalGDB, hasGeom(shapeFile), sOutFields, sLogFile)
    Catch ex As Exception
      Throw New Exception("ConvertFeatureClass Error: " & ex.ToString)
    End Try

  End Function

  Private Function hasGeom(ByVal sShapeFile As String) As Boolean
    Dim sPath As String
    Dim sFile As String
    sPath = IO.Path.GetDirectoryName(sShapeFile)
    sFile = IO.Path.GetFileNameWithoutExtension(sShapeFile)

    Dim pWSF As IWorkspaceFactory
    pWSF = New ShapefileWorkspaceFactory
    Dim pWS As IWorkspace
    pWS = pWSF.OpenFromFile(sPath, 0)

    hasGeom = True

    Dim pEDS As IEnumDataset
    pEDS = pWS.Datasets(esriDatasetType.esriDTAny)
    Dim pDS As IDataset
    pDS = pEDS.Next
    Do While Not pDS Is Nothing
      If pDS.Name = sFile Then
        If pDS.Type = esriDatasetType.esriDTTable Then hasGeom = False
        Exit Function
      End If
      pDS = pEDS.Next
    Loop

  End Function

End Class



