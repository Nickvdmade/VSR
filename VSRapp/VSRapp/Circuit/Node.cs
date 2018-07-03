using System;
using System.Collections.Generic;
using System.Windows;

namespace VSRapp
{
    public abstract class Node : ICloneable, IGetKey<String>
    {
        private List<Node> inputNodes_;
        private List<Node> outputNodes_;
        private String name_;

        protected Node()
        {
        }

        public static Node create(String name)
        {
            return FactoryMethod<String, Node>.create(name);
        }

        public void setName(String name)
        {
            name_ = name;
        }

        public String getName()
        {
            return name_;
        }

        public void addInput(Node node)
        {
            if (inputNodes_.Count == 2)
            {
                MessageBox.Show(name_ + " can't have more than 2 inputs", "Too many inputs");
                return;
            }
            inputNodes_.Add(node);
            showConnections();
        }

        public void removeInput(Node node)
        {
            if (inputNodes_.Contains(node))
            {
                inputNodes_.Remove(node);
            }
            showConnections();
        }

        public void addOutput(Node node)
        {
            outputNodes_.Add(node);
            showConnections();
        }

        public void removeOutput(Node node)
        {
            if (outputNodes_.Contains(node))
            {
                outputNodes_.Remove(node);
            }
            showConnections();
        }

        private void showConnections()
        {
            String inputs = "";
            String outputs = "";
            foreach (var node in inputNodes_)
            {
                inputs += "\t" + node.getName();
            }
            foreach (var node in outputNodes_)
            {
                outputs += "\t" + node.getName();
            }
            MessageBox.Show("Connections to input" + inputs + "\nConnections from output" + outputs,
                "Inputs and Outputs");
        }

        public abstract String getKey();
        public abstract object Clone();
    }
}