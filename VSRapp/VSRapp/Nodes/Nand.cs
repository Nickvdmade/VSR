namespace VSRapp
{
    public class Nand : Node
    {
        public Nand()
        {
        }

        public override string getKey()
        {
            return "NAND";
        }

        public override object Clone()
        {
            return new Nand();
        }
    }
}