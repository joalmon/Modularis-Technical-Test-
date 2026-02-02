using ModularisTest.Enums;

namespace ModularisTest.Interfaces
{
    public interface ILogStrategy
    {
        void Log(string message, MessageType type);
    }
}
