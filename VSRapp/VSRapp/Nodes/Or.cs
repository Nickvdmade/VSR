namespace VSRapp
{
    public class Or : Node
    {
        public Or()
        {
        }

        public override string getKey()
        {
            return "Or";
        }

        public override object Clone()
        {
            return new Or();
        }
    }
}