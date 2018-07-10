using System;
using System.Windows;
using System.Windows.Controls;

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

        public override bool addInput(Node node)
        {
            if (inputNodes_.Count == 1)
            {
                MessageBox.Show(name_ + " can't have more than 1 input", "Add inputs");
                return false;
            }
            inputNodes_.Add(node);
            return true;
        }

        public override Point getInputPoint(Point relativePoint, Image image, Node fromNode)
        {
            relativePoint.Y += image.Height / 2;
            return relativePoint;
        }

        public override string getKey()
        {
            return "PROBE";
        }

        public override Uri getUri()
        {
            return new Uri(@"Nodes/Images/PROBE.png", UriKind.Relative);
        }

        public override object Clone()
        {
            return new Probe();
        }
    }
}