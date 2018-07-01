namespace VSRapp
{
    public class Nor : Node
    {
        public Nor()
        {
        }

        public override string getKey()
        {
            return "NOR";
        }

        public override object Clone()
        {
            return new Nor();
        }
    }
}