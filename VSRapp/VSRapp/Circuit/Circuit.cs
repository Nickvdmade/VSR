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

        public static void addNode(String name, Node node)
        {
            Circuit instance = Circuit.instance();
            if (!instance.nodes_.ContainsKey(name))
            {
                Dictionary<String, Node> nodes = instance.nodes_;
                nodes.Add(name, node);
                Image image = new Image();
                image.Source = new BitmapImage(node.getUri());
                image.Width = 93;
                image.Height = 40;
                instance.images_.Add(name, image);
                TextBlock text = new TextBlock();
                text.Text = name;
                MainWindow.addImage(image, text);
                instance.texts_.Add(name, text);
                instance.counter_++;
                return;
            }
            MessageBox.Show("name already exists", "Add node");
        }

        public static void removeNode(String name)
        {
            Circuit instance = Circuit.instance();
            if (instance.hasNode(name))
            {
                Dictionary<String, Node> nodes = instance.nodes_;
                Node node = nodes[name];
                node.removeConnections();
                nodes.Remove(name);
                Image image = instance.images_[name];
                MainWindow.removeElement(image);
                instance.images_.Remove(name);
                TextBlock text = instance.texts_[name];
                MainWindow.removeElement(text);
                instance.texts_.Remove(name);
                MessageBox.Show(name + " has been removed", "Remove node");
            }
        }

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

        public static int getAmount()
        {
            return instance().counter_;
        }

        public static Dictionary<String, Node> getList()
        {
            return instance().nodes_;
        }

        public static void addConnection(String fromNode, String toNode, Canvas circuitCanvas)
        {
            if (instance().hasNode(fromNode) && instance().hasNode(toNode))
            {
                Dictionary<String, Node> nodes = instance().nodes_;
                Node nodeTo = nodes[toNode];
                Node nodeFrom = nodes[fromNode];
                if (!nodeTo.addInput(nodeFrom))
                    return;
                if (!nodeFrom.addOutput(nodeTo))
                {
                    nodeTo.removeInput(nodeFrom);
                    return;
                }

                Dictionary<String, Image> images = instance().images_;
                Image fromImage = images[fromNode];
                Image toImage = images[toNode];
                Point fromPoint = fromImage.TranslatePoint(new Point(0, 0), circuitCanvas);
                Point toPoint = toImage.TranslatePoint(new Point(0, 0), circuitCanvas);
                fromPoint = nodeFrom.getOutputPoint(fromPoint, fromImage);
                toPoint = nodeTo.getInputPoint(toPoint, toImage, nodeFrom);

                Line line = new Line();
                line.Stroke = Brushes.Black;
                line.X1 = fromPoint.X;
                line.Y1 = fromPoint.Y;
                line.X2 = toPoint.X;
                line.Y2 = toPoint.Y;
                line.StrokeThickness = 1;
                circuitCanvas.Children.Add(line);

                Connection connection = new Connection(nodeFrom, nodeTo);
                instance().connections_.Add(connection, line);

                MessageBox.Show("Added connection from " + fromNode + " and " + toNode, "Add connection");
            }
        }

        public static void removeConnection(string fromNode, string toNode, Canvas circuitCanvas)
        {
            if (instance().hasNode(fromNode) && instance().hasNode(toNode))
            {
                Dictionary<String, Node> nodes = instance().nodes_;
                if (!nodes[toNode].removeInput(nodes[fromNode]))
                {
                    MessageBox.Show("No connection from " + fromNode + " and " + toNode, "Remove connection");
                    return;
                }
                nodes[fromNode].removeOutput(nodes[toNode]);

                Dictionary<Connection, Line> connections = instance().connections_;
                foreach (var item in connections)
                {
                    Connection connection = item.Key;
                    if (connection.getFrom() == nodes[fromNode])
                    {
                        if (connection.getTo() == nodes[toNode])
                        {
                            circuitCanvas.Children.Remove(connections[connection]);
                            connections.Remove(connection);
                            break;
                        }
                    }
                }

                MessageBox.Show("Removed connection from " + fromNode + " and " + toNode, "Remove connection");
            }
        }

        public static void updateConnections(Image image, Canvas circuitCanvas)
        {
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

            Node node = instance().nodes_[name];
            Dictionary<Connection, Line> connectionLines = instance().connections_;
            List<Connection> connections = new List<Connection>();
            foreach (var item in connectionLines)
            {
                connections.Add(item.Key);
            }
            connections = node.filterConnections(connections);

            foreach (var connection in connections)
            {
                Line line = connectionLines[connection];
                Point start = new Point(line.X1, line.Y1);
                Point end = new Point(line.X2, line.Y2);
                if (connection.getFrom() == node)
                {
                    Point point = image.TranslatePoint(new Point(0, 0), circuitCanvas);
                    start = node.getOutputPoint(point, image);
                    line.X1 = start.X;
                    line.Y1 = start.Y;
                }
                if (connection.getTo() == node)
                {
                    Point point = image.TranslatePoint(new Point(0, 0), circuitCanvas);
                    end = node.getInputPoint(point, image, connection.getFrom());
                    line.X2 = end.X;
                    line.Y2 = end.Y;
                }
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

        private Boolean hasNode(String name)
        {
            if (nodes_.ContainsKey(name))
                return true;
            MessageBox.Show("No node with the name: " + name, "Unknown node name");
            return false;
        }

    }
}