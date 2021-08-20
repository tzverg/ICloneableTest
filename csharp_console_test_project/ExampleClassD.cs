namespace Testnamespace
{
    [CloneableValidation]
    public class ExampleClassD
    {
        public string dStringValue = "default value D";
        protected bool cBoolValue = false;
        internal int cIntValue = 400;
        private float cFloatValue = 600.3F;
        protected internal double cDoubleValue = 200.2;

        public ExampleClassC exampleClassC;

        public ExampleClassD()
        {
            // Console.WriteLine($"created " + this.ToString());
        }
    }
}