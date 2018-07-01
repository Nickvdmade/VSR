namespace VSRapp
{
    public class Probe : Node
    {
        public Probe()
        {
        }

        public override string getKey()
        {
            return "PROBE";
        }

        public override object Clone()
        {
            return new Probe();
        }
    }
}