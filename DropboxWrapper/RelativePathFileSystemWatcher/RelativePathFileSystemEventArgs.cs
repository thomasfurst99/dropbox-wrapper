using System.IO;

namespace DropboxWrapper
{
	public class RelativePathFileSystemEventArgs
    {
        private FileSystemEventArgs baseArgs;

        private string relativePath;

		public RelativePathFileSystemEventArgs(FileSystemEventArgs baseArgs, string relativePath)
		{
			this.baseArgs = baseArgs;
			this.relativePath = relativePath;
		}

		public string RelativePath
        {
            get { return relativePath; }
        }

        public string FullPath
        {
            get { return baseArgs.FullPath; }
        }

        public string Name
        {
            get { return baseArgs.Name; }
        }

        public WatcherChangeTypes ChangeType
        {
            get { return baseArgs.ChangeType; }
        }

        public FileSystemEventArgs BaseArgs
        {
            get { return baseArgs; }
        }
    }
}
