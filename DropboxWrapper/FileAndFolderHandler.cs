using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace DropboxWrapper
{
	public enum FileAndFolderHandlerUploadType : int
    {
        ZIP = 0,
        WinRAR = 1
    }

    public static class FileAndFolderHandler
    {
		public static void UploadFile(string relativePath, FileAndFolderHandlerUploadType type)
		{
			switch (type)
			{
				case FileAndFolderHandlerUploadType.ZIP:
					UploadZipFile(relativePath);
					break;
				case FileAndFolderHandlerUploadType.WinRAR:
					UploadRarFile(relativePath);
					break;
				default:
					break;
			}
		}

		public static void DeleteFile(string relativePath)
		{
			string fullPath = Globals.DropboxPath + relativePath;
			string dirName = Path.GetDirectoryName(fullPath);

			string noExt = Path.GetFileNameWithoutExtension(fullPath);
			string pattern = string.Format("{0}.*", noExt);

			string[] files = Directory.GetFiles(dirName, pattern, SearchOption.AllDirectories);

			foreach (string file in files)
			{
				File.Delete(file);
			}
		}

		public static void RenameFile(string oldRelativePath, string newRelativePath, FileAndFolderHandlerUploadType type)
		{
			switch (type)
			{
				case FileAndFolderHandlerUploadType.ZIP:
					RenameZipFile(oldRelativePath, newRelativePath);
					break;
				case FileAndFolderHandlerUploadType.WinRAR:
					RenameRarFile(oldRelativePath, newRelativePath);
					break;
				default:
					break;
			}
		}

		public static void UploadFolder(string relativePath)
		{
			string fullPath = Globals.DropboxPath + relativePath;
			Directory.CreateDirectory(fullPath);
		}

		public static void DeleteFolder(string relativePath)
		{
			string fullPath = Globals.DropboxPath + relativePath;
			Directory.Delete(fullPath, true);
		}

		public static void RenameFolder(string oldRelativePath, string newRelativePath)
		{
			string fullOldPath = Globals.DropboxPath + oldRelativePath;
			string fullNewPath = Globals.DropboxPath + newRelativePath;

			Directory.Move(fullOldPath, fullNewPath);
		}

		public static bool PathIsDirectoryInWrap(string relativePath)
		{
			string fullPath = Globals.WrapPath + relativePath;
			return Directory.Exists(fullPath);
		}

		public static bool PathIsDirectoryInDropbox(string relativePath)
		{
			string fullPath = Globals.DropboxPath + relativePath;
			return Directory.Exists(fullPath);
		}

		public static void UploadMissingFiles()
		{
			string wrapPath = Globals.WrapPath;
			string dropboxPath = Globals.DropboxPath;

			string filePattern = "*.*";
			string folderPattern = "*";

			string zipType = FileAndFolderHandlerUploadType.ZIP.ToString();
			string uploadType = Utils.RegistryGet<string>(Properties.Resources.UploadType, zipType);

			FileAndFolderHandlerUploadType type =
					(FileAndFolderHandlerUploadType)Enum.Parse(typeof(FileAndFolderHandlerUploadType), uploadType);

			string[] files = Directory.GetFiles(wrapPath, filePattern, SearchOption.AllDirectories);
			string[] dirs = Directory.GetDirectories(wrapPath, folderPattern, SearchOption.AllDirectories);

			foreach (string file in files)
			{
				string relativePath = Utils.GetRelativePath(file, wrapPath);
				string fullDropboxPath = dropboxPath + relativePath;
				string ext = ".rar";

				string fullDropboxPathWithoutExt = Path.ChangeExtension(fullDropboxPath, ext);

				if (!File.Exists(fullDropboxPathWithoutExt))
				{
					UploadFile(relativePath, type);
				}
			}

			foreach (string dir in dirs)
			{
				string relativePath = Utils.GetRelativePath(dir, wrapPath);
				string fullDropboxPath = dropboxPath + relativePath;

				if (!Directory.Exists(fullDropboxPath))
				{
					UploadFolder(relativePath);
				}
			}
		}

		public static void DeleteIndwellingFiles()
		{
			string wrapPath = Globals.WrapPath;
			string dropboxPath = Globals.DropboxPath;

			string filePattern = "*.*";
			string folderPattern = "*";

			string[] files = Directory.GetFiles(dropboxPath, filePattern, SearchOption.AllDirectories);
			string[] dirs = Directory.GetDirectories(dropboxPath, folderPattern, SearchOption.AllDirectories);

			foreach (string file in files)
			{
				string parentFolder = Path.GetDirectoryName(file);
				string filenameWithoutExt = Path.GetFileNameWithoutExtension(file);
				string pattern = filenameWithoutExt + ".*";

				string parrentRelativePath = Utils.GetRelativePath(parentFolder, dropboxPath);
				string fullWrapPath = wrapPath + parrentRelativePath;

				string[] filesFound = Directory.GetFiles(fullWrapPath, pattern, SearchOption.TopDirectoryOnly);

				if (filesFound.Length != 1)
				{
					File.Delete(file);
				}
			}

			foreach (string dir in dirs)
			{
				string parentFolder = Path.GetDirectoryName(dir);
				string folderName = Path.GetFileName(dir);

				string parentRelativPath = Utils.GetRelativePath(parentFolder, dropboxPath);
				string fullWrapPath = wrapPath + parentRelativPath;

				string[] dirsFound = Directory.GetDirectories(fullWrapPath, folderName, SearchOption.TopDirectoryOnly);

				if (dirsFound.Length != 1)
				{
					Directory.Delete(dir);
				}
			}
		}

		private static void UploadZipFile(string relativePath)
		{
			string fullWrapPath = Globals.WrapPath + relativePath;
			string fullPath = Globals.DropboxPath + relativePath;

			string zipPath = Path.ChangeExtension(fullPath, ".zip");

			using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Create))
			{
				string fileExt = Path.GetExtension(fullPath);
				string entryName = "file" + fileExt;

				archive.CreateEntryFromFile(fullWrapPath, entryName);
			}
		}

		private static void UploadRarFile(string relativePath)
		{
			string rarExt = ".rar";

			string fullWrapPath = Globals.WrapPath + relativePath;
			string fullPath = Globals.DropboxPath + relativePath;

			string rarFilePath;

			if (Path.GetExtension(fullPath) == "")
			{
				rarFilePath = fullPath + rarExt;
			}
			else
			{
				rarFilePath = Path.ChangeExtension(fullPath, rarExt);
            }

            string rarType = FileAndFolderHandlerUploadType.WinRAR.ToString();
			string rarPath = Utils.RegistryGet<string>(rarType);

			string processStartInfo = "\"" + rarPath + "\\rar.exe" + "\"";
			string arguments = " a -ep \"" + rarFilePath + "\" \"" + fullWrapPath + "\"";

			File.Delete(rarFilePath);
			ProcessStartInfo startInfo = new ProcessStartInfo(processStartInfo);
			startInfo.WindowStyle = ProcessWindowStyle.Hidden;
			startInfo.Arguments = arguments;

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
		}

		private static void RenameZipFile(string oldRelativePath, string newRelativePath)
		{
			string fullOldPath = Globals.DropboxPath + oldRelativePath;
			string fullNewPath = Globals.DropboxPath + newRelativePath;

			File.Move(fullOldPath, fullNewPath);
		}

		private static void RenameRarFile(string oldRelativePath, string newRelativePath)
		{
			string fullOldWrapPath = Globals.WrapPath + oldRelativePath;
			string fullNewWrapPath = Globals.WrapPath + newRelativePath;

			string fullOldPath = Globals.DropboxPath + oldRelativePath;
			string fullNewPath = Globals.DropboxPath + newRelativePath;

			File.Move(fullOldPath, fullNewPath);

			// Rename file in archive
			string rarType = FileAndFolderHandlerUploadType.WinRAR.ToString();
			string rarPath = Utils.RegistryGet<string>(rarType);

			string fileName = Path.GetFileName(fullOldWrapPath);
			string newFileName = Path.GetFileName(fullNewWrapPath);

			string processStartInfo = rarPath + "\\rar.exe";
			string arguments = "rn \"" + fullNewPath + "\" \"" + fileName + "\" \"" + newFileName + "\"";

			ProcessStartInfo startInfo = new ProcessStartInfo(processStartInfo);
			startInfo.WindowStyle = ProcessWindowStyle.Hidden;
			startInfo.Arguments = arguments;

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
		}
    }
}
