using System;
using System.Windows;

namespace VSRapp
{
    public class InputHigh : Node
    {
        public InputHigh()
        {
        }

        public override Boolean addInput(Node node)
        {
            MessageBox.Show(name_ + " can't have inputs", "Add inputs");
            return false;
        }

        public override string getKey()
        {
            return "INPUTHIGH";
        }

        public override object Clone()
        {
            return new InputHigh();
        }
    }
}