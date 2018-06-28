namespace VSRapp
{
    public class Probe : Node
    {
        public Probe()
        {
        }

        public override string getKey()
        {
            return "Probe";
        }

        public override object Clone()
        {
            return new Probe();
        }
    }
}