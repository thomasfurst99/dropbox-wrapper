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

		private RelativePathFileSystemWatcher fileWatcher;

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

            MenuItem[] menuItems = { selectPaths, exit };
            notifyIcon.ContextMenu = new ContextMenu(menuItems);

            notifyIcon.Visible = true;

            notifyIcon.DoubleClick += new EventHandler(OpenWrapFolder);

            // If this is first run
            if (Utils.RegistryGet<string>(Properties.Resources.FirstRun) == null)
            {
                Utils.RegistrySet(Properties.Resources.FirstRun, "Program is not running for the first time.");

                // Set defaults for registers
                Utils.RegistrySet(Properties.Resources.DropboxFolderPath, null);
                Utils.RegistrySet(Properties.Resources.WrapFolderPath, null);

                string zipType = FileAndFolderHandlerUploadType.ZIP.ToString();
                Utils.RegistrySet(Properties.Resources.UploadType, zipType);

                Utils.RegistrySet(Properties.Resources.UploadTypeIndex, 0);
            }

            string inStartup = Utils.RegistryGet<string>(Properties.Resources.AppName);

            // Check if it's in startup folder. If not add it!
            if (inStartup == null)
            {
                string applicationExecutable = Application.ExecutablePath.ToString();
                Utils.RegistrySet(Properties.Resources.AppName, applicationExecutable);
            }

            // Setting values for Globals
            Globals.DropboxPath = Utils.RegistryGet<string>(Properties.Resources.DropboxFolderPath);
            Globals.WrapPath = Utils.RegistryGet<string>(Properties.Resources.WrapFolderPath);

            string dbPath = Globals.DropboxPath;
            string wrapPath = Globals.WrapPath;
            string uploadType = Utils.RegistryGet<string>(Properties.Resources.UploadType);

            // Check if all requred paths are valid.
            if (!Directory.Exists(dbPath) || !Directory.Exists(wrapPath) || uploadType == null)
            {
                SelectPaths sp = new SelectPaths();
                sp.ShowDialog();
            }
            else
            {
				// initializing file watcher
				fileWatcher = new RelativePathFileSystemWatcher(wrapPath);
				fileWatcher.Changed += new RelativePathFileSystemEventHandler(UploadToDropbox);
				fileWatcher.Created += new RelativePathFileSystemEventHandler(UploadToDropbox);
				fileWatcher.Deleted += new RelativePathFileSystemEventHandler(DeleteFromDropbox);
				fileWatcher.Renamed += new RelativePathRenamedEventHandler(RenameOnDropbox);

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

				// Upload missing files and delete indwelling files
				FileAndFolderHandler.DeleteIndwellingFiles();
				FileAndFolderHandler.UploadMissingFiles();
            }
        }

        private void OpenWrapFolder(Object sender, EventArgs e)
        {
            string wrapPath = Globals.WrapPath; 
            if (Directory.Exists(wrapPath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo("explorer.exe");
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                startInfo.Arguments = wrapPath;
                startInfo.Verb = "open";

                try
                {
                    Process.Start(startInfo);
                }
                catch
                {

                }
            }
        }

		private void UploadToDropbox(object sender, RelativePathFileSystemEventArgs e)
        {
            if (FileAndFolderHandler.PathIsDirectoryInWrap(e.RelativePath))
            {
				FileAndFolderHandler.UploadFolder(e.RelativePath);
            }
            else
            {
                string zipType = FileAndFolderHandlerUploadType.ZIP.ToString();
                string uploadType = Utils.RegistryGet<string>(Properties.Resources.UploadType, zipType);

                FileAndFolderHandlerUploadType type = 
                    (FileAndFolderHandlerUploadType)Enum.Parse(typeof(FileAndFolderHandlerUploadType), uploadType);

                FileAndFolderHandler.UploadFile(e.RelativePath, type);
            }
        }

        private void DeleteFromDropbox(object sender, RelativePathFileSystemEventArgs e)
		{
			if (FileAndFolderHandler.PathIsDirectoryInDropbox(e.RelativePath))
            {
                FileAndFolderHandler.DeleteFolder(e.RelativePath);
            }
            else
            {
				FileAndFolderHandler.DeleteFile(e.RelativePath);
            }
        }

		private void RenameOnDropbox(object sender, RelativePathRenamedEventArgs e)
        {
            if (FileAndFolderHandler.PathIsDirectoryInWrap(e.RelativePath))
            {
                FileAndFolderHandler.RenameFolder(e.OldRelativePath, e.RelativePath);
            }
            else
            {
                string zipType = FileAndFolderHandlerUploadType.ZIP.ToString();
                string uploadType = Utils.RegistryGet<string>(Properties.Resources.UploadType, zipType);

                FileAndFolderHandlerUploadType type =
                    (FileAndFolderHandlerUploadType)Enum.Parse(typeof(FileAndFolderHandlerUploadType), uploadType);

                FileAndFolderHandler.RenameFile(e.OldRelativePath, e.RelativePath, type);
            }
        }

    }
}
