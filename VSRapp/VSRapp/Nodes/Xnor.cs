namespace VSRapp
{
    public class Xnor : Node
    {
        public Xnor()
        {
        }

        public override string getKey()
        {
            return "XNOR";
        }

        public override object Clone()
        {
            return new Xnor();
        }
    }
}