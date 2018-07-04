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

        public Boolean addInput(Node node)
        {
            if (inputNodes_.Count == 2)
            {
                MessageBox.Show(name_ + " can't have more than 2 inputs", "Too many inputs");
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

        public void addOutput(Node node)
        {
            outputNodes_.Add(node);
        }

        public void removeOutput(Node node)
        {
            if (outputNodes_.Contains(node))
                outputNodes_.Remove(node);
        }

        public abstract String getKey();
        public abstract object Clone();
    }
}