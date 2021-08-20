namespace Testnamespace
{
    class ExampleClassB
    {
        public string stringValue = "default value B";
        protected bool boolValue = true;
        internal int intValue = 4;
        private float floatValue = 6.3F;
        protected internal double doubleValue = 2.2;
        protected private byte PropertyValue { get; private set; }

        public DelegateMessage messageB;

        public ExampleClassB()
        {
            // Console.WriteLine($"created " + this.ToString());
            messageB = MessageB;
        }

        private string MessageB()
        {
            string messageString = $"print MessageB, created in " + this.ToString();
            System.Console.WriteLine(messageString);
            return messageString;
        }
    }
}