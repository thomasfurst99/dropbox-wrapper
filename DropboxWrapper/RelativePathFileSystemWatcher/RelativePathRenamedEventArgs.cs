using System.IO;

namespace DropboxWrapper
{
	public class RelativePathRenamedEventArgs
	{
		private RenamedEventArgs baseArgs;

		private string oldRelativePath;
		private string relativePath;

		public RelativePathRenamedEventArgs(RenamedEventArgs baseArgs, string oldRelativePath, string relativePath)
		{
			this.baseArgs = baseArgs;
			this.oldRelativePath = oldRelativePath;
			this.relativePath = relativePath;
		}

		public string OldRelativePath
		{
			get { return oldRelativePath; }
		}

		public string RelativePath
		{
			get { return relativePath; }
		}

		public string OldFullPath
		{
			get { return baseArgs.OldFullPath; }
		}

		public string FullPath
		{
			get { return baseArgs.FullPath; }
		}

		public string OldName
		{
			get { return baseArgs.OldName; }
		}

		public string Name
		{
			get { return baseArgs.Name; }
		}

		public WatcherChangeTypes ChangeType
		{
			get { return baseArgs.ChangeType; }
		}

		public RenamedEventArgs BaseArgs
		{
			get { return baseArgs; }
		}
	}
}
