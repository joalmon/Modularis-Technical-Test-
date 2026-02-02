using ModularisTest.Enums;
using ModularisTest.Interfaces;
using System;

namespace ModularisTest
{
    public class ConsoleLogStrategy : ILogStrategy
    {
        public void Log(string message, MessageType type)
        {
            switch (type)
            {
                case MessageType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case MessageType.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case MessageType.Message:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            Console.WriteLine($"{DateTime.Now.ToShortDateString()} {message}");
            Console.ResetColor();
        }
    }
}
