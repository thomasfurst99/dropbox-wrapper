using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace DropboxWrapper
{


    /// <summary>
    /// Use 0 if path is not needed, 1 otherwise
    /// </summary>
    public enum FileAndFolderHandlerUploadType: int
    {
        ZIP = 0,
        WinRAR = 1
    }

    public static class FileAndFolderHandler
    {
        /// <summary>
        /// Add (upload) new file to the user defined Dropbox folder.
        /// </summary>
        /// <param name="path">Path to the file in wrap folder.</param>
        public static void UploadFile(string path, FileAndFolderHandlerUploadType type)
        {
            switch(type)
            {
                case FileAndFolderHandlerUploadType.ZIP:
                    string zipPath = ConvertWrapFolderPathToDropboxFolderPath(path);
                    using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Create))
                    {
                        archive.CreateEntryFromFile(path, "file" + Path.GetExtension(path));
                    }
                    break;
                case FileAndFolderHandlerUploadType.WinRAR:
                    string rarPath = ConvertWrapFolderPathToDropboxFolderPath(path, false, ".rar");
                    File.Delete(rarPath);
                    ProcessStartInfo startInfo = new ProcessStartInfo((string)Registry.GetValue(Properties.Resources.RegistryPath, type.ToString(), null) + "\\rar.exe");
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.Arguments = "a -ep \"" + rarPath + "\" \"" + path + "\"";
                    try
                    {
                        using (Process exeProcess = Process.Start(startInfo))
                        {
                            exeProcess.WaitForExit();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Something goes wrong while RARing your file! We will fix it later.");
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Delete file from the user defined Dropbox folder.
        /// </summary>
        /// <param name="path">Path to file in wrap folder.</param>
        public static void DeleteFile(string path)
        {
            string[] files = Directory.GetFiles(Path.GetDirectoryName(ConvertWrapFolderPathToDropboxFolderPath(path)), 
                string.Format("{0}.*", Path.GetFileNameWithoutExtension(ConvertWrapFolderPathToDropboxFolderPath(path))), 
                SearchOption.AllDirectories);
            foreach (string file in files)
            {
                File.Delete(file);
            }
        }

        /// <summary>
        /// Rename file in the user defined Dropbox folder.
        /// </summary>
        /// <param name="path">Path to the file in the wrap folder.</param>
        /// <param name="newName">File will be renamed to this new name.</param>
        public static void RenameFile(string path, string newPath, FileAndFolderHandlerUploadType type = FileAndFolderHandlerUploadType.ZIP)
        {
            switch(type)
            {
                case FileAndFolderHandlerUploadType.ZIP:
                    File.Move(ConvertWrapFolderPathToDropboxFolderPath(path), ConvertWrapFolderPathToDropboxFolderPath(newPath));
                    break;
                case FileAndFolderHandlerUploadType.WinRAR:
                    File.Move(ConvertWrapFolderPathToDropboxFolderPath(path, false, ".rar"), ConvertWrapFolderPathToDropboxFolderPath(newPath, false, ".rar"));

                    // Rename file in archive
                    string rarPath = ConvertWrapFolderPathToDropboxFolderPath(newPath, false, ".rar");
                    ProcessStartInfo startInfo = new ProcessStartInfo((string)Registry.GetValue(Properties.Resources.RegistryPath, type.ToString(), null) + "\\rar.exe");
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.Arguments = "rn \"" + rarPath + "\" \"" + Path.GetFileName(path) + "\" \"" + Path.GetFileName(newPath) + "\"";
                    try
                    {
                        using (Process exeProcess = Process.Start(startInfo))
                        {
                            exeProcess.WaitForExit();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Something goes wrong while RARing your file! We will fix it later.");
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Create a new folder in the user defined Dropbox folder.
        /// </summary>
        /// <param name="path">Path of the folder in user wrap folder.</param>
        public static void UploadFolder(string path)
        {
            Directory.CreateDirectory(ConvertWrapFolderPathToDropboxFolderPath(path, true));
        }

        /// <summary>
        /// Delete complete folder from the user defined Dropbox folder.
        /// </summary>
        /// <param name="path">Path of the folder to be deleted in user wrap folder.</param>
        public static void DeleteFolder(string path)
        {
            Directory.Delete(ConvertWrapFolderPathToDropboxFolderPath(path, true), true);
        }

        /// <summary>
        /// Rename specified folder in the user defined Dropbox folder.
        /// </summary>
        /// <param name="path">Path to the folder in user wrap folder</param>
        /// <param name="newName">New name of the folder.</param>
        public static void RenameFolder(string path, string newPath)
        {
            // Now using File.Move function. Maybe not the best option.
            Directory.Move(ConvertWrapFolderPathToDropboxFolderPath(path, true), ConvertWrapFolderPathToDropboxFolderPath(newPath, true));
        }

        /// <summary>
        /// Convert path from user defined wrap folder to user defined Dropbox folder path.
        /// </summary>
        /// <param name="path">Path in user defined wrap folder.</param>
        /// <returns>Returns path in user defined Dropbox folder.</returns>
        public static string ConvertWrapFolderPathToDropboxFolderPath(string path, bool directory = false, string extension = ".zip")
        {
            return Path.ChangeExtension((string)Registry.GetValue(Properties.Resources.RegistryPath, Properties.Resources.DropboxFolderPath, null) +
                path.Replace((string)Registry.GetValue(Properties.Resources.RegistryPath, Properties.Resources.WrapFolderPath, null), ""), directory ? "" : extension);
        }

        /// <summary>
        /// Check is specified path is directory or file.
        /// </summary>
        /// <param name="path">Path to be checked.</param>
        /// <returns>Returns true if path is directory, false otherwise.</returns>
        public static bool PathIsDirectory(string path)
        {
            FileAttributes attributes = File.GetAttributes(path);

            if (attributes.HasFlag(FileAttributes.Directory))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
