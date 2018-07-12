using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace VSRapp
{
    public abstract class Node : ICloneable, IGetKey<String>
    {
        protected List<Node> inputNodes_;
        protected List<Node> outputNodes_;
        protected Dictionary<Node, Line> outputLines_;
        protected String name_;
        protected int count_ = 0;

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

        public virtual bool addInput(Node node)
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

        public List<Node> getOutputs()
        {
            return outputNodes_;
        }

        /// <summary>
        /// Remove all connections to and from node
        /// </summary>
        public void removeConnections()
        {
            foreach (var node in inputNodes_)
                node.removeOutput(this);
            inputNodes_.Clear();
            foreach (var node in outputNodes_)
                node.removeInput(this);
            outputNodes_.Clear();
        }

        /// <summary>
        /// Get relative output point of node
        /// </summary>
        public Point getOutputPoint(Point relativePoint, Image image)
        {
            relativePoint.X += image.Width;
            relativePoint.Y += image.Height / 2;
            return relativePoint;
        }

        /// <summary>
        /// Get relative input point of node
        /// </summary>
        public virtual Point getInputPoint(Point relativePoint, Image image, Node fromNode)
        {
            int index = 0;
            foreach (var node in inputNodes_)
            {
                if (node == fromNode)
                {
                    if (index == 0)
                        relativePoint.Y += image.Height / 4;
                    if (index == 1)
                        relativePoint.Y += image.Height - image.Height / 4;
                    return relativePoint;
                }
                index++;
            }
            return relativePoint;
        }

        /// <summary>
        /// Get all connections to and from node
        /// </summary>
        public List<Connection> filterConnections(List<Connection> allConnections)
        {
            List<Connection> connections = new List<Connection>();
            foreach (var connection in allConnections)
            {
                if (connection.getTo() == this || connection.getFrom() == this)
                    connections.Add(connection);
            }
            return connections;
        }

        public Node useForSave()
        {
            count_++;
            if (count_ == inputNodes_.Count)
                return this;
            return null;
        }

        public abstract String getKey();
        public abstract Uri getUri();
        public abstract object Clone();
    }
}