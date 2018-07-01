namespace VSRapp
{
    public class InputLow : Node
    {
        public InputLow()
        {
        }

        public override string getKey()
        {
            return "INPUTLOW";
        }

        public override object Clone()
        {
            return new InputLow();
        }
    }
}