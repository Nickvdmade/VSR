namespace VSRapp
{
    public class Not : Node
    {
        public Not()
        {
        }

        public override string getKey()
        {
            return "Not";
        }

        public override object Clone()
        {
            return new Not();
        }
    }
}