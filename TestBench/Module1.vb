Imports translator
Imports System.io

Module Module1

    Public Sub Main()
        Try
            Dim testTranslator As GISTranslator
            Dim intResult As Integer
            Dim strFields As String()
            testTranslator = New GISTranslator
            'intResult = testTranslator.callIMUT("c:\temp\mst_nodes_ac.tab", "c:\temp\shp\", TranslationTypes.SHAPEtoTAB, OutputTypes.Point)
            'intResult = testTranslator.shapeToPGDB("c:\temp\shp\mst_nodes_ac.shp", "c:\temp\pgdb\foo.mdb", "mst_nodes_ac", True)
            'intResult = testTranslator.renamePGDBFromTAB("c:\temp\mst_nodes_ac.tab", "c:\temp\pgdb\mst_nodes_ac.mdb", "mst_nodes_ac")
            'intResult = testTranslator.reprojectPGDB("c:\temp\pgdb\mst_nodes_ac.mdb")
            'intResult = testTranslator.translateModel("D:\Temp\Linnton_Test")
            'intResult = testTranslator.translateModel("D:\Temp\TRANTEST")
            'intResult = testTranslator.translateModelResults("Z:\Alternatives\RP02\NWN_RP02_25")
            intResult = testTranslator.translateModel("W:\Projects\5375_MocksPS\models\MPS_EX_DWF\")
            'Dim b As Boolean
            'Directory.Delete("d:\temp\alternatives", True)
            'If dotnetutils.AccessUtils.tableExists("\\cassio\modeling\Model_Programs\Emgaats\binV2.1\mdbs\LookupTables.mdb", "ArcGISModelTranslation") Then
            ''dotnetutils.AccessUtils.copyTable("\\cassio\modeling\Model_Programs\Emgaats\binV2.1\mdbs\LookupTables.mdb", _
            '   "ArcGISModelTranslation", "D:\Temp\Linnton_Test\mdbs\LookupTables.mdb", "ArcGISModelTranslation")
            'dotnetutils.AccessUtils.copyTable("D:\Temp\Linnton_Test\mdbs\LookupTables.mdb", _
            '   "ArcGISModelTranslation2", "D:\Temp\Linnton_Test\mdbs\LookupTables.mdb", "ArcGISModelTranslation3")
            'End If

        Catch
            MsgBox(Err.Description)
        End Try

    End Sub

End Module
