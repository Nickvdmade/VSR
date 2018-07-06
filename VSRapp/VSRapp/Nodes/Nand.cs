using System;

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

        public override Uri getUri()
        {
            return new Uri(@"Nodes/Images/NAND.png", UriKind.Relative);
        }

        public override object Clone()
        {
            return new Nand();
        }
    }
}