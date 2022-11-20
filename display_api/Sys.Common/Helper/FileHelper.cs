using Sys.Common.Logs;
using System;
using System.IO;

namespace Sys.Common.Helper
{
    public static class FileHelper
    {
        private static nProxLog log = new nProxLog();

        public static string ReadFileText(string path)
        {
            string result = null;
            try
            {
                result = File.ReadAllText(path);
            }
            catch (Exception ex)
            {
                log.Log(LogConstants.LogType.Error, ex.Message);
            }
            return result;
        }

        public static void CreateFile(string path, string content)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    File.Create(path).Dispose();
                    using (TextWriter tw = new StreamWriter(path))
                    {
                        tw.Write(content);
                    }
                }
                else
                {
                    File.WriteAllText(path, string.Empty);
                    using (TextWriter tw = new StreamWriter(path))
                    {
                        tw.Write(content);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Log(LogConstants.LogType.Error, ex.Message);
            }
        }

        public static bool ExistedFile(string path)
        {
            return File.Exists(path);
        }
    }
}