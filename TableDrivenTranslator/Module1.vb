Imports ESRI.ArcGIS.esriSystem

Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Utility


Module Module1

    Private m_AOLicenseInitializer As LicenseInitializer = New TableDrivenTranslator.LicenseInitializer()
    Sub Main(ByVal CmdArgs() As String)
        'ESRI License Initializer generated code.
        m_AOLicenseInitializer.InitializeApplication(New esriLicenseProductCode() {esriLicenseProductCode.esriLicenseProductCodeArcView}, _
        New esriLicenseExtensionCode() {})
        Dim accessDB As String
        Dim accessTable As String
        Dim outputPGDB As String

        If CmdArgs.Length <> 3 Then
            Console.WriteLine("Error! Please specify 3 parameters using the following format:")
            Console.WriteLine("TableDrivenTranslator.exe accessDB accessTable outputPGDB")
            Console.WriteLine("     accessDB - Full path to a Microsoft Access .mdb file")
            Console.WriteLine("     accessTAble - A table in accessDB")
            Console.WriteLine("     outputPGDB - Full path to the output PGDB (This file will be created if it does not exist)")
            Exit Sub
        Else
            accessDB = CmdArgs(0)
            accessTable = CmdArgs(1)
            outputPGDB = CmdArgs(2)
        End If

        Dim accessFI As System.IO.FileInfo = New System.IO.FileInfo(accessDB)
        If Not accessFI.Exists() Then
            Console.WriteLine("Error! The Access database '" & accessDB & "' does not exist!")
            Exit Sub
        End If

        Dim tranny As translator.GISTranslator
        tranny = New translator.GISTranslator

        tranny.translateFromAccessTable(accessDB, accessTable, outputPGDB)

        Dim rs As ADODB.Recordset = New ADODB.Recordset
        Dim conn As ADODB.Connection = New ADODB.Connection
        conn.Mode = ADODB.ConnectModeEnum.adModeRead
        conn.Open("Provider='Microsoft.Jet.OLEDB.4.0';Data Source='" & accessDB & "';")
        rs.ActiveConnection = conn
        rs.Open(accessTable, conn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockReadOnly)

        Dim pWSF As AccessWorkspaceFactory = New AccessWorkspaceFactory
        Dim pWS As Workspace
        pWS = pWSF.OpenFromFile(outputPGDB, 0)
        Dim pEnumDS As IEnumDataset
        pEnumDS = pWS.Datasets(esriDatasetType.esriDTFeatureClass)

        rs.MoveFirst()
        Do
            Try
                Dim fcName As String
                Dim indexField As String
                If Not TypeOf rs.Fields("renameOutput").Value Is System.DBNull Then
                    fcName = rs.Fields("renameOutput").Value
                    Console.WriteLine(" Adding indices for table '" & fcName & "'...")
                End If
                If Not TypeOf rs.Fields("indexFields").Value Is System.DBNull Then
                    indexField = rs.Fields("indexFields").Value
                Else
                    Console.WriteLine(" No indices found.")
                End If

                Dim pFC As IFeatureClass

                pEnumDS.Reset()
                pFC = pEnumDS.Next()
                Do Until pFC Is Nothing
                    If pFC.AliasName = fcName Then Exit Do
                    pFC = pEnumDS.Next
                Loop

                'Delete existing indices
                Dim pTempIndexes As IIndexes
                pTempIndexes = pFC.Indexes 'FindIndexesByFieldName(indices(i))
                Console.WriteLine(" Deleting existing indices.")
                'Delete all indices except Index(0) and Index(1) since these are the indices for ObjectID and Shape_Field
                For i As Integer = pTempIndexes.IndexCount - 1 To 0 Step -1
                    If pTempIndexes.Index(i).Name <> "Shape_INDEX" And Not pTempIndexes.Index(i).Name.StartsWith("FDO_OBJECTID") Then
                        Console.WriteLine("     Deleted index '" & pTempIndexes.Index(i).Name & "'.")
                        pFC.DeleteIndex(pTempIndexes.Index(i))
                    Else
                        Console.WriteLine("     Preserving system index '" & pTempIndexes.Index(i).Name & "'.")
                    End If
                Next
                If Not pFC Is Nothing And Not indexField Is Nothing Then
                    Dim indices() As String = indexField.Split(",")
                    Console.WriteLine(" Found " & indices.Length & " field names.")
                    For i As Integer = 0 To indices.Length - 1
                        indices(i) = indices(i).Trim(" ")
                        Dim pIdxFields As IFields
                        Dim pIdxFieldsEdit As IFieldsEdit
                        Dim pIdxField As IField
                        Console.Write(" Adding index " & i + 1 & ", '" & indices(i) & "'...")
                        pIdxFields = New FieldsClass
                        pIdxFieldsEdit = pIdxFields
                        pIdxFieldsEdit.FieldCount_2 = 1
                        Dim j As Integer = pFC.FindField(indices(i))
                        If j <> -1 Then
                            Console.WriteLine("Found Field")
                            Try
                                pIdxField = pFC.Fields.Field(j)
                                pIdxFieldsEdit.Field_2(0) = pIdxField
                                Dim pIdx As IIndex
                                Dim pIdxEdit As IIndexEdit
                                pIdx = New Index
                                pIdxEdit = pIdx
                                pIdxEdit.Name_2 = "Idx_" & indices(i)
                                pIdxEdit.Fields_2() = pIdxFields
                                'Add the new index
                                pFC.AddIndex(pIdx)
                                Console.WriteLine("     Added index succesfully.")
                            Catch ex As Exception
                                Console.WriteLine("     ERROR: COULD NOT ADD INDEX.")
                                Console.WriteLine(ex.ToString)
                                Console.WriteLine("**************************************************")
                            End Try
                        Else
                            Console.WriteLine("     ERROR: FIELD NOT FOUND")
                        End If
                    Next i
                End If
            Catch ex As Exception
                Console.WriteLine(ex.ToString)
            Finally
                rs.MoveNext()
            End Try
        Loop Until rs.EOF

        'ESRI License Initializer generated code.
        'Do not make any call to ArcObjects after ShutDownApplication()
        m_AOLicenseInitializer.ShutdownApplication()
    End Sub

End Module
