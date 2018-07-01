namespace VSRapp
{
    public class Not : Node
    {
        public Not()
        {
        }

        public override string getKey()
        {
            return "NOT";
        }

        public override object Clone()
        {
            return new Not();
        }
    }
}