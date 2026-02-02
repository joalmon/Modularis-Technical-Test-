using ModularisTest.Enums;
using ModularisTest.Interfaces;
using System;
using System.Configuration;
using System.IO;

namespace ModularisTest
{
    public class FileLogStrategy : ILogStrategy
    {
        private readonly string _logDirectory;

        public FileLogStrategy(string logDirectory)
        {
            _logDirectory = logDirectory ?? Environment.CurrentDirectory;
        }

        public void Log(string message, MessageType type)
        {
            string fileName = "LogFile" + DateTime.Now.ToString("yyyy.MM.dd") + ".txt";
            string fullPath = Path.Combine(_logDirectory, fileName);

            string logLine = $"{DateTime.Now.ToShortDateString()} {type}: {message}{Environment.NewLine}";

            if (!Directory.Exists(_logDirectory))
            {
                Directory.CreateDirectory(_logDirectory);
            }

            File.AppendAllText(fullPath, logLine);
        }
    }
}
