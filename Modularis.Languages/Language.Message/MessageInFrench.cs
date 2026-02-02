namespace Language.Message
{
    public class MessageInFrench : Interface.IMessage
    {
        public string Description
        {
            get { return "French"; }
        }

        public string Message
        {
            get { return "Bonjour"; }
        }
    }
}