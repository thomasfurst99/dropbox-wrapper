using System.IO;

namespace DropboxWrapper
{
	public delegate void RelativePathFileSystemEventHandler(object sender, RelativePathFileSystemEventArgs e);
    public delegate void RelativePathRenamedEventHandler(object sender, RelativePathRenamedEventArgs e);

    public class RelativePathFileSystemWatcher
    {
        public event RelativePathFileSystemEventHandler Created;
        public event RelativePathFileSystemEventHandler Changed;
        public event RelativePathFileSystemEventHandler Deleted;
        public event RelativePathRenamedEventHandler Renamed;

        private FileSystemWatcher fileSystemWatcher;
        private string wrapPath;

        public RelativePathFileSystemWatcher(string path, string filter = "*.*")
        {
            fileSystemWatcher = new FileSystemWatcher(path, filter);

            fileSystemWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Attributes |
                    NotifyFilters.DirectoryName | NotifyFilters.FileName;
            fileSystemWatcher.IncludeSubdirectories = true;

            fileSystemWatcher.Created += new FileSystemEventHandler(OnCreated);
            fileSystemWatcher.Changed += new FileSystemEventHandler(OnChanged);
            fileSystemWatcher.Deleted += new FileSystemEventHandler(OnDeleted);
            fileSystemWatcher.Renamed += new RenamedEventHandler(OnRenamed);

            fileSystemWatcher.EnableRaisingEvents = true;

            wrapPath = Globals.WrapPath;
        }

		public FileSystemWatcher FileWatcher
		{
			get { return fileSystemWatcher; }
		}

        private void OnCreated(object sender, FileSystemEventArgs e)
		{
			string path = e.FullPath;
			string root = Globals.WrapPath;

			string relativePath = Utils.GetRelativePath(path, root);

			RelativePathFileSystemEventHandler handler = Created;

			if (handler != null)
			{
				RelativePathFileSystemEventArgs args = new RelativePathFileSystemEventArgs(e, relativePath);
				handler(sender, args);
			}
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
			string path = e.FullPath;
			string root = Globals.WrapPath;

			string relativePath = Utils.GetRelativePath(path, root);

			RelativePathFileSystemEventHandler handler = Changed;

			if (handler != null)
			{
				RelativePathFileSystemEventArgs args = new RelativePathFileSystemEventArgs(e, relativePath);
				handler(sender, args);
			}
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
			string path = e.FullPath;
			string root = Globals.WrapPath;

			string relativePath = Utils.GetRelativePath(path, root);

			RelativePathFileSystemEventHandler handler = Deleted;

			if (handler != null)
			{
				RelativePathFileSystemEventArgs args = new RelativePathFileSystemEventArgs(e, relativePath);
				handler(sender, args);
			}
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
			string path = e.FullPath;
			string oldPath = e.OldFullPath;

			string root = Globals.WrapPath;

			string relativePath = Utils.GetRelativePath(path, root);
			string oldRelativePath = Utils.GetRelativePath(oldPath, root);

			RelativePathRenamedEventHandler handler = Renamed;

			if (handler != null)
			{
				RelativePathRenamedEventArgs args = new RelativePathRenamedEventArgs(e, oldRelativePath, relativePath);
				handler(sender, args);
			}
        }
    }
}
