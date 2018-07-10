using System;
using System.Windows;
using System.Windows.Controls;

namespace VSRapp
{
    public class InputHigh : Node
    {
        public InputHigh()
        {
        }

        public override bool addInput(Node node)
        {
            MessageBox.Show(name_ + " can't have inputs", "Add inputs");
            return false;
        }

        public override string getKey()
        {
            return "INPUTHIGH";
        }

        public override Uri getUri()
        {
            return new Uri(@"Nodes/Images/INPUT_HIGH.png", UriKind.Relative);
        }

        public override object Clone()
        {
            return new InputHigh();
        }
    }
}