namespace Testnamespace
{
    public class ExampleClassC
    {
        public string cStringValue = "default value C";
        protected bool cBoolValue = false;
        internal int cIntValue = 40;
        private float cFloatValue = 60.3F;
        protected internal double cDoubleValue = 20.2;

        public DelegateMessage messageC;

        public ExampleClassC()
        {
            // Console.WriteLine($"created " + this.ToString());
            messageC = MessageC;
        }

        private string MessageC()
        {
            string messageString = $"print MessageC, created in " + this.ToString();
            System.Console.WriteLine(messageString);
            return messageString;
        }
    }
}