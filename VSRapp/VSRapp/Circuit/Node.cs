using System;
using System.Collections.Generic;
using System.Windows;

namespace VSRapp
{
    public abstract class Node : ICloneable, IGetKey<String>
    {
        protected List<Node> inputNodes_;
        protected List<Node> outputNodes_;
        protected String name_;

        protected Node()
        {
            inputNodes_ = new List<Node>();
            outputNodes_ = new List<Node>();
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

        public virtual Boolean addInput(Node node)
        {
            if (inputNodes_.Count == 2)
            {
                MessageBox.Show(name_ + " can't have more than 2 inputs", "Add inputs");
                return false;
            }
            inputNodes_.Add(node);
            return true;
        }

        public Boolean removeInput(Node node)
        {
            if (!inputNodes_.Contains(node))
                return false;
            inputNodes_.Remove(node);
            return true;
        }

        public virtual Boolean addOutput(Node node)
        {
            outputNodes_.Add(node);
            return true;
        }

        public void removeOutput(Node node)
        {
            if (outputNodes_.Contains(node))
                outputNodes_.Remove(node);
        }

        public void removeConnections()
        {
            foreach (var node in inputNodes_)
                node.removeOutput(this);
            inputNodes_.Clear();
            foreach (var node in outputNodes_)
                node.removeInput(this);
            outputNodes_.Clear();
        }

        public abstract String getKey();
        public abstract object Clone();
    }
}