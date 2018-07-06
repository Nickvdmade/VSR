using System;

namespace VSRapp
{
    public class And : Node
    {
        public And()
        {
        }

        public override string getKey()
        {
            return "AND";
        }

        public override Uri getUri()
        {
            return new Uri(@"Nodes/Images/AND.png", UriKind.Relative);
        }

        public override object Clone()
        {
            return new And();
        }
    }
}