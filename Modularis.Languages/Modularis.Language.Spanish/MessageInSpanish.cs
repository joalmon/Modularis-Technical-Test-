using Language.Interface; 

namespace Modularis.Language.Spanish
{
    public class MessageInSpanish : IMessage
    {
        public string Description
        {
            get { return "Spanish"; }
        }
        public string Message
        {
            get { return "Hola"; }
        }
    }
}