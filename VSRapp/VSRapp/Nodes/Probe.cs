using System;
using System.Windows;

namespace VSRapp
{
    public class Probe : Node
    {
        public Probe()
        {
        }

        public override Boolean addOutput(Node node)
        {
            MessageBox.Show(name_ + " can't have outputs", "Add output");
            return false;
        }

        public override Boolean addInput(Node node)
        {
            if (inputNodes_.Count == 1)
            {
                MessageBox.Show(name_ + " can't have more than 1 input", "Add inputs");
                return false;
            }
            inputNodes_.Add(node);
            return true;
        }

        public override string getKey()
        {
            return "PROBE";
        }

        public override object Clone()
        {
            return new Probe();
        }
    }
}