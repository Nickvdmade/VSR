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

        public override object Clone()
        {
            return new Xor();
        }
    }
}