using System;

namespace VSRapp
{
    public class Or : Node
    {
        public Or()
        {
        }

        public override string getKey()
        {
            return "OR";
        }

        public override Uri getUri()
        {
            return new Uri(@"Nodes/Images/OR.png", UriKind.Relative);
        }

        public override object Clone()
        {
            return new Or();
        }
    }
}