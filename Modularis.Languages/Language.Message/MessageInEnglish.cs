namespace Language.Message
{
    public class MessageInEnglish : Interface.IMessage
    {
        public string Description
        {
            get { return "English"; }
        }

        public string Message
        {
            get { return "Hello"; }
        }
    }
}