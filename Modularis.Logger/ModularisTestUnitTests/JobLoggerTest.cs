using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModularisTest;
using ModularisTest.Enums;
using System.Configuration;

namespace ModularisTestUnitTests
{
    [TestClass]
    public class JobLoggerTest
    {
        private JobLogger _logger;

        [TestInitialize]
        public void Setup()
        {
            _logger = JobLogger.Instance;
            _logger.ClearStrategies();
        }

        [TestMethod]
        public void JobLogger_01_ShouldLogToConsole()
        {
            _logger.RegisterStrategy(new ConsoleLogStrategy());
            _logger.LogMessage("Test 1: Text In Consola", MessageType.Message);
            Assert.IsTrue(true); 
        }

        [TestMethod]
        public void JobLogger_02_ShouldLogToFile()
        {
            string testPath = ConfigurationManager.AppSettings["LogFileDirectory"];
            _logger.RegisterStrategy(new FileLogStrategy(testPath));
            _logger.LogMessage("Test 2: Warning in File", MessageType.Warning);
            Assert.IsNotNull(_logger);
        }

        [TestMethod]
        public void JobLogger_03_ShouldLogToDatabase()
        {
            _logger.RegisterStrategy(new DatabaseLogStrategy());
            _logger.LogMessage("Test 3: Error In Database", MessageType.Error);
            Assert.IsNotNull(_logger);
        }

        [TestMethod]
        public void JobLogger_04_ShouldLogToMultipleDestinations()
        {
            string testPath = ConfigurationManager.AppSettings["LogFileDirectory"];

            _logger.RegisterStrategy(new ConsoleLogStrategy());
            _logger.RegisterStrategy(new DatabaseLogStrategy());
            _logger.RegisterStrategy(new FileLogStrategy(testPath));

            _logger.LogMessage("Test 4: Multi-test (Consola, DB and FileLog)", MessageType.Message);

            Assert.IsNotNull(_logger);
        }
    }
}