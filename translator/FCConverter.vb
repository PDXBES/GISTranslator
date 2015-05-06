Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Utility

'Portions of this code used with permission from ESRI

' Copyright 1995-2004 ESRI

' All rights reserved under the copyright laws of the United States.

' You may freely redistribute and use this sample code, with or without modification.

' Disclaimer: THE SAMPLE CODE IS PROVIDED "AS IS" AND ANY EXPRESS OR IMPLIED 
' WARRANTIES, INCLUDING THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS 
' FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL ESRI OR 
' CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, 
' OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF 
' SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
' INTERRUPTION) SUSTAINED BY YOU OR A THIRD PARTY, HOWEVER CAUSED AND ON ANY 
' THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT ARISING IN ANY 
' WAY OUT OF THE USE OF THIS SAMPLE CODE, EVEN IF ADVISED OF THE POSSIBILITY OF 
' SUCH DAMAGE.

' For additional information contact: Environmental Systems Research Institute, Inc.
' Attn: Contracts Dept.
' 380 New York Street
' Redlands, California, U.S.A. 92373 
' Email: contracts@esri.com

Module FCConverter

    Public Enum convDataType
        convDataTypeGDB = 0
        convDataTypePersonalGDB = 1
        convDataTypeShapefile = 2
        convDataTypeCoverage = 3
        convDataTypeInfo = 4
        convDataTypeDbase = 5
        convDataTypeOLEDB = 6
    End Enum

    Public Function ConvertData(ByVal pInPropertySet As IPropertySet, _
    ByVal sInName As String, _
    ByVal eInDataType As convDataType, _
    ByVal pOutPropertySet As IPropertySet, _
    ByVal sOutName As String, _
    ByVal sOutFDName As String, _
    ByVal eOutDataType As convDataType, _
    ByVal bOutFC As Boolean, _
    Optional ByVal sOutFields As String() = Nothing, Optional ByVal sLogFile As String = "c:\log.txt") As String

        Dim log As dotnetutils.LogFile = New dotnetutils.LogFile(sLogFile)

        ' Setup input workspace
        Dim pInWorkspaceName As IWorkspaceName
        pInWorkspaceName = New WorkspaceName
        pInWorkspaceName.ConnectionProperties = pInPropertySet
        pInWorkspaceName.WorkspaceFactoryProgID = GetProgID(eInDataType)

        ' Setup output workspace.
        Dim pOutWorkspaceName As IWorkspaceName
        pOutWorkspaceName = New WorkspaceName
        pOutWorkspaceName.ConnectionProperties = pOutPropertySet
        pOutWorkspaceName.WorkspaceFactoryProgID = GetProgID(eOutDataType)

        Try
            ' Setup input dataset name
            Dim pInDatasetName As IDatasetName

            If bOutFC Then
                pInDatasetName = New FeatureClassName
            Else
                pInDatasetName = New TableName
            End If
            pInDatasetName.Name = sInName
            pInDatasetName.WorkspaceName = pInWorkspaceName

            ' Set output feature dataset name
            Dim pOutFDName As IDatasetName
            If sOutFDName = "" Then
                pOutFDName = Nothing
            Else
                pOutFDName = New FeatureDatasetName
                pOutFDName.WorkspaceName = pOutWorkspaceName
                pOutFDName.Name = sOutFDName
            End If

            ' Set output dataset name
            Dim pOutDatasetName As IDatasetName
            If bOutFC Then
                pOutDatasetName = New FeatureClassName
                If Not pOutFDName Is Nothing Then
                    Dim pFCName As IFeatureClassName
                    pFCName = pOutDatasetName
                    pFCName.FeatureDatasetName = pOutFDName
                End If
            Else
                pOutDatasetName = New TableName
            End If
            pOutDatasetName.WorkspaceName = pOutWorkspaceName
            pOutDatasetName.Name = sOutName

            ' Open input table to get field definitions
            Dim pName As IName
            Dim pInTable As ITable
            pName = pInDatasetName
            pInTable = pName.Open

            ' Validate the field names, report any errors
            Dim pInFields As IFields
            pInFields = pInTable.Fields

            Dim pFieldCheck As IFieldChecker
            pFieldCheck = New FieldChecker
            Dim pOutFields As IFields
            Dim pEnumFieldError As IEnumFieldError
            pFieldCheck.Validate(pInFields, pEnumFieldError, pOutFields)
            If Not pEnumFieldError Is Nothing Then
                Dim strFldErr As String
                strFldErr = "Some columns will be given new names as follows:"
                Dim pFieldError As IFieldError
                pFieldError = pEnumFieldError.Next
                Do Until pFieldError Is Nothing
                    Dim pInField As IField
                    Dim pOutField As IField
                    pInField = pInFields.Field(pFieldError.FieldIndex)
                    pOutField = pOutFields.Field(pFieldError.FieldIndex)
                    strFldErr = strFldErr & vbNewLine & _
                                vbTab & vbTab & pOutField.Name & vbTab & "reason: " & pInField.Name & "  " & _
                                GetFieldError(pFieldError.FieldError)
                    pFieldError = pEnumFieldError.Next
                Loop
            End If

            If Not sOutFields Is Nothing Then
                Dim pRenamedFields As IFieldsEdit
                Dim pFields As IFields
                pFields = New Fields
                pRenamedFields = pFields
                pRenamedFields.FieldCount_2 = pOutFields.FieldCount

                pRenamedFields.Field_2(0) = pOutFields.Field(0)

                pRenamedFields.Field_2(1) = pOutFields.Field(1)
                For i As Integer = 0 To sOutFields.Length - 1
                    Dim pField As IField
                    pField = New Field

                    Dim pRenamedField As IFieldEdit
                    pRenamedField = pField
                    pRenamedField = pOutFields.Field(i + 2)
                    pRenamedField.Name_2 = sOutFields(i)
                    pRenamedField.AliasName_2 = sOutFields(i)
                    If pRenamedField.Scale = 0 And pRenamedField.Type = esriFieldType.esriFieldTypeDouble Then
                        pRenamedField.Type_2 = esriFieldType.esriFieldTypeInteger
                    End If
                    pRenamedFields.Field_2(i + 2) = pRenamedField
                Next
                ' +++ Loop through the output fields to find the geometry field
                pOutFields = pRenamedFields
            End If

            ' Do the conversion
            Dim pConverter As IFeatureDataConverter
            pConverter = New FeatureDataConverterClass

            Dim pEnumErrors As IEnumInvalidObject

            ' If we are converting to a feature class,
            ' set up the output geometry definition
            If bOutFC Then
                ' Loop through the output fields to find the geometry field
                Dim i As Long
                Dim pGeoField As IField
                For i = 0 To pOutFields.FieldCount - 1
                    If pOutFields.Field(i).Type = esriFieldType.esriFieldTypeOID Then
                        Dim renameField As IFieldEdit
                        renameField = pOutFields.Field(i)
                        renameField.AliasName_2 = "OBJECTID"
                        renameField.Name_2 = "OBJECTID"
                        Continue For
                    End If
                    If pOutFields.Field(i).Type = esriFieldType.esriFieldTypeGeometry Then
                        pGeoField = pOutFields.Field(i)
                        Continue For
                    End If
                Next i

                ' Get the geometry field's geometry defenition
                Dim pOutFCGeoDef As IGeometryDef
                pOutFCGeoDef = pGeoField.GeometryDef

                ' Give the geometry definition a spatial index grid count and grid size
                Dim pOutFCGeoDefEdit As IGeometryDefEdit
                pOutFCGeoDefEdit = pOutFCGeoDef
                pOutFCGeoDefEdit.GridCount_2 = 1
                pOutFCGeoDefEdit.GridSize_2(0) = DefaultIndexGrid(pInTable)
                pOutFCGeoDefEdit.SpatialReference_2 = pGeoField.GeometryDef.SpatialReference
                'Dim icp2 As IControlPrecision2
                'icp2 = pOutFCGeoDefEdit.SpatialReference
                'icp2.IsHighPrecision = False 'Forces low-precision featureclass for backwards compatibility. Should be removed when 9.1 and less are purged

                ' Convert to feature class
                pEnumErrors = pConverter.ConvertFeatureClass(pInDatasetName, Nothing, _
                                                                 pOutFDName, pOutDatasetName, pOutFCGeoDef, _
                                                                 pOutFields, "", 1000, 0)
            Else
                ' Convert to table
                pEnumErrors = pConverter.ConvertTable(pInDatasetName, Nothing, _
                                                         pOutDatasetName, pOutFields, _
                                                         "", 1000, 0)
            End If

            ' If some of the records do not load, report to report window.
            Dim pErrInfo As IInvalidObjectInfo
            'pEnumErrors.Reset
            pErrInfo = pEnumErrors.Next
            If pErrInfo Is Nothing Then
                ConvertData = "Load completed"
            Else
                'Load(frmReport)
                'frmReport.Caption = "Rejected Objects"
                'frmReport.Visible = True
                Do Until pErrInfo Is Nothing
                    'frmReport.lstReport.AddItem(pErrInfo.InvalidObjectID & vbTab & pErrInfo.ErrorDescription)
                    pErrInfo = pEnumErrors.Next
                Loop
                ConvertData = "Load completed with errors"
            End If
            log.writeLog("      FCConverter: " & ConvertData)
        Catch ex As Exception
            log.writeLog("      FCConverter: Error - " & ex.ToString)
        Finally
            pInWorkspaceName = Nothing
            pOutWorkspaceName = Nothing
        End Try



    End Function

    Public Function GetPathName(ByVal sPath As String, ByVal iOption As Integer) As String

        Dim pos As Long
        pos = InStrRev(sPath, "\")
        Dim fname As String
        fname = Right(sPath, (Len(sPath) - pos))
        Dim pName As String
        pName = Left(sPath, pos - 1)

        If iOption = 0 Then
            GetPathName = pName
        Else
            GetPathName = fname
        End If
    End Function

    Private Function DefaultIndexGrid(ByVal InFC As IFeatureClass) As Double
        ' Calculate approximate first grid
        ' based on the average of a random sample of feature extents times five
        Dim lngNumFeat As Long
        Dim lngSampleSize As Long
        Dim pFields As IFields
        Dim pField As IField
        Dim strFIDName As String
        Dim strWhereClause As String
        Dim lngCurrFID As Long
        Dim pFeat As IFeature
        Dim pFeatCursor As IFeatureCursor
        Dim pFeatEnv As IEnvelope
        Dim pQueryFilter As IQueryFilter
        Dim pNewCol As New Collection
        Dim lngKMax As Long

        Dim dblMaxDelta As Double
        dblMaxDelta = 0
        Dim dblMinDelta As Double
        dblMinDelta = 1000000000000.0#
        Dim dblSquareness As Double
        dblSquareness = 1

        Dim i As Long
        Dim j As Long
        Dim k As Long

        Const SampleSize = 1
        Const Factor = 1

        ' Create a recordset
        Dim ColInfo(0), c0(3)
        c0(0) = "minext"
        c0(1) = CInt(5)
        c0(2) = CInt(-1)
        c0(3) = False
        ColInfo(0) = c0

        lngNumFeat = InFC.FeatureCount(Nothing) - 1
        If lngNumFeat <= 0 Then
            DefaultIndexGrid = 1000
            Exit Function
        End If

        'if the feature type is points use the density function
        If InFC.ShapeType = esriGeometryType.esriGeometryMultipoint Or InFC.ShapeType = esriGeometryType.esriGeometryPoint Then
            DefaultIndexGrid = DefaultIndexGridPoint(InFC)
            Exit Function
        End If

        ' Get the sample size
        lngSampleSize = lngNumFeat * SampleSize
        ' Don't allow too large a sample size to speed
        If lngSampleSize > 1000 Then lngSampleSize = 1000

        ' Get the ObjectID Fieldname of the feature class
        pFields = InFC.Fields
        ' FID is always the first field
        pField = pFields.Field(0)
        strFIDName = pField.Name

        ' Add every nth feature to the collection of FIDs
        For i = 1 To lngNumFeat Step CLng(lngNumFeat / lngSampleSize)
            pNewCol.Add(i)
        Next i

        For j = 0 To pNewCol.Count - 1 Step 250
            ' Will we top out the features before the next 250 chunk?
            lngKMax = Min(pNewCol.Count - j, 250)
            strWhereClause = strFIDName + " IN("
            For k = 1 To lngKMax
                strWhereClause = strWhereClause + CStr(pNewCol.Item(j + k)) + ","
            Next k
            ' Remove last comma and add close parenthesis
            strWhereClause = Mid(strWhereClause, 1, Len(strWhereClause) - 1) + ")"
            pQueryFilter = New QueryFilter
            pQueryFilter.WhereClause = strWhereClause
            pFeatCursor = InFC.Search(pQueryFilter, True)
            pFeat = pFeatCursor.NextFeature
            While Not pFeat Is Nothing
                ' Get the extent of the current feature
                pFeatEnv = pFeat.Extent
                ' Find the min, max side of all extents. The "Squareness", a measure
                ' of how close the extent is to a square, is accumulated for later
                ' average calculation.
                dblMaxDelta = Max(dblMaxDelta, Max(pFeatEnv.Width, pFeatEnv.Height))
                dblMinDelta = Min(dblMinDelta, Min(pFeatEnv.Width, pFeatEnv.Height))
                '  lstSort.AddItem Max(pFeatEnv.Width, pFeatEnv.Height)
                If dblMinDelta <> 0 Then
                    dblSquareness = dblSquareness + ((Min(pFeatEnv.Width, pFeatEnv.Height) / (Max(pFeatEnv.Width, pFeatEnv.Height))))
                Else
                    dblSquareness = dblSquareness + 0.0001
                End If
                pFeat = pFeatCursor.NextFeature
            End While
        Next j

        ' If the average envelope approximates a square set the grid size half
        ' way between the min and max sides. If the envelope is more rectangular,
        ' then set the grid size to half of the max.
        If ((dblSquareness / lngSampleSize) > 0.5) Then
            DefaultIndexGrid = (dblMinDelta + ((dblMaxDelta - dblMinDelta) / 2)) * Factor
        Else
            DefaultIndexGrid = (dblMaxDelta / 2) * Factor
        End If

    End Function

    Function DefaultIndexGridPoint(ByVal pInFC As IFeatureClass) As Double
        '**************
        ' Calculates the Index grid based on input feature class
        '**************

        ' Get the dataset
        Dim pGeoDataSet As IGeoDataset
        pGeoDataSet = pInFC

        ' Get the envelope of the input dataset
        Dim pEnvelope As IEnvelope
        pEnvelope = pGeoDataSet.Extent

        'Calculate approximate first grid
        Dim lngNumFeat As Long
        Dim dblArea As Double
        lngNumFeat = pInFC.FeatureCount(Nothing)

        If lngNumFeat = 0 Or pEnvelope.IsEmpty Then
            ' when there are no features or an empty bnd - return 1000
            DefaultIndexGridPoint = 1000
        Else
            dblArea = pEnvelope.Height * pEnvelope.Width
            ' approximate grid size is the square root of area over the number of features
            DefaultIndexGridPoint = System.Math.Sqrt(dblArea / lngNumFeat)
        End If

    End Function

    Private Function Min(ByVal v1 As Object, ByVal v2 As Object) As Object
        Min = IIf(v1 < v2, v1, v2)
    End Function

    Private Function Max(ByVal v1 As Object, ByVal v2 As Object) As Object
        Max = IIf(v1 > v2, v1, v2)
    End Function

    Public Function GetProgID(ByVal eDataType As convDataType) As String
        Dim strResult As String

        Select Case eDataType
            Case convDataType.convDataTypeGDB
                strResult = "esriDataSourcesGDB.SDEWorkspaceFactory.1"
            Case convDataType.convDataTypeShapefile
                strResult = "esriDataSourcesFile.ShapefileWorkspaceFactory.1"
            Case convDataType.convDataTypePersonalGDB
                strResult = "esriDataSourcesGDB.AccessWorkspaceFactory.1"
            Case convDataType.convDataTypeOLEDB
                strResult = "esriDataSourcesOleDB.OLEDBWorkspaceFactory.1"
            Case convDataType.convDataTypeCoverage
                strResult = "esriDataSourcesFile.ArcInfoWorkspaceFactory.1"
            Case convDataType.convDataTypeDbase
                strResult = "esriDataSourcesFile.ShapefileWorkspaceFactory.1"
            Case convDataType.convDataTypeInfo
                strResult = "esriDataSourcesFile.ArcInfoWorkspaceFactory.1"
        End Select

        GetProgID = strResult
    End Function

    Private Function GetFieldError(ByVal eError As esriFieldNameErrorType) As String
        Dim strResult As String
        strResult = ""

        Select Case eError
            Case esriFieldNameErrorType.esriSQLReservedWord
                strResult = "is SQL reserved word"
            Case esriFieldNameErrorType.esriDuplicatedFieldName
                strResult = "is duplicated field name"
            Case esriFieldNameErrorType.esriInvalidCharacter
                strResult = "contains invalid character"
            Case esriFieldNameErrorType.esriInvalidFieldNameLength
                strResult = "too long"
        End Select

        GetFieldError = strResult
    End Function

End Module
