// Logger Version 0.1 by JamPaul
// www.jampaul.xyz. Copyright all rights reserved.
// For more info visit https://jampaul.xyz/cs/logger
using System;
using System.IO;
namespace Logger
{
    public enum LogType
	{
        Info,
        Warning,
        Error
	}
    public static class Logger
    {
        private static string dir = AppDomain.CurrentDomain.BaseDirectory + "logs\\";
        private static  readonly string lattestFile = "lattest.log";
        public static string LattestFile { get { return Path.Combine(new string[] { dir, lattestFile }); } }
        private static bool _initialized = false;
        /// <summary>
        /// Initializes the Logger on the default folder of BaseDirectory\logs
        /// </summary>
        /// <remarks>
        /// Must always be called once.
        /// </remarks>
        public static void Initialize()
		{
            if (_initialized)
                throw new Exception("Logger has already be initialized!");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            if(File.Exists(LattestFile))
                ArchiveLattest();
            _initialized = true;
        }
        /// <summary>
        /// Initializes the Logger on a custom directory
        /// </summary>
        /// <param name="Folder">The folder of the logs</param>
        public static void Initialize(string Folder)
        {
            if (_initialized)
                throw new Exception("Logger has already be initialized!");
            dir = Folder;
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            if (File.Exists(LattestFile))
                ArchiveLattest();
            _initialized = true;
        }
        private static void ArchiveLattest()
		{
            
			if (File.Exists(LattestFile))
			{
                var data = File.ReadAllBytes(LattestFile);
                File.Delete(LattestFile);
                File.WriteAllBytes(Path.Combine(new string[] { dir, "log " + DateTime.Now.ToString("dd-mm-yy_hh-mm-ss") + ".old" }), data);
			}               
		}
        /// <summary>
        /// Log something to the lattest log LattestFile.
        /// </summary>
        /// <param name="message">Message of the log</param>
        /// <param name="type">Deufalt : LogType.Info</param>
        public static void Log(string message,LogType type = LogType.Info)
		{
            if (!_initialized)
                throw new Exception("Logger is not initialized.");
            File.AppendAllText(LattestFile, MakeString(message, type));
		}
        private static string MakeString(string message,LogType type)
		{
            string result = string.Empty;
            result = string.Format("[{0}][{1}] - {2}", type, DateTime.Now, message);
            return result;
		}
        /// <summary>
        /// Finalize function. Must be called before the program exists.
        /// </summary>
        public static void Finalize()
		{
            ArchiveLattest();
            _initialized = false;
        }
    }
}
