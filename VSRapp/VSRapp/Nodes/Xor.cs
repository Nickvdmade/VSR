using System;

namespace VSRapp
{
    public class Xor : Node
    {
        public Xor()
        {
        }

        public override string getKey()
        {
            return "XOR";
        }

        public override Uri getUri()
        {
            return new Uri(@"Nodes/Images/XOR.png", UriKind.Relative);
        }

        public override object Clone()
        {
            return new Xor();
        }
    }
}