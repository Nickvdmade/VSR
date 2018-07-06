using System;

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

        public override Uri getUri()
        {
            return new Uri(@"Nodes/Images/NOR.png", UriKind.Relative);
        }

        public override object Clone()
        {
            return new Nor();
        }
    }
}