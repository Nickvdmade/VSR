using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace VSRapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Point startingPoint;
        private static Point endPoint;
        private static Boolean itemSelected;
        private static Boolean horizontal;
        private static Boolean vertical;
        private static Image image;
        private static Canvas circuitCanvas;

        public MainWindow()
        {
            InitializeComponent();

            circuitCanvas = CircuitCanvas;

            Dictionary<String, Node> listNodes = FactoryMethod<String, Node>.getList();
            foreach (var node in listNodes)
            {
                NodeList.Items.Add(node.Key);
            }
        }

        public static void addImage(Image toAdd)
        {
            toAdd.MouseLeftButtonDown += selectItemHorizontal;
            toAdd.MouseLeftButtonUp += deselectItemHorizontal;
            toAdd.MouseRightButtonDown += selectItemVertical;
            toAdd.MouseRightButtonUp += deselectItemVertical;
            toAdd.MouseMove += moveItem;
            circuitCanvas.Children.Add(toAdd);
        }

        public static void removeImage(Image toRemove)
        {
            circuitCanvas.Children.Remove(toRemove);
        }

        private void nodeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NodeList.SelectedItem != null)
            {
                AddNode.IsEnabled = true;
                int amount = Circuit.getAmount() + 1;
                NodeName.Text = "Node" + amount;
            }
            else
                AddNode.IsEnabled = false;
        }

        private void addNode(object sender, RoutedEventArgs e)
        {
            String nodeName = (String) NodeList.SelectedItem;
            String name = NodeName.Text;
            Node node = FactoryMethod<String, Node>.create(nodeName);
            node.setName(name);
            Circuit.addNode(name, node);

            updateConnectionList();
            
            NodeList.UnselectAll();
        }

        private void removeNode(object sender, RoutedEventArgs e)
        {
            String name = NodeName.Text;
            Circuit.removeNode(name);
            updateConnectionList();
        }

        private void updateConnectionList()
        {
            Dictionary<String, Node> circuitList = Circuit.getList();
            OutputNode.Items.Clear();
            InputNode.Items.Clear();
            if (circuitList.Count > 0)
            {
                foreach (var item in circuitList)
                {
                    OutputNode.Items.Add(item.Key);
                    InputNode.Items.Add(item.Key);
                }
                OutputNode.IsEnabled = true;
                InputNode.IsEnabled = true;
                AddConnection.IsEnabled = true;
                RemoveConnection.IsEnabled = true;
            }
            else
            {
                OutputNode.IsEnabled = false;
                InputNode.IsEnabled = false;
                AddConnection.IsEnabled = false;
                RemoveConnection.IsEnabled = false;
            }
        }

        private void addConnection(object sender, RoutedEventArgs e)
        {
            String outputNode = OutputNode.Text;
            String inputNode = InputNode.Text;
            Circuit.addConnection(outputNode, inputNode);
        }

        private void removeConnection(object sender, RoutedEventArgs e)
        {
            String outputNode = OutputNode.Text;
            String inputNode = InputNode.Text;
            Circuit.removeConnection(outputNode, inputNode);
        }

        private static void selectItemHorizontal(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            startingPoint = Mouse.GetPosition(circuitCanvas);
            itemSelected = true;
            horizontal = true;
            image = sender as Image;
        }

        private static void deselectItemHorizontal(object sender, MouseButtonEventArgs e)
        {
            endPoint = Mouse.GetPosition(circuitCanvas);
            if (horizontal)
                if (Math.Abs(endPoint.X - startingPoint.X) >= 10)
                {
                    double x = endPoint.X - startingPoint.X;
                    Point testLocation = image.TranslatePoint(new Point(0, 0), circuitCanvas);
                    Canvas.SetLeft(image, testLocation.X + x);
                    startingPoint = endPoint;
                }
            
            itemSelected = false;
            horizontal = false;
            image = null;
        }

        private static void selectItemVertical(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            startingPoint = Mouse.GetPosition(circuitCanvas);
            itemSelected = true;
            vertical = true;
            image = sender as Image;
        }

        private static void deselectItemVertical(object sender, MouseButtonEventArgs e)
        {
            endPoint = Mouse.GetPosition(circuitCanvas);
            if (vertical)
                if (Math.Abs(endPoint.Y - startingPoint.Y) >= 10)
                {
                    double y = endPoint.Y - startingPoint.Y;
                    Point testLocation = image.TranslatePoint(new Point(0, 0), circuitCanvas);
                    Canvas.SetTop(image, testLocation.Y + y);
                    startingPoint = endPoint;
                }

            itemSelected = false;
            vertical = false;
            image = null;
        }

        private static void moveItem(object sender, MouseEventArgs e)
        {
            if (itemSelected)
            {
                endPoint = Mouse.GetPosition(circuitCanvas);
                if (horizontal)
                    if (Math.Abs(endPoint.X - startingPoint.X) >= 10)
                    {
                        double x = endPoint.X - startingPoint.X;
                        Point testLocation = image.TranslatePoint(new Point(0, 0), circuitCanvas);
                        Canvas.SetLeft(image, testLocation.X + x);
                        startingPoint = endPoint;
                    }
                if (vertical)
                    if (Math.Abs(endPoint.Y - startingPoint.Y) >= 10)
                    {
                        double y = endPoint.Y - startingPoint.Y;
                        Point testLocation = image.TranslatePoint(new Point(0, 0), circuitCanvas);
                        Canvas.SetTop(image, testLocation.Y + y);
                        startingPoint = endPoint;
                    }
            }
        }
    }
}
