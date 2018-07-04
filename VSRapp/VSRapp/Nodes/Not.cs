using System;
using System.Windows;

namespace VSRapp
{
    public class Not : Node
    {
        public Not()
        {
        }

        public override Boolean addInput(Node node)
        {
            if (inputNodes_.Count == 1)
            {
                MessageBox.Show(name_ + " can't have more than 1 inputs", "Add inputs");
                return false;
            }
            inputNodes_.Add(node);
            return true;
        }

        public override string getKey()
        {
            return "NOT";
        }

        public override object Clone()
        {
            return new Not();
        }
    }
}