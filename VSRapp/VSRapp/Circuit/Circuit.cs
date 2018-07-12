using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VSRapp
{
    public class Circuit
    {

        static private Circuit instance_;
        private Dictionary<String, Node> nodes_ = new Dictionary<String, Node>();
        private Dictionary<String, Image> images_ = new Dictionary<String, Image>();
        private Dictionary<String, TextBlock> texts_ = new Dictionary<string, TextBlock>();
        private Dictionary<Connection, Line> connections_ = new Dictionary<Connection, Line>();
        private int counter_ = 1;

        /// <summary>
        /// Add node to circuit
        /// </summary>
        public static void addNode(String name, Node node)
        {
            Circuit instance = Circuit.instance();

            // Check if name already exists in circuit
            if (!instance.nodes_.ContainsKey(name))
            {
                Dictionary<String, Node> nodes = instance.nodes_;
                nodes.Add(name, node);
                Image image = new Image();
                image.Source = new BitmapImage(node.getUri());

                // Width and height set hardcoded because dynamically gives 0 of NaN
                image.Width = 93;
                image.Height = 40;

                TextBlock text = new TextBlock();
                text.Text = name;
                MainWindow.addImage(image, text);
                instance.images_.Add(name, image);
                instance.texts_.Add(name, text);
                instance.counter_++; // Increase counter for standard name
                return;
            }
            MessageBox.Show("name already exists", "Add node");
        }

        /// <summary>
        /// Remove node from circuit
        /// </summary>
        public static void removeNode(String name)
        {
            Circuit instance = Circuit.instance();

            // Check if the name exists in circuit
            if (instance.hasNode(name))
            {
                Dictionary<String, Node> nodes = instance.nodes_;
                Node node = nodes[name];
                node.removeConnections();
                nodes.Remove(name);

                // Remove image and text from canvas
                Image image = instance.images_[name];
                MainWindow.removeElement(image);
                instance.images_.Remove(name);
                TextBlock text = instance.texts_[name];
                MainWindow.removeElement(text);
                instance.texts_.Remove(name);
                MessageBox.Show(name + " has been removed", "Remove node");
            }
        }

        /// <summary>
        /// Get text that is linked to image
        /// </summary>
        public static TextBlock getRelativeText(Image image)
        {
            Circuit instance = Circuit.instance();
            foreach (var item in instance.images_)
            {
                if (Equals(item.Value, image))
                    return instance.texts_[item.Key];
            }
            return null;
        }

        /// <summary>
        /// Get counter for standard name
        /// </summary>
        public static int getAmount()
        {
            return instance().counter_;
        }

        /// <summary>
        /// Get list of nodes in circuit
        /// </summary>
        public static Dictionary<String, Node> getList()
        {
            return instance().nodes_;
        }

        /// <summary>
        /// Add connection between two nodes
        /// </summary>
        public static void addConnection(String fromNode, String toNode)
        {
            Circuit instance = Circuit.instance();

            // Check if circuit has both nodes
            if (instance.hasNode(fromNode) && instance.hasNode(toNode))
            {
                Dictionary<String, Node> nodes = instance.nodes_;
                Node nodeTo = nodes[toNode];
                Node nodeFrom = nodes[fromNode];

                // Check if adding input is allowed
                if (!nodeTo.addInput(nodeFrom))
                    return;

                // Check if adding output is allowed
                if (!nodeFrom.addOutput(nodeTo))
                {
                    nodeTo.removeInput(nodeFrom);
                    return;
                }

                // Get relative images
                Dictionary<String, Image> images = instance.images_;
                Image fromImage = images[fromNode];
                Image toImage = images[toNode];

                Line line = MainWindow.addConnectionLine(nodeFrom, nodeTo, fromImage, toImage);

                Connection connection = new Connection(nodeFrom, nodeTo);
                instance.connections_.Add(connection, line);

                MessageBox.Show("Added connection from " + fromNode + " and " + toNode, "Add connection");
            }
        }

        /// <summary>
        /// Remove connection between two nodes
        /// </summary>
        public static void removeConnection(String fromNode, String toNode)
        {
            Circuit instance = Circuit.instance();
            
            // Check if circuit has both nodes
            if (instance.hasNode(fromNode) && instance.hasNode(toNode))
            {
                Dictionary<String, Node> nodes = instance.nodes_;
                // Check if connection is present
                if (!nodes[toNode].removeInput(nodes[fromNode]))
                {
                    MessageBox.Show("No connection from " + fromNode + " and " + toNode, "Remove connection");
                    return;
                }
                nodes[fromNode].removeOutput(nodes[toNode]);

                // Go through all connections to remove the needed connection
                Dictionary<Connection, Line> connections = instance.connections_;
                foreach (var item in connections)
                {
                    Connection connection = item.Key;
                    if (connection.getFrom() == nodes[fromNode])
                    {
                        if (connection.getTo() == nodes[toNode])
                        {
                            MainWindow.removeConnectionLine(connections[connection]);
                            connections.Remove(connection);
                            break;
                        }
                    }
                }

                MessageBox.Show("Removed connection from " + fromNode + " and " + toNode, "Remove connection");
            }
        }

        /// <summary>
        /// Update the position of the connection line
        /// </summary>
        public static void updateConnections(Image image, Canvas circuitCanvas)
        {
            // Search for the correct name
            Dictionary<String, Image> images = instance().images_;
            String name = null;
            foreach (var item in images)
            {
                if (Equals(item.Value, image))
                {
                    name = item.Key;
                    break;
                }
            }

            if (name == null)
                return;

            // Get list of connections to and from node
            Node node = instance().nodes_[name];
            Dictionary<Connection, Line> connectionLines = instance().connections_;
            List<Connection> connections = new List<Connection>();
            foreach (var item in connectionLines)
            {
                connections.Add(item.Key);
            }
            connections = node.filterConnections(connections);

            // Update connection lines
            foreach (var connection in connections)
            {
                Line line = connectionLines[connection];
                MainWindow.updateConnectionLine(line, connection, node, image);
            }
        }

        private Circuit()
        {
            nodes_ = new Dictionary<String, Node>();
        }

        /// <summary>
        /// Get instance of circuit
        /// </summary>
        private static Circuit instance()
        {
            if (instance_ == null)
            {
                instance_ = new Circuit();
            }
            return instance_;
        }

        /// <summary>
        /// Check if circuit has node
        /// </summary>
        private Boolean hasNode(String name)
        {
            if (nodes_.ContainsKey(name))
                return true;
            MessageBox.Show("No node with the name: " + name, "Unknown node name");
            return false;
        }

    }
}