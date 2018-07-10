using System;
using System.Windows;
using System.Windows.Controls;

namespace VSRapp
{
    public class InputLow : Node
    {
        public InputLow()
        {
        }

        public override bool addInput(Node node)
        {
            MessageBox.Show(name_ + " can't have inputs", "Add inputs");
            return false;
        }

        public override string getKey()
        {
            return "INPUTLOW";
        }

        public override Uri getUri()
        {
            return new Uri(@"Nodes/Images/INPUT_LOW.png", UriKind.Relative);
        }

        public override object Clone()
        {
            return new InputLow();
        }
    }
}