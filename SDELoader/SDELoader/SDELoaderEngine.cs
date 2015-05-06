using System;
using System.Collections.Generic;
using System.Text;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.EditorExt;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataManagementTools;
using ESRI.ArcGIS.Geoprocessing;

namespace SDELoader
{
    public class SDELoaderEngine
    {
        public delegate void OnFeatureCopiedEventHandler(int newValue);
        public delegate void OnMessageSentEventHandler(string description, bool interrupt);

        string pgdb, server, instance, database, version, user, password;
           
        IFeatureWorkspace pgdbFWS, sdeFWS;
        SDELoaderConfig sdeLoaderConfig;
        
        public SDELoaderEngine(SDELoaderConfig sdeLoaderConfig)
        {
            IWorkspaceFactory pgdbWSF, sdeWSF;
            IPropertySet sdePropertySet;

            this.sdeLoaderConfig = sdeLoaderConfig;

            SDELoaderConfig.PGDBWorkspaceRow pgdbRow;
            SDELoaderConfig.SDEWorkspaceRow sdeRow;
            pgdbRow = sdeLoaderConfig.PGDBWorkspace[0];
            sdeRow = sdeLoaderConfig.SDEWorkspace[0];
            this.pgdb = pgdbRow.PGDB;
            this.server = sdeRow.Server;
            this.instance = sdeRow.Instance;
            this.database = sdeRow.Database;
            this.version = sdeRow.Version;
            this.user = sdeRow.User;
            this.password = sdeRow.Password;

            sdePropertySet = new PropertySetClass();
            sdePropertySet.SetProperty("SERVER", this.server);
            sdePropertySet.SetProperty("INSTANCE", this.instance);
            sdePropertySet.SetProperty("DATABASE", this.database);
            sdePropertySet.SetProperty("USER", this.user);
            sdePropertySet.SetProperty("PASSWORD", this.password);
            sdePropertySet.SetProperty("VERSION", this.version);
            
            sdeWSF = new SdeWorkspaceFactoryClass();
            sdeFWS = (IFeatureWorkspace)sdeWSF.Open(sdePropertySet, 0);
            if (System.IO.File.Exists(System.IO.Path.GetTempPath() + "sdeloader.sde"))
            {
                System.IO.File.Delete(System.IO.Path.GetTempPath() + "sdeloader.sde");
            }
            sdeWSF.Create(System.IO.Path.GetTempPath(), "sdeloader.sde", sdePropertySet, 0);        

            pgdbWSF = new AccessWorkspaceFactoryClass();
            pgdbFWS = (IFeatureWorkspace)pgdbWSF.OpenFromFile(pgdb, 0);
        }

        public void RefreshSDE()
        {            
            string sourceFCName, destFCName;

            foreach (SDELoaderConfig.FeatureClassRow fcRow in sdeLoaderConfig.FeatureClass)
            {
                IWorkspaceEdit sdeEditor;
                sdeEditor = (IWorkspaceEdit)sdeFWS;
                IVersion version;
                version = (IVersion)sdeFWS;
                IEnumLockInfo enumLockInfo = version.VersionLocks;
                ILockInfo lockInfo = enumLockInfo.Next();
                while (lockInfo != null)
                {
                    this.OnMessageSent("LockInfo.UserName = " + lockInfo.UserName + ". LockInfo.LockType = " + lockInfo.LockType.ToString(), false);
                }
                
                try
                {
                    sourceFCName = fcRow.SourceFC;
                    destFCName = fcRow.DestFC == null || fcRow.DestFC == ""
                        ? sourceFCName : fcRow.DestFC;

                    ITable sourceTable = null;
                    ITable destTable = null;
                    IFeatureClass sourceFC = null;
                    IFeatureClass destFC = null;

                    IWorkspace ws;
                    ws = (IWorkspace)pgdbFWS;
                    IEnumDataset enumDS;
                    enumDS = ws.get_Datasets(esriDatasetType.esriDTAny);
                    IDataset ds;
                    ds = enumDS.Next();
                    while (ds != null)
                    {
                        //FeatureClass
                        if (ds.Name == sourceFCName && ds.Type == esriDatasetType.esriDTFeatureClass)
                        {
                            

                            sourceFC = pgdbFWS.OpenFeatureClass(sourceFCName);
                            try
                            {
                                destFC = sdeFWS.OpenFeatureClass(/*this.database + "." + this.user + "." + */destFCName);
                            }
                            catch (Exception ex)
                            {
                                destFC = sdeFWS.CreateFeatureClass(destFCName, sourceFC.Fields, null, null, sourceFC.FeatureType, sourceFC.ShapeFieldName, "");
                            }
                            sourceTable = (ITable)sourceFC;
                            destTable = (ITable)destFC;
                            
                            break;
                        }
                        //Table
                        if (ds.Name == sourceFCName && ds.Type == esriDatasetType.esriDTTable)
                        {
                            sourceTable = pgdbFWS.OpenTable(sourceFCName);
                            try
                            {
                                destTable = sdeFWS.OpenTable(/*this.database + "." + this.user + "." + */destFCName);
                            }
                            catch (Exception ex)
                            {
                                destTable = sdeFWS.CreateTable(destFCName, sourceTable.Fields, null, null, "");
                            }
                            break;
                        }
                        ds = enumDS.Next();
                    }

                    if (sourceTable == null || destTable == null)
                    {
                        this.OnMessageSent("Could not find " + sourceFCName, true);
                    }

                    //Set up query filter for incoming table
                    String inFieldList = "";
                    IFields outFields = new FieldsClass();
                    IFieldsEdit outFieldsEdit = (IFieldsEdit)outFields;

                    for (int i = 1; i < sourceTable.Fields.FieldCount - 1; i++)
                    {
                        inFieldList = inFieldList + sourceTable.Fields.get_Field(i).Name + ",";
                        outFieldsEdit.AddField(sourceTable.Fields.get_Field(i));
                    }
                    inFieldList = inFieldList + sourceTable.Fields.get_Field(sourceTable.Fields.FieldCount - 1).Name;
                    outFieldsEdit.AddField(sourceTable.Fields.get_Field(sourceTable.Fields.FieldCount - 1));
                    IQueryFilter queryFilter = new QueryFilterClass();
                    queryFilter.SubFields = inFieldList;
                    
                    IEnumInvalidObject enumInvalidObject;
                    
                    sdeEditor.StartEditing(false);
                    sdeEditor.StartEditOperation();

                    ISchemaLock schemaLock;
                    ISchemaLockInfo schemaLockInfo;
                    IEnumSchemaLockInfo enumSchemaLockInfo;
                    schemaLock = (ISchemaLock)destTable;
                    schemaLock.GetCurrentSchemaLocks(out enumSchemaLockInfo);
                    enumSchemaLockInfo.Reset();
                    schemaLockInfo = enumSchemaLockInfo.Next();
                    while (schemaLockInfo != null)
                    {
                        this.OnMessageSent("Lock type: " + schemaLockInfo.SchemaLockType.ToString() + ". User name: " + schemaLockInfo.UserName + ".", false);                                             
                        schemaLockInfo = enumSchemaLockInfo.Next();
                    }

                    this.OnMessageSent("Clearing " + destTable.RowCount(null) + " existing features from " + ds.BrowseName + "...", false);
                    /*DeleteFeatures df = new DeleteFeatures(destFC);
                    Geoprocessor pGeoProc = new Geoprocessor();
                    ESRI.ArcGIS.Carto.TrackCancel tc = new ESRI.ArcGIS.Carto.TrackCancelClass();
                    GeoProcessorResult pGeoResult = new GeoProcessorResult();
                    pGeoProc.SetEnvironmentValue("workspace", System.IO.Path.GetTempPath() + "sdeloader.sde");
                    pGeoResult = (GeoProcessorResult)pGeoProc.Execute(df, tc);
                    
                    for (int i = 0; i < pGeoProc.MessageCount; i++)
                    {
                       this.OnMessageSent(pGeoProc.GetMessage(i), false);
                    }*/
                    

                    ds = (IDataset)destTable;
                    IQueryFilter qf = new QueryFilterClass();
                    qf.WhereClause = "";
                    destTable.DeleteSearchedRows(qf);

                    IObjectLoader objectLoader = new ObjectLoaderClass();
                    ds = (IDataset)sourceTable;
                    this.OnMessageSent("Loading " + sourceTable.RowCount(null) + " features from " + ds.BrowseName + "...", false);
                    objectLoader.LoadObjects(null, sourceTable, queryFilter, destTable, outFields, false, 0, false, false, 10, out enumInvalidObject);
                    IInvalidObjectInfo invalidObject = enumInvalidObject.Next();

                    if (invalidObject != null)
                    {
                        int j = 0;
                        string errorDescription = invalidObject.ErrorDescription;
                        while (invalidObject != null)
                        {
                            j++;
                            invalidObject = enumInvalidObject.Next();
                        }
                        this.OnMessageSent("Error loading " + sourceFCName + ": " + j + " features did not load", true);
                        this.OnMessageSent("The first error encountered was: ", false);
                        this.OnMessageSent("    " + errorDescription, false);
                    }
                    

                    UpdateMetaData(sourceFCName, destFCName);
                }
                catch (Exception ex)
                {
                    this.OnMessageSent(ex.ToString(), true);
                }
                finally
                {
                    if (sdeEditor.IsBeingEdited())
                    {
                        sdeEditor.StartEditOperation();
                        sdeEditor.StopEditing(true);
                    }
                }
            }
            
        }

        public void UpdateMetaData(string sourceName, string destName)
        {
            IWorkspace sdeWS;
            IEnumDatasetName enumDSName;
            IDatasetName dsName;
            sdeWS = (IWorkspace)sdeFWS;
            enumDSName = sdeWS.get_DatasetNames(esriDatasetType.esriDTAny);
            dsName = enumDSName.Next();
            while (dsName != null)
            {
                string[] shortName;
                shortName = dsName.Name.Split(new char[] {'.'}, StringSplitOptions.RemoveEmptyEntries);
               
                if (shortName[shortName.Length-1].ToUpper() == destName.ToUpper())
                {
                    break;
                }
                dsName = enumDSName.Next();
            }
            if (dsName == null)
            {
                throw new Exception("Could not update metadata.");
            }
            IMetadata md;
            md = (IMetadata)dsName;

            string mdOrigin;            
            object[] o;
            md.Synchronize(esriMetadataSyncAction.esriMSAAlways, 1);
            
            
            IXmlPropertySet2 xPS;
            xPS = (IXmlPropertySet2)md.Metadata;

            mdOrigin = "Generated from " + pgdb + "\\" + sourceName + " by user '" + System.Environment.UserName + "'.";
            xPS.SetPropertyX("idinfo/descript/supplinf", mdOrigin, esriXmlPropertyType.esriXPTText, esriXmlSetPropertyAction.esriXSPAAddOrReplace, false);
            xPS.SetPropertyX("idinfo/timeperd/timeinfo/sngdate/time", System.DateTime.Now.Date.ToShortTimeString(), esriXmlPropertyType.esriXPTText, esriXmlSetPropertyAction.esriXSPAAddOrReplace, false);
            xPS.SetPropertyX("idinfo/imeperd/timeinfo/sngdate/caldate", System.DateTime.Now.Date.ToShortDateString(), esriXmlPropertyType.esriXPTText, esriXmlSetPropertyAction.esriXSPAAddOrReplace, false);
            xPS.SetPropertyX("idinfo/citation/citeinfo/pubinfo/publish", "BES Systems Analysis", esriXmlPropertyType.esriXPTText, esriXmlSetPropertyAction.esriXSPAAddOrReplace, false);
            xPS.SetPropertyX("idinfo/citation/citeinfo/pubinfo/pubplace", "City of Portland", esriXmlPropertyType.esriXPTText, esriXmlSetPropertyAction.esriXSPAAddOrReplace, false);
            xPS.SetPropertyX("idinfo/useconst", "The information contained in this layer is intended for modeling purposes and should be verified before used for any other purposes.", esriXmlPropertyType.esriXPTText, esriXmlSetPropertyAction.esriXSPAAddOrReplace, false);
            xPS.SetPropertyX("idinfo/ptcontac/cntinfo/cntorgp/cntorg", "BES Systems Analysis", esriXmlPropertyType.esriXPTText, esriXmlSetPropertyAction.esriXSPAAddOrReplace, false);
            //md.Metadata.SetProperty("idinfo/citation/citeinfo/origin", mdText);                                    
            md.Metadata = (IPropertySet)xPS;
        }

        public void RefreshPGDB()
        {
        }

        private IFeatureClass getFeatureClass(IEnumDatasetName enumDS, string fcName)
        {            
            IDatasetName ds;
            IFeatureClass fc = null;
            ds = enumDS.Next();
            while (ds != null)
            {
                if (ds.Name == fcName)
                {
                    fc = (IFeatureClass)ds;
                    break;
                }                
            }
            return fc;
        }


        public event OnFeatureCopiedEventHandler FeatureCopied;

        /// <summary>
        /// Internally called function that fires the OnStatusChanged event.
        /// </summary>
        /// <param name="e">Parameter that contains the string describing the new state.</param>
        protected virtual void OnFeatureCopied(int newValue)
        {
            if (FeatureCopied != null)
                FeatureCopied(newValue);
        }

        public event OnMessageSentEventHandler MessageSent;

        /// <summary>
        /// Internally called function that fires the OnStatusChanged event.
        /// </summary>
        /// <param name="e">Parameter that contains the string describing the new state.</param>
        protected virtual void OnMessageSent(string description, bool interrupt)
        {
            if (MessageSent != null)
                MessageSent(description, interrupt);
        }
    }
}
