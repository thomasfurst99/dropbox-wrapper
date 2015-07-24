using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;

namespace DropboxWrapper
{
    public partial class SelectPaths : Form
    {
        public SelectPaths()
        {
            InitializeComponent();

            reloadData();
        }

        private void chooseWrapFolderBtn_Click(object sender, System.EventArgs e)
        {
            folderSelect.Description = "Choose your wrap folder";
            folderSelect.SelectedPath = (string)Registry.GetValue(Properties.Resources.RegistryPath, Properties.Resources.WrapFolderPath, null);
            if (folderSelect.ShowDialog() == DialogResult.OK)
            {
                Registry.SetValue(Properties.Resources.RegistryPath, Properties.Resources.WrapFolderPath, folderSelect.SelectedPath);
            }

            reloadData();
            folderSelect.Reset();
        }

        private void chooseDropboxFolderBtn_Click(object sender, System.EventArgs e)
        {
            folderSelect.Description = "Choose your Dropbox folder";
            folderSelect.SelectedPath = (string)Registry.GetValue(Properties.Resources.RegistryPath, Properties.Resources.DropboxFolderPath, null);
            if (folderSelect.ShowDialog() == DialogResult.OK)
            {
                Registry.SetValue(Properties.Resources.RegistryPath, Properties.Resources.DropboxFolderPath, folderSelect.SelectedPath);
            }

            reloadData();
            folderSelect.Reset();
        }

        private void chooseCompressionAppFolderBtn_Click(object sender, EventArgs e)
        {
            folderSelect.Description = "Choose your folder for " + uploadTypeCheckbox.SelectedItem.ToString();
            folderSelect.SelectedPath = (string)Registry.GetValue(Properties.Resources.RegistryPath, uploadTypeCheckbox.SelectedItem.ToString(), null);
            if (folderSelect.ShowDialog() == DialogResult.OK)
            {
                // If user want to use win rar to compress files must check if selected folder contains WinRAR (rar.exe)
                if (uploadTypeCheckbox.SelectedItem.ToString() == FileAndFolderHandlerUploadType.WinRAR.ToString())
                {
                    if (Directory.GetFiles(folderSelect.SelectedPath, "rar.exe").Length == 1)
                    {
                        Registry.SetValue(Properties.Resources.RegistryPath, uploadTypeCheckbox.SelectedItem.ToString(), folderSelect.SelectedPath);
                    }
                    else
                    {
                        MessageBox.Show("In this folder there is no WinRAR! Try again.", "Error");
                    }
                }
            }

            reloadData();
            folderSelect.Reset();
        }

        private void reloadData()
        {
            wrapFolderPathLabel.Text =
                (string)Registry.GetValue(Properties.Resources.RegistryPath, Properties.Resources.WrapFolderPath, null) ??
                Properties.Resources.MissingPath;
            dropboxFolderPathLabel.Text =
                (string)Registry.GetValue(Properties.Resources.RegistryPath, Properties.Resources.DropboxFolderPath, null) ??
                Properties.Resources.MissingPath;

            // Load values to checkbox
            uploadTypeCheckbox.Items.Clear();
            foreach (FileAndFolderHandlerUploadType type in Enum.GetValues(typeof(FileAndFolderHandlerUploadType)))
            {
                uploadTypeCheckbox.Items.Add(type.ToString());
            }

            int index = (int)Registry.GetValue(Properties.Resources.RegistryPath, Properties.Resources.UploadTypeIndex, 0);
            if (uploadTypeCheckbox.Items.Count != 0 && index >= uploadTypeCheckbox.Items.Count)
            {
                MessageBox.Show("No compression methods!", "Warning");
            }
            else
            {
                uploadTypeCheckbox.SelectedIndex = index;
            }
        }

        private void uploadTypeCheckbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Registry.SetValue(Properties.Resources.RegistryPath, Properties.Resources.UploadTypeIndex, uploadTypeCheckbox.SelectedIndex);
            Registry.SetValue(Properties.Resources.RegistryPath, Properties.Resources.UploadType, uploadTypeCheckbox.SelectedItem.ToString());
            foreach (FileAndFolderHandlerUploadType type in Enum.GetValues(typeof(FileAndFolderHandlerUploadType)))
            {
                if (type.ToString() == uploadTypeCheckbox.SelectedItem.ToString() && type.GetHashCode() == 1)
                {
                    compressionApplicationFolderPathLabel.Enabled = true;
                    compressionApplicationFolderPathLabel.Text = (string)Registry.GetValue(Properties.Resources.RegistryPath, type.ToString(), Properties.Resources.MissingPath);
                    chooseCompressionAppFolderBtn.Enabled = true;
                }
                else
                {
                    compressionApplicationFolderPathLabel.Enabled = false;
                    compressionApplicationFolderPathLabel.Text = Properties.Resources.CompressionAppText;
                    chooseCompressionAppFolderBtn.Enabled = false;
                }
            }
        }

    }
}
