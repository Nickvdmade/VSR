namespace VSRapp
{
    public class And : Node
    {
        public And()
        {
        }

        public override string getKey()
        {
            return "And";
        }

        public override object Clone()
        {
            return new And();
        }
    }
}