using System;

namespace VSRapp
{
    public class Xnor : Node
    {
        public Xnor()
        {
        }

        public override string getKey()
        {
            return "XNOR";
        }

        public override Uri getUri()
        {
            return new Uri(@"Nodes/Images/XNOR.png", UriKind.Relative);
        }

        public override object Clone()
        {
            return new Xnor();
        }
    }
}