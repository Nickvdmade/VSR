namespace VSRapp
{
    public class Nor : Node
    {
        public Nor()
        {
        }

        public override string getKey()
        {
            return "Nor";
        }

        public override object Clone()
        {
            return new Nor();
        }
    }
}