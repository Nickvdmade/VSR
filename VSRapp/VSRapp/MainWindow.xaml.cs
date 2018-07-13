using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace VSRapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Point startingPoint_;
        private static Point endPoint_;
        private static Boolean itemSelected_;
        private static Boolean horizontal_;
        private static Boolean vertical_;
        private static Image image_;
        private static Canvas circuitCanvas_;
        private Boolean circuitSaved_ = true;

        private static double movement = 5;

        public MainWindow()
        {
            InitializeComponent();

            Closing += closeWindow;

            circuitCanvas_ = CircuitCanvas;

            // Show list of possible nodes
            Dictionary<String, Node> listNodes = FactoryMethod<String, Node>.getList();
            foreach (var node in listNodes)
            {
                NodeList.Items.Add(node.Key);
            }
        }

        #region Canvas operations

        #region Adding and removing
        /// <summary>
        /// Add Image to canvas and link functions to image
        /// Set position of text relative to image
        /// </summary>
        public static void addImage(Image image, TextBlock text)
        {
            // Link functions to image
            image.MouseLeftButtonDown += selectItemHorizontal;
            image.MouseLeftButtonUp += deselectItemHorizontal;
            image.MouseRightButtonDown += selectItemVertical;
            image.MouseRightButtonUp += deselectItemVertical;
            image.MouseMove += moveItem;

            // Add image and text to canvas
            circuitCanvas_.Children.Add(image);
            circuitCanvas_.Children.Add(text);

            // Set position of text relative to image
            Point location = image.TranslatePoint(new Point(0, 0), circuitCanvas_);
            Canvas.SetLeft(text, location.X);
            Canvas.SetTop(text, location.Y + image.Height);
        }

        /// <summary>
        /// Remove item from canvas
        /// </summary>
        public static void removeElement(UIElement toRemove)
        {
            circuitCanvas_.Children.Remove(toRemove);
        }

        /// <summary>
        /// Clear canvas for new circuit
        /// </summary>
        public static void clearCanvas()
        {
            circuitCanvas_.Children.Clear();
        }

        #endregion

        #region Image moving
        /// <summary>
        /// Set variables for horizontal movement
        /// </summary>
        private static void selectItemHorizontal(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Only able to move in one direction at a time
            if (!vertical_)
            {
                startingPoint_ = Mouse.GetPosition(circuitCanvas_);
                itemSelected_ = true;
                horizontal_ = true;
                image_ = sender as Image;
            }
        }

        /// <summary>
        /// Set variables for no movement
        /// </summary>
        private static void deselectItemHorizontal(object sender, MouseButtonEventArgs e)
        {
            // Only when horizontal_ is true
            if (horizontal_)
            {
                itemSelected_ = false;
                horizontal_ = false;
                image_ = null;
            }
        }

        /// <summary>
        /// Set variables for vertical movement
        /// </summary>
        private static void selectItemVertical(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Only able to move in one direction at a time
            if (!horizontal_)
            {
                startingPoint_ = Mouse.GetPosition(circuitCanvas_);
                itemSelected_ = true;
                vertical_ = true;
                image_ = sender as Image;
            }
        }

        /// <summary>
        /// Set variables for no movement
        /// </summary>
        private static void deselectItemVertical(object sender, MouseButtonEventArgs e)
        {
            // Only if vertical_ is true
            if (vertical_)
            {
                itemSelected_ = false;
                vertical_ = false;
                image_ = null;
            }
        }

        /// <summary>
        /// Move selected item and
        /// </summary>
        private static void moveItem(object sender, MouseEventArgs e)
        {
            // Only if an item is selected
            if (itemSelected_)
            {
                // Make movement steps
                endPoint_ = Mouse.GetPosition(circuitCanvas_);
                if (horizontal_)
                {
                    double x = endPoint_.X - startingPoint_.X;
                    if (Math.Abs(x) >= movement)
                    {
                        if (x > 0)
                            x = movement;
                        else
                            x = -movement;
                        Point location = image_.TranslatePoint(new Point(0, 0), circuitCanvas_);
                        Canvas.SetLeft(image_, location.X + x);
                        startingPoint_ = endPoint_;
                    }
                }
                if (vertical_)
                {
                double y = endPoint_.Y - startingPoint_.Y;
                    if (Math.Abs(y) >= movement)
                    {
                        if (y > 0)
                            y = movement;
                        else
                            y = -movement;
                        Point location = image_.TranslatePoint(new Point(0, 0), circuitCanvas_);
                        Canvas.SetTop(image_, location.Y + y);
                        startingPoint_ = endPoint_;
                    }
                }

                // Update connection lines and text position
                Circuit.updateConnections(image_);
                updateText();
            }
        }

        /// <summary>
        /// Update text position relative to selected image
        /// </summary>
        private static void updateText()
        {
            TextBlock text = Circuit.getRelativeText(image_);
            Point location = image_.TranslatePoint(new Point(0, 0), circuitCanvas_);
            Canvas.SetLeft(text, location.X);
            Canvas.SetTop(text, location.Y + image_.Height);
        }

        /// <summary>
        /// Add line for connection on canvas
        /// </summary>
        public static Line addConnectionLine(Node fromNode, Node toNode, Image fromImage, Image toImage)
        {
            Point from = fromImage.TranslatePoint(new Point(0, 0), circuitCanvas_);
            Point to = toImage.TranslatePoint(new Point(0, 0), circuitCanvas_);
            from = fromNode.getOutputPoint(from, fromImage);
            to = toNode.getInputPoint(to, toImage, fromNode);
            Line line = new Line();
            line.Stroke = Brushes.Black;
            line.X1 = from.X;
            line.Y1 = from.Y;
            line.X2 = to.X;
            line.Y2 = to.Y;
            line.StrokeThickness = 1;
            circuitCanvas_.Children.Add(line);
            return line;
        }

        /// <summary>
        /// Remove line for connection from canvas
        /// </summary>
        public static void removeConnectionLine(Line line)
        {
            circuitCanvas_.Children.Remove(line);
        }

        /// <summary>
        /// Update start or end position of connection line
        /// </summary>
        public static void updateConnectionLine(Line line, Connection connection, Node node, Image image)
        {
            Point start = new Point(line.X1, line.Y1);
            Point end = new Point(line.X2, line.Y2);
            if (connection.getFrom() == node)
            {
                Point point = image.TranslatePoint(new Point(0, 0), circuitCanvas_);
                start = node.getOutputPoint(point, image);
                line.X1 = start.X;
                line.Y1 = start.Y;
            }
            if (connection.getTo() == node)
            {
                Point point = image.TranslatePoint(new Point(0, 0), circuitCanvas_);
                end = node.getInputPoint(point, image, connection.getFrom());
                line.X2 = end.X;
                line.Y2 = end.Y;
            }
        }

        #endregion

        #endregion

        #region Node operations
        /// <summary>
        /// Item selected in list of nodes has changed
        /// </summary>
        private void nodeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Enable add node button if a node is selected
            if (NodeList.SelectedItem != null)
            {
                AddNode.IsEnabled = true;
                int amount = Circuit.getAmount();
                NodeName.Text = "Node" + amount; // Set standard name for the node
            }
            else
                AddNode.IsEnabled = false;
        }

        /// <summary>
        /// Adding a new node to the circuit
        /// </summary>
        private void addNode(object sender, RoutedEventArgs e)
        {
            // Get type and name of node
            String nodeName = (String) NodeList.SelectedItem;
            String name = NodeName.Text;

            // Create new node from factory
            Node node = FactoryMethod<String, Node>.create(nodeName);
            node.setName(name);
            Circuit.addNode(name, node);

            updateConnectionList();
            
            NodeList.UnselectAll();
            circuitSaved_ = false;
        }

        /// <summary>
        /// Remove node from circuit
        /// </summary>
        private void removeNode(object sender, RoutedEventArgs e)
        {
            String name = NodeName.Text;
            Circuit.removeNode(name);
            updateConnectionList();
            circuitSaved_ = false;
        }

        #endregion

        #region Connection operations
        /// <summary>
        /// Update the connection lists and buttons
        /// </summary>
        private void updateConnectionList()
        {
            // Get the list of nodes currently in the circuit
            Dictionary<String, Node> circuitList = Circuit.getList();

            // Clear the lists of nodes to get rid of removed nodes
            FromNode.Items.Clear();
            ToNode.Items.Clear();

            // Only enable if there are more than two nodes in the circuit
            if (circuitList.Count > 1)
            {
                foreach (var item in circuitList)
                {
                    FromNode.Items.Add(item.Key);
                    ToNode.Items.Add(item.Key);
                }
                FromNode.IsEnabled = true;
                ToNode.IsEnabled = true;
                AddConnection.IsEnabled = true;
                RemoveConnection.IsEnabled = true;
            }
            else
            {
                FromNode.IsEnabled = false;
                ToNode.IsEnabled = false;
                AddConnection.IsEnabled = false;
                RemoveConnection.IsEnabled = false;
            }
        }

        /// <summary>
        /// Add connection to circuit
        /// </summary>
        private void addConnection(object sender, RoutedEventArgs e)
        {
            String fromNode = FromNode.Text;
            String toNode = ToNode.Text;
            Circuit.addConnection(fromNode, toNode);
            circuitSaved_ = false;
        }

        /// <summary>
        /// Remove connection from circuit
        /// </summary>
        private void removeConnection(object sender, RoutedEventArgs e)
        {
            String fromNode = FromNode.Text;
            String toNode = ToNode.Text;
            Circuit.removeConnection(fromNode, toNode);
            circuitSaved_ = false;
        }

        #endregion

        #region Toolbar buttons

        private void newCircuit(object sender, RoutedEventArgs e)
        {
            if (!circuitSaved_)
            {
                MessageBoxResult result = MessageBox.Show("Would you like to save the current circuit?", "New Circuit",
                    MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                {
                    saveCircuit();
                }
                if (result == MessageBoxResult.Cancel)
                    return;
            }
            Circuit.clearCircuit();
            circuitSaved_ = false;
        }

        private void saveCircuitButton(object sender, RoutedEventArgs routedEventArgs)
        {
            saveCircuit();
        }

        private void saveCircuit()
        {
            Dictionary<String, FileType> fileTypes = FactoryMethod<String, FileType>.getList();
            String filter = "";

            foreach (var type in fileTypes)
            {
                filter += type.Value.getFilter();
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = filter.Substring(0, filter.LastIndexOf('|'));
            saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

            if (saveFileDialog.ShowDialog() == true)
            {
                String fileName = saveFileDialog.FileName;
                int index = fileName.LastIndexOf('.') + 1;
                String extension = fileName.Substring(index, fileName.Length - index);

                fileTypes[extension].save(fileName, Circuit.getList());
                circuitSaved_ = true;
            }
        }

        private void openCircuit(object sender, RoutedEventArgs e)
        {
            Dictionary<String, FileType> fileTypes = FactoryMethod<String, FileType>.getList();
            String filter = "";

            foreach (var type in fileTypes)
            {
                filter += type.Value.getFilter();
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = filter.Substring(0, filter.LastIndexOf('|'));
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

            if (openFileDialog.ShowDialog() == true)
            {
                String fileName = openFileDialog.FileName;
                int index = fileName.LastIndexOf('.') + 1;
                String extension = fileName.Substring(index, fileName.Length - index);

                fileTypes[extension].open(fileName);
            }

            updateConnectionList();
        }

        #endregion

        public void closeWindow(object sender, CancelEventArgs e)
        {
            if (!circuitSaved_)
            {
                MessageBoxResult result = MessageBox.Show("Would you like to save the current circuit?", "New Circuit",
                    MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    saveCircuit();
                }
            }
        }
    }
}
