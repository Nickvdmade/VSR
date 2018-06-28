namespace VSRapp
{
    public class InputHigh : Node
    {
        public InputHigh()
        {
        }

        public override string getKey()
        {
            return "InputHigh";
        }

        public override object Clone()
        {
            return new InputHigh();
        }
    }
}