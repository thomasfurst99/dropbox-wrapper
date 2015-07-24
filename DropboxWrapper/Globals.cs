namespace DropboxWrapper
{
	public static class Globals
    {
        private static string dropboxPath = null;
        private static string wrapPath = null;

        public static string DropboxPath
        {
            get { return dropboxPath; }
            set { dropboxPath = value; }
        }

        public static string WrapPath
        {
            get { return wrapPath; }
            set { wrapPath = value; }
        }
    }
}
