using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SDELoader;

namespace SDELoader
{
    public partial class SDELoaderForm : Form
    {
        ModalProgressDialog pb;

        public SDELoaderForm()
        {
            InitializeComponent();
            SDELoaderConfig.UploadCommandRow row;
            row = sdeLoaderConfig.UploadCommand.AddUploadCommandRow("");
            sdeLoaderConfig.PGDBWorkspace.AddPGDBWorkspaceRow(row, "");
            sdeLoaderConfig.SDEWorkspace.AddSDEWorkspaceRow(row, "", "", "", "", "", "sde.Default" );
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            string fileName;
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            fileName = saveFileDialog1.FileName;
            //sdeLoaderConfig.Clear();
            
            sdeLoaderConfig.WriteXml(fileName);
        }

        private void btnLoadConfig_Click(object sender, EventArgs e)
        {
            string fileName;
            if (openConfigFile.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            fileName = openConfigFile.FileName;
            sdeLoaderConfig.Clear();
            sdeLoaderConfig.ReadXml(fileName);
        }

        private void SDELoaderForm_Load(object sender, EventArgs e)
        {

        }

        private void btnBrowsePGDB_Click(object sender, EventArgs e)
        {                        
            if (browsePGDBDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            sdeLoaderConfig.PGDBWorkspace[0].PGDB = browsePGDBDialog.FileName;
        }

        private void btnLoadToSDE_Click(object sender, EventArgs e)
        {
            try
            {
                pb = new ModalProgressDialog();
                SDELoaderEngine loader;
                loader = new SDELoaderEngine(sdeLoaderConfig);

                loader.FeatureCopied += new SDELoaderEngine.OnFeatureCopiedEventHandler(sdeEngine_FeatureCopied);
                loader.MessageSent += new SDELoaderEngine.OnMessageSentEventHandler(sdeEngine_MessageSent);                                
                this.Cursor = Cursors.WaitCursor;
                loader.RefreshSDE();                
            }
            catch (Exception ex)
            {
                pb.Hide();
                MessageBox.Show(ex.Message);
            }
            finally
            {
                pb.Hide();
                this.Cursor = Cursors.Default;                
            }

        }

        private void sdeEngine_MessageSent(string description, bool interrupt)
        {
            if (interrupt)
            {
                pb.Hide();
                MessageBox.Show(description);
                pb.Reset(description);
            }
            else
            {
                pb.Reset(description);
            }
        }
 
        private void sdeEngine_FeatureCopied(int newValue)
        {
            pb.UpdateProgress(newValue);
            Application.DoEvents();
        }
    }
}