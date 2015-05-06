using System;
using System.Collections.Generic;
using System.Text;
using SDELoader;
using ESRI.ArcGIS.esriSystem;
using MyLicenseInitializer;

namespace SDELoaderBatch
{
    class Program
    {        
        private static LicenseInitializer m_AOLicenseInitializer = new LicenseInitializer();
    
        static void Main(string[] args)
        {
            //ESRI License Initializer generated code.
            m_AOLicenseInitializer.InitializeApplication(new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeArcInfo },
            new esriLicenseExtensionCode[] { });
            Console.WriteLine();
            Console.WriteLine("Entering SDELoader...");
            SDELoaderConfig sdeConfig;            
            if (args.Length != 1)
            {                
                Console.WriteLine("Error: Proper format is 'SDELoader.exe configFile.xml'.");                                
                return;

            }
            string xmlFile;
            xmlFile = args[0];
            if (!System.IO.File.Exists(xmlFile))
            {
                Console.WriteLine("Error: Config file '" + xmlFile + "' not found.");
                return;
            }
            try
            {
                sdeConfig = new SDELoaderConfig();
                sdeConfig.ReadXml(xmlFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Could not parse file " + xmlFile + ":");
                Console.WriteLine(ex.Message);
                return;
            }

            try
            {
                SDELoaderEngine loader;
                loader = new SDELoaderEngine(sdeConfig);
                loader.MessageSent += new SDELoaderEngine.OnMessageSentEventHandler(sdeEngine_MessageSent);
                loader.RefreshSDE();
                Console.WriteLine("SDELoader completed succesfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Could not upload to SDE:");
                Console.WriteLine(ex.ToString());
                return;
            }
            finally
            {
                //Console.ReadLine();
            }

            //ESRI License Initializer generated code.
            //Do not make any call to ArcObjects after ShutDownApplication()
            m_AOLicenseInitializer.ShutdownApplication();
            
        }

        private static void sdeEngine_MessageSent(string description, bool interrupt)
        {
            Console.WriteLine(description);
        }
    }
}
