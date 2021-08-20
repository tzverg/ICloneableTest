namespace AR_496
{
    public class MessageHandler : IHandler
    {
        public void Message(string value)
        {
            System.Console.WriteLine($"{this.ToString()} : {value}");
        }
    }
}