using ModularisTest.Enums;
using ModularisTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace ModularisTest
{
    public class JobLogger
    {
        private static JobLogger _instance;
        private static readonly object _lock = new object();
        private readonly List<ILogStrategy> _strategies;

        private bool _logError;
        private bool _logWarning;
        private bool _logMessage;

        private JobLogger()
        {
            _strategies = new List<ILogStrategy>();
            LoadConfiguration();
        }

        public static JobLogger Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ?? (_instance = new JobLogger());
                }
            }
        }

        private void LoadConfiguration()
        {
            _logError = GetConfigValue("LogError");
            _logWarning = GetConfigValue("LogWarning");
            _logMessage = GetConfigValue("LogMessage");
        }

        private bool GetConfigValue(string key)
        {
            return bool.TryParse(ConfigurationManager.AppSettings[key], out bool result) && result;
        }

        public void RegisterStrategy(ILogStrategy strategy)
        {
            if (strategy != null) _strategies.Add(strategy);
        }

        public void ClearStrategies()
        {
            _strategies.Clear();
        }

        public void LogMessage(string message, MessageType type)
        {
            if (string.IsNullOrWhiteSpace(message)) return;

            if (type == MessageType.Error && !_logError) return;
            if (type == MessageType.Warning && !_logWarning) return;
            if (type == MessageType.Message && !_logMessage) return;

            foreach (var strategy in _strategies)
            {
                strategy.Log(message.Trim(), type);
            }
        }
    }
}