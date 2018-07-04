using System;
using System.Collections.Generic;
using System.Windows;

namespace VSRapp
{
    public class Circuit
    {

        static private Circuit instance_ = null;
        private Dictionary<String, Node> nodes_ = new Dictionary<String, Node>();

        public static void addNode(String name, Node node)
        {
            Dictionary<String, Node> nodes = instance().nodes_;
            nodes.Add(name, node);
        }

        public static void removeNode(String name)
        {
            if (instance().hadNode(name))
            {
                Dictionary<String, Node> nodes = instance().nodes_;
                Node node = nodes[name];
                node.removeConnections();
                nodes.Remove(name);
                MessageBox.Show(name + " has been removed", "Remove node");
            }
        }

        public static int getAmount()
        {
            Dictionary<String, Node> nodes = instance().nodes_;
            return nodes.Count;
        }

        public static Dictionary<String, Node> getList()
        {
            return instance().nodes_;
        }

        public static void addConnection(String outputNode, String inputNode)
        {
            if (instance().hadNode(outputNode) && instance().hadNode(inputNode))
            {
                Dictionary<String, Node> nodes = instance().nodes_;
                if (!nodes[inputNode].addInput(nodes[outputNode]))
                    return;
                nodes[outputNode].addOutput(nodes[inputNode]);
                MessageBox.Show("Added connection between " + outputNode + " and " + inputNode, "Add connection");
            }
        }

        public static void removeConnection(String outputNode, String inputNode)
        {
            if (instance().hadNode(outputNode) && instance().hadNode(inputNode))
            {
                Dictionary<String, Node> nodes = instance().nodes_;
                if (!nodes[inputNode].removeInput(nodes[outputNode]))
                {
                    MessageBox.Show("No connection between " + outputNode + " and " + inputNode, "Remove connection");
                    return;
                }
                nodes[outputNode].removeOutput(nodes[inputNode]);
                MessageBox.Show("Removed connection between " + outputNode + " and " + inputNode, "Remove connection");
            }
        }

        private Circuit()
        {
            nodes_ = new Dictionary<String, Node>();
        }

        private static Circuit instance()
        {
            if (instance_ == null)
            {
                instance_ = new Circuit();
            }
            return instance_;
        }

        private Boolean hadNode(String name)
        {
            if (nodes_.ContainsKey(name))
                return true;
            MessageBox.Show("No node with the name: " + name, "Unknown node name");
            return false;
        }

    }
}