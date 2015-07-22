using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace DropboxWrapper
{
    public partial class Context : ApplicationContext
    {

        private NotifyIcon notifyIcon;

        private FileSystemWatcher fileWatcher;

        public Context()
        {
            // Items for notify icon
            MenuItem selectPaths = new MenuItem("Select paths", new EventHandler(delegate(object o, EventArgs e)
                {
                    SelectPaths sp = new SelectPaths();
                    sp.ShowDialog();
                }));
            MenuItem exit = new MenuItem("Exit", new EventHandler(delegate (object o, EventArgs e)
                {
                    Environment.Exit(1);
                }));

            // Initializing notify icon
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = Properties.Resources.DBW_icon;
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] {selectPaths, exit});
            notifyIcon.Visible = true;
            notifyIcon.DoubleClick += new EventHandler(OpenWrapFolder);

            // Check if it's in startup folder. If not add it!
            if (Registry.GetValue(Properties.Resources.RegistryStartupPath, Properties.Resources.AppName, null) == null)
            {
                Registry.SetValue(Properties.Resources.RegistryStartupPath, Properties.Resources.AppName, Application.ExecutablePath.ToString());
            }

            // Check if all requred paths are valid.
            if (!Directory.Exists((string)Registry.GetValue(Properties.Resources.RegistryPath, Properties.Resources.WrapFolderPath, null)) ||
                !Directory.Exists((string)Registry.GetValue(Properties.Resources.RegistryPath, Properties.Resources.DropboxFolderPath, null)) ||
                (string)Registry.GetValue(Properties.Resources.RegistryPath, Properties.Resources.UploadType, null) == null)
            {
                SelectPaths sp = new SelectPaths();
                sp.ShowDialog();
            }
            else
            {
                // initializing file watcher
                fileWatcher = new FileSystemWatcher();
                fileWatcher.Path = (string)Registry.GetValue(Properties.Resources.RegistryPath, Properties.Resources.WrapFolderPath, null);
                fileWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Attributes | 
                    NotifyFilters.DirectoryName | NotifyFilters.FileName;
                fileWatcher.Filter = "*.*";
                fileWatcher.IncludeSubdirectories = true;
                fileWatcher.Changed += new FileSystemEventHandler(UploadToDropbox);
                fileWatcher.Created += new FileSystemEventHandler(UploadToDropbox);
                fileWatcher.Deleted += new FileSystemEventHandler(DeleteFromDropbox);
                fileWatcher.Renamed += new RenamedEventHandler(RenameOnDropbox);

                fileWatcher.EnableRaisingEvents = true;

                string uploadType = (string)Registry.GetValue(Properties.Resources.RegistryPath, Properties.Resources.UploadType, "");
                try
                {
                    if (Enum.GetValues(typeof(FileAndFolderHandlerUploadType)).Length > 0)
                    {
                        bool missingUploadType = true;
                        foreach (FileAndFolderHandlerUploadType type in Enum.GetValues(typeof(FileAndFolderHandlerUploadType)))
                        {
                            if (type.ToString() == uploadType)
                            {
                                missingUploadType = false;
                                break;
                            }
                        }
                        if (missingUploadType)
                        {
                            throw new Exception();
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    // This is bad because there's no compression method to use in our Dropbox wrapper.
                    MessageBox.Show("It looks there is no compression method!!!", "Big error!");
                    Environment.Exit(1);
                }
            }
        }

        /// <summary>
        /// Open user defined wrap folder.
        /// </summary>
        private void OpenWrapFolder(Object sender, EventArgs e)
        {
            if (Directory.Exists((string)Registry.GetValue(Properties.Resources.RegistryPath, Properties.Resources.WrapFolderPath, null)))
            {
                Process.Start(new ProcessStartInfo()
                {
                    FileName = (string)Registry.GetValue(Properties.Resources.RegistryPath, Properties.Resources.WrapFolderPath, null),
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
        }

        /// <summary>
        /// Called whenever file or directory is changed in user defined wrap folder.
        /// </summary>
        private void UploadToDropbox(object sender, FileSystemEventArgs e)
        {
            if (FileAndFolderHandler.PathIsDirectory(e.FullPath))
            {
                FileAndFolderHandler.UploadFolder(e.FullPath);
            }
            else
            {
                FileAndFolderHandler.UploadFile(e.FullPath, (FileAndFolderHandlerUploadType)Enum.Parse(typeof(FileAndFolderHandlerUploadType), 
                    (string)Registry.GetValue(Properties.Resources.RegistryPath, 
                    Properties.Resources.UploadType, FileAndFolderHandlerUploadType.ZIP.ToString())));
            }
        }

        /// <summary>
        /// Called whenever file or directory is deleted in user defined wrap folder.
        /// </summary>
        private void DeleteFromDropbox(object sender, FileSystemEventArgs e)
        {
            if (Directory.Exists(FileAndFolderHandler.ConvertWrapFolderPathToDropboxFolderPath(e.FullPath, true)))
            {
                FileAndFolderHandler.DeleteFolder(e.FullPath);
            }
            else
            {
                FileAndFolderHandler.DeleteFile(e.FullPath);
            }
        }

        /// <summary>
        /// Called whenever file or directory is renamed in user defined wrap folder.
        /// </summary>
        private void RenameOnDropbox(object sender, RenamedEventArgs e)
        {
            if (FileAndFolderHandler.PathIsDirectory(e.FullPath))
            {
                FileAndFolderHandler.RenameFolder(e.OldFullPath, e.FullPath);
            }
            else
            {
                FileAndFolderHandler.RenameFile(e.OldFullPath, e.FullPath, (FileAndFolderHandlerUploadType)Enum.Parse(typeof(FileAndFolderHandlerUploadType),
                    (string)Registry.GetValue(Properties.Resources.RegistryPath,
                    Properties.Resources.UploadType, FileAndFolderHandlerUploadType.ZIP.ToString())));
            }
        }

    }
}
