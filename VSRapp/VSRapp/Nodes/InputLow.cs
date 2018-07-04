using System;
using System.Windows;

namespace VSRapp
{
    public class InputLow : Node
    {
        public InputLow()
        {
        }

        public override Boolean addInput(Node node)
        {
            MessageBox.Show(name_ + " can't have inputs", "Add inputs");
            return false;
        }

        public override string getKey()
        {
            return "INPUTLOW";
        }

        public override object Clone()
        {
            return new InputLow();
        }
    }
}