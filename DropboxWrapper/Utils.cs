using Microsoft.Win32;
using System;

namespace DropboxWrapper
{
    public static class Utils
    {
        public static T RegistryGet<T>(string key, object defaultValue = null)
        {
            try
            {
                T result = (T)Registry.GetValue(Properties.Resources.RegistryPath, key, defaultValue);
                return result;
            }
            catch (NullReferenceException)
            {
                return (T)defaultValue;
            }
        }

        public static void RegistrySet(string key, object value)
        {
            Registry.SetValue(Properties.Resources.RegistryPath, key, value);
        }

        public static string GetRelativePath(string path, string root)
        {
            string relativePath = path.Replace(root, "");

            return relativePath;
        }
    }
}
